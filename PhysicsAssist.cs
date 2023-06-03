// Decompiled with JetBrains decompiler
// Type: PhysicsAssist
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PhysicsAssist : MonoBehaviour
{
  public float assistAmount = 5f;

  public void OnCollisionEnter(Collision col) => col.rigidbody.AddForce(Vector3.down * assistAmount, ForceMode.VelocityChange);
}
