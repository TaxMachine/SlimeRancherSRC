// Decompiled with JetBrains decompiler
// Type: CFX_SpawnSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CFX_SpawnSystem : MonoBehaviour
{
  private static CFX_SpawnSystem instance;
  public GameObject[] objectsToPreload = new GameObject[0];
  public int[] objectsToPreloadTimes = new int[0];
  public bool hideObjectsInHierarchy;
  private bool allObjectsLoaded;
  private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();
  private Dictionary<int, int> poolCursors = new Dictionary<int, int>();

  public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
  {
    int instanceId = sourceObj.GetInstanceID();
    if (!instance.poolCursors.ContainsKey(instanceId))
    {
      Debug.LogError("[CFX_SpawnSystem.GetNextPoolObject()] Object hasn't been preloaded: " + sourceObj.name + " (ID:" + instanceId + ")");
      return null;
    }
    int poolCursor = instance.poolCursors[instanceId];
    instance.poolCursors[instanceId]++;
    if (instance.poolCursors[instanceId] >= instance.instantiatedObjects[instanceId].Count)
      instance.poolCursors[instanceId] = 0;
    GameObject nextObject = instance.instantiatedObjects[instanceId][poolCursor];
    if (activateObject)
      nextObject.SetActive(true);
    return nextObject;
  }

  public static void PreloadObject(GameObject sourceObj, int poolSize = 1) => instance.addObjectToPool(sourceObj, poolSize);

  public static void UnloadObjects(GameObject sourceObj) => instance.removeObjectsFromPool(sourceObj);

  public static bool AllObjectsLoaded => instance.allObjectsLoaded;

  private void addObjectToPool(GameObject sourceObject, int number)
  {
    int instanceId = sourceObject.GetInstanceID();
    if (!instantiatedObjects.ContainsKey(instanceId))
    {
      instantiatedObjects.Add(instanceId, new List<GameObject>());
      poolCursors.Add(instanceId, 0);
    }
    for (int index = 0; index < number; ++index)
    {
      GameObject gameObject = Instantiate(sourceObject);
      gameObject.SetActive(false);
      foreach (CFX_AutoDestructShuriken componentsInChild in gameObject.GetComponentsInChildren<CFX_AutoDestructShuriken>(true))
        componentsInChild.OnlyDeactivate = true;
      foreach (CFX_LightIntensityFade componentsInChild in gameObject.GetComponentsInChildren<CFX_LightIntensityFade>(true))
        componentsInChild.autodestruct = false;
      instantiatedObjects[instanceId].Add(gameObject);
      if (hideObjectsInHierarchy)
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
  }

  private void removeObjectsFromPool(GameObject sourceObject)
  {
    int instanceId = sourceObject.GetInstanceID();
    if (!instantiatedObjects.ContainsKey(instanceId))
    {
      Debug.LogWarning("[CFX_SpawnSystem.removeObjectsFromPool()] There aren't any preloaded object for: " + sourceObject.name + " (ID:" + instanceId + ")");
    }
    else
    {
      for (int index = instantiatedObjects[instanceId].Count - 1; index >= 0; --index)
      {
        GameObject instance = instantiatedObjects[instanceId][index];
        instantiatedObjects[instanceId].RemoveAt(index);
        Destroyer.Destroy(instance, "CFX_SpawnSystem.removeObjectsFromPool");
      }
      instantiatedObjects.Remove(instanceId);
      poolCursors.Remove(instanceId);
    }
  }

  private void Awake()
  {
    if (instance != null)
      Debug.LogWarning("CFX_SpawnSystem: There should only be one instance of CFX_SpawnSystem per Scene!");
    instance = this;
  }

  private void Start()
  {
    allObjectsLoaded = false;
    for (int index = 0; index < objectsToPreload.Length; ++index)
      PreloadObject(objectsToPreload[index], objectsToPreloadTimes[index]);
    allObjectsLoaded = true;
  }
}
