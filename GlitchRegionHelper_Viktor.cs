// Decompiled with JetBrains decompiler
// Type: GlitchRegionHelper_Viktor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class GlitchRegionHelper_Viktor : 
  SRSingleton<GlitchRegionHelper_Viktor>,
  AmbianceDirector.TimeOfDay
{
  [Tooltip("Reference to the GlitchTerminalActivator in the scene.")]
  public GlitchTerminalActivator activator;

  public override void Awake()
  {
    base.Awake();
    SRSingleton<SceneContext>.Instance.RegionRegistry.ManageWithRegionSet(gameObject, RegionRegistry.RegionSetId.VIKTOR_LAB);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.RegionRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.RegionRegistry.ReleaseFromRegionSet(gameObject, RegionRegistry.RegionSetId.VIKTOR_LAB);
  }

  public void OnEnable() => SRSingleton<SceneContext>.Instance.AmbianceDirector.Register(this);

  public void OnDisable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.AmbianceDirector != null))
      return;
    SRSingleton<SceneContext>.Instance.AmbianceDirector.Deregister(this);
  }

  public float GetCurrentDayFraction_Position() => SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.ambianceTimeOfDay;

  public float GetCurrentDayFraction_Color() => SRSingleton<SceneContext>.Instance.TimeDirector.CurrDayFraction();
}
