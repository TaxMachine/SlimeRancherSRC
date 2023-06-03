// Decompiled with JetBrains decompiler
// Type: CheckboxStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Toggle))]
public class CheckboxStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.CheckboxStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Toggle toggle;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    toggle = GetComponent<Toggle>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.CheckboxStyle checkboxStyle = styleDir.GetCheckboxStyle(styleName);
    if (checkboxStyle == null)
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
        TextStyler.ApplyTextStyle(text, checkboxStyle);
      if (checkboxStyle.bgSprite.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.enabled = checkboxStyle.bgSprite != null;
      if (checkboxStyle.bgColor.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.color = checkboxStyle.bgColor.value;
      if (checkboxStyle.bgSprite.apply && toggle.targetGraphic is Image)
        ((Image) toggle.targetGraphic).sprite = checkboxStyle.bgSprite.value;
      if (checkboxStyle.markColor.apply && toggle.graphic != null)
        toggle.graphic.color = checkboxStyle.markColor.value;
      if (checkboxStyle.markSprite.apply && toggle.graphic is Image)
        ((Image) toggle.graphic).sprite = checkboxStyle.markSprite.value;
      ColorBlock colors = toggle.colors;
      if (checkboxStyle.normalTint.apply)
        colors.normalColor = checkboxStyle.normalTint.value;
      if (checkboxStyle.highlightedTint.apply)
        colors.highlightedColor = checkboxStyle.highlightedTint.value;
      if (checkboxStyle.pressedTint.apply)
        colors.pressedColor = checkboxStyle.pressedTint.value;
      if (checkboxStyle.disabledTint.apply)
        colors.disabledColor = checkboxStyle.disabledTint.value;
      toggle.colors = colors;
    }
  }
}
