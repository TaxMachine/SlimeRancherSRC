// Decompiled with JetBrains decompiler
// Type: LandPlotUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public abstract class LandPlotUI : BaseUI
{
  protected LandPlot activator;
  protected PlayerState playerState;
  private PurchaseUI purchaseUI;
  private const string ERR_ALREADY_HAS_UPGRADE = "e.already_has_upgrade";

  public override void Awake()
  {
    base.Awake();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
  }

  protected abstract GameObject CreatePurchaseUI();

  public virtual void SetActivator(LandPlot activator)
  {
    this.activator = activator;
    RebuildUI();
  }

  public void RebuildUI()
  {
    for (int index = 0; index < transform.childCount; ++index)
      Destroyer.Destroy(transform.GetChild(index).gameObject, "LandPlotUI.RebuildUI");
    GameObject purchaseUi = CreatePurchaseUI();
    purchaseUI = purchaseUi.GetComponent<PurchaseUI>();
    purchaseUi.transform.SetParent(transform, false);
    statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
  }

  protected GameObject Replace(GameObject replacementPrefab)
  {
    GameObject gameObject = activator.transform.parent.GetComponent<LandPlotLocation>().Replace(activator, replacementPrefab);
    Close();
    return gameObject;
  }

  protected void Upgrade(LandPlot.Upgrade upgrade, int cost)
  {
    if (activator.HasUpgrade(upgrade))
      Error("e.already_has_upgrade");
    else if (playerState.GetCurrency() >= cost)
    {
      playerState.SpendCurrency(cost);
      activator.AddUpgrade(upgrade);
      PlayPurchaseUpgradeCue();
      RebuildUI();
      purchaseUI.PlayPurchaseFX();
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_coins");
    }
  }

  protected bool BuyPlot(PlotPurchaseItem plot)
  {
    if (playerState.GetCurrency() >= plot.cost)
    {
      playerState.SpendCurrency(plot.cost);
      PlayPurchaseCue();
      Replace(plot.plotPrefab);
      return true;
    }
    PlayErrorCue();
    Error("e.insuf_coins");
    return false;
  }

  protected void PlayPurchaseUpgradeCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseUpgradeCue);

  protected void PlayPurchaseCue() => Play(SRSingleton<GameContext>.Instance.UITemplates.purchasePlotCue);

  [Serializable]
  public class PurchaseItem
  {
    public Sprite icon;
    public Sprite img;
    public int cost;
  }

  [Serializable]
  public class PlotPurchaseItem : PurchaseItem
  {
    public GameObject plotPrefab;
  }

  [Serializable]
  public class UpgradePurchaseItem : PurchaseItem
  {
    public LandPlot.Upgrade upgrade;
  }
}
