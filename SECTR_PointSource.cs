// Decompiled with JetBrains decompiler
// Type: SECTR_PointSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Point Source")]
public class SECTR_PointSource : SECTR_AudioSource
{
  protected SECTR_AudioCueInstance instance;

  public override bool IsPlaying => instance;

  public override void Play()
  {
    if (IsPlaying && instance.Loops)
      instance.Stop(false);
    if (!(Cue != null))
      return;
    instance = Cue.Spatialization != SECTR_AudioCue.Spatializations.Infinite3D ? SECTR_AudioSystem.Play(Cue, transform, Vector3.zero, Loop) : SECTR_AudioSystem.Play(Cue, SECTR_AudioSystem.Listener, Random.onUnitSphere, Loop);
    if (!instance)
      return;
    instance.Volume = volume;
    instance.Pitch = pitch;
  }

  public override void Stop(bool stopImmediately) => instance.Stop(stopImmediately);

  protected override void OnVolumePitchChanged()
  {
    if (!instance)
      return;
    instance.Volume = volume;
    instance.Pitch = pitch;
  }
}
