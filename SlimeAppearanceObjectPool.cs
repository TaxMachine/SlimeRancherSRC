// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceObjectPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class SlimeAppearanceObjectPool : MonoBehaviour, SlimeAppearanceObjectProvider
{
  private static SlimeAppearanceObjectPool _instance;
  private static List<GameObject> tempList = new List<GameObject>();
  private Dictionary<GameObject, int> pooledObjectMaxInstances = new Dictionary<GameObject, int>();
  private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
  private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
  public StartupPoolMode startupPoolMode;
  public StartupPool[] startupPools;
  private bool startupPoolsCreated;

  private void Awake()
  {
    _instance = this;
    if (startupPoolMode != StartupPoolMode.Awake)
      return;
    CreateStartupPools();
  }

  private void Start()
  {
    if (startupPoolMode != StartupPoolMode.Start)
      return;
    CreateStartupPools();
  }

  public static void CreateStartupPools()
  {
    if (instance.startupPoolsCreated)
      return;
    instance.startupPoolsCreated = true;
    StartupPool[] startupPools = instance.startupPools;
    if (startupPools == null || startupPools.Length == 0)
      return;
    for (int index = 0; index < startupPools.Length; ++index)
      CreatePool(startupPools[index].prefab, startupPools[index].size, startupPools[index].maxSize);
  }

  public static void CreatePool<T>(T prefab, int initialPoolSize, int maxPoolSize) where T : Component => CreatePool(prefab.gameObject, initialPoolSize, maxPoolSize);

  public static void CreatePool(GameObject prefab, int initialPoolSize, int maxPoolSize)
  {
    if (!(prefab != null) || instance.pooledObjects.ContainsKey(prefab))
      return;
    List<GameObject> gameObjectList = new List<GameObject>();
    instance.pooledObjects.Add(prefab, gameObjectList);
    instance.pooledObjectMaxInstances.Add(prefab, Math.Max(initialPoolSize, maxPoolSize));
    if (initialPoolSize <= 0)
      return;
    bool activeSelf = prefab.activeSelf;
    prefab.SetActive(false);
    Transform transform = instance.transform;
    while (gameObjectList.Count < initialPoolSize)
    {
      GameObject gameObject = Instantiate(prefab);
      gameObject.transform.parent = transform;
      gameObjectList.Add(gameObject);
    }
    prefab.SetActive(activeSelf);
  }

  public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component => Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();

  public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component => Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();

  public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component => Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();

  public static T Spawn<T>(T prefab, Vector3 position) where T : Component => Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();

  public static T Spawn<T>(T prefab, Transform parent) where T : Component => Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();

  public static T Spawn<T>(T prefab) where T : Component => Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();

  public static GameObject Spawn(
    GameObject prefab,
    Transform parent,
    Vector3 position,
    Quaternion rotation)
  {
    List<GameObject> gameObjectList;
    if (instance.pooledObjects.TryGetValue(prefab, out gameObjectList))
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
          instance.spawnedObjects.Add(key1, prefab);
          return key1;
        }
      }
      GameObject key2 = Instantiate(prefab);
      Transform transform1 = key2.transform;
      transform1.SetParent(parent, false);
      transform1.localPosition = position;
      transform1.localRotation = rotation;
      instance.spawnedObjects.Add(key2, prefab);
      return key2;
    }
    GameObject gameObject = Instantiate(prefab);
    Transform component = gameObject.GetComponent<Transform>();
    component.SetParent(parent, false);
    component.localPosition = position;
    component.localRotation = rotation;
    return gameObject;
  }

  public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position) => Spawn(prefab, parent, position, Quaternion.identity);

  public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation) => Spawn(prefab, null, position, rotation);

  public static GameObject Spawn(GameObject prefab, Transform parent) => Spawn(prefab, parent, Vector3.zero, Quaternion.identity);

  public static GameObject Spawn(GameObject prefab, Vector3 position) => Spawn(prefab, null, position, Quaternion.identity);

  public static GameObject Spawn(GameObject prefab) => Spawn(prefab, null, Vector3.zero, Quaternion.identity);

  public static void Recycle<T>(T obj) where T : Component => Recycle(obj.gameObject);

  public static void Recycle(GameObject obj)
  {
    GameObject prefab;
    if (instance.spawnedObjects.TryGetValue(obj, out prefab) && !PoolHasMaxInstances(prefab))
    {
      Recycle(obj, prefab);
    }
    else
    {
      obj.transform.parent = null;
      Destroyer.Destroy(obj.gameObject, "SlimeAppearanceObjectPool.Recycle");
    }
  }

  private static bool PoolHasMaxInstances(GameObject prefab) => instance.pooledObjects[prefab].Count >= instance.pooledObjectMaxInstances[prefab];

  private static void Recycle(GameObject obj, GameObject prefab)
  {
    instance.pooledObjects[prefab].Add(obj);
    instance.spawnedObjects.Remove(obj);
    obj.transform.SetParent(instance.transform, false);
    obj.SetActive(false);
  }

  public static void RecycleAll<T>(T prefab) where T : Component => RecycleAll(prefab.gameObject);

  public static void RecycleAll(GameObject prefab)
  {
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
    {
      if (spawnedObject.Value == prefab)
        tempList.Add(spawnedObject.Key);
    }
    for (int index = 0; index < tempList.Count; ++index)
      Recycle(tempList[index]);
    tempList.Clear();
  }

  public static void RecycleAll()
  {
    tempList.AddRange(instance.spawnedObjects.Keys);
    for (int index = 0; index < tempList.Count; ++index)
      Recycle(tempList[index]);
    tempList.Clear();
  }

  public static bool IsSpawned(GameObject obj) => instance.spawnedObjects.ContainsKey(obj);

  public static int CountPooled<T>(T prefab) where T : Component => CountPooled(prefab.gameObject);

  public static int CountPooled(GameObject prefab)
  {
    List<GameObject> gameObjectList;
    return instance.pooledObjects.TryGetValue(prefab, out gameObjectList) ? gameObjectList.Count : 0;
  }

  public static int CountSpawned<T>(T prefab) where T : Component => CountSpawned(prefab.gameObject);

  public static int CountSpawned(GameObject prefab)
  {
    int num = 0;
    foreach (GameObject gameObject in instance.spawnedObjects.Values)
    {
      if (prefab == gameObject)
        ++num;
    }
    return num;
  }

  public static int CountAllPooled()
  {
    int num = 0;
    foreach (List<GameObject> gameObjectList in instance.pooledObjects.Values)
      num += gameObjectList.Count;
    return num;
  }

  public static List<GameObject> GetPooled(
    GameObject prefab,
    List<GameObject> list,
    bool appendList)
  {
    if (list == null)
      list = new List<GameObject>();
    if (!appendList)
      list.Clear();
    List<GameObject> collection;
    if (instance.pooledObjects.TryGetValue(prefab, out collection))
      list.AddRange(collection);
    return list;
  }

  public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
  {
    if (list == null)
      list = new List<T>();
    if (!appendList)
      list.Clear();
    List<GameObject> gameObjectList;
    if (instance.pooledObjects.TryGetValue(prefab.gameObject, out gameObjectList))
    {
      for (int index = 0; index < gameObjectList.Count; ++index)
        list.Add(gameObjectList[index].GetComponent<T>());
    }
    return list;
  }

  public static List<GameObject> GetSpawned(
    GameObject prefab,
    List<GameObject> list,
    bool appendList)
  {
    if (list == null)
      list = new List<GameObject>();
    if (!appendList)
      list.Clear();
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
    {
      if (spawnedObject.Value == prefab)
        list.Add(spawnedObject.Key);
    }
    return list;
  }

  public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
  {
    if (list == null)
      list = new List<T>();
    if (!appendList)
      list.Clear();
    GameObject gameObject = prefab.gameObject;
    foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
    {
      if (spawnedObject.Value == gameObject)
        list.Add(spawnedObject.Key.GetComponent<T>());
    }
    return list;
  }

  public static void DestroyPooled(GameObject prefab)
  {
    List<GameObject> gameObjectList;
    if (!instance.pooledObjects.TryGetValue(prefab, out gameObjectList))
      return;
    for (int index = 0; index < gameObjectList.Count; ++index)
      Destroyer.Destroy(gameObjectList[index], "ObjectPool.DestroyPooled");
    gameObjectList.Clear();
  }

  public static void DestroyPooled<T>(T prefab) where T : Component => DestroyPooled(prefab.gameObject);

  public static void DestroyAll(GameObject prefab)
  {
    RecycleAll(prefab);
    DestroyPooled(prefab);
  }

  public static void DestroyAll<T>(T prefab) where T : Component => DestroyAll(prefab.gameObject);

  public static SlimeAppearanceObjectPool instance
  {
    get
    {
      if (_instance != null)
        return _instance;
      _instance = FindObjectOfType<SlimeAppearanceObjectPool>();
      if (_instance != null)
        return _instance;
      _instance = new GameObject(nameof (SlimeAppearanceObjectPool))
      {
        transform = {
          localPosition = Vector3.zero,
          localRotation = Quaternion.identity,
          localScale = Vector3.one
        }
      }.AddComponent<SlimeAppearanceObjectPool>();
      return _instance;
    }
  }

  public List<string> CheckPooledConfiguration()
  {
    List<string> stringList = new List<string>();
    if (startupPools == null || startupPools.Length == 0)
    {
      stringList.Add("No pools are configured");
      return stringList;
    }
    for (int index = 0; index < startupPools.Length; ++index)
    {
      StartupPool startupPool = startupPools[index];
      if (startupPool == null)
        stringList.Add(string.Format("Pool {0} is null.", index));
      else if (startupPool.prefab == null)
        stringList.Add(string.Format("Pool {0} has a null prefab.", index));
      else if (startupPool.size == 0)
        stringList.Add(string.Format("Pool {0} has a pool count of zero.", index));
    }
    return stringList;
  }

  public static GameObject Preload(Dictionary<GameObject, int> prefabs)
  {
    foreach (KeyValuePair<GameObject, int> prefab in prefabs)
    {
      CreatePool(prefab.Key, 0, prefab.Value);
      int b;
      instance.pooledObjectMaxInstances.TryGetValue(prefab.Key, out b);
      instance.pooledObjectMaxInstances[prefab.Key] = Mathf.Max(prefab.Value, b);
    }
    GameObject gameObject = new GameObject("SlimeAppearanceObjectPool.Preload");
    gameObject.AddComponent<PreloadComponent>().Init(prefabs.Keys);
    gameObject.transform.SetParent(instance.transform, false);
    return gameObject;
  }

  public SlimeAppearanceObject Get(
    SlimeAppearanceObject appearanceObjectPrefab,
    GameObject targetParent)
  {
    return Spawn(appearanceObjectPrefab, targetParent.transform, appearanceObjectPrefab.transform.position, appearanceObjectPrefab.transform.rotation);
  }

  public void Put(
    SlimeAppearanceObject appearanceObjectPrefab,
    SlimeAppearanceObject appearanceObject)
  {
    Recycle(appearanceObject);
  }

  public enum StartupPoolMode
  {
    Awake,
    Start,
    CallManually,
  }

  [Serializable]
  public class StartupPool
  {
    public int size;
    public GameObject prefab;
    public int maxSize;
    public bool doesNotSelfDestruct;
  }

  private class PreloadComponent : MonoBehaviour
  {
    private List<GameObject> prefabs;
    private int index;

    public void Init(IEnumerable<GameObject> prefabs)
    {
      this.prefabs = new List<GameObject>(prefabs);
      index = 0;
    }

    public void Update()
    {
      for (; index < prefabs.Count; ++index)
      {
        GameObject prefab = prefabs[index];
        if (!PoolHasMaxInstances(prefab))
        {
          Recycle(Instantiate(prefab), prefab);
          return;
        }
      }
      Destroyer.Destroy(gameObject, "PreloadComponent.Update");
    }
  }
}
