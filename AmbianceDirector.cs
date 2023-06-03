// Decompiled with JetBrains decompiler
// Type: AmbianceDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmbianceDirector : SRBehaviour, WorldModel.Participant, AmbianceDirector.TimeOfDay
{
  public static OnAwakeDelegate onAwakeDelegate;
  public AmbianceDirectorZoneSetting[] zoneSettings;
  public float zoneSettingTransitionTime = 1f;
  [Tooltip("The fog density while the camera is in the water.")]
  public float waterFogDensity = 0.24f;
  [Tooltip("The fog density while the camera is in the jelly sea.")]
  public float seaFogDensity = 0.36f;
  [Tooltip("The fog color while the camera is in the water or sea.")]
  public Color waterFogColor = Color.black;
  [Tooltip("The color to shift lights to at dusk time.")]
  public Color duskLightColor;
  public WeatherEntry[] weatherEntries;
  [Tooltip("Whether weather is active.")]
  public bool weatherEnabled = true;
  [Tooltip("Min hours before reselecting a weather type.")]
  public float minWeatherCycleHours = 3f;
  [Tooltip("Max hours before reselecting a weather type.")]
  public float maxWeatherCycleHours = 12f;
  private float caveDarkness;
  private Dictionary<Zone, int> caveTypeCounts = new Dictionary<Zone, int>();
  private bool firestormActive;
  private int waterCount;
  private int seaCount;
  private TimeDirector timeDir;
  private Dictionary<Weather, GameObject> weatherPrefabs = new Dictionary<Weather, GameObject>();
  private WeatherEffectAttachment weatherAttach;
  private Dictionary<Light, Color> duskedLightDefaultColors = new Dictionary<Light, Color>();
  private Dictionary<Zone, AmbianceDirectorZoneSetting> zoneDict;
  private List<DaynessListener> daynessListeners = new List<DaynessListener>();
  private List<TimeOfDay> timeOfDay = new List<TimeOfDay>();
  private AmbianceDirectorZoneSetting currZoneSetting;
  private AmbianceDirectorZoneSetting transitionFromZoneSetting;
  private Zone transitionToZone;
  private float transitionToZoneTime;
  private const float MINS_PER_FADE = 5f;
  private const float LIGHT_FADE_SPEED = 288f;
  private int skyboxBlendId;
  private int skyboxDaynessId;
  private int skyboxDayColorId;
  private int skyboxDayHorizonId;
  private int skyboxNightColorId;
  private int skyboxNightHorizonId;
  private List<Rotator> rotators = new List<Rotator>();
  private WorldModel worldModel;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    timeOfDay.Insert(0, this);
    foreach (WeatherEntry weatherEntry in weatherEntries)
      weatherPrefabs[weatherEntry.weather] = weatherEntry.prefab;
    if (onAwakeDelegate != null)
      onAwakeDelegate(this);
    if (weatherAttach == null && SRSingleton<SceneContext>.Instance.Player != null)
      weatherAttach = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeatherEffectAttachment>();
    foreach (Light key in duskedLightDefaultColors.Keys)
      duskedLightDefaultColors[key] = key.color;
    zoneDict = zoneSettings.ToDictionary(m => m.zone, m => m);
    skyboxBlendId = Shader.PropertyToID("_Blend");
    skyboxDaynessId = Shader.PropertyToID("_Dayness");
    skyboxDayColorId = Shader.PropertyToID("_SkyColor");
    skyboxDayHorizonId = Shader.PropertyToID("_HorizonColor");
    skyboxNightColorId = Shader.PropertyToID("_SkyColorNight");
    skyboxNightHorizonId = Shader.PropertyToID("_HorizonNight");
    InitSkybox();
  }

  public void InitModel(WorldModel worldModel)
  {
    worldModel.currWeather = Weather.NONE;
    worldModel.weatherUntil = timeDir.HoursFromNowOrStart(Randoms.SHARED.GetInRange(minWeatherCycleHours, maxWeatherCycleHours));
  }

  public void SetModel(WorldModel worldModel)
  {
    this.worldModel = worldModel;
    if (!(SRSingleton<SceneContext>.Instance.Player != null))
      return;
    weatherAttach = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeatherEffectAttachment>();
    weatherAttach.SetWeather(weatherPrefabs[worldModel.currWeather]);
  }

  public void InitForLevel()
  {
    SRSingleton<SceneContext>.Instance.GameModel.RegisterWorldParticipant(this);
    InitSkybox();
    caveTypeCounts.Clear();
    waterCount = 0;
    seaCount = 0;
    caveDarkness = 0.0f;
    currZoneSetting = null;
    UpdateZoneSetting();
  }

  public void Update()
  {
    UpdateZoneSetting();
    TimeOfDay timeOfDay = this.timeOfDay.Last();
    float fractionPosition = timeOfDay.GetCurrentDayFraction_Position();
    float dayFractionColor = timeOfDay.GetCurrentDayFraction_Color();
    float num1;
    float num2;
    if (dayFractionColor < 0.25)
    {
      num1 = 0.0f;
      num2 = Mathf.Clamp((float) ((0.25 - dayFractionColor) * 288.0), 0.01f, 1f);
    }
    else if (dayFractionColor > 0.75)
    {
      num1 = 0.0f;
      num2 = Mathf.Clamp((float) ((dayFractionColor - 0.75) * 288.0), 0.01f, 1f);
    }
    else
    {
      num2 = 0.0f;
      num1 = Mathf.Clamp(Mathf.Min(dayFractionColor - 0.25f, 0.75f - dayFractionColor) * 288f, 0.01f, 1f);
    }
    if (caveTypeCounts.Count > 0 && caveDarkness < 1.0)
      caveDarkness = Mathf.Min(1f, caveDarkness + Time.deltaTime / zoneSettingTransitionTime);
    else if (caveTypeCounts.Count <= 0 && caveDarkness > 0.0)
      caveDarkness = Mathf.Max(0.0f, caveDarkness - Time.deltaTime / zoneSettingTransitionTime);
    float num3 = Mathf.Clamp((float) ((Mathf.Abs(0.5f - dayFractionColor) - 0.15000000596046448) * 5.0), 0.0f, 1f);
    float dayness = 1f - num3;
    float num4 = Mathf.Pow(num3, 6f);
    float num5 = 1f - num4;
    if (seaCount + waterCount > 0)
    {
      RenderSettings.fogColor = waterFogColor;
      RenderSettings.fogDensity = seaCount > 0 ? seaFogDensity : waterFogDensity;
    }
    else
    {
      RenderSettings.fogColor = Color.Lerp(currZoneSetting.dayFogColor, currZoneSetting.nightFogColor, num3);
      RenderSettings.fogDensity = (float) (num5 * (double) currZoneSetting.dayFogDensity + num4 * (double) currZoneSetting.nightFogDensity);
    }
    RenderSettings.ambientLight = Color.Lerp(currZoneSetting.dayAmbientColor, currZoneSetting.nightAmbientColor, num3);
    float x = (float) (360.0 * (fractionPosition - 0.5));
    foreach (Rotator rotator in rotators)
    {
      rotator.transform.rotation = rotator.defaultRot * Quaternion.Euler(x, 0.0f, 0.0f);
      foreach (Light childLight in rotator.childLights)
      {
        float intensityMod = (rotator.isNightLight ? num2 : num1) * (1f - caveDarkness);
        UpdateLightIntensity(childLight, intensityMod, rotator.defaultIntensities[childLight]);
      }
    }
    foreach (Light key in duskedLightDefaultColors.Keys)
      key.color = Color.Lerp(duskLightColor, duskedLightDefaultColors[key], Mathf.Clamp((float) ((dayness - 0.5) * 2.0), 0.0f, 1f));
    RenderSettings.skybox.SetFloat(skyboxBlendId, num3);
    RenderSettings.skybox.SetFloat(skyboxDaynessId, dayness);
    RenderSettings.skybox.SetColor(skyboxDayColorId, currZoneSetting.daySkyColor);
    RenderSettings.skybox.SetColor(skyboxDayHorizonId, currZoneSetting.daySkyHorizon);
    RenderSettings.skybox.SetColor(skyboxNightColorId, currZoneSetting.nightSkyColor);
    RenderSettings.skybox.SetColor(skyboxNightHorizonId, currZoneSetting.nightSkyHorizon);
    foreach (DaynessListener daynessListener in daynessListeners)
      daynessListener.SetDayness(dayness);
    if (!(weatherAttach != null) || !timeDir.HasReached(worldModel.weatherUntil) || !weatherEnabled)
      return;
    worldModel.currWeather = RandomWeather();
    weatherAttach.SetWeather(weatherPrefabs[worldModel.currWeather]);
    worldModel.weatherUntil = timeDir.HoursFromNowOrStart(Randoms.SHARED.GetInRange(minWeatherCycleHours, maxWeatherCycleHours));
  }

  private void UpdateLightIntensity(Light light, float intensityMod, float defaultIntensity)
  {
    float num = defaultIntensity * intensityMod;
    light.intensity = Mathf.Clamp(num, intensityMod == 0.0 ? 0.0f : 1f / 1000f, 1f);
  }

  private void UpdateZoneSetting()
  {
    if (SRSingleton<GameContext>.Instance == null || SRSingleton<SceneContext>.Instance.Player == null)
    {
      if (!(currZoneSetting == null))
        return;
      currZoneSetting = zoneDict[Zone.DEFAULT].Clone();
    }
    else
    {
      Zone key1 = Zone.DEFAULT;
      if (caveTypeCounts.Count > 0)
      {
        foreach (Zone key2 in caveTypeCounts.Keys)
        {
          if (key2 > key1)
            key1 = key2;
        }
      }
      else if (firestormActive)
      {
        key1 = Zone.FIRESTORM;
      }
      else
      {
        foreach (Region region in SRSingleton<SceneContext>.Instance.Player.GetComponent<RegionMember>().regions)
        {
          Zone ambianceZone = region.cellDir.ambianceZone;
          if (ambianceZone > key1)
            key1 = ambianceZone;
        }
      }
      AmbianceDirectorZoneSetting targetZoneSetting = zoneDict[key1];
      if (currZoneSetting == null)
      {
        currZoneSetting = targetZoneSetting.Clone();
        transitionToZone = key1;
        transitionToZoneTime = 1f;
      }
      else if (key1 != transitionToZone)
      {
        transitionToZoneTime = Mathf.Min(1f, Time.deltaTime / zoneSettingTransitionTime);
        transitionFromZoneSetting = currZoneSetting.Clone();
        transitionToZone = key1;
        AdjustZoneSettings(targetZoneSetting);
      }
      else
      {
        if (transitionToZoneTime >= 1.0)
          return;
        transitionToZoneTime = Mathf.Min(1f, transitionToZoneTime + Time.deltaTime / zoneSettingTransitionTime);
        AdjustZoneSettings(targetZoneSetting);
      }
    }
  }

  private void AdjustZoneSettings(AmbianceDirectorZoneSetting targetZoneSetting)
  {
    if (!(transitionFromZoneSetting != null))
      return;
    currZoneSetting.dayFogColor = AdjustZoneSetting(transitionFromZoneSetting.dayFogColor, targetZoneSetting.dayFogColor, transitionToZoneTime);
    currZoneSetting.dayFogDensity = AdjustZoneSetting(transitionFromZoneSetting.dayFogDensity, targetZoneSetting.dayFogDensity, transitionToZoneTime);
    currZoneSetting.dayAmbientColor = AdjustZoneSetting(transitionFromZoneSetting.dayAmbientColor, targetZoneSetting.dayAmbientColor, transitionToZoneTime);
    currZoneSetting.nightFogColor = AdjustZoneSetting(transitionFromZoneSetting.nightFogColor, targetZoneSetting.nightFogColor, transitionToZoneTime);
    currZoneSetting.nightFogDensity = AdjustZoneSetting(transitionFromZoneSetting.nightFogDensity, targetZoneSetting.nightFogDensity, transitionToZoneTime);
    currZoneSetting.nightAmbientColor = AdjustZoneSetting(transitionFromZoneSetting.nightAmbientColor, targetZoneSetting.nightAmbientColor, transitionToZoneTime);
    currZoneSetting.daySkyColor = AdjustZoneSetting(transitionFromZoneSetting.daySkyColor, targetZoneSetting.daySkyColor, transitionToZoneTime);
    currZoneSetting.daySkyHorizon = AdjustZoneSetting(transitionFromZoneSetting.daySkyHorizon, targetZoneSetting.daySkyHorizon, transitionToZoneTime);
    currZoneSetting.nightSkyColor = AdjustZoneSetting(transitionFromZoneSetting.nightSkyColor, targetZoneSetting.nightSkyColor, transitionToZoneTime);
    currZoneSetting.nightSkyHorizon = AdjustZoneSetting(transitionFromZoneSetting.nightSkyHorizon, targetZoneSetting.nightSkyHorizon, transitionToZoneTime);
  }

  private Color AdjustZoneSetting(Color origColor, Color targetColor, float t) => Color.Lerp(origColor, targetColor, t);

  private float AdjustZoneSetting(float origVal, float targetVal, float t) => Mathf.Lerp(origVal, targetVal, t);

  public void RegisterDaynessListener(DaynessListener listener) => daynessListeners.Add(listener);

  public void UnregisterDaynessListener(DaynessListener listener) => daynessListeners.Remove(listener);

  public void RegisterTimeOfDayRotator(GameObject rotator, bool isNightLight) => rotators.Add(new Rotator(rotator, isNightLight));

  public void RegisterDuskedLight(Light light) => duskedLightDefaultColors[light] = light.color;

  public void EnterCave(Zone caveZone) => caveTypeCounts[caveZone] = caveTypeCounts.Get(caveZone) + 1;

  public void ExitCave(Zone caveZone)
  {
    caveTypeCounts[caveZone] = caveTypeCounts.Get(caveZone) - 1;
    if (caveTypeCounts[caveZone] > 0)
      return;
    caveTypeCounts.Remove(caveZone);
  }

  public void SetFirestormActive(bool active) => firestormActive = active;

  public void EnterWater() => ++waterCount;

  public void ExitWater() => --waterCount;

  public void EnterSea() => ++seaCount;

  public void ExitSea() => --seaCount;

  public bool IsInWater() => waterCount > 0;

  public void ExitAllLiquid()
  {
    waterCount = 0;
    seaCount = 0;
  }

  public float PrecipitationRate() => worldModel.currWeather == Weather.RAIN ? 1f : 0.0f;

  private void InitSkybox()
  {
    rotators.Clear();
    if (!(RenderSettings.skybox != null))
      return;
    RenderSettings.skybox = new Material(RenderSettings.skybox);
  }

  private Weather RandomWeather() => Randoms.SHARED.Pick((Weather[]) Enum.GetValues(typeof (Weather)));

  public void Register(TimeOfDay instance) => timeOfDay.Add(instance);

  public void Deregister(TimeOfDay instance) => timeOfDay.Remove(instance);

  public float GetCurrentDayFraction_Position() => timeDir.CurrDayFraction();

  public float GetCurrentDayFraction_Color() => timeDir.CurrDayFraction();

  public delegate void OnAwakeDelegate(AmbianceDirector ambianceDir);

  public interface DaynessListener
  {
    void SetDayness(float dayness);
  }

  public interface TimeOfDay
  {
    float GetCurrentDayFraction_Position();

    float GetCurrentDayFraction_Color();
  }

  public enum Weather
  {
    NONE,
    RAIN,
  }

  public enum Zone
  {
    DEFAULT = 0,
    QUARRY = 1,
    MOSS = 2,
    DESERT = 3,
    RUINS = 4,
    WILDS = 5,
    OGDEN_RANCH = 6,
    VALLEY = 7,
    MOCHI_RANCH = 8,
    SLIMULATIONS = 9,
    VIKTOR_LAB = 10, // 0x0000000A
    AUX1 = 1000, // 0x000003E8
    AUX2 = 1001, // 0x000003E9
    FIRESTORM = 2000, // 0x000007D0
    CAVE = 10000, // 0x00002710
    CAVE_VOLCANIC = 10001, // 0x00002711
  }

  [Serializable]
  public class WeatherEntry
  {
    public Weather weather;
    public GameObject prefab;
  }

  private class Rotator
  {
    public GameObject gameObject;
    public Quaternion defaultRot;
    public Dictionary<Light, float> defaultIntensities = new Dictionary<Light, float>();
    public bool isNightLight;
    public Transform transform;
    public List<Light> childLights;

    public Rotator(GameObject gameObject, bool isNightLight)
    {
      this.gameObject = gameObject;
      defaultRot = gameObject.transform.rotation;
      this.isNightLight = isNightLight;
      transform = gameObject.transform;
      childLights = new List<Light>();
      foreach (Light componentsInChild in gameObject.GetComponentsInChildren<Light>(true))
      {
        if (componentsInChild != null)
        {
          childLights.Add(componentsInChild);
          defaultIntensities[componentsInChild] = componentsInChild.intensity;
        }
      }
    }
  }
}
