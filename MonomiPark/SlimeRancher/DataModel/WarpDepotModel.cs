// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.WarpDepotModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class WarpDepotModel : GadgetModel
  {
    public bool isPrimary;
    public AmmoModel ammo = new AmmoModel();

    public WarpDepotModel(Gadget.Id gadgetId, string siteId, Transform transform)
      : base(gadgetId, siteId, transform)
    {
    }

    public void Push(bool isPrimary, Ammo.Slot[] slots)
    {
      this.isPrimary = isPrimary;
      ammo.Push(slots);
    }

    public void Pull(out bool isPrimary, out Ammo.Slot[] slots)
    {
      isPrimary = this.isPrimary;
      ammo.Pull(out slots);
    }
  }
}
