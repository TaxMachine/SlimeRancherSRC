// Decompiled with JetBrains decompiler
// Type: ToyShopUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToyShopUI : BaseUI
{
  public Sprite titleIcon;
  private PlayerState playerState;
  private LookupDirector lookupDir;
  private PurchaseUI purchaseUI;
  private AchievementsDirector achieveDir;
  private ToyDirector toyDirector;
  private const float EJECT_FORCE = 25f;
  [HideInInspector]
  public GameObject ejectionPoint;
  [HideInInspector]
  public RegionRegistry.RegionSetId regionSetId;

  public override void Awake()
  {
    base.Awake();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    toyDirector = SRSingleton<GameContext>.Instance.ToyDirector;
    SRSingleton<SceneContext>.Instance.PediaDirector.UnlockWithoutPopup(PediaDirector.Id.SLIME_TOYS);
    RebuildUI();
  }

  public void RebuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "ToyShopUI.RebuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    purchaseUi.transform.SetParent(transform, false);
    statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    List<PurchaseUI.Purchasable> purchasableList = new List<PurchaseUI.Purchasable>();
    foreach (Identifiable.Id purchaseableToy in toyDirector.GetPurchaseableToys())
      purchasableList.Add(CreatePurchasableToy(purchaseableToy));
    return SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.slime_toys"), purchasableList.ToArray(), false, Close);
  }

  private PurchaseUI.Purchasable CreatePurchasableToy(Identifiable.Id toyId)
  {
    ToyDefinition toy = lookupDir.GetToyDefinition(toyId);
    return new PurchaseUI.Purchasable(string.Format("m.toy.name.{0}", toy.NameKey), toy.Icon, toy.Icon, string.Format("m.toy.desc.{0}", toy.NameKey), toy.Cost, new PediaDirector.Id?(), () => BuyToy(toyId, toy.Cost), () => true, () => true);
  }

  protected void BuyToy(Identifiable.Id toyId, int cost)
  {
    if (playerState.GetCurrency() >= cost)
    {
      Play(SRSingleton<GameContext>.Instance.UITemplates.purchasePersonalUpgradeCue);
      playerState.SpendCurrency(cost);
      InstantiateToy(toyId);
      purchaseUI.PlayPurchaseFX();
      achieveDir.AddToStat(AchievementsDirector.EnumStat.SLIME_TOYS_BOUGHT, toyId);
      Close();
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_coins");
    }
  }

  private void InstantiateToy(Identifiable.Id toyId)
  {
    if (!(ejectionPoint != null))
      return;
    Rigidbody component = InstantiateActor(lookupDir.GetPrefab(toyId), regionSetId, ejectionPoint.transform.position, ejectionPoint.transform.rotation).GetComponent<Rigidbody>();
    component.isKinematic = false;
    component.AddForce(transform.forward * 25f);
  }
}
