// Decompiled with JetBrains decompiler
// Type: GlintController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlintController : RegisteredActorBehaviour, RegistryUpdateable, LiquidConsumer
{
  private static readonly Vector2 SPAWN_RADIUS = new Vector2(7.5f, 30f);
  private const float UPDATE_PERIOD = 0.166666672f;
  private const float TIME_SPAWN = 0.5f;
  private const float TIME_PHASE_BASE = 1f;
  private const float TIME_PHASE_READY = 0.5f;
  private const float TIME_PHASE_FREE = 0.5f;
  private const float FAST_FORWARD_MIN_SECONDS = 7200f;
  private GameObject suspendedGlintPrefab;
  private GameObject readyGlintPrefab;
  private GameObject freeGlintPrefab;
  private double nextUpdateTime;
  private double nextSpawnTime;
  private double previousUpdateTime;
  private TimeDirector timeDirector;
  private SlimeEmotions emotions;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private List<Glint> glints = new List<Glint>();

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateGlintAppearance;
    if (!(slimeAppearanceApplicator.Appearance != null))
      return;
    UpdateGlintAppearance(slimeAppearanceApplicator.Appearance);
  }

  public override void Start()
  {
    base.Start();
    float curr = emotions.GetCurr(SlimeEmotions.Emotion.AGITATION);
    nextSpawnTime = timeDirector.HoursFromNow(AdjustTime(0.5f, curr));
    Initialize(Phase.SUSPENDED, AdjustTime(1f, curr), curr);
    Initialize(Phase.READY, AdjustTime(0.5f, curr), curr);
    previousUpdateTime = timeDirector.WorldTime();
  }

  public override void OnEnable()
  {
    base.OnEnable();
    foreach (Glint glint in glints)
    {
      if (glint.gameObject != null)
        glint.gameObject.SetActive(true);
    }
  }

  public override void OnDisable()
  {
    base.OnDisable();
    foreach (Glint glint in glints)
    {
      if (glint.gameObject != null)
        glint.gameObject.SetActive(false);
    }
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    DestroyAllGlints();
  }

  public void RegistryUpdate()
  {
    if (!timeDirector.HasReached(nextUpdateTime))
      return;
    nextUpdateTime = timeDirector.HoursFromNow(0.166666672f);
    UpdateToTime(timeDirector.WorldTime());
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    DestroyAllGlints();
  }

  private static float AdjustTime(float hours, float agitation) => hours * (float) (1.0 - 0.800000011920929 * agitation);

  private void UpdateGlintAppearance(SlimeAppearance appearance)
  {
    suspendedGlintPrefab = appearance.GlintAppearance.suspendedGlintPrefab;
    readyGlintPrefab = appearance.GlintAppearance.readyGlintPrefab;
    freeGlintPrefab = appearance.GlintAppearance.freeGlintPrefab;
  }

  private void DestroyAllGlints()
  {
    foreach (Glint glint in glints)
    {
      if (glint.gameObject != null)
        RequestDestroy(glint.gameObject, "GlintController.DestroyAllGlints");
    }
    glints.Clear();
  }

  private void UpdateToTime(double time)
  {
    float curr = emotions.GetCurr(SlimeEmotions.Emotion.AGITATION);
    if (time - previousUpdateTime >= 7200.0)
    {
      DestroyAllGlints();
      Initialize(Phase.SUSPENDED, AdjustTime(1f, curr), curr);
      Initialize(Phase.READY, AdjustTime(0.5f, curr), curr);
      Initialize(Phase.FREE, AdjustTime(0.5f, curr), curr);
    }
    else
    {
      glints.RemoveAll(g => g.gameObject == null);
      foreach (Glint glint in glints)
      {
        if (TimeUtil.HasReached(time, glint.phaseTimes[Phase.FREE]))
        {
          RequestDestroy(glint.gameObject, "GlintDestroyer.UpdateToTime");
          glint.gameObject = null;
        }
        else if (glint.phase < Phase.FREE && TimeUtil.HasReached(time, glint.phaseTimes[Phase.READY]))
          ChangePhase(glint, Phase.FREE, time, curr);
        else if (glint.phase < Phase.READY && TimeUtil.HasReached(time, glint.phaseTimes[Phase.SUSPENDED]))
          ChangePhase(glint, Phase.READY, time, curr);
      }
      if (TimeUtil.HasReached(time, nextSpawnTime))
      {
        nextSpawnTime = TimeDirector.HoursFromTime(AdjustTime(0.5f, curr), time);
        glints.Add(ChangePhase(new Glint(), Phase.SUSPENDED, time, curr));
      }
    }
    previousUpdateTime = time;
  }

  private Glint ChangePhase(
    Glint instance,
    Phase phase,
    double currentTime,
    float agitation)
  {
    instance.phase = phase;
    if (instance.gameObject != null)
    {
      GameObject gameObject = Instantiate(phase, instance.gameObject.transform.position, instance.gameObject.transform.rotation);
      RequestDestroy(instance.gameObject, "GlintDestroyer.ChangePhase");
      instance.gameObject = gameObject;
    }
    else
    {
      Vector3 vector3 = UnityEngine.Random.insideUnitSphere * SPAWN_RADIUS.Lerp(agitation);
      vector3.y = Mathf.Abs(vector3.y);
      instance.gameObject = Instantiate(phase, transform.position + vector3, Quaternion.identity);
    }
    instance.phaseTimes = new Dictionary<Phase, double>();
    for (Phase phase1 = phase; phase1 <= Phase.FREE; ++phase1)
    {
      Dictionary<Phase, double> phaseTimes = instance.phaseTimes;
      int key = (int) phase1;
      double hours = phase1 == Phase.SUSPENDED ? AdjustTime(1f, agitation) : (phase1 == Phase.READY ? AdjustTime(0.5f, agitation) : 0.5);
      double time = currentTime;
      double num1;
      double num2 = num1 = TimeDirector.HoursFromTime((float) hours, time);
      phaseTimes[(Phase) key] = num1;
      currentTime = num2;
    }
    return instance;
  }

  private void Initialize(Phase phase, float phaseHours, float agitation)
  {
    int num = Mathf.RoundToInt(phaseHours / AdjustTime(0.5f, agitation));
    for (int index = 0; index < num; ++index)
      glints.Add(ChangePhase(new Glint(), phase, timeDirector.WorldTime() - Randoms.SHARED.GetFloat(phaseHours) * 3600.0, agitation));
  }

  private GameObject Instantiate(
    Phase phase,
    Vector3 position,
    Quaternion rotation)
  {
    GameObject original;
    switch (phase)
    {
      case Phase.SUSPENDED:
        original = suspendedGlintPrefab;
        break;
      case Phase.READY:
        original = readyGlintPrefab;
        break;
      case Phase.FREE:
        original = freeGlintPrefab;
        break;
      default:
        original = null;
        break;
    }
    Vector3 position1 = position;
    Quaternion rotation1 = rotation;
    GameObject gameObject = InstantiatePooledDynamic(original, position1, rotation1);
    Recycler component = gameObject.GetComponent<Recycler>();
    component.pool = SRSingleton<SceneContext>.Instance.fxPool;
    component.OnBeforeRecycle += OnBeforeRecycle;
    return gameObject;
  }

  private void OnBeforeRecycle(GameObject gameObject)
  {
    gameObject.GetComponent<Recycler>().OnBeforeRecycle -= OnBeforeRecycle;
    glints.First(g => g.gameObject == gameObject).gameObject = null;
  }

  private enum Phase
  {
    SUSPENDED,
    READY,
    FREE,
  }

  private class Glint
  {
    public GameObject gameObject;
    public Dictionary<Phase, double> phaseTimes;
    public Phase phase;
  }
}
