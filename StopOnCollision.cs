// Decompiled with JetBrains decompiler
// Type: StopOnCollision
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StopOnCollision : CollidableActorBehaviour, Collidable
{
  public float distFromCol = 0.25f;

  public void ProcessCollisionEnter(Collision col)
  {
    Vector3 vector3 = col.contacts[0].point + col.contacts[0].normal * distFromCol;
    GetComponent<Rigidbody>().position = vector3;
    GetComponent<Rigidbody>().velocity = Vector3.zero;
    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
  }

  public void ProcessCollisionExit(Collision col)
  {
  }
}
