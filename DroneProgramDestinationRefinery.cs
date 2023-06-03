// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationRefinery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneProgramDestinationRefinery : DroneProgramDestination<SiloCatcher>
{
  private GadgetDirector gadgetDirector;
  private CatcherOrientation orientation;
  private double time;

  public override void Awake()
  {
    base.Awake();
    gadgetDirector = SRSingleton<SceneContext>.Instance.GadgetDirector;
  }

  public override void Deselected()
  {
    base.Deselected();
    if (orientation == null)
      return;
    orientation.Dispose();
    orientation = null;
  }

  public override int GetAvailableSpace(Identifiable.Id id) => !GetDestinations(id, false).Any() ? 0 : gadgetDirector.GetRefinerySpaceAvailable(id);

  public override bool HasAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Any() && gadgetDirector.HasRefinerySpaceAvailable(id);

  protected override IEnumerable<SiloCatcher> Prioritize(IEnumerable<SiloCatcher> destinations) => destinations.OrderBy(d => d, new Comparer().OrderBy(m => (m.transform.position - drone.transform.position).sqrMagnitude));

  protected override IEnumerable<SiloCatcher> GetDestinations(Identifiable.Id id, bool overflow)
  {
    DroneProgramDestinationRefinery destinationRefinery = this;
    if (overflow || destinationRefinery.gadgetDirector.HasRefinerySpaceAvailable(id))
    {
      foreach (SiloCatcher refineryCatcher in destinationRefinery.drone.network.RefineryCatchers)
        yield return refineryCatcher;
    }
  }

  protected override IEnumerable<Orientation> GetTargetOrientations()
  {
    /*// ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    DroneProgramDestinationRefinery destinationRefinery = this;
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
    destinationRefinery.orientation = destinationRefinery.GetTargetOrientation_Catcher(destinationRefinery.destination.gameObject);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = destinationRefinery.orientation.orientation;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield break;
  }

  protected override Vector3 GetTargetPosition() => destination.transform.position;

  protected override bool OnAction_Deposit(bool overflow)
  {
    if (!timeDirector.HasReached(time))
      return false;
    if (gadgetDirector.AddToRefinery(drone.ammo.GetSlotName(), 1, overflow) <= 0)
      return true;
    Identifiable.Id id = drone.ammo.Pop();
    time = timeDirector.HoursFromNow(0.00166666682f);
    return !overflow && !gadgetDirector.HasRefinerySpaceAvailable(id);
  }

  public override FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward)
  {
    maxFastForward = gadgetDirector.AddToRefinery(id, maxFastForward, overflow);
    return new FastForward_Response()
    {
      deposits = maxFastForward
    };
  }

  private class Comparer : SRComparer<SiloCatcher>
  {
  }
}
