// Decompiled with JetBrains decompiler
// Type: PlaceGadgetUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceGadgetUI : BaseUI
{
  public PurchaseItem demolish;
  public PurchaseItem pickUp;
  public Sprite titleIcon;
  private GadgetSite site;
  private GadgetSiteModel siteModel;
  private PurchaseUI purchaseUI;
  private const string ERR_CANNOT_PICKUP_GADGET = "e.cannot_pickup_gadget";
  private const string ERR_CANNOT_DESTROY_GADGET = "e.cannot_destroy_gadget";

  public void SetSite(GadgetSite site, GadgetSiteModel siteModel)
  {
    this.site = site;
    this.siteModel = siteModel;
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnPlaceGadgetOpen();
    RebuildUI();
  }

  public void RebuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "PlaceGadgetUI.RebuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    purchaseUi.transform.SetParent(transform, false);
    statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    GadgetDirector gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
    List<PurchaseUI.Purchasable> purchasableList1 = new List<PurchaseUI.Purchasable>();
    Dictionary<PediaDirector.Id, List<PurchaseUI.Purchasable>> dict = new Dictionary<PediaDirector.Id, List<PurchaseUI.Purchasable>>();
    Gadget.Id attachedId = site.GetAttachedId();
    if (attachedId == Gadget.Id.NONE)
    {
      foreach (GadgetDefinition gadgetDefinition in SRSingleton<GameContext>.Instance.LookupDirector.GadgetDefinitions)
      {
        GadgetDirector.PlacementError placementError = gadgetDir.GetPlacementError(site, gadgetDefinition.id);
        string lowerInvariant = Enum.GetName(typeof (Gadget.Id), gadgetDefinition.id).ToLowerInvariant();
        Gadget.Id finalId = gadgetDefinition.id;
        string warning = placementError != null ? placementError.message : (gadgetDefinition.destroyOnRemoval || Gadget.IsLinkDestroyerType(gadgetDefinition.id) ? "w.gadget_install_permanent" : null);
        PurchaseUI.Purchasable purchasable = new PurchaseUI.Purchasable("m.gadget.name." + lowerInvariant, gadgetDefinition.icon, gadgetDefinition.icon, "m.gadget.desc." + lowerInvariant, 0, new PediaDirector.Id?(gadgetDefinition.pediaLink), () => Place(finalId), () => gadgetDir.GetGadgetCount(finalId) > 0, () => gadgetDir.CanPlaceGadget(site, finalId), placementError?.button, warning, () => gadgetDir.GetGadgetCount(finalId));
        purchasableList1.Add(purchasable);
        List<PurchaseUI.Purchasable> purchasableList2 = dict.Get(gadgetDefinition.pediaLink) ?? new List<PurchaseUI.Purchasable>();
        purchasableList2.Add(purchasable);
        dict[gadgetDefinition.pediaLink] = purchasableList2;
      }
    }
    else if (site.DestroysLinkedPairOnRemoval())
      purchasableList1.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_linked_gadget"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_linked_gadget"), demolish.cost, new PediaDirector.Id?(), DemolishPair, () => true, () => true, "b.demolish", site.DestroyingWillDestroyContents() ? "w.destroying_gadget_destroys_contents" : null));
    else if (site.DestroysOnRemoval() || GordoSnare.HasSnaredGordo(site))
    {
      purchasableList1.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_gadget"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_gadget"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish", site.DestroyingWillDestroyContents() ? "w.destroying_gadget_destroys_contents" : null));
    }
    else
    {
      string warning = attachedId != Gadget.Id.DRONE && attachedId != Gadget.Id.DRONE_ADVANCED || !site.GetAttached().GetComponentInChildren<Drone>().ammo.Any() ? (site.DestroyingWillDestroyContents() ? "w.pick_up_gadget_destroys_contents" : null) : "w.drone_reprogram_drops_ammo";
      purchasableList1.Add(new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.pick_up_gadget"), pickUp.icon, pickUp.img, MessageUtil.Qualify("ui", "m.desc.pick_up_gadget"), pickUp.cost, new PediaDirector.Id?(), PickUp, () => true, () => true, "b.pick_up", warning));
    }
    GameObject purchaseUi = SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.place_gadget"), purchasableList1.ToArray(), true, Close);
    if (attachedId == Gadget.Id.NONE)
    {
      List<PurchaseUI.Category> categories = new List<PurchaseUI.Category>();
      foreach (PediaDirector.Id key in PediaUI.SCIENCE_ENTRIES)
      {
        if (dict.ContainsKey(key))
          categories.Add(new PurchaseUI.Category(key.ToString().ToLowerInvariant(), dict[key].ToArray()));
      }
      purchaseUi.GetComponent<PurchaseUI>().SetCategories(categories);
      purchaseUi.GetComponent<PurchaseUI>().SetPurchaseMsgs("b.place", "b.place");
    }
    return purchaseUi;
  }

  private void Place(Gadget.Id id)
  {
    SRSingleton<SceneContext>.Instance.GameModel.InstantiateGadget(SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(id).prefab, siteModel);
    site.RotateToPlayer();
    PlayPurchaseCue();
    SRSingleton<SceneContext>.Instance.GadgetDirector.SpendGadget(id);
    Close();
    AnalyticsUtil.CustomEvent("PlaceGadget." + id.ToString(), new Dictionary<string, object>()
    {
      {
        "GadgetSite.Position",
        AnalyticsUtil.GetEventData(site.transform.position)
      },
      {
        "GadgetSite.Id",
        site.id
      },
      {
        "Gadget.Id",
        id
      }
    });
  }

  public void Demolish()
  {
    if (site.HasAttached())
    {
      if (!site.DestroysOnRemoval() && !GordoSnare.HasSnaredGordo(site) || site.DestroysLinkedPairOnRemoval())
      {
        Error("e.cannot_destroy_gadget");
      }
      else
      {
        site.DestroyAttached();
        Play(SRSingleton<GameContext>.Instance.UITemplates.removeGadgetCue);
        RebuildUI();
        purchaseUI.PlayPurchaseFX();
      }
    }
    else
      Error("e.cannot_destroy_gadget");
  }

  public void DemolishPair()
  {
    if (site.HasAttached())
    {
      if (!site.DestroysLinkedPairOnRemoval())
      {
        Error("e.cannot_destroy_gadget");
      }
      else
      {
        site.DestroyAttachedWithPair();
        Play(SRSingleton<GameContext>.Instance.UITemplates.removeGadgetCue);
        RebuildUI();
        purchaseUI.PlayPurchaseFX();
      }
    }
    else
      Error("e.cannot_destroy_gadget");
  }

  public void PickUp()
  {
    if (site.HasAttached())
    {
      if (site.DestroysOnRemoval() || site.DestroysLinkedPairOnRemoval())
      {
        Error("e.cannot_pickup_gadget");
      }
      else
      {
        Gadget.Id attachedId = site.GetAttachedId();
        site.DestroyAttached();
        Play(SRSingleton<GameContext>.Instance.UITemplates.removeGadgetCue);
        SRSingleton<SceneContext>.Instance.GadgetDirector.AddGadget(attachedId);
        RebuildUI();
        purchaseUI.PlayPurchaseFX();
      }
    }
    else
      Error("e.cannot_pickup_gadget");
  }

  protected void PlayPurchaseCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.placeGadgetCue);

  [Serializable]
  public class PurchaseItem
  {
    public Sprite icon;
    public Sprite img;
    public int cost;
  }
}
