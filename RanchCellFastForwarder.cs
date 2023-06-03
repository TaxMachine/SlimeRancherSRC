// Decompiled with JetBrains decompiler
// Type: RanchCellFastForwarder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (CellDirector))]
[RequireComponent(typeof (DroneNetwork))]
[RequireComponent(typeof (Region))]
public class RanchCellFastForwarder : IdHandler, RanchCellModel.Participant
{
  private RanchCellModel model;
  private static List<GameObject> HUNGRY_SLIMES = new List<GameObject>();
  private static List<Identifiable.Id> PRODUCED = new List<Identifiable.Id>();
  private static List<Identifiable.Id> COLLECTED = new List<Identifiable.Id>();
  private const double FASTFORWARD_MIN_HOURS = 2.0;
  private const double FASTFORWARD_MIN_SECS = 7200.0;
  private const double FASTFORWARD_CHUNK_HOURS = 4.0;
  private const double FASTFORWARD_CHUNK_SECS = 14400.0;
  private DroneNetwork network;
  private Region region;
  private TimeDirector timeDirector;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    network = GetComponent<DroneNetwork>();
    region = GetComponent<Region>();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterRanchCell(id, this);
  }

  public void Start()
  {
    region.onHibernationStateChanged += OnHibernationStateChanged;
    timeDirector.onFastForwardChanged += OnFastForwardChanged;
  }

  public void InitModel(RanchCellModel model)
  {
  }

  public void SetModel(RanchCellModel model) => this.model = model;

  public void Update()
  {
    if (!model.sleepingTime.HasValue)
      return;
    double num1 = model.sleepingTime.Value + 14400.0;
    if (!timeDirector.HasReached(num1) && timeDirector.IsFastForwarding())
      return;
    double num2 = Math.Min(num1, timeDirector.WorldTime());
    FastForwardDrones(model.sleepingTime.Value, num2);
    model.sleepingTime = timeDirector.IsFastForwarding() && AnyDronesActive(num2) ? new double?(num2) : new double?();
  }

  public void OnDestroy()
  {
    if (timeDirector != null)
    {
      timeDirector.onFastForwardChanged -= OnFastForwardChanged;
      timeDirector = null;
    }
    if (region != null)
    {
      region.onHibernationStateChanged -= OnHibernationStateChanged;
      region = null;
    }
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterRanchCell(id);
  }

  protected override string IdPrefix() => "ranch";

  private void OnHibernationStateChanged(bool hibernating)
  {
    if (hibernating)
      OnHibernation();
    else
      SRSingleton<GameContext>.Instance.StartCoroutine(OnFastForward());
  }

  private void OnHibernation()
  {
    if (model.hibernationTime.HasValue)
      return;
    model.hibernationTime = new double?(timeDirector.WorldTime());
  }

  private IEnumerator OnFastForward()
  {
    RanchCellFastForwarder source = this;
    if (source.model.hibernationTime.HasValue)
    {
      double startTime = source.model.hibernationTime.Value;
      double endTime = source.timeDirector.WorldTime();
      source.model.hibernationTime = new double?();
      if (endTime - startTime >= 7200.0)
      {
        try
        {
          DroneFastForwarder.FastForward_Pre(source);
          double chunkEndTime;
          for (; startTime < endTime; startTime = chunkEndTime)
          {
            chunkEndTime = source.AnyDronesActive(startTime) ? Math.Min(endTime, startTime + 14400.0) : endTime;
            source.FastForwardCorrals(startTime, chunkEndTime);
            source.FastForwardGardens(startTime, chunkEndTime);
            source.FastForwardPonds(startTime, chunkEndTime);
            yield return new WaitForFixedUpdate();
            source.FastForwardDrones(startTime, chunkEndTime);
          }
        }
        finally
        {
          DroneFastForwarder.FastForward_Post(this);
        }
      }
    }
  }

  private void FastForwardCorrals(double startTime, double endTime)
  {
    foreach (DroneNetwork.LandPlotMetadata plot in network.Plots)
    {
      if (plot.plot.typeId == LandPlot.Id.CORRAL)
        FeedSlimes(plot, endTime, new FeedingSource.AutoFeeder(plot, endTime), new FeedingSource.Dynamic(plot.trackers.First()));
    }
  }

  private void FastForwardPonds(double startTime, double endTime)
  {
    foreach (DroneNetwork.LandPlotMetadata plot in network.Plots)
    {
      if (plot.plot.typeId == LandPlot.Id.POND)
      {
        FeedingSource.LiquidSource componentInChildren = plot.plot.GetComponentInChildren<FeedingSource.LiquidSource>();
        FeedSlimes(plot, endTime, new RanchCellFastForwarder.FeedingSource.LiquidSource(componentInChildren));
      }
    }
  }

  public static int FeedSlimes(
    DroneNetwork.LandPlotMetadata metadata,
    double endTime,
    params FeedingSource[] sources)
  {
    int num = 0;
    HUNGRY_SLIMES.Clear();
    PRODUCED.Clear();
    COLLECTED.Clear();
    if (sources.Any(s => s.ids.Any()))
    {
      metadata.trackers.First().GetTrackedItemsOfClass(Identifiable.EATERS_CLASS, HUNGRY_SLIMES);
      while (HUNGRY_SLIMES.Any() && sources.Any(s => s.ids.Any()))
      {
        GameObject slime = Randoms.SHARED.Pluck(HUNGRY_SLIMES, null);
        slime.GetComponent<SlimeEmotions>().UpdateToTime(endTime);
        if (FeedSlime(metadata, slime, sources))
        {
          HUNGRY_SLIMES.Add(slime);
          ++num;
        }
      }
      HUNGRY_SLIMES.Clear();
      PRODUCED.Clear();
      COLLECTED.Clear();
    }
    return num;
  }

  private static bool FeedSlime(
    DroneNetwork.LandPlotMetadata metadata,
    GameObject slime,
    FeedingSource[] sources)
  {
    switch (Identifiable.GetId(slime))
    {
      case Identifiable.Id.PUDDLE_SLIME:
        return FeedSlime(metadata, slime.GetComponent<SlimeEatWater>(), sources);
      case Identifiable.Id.FIRE_SLIME:
        return FeedSlime(metadata, slime.GetComponent<SlimeEatAsh>(), sources);
      default:
        return FeedSlime(metadata, slime.GetComponent<SlimeEat>(), sources);
    }
  }

  private static bool FeedSlime(
    DroneNetwork.LandPlotMetadata metadata,
    SlimeEat eat,
    FeedingSource[] sources)
  {
    if (eat.WantsToEat())
    {
      PlortCollector componentInChildren = metadata.plot.GetComponentInChildren<PlortCollector>();
      foreach (FeedingSource source in sources)
      {
        foreach (Identifiable.Id id in source.ids)
        {
          if (eat.WillNowEat(id) && source.Selected(id))
          {
            PRODUCED = eat.GetProducedIds(id, PRODUCED);
            COLLECTED.Clear();
            if (componentInChildren != null)
              componentInChildren.FastForward(PRODUCED, COLLECTED);
            eat.EatImmediate(source.GetTarget(id), id, PRODUCED, COLLECTED, true);
            return true;
          }
        }
      }
    }
    return false;
  }

  private static bool FeedSlime(
    DroneNetwork.LandPlotMetadata metadata,
    SlimeEatWater eat,
    FeedingSource[] sources)
  {
    foreach (FeedingSource source in sources)
    {
      foreach (Identifiable.Id id in source.ids)
      {
        if (eat.WillNowEat(id) && source.Selected(id))
        {
          PRODUCED = eat.GetProducedIds(id, PRODUCED);
          COLLECTED.Clear();
          eat.EatImmediate(source.GetTarget(id), id, PRODUCED, COLLECTED, true);
          return true;
        }
      }
    }
    return false;
  }

  private static bool FeedSlime(
    DroneNetwork.LandPlotMetadata metadata,
    SlimeEatAsh eat,
    FeedingSource[] sources)
  {
    return false;
  }

  private void FastForwardGardens(double startTime, double endTime)
  {
    foreach (DroneNetwork.LandPlotMetadata plot in network.Plots)
    {
      if (plot.plot.typeId == LandPlot.Id.GARDEN)
      {
        SpawnResource componentInChildren = plot.plot.GetComponentInChildren<SpawnResource>();
        if (componentInChildren != null)
          componentInChildren.FastForward(startTime, endTime);
      }
    }
  }

  private void FastForwardDrones(double startTime, double endTime)
  {
    foreach (Drone drone in network.Drones)
      DroneFastForwarder.FastForward(drone, startTime, endTime);
  }

  private bool AnyDronesActive(double time) => network.Drones.Any(d => d.station.battery.Time > time);

  private void OnFastForwardChanged(bool isFastForwarding)
  {
    if (isFastForwarding)
    {
      if (region.Hibernated || !AnyDronesActive(timeDirector.WorldTime()))
        return;
      DroneFastForwarder.FastForward_Pre(this);
      model.sleepingTime = new double?(timeDirector.WorldTime());
    }
    else
      SRSingleton<SceneContext>.Instance.StartCoroutine(OnFastForwardChangedCoroutine());
  }

  private IEnumerator OnFastForwardChangedCoroutine()
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    RanchCellFastForwarder source = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      DroneFastForwarder.FastForward_Post(source);
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForEndOfFrame();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;*/
    yield return new WaitForEndOfFrame();
  }

  public abstract class FeedingSource
  {
    public abstract IEnumerable<Identifiable.Id> ids { get; }

    public virtual bool Selected(Identifiable.Id id) => true;

    public virtual GameObject GetTarget(Identifiable.Id id) => null;

    public class Basic : FeedingSource
    {
      protected Identifiable.Id id;
      protected int remaining;

      public override IEnumerable<Identifiable.Id> ids
      {
        get
        {
          if (remaining > 0)
            yield return id;
        }
      }

      public Basic()
      {
      }

      public Basic(Identifiable.Id id, int remaining)
      {
        this.id = id;
        this.remaining = remaining;
      }

      public override bool Selected(Identifiable.Id id)
      {
        if (!base.Selected(id))
          return false;
        --remaining;
        return true;
      }
    }

    public class AutoFeeder : Basic
    {
      private SlimeFeeder feeder;

      public AutoFeeder(DroneNetwork.LandPlotMetadata metadata, double endTime)
      {
        feeder = metadata.plot.GetComponentInChildren<SlimeFeeder>();
        if (!(feeder != null))
          return;
        feeder.UpdateToTime(endTime);
        id = feeder.GetFoodId();
        remaining = feeder.RemainingFeedOperationsFastForward();
      }

      public override bool Selected(Identifiable.Id id)
      {
        if (!base.Selected(id))
          return false;
        feeder.ProcessFeedOperationFastForward();
        return true;
      }
    }

    public class Dynamic : FeedingSource
    {
      private TrackContainedIdentifiables container;

      public override IEnumerable<Identifiable.Id> ids => container.GetTrackedIdentifiableTypes().Where(id => Identifiable.IsFood(id));

      public Dynamic(TrackContainedIdentifiables container) => this.container = container;

      public override GameObject GetTarget(Identifiable.Id id) => container.RemoveTrackedObject(id).gameObject;
    }

    public class LiquidSource : FeedingSource
    {
      private LiquidSource source;
      private Identifiable.Id liquidId;

      public override IEnumerable<Identifiable.Id> ids
      {
        get
        {
          yield return source.liquidId;
        }
      }

      public LiquidSource(LiquidSource source) => this.source = source;

      public override bool Selected(Identifiable.Id id)
      {
        /*if (!this.source.Available())
          return false;
        this.source.ConsumeLiquid();*/
        return true;
      }

      //public override GameObject GetTarget(Identifiable.Id id) => source.gameObject;
    }
  }
}
