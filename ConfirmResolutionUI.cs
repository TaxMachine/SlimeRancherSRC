// Decompiled with JetBrains decompiler
// Type: ConfirmResolutionUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class ConfirmResolutionUI : SRBehaviour
{
  public TMP_Text countdownText;
  public OnCancel onCancel;
  public OnConfirm onConfirm;
  private float expireTime;
  private const float COUNTDOWN_TIME = 15f;

  public void Awake() => expireTime = Time.unscaledTime + 15f;

  public void OK()
  {
    if (onConfirm != null)
      onConfirm();
    Destroyer.Destroy(gameObject, "ConfirmResolutionUI.OK");
  }

  public void Cancel()
  {
    if (onCancel != null)
      onCancel();
    Destroyer.Destroy(gameObject, "ConfirmResolutionUI.Cancel");
  }

  public void Update()
  {
    float f = expireTime - Time.unscaledTime;
    if (SRInput.PauseActions.cancel.WasPressed || f <= 0.0)
      Cancel();
    else
      countdownText.text = Mathf.FloorToInt(f).ToString();
  }

  public delegate void OnCancel();

  public delegate void OnConfirm();
}
