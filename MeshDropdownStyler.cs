// Decompiled with JetBrains decompiler
// Type: MeshDropdownStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (TMP_Dropdown))]
public class MeshDropdownStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.MeshDropdownStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private TMP_Dropdown dropdown;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    dropdown = GetComponent<TMP_Dropdown>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.MeshDropdownStyle meshDropdownStyle = styleDir.GetMeshDropdownStyle(styleName);
    if (meshDropdownStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown dropdown style: " + styleName);
    }
    else
    {
      List<TMP_Text> tmpTextList = new List<TMP_Text>();
      foreach (TMP_Text componentsInChild in GetComponentsInChildren<TMP_Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          tmpTextList.Add(componentsInChild);
      }
      foreach (TMP_Text text in tmpTextList)
        MeshTextStyler.ApplyTextStyle(text, meshDropdownStyle);
      if (meshDropdownStyle.bgSprite.apply)
        dropdown.image.enabled = meshDropdownStyle.bgSprite.value != null;
      if (meshDropdownStyle.bgColor.apply)
        dropdown.image.color = meshDropdownStyle.bgColor.value;
      ColorBlock colors1 = dropdown.colors;
      if (meshDropdownStyle.normalTint.apply)
        colors1.normalColor = meshDropdownStyle.normalTint.value;
      if (meshDropdownStyle.highlightedTint.apply)
        colors1.highlightedColor = meshDropdownStyle.highlightedTint.value;
      if (meshDropdownStyle.pressedTint.apply)
        colors1.pressedColor = meshDropdownStyle.pressedTint.value;
      if (meshDropdownStyle.disabledTint.apply)
        colors1.disabledColor = meshDropdownStyle.disabledTint.value;
      dropdown.colors = colors1;
      if (meshDropdownStyle.bgSprite.apply)
        dropdown.image.sprite = meshDropdownStyle.bgSprite.value;
      Image component = dropdown.template.GetComponent<Image>();
      if (meshDropdownStyle.menuBgSprite.apply)
      {
        bool flag = meshDropdownStyle.menuBgSprite.value != null;
        component.enabled = flag;
        if (!flag)
          component.sprite = meshDropdownStyle.menuBgSprite.value;
      }
      if (meshDropdownStyle.menuBgColor.apply)
        component.color = meshDropdownStyle.menuBgColor.value;
      Toggle componentsInChild1 = component.GetComponentsInChildren<Toggle>(true)[0];
      Image targetGraphic = (Image) componentsInChild1.targetGraphic;
      if (meshDropdownStyle.itemBgSprite.apply)
      {
        bool flag = meshDropdownStyle.itemBgSprite.value != null;
        targetGraphic.enabled = flag;
        if (!flag)
          targetGraphic.sprite = meshDropdownStyle.itemBgSprite.value;
      }
      if (meshDropdownStyle.itemBgColor.apply)
        targetGraphic.color = meshDropdownStyle.itemBgColor.value;
      ColorBlock colors2 = componentsInChild1.colors;
      if (meshDropdownStyle.itemNormalTint.apply)
        colors2.normalColor = meshDropdownStyle.itemNormalTint.value;
      if (meshDropdownStyle.itemHighlightedTint.apply)
        colors2.highlightedColor = meshDropdownStyle.itemHighlightedTint.value;
      if (meshDropdownStyle.itemPressedTint.apply)
        colors2.pressedColor = meshDropdownStyle.itemPressedTint.value;
      if (meshDropdownStyle.itemDisabledTint.apply)
        colors2.disabledColor = meshDropdownStyle.itemDisabledTint.value;
      componentsInChild1.colors = colors2;
    }
  }

  public static void Convert(GameObject obj)
  {
    foreach (DropdownStyler componentsInChild1 in obj.GetComponentsInChildren<DropdownStyler>(true))
    {
      Dropdown component1 = componentsInChild1.GetComponent<Dropdown>();
      DropdownData dropdownData = new DropdownData(component1, componentsInChild1);
      if ((bool) (Object) component1.itemText.GetComponent<TextStyler>())
        MeshTextStyler.Convert(component1.itemText.GetComponent<TextStyler>());
      if ((bool) (Object) component1.captionText.GetComponent<TextStyler>())
        MeshTextStyler.Convert(component1.captionText.GetComponent<TextStyler>());
      GameObject gameObject1 = componentsInChild1.gameObject;
      InitSelected component2 = component1.GetComponent<InitSelected>();
      bool flag = component2 != null;
      if (component2 != null)
        DestroyImmediate(component2);
      DestroyImmediate(componentsInChild1);
      DestroyImmediate(component1);
      TMP_Dropdown dropdown = gameObject1.AddComponent<TMP_Dropdown>();
      MeshDropdownStyler styler = gameObject1.AddComponent<MeshDropdownStyler>();
      foreach (Text componentsInChild2 in gameObject1.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild2.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild2.GetComponent<MeshTextStyler>())
        {
          GameObject gameObject2 = componentsInChild2.gameObject;
          DestroyImmediate(componentsInChild2);
          gameObject2.AddComponent<TextMeshProUGUI>();
        }
      }
      dropdownData.ApplyTo(dropdown, styler);
      if (flag)
        gameObject1.AddComponent<InitSelected>();
    }
  }

  protected class DropdownData
  {
    private Dictionary<string, MeshTextStyler.TextData> textDict;
    private string styleName;
    private RectTransform template;
    private GameObject captionTextObj;
    private Image captionImage;
    private GameObject itemTextObj;
    private Image itemImage;

    public DropdownData(Dropdown dropdown, DropdownStyler styler)
    {
      textDict = new Dictionary<string, MeshTextStyler.TextData>();
      foreach (Text componentsInChild in styler.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          textDict[GetPath(componentsInChild.transform)] = new MeshTextStyler.TextData(componentsInChild, null);
      }
      styleName = styler.styleName;
      template = dropdown.template;
      captionTextObj = dropdown.captionText == null ? null : dropdown.captionText.gameObject;
      captionImage = dropdown.captionImage;
      itemTextObj = dropdown.itemText == null ? null : dropdown.itemText.gameObject;
      itemImage = dropdown.itemImage;
    }

    private string GetPath(Transform trans) => trans.parent != null ? GetPath(trans.parent) + "/" + trans.name : trans.name;

    public void ApplyTo(TMP_Dropdown dropdown, MeshDropdownStyler styler)
    {
      foreach (TextMeshProUGUI componentsInChild in styler.GetComponentsInChildren<TextMeshProUGUI>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          textDict[GetPath(componentsInChild.transform)].ApplyTo(componentsInChild, null);
      }
      styler.styleName = styleName;
      dropdown.template = template;
      dropdown.captionText = captionTextObj == null ? null : captionTextObj.GetComponent<TMP_Text>();
      dropdown.captionImage = captionImage;
      dropdown.itemText = itemTextObj == null ? null : itemTextObj.GetComponent<TMP_Text>();
      dropdown.itemImage = itemImage;
    }
  }
}
