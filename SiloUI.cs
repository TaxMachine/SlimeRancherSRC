// Decompiled with JetBrains decompiler
// Type: SiloUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class SiloUI : LandPlotUI
{
  public UpgradePurchaseItem storage2;
  public UpgradePurchaseItem storage3;
  public UpgradePurchaseItem storage4;
  public PlotPurchaseItem demolish;
  public Sprite titleIcon;

  protected override GameObject CreatePurchaseUI() => SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.silo", new PurchaseUI.Purchasable[4]
  {
    new PurchaseUI.Purchasable("m.upgrade.name.silo.storage2", storage2.icon, storage2.img, "m.upgrade.desc.silo.storage2", storage2.cost, new PediaDirector.Id?(PediaDirector.Id.SILO), UpgradeStorage2, () => !activator.HasUpgrade(LandPlot.Upgrade.STORAGE2), () => true),
    new PurchaseUI.Purchasable("m.upgrade.name.silo.storage2", storage3.icon, storage3.img, "m.upgrade.desc.silo.storage2", storage3.cost, new PediaDirector.Id?(PediaDirector.Id.SILO), UpgradeStorage3, () => activator.HasUpgrade(LandPlot.Upgrade.STORAGE2) && !activator.HasUpgrade(LandPlot.Upgrade.STORAGE3), () => true),
    new PurchaseUI.Purchasable("m.upgrade.name.silo.storage2", storage4.icon, storage4.img, "m.upgrade.desc.silo.storage2", storage4.cost, new PediaDirector.Id?(PediaDirector.Id.SILO), UpgradeStorage4, () => activator.HasUpgrade(LandPlot.Upgrade.STORAGE3), () => !activator.HasUpgrade(LandPlot.Upgrade.STORAGE4)),
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish", activator.GetComponent<SiloStorage>().GetRelevantAmmo().IsEmpty() ? null : "w.destroying_silo_destroys_contents", requireHoldToPurchase: true)
  }, false, Close);

  public void UpgradeStorage2() => Upgrade(LandPlot.Upgrade.STORAGE2, storage2.cost);

  public void UpgradeStorage3() => Upgrade(LandPlot.Upgrade.STORAGE3, storage3.cost);

  public void UpgradeStorage4() => Upgrade(LandPlot.Upgrade.STORAGE4, storage4.cost);

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
