// Decompiled with JetBrains decompiler
// Type: DLCManageUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DLCManageUI : BaseUI
{
  [Tooltip("Icon displayed at the top of the modal (see PurchaseUI).")]
  public Sprite icon;
  [Tooltip("Prefab showing the 'included in...' text/icon.")]
  public DLCManageUI_IncludedInPackage includedInPackagePrefab;
  private DLCManageUI_IncludedInPackage includedInPackage;
  private const float MIN_LOADING_TIME = 0.25f;
  private PurchaseUI purchaseUI;

  public static bool IsEnabled()
  {
    DLCDirector director = SRSingleton<GameContext>.Instance.DLCDirector;
    return (Levels.isSpecial() || SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().enableDLC) && director.GetSupportedPackages().Any(id => director.HasReached(id, DLCPackage.State.AVAILABLE));
  }

  public override void Awake()
  {
    base.Awake();
    includedInPackage = Instantiate(includedInPackagePrefab.gameObject).GetComponent<DLCManageUI_IncludedInPackage>();
    purchaseUI = SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(icon, "t.dlc", new PurchaseUI.Purchasable[0], true, Close).GetComponent<PurchaseUI>();
    includedInPackage.transform.SetParent(purchaseUI.customizationPanel.transform, false);
    purchaseUI.transform.SetParent(transform, false);
    purchaseUI.Resize(450f, 600f);
    purchaseUI.ReselectOnReturnFromPedia();
    statusArea = purchaseUI.statusArea;
    StartCoroutine(Refresh_Coroutine());
  }

  public void OnApplicationFocus(bool hasFocus)
  {
    if (!hasFocus)
      return;
    StartCoroutine(Refresh_Coroutine());
  }

  private IEnumerator Refresh_Coroutine()
  {
    float minLoadingTime = Time.unscaledTime + 0.25f;
    purchaseUI.SetActivePanels(PurchaseUI.Panel.LOADING);
    purchaseUI.ClearButtons();
    DLCDirector director = SRSingleton<GameContext>.Instance.DLCDirector;
    yield return director.RegisterPackagesAsync();
    yield return new WaitUntil(() => Time.unscaledTime >= (double) minLoadingTime);
    foreach (PurchaseUI.Purchasable purchasable in director.LoadPackageMetadatas().SelectMany(package => package.contents.Select(item => new PurchaseUI.Purchasable()
             {
               nameKey = string.Format("m.dlc.{0}.contents.{1}", package.id.ToString().ToLowerInvariant(), item.id),
               descKey = string.Format("m.dlc.{0}.contents.{1}.desc", package.id.ToString().ToLowerInvariant(), item.id),
               icon = item.image,
               mainImg = item.imageLarge,
               onPurchase = () => director.ShowPackageInStore(package.id),
               onSelected = p => OnPackageSelected(package),
               unlocked = () => director.HasReached(package.id, DLCPackage.State.AVAILABLE),
               avail = () => !director.HasReached(package.id, DLCPackage.State.INSTALLED),
               btnOverride = director.HasReached(package.id, DLCPackage.State.INSTALLED) ? "b.dlc.installed" : "b.dlc.view_in_store"
             })).ToArray())
      purchaseUI.AddButton(purchasable, false);
    purchaseUI.SetActivePanels(PurchaseUI.Panel.DEFAULT);
    purchaseUI.SelectFirst();
  }

  public override void OnBundlesAvailable(MessageDirector msg)
  {
    base.OnBundlesAvailable(msg);
    if (!(purchaseUI != null))
      return;
    purchaseUI.Rebuild(false);
  }

  private void OnPackageSelected(DLCPackageMetadata package)
  {
    if (package.contents.Count > 1)
    {
      includedInPackage.text.text = uiBundle.Get("m.dlc.included_in", new string[1]
      {
        SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia").Get(string.Format("m.dlc.{0}", package.id.ToString().ToLowerInvariant()))
      });
      includedInPackage.icon.sprite = package.icon;
      includedInPackage.gameObject.SetActive(true);
    }
    else
      includedInPackage.gameObject.SetActive(false);
  }
}
