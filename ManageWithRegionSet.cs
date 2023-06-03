// Decompiled with JetBrains decompiler
// Type: ManageWithRegionSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class ManageWithRegionSet : MonoBehaviour
{
  public RegionRegistry.RegionSetId setId;

  public void Awake() => SRSingleton<SceneContext>.Instance.RegionRegistry.ManageWithRegionSet(gameObject, setId);

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.RegionRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.RegionRegistry.ReleaseFromRegionSet(gameObject, setId);
  }
}
