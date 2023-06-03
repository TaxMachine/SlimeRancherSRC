// Decompiled with JetBrains decompiler
// Type: DebugUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DebugUI : SRBehaviour
{
  [Tooltip("Grid object to parent buttons to.")]
  public GameObject grid;
  [Tooltip("Debug button prefab.")]
  public GameObject buttonPrefab;
  [Tooltip("Tab button left/previous. (optional)")]
  public GameObject buttonTabLeft;
  [Tooltip("Tab button right/next. (optional)")]
  public GameObject buttonTabRight;
  [Tooltip("Number of buttons to display on each page.")]
  public int buttonsPerPage = 10;
}
