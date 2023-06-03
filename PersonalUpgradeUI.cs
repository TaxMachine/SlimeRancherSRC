// Decompiled with JetBrains decompiler
// Type: PersonalUpgradeUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class PersonalUpgradeUI : BaseUI
{
  public Sprite titleIcon;
  private PlayerState playerState;
  private LookupDirector lookupDir;
  private PurchaseUI purchaseUI;
  private const string ALREADY_HAS_UPGRADE = "e.already_has_personal_upgrade";
  private const string INELIGIBLE_FOR_UPGRADE = "e.ineligible_for_personal_upgrade";

  public override void Awake()
  {
    base.Awake();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    RebuildUI();
  }

  public void RebuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "PersonalUpgradeUI.RebuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    purchaseUi.transform.SetParent(transform, false);
    statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    PurchaseUI.Purchasable[] purchasables = new PurchaseUI.Purchasable[22]
    {
      CreateUpgradePurchasable(PlayerState.Upgrade.LIQUID_SLOT),
      CreateUpgradePurchasable(PlayerState.Upgrade.JETPACK),
      CreateUpgradePurchasable(PlayerState.Upgrade.JETPACK_EFFICIENCY),
      CreateUpgradePurchasable(PlayerState.Upgrade.RUN_EFFICIENCY),
      CreateUpgradePurchasable(PlayerState.Upgrade.RUN_EFFICIENCY_2),
      CreateUpgradePurchasable(PlayerState.Upgrade.AIR_BURST),
      CreateUpgradePurchasable(PlayerState.Upgrade.HEALTH_1),
      CreateUpgradePurchasable(PlayerState.Upgrade.HEALTH_2),
      CreateUpgradePurchasable(PlayerState.Upgrade.HEALTH_3),
      CreateUpgradePurchasable(PlayerState.Upgrade.HEALTH_4),
      CreateUpgradePurchasable(PlayerState.Upgrade.ENERGY_1),
      CreateUpgradePurchasable(PlayerState.Upgrade.ENERGY_2),
      CreateUpgradePurchasable(PlayerState.Upgrade.ENERGY_3),
      CreateUpgradePurchasable(PlayerState.Upgrade.AMMO_1),
      CreateUpgradePurchasable(PlayerState.Upgrade.AMMO_2),
      CreateUpgradePurchasable(PlayerState.Upgrade.AMMO_3),
      CreateUpgradePurchasable(PlayerState.Upgrade.AMMO_4),
      CreateUpgradePurchasable(PlayerState.Upgrade.TREASURE_CRACKER_1),
      CreateUpgradePurchasable(PlayerState.Upgrade.TREASURE_CRACKER_2),
      CreateUpgradePurchasable(PlayerState.Upgrade.TREASURE_CRACKER_3),
      CreateUpgradePurchasable(PlayerState.Upgrade.GOLDEN_SURESHOT),
      CreateUpgradePurchasable(PlayerState.Upgrade.SPARE_KEY)
    };
    return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.personal_upgrades"), purchasables, false, Close);
  }

  private PurchaseUI.Purchasable CreateUpgradePurchasable(PlayerState.Upgrade upgrade)
  {
    UpgradeDefinition item = lookupDir.GetUpgradeDefinition(upgrade);
    string lowerInvariant = upgrade.ToString().ToLowerInvariant();
    return new PurchaseUI.Purchasable("m.upgrade.name.personal." + lowerInvariant, item.Icon, item.Icon, "m.upgrade.desc.personal." + lowerInvariant, item.Cost, new PediaDirector.Id?(), () => Upgrade(upgrade, item.Cost), () => playerState.HasOrCanGetUpgrade(upgrade), () => !playerState.HasUpgrade(upgrade));
  }

  protected void Upgrade(PlayerState.Upgrade upgrade, int cost)
  {
    if (playerState.HasUpgrade(upgrade))
    {
      PlayErrorCue();
      Error("e.already_has_personal_upgrade");
    }
    else if (!playerState.CanGetUpgrade(upgrade))
    {
      PlayErrorCue();
      Error("e.ineligible_for_personal_upgrade");
    }
    else if (playerState.GetCurrency() >= cost)
    {
      Play(SRSingleton<GameContext>.Instance.UITemplates.purchasePersonalUpgradeCue);
      playerState.SpendCurrency(cost);
      playerState.AddUpgrade(upgrade, true);
      RebuildUI();
      purchaseUI.PlayPurchaseFX();
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_coins");
    }
  }
}
