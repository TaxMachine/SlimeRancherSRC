// Decompiled with JetBrains decompiler
// Type: ObjectPoolConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjectPoolConfig")]
public class ObjectPoolConfig : ScriptableObject
{
  public StartupPool[] startupPools;
  public StartupPoolMode startupPoolMode;
  public bool loggingEnabled;

  public IEnumerable<string> CheckPooledConfiguration()
  {
    ObjectPoolConfig objectPoolConfig = this;
    for (int ii = 0; ii < objectPoolConfig.startupPools.Length; ++ii)
    {
      StartupPool startupPool = objectPoolConfig.startupPools[ii];
      if (startupPool == null)
        yield return string.Format("Pool {1}[{0}] is null.", ii, objectPoolConfig.name);
      else if (startupPool.prefab == null)
      {
        yield return string.Format("Pool {1}[{0}] has a null prefab.", ii, objectPoolConfig.name);
      }
      else
      {
        string str = objectPoolConfig.CheckForPooledParticleFX(startupPool.prefab, ii, !startupPool.doesNotSelfDestruct);
        if (!string.IsNullOrEmpty(str))
          yield return str;
      }
    }
  }

  private string CheckForPooledParticleFX(GameObject prefab, int index, bool shouldAutoDestruct)
  {
    string str = string.Format("Pool {2}[{0}] ({1}) ", index, prefab.name, name);
    ParticleSystem particleSystem = prefab.GetComponent<ParticleSystem>();
    bool flag = false;
    if (particleSystem == null)
    {
      particleSystem = prefab.GetComponentInChildren<ParticleSystem>();
      if (particleSystem == null)
        return null;
      flag = true;
      str += "child particle system ";
    }
    CFX_AutoDestructShuriken component = particleSystem.gameObject.GetComponent<CFX_AutoDestructShuriken>();
    if (shouldAutoDestruct)
    {
      if (component == null)
        return string.Format(str + "does not have a CFX_AutoDestructShuriken script.");
      if (!component.RecycleOnCompletion)
        return string.Format(str + "is not set to be recycled.");
      if (flag && !component.RecycleParent)
        return string.Format(str + "is not set to have its parent recycled.");
    }
    else if (component != null)
      return string.Format(str + "is not supposed to have a CFX_AutoDestructShuriken script.");
    return null;
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
}
