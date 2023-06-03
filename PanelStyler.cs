// Decompiled with JetBrains decompiler
// Type: PanelStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Image))]
public class PanelStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.PanelStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Image bg;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    bg = GetComponent<Image>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.PanelStyle panelStyle = styleDir.GetPanelStyle(styleName);
    if (panelStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown panel style: " + styleName);
    }
    else
    {
      if (panelStyle.bgSprite.apply)
        bg.enabled = panelStyle.bgSprite != null;
      if (panelStyle.bgColor.apply)
        bg.color = panelStyle.bgColor.value;
      if (!panelStyle.bgSprite.apply)
        return;
      bg.sprite = panelStyle.bgSprite.value;
    }
  }
}
