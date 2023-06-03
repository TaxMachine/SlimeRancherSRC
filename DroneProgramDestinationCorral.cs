// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationCorral
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneProgramDestinationCorral : 
  DroneProgramDestination<DroneProgramDestinationCorral.Destination>
{
  private double time;
  private int dropCount;
  private const float FOODS_PER_SLIME = 1.2f;
  private const int MINIMUM_DELIVERY = 5;

  public override int GetAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Aggregate(0, (cd, m) => cd + m.available);

  public override bool HasAvailableSpace(Identifiable.Id id) => GetDestinations(id, false).Any(m => m.available > 0);

  protected override IEnumerable<Destination> GetDestinations(
    Identifiable.Id id,
    bool overflow)
  {
    return drone.network.Plots.Where(m => m.plot.typeId == LandPlot.Id.CORRAL).Select(m => new Destination(m, id)).Where(m => m.anyEat && m.available >= 5);
  }

  protected override IEnumerable<Destination> Prioritize(
    IEnumerable<Destination> destinations)
  {
    return destinations.OrderBy(d => d, Destination.Comparer.Default);
  }

  protected override bool CanCancel() => base.CanCancel() || destination.CanCancel();

  protected override IEnumerable<Orientation> GetTargetOrientations() => GetTargetOrientations(destination);

  protected static IEnumerable<Orientation> GetTargetOrientations(
    Destination destination)
  {
    yield return new Orientation(destination.metadata.plot.transform.position + Vector3.up * (destination.metadata.plot.HasUpgrade(LandPlot.Upgrade.WALLS) ? 6f : 3f), Quaternion.Euler(0.0f, Randoms.SHARED.GetInRange(0, 360), 0.0f));
  }

  protected override Vector3 GetTargetPosition() => destination.metadata.plot.transform.position;

  protected override void OnFirstAction()
  {
    base.OnFirstAction();
    time = timeDirector.HoursFromNow(0.0133333346f);
    dropCount = new Destination(destination.metadata, drone.ammo.GetSlotName()).available;
  }

  protected override bool OnAction_Deposit(bool overflow)
  {
    if (dropCount > 0 | overflow)
      dropCount -= OnAction_DumpAmmo(ref time) ? 1 : 0;
    return !overflow && dropCount <= 0;
  }

  public override FastForward_Response FastForward(
    Identifiable.Id id,
    bool overflow,
    double endTime,
    int maxFastForward)
  {
    Destination destination = Prioritize(GetDestinations(id, overflow)).First();
    maxFastForward = overflow ? maxFastForward : Mathf.Min(maxFastForward, destination.available);
    maxFastForward = RanchCellFastForwarder.FeedSlimes(destination.metadata, endTime, new RanchCellFastForwarder.FeedingSource.Basic(id, maxFastForward));
    return new FastForward_Response()
    {
      deposits = maxFastForward
    };
  }

  protected override DroneAnimator.Id animation => DroneAnimator.Id.IDLE;

  protected override DroneAnimatorState.Id animationStateBegin => DroneAnimatorState.Id.NONE;

  protected override DroneAnimatorState.Id animationStateEnd => DroneAnimatorState.Id.NONE;

  public class Destination
  {
    public readonly DroneNetwork.LandPlotMetadata metadata;
    public readonly int available;
    public readonly bool anyEat;
    public readonly bool anyFavorite;

    public Destination(DroneNetwork.LandPlotMetadata metadata, Identifiable.Id id)
    {
      this.metadata = metadata;
      int num1 = 0;
      int num2 = 0;
      foreach (TrackContainedIdentifiables tracker in metadata.trackers)
      {
        foreach (KeyValuePair<Identifiable.Id, HashSet<Identifiable>> keyValuePair in tracker.GetAllTracked())
        {
          if (keyValuePair.Value.Any())
          {
            if (Identifiable.IsSlime(keyValuePair.Key))
            {
              if (!anyEat || !anyFavorite)
              {
                SlimeEat component = keyValuePair.Value.First().GetComponent<SlimeEat>();
                anyEat |= component.DoesEat(id);
                anyFavorite |= component.GetEatMapById(id).Any(e => e.isFavorite);
              }
              num1 += keyValuePair.Value.Count;
            }
            else if (Identifiable.IsFood(keyValuePair.Key))
              num2 += keyValuePair.Value.Count;
          }
        }
      }
      available = Mathf.Max(0, Mathf.Max(5, Mathf.CeilToInt(num1 * 1.2f)) - num2);
    }

    public bool CanCancel() => metadata.plot == null;

    public class Comparer : SRComparer<Destination>
    {
      public static Comparer<Destination> Default = new Comparer().OrderByDescending(m => m.anyFavorite).OrderBy(m => m.available);
    }
  }
}
