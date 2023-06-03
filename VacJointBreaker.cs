// Decompiled with JetBrains decompiler
// Type: VacJointBreaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VacJointBreaker : MonoBehaviour
{
  private const float FLING_REDUCTION_FACTOR = 0.5f;

  public void OnJointBreak(float breakForce)
  {
    Vacuumable component = GetComponent<Joint>().connectedBody.GetComponent<Vacuumable>();
    component.release();
    component.gameObject.GetComponent<Rigidbody>().velocity = component.gameObject.GetComponent<Rigidbody>().velocity * 0.5f;
    Destroyer.Destroy(gameObject, "VacJointBreaker.OnJointBreak");
  }
}
