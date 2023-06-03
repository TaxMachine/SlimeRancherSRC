// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchTerminalAudio : SRBehaviour
{
  [Tooltip("Reference to the parent animator.")]
  public GlitchTerminalAnimator animator;
  [Tooltip("Transform of where to play the BOOT_UP state sound.")]
  public Transform onStateBootup;
  [Tooltip("Sound component playing the IDLE state sound.")]
  public PlaySoundOnEnable onStateIdle;

  public void Awake()
  {
    GlitchMetadata metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    onStateIdle.cue = metadata.animationOnTerminalIdleCue;
    onStateIdle.gameObject.SetActive(false);
    animator.onStateEnter += id =>
    {
      switch (id)
      {
        case GlitchTerminalAnimatorState.Id.SLEEP:
          onStateIdle.gameObject.SetActive(false);
          break;
        case GlitchTerminalAnimatorState.Id.BOOT_UP:
          SECTR_AudioSystem.Play(metadata.animationOnTerminalBootupCue, onStateBootup.position, false);
          break;
        case GlitchTerminalAnimatorState.Id.IDLE:
          onStateIdle.gameObject.SetActive(true);
          break;
      }
    };
  }
}
