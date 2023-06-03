// Decompiled with JetBrains decompiler
// Type: FabricateGadgetUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FabricateGadgetUI : BaseUI
{
  public Sprite titleIcon;
  private PurchaseUI purchaseUI;
  private Dictionary<string, string> categoryMap = new Dictionary<string, string>();
  private const string ERR_INSUF_CRAFT_RESOURCES = "e.insuf_craft_resources";
  private const string ERR_CANNOT_ADD_GADGET = "e.cannot_add_gagdget";

  public override void Awake()
  {
    base.Awake();
    BuildUI();
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnFabricatorOpen();
  }

  public void BuildUI()
  {
    if (purchaseUI != null && purchaseUI.gameObject != null)
      Destroyer.Destroy(purchaseUI.gameObject, "FabricateGadgetUI.BuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUi.transform.SetParent(transform, false);
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    statusArea = purchaseUI.statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    categoryMap.Clear();
    GadgetDirector gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
    List<PurchaseUI.Purchasable> purchasableList1 = new List<PurchaseUI.Purchasable>();
    Dictionary<PediaDirector.Id, List<PurchaseUI.Purchasable>> dict = new Dictionary<PediaDirector.Id, List<PurchaseUI.Purchasable>>();
    foreach (GadgetDefinition gadgetDefinition in SRSingleton<GameContext>.Instance.LookupDirector.GadgetDefinitions)
    {
      string lowerInvariant = Enum.GetName(typeof (Gadget.Id), gadgetDefinition.id).ToLowerInvariant();
      GadgetDefinition finalDefinition = gadgetDefinition;
      Gadget.Id finalId = gadgetDefinition.id;
      string descKey = "m.gadget.desc." + lowerInvariant;
      string str = "m.gadget.name." + lowerInvariant;
      PurchaseUI.Purchasable purchasable = new PurchaseUI.Purchasable(str, gadgetDefinition.icon, gadgetDefinition.icon, descKey, 0, new PediaDirector.Id?(gadgetDefinition.pediaLink), () => Fabricate(finalId), () => gadgetDir.HasBlueprint(finalId), () => gadgetDir.CanAddGadget(finalDefinition), currCount: () => gadgetDir.GetGadgetCount(finalId), craftCosts: gadgetDefinition.craftCosts);
      purchasableList1.Add(purchasable);
      categoryMap[str] = gadgetDefinition.pediaLink.ToString().ToLowerInvariant();
      List<PurchaseUI.Purchasable> purchasableList2 = dict.Get(gadgetDefinition.pediaLink) ?? new List<PurchaseUI.Purchasable>();
      purchasableList2.Add(purchasable);
      dict[gadgetDefinition.pediaLink] = purchasableList2;
    }
    GameObject purchaseUi = SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.fabricate_gadget"), purchasableList1.ToArray(), true, Close);
    List<PurchaseUI.Category> categories = new List<PurchaseUI.Category>();
    foreach (PediaDirector.Id key in PediaUI.SCIENCE_ENTRIES)
    {
      if (dict.ContainsKey(key))
        categories.Add(new PurchaseUI.Category(key.ToString().ToLowerInvariant(), dict[key].ToArray()));
    }
    purchaseUi.GetComponent<PurchaseUI>().SetCategories(categories);
    purchaseUi.GetComponent<PurchaseUI>().SetPurchaseMsgs("b.fabricate", "b.sold_out");
    return purchaseUi;
  }

  public void Fabricate(Gadget.Id id)
  {
    GadgetDirector gadgetDirector = SRSingleton<SceneContext>.Instance.GadgetDirector;
    AchievementsDirector achievementsDirector = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    GadgetDefinition gadgetDefinition = SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(id);
    if (!gadgetDirector.CanAddGadget(gadgetDefinition))
    {
      PlayErrorCue();
      Error("e.cannot_add_gagdget");
    }
    else if (TrySpendResources(gadgetDefinition.craftCosts))
    {
      ClearStatus();
      PlayPurchaseCue();
      gadgetDirector.AddGadget(id);
      achievementsDirector.AddToStat(AchievementsDirector.GameIntStat.FABRICATED_GADGETS, 1);
      if (gadgetDefinition.buyInPairs)
        gadgetDirector.AddGadget(id);
      AnalyticsUtil.CustomEvent(nameof (Fabricate), new Dictionary<string, object>()
      {
        {
          nameof (id),
          id.ToString()
        }
      });
      purchaseUI.PlayPurchaseFX();
      purchaseUI.Rebuild(false);
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_craft_resources");
    }
  }

  private bool TrySpendResources(GadgetDefinition.CraftCost[] costs) => SRSingleton<SceneContext>.Instance.GadgetDirector.TryToSpendFromRefinery(costs);

  protected void PlayPurchaseCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseBlueprintCue);
}
