// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.AnimalModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class AnimalModel : ResourceModel
  {
    public double transformTime;
    public double nextReproduceTime;
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();

    public AnimalModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
      : base(actorId, ident, regionSetId, transform)
    {
    }

    public void Push(ActorDataV09 persistence)
    {
      transformTime = persistence.transformTime;
      nextReproduceTime = persistence.reproduceTime;
      fashions = persistence.fashions;
    }

    public void Pull(ref ActorDataV09 persistence)
    {
      persistence.transformTime = transformTime;
      persistence.reproduceTime = nextReproduceTime;
      persistence.fashions = new List<Identifiable.Id>(fashions);
    }
  }
}
