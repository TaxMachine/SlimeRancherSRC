// Decompiled with JetBrains decompiler
// Type: ListAsset`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListAsset<T> : ScriptableObject, IEnumerable<T>, IEnumerable
{
  [SerializeField]
  private List<T> items = new List<T>();

  public T this[int index] => items[index];

  public int Count => items.Count;

  public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
