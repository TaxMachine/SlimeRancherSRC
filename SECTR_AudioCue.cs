// Decompiled with JetBrains decompiler
// Type: SECTR_AudioCue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SECTR_AudioCue : ScriptableObject
{
  [SerializeField]
  [HideInInspector]
  private SECTR_AudioCue template;
  [SerializeField]
  [HideInInspector]
  private SECTR_AudioBus bus;
  private int clipPlaybackIndex = -1;
  private bool needsShuffling = true;
  private bool pingPongIncrement = true;
  [SECTR_ToolTip("List of Audio Clips for this Cue to choose from.")]
  public List<ClipData> AudioClips = new List<ClipData>();
  [SECTR_ToolTip("The rules for selecting which audio clip to play next")]
  public PlaybackModes PlaybackMode;
  [SECTR_ToolTip("Determines if the sound should be mixed in HDR or LDR.")]
  public bool HDR;
  [SECTR_ToolTip("The loudness, in dB(SPL), of this HDR Cue.")]
  public Vector2 Loudness = new Vector2(50f, 50f);
  [SECTR_ToolTip("The volume of this Cue.")]
  public Vector2 Volume = Vector2.one;
  [SECTR_ToolTip("The pitch adjustment of this Cue.")]
  public Vector2 Pitch = Vector2.one;
  [SECTR_ToolTip("Set to true to auto-loop this Cue.")]
  public bool Loops;
  [SECTR_ToolTip("Cue priority, lower is more important.", 0.0f, 255f)]
  public int Priority = 128;
  [SECTR_ToolTip("Chance cue will play at all.", 0.0f, 1f)]
  public float ChanceToPlay = 1f;
  [SECTR_ToolTip("Prevent this Cue from recieving Audio Effects.")]
  public bool BypassEffects;
  [SECTR_ToolTip("Maximum number of instances of this Cue that can be played at once.", 1f, -1f)]
  public int MaxInstances = 10;
  [SECTR_ToolTip("Number of seconds over which to fade in the Cue when played.", 0.0f, -1f)]
  public float FadeInTime;
  [SECTR_ToolTip("Number of seconds over which to fade out the Cue when stopped.", 0.0f, -1f)]
  public float FadeOutTime;
  [SECTR_ToolTip("Sets rules for how to spatialize this sound.")]
  public Spatializations Spatialization = Spatializations.Local3D;
  [SECTR_ToolTip("Expands or narrows the range of speakers out of which this Cue plays.", 0.0f, 360f)]
  public float Spread;
  [SECTR_ToolTip("Moves the sound around the speaker field.", -1f, 1f)]
  public float Pan2D;
  [SECTR_ToolTip("Attenuation style of this clip.")]
  public FalloffTypes Falloff;
  [SECTR_ToolTip("The range at which the sound is no longer audible.", 0.0f, -1f)]
  public float MaxDistance = 100f;
  [SECTR_ToolTip("The range within which the sound will be at peak volume/loudness.", 0.0f, -1f)]
  public float MinDistance = 10f;
  [SECTR_ToolTip("Scales the amount of doppler effect applied to this Cue.", 0.0f, 1f)]
  public float DopplerLevel;
  [SECTR_ToolTip("Prevents too many instances of a cue playing near one another.", 0.0f, -1f)]
  public int ProximityLimit;
  [SECTR_ToolTip("The size of the proximity limit check.", "ProximityLimit", 0.0f, -1f)]
  public float ProximityRange = 10f;
  [SECTR_ToolTip("Allows you to scale down the amount of occlusion applied to this Cue (when occluded).", 0.0f, 1f)]
  public float OcclusionScale = 1f;
  [SECTR_ToolTip("The chance that this cue will actually make a sound when played.", 0.0f, 1f)]
  public float PlayProbability = 1f;
  [SECTR_ToolTip("Random delay before start of playback.")]
  public Vector2 Delay = Vector2.zero;

  public SECTR_AudioCue Template
  {
    set
    {
      if (!(template != value) || !(value != this))
        return;
      template = value;
    }
    get => template;
  }

  public SECTR_AudioBus Bus
  {
    set
    {
      if (!(bus != value))
        return;
      bus = value;
    }
    get => bus;
  }

  public SECTR_AudioCue SourceCue => !(template != null) ? this : template;

  public bool Is3D => Spatialization != 0;

  public bool IsLocal => Spatialization == Spatializations.Simple2D || Spatialization == Spatializations.Infinite3D;

  public int ClipIndex => clipPlaybackIndex;

  public ClipData GetNextClip()
  {
    if (UnityEngine.Random.Range(0.0f, 1f) > (double) SourceCue.ChanceToPlay)
      return null;
    int count = AudioClips.Count;
    if (count == 1)
      return AudioClips[0];
    if (count > 0)
    {
      switch (PlaybackMode)
      {
        case PlaybackModes.Random:
          return AudioClips[UnityEngine.Random.Range(0, count)];
        case PlaybackModes.Shuffle:
          ++clipPlaybackIndex;
          if (clipPlaybackIndex >= count)
          {
            clipPlaybackIndex = 0;
            needsShuffling = true;
          }
          if (needsShuffling)
          {
            _ShuffleClips();
            needsShuffling = false;
          }
          return AudioClips[clipPlaybackIndex];
        case PlaybackModes.Loop:
          clipPlaybackIndex = ++clipPlaybackIndex % count;
          return AudioClips[clipPlaybackIndex];
        case PlaybackModes.PingPong:
          if (pingPongIncrement)
          {
            ++clipPlaybackIndex;
            pingPongIncrement = clipPlaybackIndex < AudioClips.Count - 1;
          }
          else
          {
            --clipPlaybackIndex;
            pingPongIncrement = clipPlaybackIndex <= 0;
          }
          return AudioClips[clipPlaybackIndex];
      }
    }
    return null;
  }

  public float MinClipLength()
  {
    float a = float.MaxValue;
    bool flag = false;
    int count = AudioClips.Count;
    for (int index = 0; index < count; ++index)
    {
      AudioClip clip = AudioClips[index].Clip;
      if ((bool) (UnityEngine.Object) clip)
      {
        a = Mathf.Min(a, clip.length);
        flag = true;
      }
    }
    return !flag ? 0.0f : a;
  }

  public float MaxClipLength()
  {
    float a = 0.0f;
    int count = AudioClips.Count;
    for (int index = 0; index < count; ++index)
    {
      AudioClip clip = AudioClips[index].Clip;
      if ((bool) (UnityEngine.Object) clip)
        a = Mathf.Max(a, clip.length);
    }
    return a;
  }

  public void ResetClipIndex()
  {
    needsShuffling = true;
    pingPongIncrement = true;
    clipPlaybackIndex = -1;
  }

  private void OnEnable() => ResetClipIndex();

  private void OnDisable()
  {
  }

  private void _ShuffleClips()
  {
    System.Random random = new System.Random();
    int count = AudioClips.Count;
    while (count >= 1)
    {
      --count;
      int index = random.Next(count + 1);
      ClipData audioClip = AudioClips[index];
      AudioClips[index] = AudioClips[count];
      AudioClips[count] = audioClip;
    }
  }

  public enum PlaybackModes
  {
    Random,
    Shuffle,
    Loop,
    PingPong,
  }

  public enum FalloffTypes
  {
    Linear,
    Logrithmic,
  }

  public enum Spatializations
  {
    Simple2D,
    Infinite3D,
    Local3D,
    Occludable3D,
  }

  [Serializable]
  public class ClipData
  {
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private bool playedInShuffle;
    [SerializeField]
    private float volume = 1f;
    [SerializeField]
    private SECTR_ULong bakeTimestamp;
    public AnimationCurve HDRCurve;

    public ClipData(AudioClip clip)
    {
      this.clip = clip;
      playedInShuffle = false;
      volume = 1f;
    }

    public AudioClip Clip => clip;

    public float Volume
    {
      get => volume;
      set => volume = value;
    }

    public bool PlayedInShuffle
    {
      get => playedInShuffle;
      set => playedInShuffle = value;
    }
  }
}
