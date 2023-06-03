// Decompiled with JetBrains decompiler
// Type: ConfirmUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ConfirmUI : BaseUI
{
  public OnConfirm onConfirm;

  public void OK()
  {
    Destroyer.Destroy(gameObject, "ConfirmUI.OK");
    onConfirm();
  }

  public void Cancel() => Destroyer.Destroy(gameObject, "ConfirmUI.Cancel");

  public delegate void OnConfirm();
}
