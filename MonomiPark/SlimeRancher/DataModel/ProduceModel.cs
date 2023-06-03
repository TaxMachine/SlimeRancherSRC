// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ProduceModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class ProduceModel : ResourceModel
  {
    public ResourceCycle.State state = ResourceCycle.State.EDIBLE;
    public double progressTime = double.NaN;

    public ProduceModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
      : base(actorId, ident, regionSetId, transform)
    {
    }

    public override bool IsEdible() => base.IsEdible() && state == ResourceCycle.State.EDIBLE;

    public void Push(ResourceCycle.State state, double progressTime)
    {
      this.state = state;
      this.progressTime = progressTime;
    }

    public void Pull(out ResourceCycle.State state, out double progressTime)
    {
      state = this.state;
      progressTime = this.progressTime;
    }
  }
}
