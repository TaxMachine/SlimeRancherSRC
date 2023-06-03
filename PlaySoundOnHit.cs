// Decompiled with JetBrains decompiler
// Type: PlaySoundOnHit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour, ControllerCollisionListener
{
  [Tooltip("The audio cue to play on hit")]
  public SECTR_AudioCue hitCue;
  [Tooltip("Minimum time between playing sound, in seconds.")]
  public float minTimeBetween = 1f;
  [Tooltip("Minimum force to trigger the sound.")]
  public float minForce;
  [Tooltip("Whether we should count controller collisions for whether we play the hit.")]
  public bool includeControllerCollisions;
  private float nextTime;

  public void OnCollisionEnter(Collision col)
  {
    if (col.impulse.sqrMagnitude < minForce * (double) minForce)
      return;
    MaybePlaySound();
  }

  public void OnControllerCollision(GameObject gameObj)
  {
    if (!includeControllerCollisions)
      return;
    MaybePlaySound();
  }

  private void MaybePlaySound()
  {
    if (Time.time < (double) nextTime)
      return;
    if (hitCue != null)
      SECTR_AudioSystem.Play(hitCue, transform.position, false);
    nextTime = Time.time + minTimeBetween;
  }
}
