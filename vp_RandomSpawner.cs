// Decompiled with JetBrains decompiler
// Type: vp_RandomSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class vp_RandomSpawner : MonoBehaviour
{
  private AudioSource m_Audio;
  public AudioClip Sound;
  public float SoundMinPitch = 0.8f;
  public float SoundMaxPitch = 1.2f;
  public bool RandomAngle = true;
  public List<GameObject> SpawnObjects;

  private void Awake()
  {
    if (SpawnObjects == null)
      return;
    int index = Random.Range(0, SpawnObjects.Count);
    if (SpawnObjects[index] == null)
      return;
    ((GameObject) vp_Utility.Instantiate(SpawnObjects[index], transform.position, transform.rotation)).transform.Rotate(Random.rotation.eulerAngles);
    m_Audio = GetComponent<AudioSource>();
    m_Audio.playOnAwake = true;
    if (!(Sound != null))
      return;
    m_Audio.rolloffMode = AudioRolloffMode.Linear;
    m_Audio.clip = Sound;
    m_Audio.pitch = Random.Range(SoundMinPitch, SoundMaxPitch) * Time.timeScale;
    m_Audio.Play();
  }
}
