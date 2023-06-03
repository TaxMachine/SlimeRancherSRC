// Decompiled with JetBrains decompiler
// Type: MeshCheckboxStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Toggle))]
public class MeshCheckboxStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.MeshCheckboxStyle))]
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
    UIStyleDirector.MeshCheckboxStyle meshCheckboxStyle = styleDir.GetMeshCheckboxStyle(styleName);
    if (meshCheckboxStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown panel style: " + styleName);
    }
    else
    {
      List<TMP_Text> tmpTextList = new List<TMP_Text>();
      foreach (TMP_Text componentsInChild in GetComponentsInChildren<TMP_Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          tmpTextList.Add(componentsInChild);
      }
      foreach (TMP_Text text in tmpTextList)
        MeshTextStyler.ApplyTextStyle(text, meshCheckboxStyle);
      if (meshCheckboxStyle.bgSprite.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.enabled = meshCheckboxStyle.bgSprite != null;
      if (meshCheckboxStyle.bgColor.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.color = meshCheckboxStyle.bgColor.value;
      if (meshCheckboxStyle.bgSprite.apply && toggle.targetGraphic is Image)
        ((Image) toggle.targetGraphic).sprite = meshCheckboxStyle.bgSprite.value;
      if (meshCheckboxStyle.markColor.apply && toggle.graphic != null)
        toggle.graphic.color = meshCheckboxStyle.markColor.value;
      if (meshCheckboxStyle.markSprite.apply && toggle.graphic is Image)
        ((Image) toggle.graphic).sprite = meshCheckboxStyle.markSprite.value;
      ColorBlock colors = toggle.colors;
      if (meshCheckboxStyle.normalTint.apply)
        colors.normalColor = meshCheckboxStyle.normalTint.value;
      if (meshCheckboxStyle.highlightedTint.apply)
        colors.highlightedColor = meshCheckboxStyle.highlightedTint.value;
      if (meshCheckboxStyle.pressedTint.apply)
        colors.pressedColor = meshCheckboxStyle.pressedTint.value;
      if (meshCheckboxStyle.disabledTint.apply)
        colors.disabledColor = meshCheckboxStyle.disabledTint.value;
      toggle.colors = colors;
    }
  }

  public static void Convert(GameObject obj)
  {
    foreach (CheckboxStyler componentsInChild1 in obj.GetComponentsInChildren<CheckboxStyler>(true))
    {
      Toggle component = componentsInChild1.GetComponent<Toggle>();
      CheckboxData checkboxData = new CheckboxData(component, componentsInChild1);
      GameObject gameObject1 = componentsInChild1.gameObject;
      DestroyImmediate(componentsInChild1);
      MeshCheckboxStyler styler = gameObject1.AddComponent<MeshCheckboxStyler>();
      foreach (Text componentsInChild2 in gameObject1.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild2.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild2.GetComponent<MeshTextStyler>())
        {
          GameObject gameObject2 = componentsInChild2.gameObject;
          DestroyImmediate(componentsInChild2);
          gameObject2.AddComponent<TextMeshProUGUI>();
        }
      }
      checkboxData.ApplyTo(component, styler);
    }
  }

  protected class CheckboxData
  {
    private Dictionary<string, MeshTextStyler.TextData> textDict;
    private string styleName;

    public CheckboxData(Toggle checkbox, CheckboxStyler styler)
    {
      textDict = new Dictionary<string, MeshTextStyler.TextData>();
      foreach (Text componentsInChild in styler.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          textDict[GetPath(componentsInChild.transform)] = new MeshTextStyler.TextData(componentsInChild, null);
      }
      styleName = styler.styleName;
    }

    private string GetPath(Transform trans) => trans.parent != null ? GetPath(trans.parent) + "/" + trans.name : trans.name;

    public void ApplyTo(Toggle checkbox, MeshCheckboxStyler styler)
    {
      foreach (TextMeshProUGUI componentsInChild in styler.GetComponentsInChildren<TextMeshProUGUI>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          textDict[GetPath(componentsInChild.transform)].ApplyTo(componentsInChild, null);
      }
      styler.styleName = styleName;
    }
  }
}
