// Decompiled with JetBrains decompiler
// Type: DeluxeCoopUpgrader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeluxeCoopUpgrader : PlotUpgrader
{
  public GameObject deluxeStuff;
  public CoopRegion[] coopRegions;

  public override void Apply(LandPlot.Upgrade upgrade)
  {
    if (upgrade != LandPlot.Upgrade.DELUXE_COOP)
      return;
    if (deluxeStuff != null && !deluxeStuff.activeSelf)
      deluxeStuff.SetActive(true);
    foreach (CoopRegion coopRegion in coopRegions)
      coopRegion.SetDeluxe();
  }
}
