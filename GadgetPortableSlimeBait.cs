// Decompiled with JetBrains decompiler
// Type: GadgetPortableSlimeBait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GadgetPortableSlimeBait : SRBehaviour
{
  [Tooltip("SFX played when the slime bait is hit.")]
  public SECTR_AudioCue onHitCue;
  private float nextHitTime;

  public void OnHit(Transform onHitTransform)
  {
    if (Time.time < (double) nextHitTime)
      return;
    SECTR_AudioSystem.Play(onHitCue, onHitTransform.position, false);
    nextHitTime = Time.time + 1f;
  }
}
