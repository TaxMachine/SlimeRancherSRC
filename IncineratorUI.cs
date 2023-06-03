// Decompiled with JetBrains decompiler
// Type: IncineratorUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class IncineratorUI : LandPlotUI
{
  public UpgradePurchaseItem ashTrough;
  public PlotPurchaseItem demolish;
  public Sprite titleIcon;

  protected override GameObject CreatePurchaseUI() => SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.incinerator", new PurchaseUI.Purchasable[2]
  {
    new PurchaseUI.Purchasable("m.upgrade.name.incinerator.ash_trough", ashTrough.icon, ashTrough.img, "m.upgrade.desc.incinerator.ash_trough", ashTrough.cost, new PediaDirector.Id?(PediaDirector.Id.CORRAL), UpgradeAshTrough, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.ASH_TROUGH)),
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish")
  }, false, Close);

  public void UpgradeAshTrough() => Upgrade(LandPlot.Upgrade.ASH_TROUGH, ashTrough.cost);

  public void Demolish()
  {
    if (playerState.GetCurrency() >= demolish.cost)
    {
      playerState.SpendCurrency(demolish.cost);
      Replace(demolish.plotPrefab);
      PlayPurchaseCue();
    }
    else
      Error("e.insuf_coins");
  }
}
