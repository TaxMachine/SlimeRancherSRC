// Decompiled with JetBrains decompiler
// Type: DirectedCrateSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Linq;
using UnityEngine;

public class DirectedCrateSpawner : SRBehaviour
{
  private ZoneDirector zoneDirector;

  public void Start()
  {
    zoneDirector = GetComponentInParent<ZoneDirector>();
    zoneDirector.Register(this);
  }

  public GameObject Spawn(GameObject zoneCratePrefab)
  {
    RegionRegistry.RegionSetId regionSetId = zoneDirector.regionSetId;
    return SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventGordos.Any() && Randoms.SHARED.GetProbability(HolidayModel.EventGordo.CRATE_CHANCE) ? InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(HolidayModel.EventGordo.CRATE), regionSetId, transform.position, transform.rotation) : InstantiateActor(zoneCratePrefab, regionSetId, transform.position, transform.rotation);
  }
}
