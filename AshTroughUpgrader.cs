// Decompiled with JetBrains decompiler
// Type: AshTroughUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AshTroughUpgrader : PlotUpgrader
{
  public GameObject ashTrough;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.ASH_TROUGH)
      return;
    ashTrough.SetActive(true);
  }
}
