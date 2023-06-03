// Decompiled with JetBrains decompiler
// Type: QuantumVibrationMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class QuantumVibrationMarker : SRBehaviour
{
  private SECTR_PointSource audioSrc;
  public SECTR_AudioCue vibratingCue;

  public void Awake() => audioSrc = gameObject.GetComponent<SECTR_PointSource>();

  public void PlayVibrating() => PlayCue(vibratingCue);

  public void PlayCalm() => PlayCue(null);

  private void PlayCue(SECTR_AudioCue cue)
  {
    audioSrc.Cue = cue;
    if (cue != null)
      audioSrc.Play();
    else
      audioSrc.Stop(false);
  }
}
