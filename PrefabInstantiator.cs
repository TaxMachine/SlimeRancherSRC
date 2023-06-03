// Decompiled with JetBrains decompiler
// Type: PrefabInstantiator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public interface PrefabInstantiator
{
  GameObject InstantiateActor(
    long actorId,
    Identifiable.Id id,
    RegionRegistry.RegionSetId regionSetId,
    Vector3 pos,
    Vector3 rot,
    GameModel gameModel);

  GameObject InstantiateGadget(Gadget.Id id, GadgetSiteModel site, GameModel gameModel);

  void InstantiatePlot(LandPlot.Id id, LandPlotModel plotModel, bool expectingPush);
}
