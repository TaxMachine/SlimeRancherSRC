// Decompiled with JetBrains decompiler
// Type: QuantumCeiling
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuantumCeiling : SRBehaviour
{
  public static List<QuantumCeiling> Instances = new List<QuantumCeiling>();
  private Collider collider;

  public void Awake()
  {
    Instances.Add(this);
    collider = GetRequiredComponent<Collider>();
  }

  public void OnDestroy() => Instances.Remove(this);

  public static float AdjustMinDist(Vector3 pos, float defaultDist)
  {
    float a = defaultDist;
    foreach (QuantumCeiling instance in Instances)
    {
      if (instance.isActiveAndEnabled)
      {
        Vector3 vector3 = instance.collider.ClosestPoint(pos);
        if (vector3.x == (double) pos.x && vector3.z == (double) pos.z)
          a = Mathf.Max(a, pos.y - instance.collider.bounds.min.y);
      }
    }
    return a;
  }
}
