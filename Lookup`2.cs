// Decompiled with JetBrains decompiler
// Type: Lookup`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

public class Lookup<K, V> : IEnumerable
{
  private Dictionary<K, V> dictionary;
  private Dictionary<V, K> dictionaryReversed;

  public Lookup(IEqualityComparer<K> keyComparer)
  {
    dictionary = new Dictionary<K, V>(keyComparer);
    dictionaryReversed = new Dictionary<V, K>();
  }

  public void Add(K key, V value)
  {
    dictionary.Add(key, value);
    dictionaryReversed.Add(value, key);
  }

  public IEnumerable<K> Keys => dictionary.Keys;

  public IEnumerator GetEnumerator() => dictionary.GetEnumerator();

  public bool TryGetValue(K key, out V value) => dictionary.TryGetValue(key, out value);

  public bool TryGetValue(V key, out K value) => dictionaryReversed.TryGetValue(key, out value);
}
