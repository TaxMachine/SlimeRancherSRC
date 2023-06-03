// Decompiled with JetBrains decompiler
// Type: SECTR_RegionSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Region Source")]
public class SECTR_RegionSource : SECTR_PointSource
{
  [SECTR_ToolTip("Determine the closest point by raycast instead of bounding box. More accurate but more expensive.")]
  public bool Raycast;

  private void Update()
  {
    if (!instance)
      return;
    Vector3 position = SECTR_AudioSystem.Listener.position;
    Vector3 vector3 = transform.position;
    Collider component = GetComponent<Collider>();
    if (Raycast && (bool) (Object) component)
    {
      Vector3 direction = transform.position - position;
      float magnitude = direction.magnitude;
      direction /= magnitude;
      RaycastHit hitInfo;
      vector3 = !component.Raycast(new Ray(position, direction), out hitInfo, magnitude) ? position : hitInfo.point;
    }
    else if ((bool) (Object) component)
      vector3 = !component.bounds.Contains(position) ? component.ClosestPointOnBounds(position) : position;
    instance.Position = vector3;
  }
}
