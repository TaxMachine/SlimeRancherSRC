// Decompiled with JetBrains decompiler
// Type: vp_Pickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SphereCollider))]
[RequireComponent(typeof (AudioSource))]
public abstract class vp_Pickup : MonoBehaviour
{
  protected Transform m_Transform;
  protected Rigidbody m_Rigidbody;
  protected AudioSource m_Audio;
  public string InventoryName = "Unnamed";
  public List<string> RecipientTags = new List<string>();
  private Collider m_LastCollider;
  private vp_FPPlayerEventHandler m_Recipient;
  public string GiveMessage = "Picked up an item";
  public string FailMessage = "You currently can't pick up this item!";
  protected Vector3 m_SpawnPosition = Vector3.zero;
  protected Vector3 m_SpawnScale = Vector3.zero;
  public bool Billboard;
  public Vector3 Spin = Vector3.zero;
  public float BobAmp;
  public float BobRate;
  public float BobOffset = -1f;
  public Vector3 RigidbodyForce = Vector3.zero;
  public float RigidbodySpin;
  public float RespawnDuration = 10f;
  public float RespawnScaleUpDuration;
  public float RemoveDuration;
  public AudioClip PickupSound;
  public AudioClip PickupFailSound;
  public AudioClip RespawnSound;
  public bool PickupSoundSlomo = true;
  public bool FailSoundSlomo = true;
  public bool RespawnSoundSlomo = true;
  protected bool m_Depleted;
  protected bool m_AlreadyFailed;
  protected vp_Timer.Handle m_RespawnTimer = new vp_Timer.Handle();
  private Transform m_CameraMainTransform;

  protected virtual void Start()
  {
    m_Transform = transform;
    m_Rigidbody = GetComponent<Rigidbody>();
    m_Audio = GetComponent<AudioSource>();
    if (Camera.main != null)
      m_CameraMainTransform = Camera.main.transform;
    GetComponent<Collider>().isTrigger = true;
    m_Audio.clip = PickupSound;
    m_Audio.playOnAwake = false;
    m_Audio.minDistance = 3f;
    m_Audio.maxDistance = 150f;
    m_Audio.rolloffMode = AudioRolloffMode.Linear;
    m_Audio.dopplerLevel = 0.0f;
    m_SpawnPosition = m_Transform.position;
    m_SpawnScale = m_Transform.localScale;
    RespawnScaleUpDuration = m_Rigidbody == null ? Mathf.Abs(RespawnScaleUpDuration) : 0.0f;
    if (BobOffset == -1.0)
      BobOffset = Random.value;
    if (RecipientTags.Count == 0)
      RecipientTags.Add("Player");
    if (RemoveDuration != 0.0)
      vp_Timer.In(RemoveDuration, Remove);
    if (!(m_Rigidbody != null))
      return;
    if (RigidbodyForce != Vector3.zero)
      m_Rigidbody.AddForce(RigidbodyForce, ForceMode.Impulse);
    if (RigidbodySpin == 0.0)
      return;
    m_Rigidbody.AddTorque(Random.rotation.eulerAngles * RigidbodySpin);
  }

  protected virtual void Update()
  {
    UpdateMotion();
    if (m_Depleted && !m_Audio.isPlaying)
      Remove();
    if (m_Depleted || !(m_Rigidbody != null) || !m_Rigidbody.IsSleeping() || m_Rigidbody.isKinematic)
      return;
    m_Rigidbody.isKinematic = true;
    foreach (Collider component in GetComponents<Collider>())
    {
      if (!component.isTrigger)
        component.enabled = false;
    }
  }

  protected virtual void UpdateMotion()
  {
    if (m_Rigidbody != null)
      return;
    if (Billboard)
    {
      if (m_CameraMainTransform != null)
        m_Transform.localEulerAngles = m_CameraMainTransform.eulerAngles;
    }
    else
      m_Transform.localEulerAngles += Spin * Time.deltaTime;
    if (BobRate != 0.0 && BobAmp != 0.0)
      m_Transform.position = m_SpawnPosition + Vector3.up * (Mathf.Cos((float) ((Time.time + (double) BobOffset) * (BobRate * 10.0))) * BobAmp);
    if (!(m_Transform.localScale != m_SpawnScale))
      return;
    m_Transform.localScale = Vector3.Lerp(m_Transform.localScale, m_SpawnScale, Time.deltaTime / RespawnScaleUpDuration);
  }

  protected virtual void OnTriggerEnter(Collider col)
  {
    if (m_Depleted)
      return;
    using (List<string>.Enumerator enumerator = RecipientTags.GetEnumerator())
    {
      string current;
      do
      {
        if (enumerator.MoveNext())
          current = enumerator.Current;
        else
          goto label_1;
      }
      while (!(col.gameObject.tag == current));
      goto label_9;
label_1:
      return;
    }
label_9:
    if (col != m_LastCollider)
      m_Recipient = col.gameObject.GetComponent<vp_FPPlayerEventHandler>();
    if (m_Recipient == null)
      return;
    if (TryGive(m_Recipient))
    {
      m_Audio.pitch = PickupSoundSlomo ? Time.timeScale : 1f;
      m_Audio.Play();
      GetComponent<Renderer>().enabled = false;
      m_Depleted = true;
      m_Recipient.HUDText.Send(GiveMessage);
    }
    else
    {
      if (m_AlreadyFailed)
        return;
      m_Audio.pitch = FailSoundSlomo ? Time.timeScale : 1f;
      m_Audio.PlayOneShot(PickupFailSound);
      m_AlreadyFailed = true;
      m_Recipient.HUDText.Send(FailMessage);
    }
  }

  protected virtual void OnTriggerExit(Collider col) => m_AlreadyFailed = false;

  protected virtual bool TryGive(vp_FPPlayerEventHandler player) => player.AddItem.Try(new object[2]
  {
    InventoryName,
    1
  });

  protected virtual void Remove()
  {
    if (this == null)
      return;
    if (RespawnDuration == 0.0)
    {
      vp_Utility.Destroy(gameObject);
    }
    else
    {
      if (m_RespawnTimer.Active)
        return;
      vp_Utility.Activate(gameObject, false);
      vp_Timer.In(RespawnDuration, Respawn, m_RespawnTimer);
    }
  }

  protected virtual void Respawn()
  {
    if (m_Transform == null)
      return;
    if (Camera.main != null)
      m_CameraMainTransform = Camera.main.transform;
    m_RespawnTimer.Cancel();
    m_Transform.position = m_SpawnPosition;
    if (m_Rigidbody == null && RespawnScaleUpDuration > 0.0)
      m_Transform.localScale = Vector3.zero;
    GetComponent<Renderer>().enabled = true;
    vp_Utility.Activate(gameObject);
    m_Audio.pitch = RespawnSoundSlomo ? Time.timeScale : 1f;
    m_Audio.PlayOneShot(RespawnSound);
    m_Depleted = false;
    if (BobOffset == -1.0)
      BobOffset = Random.value;
    if (!(m_Rigidbody != null))
      return;
    m_Rigidbody.isKinematic = false;
    foreach (Collider component in GetComponents<Collider>())
    {
      if (!component.isTrigger)
        component.enabled = true;
    }
  }
}
