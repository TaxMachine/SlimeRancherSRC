// Decompiled with JetBrains decompiler
// Type: EventBreakOnImpact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class EventBreakOnImpact : BreakOnImpactBase
{
  protected override IEnumerable<GameObject> GetRewardPrefabs()
  {
    yield return SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(PickOrnamentReward());
  }

  private Identifiable.Id PickOrnamentReward()
  {
    HolidayModel.EventGordo eventGordo = Randoms.SHARED.Pick(SRSingleton<SceneContext>.Instance.GameModel.GetHolidayModel().eventGordos, null);
    if (eventGordo is HolidayModel.EventGordo.Default)
      return !Randoms.SHARED.GetProbability(HolidayModel.EventGordo.RARE_ORNAMENT_CHANCE) ? Randoms.SHARED.Pick(((HolidayModel.EventGordo.Default) eventGordo).commons) : Randoms.SHARED.Pick(HolidayModel.EventGordo.RARE_ORNAMENTS);
    if (eventGordo is HolidayModel.EventGordo.Fixed)
      return ((HolidayModel.EventGordo.Fixed) eventGordo).ornament;
    return !Randoms.SHARED.GetProbability(HolidayModel.EventGordo.RARE_ORNAMENT_CHANCE) ? Identifiable.Id.PINK_ORNAMENT : Randoms.SHARED.Pick(HolidayModel.EventGordo.RARE_ORNAMENTS);
  }
}
