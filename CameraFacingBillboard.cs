// Decompiled with JetBrains decompiler
// Type: CameraFacingBillboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
  private Camera mainCamera;

  public void Awake() => FaceCamera();

  public void OnRenderObject() => FaceCamera();

  private void FaceCamera()
  {
    if (mainCamera == null)
      mainCamera = Camera.main;
    transform.LookAt(mainCamera.transform);
  }
}
