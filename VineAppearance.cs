// Decompiled with JetBrains decompiler
// Type: VineAppearance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Appearance Extras/Vine Appearance")]
[Serializable]
public class VineAppearance : ScriptableObject
{
  public GameObject vinePrefab;
  public GameObject vineEnterFx;
  public GameObject vineExitFx;
}
