// Decompiled with JetBrains decompiler
// Type: SECTR_DoorAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("SECTR/Audio/SECTR Door Audio")]
public class SECTR_DoorAudio : MonoBehaviour
{
  private SECTR_AudioCueInstance instance;
  [SECTR_ToolTip("Sound to play while door is in Open state.", null, false)]
  public SECTR_AudioCue OpenLoopCue;
  [SECTR_ToolTip("Sound to play while door is in Closed state.", null, false)]
  public SECTR_AudioCue ClosedLoopCue;
  [SECTR_ToolTip("Sound to play when door starts to open.", null, false)]
  public SECTR_AudioCue OpeningCue;
  [SECTR_ToolTip("Sound to play while door starts to close.", null, false)]
  public SECTR_AudioCue ClosingCue;
  [SECTR_ToolTip("Sound to play while waiting for the door to start opening.", null, false)]
  public SECTR_AudioCue WaitingCue;

  private void OnDisable() => _Stop(true);

  private void OnOpen()
  {
    _Stop(false);
    instance = SECTR_AudioSystem.Play(OpenLoopCue, transform, Vector3.zero, true);
  }

  private void OnOpening()
  {
    _Stop(false);
    instance = SECTR_AudioSystem.Play(OpeningCue, transform, Vector3.zero, false);
  }

  private void OnClose()
  {
    _Stop(false);
    instance = SECTR_AudioSystem.Play(ClosedLoopCue, transform, Vector3.zero, true);
  }

  private void OnClosing()
  {
    _Stop(false);
    instance = SECTR_AudioSystem.Play(ClosingCue, transform, Vector3.zero, false);
  }

  private void OnWaiting()
  {
    _Stop(false);
    instance = SECTR_AudioSystem.Play(WaitingCue, transform, Vector3.zero, true);
  }

  private void _Stop(bool stopImmediately) => instance.Stop(stopImmediately);
}
