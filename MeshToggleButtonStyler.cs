// Decompiled with JetBrains decompiler
// Type: MeshToggleButtonStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Toggle))]
public class MeshToggleButtonStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.MeshToggleButtonStyle))]
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
    UIStyleDirector.MeshToggleButtonStyle toggleButtonStyle = styleDir.GetMeshToggleButtonStyle(styleName);
    if (toggleButtonStyle == null)
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
        MeshTextStyler.ApplyTextStyle(text, toggleButtonStyle);
      if (toggleButtonStyle.bgSprite.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.enabled = toggleButtonStyle.bgSprite != null;
      if (toggleButtonStyle.bgColor.apply && toggle.targetGraphic != null)
        toggle.targetGraphic.color = toggleButtonStyle.bgColor.value;
      if (toggleButtonStyle.bgSprite.apply && toggle.targetGraphic is Image)
        ((Image) toggle.targetGraphic).sprite = toggleButtonStyle.bgSprite.value;
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

  public static void Convert(GameObject obj)
  {
    foreach (ToggleButtonStyler componentsInChild1 in obj.GetComponentsInChildren<ToggleButtonStyler>(true))
    {
      Toggle component = componentsInChild1.GetComponent<Toggle>();
      ToggleButtonData toggleButtonData = new ToggleButtonData(component, componentsInChild1);
      GameObject gameObject1 = componentsInChild1.gameObject;
      DestroyImmediate(componentsInChild1);
      MeshToggleButtonStyler styler = gameObject1.AddComponent<MeshToggleButtonStyler>();
      foreach (Text componentsInChild2 in gameObject1.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild2.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild2.GetComponent<MeshTextStyler>())
        {
          GameObject gameObject2 = componentsInChild2.gameObject;
          DestroyImmediate(componentsInChild2);
          gameObject2.AddComponent<TextMeshProUGUI>();
        }
      }
      toggleButtonData.ApplyTo(component, styler);
    }
  }

  protected class ToggleButtonData
  {
    private Dictionary<string, MeshTextStyler.TextData> textDict;
    private string styleName;

    public ToggleButtonData(Toggle ToggleButton, ToggleButtonStyler styler)
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

    public void ApplyTo(Toggle ToggleButton, MeshToggleButtonStyler styler)
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
