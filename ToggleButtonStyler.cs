// Decompiled with JetBrains decompiler
// Type: ToggleButtonStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Toggle))]
public class ToggleButtonStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.ToggleButtonStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Toggle toggle;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    toggle = GetComponent<Toggle>();
    ApplyStyle();
  }

  public void ChangeStyle(string styleName)
  {
    this.styleName = styleName;
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.ToggleButtonStyle toggleButtonStyle = styleDir.GetToggleButtonStyle(styleName);
    if (toggleButtonStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown panel style: " + styleName);
    }
    else
    {
      List<Text> textList = new List<Text>();
      foreach (Text componentsInChild in GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>())
          textList.Add(componentsInChild);
      }
      foreach (Text text in textList)
        TextStyler.ApplyTextStyle(text, toggleButtonStyle);
      if (toggleButtonStyle.bgSprite.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.enabled = toggleButtonStyle.bgSprite != null;
      if (toggleButtonStyle.bgColor.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.color = toggleButtonStyle.bgColor.value;
      if (toggleButtonStyle.bgSprite.apply && toggle.targetGraphic is Image)
        ((Image) toggle.targetGraphic).sprite = toggleButtonStyle.bgSprite.value;
      if (toggleButtonStyle.selectedColor.apply && toggle.graphic != null)
        toggle.graphic.color = toggleButtonStyle.selectedColor.value;
      if (toggleButtonStyle.selectedSprite.apply && toggle.graphic is Image)
        ((Image) toggle.graphic).sprite = toggleButtonStyle.selectedSprite.value;
      ColorBlock colors = toggle.colors;
      if (toggleButtonStyle.normalTint.apply)
        colors.normalColor = toggleButtonStyle.normalTint.value;
      if (toggleButtonStyle.highlightedTint.apply)
        colors.highlightedColor = toggleButtonStyle.highlightedTint.value;
      if (toggleButtonStyle.pressedTint.apply)
        colors.pressedColor = toggleButtonStyle.pressedTint.value;
      if (toggleButtonStyle.disabledTint.apply)
        colors.disabledColor = toggleButtonStyle.disabledTint.value;
      toggle.colors = colors;
    }
  }
}
