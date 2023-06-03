// Decompiled with JetBrains decompiler
// Type: GardenUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class GardenUI : LandPlotUI
{
  public UpgradePurchaseItem soil;
  public UpgradePurchaseItem sprinkler;
  public UpgradePurchaseItem scareslime;
  public UpgradePurchaseItem miracleMix;
  public UpgradePurchaseItem deluxe;
  public PurchaseItem clearCrop;
  public PlotPurchaseItem demolish;
  public Sprite titleIcon;
  public GameObject plantButtonPanelObject;

  protected override GameObject CreatePurchaseUI() => SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.garden", new PurchaseUI.Purchasable[7]
  {
    new PurchaseUI.Purchasable("m.upgrade.name.garden.soil", soil.icon, soil.img, "m.upgrade.desc.garden.soil", soil.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), UpgradeSoil, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SOIL)),
    new PurchaseUI.Purchasable("m.upgrade.name.garden.sprinkler", sprinkler.icon, sprinkler.img, "m.upgrade.desc.garden.sprinkler", sprinkler.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), UpgradeSprinkler, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SPRINKLER)),
    new PurchaseUI.Purchasable("m.upgrade.name.garden.scareslime", scareslime.icon, scareslime.img, "m.upgrade.desc.garden.scareslime", scareslime.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), UpgradeScareslime, () => true, () => !activator.HasUpgrade(LandPlot.Upgrade.SCARESLIME)),
    new PurchaseUI.Purchasable("m.upgrade.name.garden.miracle_mix", miracleMix.icon, miracleMix.img, "m.upgrade.desc.garden.miracle_mix", miracleMix.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), UpgradeMiracleMix, () => SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS) >= 1, () => !activator.HasUpgrade(LandPlot.Upgrade.MIRACLE_MIX)),
    new PurchaseUI.Purchasable("m.upgrade.name.garden.deluxe", deluxe.icon, deluxe.img, "m.upgrade.desc.garden.deluxe", deluxe.cost, new PediaDirector.Id?(PediaDirector.Id.GARDEN), UpgradeDeluxe, () => SRSingleton<SceneContext>.Instance.ProgressDirector.GetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS) >= 2, () => !activator.HasUpgrade(LandPlot.Upgrade.DELUXE_GARDEN)),
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "b.clear_crop"), clearCrop.icon, clearCrop.img, MessageUtil.Qualify("ui", "m.desc.clear_crop"), clearCrop.cost, new PediaDirector.Id?(), ClearCrop, () => activator.HasAttached(), () => true),
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish")
  }, false, Close);

  public void UpgradeSoil() => Upgrade(LandPlot.Upgrade.SOIL, soil.cost);

  public void UpgradeSprinkler() => Upgrade(LandPlot.Upgrade.SPRINKLER, sprinkler.cost);

  public void UpgradeScareslime() => Upgrade(LandPlot.Upgrade.SCARESLIME, scareslime.cost);

  public void UpgradeMiracleMix() => Upgrade(LandPlot.Upgrade.MIRACLE_MIX, miracleMix.cost);

  public void UpgradeDeluxe() => Upgrade(LandPlot.Upgrade.DELUXE_GARDEN, deluxe.cost);

  public void ClearCrop()
  {
    if (playerState.GetCurrency() >= clearCrop.cost)
    {
      playerState.SpendCurrency(clearCrop.cost);
      activator.DestroyAttached();
      PlayPurchaseCue();
      Close();
    }
    else
      Error("e.insuf_coins");
  }

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
