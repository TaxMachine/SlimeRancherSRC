// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.SnareModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class SnareModel : GadgetModel
  {
    public Identifiable.Id baitTypeId;
    public Identifiable.Id gordoTypeId;
    public int gordoEatenCount;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();
    public int gordoTargetCount;

    public SnareModel(Gadget.Id gadgetId, string siteId, Transform transform)
      : base(gadgetId, siteId, transform)
    {
    }

    public void Push(
      Identifiable.Id baitTypeId,
      Identifiable.Id gordoTypeId,
      int gordoEatenCount,
      List<Identifiable.Id> fashions)
    {
      this.baitTypeId = baitTypeId;
      this.gordoTypeId = gordoTypeId;
      this.gordoEatenCount = gordoEatenCount;
      this.fashions = fashions;
    }

    public void Pull(
      out Identifiable.Id baitTypeId,
      out Identifiable.Id gordoTypeId,
      out int gordoEatenCount,
      out List<Identifiable.Id> fashions)
    {
      baitTypeId = this.baitTypeId;
      gordoTypeId = this.gordoTypeId;
      gordoEatenCount = this.gordoEatenCount;
      fashions = this.fashions;
    }
  }
}
