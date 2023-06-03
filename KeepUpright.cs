// Decompiled with JetBrains decompiler
// Type: KeepUpright
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class KeepUpright : RegisteredActorBehaviour, RegistryFixedUpdateable
{
  public float stability = 0.3f;
  public float speed = 2f;
  private Rigidbody body;
  private float speedFactor;
  private float momentum;

  public override void Start()
  {
    base.Start();
    body = GetComponent<Rigidbody>();
    speedFactor = 57.29578f * stability / speed;
    momentum = speed * speed * body.mass;
  }

  public virtual void RegistryFixedUpdate() => DoUpright(Vector3.up);

  protected void DoUpright(Vector3 desiredUp)
  {
    if (body == null)
      return;
    Vector3 angularVelocity = body.angularVelocity;
    body.AddTorque(Vector3.Cross(Quaternion.AngleAxis(angularVelocity.magnitude * speedFactor, angularVelocity) * transform.up, desiredUp) * momentum);
  }
}
