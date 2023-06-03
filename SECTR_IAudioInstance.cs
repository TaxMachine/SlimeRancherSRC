// Decompiled with JetBrains decompiler
// Type: SECTR_IAudioInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public interface SECTR_IAudioInstance
{
  int Generation { get; }

  bool Active { get; }

  Vector3 Position { get; set; }

  Vector3 LocalPosition { get; set; }

  float Volume { get; set; }

  float Pitch { get; set; }

  bool Mute { get; set; }

  int TimeSamples { get; set; }

  float TimeSeconds { get; set; }

  void Stop(bool stopImmediately);

  void ForceInfinite();

  void ForceOcclusion(bool occluded);

  void SkipFadeIn();

  void Pause(bool paused);
}
