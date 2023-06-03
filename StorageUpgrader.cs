// Decompiled with JetBrains decompiler
// Type: StorageUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StorageUpgrader : PlotUpgrader
{
  public GameObject storageAdd2;
  public GameObject storageAdd3;
  public GameObject storageAdd4;
  public GameObject storageOnly1;
  public GameObject storageOnly2;
  public GameObject storageOnly3And4;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    switch (upgrade)
    {
      case LandPlot.Upgrade.STORAGE2:
        storageAdd2.SetActive(true);
        storageOnly1.SetActive(false);
        storageOnly2.SetActive(true);
        storageOnly3And4.SetActive(false);
        break;
      case LandPlot.Upgrade.STORAGE3:
        storageAdd3.SetActive(true);
        storageOnly1.SetActive(false);
        storageOnly2.SetActive(false);
        storageOnly3And4.SetActive(true);
        break;
      case LandPlot.Upgrade.STORAGE4:
        storageAdd4.SetActive(true);
        storageOnly1.SetActive(false);
        storageOnly2.SetActive(false);
        storageOnly3And4.SetActive(true);
        break;
    }
  }
}
