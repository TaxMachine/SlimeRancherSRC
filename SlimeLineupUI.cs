// Decompiled with JetBrains decompiler
// Type: SlimeLineupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlimeLineupUI : MonoBehaviour
{
  public Dropdown dropdown;
  public SlimeLineup slimeLineup;
  public Button showSlimeButton;
  public Button showSlimesAndLargosButton;
  private SlimeDefinition[] baseSlimeTypes;
  private int selectedIndex;

  private void Start()
  {
    baseSlimeTypes = slimeLineup.slimeDefinitions.Slimes.Where(slime => !slime.IsLargo).ToArray();
    dropdown.AddOptions(baseSlimeTypes.Select(slime => slime.Name).ToList());
    dropdown.onValueChanged.AddListener(OnSlimeTypeSelected);
    showSlimeButton.onClick.AddListener(ShowSelectedSlime);
    showSlimesAndLargosButton.onClick.AddListener(ShowSelectedSlimeAndLargos);
  }

  public void OnSlimeTypeSelected(int index) => selectedIndex = index;

  public void ShowSelectedSlime() => slimeLineup.ShowSlime(baseSlimeTypes[selectedIndex]);

  public void ShowSelectedSlimeAndLargos() => slimeLineup.ShowSlimeAndLargos(baseSlimeTypes[selectedIndex]);
}
