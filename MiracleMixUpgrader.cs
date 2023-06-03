// Decompiled with JetBrains decompiler
// Type: MiracleMixUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MiracleMixUpgrader : PlotUpgrader
{
  public GameObject miracleMix;
  public GameObject normSoil;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.MIRACLE_MIX)
      return;
    miracleMix.SetActive(true);
    normSoil.SetActive(false);
  }
}
