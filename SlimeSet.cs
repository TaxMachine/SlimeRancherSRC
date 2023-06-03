// Decompiled with JetBrains decompiler
// Type: SlimeSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class SlimeSet
{
  public Member[] members;

  [Serializable]
  public class Member
  {
    public GameObject prefab;
    public float weight = 1f;
  }
}
