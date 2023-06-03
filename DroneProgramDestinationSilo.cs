// Decompiled with JetBrains decompiler
// Type: DroneProgramDestinationSilo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class DroneProgramDestinationSilo : 
  DroneProgramDestinationSiloStorage<DroneNetwork.StorageMetadata>
{
  protected override IEnumerable<DroneNetwork.StorageMetadata> GetDestinations(
    Identifiable.Id id,
    bool overflow)
  {
    return drone.network.Plots.SelectMany(m => m.silos).Where(s => s.storage.CanAccept(id, s.index, overflow));
  }

  protected override IEnumerable<DroneNetwork.StorageMetadata> Prioritize(
    IEnumerable<DroneNetwork.StorageMetadata> destinations)
  {
    return destinations.OrderByDescending(s => s.count);
  }
}
