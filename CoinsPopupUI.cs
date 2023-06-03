// Decompiled with JetBrains decompiler
// Type: CoinsPopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsPopupUI : MonoBehaviour
{
  public TMP_Text amountText;
  public Image icon;
  public SECTR_AudioCue cue;
  public CanvasGroup canvasGroup;
  private const float FADE_IN_TIME = 0.1f;
  private const float FADE_OUT_TIME = 0.3f;
  private const float MOVE_TIME = 1f;
  private const float MOVE_AMOUNT = 180f;

  private void Start()
  {
    SECTR_AudioSystem.Play(cue, Vector3.zero, false);
    AnimateCoinPopup();
  }

  public void Init(
    int amount,
    Sprite overrideIcon,
    Color? overrideColor,
    SECTR_AudioCue overrideCue)
  {
    amountText.text = (amount >= 0 ? "+" : (object) "").ToString() + amount;
    if (overrideIcon != null)
      icon.sprite = overrideIcon;
    if (overrideColor.HasValue)
      amountText.color = overrideColor.Value;
    if (!(overrideCue != null))
      return;
    cue = overrideCue;
  }

  private void AnimateCoinPopup() => DOTween.Sequence().Append(transform.DOBlendableMoveBy(Vector3.up * 180f, 1f)).Join(canvasGroup.DOFade(1f, 0.1f).From(0.0f)).Append(canvasGroup.DOFade(0.0f, 0.3f)).OnComplete(() => Destroyer.Destroy(gameObject, "CoinsPopupUI.DoSequence")).SetUpdate(true);
}
