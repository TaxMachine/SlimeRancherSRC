// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationPlortMarket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneProgramDestinationPlortMarket : DroneProgramDestination<ScorePlort>
{
  private double time;
  private EconomyDirector economyDirector;
  private CatcherOrientation orientation;

  public override void Awake()
  {
    base.Awake();
    economyDirector = SRSingleton<SceneContext>.Instance.EconomyDirector;
  }

  public override void Deselected()
  {
    base.Deselected();
    if (orientation == null)
      return;
    orientation.Dispose();
    orientation = null;
  }

  public override int GetAvailableSpace(Identifiable.Id id) => !GetDestinations(id, false).Any() ? 0 : MAX_AVAIL_TO_REPORT;

  protected override IEnumerable<ScorePlort> GetDestinations(Identifiable.Id id, bool overflow) => drone.network.Markets.Where(s => s.CanDeposit(id, true));

  protected override IEnumerable<Orientation> GetTargetOrientations()
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    DroneProgramDestinationPlortMarket destinationPlortMarket = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    destinationPlortMarket.orientation = destinationPlortMarket.GetTargetOrientation_Catcher(destinationPlortMarket.destination.gameObject);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = destinationPlortMarket.orientation.orientation;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield return new Orientation();
  }

  protected override Vector3 GetTargetPosition() => destination.transform.position;

  protected override bool OnAction_Deposit(bool overflow)
  {
    if (!timeDirector.HasReached(time) || economyDirector.IsMarketShutdown())
      return false;
    if (destination.Deposit(drone.ammo.GetSlotName(), coinsTypeOverride: new PlayerState.CoinsType?(PlayerState.CoinsType.DRONE)))
    {
      time = timeDirector.HoursFromNow(0.00166666682f);
      int num = (int) drone.ammo.Pop();
      return false;
    }
    return true;
  }

  public override FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward)
  {
    ScorePlort.Deposit_Response depositResponse = Prioritize(GetDestinations(id, overflow)).First().Deposit(id, maxFastForward, new PlayerState.CoinsType?(PlayerState.CoinsType.NONE), true);
    return new FastForward_Response()
    {
      deposits = depositResponse.deposits,
      currency = depositResponse.currency
    };
  }

  protected override IEnumerable<ScorePlort> Prioritize(IEnumerable<ScorePlort> destinations) => destinations.OrderBy(d => d, new Comparer().OrderBy(m => (m.transform.position - drone.transform.position).sqrMagnitude));

  private class Comparer : SRComparer<ScorePlort>
  {
  }
}
