// Decompiled with JetBrains decompiler
// Type: CoopUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class CoopUI : LandPlotUI
{
  public UpgradePurchaseItem walls;
  public UpgradePurchaseItem feeder;
  public UpgradePurchaseItem vitamizer;
  public UpgradePurchaseItem deluxe;
  public PlotPurchaseItem demolish;
  public Sprite titleIcon;

  protected override GameObject CreatePurchaseUI() => SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.coop", new PurchaseUI.Purchasable[5]
  {
    new PurchaseUI.Purchasable("m.upgrade.name.coop.walls", walls.icon, walls.img, "m.upgrade.desc.coop.walls", walls.cost, new PediaDirector.Id?(PediaDirector.Id.COOP), UpgradeWalls, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.WALLS)),
    new PurchaseUI.Purchasable("m.upgrade.name.coop.feeder", feeder.icon, feeder.img, "m.upgrade.desc.coop.feeder", feeder.cost, new PediaDirector.Id?(PediaDirector.Id.COOP), UpgradeFeeder, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.FEEDER)),
    new PurchaseUI.Purchasable("m.upgrade.name.coop.vitamizer", vitamizer.icon, vitamizer.img, "m.upgrade.desc.coop.vitamizer", vitamizer.cost, new PediaDirector.Id?(PediaDirector.Id.COOP), UpgradeVitamizer, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.VITAMIZER)),
    new PurchaseUI.Purchasable("m.upgrade.name.coop.deluxe", deluxe.icon, deluxe.img, "m.upgrade.desc.coop.deluxe", deluxe.cost, new PediaDirector.Id?(PediaDirector.Id.COOP), UpgradeDeluxe, () => SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.MOCHI_REWARDS) >= 2, () => !activator.HasUpgrade(LandPlot.Upgrade.DELUXE_COOP)),
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish")
  }, false, Close);

  public void UpgradeWalls() => Upgrade(LandPlot.Upgrade.WALLS, walls.cost);

  public void UpgradeFeeder() => Upgrade(LandPlot.Upgrade.FEEDER, feeder.cost);

  public void UpgradeVitamizer() => Upgrade(LandPlot.Upgrade.VITAMIZER, vitamizer.cost);

  public void UpgradeDeluxe() => Upgrade(LandPlot.Upgrade.DELUXE_COOP, deluxe.cost);

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
