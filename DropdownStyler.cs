// Decompiled with JetBrains decompiler
// Type: DropdownStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Dropdown))]
public class DropdownStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.DropdownStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Dropdown dropdown;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    dropdown = GetComponent<Dropdown>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.DropdownStyle dropdownStyle = styleDir.GetDropdownStyle(styleName);
    if (dropdownStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown dropdown style: " + styleName);
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
        TextStyler.ApplyTextStyle(text, dropdownStyle);
      if (dropdownStyle.bgSprite.apply)
        dropdown.image.enabled = dropdownStyle.bgSprite.value != null;
      if (dropdownStyle.bgColor.apply)
        dropdown.image.color = dropdownStyle.bgColor.value;
      ColorBlock colors1 = dropdown.colors;
      if (dropdownStyle.normalTint.apply)
        colors1.normalColor = dropdownStyle.normalTint.value;
      if (dropdownStyle.highlightedTint.apply)
        colors1.highlightedColor = dropdownStyle.highlightedTint.value;
      if (dropdownStyle.pressedTint.apply)
        colors1.pressedColor = dropdownStyle.pressedTint.value;
      if (dropdownStyle.disabledTint.apply)
        colors1.disabledColor = dropdownStyle.disabledTint.value;
      dropdown.colors = colors1;
      if (dropdownStyle.bgSprite.apply)
        dropdown.image.sprite = dropdownStyle.bgSprite.value;
      Image component = dropdown.template.GetComponent<Image>();
      if (dropdownStyle.menuBgSprite.apply)
      {
        bool flag = dropdownStyle.menuBgSprite.value != null;
        component.enabled = flag;
        if (!flag)
          component.sprite = dropdownStyle.menuBgSprite.value;
      }
      if (dropdownStyle.menuBgColor.apply)
        component.color = dropdownStyle.menuBgColor.value;
      Toggle componentsInChild1 = component.GetComponentsInChildren<Toggle>(true)[0];
      Image targetGraphic = (Image) componentsInChild1.targetGraphic;
      if (dropdownStyle.itemBgSprite.apply)
      {
        bool flag = dropdownStyle.itemBgSprite.value != null;
        targetGraphic.enabled = flag;
        if (!flag)
          targetGraphic.sprite = dropdownStyle.itemBgSprite.value;
      }
      if (dropdownStyle.itemBgColor.apply)
        targetGraphic.color = dropdownStyle.itemBgColor.value;
      ColorBlock colors2 = componentsInChild1.colors;
      if (dropdownStyle.itemNormalTint.apply)
        colors2.normalColor = dropdownStyle.itemNormalTint.value;
      if (dropdownStyle.itemHighlightedTint.apply)
        colors2.highlightedColor = dropdownStyle.itemHighlightedTint.value;
      if (dropdownStyle.itemPressedTint.apply)
        colors2.pressedColor = dropdownStyle.itemPressedTint.value;
      if (dropdownStyle.itemDisabledTint.apply)
        colors2.disabledColor = dropdownStyle.itemDisabledTint.value;
      componentsInChild1.colors = colors2;
    }
  }
}
