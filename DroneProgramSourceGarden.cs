// Decompiled with JetBrains decompiler
// Type: DroneProgramSourceGarden
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class DroneProgramSourceGarden : DroneProgramSourceLandPlot
{
  protected override LandPlot.Id GetLandPlotID() => LandPlot.Id.GARDEN;

  public override IEnumerable<DroneFastForwarder.GatherGroup> GetFastForwardGroups(double endTime) => base.GetFastForwardGroups(endTime).Concat(drone.network.Plots.Where(m => m.plot.typeId == LandPlot.Id.GARDEN).SelectMany(m => m.plot.GetComponentsInChildren<SpawnResource>()).SelectMany(r => r.GetFastForwardGroups(endTime)).Where(g => predicate(g.id)));

  protected override float GetPickupRadius()
  {
    float pickupRadius = base.GetPickupRadius();
    if (Identifiable.IsFruit(source.id))
    {
      ResourceCycle component = source.GetComponent<ResourceCycle>();
      if (component != null && component.GetState() == ResourceCycle.State.RIPE)
        pickupRadius *= 1.5f;
    }
    return pickupRadius;
  }
}
