// Decompiled with JetBrains decompiler
// Type: SECTR_MusicTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Music Trigger")]
public class SECTR_MusicTrigger : MonoBehaviour
{
  private Collider activator;
  [SECTR_ToolTip("The Cue to play as music. If null, this trigger will stop the current music.", null, false)]
  public SECTR_AudioCue Cue;
  [SECTR_ToolTip("Should music be forced to loop when playing.")]
  public bool Loop = true;
  [SECTR_ToolTip("Should the music stop when leaving the trigger.")]
  public bool StopOnExit;

  private void OnEnable()
  {
    if (!(bool) (Object) activator)
      return;
    _Play();
  }

  private void OnDisable()
  {
    if (!StopOnExit)
      return;
    _Stop(false);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!(activator == null))
      return;
    if (Cue != null)
      _Play();
    else
      _Stop(false);
    activator = other;
  }

  private void OnTriggerExit(Collider other)
  {
    if (!StopOnExit || !(other == activator))
      return;
    _Stop(false);
    activator = null;
  }

  private void _Play()
  {
    if (!(Cue != null))
      return;
    SECTR_AudioSystem.PlayMusic(Cue);
  }

  private void _Stop(bool stopImmediately) => SECTR_AudioSystem.StopMusic(stopImmediately);
}
