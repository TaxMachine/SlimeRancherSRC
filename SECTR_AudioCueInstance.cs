// Decompiled with JetBrains decompiler
// Type: SECTR_AudioCueInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public struct SECTR_AudioCueInstance : SECTR_IAudioInstance
{
  private SECTR_IAudioInstance internalInstance;
  private int generation;
  private bool loops;

  public SECTR_AudioCueInstance(SECTR_IAudioInstance internalInstance, int generation, bool loops)
  {
    this.internalInstance = internalInstance;
    this.generation = generation;
    this.loops = loops;
  }

  public bool Loops => loops;

  public int Generation => generation;

  public bool Active => internalInstance != null && generation == internalInstance.Generation && internalInstance.Active;

  public Vector3 Position
  {
    get => !Active ? Vector3.zero : internalInstance.Position;
    set
    {
      if (!Active)
        return;
      internalInstance.Position = value;
    }
  }

  public Vector3 LocalPosition
  {
    get => !Active ? Vector3.zero : internalInstance.LocalPosition;
    set
    {
      if (!Active)
        return;
      internalInstance.LocalPosition = value;
    }
  }

  public float Volume
  {
    get => !Active ? 0.0f : internalInstance.Volume;
    set
    {
      if (!Active)
        return;
      internalInstance.Volume = value;
    }
  }

  public float Pitch
  {
    get => !Active ? 1f : internalInstance.Pitch;
    set
    {
      if (!Active)
        return;
      internalInstance.Pitch = value;
    }
  }

  public bool Mute
  {
    get => Active && internalInstance.Mute;
    set
    {
      if (!Active)
        return;
      internalInstance.Mute = value;
    }
  }

  public float TimeSeconds
  {
    get => !Active ? 0.0f : internalInstance.TimeSeconds;
    set
    {
      if (!Active)
        return;
      internalInstance.TimeSeconds = value;
    }
  }

  public int TimeSamples
  {
    get => !Active ? 0 : internalInstance.TimeSamples;
    set
    {
      if (!Active)
        return;
      internalInstance.TimeSamples = value;
    }
  }

  public void Stop(bool stopImmediately)
  {
    if (!Active)
      return;
    internalInstance.Stop(stopImmediately);
  }

  public void Pause(bool paused)
  {
    if (!Active)
      return;
    internalInstance.Pause(paused);
  }

  public void ForceInfinite()
  {
    if (!Active)
      return;
    internalInstance.ForceInfinite();
  }

  public void ForceOcclusion(bool occluded)
  {
    if (!Active)
      return;
    internalInstance.ForceOcclusion(occluded);
  }

  public void SkipFadeIn()
  {
    if (!Active)
      return;
    internalInstance.SkipFadeIn();
  }

  public SECTR_IAudioInstance GetInternalInstance() => internalInstance;

  public static implicit operator bool(SECTR_AudioCueInstance x) => x.Active;
}
