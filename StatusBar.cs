// Decompiled with JetBrains decompiler
// Type: StatusBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StatusBar : MonoBehaviour
{
  [Tooltip("The image for the bar which we're filling up.")]
  public Image statusImage;
  [Tooltip("The text we will fill in.")]
  public TMP_Text label;
  public float minValue;
  public float maxValue = 100f;
  public float currValue = 50f;
  [Tooltip("The formatting to use to form our text.")]
  public string format;
  [Tooltip("The formatting to use to form our text when empty, or null if we should use default.")]
  public string emptyFormat;
  [Tooltip("The formatting to use to form our text when full, or null if we should use default.")]
  public string fullFormat;
  public bool translate;
  private float lastMinValue = float.NaN;
  private float lastMaxValue = float.NaN;
  private float lastCurrValue = float.NaN;
  private MessageBundle uiBundle;

  public Color barColor
  {
    set
    {
      if (!(statusImage != null))
        return;
      statusImage.color = value;
    }
  }

  public void Start()
  {
    if (!Application.isPlaying)
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);
  }

  public void OnDestroy()
  {
    if (!Application.isPlaying || !(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
  }

  private void OnBundlesAvailable(MessageDirector msgDir)
  {
    uiBundle = msgDir.GetBundle("ui");
    OnChanged();
  }

  public void Update()
  {
    if (minValue == (double) lastMinValue && maxValue == (double) lastMaxValue && currValue == (double) lastCurrValue)
      return;
    OnChanged();
    lastMinValue = minValue;
    lastMaxValue = maxValue;
    lastCurrValue = currValue;
  }

  private void OnChanged()
  {
    float pct = (float) ((currValue - (double) minValue) / (maxValue - (double) minValue));
    string format = this.format;
    if (pct == 0.0 && emptyFormat != null && emptyFormat != "")
      format = emptyFormat;
    else if (pct == 1.0 && fullFormat != null && fullFormat != "")
      format = fullFormat;
    if (label != null)
      label.text = ApplyFormat(format, pct);
    if (!(statusImage != null))
      return;
    statusImage.fillAmount = pct;
  }

  protected virtual string ApplyFormat(string format, float pct)
  {
    format = format.Replace("{cur}", string.Concat(currValue));
    format = format.Replace("{min}", string.Concat(minValue));
    format = format.Replace("{max}", string.Concat(maxValue));
    format = format.Replace("{cur%}", string.Format("{0:00}", (float) (pct * 100.0)));
    format = format.Replace("{cur2%}", string.Format("{0:00.0}", (float) (pct * 100.0)));
    if (translate && Application.isPlaying && uiBundle != null)
      format = uiBundle.Xlate(format);
    return format;
  }
}
