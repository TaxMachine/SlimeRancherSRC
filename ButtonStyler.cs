// Decompiled with JetBrains decompiler
// Type: ButtonStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Button))]
public class ButtonStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.ButtonStyle))]
  public string styleName = "Default";
  private InputDirector inputDir;
  private UIStyleDirector styleDir;
  private Button button;

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
    UIStyleDirector.ButtonStyle buttonStyle = styleDir.GetButtonStyle(styleName);
    if (buttonStyle == null)
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
        if (buttonStyle.hideIfGamepad)
          inputDir.onKeysChanged += OnInputDeviceChanged;
        OnInputDeviceChanged();
      }
      List<Text> textList = new List<Text>();
      foreach (Text componentsInChild in GetComponentsInChildren<Text>())
      {
        if (!(bool) (Object) componentsInChild.GetComponent<TextStyler>())
          textList.Add(componentsInChild);
      }
      foreach (Text text in textList)
        TextStyler.ApplyTextStyle(text, buttonStyle);
      if (buttonStyle.bgSprite.apply)
        button.image.enabled = buttonStyle.bgSprite.value != null;
      if (buttonStyle.bgColor.apply)
        button.image.color = buttonStyle.bgColor.value;
      ColorBlock colors = button.colors;
      if (buttonStyle.normalTint.apply)
        colors.normalColor = buttonStyle.normalTint.value;
      if (buttonStyle.highlightedTint.apply)
        colors.highlightedColor = buttonStyle.highlightedTint.value;
      if (buttonStyle.pressedTint.apply)
        colors.pressedColor = buttonStyle.pressedTint.value;
      if (buttonStyle.disabledTint.apply)
        colors.disabledColor = buttonStyle.disabledTint.value;
      button.colors = colors;
      SpriteState spriteState = button.spriteState;
      if (buttonStyle.disabledSprite.apply)
        spriteState.disabledSprite = buttonStyle.disabledSprite.value;
      if (buttonStyle.highlightedSprite.apply)
        spriteState.highlightedSprite = buttonStyle.highlightedSprite.value;
      if (buttonStyle.pressedSprite.apply)
        spriteState.pressedSprite = buttonStyle.pressedSprite.value;
      button.spriteState = spriteState;
      if (buttonStyle.transition.apply)
        button.transition = buttonStyle.transition.value;
      if (buttonStyle.bgSprite.apply)
        button.image.sprite = buttonStyle.bgSprite.value;
      if (!Application.isPlaying || !buttonStyle.includeChild.apply || !(buttonStyle.includeChild.value != null))
        return;
      Transform transform = this.transform.Find(buttonStyle.includeChild.value.name);
      GameObject gameObject1 = transform == null ? null : transform.gameObject;
      if (gameObject1 != null)
        Destroyer.Destroy(gameObject1, "ButtonStyler.ApplyStyle");
      GameObject gameObject2 = Instantiate(buttonStyle.includeChild.value);
      gameObject2.transform.SetParent(this.transform, false);
      gameObject2.name = buttonStyle.includeChild.value.name;
    }
  }

  private void OnInputDeviceChanged() => gameObject.SetActive(!styleDir.GetButtonStyle(styleName).hideIfGamepad || !InputDirector.UsingGamepad());
}
