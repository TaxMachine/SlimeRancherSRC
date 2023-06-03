// Decompiled with JetBrains decompiler
// Type: SECTR_AudioAmbience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SECTR_AudioAmbience
{
  [SECTR_ToolTip("The looping 2D cue to play as long as this ambience is active.", null, false)]
  public SECTR_AudioCue BackgroundLoop;
  [SECTR_ToolTip("A list of one-shots that will play randomly around the listener.")]
  public List<SECTR_AudioCue> OneShots = new List<SECTR_AudioCue>();
  [SECTR_ToolTip("The min and max time between one-shot playback.", "OneShots")]
  public Vector2 OneShotInterval = new Vector2(30f, 60f);
  [SECTR_ToolTip("The a volume scalar for the Cues in this Ambience. Combines with the base Cue volume.")]
  public float Volume = 1f;
}
