// Decompiled with JetBrains decompiler
// Type: AirNetUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AirNetUpgrader : PlotUpgrader
{
  [Tooltip("All the air net objects we need to activate/deactivate")]
  public GameObject[] airNets;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.AIR_NET)
      return;
    foreach (GameObject airNet in airNets)
      airNet.SetActive(true);
  }
}
