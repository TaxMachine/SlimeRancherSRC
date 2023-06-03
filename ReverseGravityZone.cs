// Decompiled with JetBrains decompiler
// Type: ReverseGravityZone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityZone : MonoBehaviour
{
  private bool operating;
  private List<Rigidbody> bodies = new List<Rigidbody>();

  public void SetOperating(bool operating)
  {
    if (operating == this.operating)
      return;
    this.operating = operating;
  }

  public bool GetOperating() => operating;

  public void OnTriggerEnter(Collider col)
  {
    Rigidbody component = col.GetComponent<Rigidbody>();
    if (!(component != null))
      return;
    bodies.Add(component);
  }

  public void OnTriggerExit(Collider col)
  {
    Rigidbody component = col.GetComponent<Rigidbody>();
    if (!(component != null))
      return;
    bodies.Remove(component);
  }

  public void Update()
  {
    if (!operating)
      return;
    List<Rigidbody> rigidbodyList = new List<Rigidbody>();
    foreach (Rigidbody body in bodies)
    {
      if (body == null)
        rigidbodyList.Add(body);
      else
        AntiGrav(body);
    }
    foreach (Rigidbody rigidbody in rigidbodyList)
      bodies.Remove(rigidbody);
  }

  private void AntiGrav(Rigidbody body)
  {
    Vector3 force = -Physics.gravity * 2f;
    body.AddForce(force, ForceMode.Acceleration);
  }
}
