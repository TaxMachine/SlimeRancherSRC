// Decompiled with JetBrains decompiler
// Type: ObjectPoolPreloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolPreloader : MonoBehaviour
{
  [Tooltip("List of prefab entries to preload.")]
  public List<PreloadEntry> preloads;
  private Dictionary<GameObject, int> preloadsDict;
  private GameObject handler;

  public void Awake() => preloadsDict = preloads.ToDictionary(p => p.prefab, p => p.count);

  public void OnEnable()
  {
    Destroyer.Destroy(handler, "ObjectPoolPreloader.OnEnable");
    handler = SRSingleton<SceneContext>.Instance.fxPool.Preload(preloadsDict);
  }

  public void OnDisable()
  {
    Destroyer.Destroy(handler, "ObjectPoolPreloader.OnDisable");
    if (!(SRSingleton<SceneContext>.Instance != null) || SRSingleton<SceneContext>.Instance.fxPool == null)
      return;
    foreach (GameObject key in preloadsDict.Keys)
      SRSingleton<SceneContext>.Instance.fxPool.DestroyPooled(key);
  }

  [Serializable]
  public class PreloadEntry
  {
    [Tooltip("Prefab to preload.")]
    public GameObject prefab;
    [Tooltip("Number of prefab instances to preload.")]
    public int count;
  }
}
