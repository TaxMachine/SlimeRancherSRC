// Decompiled with JetBrains decompiler
// Type: ScenePrefabInstantiator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class ScenePrefabInstantiator : PrefabInstantiator
{
  private LookupDirector lookupDir;

  public ScenePrefabInstantiator(LookupDirector lookupDir) => this.lookupDir = lookupDir;

  public GameObject InstantiateActor(
    long actorId,
    Identifiable.Id id,
    RegionRegistry.RegionSetId regionSetId,
    Vector3 pos,
    Vector3 rot,
    GameModel gameModel)
  {
    GameObject prefab = lookupDir.GetPrefab(id);
    if (!(prefab == null))
      return gameModel.InstantiateActorWithoutNotify(actorId, prefab, regionSetId, pos, Quaternion.Euler(rot));
    Log.Warning("Could not instantiate actor: " + id);
    return null;
  }

  public GameObject InstantiateGadget(Gadget.Id id, GadgetSiteModel site, GameModel gameModel)
  {
    GadgetDefinition gadgetDefinition = lookupDir.GetGadgetDefinition(id);
    return gameModel.InstantiateGadgetWithoutNotify(gadgetDefinition.prefab, site);
  }

  public void InstantiatePlot(LandPlot.Id id, LandPlotModel plotModel, bool expectingPush) => plotModel.InstantiatePlot(lookupDir.GetPlotPrefab(id), expectingPush);
}
