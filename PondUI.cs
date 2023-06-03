// Decompiled with JetBrains decompiler
// Type: PondUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class PondUI : LandPlotUI
{
  public PlotPurchaseItem demolish;
  public Sprite titleIcon;

  protected override GameObject CreatePurchaseUI() => SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, "t.pond", new PurchaseUI.Purchasable[1]
  {
    new PurchaseUI.Purchasable(MessageUtil.Qualify("ui", "l.demolish_plot"), demolish.icon, demolish.img, MessageUtil.Qualify("ui", "m.desc.demolish_plot"), demolish.cost, new PediaDirector.Id?(), Demolish, () => true, () => true, "b.demolish")
  }, false, Close);

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
