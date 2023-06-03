// Decompiled with JetBrains decompiler
// Type: vp_PoolManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class vp_PoolManager : MonoBehaviour
{
  public int MaxAmount = 25;
  public bool PoolOnDestroy = true;
  public List<GameObject> IgnoredPrefabs = new List<GameObject>();
  public List<vp_CustomPooledObject> CustomPrefabs = new List<vp_CustomPooledObject>();
  protected Transform m_Transform;
  protected Dictionary<string, List<UnityEngine.Object>> m_AvailableObjects = new Dictionary<string, List<UnityEngine.Object>>();
  protected Dictionary<string, List<UnityEngine.Object>> m_UsedObjects = new Dictionary<string, List<UnityEngine.Object>>();
  protected static vp_PoolManager m_Instance;

  public static vp_PoolManager Instance => m_Instance;

  protected virtual void Awake()
  {
    m_Instance = this;
    m_Transform = transform;
  }

  protected virtual void Start()
  {
    foreach (vp_CustomPooledObject customPrefab in CustomPrefabs)
      AddObjects(customPrefab.Prefab, Vector3.zero, Quaternion.identity, customPrefab.Buffer);
  }

  protected virtual void OnEnable()
  {
    vp_GlobalEventReturn<UnityEngine.Object, Vector3, Quaternion, UnityEngine.Object>.Register("vp_PoolManager Instantiate", InstantiateInternal);
    vp_GlobalEvent<UnityEngine.Object, float>.Register("vp_PoolManager Destroy", DestroyInternal);
  }

  protected virtual void OnDisable()
  {
    vp_GlobalEventReturn<UnityEngine.Object, Vector3, Quaternion, UnityEngine.Object>.Unregister("vp_PoolManager Instantiate", InstantiateInternal);
    vp_GlobalEvent<UnityEngine.Object, float>.Unregister("vp_PoolManager Destroy", DestroyInternal);
  }

  public virtual void AddObjects(UnityEngine.Object obj, Vector3 position, Quaternion rotation, int amount = 1)
  {
    if (obj == null)
      return;
    if (!m_AvailableObjects.ContainsKey(obj.name))
    {
      m_AvailableObjects.Add(obj.name, new List<UnityEngine.Object>());
      m_UsedObjects.Add(obj.name, new List<UnityEngine.Object>());
    }
    for (int index = 0; index < amount; ++index)
    {
      GameObject gameObject = Instantiate(obj, position, rotation) as GameObject;
      gameObject.name = obj.name;
      gameObject.transform.parent = m_Transform;
      vp_Utility.Activate(gameObject, false);
      m_AvailableObjects[obj.name].Add(gameObject);
    }
  }

  protected virtual UnityEngine.Object InstantiateInternal(
    UnityEngine.Object original,
    Vector3 position,
    Quaternion rotation)
  {
    if (IgnoredPrefabs.FirstOrDefault(obj => obj.name == original.name || obj.name == original.name + "(Clone)") != null)
      return Instantiate(original, position, rotation);
    List<UnityEngine.Object> source1 = null;
    List<UnityEngine.Object> source2 = null;
    if (m_AvailableObjects.TryGetValue(original.name, out source1))
    {
      GameObject gameObject1;
      while (true)
      {
        m_UsedObjects.TryGetValue(original.name, out source2);
        int num = source1.Count + source2.Count;
        if (CustomPrefabs.FirstOrDefault(obj => obj.Prefab.name == original.name) == null && num < MaxAmount && source1.Count == 0)
          AddObjects(original, position, rotation);
        if (source1.Count == 0)
        {
          GameObject gameObject2 = source2.FirstOrDefault() as GameObject;
          if (gameObject2 == null)
          {
            source2.Remove(gameObject2);
          }
          else
          {
            vp_Utility.Activate(gameObject2, false);
            source2.Remove(gameObject2);
            source1.Add(gameObject2);
          }
        }
        else
        {
          gameObject1 = source1.FirstOrDefault() as GameObject;
          if (gameObject1 == null)
            source1.Remove(gameObject1);
          else
            break;
        }
      }
      gameObject1.transform.position = position;
      gameObject1.transform.rotation = rotation;
      source1.Remove(gameObject1);
      source2.Add(gameObject1);
      vp_Utility.Activate(gameObject1);
      return gameObject1;
    }
    AddObjects(original, position, rotation);
    return InstantiateInternal(original, position, rotation);
  }

  protected virtual void DestroyInternal(UnityEngine.Object obj, float t)
  {
    if (obj == null)
      return;
    if (IgnoredPrefabs.FirstOrDefault(o => o.name == obj.name || o.name == obj.name + "(Clone)") != null || !m_AvailableObjects.ContainsKey(obj.name) && !PoolOnDestroy)
      Destroyer.Destroy(obj, t, "vp_PoolManager.DestroyInternal");
    else if (t != 0.0)
      vp_Timer.In(t, () => DestroyInternal(obj, 0.0f));
    else if (!m_AvailableObjects.ContainsKey(obj.name))
    {
      AddObjects(obj, Vector3.zero, Quaternion.identity);
    }
    else
    {
      List<UnityEngine.Object> objectList = null;
      List<UnityEngine.Object> source = null;
      m_AvailableObjects.TryGetValue(obj.name, out objectList);
      m_UsedObjects.TryGetValue(obj.name, out source);
      GameObject gameObject = source.FirstOrDefault(o => o.GetInstanceID() == obj.GetInstanceID()) as GameObject;
      if (gameObject == null)
        return;
      gameObject.transform.parent = m_Transform;
      vp_Utility.Activate(gameObject, false);
      source.Remove(gameObject);
      objectList.Add(gameObject);
    }
  }

  [Serializable]
  public class vp_CustomPooledObject
  {
    public GameObject Prefab;
    public int Buffer = 15;
    public int MaxAmount = 25;
  }
}
