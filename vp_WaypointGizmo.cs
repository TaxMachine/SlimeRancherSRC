// Decompiled with JetBrains decompiler
// Type: vp_WaypointGizmo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_WaypointGizmo : MonoBehaviour
{
  protected Color m_GizmoColor = new Color(1f, 1f, 1f, 0.4f);
  protected Color m_SelectedGizmoColor = new Color32(160, byte.MaxValue, 100, 100);

  public void OnDrawGizmos()
  {
    Gizmos.matrix = transform.localToWorldMatrix;
    Gizmos.color = m_GizmoColor;
    Gizmos.DrawCube(Vector3.zero, Vector3.one);
    Gizmos.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    Gizmos.DrawLine(Vector3.zero, Vector3.forward);
  }

  public void OnDrawGizmosSelected()
  {
    Gizmos.matrix = transform.localToWorldMatrix;
    Gizmos.color = m_SelectedGizmoColor;
    Gizmos.DrawCube(Vector3.zero, Vector3.one);
    Gizmos.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    Gizmos.DrawLine(Vector3.zero, Vector3.forward);
  }
}
