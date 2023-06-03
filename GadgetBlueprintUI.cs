// Decompiled with JetBrains decompiler
// Type: GadgetBlueprintUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GadgetBlueprintUI : BaseUI
{
  public Sprite titleIcon;

  public override void Awake()
  {
    base.Awake();
    RebuildUI();
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnBuilderShopOpen();
  }

  public void RebuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "GadgetBlueprintUI.RebuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUi.transform.SetParent(transform, false);
    statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    GadgetDirector gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
    List<PurchaseUI.Purchasable> purchasableList1 = new List<PurchaseUI.Purchasable>();
    foreach (GadgetDefinition gadgetDefinition in SRSingleton<GameContext>.Instance.LookupDirector.GadgetDefinitions)
    {
      GadgetDefinition entry = gadgetDefinition;
      string lowerInvariant = Enum.GetName(typeof (Gadget.Id), entry.id).ToLowerInvariant();
      List<PurchaseUI.Purchasable> purchasableList2 = purchasableList1;
      string nameKey = string.Format("m.gadget.name.{0}", lowerInvariant);
      string str = string.Format("m.gadget.desc.{0}", lowerInvariant);
      Sprite icon1 = entry.icon;
      Sprite icon2 = entry.icon;
      string descKey = str;
      int blueprintCost = entry.blueprintCost;
      PediaDirector.Id? pediaId = new PediaDirector.Id?(entry.pediaLink);
      UnityAction onPurchase = () => BuyBlueprint(entry.id);
      Func<bool> unlocked = () => gadgetDir.HasBlueprint(entry.id) || gadgetDir.IsBlueprintUnlocked(entry.id);
      Func<bool> avail = () => !gadgetDir.HasBlueprint(entry.id);
      PurchaseUI.Purchasable purchasable = new PurchaseUI.Purchasable(nameKey, icon1, icon2, descKey, blueprintCost, pediaId, onPurchase, unlocked, avail);
      purchasableList2.Add(purchasable);
    }
    return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.purchase_blueprint"), purchasableList1.ToArray(), false, Close);
  }

  public void BuyBlueprint(Gadget.Id id)
  {
    PlayerState playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    GadgetDefinition gadgetDefinition = SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(id);
    if (playerState.GetCurrency() >= gadgetDefinition.blueprintCost)
    {
      playerState.SpendCurrency(gadgetDefinition.blueprintCost);
      SRSingleton<SceneContext>.Instance.GadgetDirector.AddBlueprint(id);
      PlayPurchaseCue();
      Close();
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_coins");
    }
  }

  protected void PlayPurchaseCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseBlueprintCue);
}
