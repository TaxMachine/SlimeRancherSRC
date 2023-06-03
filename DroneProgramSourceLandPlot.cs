// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceLandPlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public abstract class DroneProgramSourceLandPlot : DroneProgramSourceDynamic
{
  protected DroneNetwork.LandPlotMetadata currentLandPlot;
  private static ReferenceCount<int> GRAYLIST = new ReferenceCount<int>();
  private int? grayListHashCode;

  public override void Awake()
  {
    base.Awake();
    plexer.onSubbehaviourSelected += OnDroneSubbehaviourSelected;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    plexer.onSubbehaviourSelected -= OnDroneSubbehaviourSelected;
  }

  public override bool Relevancy()
  {
    if (!base.Relevancy())
      return false;
    currentLandPlot = drone.network.GetContaining(source);
    if (currentLandPlot != null)
    {
      grayListHashCode = new int?(currentLandPlot.GetHashCode());
      GRAYLIST.Increment(grayListHashCode.Value);
    }
    return true;
  }

  public override void Deselected()
  {
    base.Deselected();
    if (!grayListHashCode.HasValue)
      return;
    GRAYLIST.Decrement(grayListHashCode.Value);
    grayListHashCode = new int?();
  }

  protected override bool CanCancel() => base.CanCancel() || currentLandPlot == null || !currentLandPlot.Contains(source);

  protected override IEnumerable<Identifiable> GetSources(Predicate<Identifiable.Id> predicate) => drone.network.Plots.Where(m => m.plot.typeId == GetLandPlotID()).Select(m => new Intermediate()
  {
    metadata = m,
    sources = GetSources(predicate, m)
  }).OrderBy(o => o, new Intermediate.Comparer().OrderByDescending(o => o.metadata == currentLandPlot).OrderBy(o => GRAYLIST.ContainsKey(o.metadata.GetHashCode())).OrderByDescending(o => o.sources.Count())).SelectMany(o => o.sources);

  protected virtual IEnumerable<Identifiable> GetSources(
    Predicate<Identifiable.Id> predicate,
    DroneNetwork.LandPlotMetadata metadata)
  {
    return metadata.trackers.SelectMany(tracker => tracker.GetAllTracked().Where(kv => predicate(kv.Key)).SelectMany(kv => kv.Value).Where(id => SourcePredicate(metadata, id)));
  }

  protected override bool SourcePredicate(
    DroneNetwork.LandPlotMetadata metadata,
    Identifiable source)
  {
    return base.SourcePredicate(metadata, source) && metadata != null && metadata.plot.typeId == GetLandPlotID();
  }

  private void OnDroneSubbehaviourSelected(DroneSubbehaviour subbehaviour)
  {
    if (!(subbehaviour != this) || subbehaviour is DroneSubbehaviourIdle)
      return;
    currentLandPlot = null;
  }

  protected override GardenDroneSubnetwork GetSubnetwork() => currentLandPlot == null ? null : currentLandPlot.subnetwork;

  protected abstract LandPlot.Id GetLandPlotID();

  private class Intermediate
  {
    public DroneNetwork.LandPlotMetadata metadata;
    public IEnumerable<Identifiable> sources;

    public class Comparer : SRComparer<Intermediate>
    {
    }
  }
}
