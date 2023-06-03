// Decompiled with JetBrains decompiler
// Type: SRToggleGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SRToggleGroup : ToggleGroup
{
  private SRToggle slime1992_previousEnabled;
  private bool slime1992_requiresHotfix;

  protected override void Awake()
  {
    base.Awake();
    slime1992_requiresHotfix = !allowSwitchOff;
  }

  public void OnToggleEnable(SRToggle instance)
  {
    if (!slime1992_requiresHotfix || !(instance == slime1992_previousEnabled))
      return;
    slime1992_previousEnabled.SetIsOnWithoutNotify(true);
    slime1992_previousEnabled = null;
    allowSwitchOff = false;
  }

  public void OnToggleWillDisable(SRToggle instance)
  {
    if (!slime1992_requiresHotfix || !(instance != null) || !instance.isOn)
      return;
    allowSwitchOff = true;
    slime1992_previousEnabled = instance;
    slime1992_previousEnabled.SetIsOnWithoutNotify(false);
  }
}
