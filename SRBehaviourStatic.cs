// Decompiled with JetBrains decompiler
// Type: SRBehaviourStatic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public static class SRBehaviourStatic
{
  public static I GetInterfaceComponent<I>(this GameObject obj) where I : class => obj.GetComponent(typeof (I)) as I;

  public static I GetInterfaceComponentInParent<I>(this GameObject obj) where I : class => obj.GetComponentInParent(typeof (I)) as I;

  public static V Get<K, V>(this Dictionary<K, V> dict, K key)
  {
    V v;
    dict.TryGetValue(key, out v);
    return v;
  }
}
