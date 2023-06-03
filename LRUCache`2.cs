// Decompiled with JetBrains decompiler
// Type: LRUCache`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class LRUCache<K, V>
{
  private int capacity;
  private Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>> cacheMap = new Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>>();
  private LinkedList<LRUCacheItem<K, V>> lruList = new LinkedList<LRUCacheItem<K, V>>();

  public LRUCache(int capacity) => this.capacity = capacity;

  public bool contains(K key) => cacheMap.ContainsKey(key);

  public V get(K key)
  {
    LinkedListNode<LRUCacheItem<K, V>> node;
    if (!cacheMap.TryGetValue(key, out node))
      throw new KeyNotFoundException();
    V v = node.Value.value;
    lruList.Remove(node);
    lruList.AddLast(node);
    return v;
  }

  public void put(K key, V val)
  {
    if (cacheMap.Count >= capacity)
      removeFirst();
    LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(new LRUCacheItem<K, V>(key, val));
    lruList.AddLast(node);
    cacheMap.Add(key, node);
  }

  public void clear(K key)
  {
    LinkedListNode<LRUCacheItem<K, V>> node;
    if (!cacheMap.TryGetValue(key, out node))
      return;
    lruList.Remove(node);
    cacheMap.Remove(key);
  }

  protected void removeFirst()
  {
    LinkedListNode<LRUCacheItem<K, V>> first = lruList.First;
    lruList.RemoveFirst();
    cacheMap.Remove(first.Value.key);
  }
}
