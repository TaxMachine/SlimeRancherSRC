// Decompiled with JetBrains decompiler
// Type: BoxColliderSolid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (BoxCollider))]
public class BoxColliderSolid : MonoBehaviour
{
  public Color color = new Color(1f, 1f, 1f, 0.8f);

  public void OnDrawGizmos()
  {
    BoxCollider component = GetComponent<BoxCollider>();
    Gizmos.color = color;
    Gizmos.DrawCube(transform.TransformPoint(component.center), transform.TransformVector(component.size));
  }
}
