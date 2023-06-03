// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationIncinerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneProgramDestinationIncinerator : DroneProgramDestination<Incinerate>
{
  private double time;
  private int dropCount;

  public override int GetAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Aggregate(0, (agg, i) => agg + i.GetAshSpace());

  public override bool HasAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Any(i => i.GetAshSpace() > 0);

  protected override IEnumerable<Incinerate> GetDestinations(Identifiable.Id id, bool overflow) => drone.network.Plots.SelectMany(i => i.incinerators).Where(i => i.GetAshSpace() > 0);

  protected override IEnumerable<Incinerate> Prioritize(IEnumerable<Incinerate> destinations) => destinations.OrderBy(d => d, new Comparer().OrderByDescending(i => i.GetAshSpace()).OrderBy(i => (i.transform.position - drone.transform.position).sqrMagnitude));

  protected override IEnumerable<Orientation> GetTargetOrientations() => GetTargetOrientations_Gather(destination.gameObject, new GatherConfig()
  {
    fallbackOffset = Vector3.forward,
    distanceHorizontal = 2.5f
  });

  protected override Vector3 GetTargetPosition() => destination.transform.position;

  protected override void OnFirstAction()
  {
    base.OnFirstAction();
    dropCount = destination.GetAshSpace();
  }

  protected override bool OnAction_Deposit(bool overflow)
  {
    if (dropCount > 0 | overflow && timeDirector.HasReached(time))
    {
      Identifiable.Id slotName = drone.ammo.GetSlotName();
      time = timeDirector.HoursFromNow(0.00166666682f);
      drone.ammo.Decrement(slotName);
      destination.ProcessIncinerateResults(slotName, 1, destination.transform.position + (drone.transform.position - destination.transform.position).normalized * PhysicsUtil.RadiusOfObject(destination.gameObject) * 0.25f, Quaternion.identity);
      --dropCount;
    }
    return !overflow && dropCount <= 0;
  }

  public override FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward)
  {
    Incinerate incinerate = Prioritize(GetDestinations(id, overflow)).First();
    maxFastForward = overflow ? maxFastForward : Mathf.Min(maxFastForward, incinerate.GetAshSpace());
    incinerate.ProcessIncinerateResults(id, maxFastForward);
    return new FastForward_Response()
    {
      deposits = maxFastForward
    };
  }

  private class Comparer : SRComparer<Incinerate>
  {
  }
}
