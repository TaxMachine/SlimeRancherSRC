// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchSlimeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchSlimeModel : SlimeModel
  {
    public float exposureChance;
    public double deathTime;

    public GlitchSlimeModel(
      long actorId,
      Identifiable.Id ident,
      RegionRegistry.RegionSetId regionSetId,
      Transform transform)
      : base(actorId, ident, regionSetId, transform)
    {
    }

    public void Push(GlitchSlimeDataV01 persistence)
    {
      exposureChance = persistence.exposureChance;
      deathTime = persistence.deathTime;
    }

    public GlitchSlimeDataV01 Pull() => new GlitchSlimeDataV01()
    {
      exposureChance = exposureChance,
      deathTime = deathTime
    };
  }
}
