// Decompiled with JetBrains decompiler
// Type: ColorShopUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorShopUI : BaseUI
{
  public Sprite titleIcon;
  public SECTR_AudioCue selectCue;
  public RanchDirector.PaletteType[] paletteTypes;
  private PurchaseUI purchaseUI;
  private AchievementsDirector achieveDir;

  public override void Awake()
  {
    base.Awake();
    SRSingleton<SceneContext>.Instance.PediaDirector.UnlockWithoutPopup(PediaDirector.Id.CHROMA);
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    BuildUI();
  }

  public void BuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "ColorShopUI.BuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUi.transform.SetParent(transform, false);
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    statusArea = purchaseUI.statusArea;
  }

  protected GameObject CreatePurchaseUI()
  {
    RanchDirector ranchDirector = SRSingleton<SceneContext>.Instance.RanchDirector;
    List<PurchaseUI.Purchasable> purchasableList = new List<PurchaseUI.Purchasable>();
    List<RanchDirector.PaletteEntry> orderedPalettes = SRSingleton<SceneContext>.Instance.RanchDirector.GetOrderedPalettes();
    List<PurchaseUI.Category> categories = new List<PurchaseUI.Category>();
    foreach (RanchDirector.PaletteType paletteType in paletteTypes)
    {
      PurchaseUI.Purchasable[] purchasableArray = new PurchaseUI.Purchasable[orderedPalettes.Count];
      for (int index = 0; index < orderedPalettes.Count; ++index)
      {
        RanchDirector.PaletteEntry entry = orderedPalettes[index];
        PurchaseUI.Purchasable purchasable = CreatePurchasable(ranchDirector, entry, paletteType);
        purchasableList.Add(purchasable);
        purchasableArray[index] = purchasable;
      }
      string lowerInvariant = Enum.GetName(typeof (RanchDirector.PaletteType), paletteType).ToLowerInvariant();
      categories.Add(new PurchaseUI.Category(lowerInvariant, purchasableArray));
    }
    GameObject purchaseUi = SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.chroma_packs"), purchasableList.ToArray(), true, Close, true);
    purchaseUi.GetComponent<PurchaseUI>().SetCategories(categories);
    purchaseUi.GetComponent<PurchaseUI>().SetPurchaseMsgs("b.select", "b.already_selected");
    return purchaseUi;
  }

  private PurchaseUI.Purchasable CreatePurchasable(
    RanchDirector ranchDir,
    RanchDirector.PaletteEntry entry,
    RanchDirector.PaletteType paletteType)
  {
    string lowerInvariant = Enum.GetName(typeof (RanchDirector.Palette), entry.palette).ToLowerInvariant();
    RanchDirector.Palette finalPalette = entry.palette;
    return new PurchaseUI.Purchasable("m.palette.name." + lowerInvariant, entry.icon, entry.icon, "m.palette.desc", 0, new PediaDirector.Id?(), () => SelectPalette(paletteType, finalPalette), () => ranchDir.HasPalette(finalPalette), () => !ranchDir.IsSelectedPalette(finalPalette, paletteType));
  }

  private void SelectPalette(RanchDirector.PaletteType paletteType, RanchDirector.Palette palette)
  {
    SRSingleton<SceneContext>.Instance.GameModel.GetRanchModel().SelectPalette(paletteType, palette);
    Play(selectCue);
    purchaseUI.PlayPurchaseFX();
    purchaseUI.Rebuild(true);
    achieveDir.AddToStat(AchievementsDirector.EnumStat.USE_CHROMAS, paletteType);
  }

  protected void PlayPurchaseCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseCue);
}
