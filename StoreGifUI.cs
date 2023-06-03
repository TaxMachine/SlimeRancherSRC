// Decompiled with JetBrains decompiler
// Type: StoreGifUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class StoreGifUI : BaseUI
{
  public OnConfirm onConfirm;
  public Button okBtn;
  public Button cancelBtn;
  public float ellipsisChangeTime;
  public float ellipsisCount;
  private bool storing;

  public void OK()
  {
    okBtn.interactable = false;
    cancelBtn.interactable = false;
    storing = true;
    onConfirm();
  }

  public void Cancel() => Destroyer.Destroy(gameObject, "StoreGifUI.Cancel");

  public override void Update()
  {
    base.Update();
    if (!storing || Time.unscaledTime <= (double) ellipsisChangeTime)
      return;
    Status(MessageUtil.Compose("m.ellipsize" + ellipsisCount, new string[1]
    {
      "m.gif_storing"
    }));
    ellipsisCount = (float) ((ellipsisCount + 1.0) % 4.0);
    ellipsisChangeTime = Time.unscaledTime + 0.5f;
  }

  public delegate void OnConfirm();
}
