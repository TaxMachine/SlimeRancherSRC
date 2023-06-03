// Decompiled with JetBrains decompiler
// Type: LandPlotLocation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class LandPlotLocation : IdHandler
{
  public void Awake() => SRSingleton<SceneContext>.Instance.GameModel.RegisterLandPlot(id, gameObject);

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterLandPlot(id);
  }

  protected override string IdPrefix() => "plot";

  public GameObject Replace(LandPlot oldLandPlot, GameObject replacementPrefab)
  {
    GameObject gameObject = Instantiate(replacementPrefab, oldLandPlot.transform.parent, false);
    gameObject.transform.position = oldLandPlot.transform.position;
    gameObject.transform.rotation = oldLandPlot.transform.rotation;
    Destroyer.Destroy(oldLandPlot.gameObject, "LandPlotUI.Replace");
    oldLandPlot.transform.parent = null;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterLandPlot(id);
    SRSingleton<SceneContext>.Instance.GameModel.RegisterLandPlot(id, this.gameObject);
    return gameObject;
  }
}
