// Decompiled with JetBrains decompiler
// Type: ContinualRotation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class ContinualRotation : MonoBehaviour
{
  public float secsPerRotate = 10f;
  private Vector3 angularVel;
  private Rigidbody ownRigidbody;

  public void Start()
  {
    ownRigidbody = GetComponent<Rigidbody>();
    ownRigidbody.isKinematic = true;
    ownRigidbody.useGravity = false;
    angularVel = new Vector3(0.0f, 360f / secsPerRotate, 0.0f);
  }

  public void FixedUpdate() => ownRigidbody.MoveRotation(Quaternion.Euler(ownRigidbody.rotation.eulerAngles + angularVel * Time.fixedDeltaTime));
}
