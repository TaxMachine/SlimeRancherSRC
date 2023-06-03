// Decompiled with JetBrains decompiler
// Type: ReferenceCount`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferenceCount<T> : IEnumerable<KeyValuePair<T, int>>, IEnumerable
{
  private readonly Dictionary<T, int> dictionary;

  public ReferenceCount() => dictionary = new Dictionary<T, int>();

  public ReferenceCount(IEqualityComparer<T> keyComparer) => dictionary = new Dictionary<T, int>(keyComparer);

  public ReferenceCount(IEnumerable<KeyValuePair<T, int>> enumerable) => dictionary = enumerable.ToDictionary(kv => kv.Key, kv => kv.Value);

  public ReferenceCount(
    IEnumerable<KeyValuePair<T, int>> enumerable,
    IEqualityComparer<T> keyComparer)
  {
    dictionary = enumerable.ToDictionary(kv => kv.Key, kv => kv.Value, keyComparer);
  }

  private static bool AOTHelper_Dictionary_Rigidbody_int() => new Dictionary<Rigidbody, int>() == null;

  private static bool AOTHelper_Dictionary_IdentifiableId_int() => new Dictionary<Identifiable.Id, int>() == null;

  public int Increment(T key)
  {
    int num = GetCount(key) + 1;
    dictionary[key] = num;
    return num;
  }

  public int Decrement(T key)
  {
    int num = GetCount(key) - 1;
    if (num < 0)
      throw new InvalidOperationException();
    if (num == 0)
      dictionary.Remove(key);
    else
      dictionary[key] = num;
    return num;
  }

  public int GetCount(T key)
  {
    int count;
    dictionary.TryGetValue(key, out count);
    return count;
  }

  public IEnumerable<T> Keys => dictionary.Keys;

  public IEnumerator<KeyValuePair<T, int>> GetEnumerator() => dictionary.GetEnumerator();

  private IEnumerator GetEnumerator_private() => GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator_private();

  public void Remove(T key)
  {
    if (!dictionary.ContainsKey(key))
      return;
    dictionary.Remove(key);
  }

  public void Clear() => dictionary.Clear();

  public bool ContainsKey(T key) => GetCount(key) > 0;

  private class Dictionary_Rigidbody_int
  {
    private Dictionary<Rigidbody, int> instance;
  }

  private class Dictionary_IdentifiableId_int
  {
    private Dictionary<Identifiable.Id, int> instance;
  }
}
