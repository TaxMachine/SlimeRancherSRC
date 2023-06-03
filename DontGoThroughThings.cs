// Decompiled with JetBrains decompiler
// Type: DontGoThroughThings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DontGoThroughThings : MonoBehaviour
{
  private Rigidbody myRigidbody;
  private bool allowDestroy;
  private const float MIN_VELOCITY = 1f;
  private const float MIN_VELOCITY_SQR = 1f;

  public void Awake()
  {
    myRigidbody = GetComponent<Rigidbody>();
    myRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
  }

  public void FixedUpdate()
  {
    if (!allowDestroy || myRigidbody.velocity.sqrMagnitude >= 1.0)
      return;
    myRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    Destroyer.Destroy(this, "DontGoThroughThings.FixedUpdate");
  }

  public void AllowDestroy() => allowDestroy = true;
}
