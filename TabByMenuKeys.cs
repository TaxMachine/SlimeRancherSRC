// Decompiled with JetBrains decompiler
// Type: TabByMenuKeys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (ToggleGroup))]
public class TabByMenuKeys : MonoBehaviour
{
  public static bool disabledForBinding;
  private Toggle[] tabs;
  private int currIdx;

  public void Awake() => tabs = GetComponentsInChildren<Toggle>(false);

  public void Start() => tabs = GetComponentsInChildren<Toggle>(false);

  public void Update()
  {
    if (disabledForBinding)
      return;
    if (SRInput.PauseActions.menuTabRight.WasPressed)
    {
      SelectNextTab();
    }
    else
    {
      if (!SRInput.PauseActions.menuTabLeft.WasPressed)
        return;
      SelectPrevTab();
    }
  }

  public void SelectNextTab()
  {
    currIdx = Math.Min(currIdx + 1, tabs.Length - 1);
    tabs[currIdx].isOn = true;
  }

  public void SelectPrevTab()
  {
    currIdx = Math.Max(currIdx - 1, 0);
    tabs[currIdx].isOn = true;
  }

  public void RecalcSelected()
  {
    if (tabs == null)
      return;
    for (int index = 0; index < tabs.Length; ++index)
    {
      if (tabs[index].isOn)
      {
        currIdx = index;
        break;
      }
    }
  }
}
