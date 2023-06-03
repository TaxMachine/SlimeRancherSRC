// Decompiled with JetBrains decompiler
// Type: SRToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SRToggle : Toggle
{
  protected override void OnEnable()
  {
    base.OnEnable();
    if (!(group != null))
      return;
    ((SRToggleGroup) group).OnToggleEnable(this);
  }

  protected override void OnDisable()
  {
    if (group != null)
      ((SRToggleGroup) group).OnToggleWillDisable(this);
    base.OnDisable();
  }

  protected override void OnDestroy()
  {
    if (group != null)
      ((SRToggleGroup) group).OnToggleEnable(this);
    base.OnDestroy();
  }
}
