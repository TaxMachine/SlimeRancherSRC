// Decompiled with JetBrains decompiler
// Type: IconStyler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof (Image))]
public class IconStyler : SRBehaviour
{
  [StyleName(typeof (UIStyleDirector.IconStyle))]
  public string styleName = "Default";
  private UIStyleDirector styleDir;
  private Image img;

  public void OnEnable()
  {
    styleDir = UIStyleDirector.Instance;
    img = GetComponent<Image>();
    ApplyStyle();
  }

  private void ApplyStyle()
  {
    UIStyleDirector.IconStyle iconStyle = styleDir.GetIconStyle(styleName);
    if (iconStyle == null)
    {
      if (!Application.isPlaying)
        return;
      Log.Warning("Unknown icon style: " + styleName);
    }
    else
    {
      if (iconStyle.color.apply)
        img.color = iconStyle.color.value;
      if (!iconStyle.sprite.apply)
        return;
      img.sprite = iconStyle.sprite.value;
    }
  }
}
