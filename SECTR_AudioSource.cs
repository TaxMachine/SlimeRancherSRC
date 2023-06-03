// Decompiled with JetBrains decompiler
// Type: SECTR_AudioSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class SECTR_AudioSource : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  protected float volume = 1f;
  [SerializeField]
  [HideInInspector]
  protected float pitch = 1f;
  [SECTR_ToolTip("The Cue to play from this source.", null, false)]
  public SECTR_AudioCue Cue;
  [SECTR_ToolTip("If the Cue should be forced to loop when playing.")]
  public bool Loop = true;
  [SECTR_ToolTip("Should the Cue auto-play when created.")]
  public bool PlayOnStart = true;
  [SECTR_ToolTip("Should looping cues restart on enabled.")]
  public bool RestartLoopsOnEnabled = true;

  public float Volume
  {
    get => volume;
    set
    {
      if (volume == (double) value)
        return;
      volume = Mathf.Clamp01(value);
      OnVolumePitchChanged();
    }
  }

  public float Pitch
  {
    get => pitch;
    set
    {
      if (pitch == (double) value)
        return;
      pitch = Mathf.Clamp(value, 0.0f, 2f);
      OnVolumePitchChanged();
    }
  }

  public abstract bool IsPlaying { get; }

  public abstract void Play();

  public abstract void Stop(bool stopImmediately);

  protected virtual void Start()
  {
    if (!Application.isPlaying || !PlayOnStart)
      return;
    Play();
  }

  protected virtual void OnDisable()
  {
    if (!Application.isPlaying)
      return;
    Stop(true);
  }

  protected virtual void OnEnable()
  {
    if (!Application.isPlaying || !(Cue != null) || !RestartLoopsOnEnabled || !Cue.Loops && !Loop)
      return;
    Play();
  }

  protected abstract void OnVolumePitchChanged();
}
