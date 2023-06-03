// Decompiled with JetBrains decompiler
// Type: AccessDoorUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class AccessDoorUI : BaseUI
{
  public Sprite titleIcon;
  private PlayerState playerState;
  private AccessDoor door;

  public override void Awake()
  {
    base.Awake();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
  }

  public void SetAccessDoor(AccessDoor door)
  {
    this.door = door;
    if (door.CurrState == AccessDoor.State.LOCKED)
    {
      GameObject purchaseUi = CreatePurchaseUI();
      purchaseUi.transform.SetParent(transform, false);
      statusArea = purchaseUi.GetComponent<PurchaseUI>().statusArea;
    }
    else
    {
      Close();
      door.CurrState = AccessDoor.State.OPEN;
      SRSingleton<SceneContext>.Instance.PediaDirector.ShowPedia(door.lockedRegionId);
    }
  }

  protected GameObject CreatePurchaseUI()
  {
    PurchaseUI.Purchasable[] purchasables = new PurchaseUI.Purchasable[1]
    {
      new PurchaseUI.Purchasable("t." + door.lockedRegionId.ToString().ToLowerInvariant(), door.doorPurchase.icon, door.doorPurchase.img, "m.intro." + door.lockedRegionId.ToString().ToLowerInvariant(), door.doorPurchase.cost, new PediaDirector.Id?(door.lockedRegionId), UnlockDoor, () => true, () => true)
    };
    GameObject purchaseUi = SRSingleton<GameContext>.Instance.UITemplates.CreatePurchaseUI(titleIcon, MessageUtil.Qualify("ui", "t.access_door"), purchasables, false, Close);
    purchaseUi.GetComponent<PurchaseUI>().Select(purchasables[0]);
    purchaseUi.GetComponent<PurchaseUI>().HideSelectionPanel();
    return purchaseUi;
  }

  public void UnlockDoor()
  {
    if (playerState.GetCurrency() >= door.doorPurchase.cost)
    {
      playerState.SpendCurrency(door.doorPurchase.cost);
      door.CurrState = AccessDoor.State.OPEN;
      if (door.linkedDoors != null)
      {
        foreach (AccessDoor linkedDoor in door.linkedDoors)
        {
          if (linkedDoor.CurrState == AccessDoor.State.LOCKED)
            linkedDoor.CurrState = AccessDoor.State.CLOSED;
        }
      }
      Play(SRSingleton<GameContext>.Instance.UITemplates.purchaseExpansionCue);
      Close();
      SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow();
    }
    else
    {
      PlayErrorCue();
      Error("e.insuf_coins");
    }
  }
}
