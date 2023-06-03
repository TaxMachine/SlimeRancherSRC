// Decompiled with JetBrains decompiler
// Type: SlimeAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class SlimeAudio : SRBehaviour, ActorModel.Participant
{
  public SlimeSounds slimeSounds;
  private SECTR_PointSource source;
  private SlimeModel slimeModel;

  public void Awake() => source = GetComponent<SECTR_PointSource>();

  public void Play(SECTR_AudioCue cue)
  {
    if (!(cue != null) || slimeModel != null && slimeModel.isFeral && slimeSounds.SuppressIfFeral(cue))
      return;
    source.Cue = cue;
    source.Play();
  }

  public void InitModel(ActorModel model)
  {
  }

  public void SetModel(ActorModel model) => slimeModel = model as SlimeModel;
}
