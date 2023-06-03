// Decompiled with JetBrains decompiler
// Type: SliderStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Slider))]
public class SliderStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.SliderStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Slider slider;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    slider = GetComponent<Slider>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.SliderStyle sliderStyle = styleDir.GetSliderStyle(styleName);
    if (sliderStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown slider style: " + styleName);
    }
    else
    {
      Transform transform = slider.transform.Find("Background");
      Image component1 = transform == null ? null : transform.GetComponent<Image>();
      if (sliderStyle.bgSprite.apply && component1 != null)
        component1.enabled = sliderStyle.bgSprite.value != null;
      if (sliderStyle.bgColor.apply && component1 != null)
        component1.color = sliderStyle.bgColor.value;
      if (sliderStyle.bgSprite.apply && component1 != null)
        component1.sprite = sliderStyle.bgSprite.value;
      Image component2 = slider.handleRect == null ? null : slider.handleRect.GetComponent<Image>();
      if (sliderStyle.handleColor.apply && component2 != null)
        component2.color = sliderStyle.handleColor.value;
      if (sliderStyle.handleSprite.apply && component2 != null)
        component2.sprite = sliderStyle.handleSprite.value;
      Image component3 = slider.fillRect == null ? null : slider.fillRect.GetComponent<Image>();
      if (sliderStyle.fillColor.apply && component3 != null)
        component3.color = sliderStyle.fillColor.value;
      if (sliderStyle.fillSprite.apply && component3 != null)
        component3.sprite = sliderStyle.fillSprite.value;
      ColorBlock colors = slider.colors;
      if (sliderStyle.normalTint.apply)
        colors.normalColor = sliderStyle.normalTint.value;
      if (sliderStyle.highlightedTint.apply)
        colors.highlightedColor = sliderStyle.highlightedTint.value;
      if (sliderStyle.pressedTint.apply)
        colors.pressedColor = sliderStyle.pressedTint.value;
      if (sliderStyle.disabledTint.apply)
        colors.disabledColor = sliderStyle.disabledTint.value;
      slider.colors = colors;
    }
  }
}
