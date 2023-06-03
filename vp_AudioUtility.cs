// Decompiled with JetBrains decompiler
// Type: vp_AudioUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public static class vp_AudioUtility
{
  public static void PlayRandomSound(
    AudioSource audioSource,
    List<AudioClip> sounds,
    Vector2 pitchRange)
  {
    if (audioSource == null || sounds == null || sounds.Count == 0)
      return;
    AudioClip sound = sounds[Random.Range(0, sounds.Count)];
    if (sound == null)
      return;
    audioSource.pitch = !(pitchRange == Vector2.one) ? Random.Range(pitchRange.x, pitchRange.y) * Time.timeScale : Time.timeScale;
    audioSource.PlayOneShot(sound);
  }

  public static void PlayRandomSound(AudioSource audioSource, List<AudioClip> sounds) => PlayRandomSound(audioSource, sounds, Vector2.one);
}
