// Decompiled with JetBrains decompiler
// Type: reflectionFollowCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class reflectionFollowCamera : MonoBehaviour
{
  private float waterLevel;
  private Camera mainCamera;

  public void Awake() => waterLevel = transform.parent.transform.position.y;

  public void Start() => mainCamera = Camera.main;

  private void Update() => transform.position = new Vector3(transform.position.x, waterLevel - (mainCamera.transform.position.y - waterLevel), transform.position.z);
}
