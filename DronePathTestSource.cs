// Decompiled with JetBrains decompiler
// Type: DronePathTestSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DronePathTestSource : MonoBehaviour
{
  public void OnDrawGizmos()
  {
    DroneNetwork componentInParent = GetComponentInParent<DroneNetwork>();
    Queue<Vector3> path = componentInParent.GeneratePath(transform.position, componentInParent.GetComponentInChildren<DronePathTestDest>().transform.position);
    if (path == null)
      return;
    Gizmos.color = Color.green;
    Vector3 vector3 = path.Dequeue();
    Gizmos.DrawLine(transform.position, vector3);
    while (path.Count > 0)
    {
      Vector3 to = path.Dequeue();
      Gizmos.DrawLine(vector3, to);
      vector3 = to;
    }
  }
}
