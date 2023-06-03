// Decompiled with JetBrains decompiler
// Type: MeshButtonStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Button))]
public class MeshButtonStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.MeshButtonStyle))]
  public string styleName = "Default";
  private InputDirector inputDir;
  private UIStyleDirector styleDir;
  private Button button;
  private GameObject includeChildInstance;

  public void Awake()
  {
    if (!Application.isPlaying)
      return;
    inputDir = SRSingleton<GameContext>.Instance.InputDirector;
  }

  public void OnDestroy()
  {
    if (!Application.isPlaying)
      return;
    inputDir.onKeysChanged -= OnInputDeviceChanged;
  }

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    button = GetComponent<Button>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.MeshButtonStyle meshButtonStyle = styleDir.GetMeshButtonStyle(styleName);
    if (meshButtonStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown button style: " + styleName);
    }
    else
    {
      if (Application.isPlaying)
      {
        inputDir.onKeysChanged -= OnInputDeviceChanged;
        if (meshButtonStyle.hideIfGamepad)
          inputDir.onKeysChanged += OnInputDeviceChanged;
        OnInputDeviceChanged();
      }
      List<TMP_Text> tmpTextList = new List<TMP_Text>();
      foreach (TMP_Text componentsInChild in GetComponentsInChildren<TMP_Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<MeshTextStyler>())
          tmpTextList.Add(componentsInChild);
      }
      foreach (TMP_Text text in tmpTextList)
        MeshTextStyler.ApplyTextStyle(text, meshButtonStyle);
      if (meshButtonStyle.bgSprite.apply)
        button.image.enabled = meshButtonStyle.bgSprite.value != null;
      if (meshButtonStyle.bgColor.apply)
        button.image.color = meshButtonStyle.bgColor.value;
      ColorBlock colors = button.colors;
      if (meshButtonStyle.normalTint.apply)
        colors.normalColor = meshButtonStyle.normalTint.value;
      if (meshButtonStyle.highlightedTint.apply)
        colors.highlightedColor = meshButtonStyle.highlightedTint.value;
      if (meshButtonStyle.pressedTint.apply)
        colors.pressedColor = meshButtonStyle.pressedTint.value;
      if (meshButtonStyle.disabledTint.apply)
        colors.disabledColor = meshButtonStyle.disabledTint.value;
      button.colors = colors;
      SpriteState spriteState = button.spriteState;
      if (meshButtonStyle.disabledSprite.apply)
        spriteState.disabledSprite = meshButtonStyle.disabledSprite.value;
      if (meshButtonStyle.highlightedSprite.apply)
        spriteState.highlightedSprite = meshButtonStyle.highlightedSprite.value;
      if (meshButtonStyle.pressedSprite.apply)
        spriteState.pressedSprite = meshButtonStyle.pressedSprite.value;
      button.spriteState = spriteState;
      if (meshButtonStyle.transition.apply)
        button.transition = meshButtonStyle.transition.value;
      if (meshButtonStyle.bgSprite.apply)
        button.image.sprite = meshButtonStyle.bgSprite.value;
      if (!Application.isPlaying || !meshButtonStyle.includeChild.apply || !(meshButtonStyle.includeChild.value != null))
        return;
      if (includeChildInstance != null)
      {
        Destroyer.Destroy(includeChildInstance, "MeshButtonStyler.ApplyStyle");
        includeChildInstance = null;
      }
      includeChildInstance = Instantiate(meshButtonStyle.includeChild.value);
      includeChildInstance.transform.SetParent(transform, false);
      includeChildInstance.name = meshButtonStyle.includeChild.value.name;
    }
  }

  private void OnInputDeviceChanged() => gameObject.SetActive(!styleDir.GetMeshButtonStyle(styleName).hideIfGamepad || !InputDirector.UsingGamepad());

  public static void Convert(GameObject obj)
  {
    foreach (ButtonStyler componentsInChild1 in obj.GetComponentsInChildren<ButtonStyler>(true))
    {
      Button component = componentsInChild1.GetComponent<Button>();
      ButtonData buttonData = new ButtonData(component, componentsInChild1);
      GameObject gameObject1 = componentsInChild1.gameObject;
      DestroyImmediate(componentsInChild1);
      MeshButtonStyler styler = gameObject1.AddComponent<MeshButtonStyler>();
      foreach (Text componentsInChild2 in gameObject1.GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild2.GetComponent<TextStyler>() && !(bool) (Object) componentsInChild2.GetComponent<MeshTextStyler>())
        {
          GameObject gameObject2 = componentsInChild2.gameObject;
          DestroyImmediate(componentsInChild2);
          gameObject2.AddComponent<TextMeshProUGUI>();
        }
      }
      buttonData.ApplyTo(component, styler);
    }
  }

  protected class ButtonData
  {
    private Dictionary<string, MeshTextStyler.TextData> textDict;
    private string styleName;

    public ButtonData(Button button, ButtonStyler styler)
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

    public void ApplyTo(Button button, MeshButtonStyler styler)
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
