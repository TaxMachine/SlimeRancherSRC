// Decompiled with JetBrains decompiler
// Type: vp_Shell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (AudioSource))]
public class vp_Shell : MonoBehaviour
{
  private Transform m_Transform;
  private Rigidbody m_Rigidbody;
  private AudioSource m_Audio;
  public float LifeTime = 10f;
  protected float m_RemoveTime;
  public float m_Persistence = 1f;
  protected RestAngleFunc m_RestAngleFunc;
  protected float m_RestTime;
  public List<AudioClip> m_BounceSounds = new List<AudioClip>();

  private void Awake()
  {
    m_Transform = transform;
    m_Rigidbody = GetComponent<Rigidbody>();
    m_Audio = GetComponent<AudioSource>();
    m_Audio.playOnAwake = false;
    m_Audio.dopplerLevel = 0.0f;
  }

  private void OnEnable()
  {
    m_RestAngleFunc = null;
    m_RemoveTime = Time.time + LifeTime;
    m_RestTime = Time.time + LifeTime * 0.25f;
    m_Rigidbody.maxAngularVelocity = 100f;
    m_Rigidbody.velocity = Vector3.zero;
    m_Rigidbody.angularVelocity = Vector3.zero;
    m_Rigidbody.constraints = RigidbodyConstraints.None;
    GetComponent<Collider>().enabled = true;
  }

  private void Update()
  {
    if (m_RestAngleFunc == null)
    {
      if (Time.time > (double) m_RestTime)
        DecideRestAngle();
    }
    else
      m_RestAngleFunc();
    if (Time.time <= (double) m_RemoveTime)
      return;
    m_Transform.localScale = Vector3.Lerp(m_Transform.localScale, Vector3.zero, (float) (Time.deltaTime * 60.0 * 0.20000000298023224));
    if (Time.time <= m_RemoveTime + 0.5)
      return;
    vp_Utility.Destroy(gameObject);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.relativeVelocity.magnitude > 2.0)
    {
      if (Random.value > 0.5)
        m_Rigidbody.AddRelativeTorque(-Random.rotation.eulerAngles * 0.15f);
      else
        m_Rigidbody.AddRelativeTorque(Random.rotation.eulerAngles * 0.15f);
      if (!(m_Audio != null) || m_BounceSounds.Count <= 0)
        return;
      m_Audio.pitch = Time.timeScale;
      m_Audio.PlayOneShot(m_BounceSounds[Random.Range(0, m_BounceSounds.Count)]);
    }
    else
    {
      if (Random.value <= (double) m_Persistence)
        return;
      GetComponent<Collider>().enabled = false;
      m_RemoveTime = Time.time + 0.5f;
    }
  }

  protected void DecideRestAngle()
  {
    if (Mathf.Abs(m_Transform.eulerAngles.x - 270f) < 55.0)
    {
      RaycastHit hitInfo;
      if (!Physics.Raycast(new Ray(m_Transform.position, Vector3.down), out hitInfo, 1f) || !(hitInfo.normal == Vector3.up))
        return;
      m_RestAngleFunc = UpRight;
      m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    else
      m_RestAngleFunc = TippedOver;
  }

  protected void UpRight() => m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, Quaternion.Euler(-90f, m_Transform.rotation.y, m_Transform.rotation.z), Time.time * (float) (Time.deltaTime * 60.0 * 0.05000000074505806));

  protected void TippedOver() => m_Transform.localRotation = Quaternion.Lerp(m_Transform.localRotation, Quaternion.Euler(0.0f, m_Transform.localEulerAngles.y, m_Transform.localEulerAngles.z), Time.time * (float) (Time.deltaTime * 60.0 * 0.004999999888241291));

  public delegate void RestAngleFunc();
}
