// Decompiled with JetBrains decompiler
// Type: AmbianceDirectorZoneSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(menuName = "Ambiance Director/Zone Setting", fileName = "AmbianceDirectorZoneSetting")]
public class AmbianceDirectorZoneSetting : ScriptableObject
{
  public AmbianceDirector.Zone zone;
  public Color dayFogColor;
  public float dayFogDensity;
  public Color dayAmbientColor;
  public Color nightFogColor;
  public float nightFogDensity;
  public Color nightAmbientColor;
  public Color daySkyColor;
  public Color daySkyHorizon;
  public Color nightSkyColor;
  public Color nightSkyHorizon;

  public AmbianceDirectorZoneSetting Clone()
  {
    AmbianceDirectorZoneSetting instance = CreateInstance<AmbianceDirectorZoneSetting>();
    instance.zone = zone;
    instance.dayFogColor = dayFogColor;
    instance.dayFogDensity = dayFogDensity;
    instance.dayAmbientColor = dayAmbientColor;
    instance.nightFogColor = nightFogColor;
    instance.nightFogDensity = nightFogDensity;
    instance.nightAmbientColor = nightAmbientColor;
    instance.daySkyColor = daySkyColor;
    instance.daySkyHorizon = daySkyHorizon;
    instance.nightSkyColor = nightSkyColor;
    instance.nightSkyHorizon = nightSkyHorizon;
    return instance;
  }
}
