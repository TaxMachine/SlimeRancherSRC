// Decompiled with JetBrains decompiler
// Type: PollenContactDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PollenContactDetector : MonoBehaviour
{
  private PollenCloudDestructor destructor;

  public void Awake() => destructor = GetComponentInParent<PollenCloudDestructor>();

  public void OnTriggerEnter(Collider col)
  {
    if (col.isTrigger || !(col.GetComponent<Rigidbody>() == null))
      return;
    destructor.AddContact();
  }

  public void OnTriggerExit(Collider col)
  {
    if (col.isTrigger || !(col.GetComponent<Rigidbody>() == null))
      return;
    destructor.RemoveContact();
  }
}
