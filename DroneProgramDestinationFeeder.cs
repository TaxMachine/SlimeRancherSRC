// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationFeeder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class DroneProgramDestinationFeeder : 
  DroneProgramDestinationSiloStorage<DroneProgramDestinationFeeder.Destination>
{
  protected override IEnumerable<Destination> GetDestinations(
    Identifiable.Id id,
    bool overflow)
  {
    return drone.network.Plots.SelectMany(m => m.feeders.Where(s => s.storage.CanAccept(id, s.index, overflow)).Select(s => new Destination(s, m, id))).Where(m => m.seeded || m.corral.anyEat);
  }

  protected override IEnumerable<Destination> Prioritize(
    IEnumerable<Destination> destinations)
  {
    return destinations.OrderBy(d => d, Destination.Comparer.Default);
  }

  public class Destination : DroneNetwork.StorageMetadata
  {
    public readonly DroneProgramDestinationCorral.Destination corral;
    public readonly bool seeded;

    public Destination(
      DroneNetwork.StorageMetadata storage,
      DroneNetwork.LandPlotMetadata metadata,
      Identifiable.Id id)
      : base(storage)
    {
      corral = new DroneProgramDestinationCorral.Destination(metadata, id);
      seeded = storage.id == id;
    }

    public class Comparer : SRComparer<Destination>
    {
      public static Comparer<Destination> Default = new Comparer().OrderByDescending(m => m.seeded).OrderBy(m => m.count).OrderByDescending(m => m.corral.anyFavorite).OrderBy(m => m.corral.available);
    }
  }
}
