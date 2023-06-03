// Decompiled with JetBrains decompiler
// Type: SECTR_ULong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class SECTR_ULong
{
  [SerializeField]
  private int first;
  [SerializeField]
  private int second;

  public ulong value
  {
    get => (ulong) second << 32 | (ulong) first;
    set
    {
      first = (int) ((long) value & uint.MaxValue);
      second = (int) (value >> 32);
    }
  }

  public SECTR_ULong(ulong newValue) => value = newValue;

  public SECTR_ULong() => value = 0UL;

  public override string ToString() => string.Format("[ULong: value={0}, firstHalf={1}, secondHalf={2}]", value, first, second);

  public static bool operator >(SECTR_ULong a, ulong b) => a.value > b;

  public static bool operator >(ulong a, SECTR_ULong b) => a > b.value;

  public static bool operator <(SECTR_ULong a, ulong b) => a.value < b;

  public static bool operator <(ulong a, SECTR_ULong b) => a < b.value;
}
