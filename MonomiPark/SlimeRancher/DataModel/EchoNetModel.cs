// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.EchoNetModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class EchoNetModel : GadgetModel
  {
    public double lastSpawnTime;

    public EchoNetModel(Gadget.Id gadgetId, string siteId, Transform transform)
      : base(gadgetId, siteId, transform)
    {
    }

    public void Push(double lastSpawnTime) => this.lastSpawnTime = lastSpawnTime;

    public void Pull(out double lastSpawnTime) => lastSpawnTime = this.lastSpawnTime;
  }
}
