// Decompiled with JetBrains decompiler
// Type: DebugQualityDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DebugQualityDropdown : MonoBehaviour
{
  public Dropdown dropdown;

  public void Awake()
  {
    List<SRQualitySettings.Level> levels = new List<SRQualitySettings.Level>()
    {
      SRQualitySettings.Level.LOWEST,
      SRQualitySettings.Level.LOW,
      SRQualitySettings.Level.DEFAULT,
      SRQualitySettings.Level.HIGH,
      SRQualitySettings.Level.VERY_HIGH
    };
    dropdown.ClearOptions();
    dropdown.AddOptions(levels.Select(level => Enum.GetName(typeof (SRQualitySettings.Level), level)).ToList());
    dropdown.onValueChanged.AddListener(index => SRQualitySettings.CurrentLevel = levels[index]);
    dropdown.value = 2;
  }
}
