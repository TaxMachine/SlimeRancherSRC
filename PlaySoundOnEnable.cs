// Decompiled with JetBrains decompiler
// Type: PlaySoundOnEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
  [Tooltip("SFX played when this object is enabled.")]
  public SECTR_AudioCue cue;
  private SECTR_AudioCueInstance instance;

  public void OnEnable() => instance = SECTR_AudioSystem.Play(cue, transform.position, false);

  public void OnDisable() => instance.Stop(false);
}
