// Decompiled with JetBrains decompiler
// Type: vp_RigidbodyImpulse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class vp_RigidbodyImpulse : MonoBehaviour
{
  protected Rigidbody m_Rigidbody;
  public Vector3 RigidbodyForce = new Vector3(0.0f, 5f, 0.0f);
  public float RigidbodySpin = 0.2f;

  protected virtual void Awake() => m_Rigidbody = GetComponent<Rigidbody>();

  protected virtual void OnEnable()
  {
    if (m_Rigidbody == null)
      return;
    if (RigidbodyForce != Vector3.zero)
      m_Rigidbody.AddForce(RigidbodyForce, ForceMode.Impulse);
    if (RigidbodySpin == 0.0)
      return;
    m_Rigidbody.AddTorque(Random.rotation.eulerAngles * RigidbodySpin);
  }
}
