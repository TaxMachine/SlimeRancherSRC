// Decompiled with JetBrains decompiler
// Type: ScrollbarStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Scrollbar))]
public class ScrollbarStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.ScrollbarStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Scrollbar scrollbar;
  private Image bg;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    scrollbar = GetComponent<Scrollbar>();
    bg = GetComponent<Image>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.ScrollbarStyle scrollbarStyle = styleDir.GetScrollbarStyle(styleName);
    if (scrollbarStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown panel style: " + styleName);
    }
    else
    {
      if (scrollbarStyle.bgSprite.apply && bg != null)
        bg.enabled = scrollbarStyle.bgSprite != null;
      if (scrollbarStyle.bgColor.apply && bg != null)
        bg.color = scrollbarStyle.bgColor.value;
      if (scrollbarStyle.bgSprite.apply && bg != null)
        bg.sprite = scrollbarStyle.bgSprite.value;
      if (scrollbarStyle.handleColor.apply && scrollbar.targetGraphic != null)
        scrollbar.targetGraphic.color = scrollbarStyle.handleColor.value;
      if (scrollbarStyle.handleSprite.apply && scrollbar.targetGraphic is Image)
        ((Image) scrollbar.targetGraphic).sprite = scrollbarStyle.handleSprite.value;
      ColorBlock colors = scrollbar.colors;
      if (scrollbarStyle.normalTint.apply)
        colors.normalColor = scrollbarStyle.normalTint.value;
      if (scrollbarStyle.highlightedTint.apply)
        colors.highlightedColor = scrollbarStyle.highlightedTint.value;
      if (scrollbarStyle.pressedTint.apply)
        colors.pressedColor = scrollbarStyle.pressedTint.value;
      if (scrollbarStyle.disabledTint.apply)
        colors.disabledColor = scrollbarStyle.disabledTint.value;
      scrollbar.colors = colors;
    }
  }
}
