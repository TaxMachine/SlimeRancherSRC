// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ScienceMatModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class ScienceMatModel : ResourceModel
  {
    public ScienceMatModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
      : base(actorId, ident, regionSetId, transform)
    {
    }
  }
}
