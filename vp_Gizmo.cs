// Decompiled with JetBrains decompiler
// Type: vp_Gizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Gizmo : MonoBehaviour
{
  public Color gizmoColor = new Color(1f, 1f, 1f, 0.4f);
  public Color selectedGizmoColor = new Color(1f, 1f, 1f, 0.4f);
  protected Collider m_Collider;

  protected Collider Collider
  {
    get
    {
      if (m_Collider == null)
        m_Collider = GetComponent<Collider>();
      return m_Collider;
    }
  }

  public void OnDrawGizmos()
  {
    Bounds bounds = Collider.bounds;
    Vector3 center = bounds.center;
    bounds = Collider.bounds;
    Vector3 size1 = bounds.size;
    Gizmos.color = gizmoColor;
    Vector3 size2 = size1;
    Gizmos.DrawCube(center, size2);
    Gizmos.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    Gizmos.DrawLine(Vector3.zero, Vector3.forward);
  }

  public void OnDrawGizmosSelected()
  {
    Bounds bounds = Collider.bounds;
    Vector3 center = bounds.center;
    bounds = Collider.bounds;
    Vector3 size1 = bounds.size;
    Gizmos.color = selectedGizmoColor;
    Vector3 size2 = size1;
    Gizmos.DrawCube(center, size2);
    Gizmos.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    Gizmos.DrawLine(Vector3.zero, Vector3.forward);
  }
}
