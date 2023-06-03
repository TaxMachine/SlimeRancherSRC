// Decompiled with JetBrains decompiler
// Type: SlimePreviewCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimePreviewCamera : MonoBehaviour
{
  public Camera cam;
  public Transform target;
  public Vector3 lookOffset;
  public float angleSpeed = 120f;
  public float moveSpeed = 20f;
  public float minDistance = 1.25f;
  public float maxDistance = 10f;
  public Vector3 cameraOffset;
  public bool zoomControlsEnabled = true;

  private void Update()
  {
    if (!(target != null) || !zoomControlsEnabled)
      return;
    float axis1 = Input.GetAxis("Horizontal");
    double axis2 = Input.GetAxis("Vertical");
    cam.transform.RotateAround(target.position, Vector3.up, -1f * axis1 * angleSpeed * Time.deltaTime);
    Vector3 normalized = (target.position - cam.transform.position).normalized;
    Vector3 vector3 = (float) axis2 * normalized * moveSpeed * Time.deltaTime;
    float num = Vector3.Distance(target.position, cam.transform.position + vector3);
    if (num >= (double) minDistance && num <= (double) maxDistance)
      cam.transform.position += vector3;
    cam.transform.LookAt(target.position + lookOffset);
  }

  public void ResetCamToTarget(Transform target)
  {
    this.target = target;
    cam.transform.position = target.position + cameraOffset;
    cam.transform.LookAt(target);
  }
}
