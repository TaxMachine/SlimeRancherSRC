// Decompiled with JetBrains decompiler
// Type: seaFollowCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class seaFollowCamera : MonoBehaviour
{
  private Rigidbody seaRigidbody;
  private Camera mainCamera;

  public void Awake() => seaRigidbody = GetComponent<Rigidbody>();

  public void Start() => mainCamera = Camera.main;

  private void Update()
  {
    Vector3 position = mainCamera.transform.position;
    seaRigidbody.MovePosition(new Vector3(position.x, transform.position.y, position.z));
  }
}
