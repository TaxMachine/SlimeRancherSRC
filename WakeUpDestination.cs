// Decompiled with JetBrains decompiler
// Type: WakeUpDestination
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class WakeUpDestination : SRBehaviour
{
  [Tooltip("Region associated with the WakeUpDestination. (unique)")]
  public RegionRegistry.RegionSetId deathRegionSetId;
  private SceneContext sceneContext;
  private RegionRegistry.RegionSetId? regionSetId;

  public void Awake()
  {
    sceneContext = SRSingleton<SceneContext>.Instance;
    sceneContext.Register(this);
  }

  public void OnDestroy() => sceneContext.Deregister(this);

  public RegionRegistry.RegionSetId GetRegionSetId()
  {
    if (!regionSetId.HasValue)
      regionSetId = new RegionRegistry.RegionSetId?(GetRequiredComponentInParent<Region>().setId);
    return regionSetId.Value;
  }
}
