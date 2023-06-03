// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceSilo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class DroneProgramSourceSilo : DroneProgramSourceSiloStorage
{
  protected override IEnumerable<DroneNetwork.StorageMetadata> GetSources(
    Predicate<Identifiable.Id> predicate)
  {
    return drone.network.Plots.SelectMany(m => m.silos).Where(s => predicate(s.id)).OrderBy(s => s.count);
  }
}
