// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceElderCollector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class DroneProgramSourceElderCollector : DroneProgramSourceSiloStorage
{
  protected override IEnumerable<DroneNetwork.StorageMetadata> GetSources(
    Predicate<Identifiable.Id> predicate)
  {
    return drone.network.Plots.SelectMany(m => m.elderCollectors).Where(s => predicate(s.id)).OrderByDescending(s => s.count);
  }
}
