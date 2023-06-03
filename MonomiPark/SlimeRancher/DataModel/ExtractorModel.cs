// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ExtractorModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class ExtractorModel : GadgetModel
  {
    public int cyclesRemaining;
    public int queuedToProduce;
    public double cycleEndTime;
    public double nextProduceTime;

    public ExtractorModel(Gadget.Id gadgetId, string siteId, Transform transform)
      : base(gadgetId, siteId, transform)
    {
    }

    public void Push(
      int cyclesRemaining,
      int queuedToProduce,
      double cycleEndTime,
      double nextProduceTime)
    {
      this.cyclesRemaining = cyclesRemaining;
      this.queuedToProduce = queuedToProduce;
      this.cycleEndTime = cycleEndTime;
      this.nextProduceTime = nextProduceTime;
    }

    public void Pull(
      out int cyclesRemaining,
      out int queuedToProduce,
      out double cycleEndTime,
      out double nextProduceTime)
    {
      cyclesRemaining = this.cyclesRemaining;
      queuedToProduce = this.queuedToProduce;
      cycleEndTime = this.cycleEndTime;
      nextProduceTime = this.nextProduceTime;
    }
  }
}
