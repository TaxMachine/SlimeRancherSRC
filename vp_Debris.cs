// Decompiled with JetBrains decompiler
// Type: vp_Debris
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class vp_Debris : MonoBehaviour
{
  public float Radius = 2f;
  public float Force = 10f;
  public float UpForce = 1f;
  private AudioSource m_Audio;
  public List<AudioClip> Sounds = new List<AudioClip>();
  public float SoundMinPitch = 0.8f;
  public float SoundMaxPitch = 1.2f;
  public float LifeTime = 5f;
  protected bool m_Destroy;
  protected Collider[] m_Colliders;
  protected Dictionary<Collider, Dictionary<string, object>> m_PiecesInitial = new Dictionary<Collider, Dictionary<string, object>>();

  private void Awake()
  {
    m_Audio = GetComponent<AudioSource>();
    m_Colliders = GetComponentsInChildren<Collider>();
    foreach (Collider collider in m_Colliders)
    {
      if ((bool) (Object) collider.GetComponent<Rigidbody>())
        m_PiecesInitial.Add(collider, new Dictionary<string, object>()
        {
          {
            "Position",
            collider.transform.localPosition
          },
          {
            "Rotation",
            collider.transform.localRotation
          }
        });
    }
  }

  private void OnEnable()
  {
    m_Destroy = false;
    m_Audio.playOnAwake = true;
    foreach (Collider collider in m_Colliders)
    {
      Rigidbody component = collider.GetComponent<Rigidbody>();
      if (component != null)
      {
        collider.transform.localPosition = (Vector3) m_PiecesInitial[collider]["Position"];
        collider.transform.localRotation = (Quaternion) m_PiecesInitial[collider]["Rotation"];
        component.velocity = Vector3.zero;
        component.angularVelocity = Vector3.zero;
        component.AddExplosionForce(Force / Time.timeScale / vp_TimeUtility.AdjustedTimeScale, transform.position, Radius, UpForce);
        Collider c = collider;
        vp_Timer.In(Random.Range(LifeTime * 0.5f, LifeTime * 0.95f), () =>
        {
          if (!(c != null))
            return;
          vp_Utility.Destroy(c.gameObject);
        });
      }
    }
    vp_Timer.In(LifeTime, () => m_Destroy = true);
    if (Sounds.Count <= 0)
      return;
    m_Audio.rolloffMode = AudioRolloffMode.Linear;
    m_Audio.clip = Sounds[Random.Range(0, Sounds.Count)];
    m_Audio.pitch = Random.Range(SoundMinPitch, SoundMaxPitch) * Time.timeScale;
    m_Audio.Play();
  }

  private void Update()
  {
    if (!m_Destroy || GetComponent<AudioSource>().isPlaying)
      return;
    vp_Utility.Destroy(gameObject);
  }
}
