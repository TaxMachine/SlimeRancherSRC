// Decompiled with JetBrains decompiler
// Type: SlimeHoopTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeHoopTrigger : MonoBehaviour
{
  public SlimeHoop hoop;
  public List<Collider> passingDownwards = new List<Collider>();

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable componentInParent = col.GetComponentInParent<Identifiable>();
    if (!(componentInParent != null) || !Identifiable.IsSlime(componentInParent.id) || col.transform.position.y <= (double) transform.position.y || col.GetComponent<Rigidbody>().velocity.y >= 10.0)
      return;
    passingDownwards.Add(col);
  }

  public void OnTriggerExit(Collider col)
  {
    if (col.isTrigger)
      return;
    Identifiable componentInParent = col.GetComponentInParent<Identifiable>();
    if (!(componentInParent != null) || !Identifiable.IsSlime(componentInParent.id) || !passingDownwards.Contains(col))
      return;
    passingDownwards.Remove(col);
    if (col.transform.position.y >= (double) transform.position.y)
      return;
    hoop.AddScore();
  }

  public void Update()
  {
    for (int index = passingDownwards.Count - 1; index >= 0; --index)
    {
      if (passingDownwards[index] == null)
        passingDownwards.RemoveAt(index);
    }
  }
}
