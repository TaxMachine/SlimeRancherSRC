// Decompiled with JetBrains decompiler
// Type: TrackCollisions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TrackCollisions : SRBehaviour
{
  private Dictionary<GameObject, List<Collider>> currColliders = new Dictionary<GameObject, List<Collider>>();
  private HashSet<GameObject> gameObjSet = new HashSet<GameObject>();
  private List<GameObject> local_gameObjsToRemove = new List<GameObject>(50);
  private List<Collider> local_collidersToKeep = new List<Collider>(50);
  private HashSet<GameObject> emptySet = new HashSet<GameObject>();

  protected virtual void OnTriggerEnter(Collider other)
  {
    if (!currColliders.ContainsKey(other.gameObject))
      currColliders[other.gameObject] = new List<Collider>();
    currColliders[other.gameObject].Add(other);
    gameObjSet.Add(other.gameObject);
  }

  protected virtual void OnTriggerExit(Collider other)
  {
    if (!currColliders.ContainsKey(other.gameObject))
      return;
    currColliders[other.gameObject].Remove(other);
    if (currColliders[other.gameObject].Count != 0)
      return;
    currColliders.Remove(other.gameObject);
    gameObjSet.Remove(other.gameObject);
  }

  public HashSet<GameObject> CurrColliders()
  {
    foreach (KeyValuePair<GameObject, List<Collider>> currCollider in currColliders)
    {
      foreach (Collider collider in currCollider.Value)
      {
        if (!RemovePredicate(collider))
          local_collidersToKeep.Add(collider);
      }
      if (local_collidersToKeep.Count == 0)
      {
        local_gameObjsToRemove.Add(currCollider.Key);
      }
      else
      {
        currCollider.Value.Clear();
        foreach (Collider collider in local_collidersToKeep)
          currCollider.Value.Add(collider);
      }
      local_collidersToKeep.Clear();
    }
    foreach (GameObject key in local_gameObjsToRemove)
    {
      gameObjSet.Remove(key);
      currColliders.Remove(key);
    }
    local_gameObjsToRemove.Clear();
    foreach (GameObject gameObj in gameObjSet)
    {
      if (RemovePredicate(gameObj))
        local_gameObjsToRemove.Add(gameObj);
    }
    foreach (GameObject gameObject in local_gameObjsToRemove)
      gameObjSet.Remove(gameObject);
    local_gameObjsToRemove.Clear();
    return gameObjSet;
  }

  private static bool RemovePredicate(Collider collider) => collider == null || !collider.enabled || RemovePredicate(collider.gameObject);

  private static bool RemovePredicate(GameObject go) => go == null || !go.activeInHierarchy;
}
