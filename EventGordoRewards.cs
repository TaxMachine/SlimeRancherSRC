// Decompiled with JetBrains decompiler
// Type: EventGordoRewards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventGordoRewards : GordoRewardsBase
{
  [Tooltip("Fashion to attach to spawned slimes. (optional)")]
  public Fashion slimeFashion;
  [Tooltip("Number of EventGordo crates to spawn on break.")]
  public int cratesToSpawn;

  protected override IEnumerable<GameObject> SelectActiveRewardPrefabs()
  {
    LookupDirector lookupDirector = SRSingleton<GameContext>.Instance.LookupDirector;
    List<GameObject> gameObjectList = new List<GameObject>();
    gameObjectList.Add(lookupDirector.GetPrefab(PickOrnamentReward()));
    gameObjectList.AddRange(Enumerable.Repeat(lookupDirector.GetPrefab(HolidayModel.EventGordo.CRATE), cratesToSpawn));
    return gameObjectList;
  }

  protected override void OnInstantiatedReward(GameObject instance)
  {
    base.OnInstantiatedReward(instance);
    if (!(slimeFashion != null))
      return;
    AttachFashions component = instance.GetComponent<AttachFashions>();
    if (!(component != null))
      return;
    component.Attach(slimeFashion, true);
  }

  private Identifiable.Id PickOrnamentReward()
  {
    string id = GetComponent<IdHandler>().id;
    HolidayModel.EventGordo eventGordo = SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventGordos.FirstOrDefault(e => e.objectId == id);
    return eventGordo is HolidayModel.EventGordo.Fixed ? ((HolidayModel.EventGordo.Fixed) eventGordo).ornament : Randoms.SHARED.Pick(HolidayModel.EventGordo.RARE_ORNAMENTS);
  }
}
