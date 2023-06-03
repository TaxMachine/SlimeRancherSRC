// Decompiled with JetBrains decompiler
// Type: UIStyleDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIStyleDirector : SRBehaviour
{
  public TextStyle[] textStyles = new TextStyle[0];
  public MeshTextStyle[] meshTextStyles = new MeshTextStyle[0];
  public DropdownStyle[] dropdownStyles = new DropdownStyle[0];
  public MeshDropdownStyle[] meshDropdownStyles = new MeshDropdownStyle[0];
  public ButtonStyle[] buttonStyles = new ButtonStyle[0];
  public MeshButtonStyle[] meshButtonStyles = new MeshButtonStyle[0];
  public PanelStyle[] panelStyles = new PanelStyle[0];
  public IconStyle[] iconStyles = new IconStyle[0];
  public FieldStyle[] fieldStyles = new FieldStyle[0];
  public ScrollbarStyle[] scrollbarStyles = new ScrollbarStyle[0];
  public CheckboxStyle[] checkboxStyles = new CheckboxStyle[0];
  public MeshCheckboxStyle[] meshCheckboxStyles = new MeshCheckboxStyle[0];
  public ToggleButtonStyle[] toggleButtonStyles = new ToggleButtonStyle[0];
  public MeshToggleButtonStyle[] meshToggleButtonStyles = new MeshToggleButtonStyle[0];
  public SliderStyle[] sliderStyles = new SliderStyle[0];
  private Dictionary<string, TextStyle> textDict = new Dictionary<string, TextStyle>();
  private string[] textStyleNames = new string[0];
  private Dictionary<string, MeshTextStyle> meshTextDict = new Dictionary<string, MeshTextStyle>();
  private string[] meshTextStyleNames = new string[0];
  private Dictionary<string, ButtonStyle> buttonDict = new Dictionary<string, ButtonStyle>();
  private string[] buttonStyleNames = new string[0];
  private Dictionary<string, MeshButtonStyle> meshButtonDict = new Dictionary<string, MeshButtonStyle>();
  private string[] meshButtonStyleNames = new string[0];
  private Dictionary<string, DropdownStyle> dropdownDict = new Dictionary<string, DropdownStyle>();
  private string[] dropdownStyleNames = new string[0];
  private Dictionary<string, MeshDropdownStyle> meshDropdownDict = new Dictionary<string, MeshDropdownStyle>();
  private string[] meshDropdownStyleNames = new string[0];
  private Dictionary<string, PanelStyle> panelDict = new Dictionary<string, PanelStyle>();
  private string[] panelStyleNames = new string[0];
  private Dictionary<string, IconStyle> iconDict = new Dictionary<string, IconStyle>();
  private string[] iconStyleNames = new string[0];
  private Dictionary<string, FieldStyle> fieldDict = new Dictionary<string, FieldStyle>();
  private string[] fieldStyleNames = new string[0];
  private Dictionary<string, ScrollbarStyle> scrollbarDict = new Dictionary<string, ScrollbarStyle>();
  private string[] scrollbarStyleNames = new string[0];
  private Dictionary<string, CheckboxStyle> checkboxDict = new Dictionary<string, CheckboxStyle>();
  private string[] checkboxStyleNames = new string[0];
  private Dictionary<string, MeshCheckboxStyle> meshCheckboxDict = new Dictionary<string, MeshCheckboxStyle>();
  private string[] meshCheckboxStyleNames = new string[0];
  private Dictionary<string, ToggleButtonStyle> toggleButtonDict = new Dictionary<string, ToggleButtonStyle>();
  private string[] toggleButtonStyleNames = new string[0];
  private Dictionary<string, MeshToggleButtonStyle> meshToggleButtonDict = new Dictionary<string, MeshToggleButtonStyle>();
  private string[] meshToggleButtonStyleNames = new string[0];
  private Dictionary<string, SliderStyle> sliderDict = new Dictionary<string, SliderStyle>();
  private string[] sliderStyleNames = new string[0];
  public TMP_FontAsset defaultMeshFont;
  private static UIStyleDirector instance;

  private MeshTextStyle ConvertToMesh(TextStyle style)
  {
    MeshTextStyle mesh = new MeshTextStyle();
    mesh.Convert(this, style);
    return mesh;
  }

  private MeshButtonStyle ConvertToMesh(ButtonStyle style)
  {
    MeshButtonStyle mesh = new MeshButtonStyle();
    mesh.Convert(this, style);
    return mesh;
  }

  private MeshDropdownStyle ConvertToMesh(DropdownStyle style)
  {
    MeshDropdownStyle mesh = new MeshDropdownStyle();
    mesh.Convert(this, style);
    return mesh;
  }

  private MeshCheckboxStyle ConvertToMesh(CheckboxStyle style)
  {
    MeshCheckboxStyle mesh = new MeshCheckboxStyle();
    mesh.Convert(this, style);
    return mesh;
  }

  private MeshToggleButtonStyle ConvertToMesh(
    ToggleButtonStyle style)
  {
    MeshToggleButtonStyle mesh = new MeshToggleButtonStyle();
    mesh.Convert(this, style);
    return mesh;
  }

  public void OnEnable()
  {
    List<string> stringList1 = new List<string>();
    foreach (TextStyle textStyle in textStyles)
    {
      textDict[textStyle.name] = textStyle;
      stringList1.Add(textStyle.name);
    }
    textStyleNames = stringList1.ToArray();
    List<string> stringList2 = new List<string>();
    foreach (MeshTextStyle meshTextStyle in meshTextStyles)
    {
      meshTextDict[meshTextStyle.name] = meshTextStyle;
      stringList2.Add(meshTextStyle.name);
    }
    meshTextStyleNames = stringList2.ToArray();
    List<string> stringList3 = new List<string>();
    foreach (ButtonStyle buttonStyle in buttonStyles)
    {
      buttonDict[buttonStyle.name] = buttonStyle;
      stringList3.Add(buttonStyle.name);
    }
    buttonStyleNames = stringList3.ToArray();
    List<string> stringList4 = new List<string>();
    foreach (MeshButtonStyle meshButtonStyle in meshButtonStyles)
    {
      meshButtonDict[meshButtonStyle.name] = meshButtonStyle;
      stringList4.Add(meshButtonStyle.name);
    }
    meshButtonStyleNames = stringList4.ToArray();
    List<string> stringList5 = new List<string>();
    foreach (DropdownStyle dropdownStyle in dropdownStyles)
    {
      dropdownDict[dropdownStyle.name] = dropdownStyle;
      stringList5.Add(dropdownStyle.name);
    }
    dropdownStyleNames = stringList5.ToArray();
    List<string> stringList6 = new List<string>();
    foreach (MeshDropdownStyle meshDropdownStyle in meshDropdownStyles)
    {
      meshDropdownDict[meshDropdownStyle.name] = meshDropdownStyle;
      stringList6.Add(meshDropdownStyle.name);
    }
    meshDropdownStyleNames = stringList6.ToArray();
    List<string> stringList7 = new List<string>();
    foreach (PanelStyle panelStyle in panelStyles)
    {
      panelDict[panelStyle.name] = panelStyle;
      stringList7.Add(panelStyle.name);
    }
    panelStyleNames = stringList7.ToArray();
    List<string> stringList8 = new List<string>();
    foreach (IconStyle iconStyle in iconStyles)
    {
      iconDict[iconStyle.name] = iconStyle;
      stringList8.Add(iconStyle.name);
    }
    iconStyleNames = stringList8.ToArray();
    List<string> stringList9 = new List<string>();
    foreach (FieldStyle fieldStyle in fieldStyles)
    {
      fieldDict[fieldStyle.name] = fieldStyle;
      stringList9.Add(fieldStyle.name);
    }
    fieldStyleNames = stringList9.ToArray();
    List<string> stringList10 = new List<string>();
    foreach (ScrollbarStyle scrollbarStyle in scrollbarStyles)
    {
      scrollbarDict[scrollbarStyle.name] = scrollbarStyle;
      stringList10.Add(scrollbarStyle.name);
    }
    scrollbarStyleNames = stringList10.ToArray();
    List<string> stringList11 = new List<string>();
    foreach (CheckboxStyle checkboxStyle in checkboxStyles)
    {
      checkboxDict[checkboxStyle.name] = checkboxStyle;
      stringList11.Add(checkboxStyle.name);
    }
    checkboxStyleNames = stringList11.ToArray();
    List<string> stringList12 = new List<string>();
    foreach (MeshCheckboxStyle meshCheckboxStyle in meshCheckboxStyles)
    {
      meshCheckboxDict[meshCheckboxStyle.name] = meshCheckboxStyle;
      stringList12.Add(meshCheckboxStyle.name);
    }
    meshCheckboxStyleNames = stringList12.ToArray();
    List<string> stringList13 = new List<string>();
    foreach (ToggleButtonStyle toggleButtonStyle in toggleButtonStyles)
    {
      toggleButtonDict[toggleButtonStyle.name] = toggleButtonStyle;
      stringList13.Add(toggleButtonStyle.name);
    }
    toggleButtonStyleNames = stringList13.ToArray();
    List<string> stringList14 = new List<string>();
    foreach (MeshToggleButtonStyle toggleButtonStyle in meshToggleButtonStyles)
    {
      meshToggleButtonDict[toggleButtonStyle.name] = toggleButtonStyle;
      stringList14.Add(toggleButtonStyle.name);
    }
    meshToggleButtonStyleNames = stringList14.ToArray();
    List<string> stringList15 = new List<string>();
    foreach (SliderStyle sliderStyle in sliderStyles)
    {
      sliderDict[sliderStyle.name] = sliderStyle;
      stringList15.Add(sliderStyle.name);
    }
    sliderStyleNames = stringList15.ToArray();
  }

  public TextStyle GetTextStyle(string name) => textDict.Get(name);

  public string[] GetTextStyles() => textStyleNames;

  public MeshTextStyle GetMeshTextStyle(string name) => meshTextDict.Get(name);

  public string[] GetMeshTextStyles() => meshTextStyleNames;

  public ButtonStyle GetButtonStyle(string name) => buttonDict.Get(name);

  public string[] GetButtonStyles() => buttonStyleNames;

  public MeshButtonStyle GetMeshButtonStyle(string name) => meshButtonDict.Get(name);

  public string[] GetMeshButtonStyles() => meshButtonStyleNames;

  public DropdownStyle GetDropdownStyle(string name) => dropdownDict.Get(name);

  public string[] GetDropdownStyles() => dropdownStyleNames;

  public MeshDropdownStyle GetMeshDropdownStyle(string name) => meshDropdownDict.Get(name);

  public string[] GetMeshDropdownStyles() => meshDropdownStyleNames;

  public PanelStyle GetPanelStyle(string name) => panelDict.Get(name);

  public string[] GetPanelStyles() => panelStyleNames;

  public IconStyle GetIconStyle(string name) => iconDict.Get(name);

  public string[] GetIconStyles() => iconStyleNames;

  public FieldStyle GetFieldStyle(string name) => fieldDict.Get(name);

  public string[] GetFieldStyles() => fieldStyleNames;

  public ScrollbarStyle GetScrollbarStyle(string name) => scrollbarDict.Get(name);

  public string[] GetScrollbarStyles() => scrollbarStyleNames;

  public CheckboxStyle GetCheckboxStyle(string name) => checkboxDict.Get(name);

  public string[] GetCheckboxStyles() => checkboxStyleNames;

  public MeshCheckboxStyle GetMeshCheckboxStyle(string name) => meshCheckboxDict.Get(name);

  public string[] GetMeshCheckboxStyles() => meshCheckboxStyleNames;

  public ToggleButtonStyle GetToggleButtonStyle(string name) => toggleButtonDict.Get(name);

  public string[] GetToggleButtonStyles() => toggleButtonStyleNames;

  public MeshToggleButtonStyle GetMeshToggleButtonStyle(string name) => meshToggleButtonDict.Get(name);

  public string[] GetMeshToggleButtonStyles() => meshToggleButtonStyleNames;

  public SliderStyle GetSliderStyle(string name) => sliderDict.Get(name);

  public string[] GetSliderStyles() => sliderStyleNames;

  public string[] GetStyleNames(Type type)
  {
    if (type == typeof (ButtonStyle))
      return GetButtonStyles();
    if (type == typeof (MeshButtonStyle))
      return GetMeshButtonStyles();
    if (type == typeof (DropdownStyle))
      return GetDropdownStyles();
    if (type == typeof (MeshDropdownStyle))
      return GetMeshDropdownStyles();
    if (type == typeof (TextStyle))
      return GetTextStyles();
    if (type == typeof (MeshTextStyle))
      return GetMeshTextStyles();
    if (type == typeof (PanelStyle))
      return GetPanelStyles();
    if (type == typeof (IconStyle))
      return GetIconStyles();
    if (type == typeof (FieldStyle))
      return GetFieldStyles();
    if (type == typeof (ScrollbarStyle))
      return GetScrollbarStyles();
    if (type == typeof (CheckboxStyle))
      return GetCheckboxStyles();
    if (type == typeof (MeshCheckboxStyle))
      return GetMeshCheckboxStyles();
    if (type == typeof (ToggleButtonStyle))
      return GetToggleButtonStyles();
    if (type == typeof (MeshToggleButtonStyle))
      return GetMeshToggleButtonStyles();
    if (type == typeof (SliderStyle))
      return GetSliderStyles();
    throw new Exception("Invalid type provided to style");
  }

  public static UIStyleDirector Instance
  {
    get
    {
      if (instance == null)
        instance = CreateInstance();
      return instance;
    }
  }

  private static UIStyleDirector CreateInstance() => Instantiate(Resources.Load(nameof (UIStyleDirector)) as GameObject).GetComponent<UIStyleDirector>();

  public class Setting
  {
  }

  [Serializable]
  public class TransitionSetting : Setting
  {
    public bool apply;
    public Selectable.Transition value;
  }

  [Serializable]
  public class ColorSetting : Setting
  {
    public bool apply;
    public Color value;
  }

  [Serializable]
  public class SpriteSetting : Setting
  {
    public bool apply;
    public Sprite value;
  }

  [Serializable]
  public class GameObjSetting : Setting
  {
    public bool apply;
    public GameObject value;
  }

  [Serializable]
  public class FontSetting : Setting
  {
    public bool apply;
    public Font value;
  }

  [Serializable]
  public class FontStyleSetting : Setting
  {
    public bool apply;
    public FontStyle value;
  }

  [Serializable]
  public class MeshFontSetting : Setting
  {
    public bool apply;
    public TMP_FontAsset value;
  }

  [Serializable]
  public class IntSetting : Setting
  {
    public bool apply;
    public int value;
  }

  [Serializable]
  public class BoolSetting : Setting
  {
    public bool apply;
    public bool value;
  }

  [Serializable]
  public class FloatSetting : Setting
  {
    public bool apply;
    public float value;
  }

  [Serializable]
  public class MaterialPresetSetting : Setting
  {
    public bool apply;
    public Material value;
  }

  [Serializable]
  public class MeshTextStyle
  {
    public string name;
    public ColorSetting textColor;
    public MeshFontSetting font;
    public MaterialPresetSetting materialPreset;
    public BoolSetting bold;
    public BoolSetting italic;
    public IntSetting fontSize;

    internal void Convert(UIStyleDirector styleDir, TextStyle oldStyle)
    {
      bold = new BoolSetting();
      bold.value = oldStyle.fontStyle.value == FontStyle.Bold || oldStyle.fontStyle.value == FontStyle.BoldAndItalic;
      bold.apply = oldStyle.fontStyle.apply;
      italic = new BoolSetting();
      italic.value = oldStyle.fontStyle.value == FontStyle.Italic || oldStyle.fontStyle.value == FontStyle.BoldAndItalic;
      italic.apply = oldStyle.fontStyle.apply;
      font = new MeshFontSetting();
      font.value = styleDir.defaultMeshFont;
      font.apply = oldStyle.font.apply;
      fontSize = new IntSetting();
      fontSize.value = oldStyle.fontSize.value;
      fontSize.apply = oldStyle.fontSize.apply;
      textColor = new ColorSetting();
      textColor.value = oldStyle.textColor.value;
      textColor.apply = oldStyle.textColor.apply;
      materialPreset = null;
      name = oldStyle.name;
    }
  }

  [Serializable]
  public class TextStyle
  {
    public string name;
    public ColorSetting textColor;
    public FontSetting font;
    public FontStyleSetting fontStyle;
    public IntSetting fontSize;
    public ColorSetting outlineColor;
    public FloatSetting outlineWidth;
  }

  [Serializable]
  public class MeshButtonStyle : MeshTextStyle
  {
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
    public GameObjSetting includeChild;
    public bool hideIfGamepad;
    public TransitionSetting transition;
    public SpriteSetting disabledSprite;
    public SpriteSetting highlightedSprite;
    public SpriteSetting pressedSprite;

    public void Convert(UIStyleDirector styleDir, ButtonStyle buttonStyle)
    {
      Convert(styleDir, (TextStyle) buttonStyle);
      bgSprite = buttonStyle.bgSprite;
      bgColor = buttonStyle.bgColor;
      normalTint = buttonStyle.normalTint;
      highlightedTint = buttonStyle.highlightedTint;
      pressedTint = buttonStyle.pressedTint;
      disabledTint = buttonStyle.disabledTint;
      includeChild = buttonStyle.includeChild;
      hideIfGamepad = buttonStyle.hideIfGamepad;
      transition = buttonStyle.transition;
      disabledSprite = buttonStyle.disabledSprite;
      highlightedSprite = buttonStyle.highlightedSprite;
      pressedSprite = buttonStyle.pressedSprite;
    }
  }

  [Serializable]
  public class ButtonStyle : TextStyle
  {
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
    public GameObjSetting includeChild;
    public bool hideIfGamepad;
    public TransitionSetting transition;
    public SpriteSetting disabledSprite;
    public SpriteSetting highlightedSprite;
    public SpriteSetting pressedSprite;
  }

  [Serializable]
  public class MeshDropdownStyle : MeshTextStyle
  {
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
    public SpriteSetting menuBgSprite;
    public ColorSetting menuBgColor;
    public SpriteSetting itemBgSprite;
    public ColorSetting itemBgColor;
    public ColorSetting itemNormalTint;
    public ColorSetting itemHighlightedTint;
    public ColorSetting itemPressedTint;
    public ColorSetting itemDisabledTint;

    public void Convert(UIStyleDirector styleDir, DropdownStyle dropdownStyle)
    {
      Convert(styleDir, (TextStyle) dropdownStyle);
      bgSprite = dropdownStyle.bgSprite;
      bgColor = dropdownStyle.bgColor;
      normalTint = dropdownStyle.normalTint;
      highlightedTint = dropdownStyle.highlightedTint;
      pressedTint = dropdownStyle.pressedTint;
      disabledTint = dropdownStyle.disabledTint;
      menuBgSprite = dropdownStyle.menuBgSprite;
      menuBgColor = dropdownStyle.menuBgColor;
      itemBgSprite = dropdownStyle.itemBgSprite;
      itemBgColor = dropdownStyle.itemBgColor;
      itemNormalTint = dropdownStyle.itemNormalTint;
      itemHighlightedTint = dropdownStyle.itemHighlightedTint;
      itemPressedTint = dropdownStyle.itemPressedTint;
      itemDisabledTint = dropdownStyle.itemDisabledTint;
    }
  }

  [Serializable]
  public class DropdownStyle : TextStyle
  {
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
    public SpriteSetting menuBgSprite;
    public ColorSetting menuBgColor;
    public SpriteSetting itemBgSprite;
    public ColorSetting itemBgColor;
    public ColorSetting itemNormalTint;
    public ColorSetting itemHighlightedTint;
    public ColorSetting itemPressedTint;
    public ColorSetting itemDisabledTint;
  }

  [Serializable]
  public class PanelStyle
  {
    public string name;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
  }

  [Serializable]
  public class IconStyle
  {
    public string name;
    public SpriteSetting sprite;
    public ColorSetting color;
  }

  [Serializable]
  public class FieldStyle : TextStyle
  {
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
    public ColorSetting placeholderTextColor;
    public FontSetting placeholderFont;
    public FontStyleSetting placeholderFontStyle;
    public IntSetting placeholderFontSize;
    public ColorSetting placeholderOutlineColor;
    public FloatSetting placeholderOutlineWidth;
    public ColorSetting selectionColor;
  }

  [Serializable]
  public class ScrollbarStyle
  {
    public string name;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public SpriteSetting handleSprite;
    public ColorSetting handleColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
  }

  [Serializable]
  public class MeshCheckboxStyle : MeshTextStyle
  {
    public SpriteSetting markSprite;
    public ColorSetting markColor;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;

    public void Convert(UIStyleDirector styleDir, CheckboxStyle checkboxStyle)
    {
      Convert(styleDir, (TextStyle) checkboxStyle);
      markSprite = checkboxStyle.markSprite;
      markColor = checkboxStyle.markColor;
      bgSprite = checkboxStyle.bgSprite;
      bgColor = checkboxStyle.bgColor;
      normalTint = checkboxStyle.normalTint;
      highlightedTint = checkboxStyle.highlightedTint;
      pressedTint = checkboxStyle.pressedTint;
      disabledTint = checkboxStyle.disabledTint;
    }
  }

  [Serializable]
  public class CheckboxStyle : TextStyle
  {
    public SpriteSetting markSprite;
    public ColorSetting markColor;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
  }

  [Serializable]
  public class MeshToggleButtonStyle : MeshTextStyle
  {
    public SpriteSetting selectedSprite;
    public ColorSetting selectedColor;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;

    public void Convert(
      UIStyleDirector styleDir,
      ToggleButtonStyle toggleButtonStyle)
    {
      Convert(styleDir, (TextStyle) toggleButtonStyle);
      selectedSprite = toggleButtonStyle.selectedSprite;
      selectedColor = toggleButtonStyle.selectedColor;
      bgSprite = toggleButtonStyle.bgSprite;
      bgColor = toggleButtonStyle.bgColor;
      normalTint = toggleButtonStyle.normalTint;
      highlightedTint = toggleButtonStyle.highlightedTint;
      pressedTint = toggleButtonStyle.pressedTint;
      disabledTint = toggleButtonStyle.disabledTint;
    }
  }

  [Serializable]
  public class ToggleButtonStyle : TextStyle
  {
    public SpriteSetting selectedSprite;
    public ColorSetting selectedColor;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
  }

  [Serializable]
  public class SliderStyle
  {
    public string name;
    public SpriteSetting bgSprite;
    public ColorSetting bgColor;
    public SpriteSetting fillSprite;
    public ColorSetting fillColor;
    public SpriteSetting handleSprite;
    public ColorSetting handleColor;
    public ColorSetting normalTint;
    public ColorSetting highlightedTint;
    public ColorSetting pressedTint;
    public ColorSetting disabledTint;
  }
}
