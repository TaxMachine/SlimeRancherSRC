// Decompiled with JetBrains decompiler
// Type: SlimeSounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "SlimeSounds", menuName = "Slimes/Slime Sounds")]
public class SlimeSounds : ScriptableObject
{
  public SECTR_AudioCue jumpCue;
  public SECTR_AudioCue bounceCue;
  public SECTR_AudioCue chompCue;
  public SECTR_AudioCue attackCue;
  public SECTR_AudioCue gulpCue;
  public SECTR_AudioCue plortCue;
  public SECTR_AudioCue splatCue;
  public SECTR_AudioCue voiceAlarmCue;
  public SECTR_AudioCue voiceAweCue;
  public SECTR_AudioCue voiceDamageCue;
  public SECTR_AudioCue voiceFearCue;
  public SECTR_AudioCue voiceFunCue;
  public SECTR_AudioCue voiceJumpCue;
  public SECTR_AudioCue voiceSplatCue;
  public SECTR_AudioCue sneezeCue;
  public SECTR_AudioCue rollCue;
  public SECTR_AudioCue stompJumpCue;
  public SECTR_AudioCue stompLandCue;
  public SECTR_AudioCue unferalCue;
  public SECTR_AudioCue wiggleCue;
  public SECTR_AudioCue cloakCue;
  public SECTR_AudioCue decloakCue;
  public SECTR_AudioCue gatherCue;

  public bool SuppressIfFeral(SECTR_AudioCue cue) => cue != null && cue == voiceAlarmCue || cue == voiceAweCue || cue == voiceFunCue || cue == voiceJumpCue || cue == voiceSplatCue;
}
