// Decompiled with JetBrains decompiler
// Type: PlortCollectorUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlortCollectorUpgrader : PlotUpgrader
{
  [Tooltip("The collector object we need to activate/deactivate")]
  public GameObject collector;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.PLORT_COLLECTOR)
      return;
    collector.SetActive(true);
  }
}
