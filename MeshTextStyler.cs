// Decompiled with JetBrains decompiler
// Type: MeshTextStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (TMP_Text))]
public class MeshTextStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.MeshTextStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private TMP_Text text;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    text = GetComponent<TMP_Text>();
    ApplyStyle();
  }

  public void SetStyle(string styleName)
  {
    this.styleName = styleName;
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.MeshTextStyle meshTextStyle = styleDir.GetMeshTextStyle(styleName);
    if (meshTextStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown text style: " + styleName + " in: " + gameObject.name);
    }
    else
      ApplyTextStyle(text, meshTextStyle);
  }

  public static void ApplyTextStyle(TMP_Text text, UIStyleDirector.MeshTextStyle style)
  {
    if (style.textColor.apply)
      text.color = style.textColor.value;
    if (style.font.apply)
      text.font = style.font.value;
    if (style.fontSize.apply)
      text.fontSize = style.fontSize.value;
    if (style.italic.apply)
    {
      if (style.italic.value)
        text.fontStyle |= FontStyles.Italic;
      else
        text.fontStyle &= ~FontStyles.Italic;
    }
    if (style.bold.apply)
    {
      if (style.bold.value)
        text.fontStyle |= FontStyles.Bold;
      else
        text.fontStyle &= ~FontStyles.Bold;
    }
    if (!style.materialPreset.apply || !(style.materialPreset.value != null))
      return;
    text.fontMaterial = style.materialPreset.value;
  }

  protected static TextAlignmentOptions Convert(TextAnchor align)
  {
    switch (align)
    {
      case TextAnchor.UpperLeft:
        return TextAlignmentOptions.TopLeft;
      case TextAnchor.UpperCenter:
        return TextAlignmentOptions.Top;
      case TextAnchor.UpperRight:
        return TextAlignmentOptions.TopRight;
      case TextAnchor.MiddleLeft:
        return TextAlignmentOptions.Left;
      case TextAnchor.MiddleCenter:
        return TextAlignmentOptions.Center;
      case TextAnchor.MiddleRight:
        return TextAlignmentOptions.Right;
      case TextAnchor.LowerLeft:
        return TextAlignmentOptions.BottomLeft;
      case TextAnchor.LowerCenter:
        return TextAlignmentOptions.Bottom;
      case TextAnchor.LowerRight:
        return TextAlignmentOptions.BottomRight;
      default:
        return TextAlignmentOptions.Center;
    }
  }

  public static void Convert(GameObject obj)
  {
    foreach (TextStyler componentsInChild in obj.GetComponentsInChildren<TextStyler>(true))
      Convert(componentsInChild);
  }

  public static void Convert(TextStyler styler)
  {
    Text component = styler.GetComponent<Text>();
    TextData textData = new TextData(component, styler);
    GameObject gameObject = styler.gameObject;
    DestroyImmediate(styler);
    DestroyImmediate(component);
    TextMeshProUGUI textMeshProUgui = gameObject.AddComponent<TextMeshProUGUI>();
    MeshTextStyler meshTextStyler = gameObject.AddComponent<MeshTextStyler>();
    TextMeshProUGUI textComp = textMeshProUgui;
    MeshTextStyler styler1 = meshTextStyler;
    textData.ApplyTo(textComp, styler1);
  }

  public class TextData
  {
    private string styleName;
    private string text;
    private bool bestFit;
    private int minSize;
    private int maxSize;
    private TextAnchor align;

    public TextData(Text textComp, TextStyler styler)
    {
      text = textComp.text;
      align = textComp.alignment;
      bestFit = textComp.resizeTextForBestFit;
      if (styler != null)
        styleName = styler.styleName;
      minSize = textComp.resizeTextMinSize;
      maxSize = textComp.resizeTextMaxSize;
    }

    public virtual void ApplyTo(TextMeshProUGUI textComp, MeshTextStyler styler)
    {
      textComp.text = text;
      if (styleName != null)
        styler.styleName = styleName;
      textComp.fontSizeMin = minSize;
      textComp.fontSizeMax = maxSize;
      textComp.alignment = Convert(align);
      textComp.enableAutoSizing = bestFit;
    }
  }
}
