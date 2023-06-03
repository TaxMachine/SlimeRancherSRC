// Decompiled with JetBrains decompiler
// Type: ObjectPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class ObjectPool
{
  private static List<GameObject> tempList = new List<GameObject>();
  private Dictionary<GameObject, int> pooledObjectMaxInstances = new Dictionary<GameObject, int>();
  private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
  private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
  public GameObject poolRoot;
  public ObjectPoolConfig config;
  private bool startupPoolsCreated;

  public void CreateStartupPools()
  {
    if (startupPoolsCreated)
      return;
    startupPoolsCreated = true;
    ObjectPoolConfig.StartupPool[] startupPools = config.startupPools;
    if (startupPools == null || startupPools.Length == 0)
      return;
    for (int index = 0; index < startupPools.Length; ++index)
      CreatePool(startupPools[index].prefab, startupPools[index].size, startupPools[index].maxSize);
  }

  public void CreatePool<T>(T prefab, int initialPoolSize, int maxPoolSize) where T : Component => CreatePool(prefab.gameObject, initialPoolSize, maxPoolSize);

  public void CreatePool(GameObject prefab, int initialPoolSize, int maxPoolSize)
  {
    if (!(prefab != null) || pooledObjects.ContainsKey(prefab))
      return;
    List<GameObject> gameObjectList = new List<GameObject>();
    pooledObjects.Add(prefab, gameObjectList);
    pooledObjectMaxInstances.Add(prefab, Math.Max(initialPoolSize, maxPoolSize));
    if (initialPoolSize <= 0)
      return;
    bool activeSelf = prefab.activeSelf;
    prefab.SetActive(false);
    Transform transform = poolRoot.transform;
    while (gameObjectList.Count < initialPoolSize)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
      gameObject.transform.SetParent(transform, false);
      gameObjectList.Add(gameObject);
    }
    prefab.SetActive(activeSelf);
  }

  public T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component => Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();

  public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component => Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();

  public T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component => Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();

  public T Spawn<T>(T prefab, Vector3 position) where T : Component => Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();

  public T Spawn<T>(T prefab, Transform parent) where T : Component => Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();

  public T Spawn<T>(T prefab) where T : Component => Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();

  public GameObject Spawn(
    GameObject prefab,
    Transform parent,
    Vector3 position,
    Quaternion rotation)
  {
    List<GameObject> gameObjectList;
    if (pooledObjects.TryGetValue(prefab, out gameObjectList))
    {
      GameObject key1 = null;
      if (gameObjectList.Count > 0)
      {
        while (key1 == null && gameObjectList.Count > 0)
        {
          key1 = gameObjectList[gameObjectList.Count - 1];
          gameObjectList.RemoveAt(gameObjectList.Count - 1);
        }
        if (key1 != null)
        {
          Transform transform = key1.transform;
          transform.SetParent(parent, false);
          transform.localPosition = position;
          transform.localRotation = rotation;
          key1.SetActive(true);
          spawnedObjects.Add(key1, prefab);
          return key1;
        }
      }
      GameObject key2 = UnityEngine.Object.Instantiate(prefab);
      Transform transform1 = key2.transform;
      transform1.SetParent(parent, false);
      transform1.localPosition = position;
      transform1.localRotation = rotation;
      spawnedObjects.Add(key2, prefab);
      return key2;
    }
    GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
    Transform component = gameObject.GetComponent<Transform>();
    component.SetParent(parent, false);
    component.localPosition = position;
    component.localRotation = rotation;
    return gameObject;
  }

  public GameObject Spawn(GameObject prefab, Transform parent, Vector3 position) => Spawn(prefab, parent, position, Quaternion.identity);

  public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation) => Spawn(prefab, null, position, rotation);

  public GameObject Spawn(GameObject prefab, Transform parent) => Spawn(prefab, parent, Vector3.zero, Quaternion.identity);

  public GameObject Spawn(GameObject prefab, Vector3 position) => Spawn(prefab, null, position, Quaternion.identity);

  public GameObject Spawn(GameObject prefab) => Spawn(prefab, null, Vector3.zero, Quaternion.identity);

  public void Recycle<T>(T obj) where T : Component => Recycle(obj.gameObject);

  public void Recycle(GameObject obj)
  {
    GameObject prefab;
    if (spawnedObjects.TryGetValue(obj, out prefab) && !PoolHasMaxInstances(prefab))
    {
      Recycle(obj, prefab);
    }
    else
    {
      if (!(obj != null))
        return;
      obj.transform.SetParent(null, false);
      Destroyer.Destroy(obj.gameObject, "ObjectPool.Recycle");
    }
  }

  public void RecycleAfterFrame(GameObject toRecycle) => SRSingleton<SceneContext>.Instance.StartCoroutine(RecycleAfterFrame_Coroutine(toRecycle));

  private IEnumerator RecycleAfterFrame_Coroutine(GameObject toRecycle)
  {
    yield return new WaitForEndOfFrame();
    Recycle(toRecycle);
  }

  private bool PoolHasMaxInstances(GameObject prefab) => pooledObjects[prefab].Count >= pooledObjectMaxInstances[prefab];

  private void Recycle(GameObject obj, GameObject prefab)
  {
    spawnedObjects.Remove(obj);
    if (!(obj != null))
      return;
    pooledObjects[prefab].Add(obj);
    obj.transform.SetParent(poolRoot.transform, false);
    obj.SetActive(false);
  }

  public void RecycleAll<T>(T prefab) where T : Component => RecycleAll(prefab.gameObject);

  public void RecycleAll(GameObject prefab)
  {
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in spawnedObjects)
    {
      if (spawnedObject.Value == prefab)
        tempList.Add(spawnedObject.Key);
    }
    for (int index = 0; index < tempList.Count; ++index)
      Recycle(tempList[index]);
    tempList.Clear();
  }

  public void RecycleAll()
  {
    tempList.AddRange(spawnedObjects.Keys);
    for (int index = 0; index < tempList.Count; ++index)
      Recycle(tempList[index]);
    tempList.Clear();
  }

  public bool IsSpawned(GameObject obj) => spawnedObjects.ContainsKey(obj);

  public int CountPooled<T>(T prefab) where T : Component => CountPooled(prefab.gameObject);

  public int CountPooled(GameObject prefab)
  {
    List<GameObject> gameObjectList;
    return pooledObjects.TryGetValue(prefab, out gameObjectList) ? gameObjectList.Count : 0;
  }

  public int CountSpawned<T>(T prefab) where T : Component => CountSpawned(prefab.gameObject);

  public int CountSpawned(GameObject prefab)
  {
    int num = 0;
    foreach (GameObject gameObject in spawnedObjects.Values)
    {
      if (prefab == gameObject)
        ++num;
    }
    return num;
  }

  public int CountAllPooled()
  {
    int num = 0;
    foreach (List<GameObject> gameObjectList in pooledObjects.Values)
      num += gameObjectList.Count;
    return num;
  }

  public List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
  {
    if (list == null)
      list = new List<GameObject>();
    if (!appendList)
      list.Clear();
    List<GameObject> collection;
    if (pooledObjects.TryGetValue(prefab, out collection))
      list.AddRange(collection);
    return list;
  }

  public List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
  {
    if (list == null)
      list = new List<T>();
    if (!appendList)
      list.Clear();
    List<GameObject> gameObjectList;
    if (pooledObjects.TryGetValue(prefab.gameObject, out gameObjectList))
    {
      for (int index = 0; index < gameObjectList.Count; ++index)
        list.Add(gameObjectList[index].GetComponent<T>());
    }
    return list;
  }

  public List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
  {
    if (list == null)
      list = new List<GameObject>();
    if (!appendList)
      list.Clear();
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in spawnedObjects)
    {
      if (spawnedObject.Value == prefab)
        list.Add(spawnedObject.Key);
    }
    return list;
  }

  public List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
  {
    if (list == null)
      list = new List<T>();
    if (!appendList)
      list.Clear();
    GameObject gameObject = prefab.gameObject;
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in spawnedObjects)
    {
      if (spawnedObject.Value == gameObject)
        list.Add(spawnedObject.Key.GetComponent<T>());
    }
    return list;
  }

  public void DestroyPooled(GameObject prefab)
  {
    List<GameObject> gameObjectList;
    if (!pooledObjects.TryGetValue(prefab, out gameObjectList))
      return;
    for (int index = 0; index < gameObjectList.Count; ++index)
      Destroyer.Destroy(gameObjectList[index], "ObjectPool.DestroyPooled");
    gameObjectList.Clear();
  }

  public void DestroyPooled<T>(T prefab) where T : Component => DestroyPooled(prefab.gameObject);

  public void DestroyAll(GameObject prefab)
  {
    RecycleAll(prefab);
    DestroyPooled(prefab);
  }

  public void DestroyAll<T>(T prefab) where T : Component => DestroyAll(prefab.gameObject);

  public GameObject Preload(Dictionary<GameObject, int> prefabs)
  {
    foreach (KeyValuePair<GameObject, int> prefab in prefabs)
    {
      CreatePool(prefab.Key, 0, prefab.Value);
      int b;
      pooledObjectMaxInstances.TryGetValue(prefab.Key, out b);
      pooledObjectMaxInstances[prefab.Key] = Mathf.Max(prefab.Value, b);
    }
    GameObject gameObject = new GameObject("ObjectPool.Preload");
    gameObject.AddComponent<PreloadComponent>().Init(prefabs.Keys, this);
    gameObject.transform.SetParent(poolRoot.transform, false);
    return gameObject;
  }

  private class PreloadComponent : MonoBehaviour
  {
    public ObjectPool pool;
    private List<GameObject> prefabs;
    private int index;

    public void Init(IEnumerable<GameObject> prefabs, ObjectPool targetPool)
    {
      pool = targetPool;
      this.prefabs = new List<GameObject>(prefabs);
      index = 0;
    }

    public void Update()
    {
      for (; index < prefabs.Count; ++index)
      {
        GameObject prefab = prefabs[index];
        if (!pool.PoolHasMaxInstances(prefab))
        {
          pool.Recycle(Instantiate(prefab), prefab);
          return;
        }
      }
      Destroyer.Destroy(gameObject, "PreloadComponent.Update");
    }
  }
}
