// Decompiled with JetBrains decompiler
// Type: LRUCacheItem`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

internal class LRUCacheItem<K, V>
{
  public K key;
  private readonly WeakReference internalValue;

  public LRUCacheItem(K k, V v)
  {
    key = k;
    internalValue = new WeakReference(v);
  }

  public V value => (V) internalValue.Target;
}
