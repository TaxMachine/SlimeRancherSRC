// Decompiled with JetBrains decompiler
// Type: DroneNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneNetwork : PathingNetwork
{
  public const float PATHING_THROTTLE_HRS = 0.0333333351f;
  [HideInInspector]
  public double pathingThrottleUntil;
  private const float MAX_CONNECTION_DIST = 40f;
  private DronePather pather = new DronePather(40f);
  private List<LandPlotMetadata> plots = new List<LandPlotMetadata>();
  private List<Drone> drones = new List<Drone>();
  private List<SiloCatcher> refineryCatchers = new List<SiloCatcher>();
  private List<ScorePlort> markets = new List<ScorePlort>();

  public override Pather Pather => pather;

  public void Register(LandPlot p) => plots.Add(new LandPlotMetadata()
  {
    plot = p,
    trackers = p.GetComponentsInChildren<TrackContainedIdentifiables>(),
    subnetwork = p.GetComponentInChildren<GardenDroneSubnetwork>(),
    feeders = p.typeId == LandPlot.Id.CORRAL ? GetStorageMetadata(p.GetComponents<FeederUpgrader>().Where(c => c != null && c.feeder.activeInHierarchy).Select(c => c.feeder.GetComponent<SiloStorage>())).ToArray() : new StorageMetadata[0],
    plortCollectors = p.typeId == LandPlot.Id.CORRAL ? GetStorageMetadata(p.GetComponents<PlortCollectorUpgrader>().Where(c => c != null && c.collector.activeInHierarchy).Select(c => c.collector.GetComponent<SiloStorage>())).ToArray() : new StorageMetadata[0],
    elderCollectors = p.typeId == LandPlot.Id.COOP ? GetStorageMetadata(p.GetComponents<DeluxeCoopUpgrader>().Where(c => c != null && c.deluxeStuff.activeInHierarchy).Select(c => c.deluxeStuff.GetComponentInChildren<SiloStorage>())).ToArray() : new StorageMetadata[0],
    silos = p.typeId == LandPlot.Id.SILO ? p.GetComponents<SiloStorage>().SelectMany(s => s.GetComponentsInChildren<SiloStorageActivator>().SelectMany(a => a.siloSlotUIs.Select(ui => ui.slotIdx)).Select(i => new StorageMetadata()
    {
      index = i,
      storage = s,
      catcher = s.GetComponentsInChildren<SiloStorageActivator>().First(a => a.siloSlotUIs.Any(ui => ui.slotIdx == i)).siloCatcher
    })).ToArray() : new StorageMetadata[0],
    gardens = p.typeId == LandPlot.Id.GARDEN ? p.GetComponentsInChildren<GardenCatcher>() : new GardenCatcher[0],
    incinerators = p.typeId == LandPlot.Id.INCINERATOR ? p.GetComponentsInChildren<Incinerate>() : new Incinerate[0]
  });

  public bool Deregister(LandPlot deregister) => plots.RemoveAll(p => p.plot == deregister) >= 1;

  public void OnUpgradesChanged(LandPlot plot)
  {
    if (!Deregister(plot))
      return;
    Register(plot);
  }

  public LandPlotMetadata GetContaining(Identifiable source) => plots.FirstOrDefault(m => m.Contains(source));

  private static IEnumerable<StorageMetadata> GetStorageMetadata(
    IEnumerable<SiloStorage> storages)
  {
    return storages.Select(s => new
    {
      storage = s,
      ammo = s.GetRelevantAmmo()
    }).SelectMany(s => Enumerable.Range(0, s.ammo.GetUsableSlotCount()).Select(i => new StorageMetadata()
    {
      index = i,
      storage = s.storage,
      catcher = s.storage.GetComponentsInChildren<SiloCatcher>().First(c => c.slotIdx == i)
    }));
  }

  public IEnumerable<LandPlotMetadata> Plots => plots;

  public IEnumerable<Drone> Drones => drones;

  public void Register(Drone drone) => drones.Add(drone);

  public bool Deregister(Drone deregister) => drones.RemoveAll(d => d == deregister) >= 1;

  public IEnumerable<SiloCatcher> RefineryCatchers => refineryCatchers;

  public void Register(SiloCatcher catcher)
  {
    if (catcher.type != SiloCatcher.Type.REFINERY)
      return;
    refineryCatchers.Add(catcher);
  }

  public bool Deregister(SiloCatcher catcher) => catcher.type != SiloCatcher.Type.REFINERY || refineryCatchers.RemoveAll(d => d == catcher) >= 1;

  public IEnumerable<ScorePlort> Markets => markets;

  public void Register(ScorePlort market) => markets.Add(market);

  public bool Deregister(ScorePlort market) => markets.RemoveAll(d => d == market) >= 1;

  public static bool IsResourceReady(GameObject go)
  {
    ResourceCycle component = go.GetComponent<ResourceCycle>();
    return component == null || component.GetState() == ResourceCycle.State.RIPE || component.GetState() == ResourceCycle.State.EDIBLE;
  }

  public static DroneNetwork Find(GameObject gameObject)
  {
    CellDirector componentInParent = gameObject.GetComponentInParent<CellDirector>();
    return !(componentInParent == null) ? componentInParent.GetComponent<DroneNetwork>() : null;
  }

  public class LandPlotMetadata
  {
    public LandPlot plot;
    public GardenCatcher[] gardens;
    public StorageMetadata[] feeders;
    public StorageMetadata[] silos;
    public StorageMetadata[] plortCollectors;
    public StorageMetadata[] elderCollectors;
    public TrackContainedIdentifiables[] trackers;
    public Incinerate[] incinerators;
    public GardenDroneSubnetwork subnetwork;

    public bool Contains(Identifiable identifiable) => trackers.Any(t => t.Contains(identifiable));
  }

  public class StorageMetadata
  {
    public SiloStorage storage;
    public SiloCatcher catcher;
    public int index;

    public StorageMetadata()
    {
    }

    public StorageMetadata(StorageMetadata other)
    {
      storage = other.storage;
      catcher = other.catcher;
      index = other.index;
    }

    public bool CanCancel() => storage == null || !storage.gameObject.activeInHierarchy;

    public Ammo ammo => storage.GetRelevantAmmo();

    public Identifiable.Id id => ammo.GetSlotName(index);

    public int count => ammo.GetSlotCount(index);

    public int maxCount => ammo.GetSlotMaxCount(index);

    public int GetAvailableSpace(Identifiable.Id id) => this.id != id && this.id != Identifiable.Id.NONE ? 0 : maxCount - count;

    public bool Increment(Identifiable.Id id, bool overflow, int count = 1) => storage.MaybeAddIdentifiable(id, index, count, overflow);

    public void Decrement(int count = 1) => ammo.Decrement(index, count);

    public bool IsFull() => count >= maxCount;
  }
}
