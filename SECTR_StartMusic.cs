// Decompiled with JetBrains decompiler
// Type: SECTR_StartMusic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("SECTR/Audio/SECTR Start Music")]
public class SECTR_StartMusic : MonoBehaviour
{
  [SECTR_ToolTip("The music to play on Start.")]
  public SECTR_AudioCue Cue;

  private void Start()
  {
    SECTR_AudioSystem.PlayMusic(Cue);
    Destroyer.Destroy(this, "SECTR_StartMusic.Start");
  }
}
