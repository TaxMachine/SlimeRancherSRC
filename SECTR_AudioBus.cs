// Decompiled with JetBrains decompiler
// Type: SECTR_AudioBus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SECTR_AudioBus : ScriptableObject
{
  [SerializeField]
  [HideInInspector]
  private SECTR_AudioBus parent;
  private List<SECTR_AudioBus> children = new List<SECTR_AudioBus>();
  private float userVolume = 1f;
  private float userPitch = 1f;
  private float effectiveVolume = 1f;
  private float effectivePitch = 1f;
  private bool muted;
  [SECTR_ToolTip("The volume of this bus, between 0 and 1.", 0.0f, 1f)]
  public float Volume = 1f;
  [SECTR_ToolTip("The pitch of this bus, between 0 and 2.", 0.0f, 2f)]
  public float Pitch = 1f;
  private int pauseCount;

  public bool Paused => pauseCount > 0;

  public void Pause(bool paused)
  {
    if (paused)
      ++pauseCount;
    else
      pauseCount = Math.Max(0, pauseCount - 1);
    for (int index = 0; index < children.Count; ++index)
      children[index].Pause(paused);
  }

  public void ResetPauseState()
  {
    pauseCount = 0;
    for (int index = 0; index < children.Count; ++index)
      children[index].ResetPauseState();
  }

  public float UserVolume
  {
    set => userVolume = value;
    get => userVolume;
  }

  public float UserPitch
  {
    set => userPitch = value;
    get => userPitch;
  }

  public bool Muted
  {
    get => muted;
    set => muted = value;
  }

  public float EffectiveVolume
  {
    get => effectiveVolume;
    set => effectiveVolume = muted ? 0.0f : Mathf.Clamp01(Volume * userVolume * value);
  }

  public float EffectivePitch
  {
    get => effectivePitch;
    set => effectivePitch = Mathf.Clamp(Pitch * userPitch * value, 0.0f, 2f);
  }

  public SECTR_AudioBus Parent
  {
    set
    {
      if (!(value != parent) || !(value != this))
        return;
      if ((bool) (UnityEngine.Object) parent)
        parent._RemoveChild(this);
      parent = value;
      if (!(bool) (UnityEngine.Object) parent)
        return;
      parent._AddChild(this);
    }
    get => parent;
  }

  public List<SECTR_AudioBus> Children => children;

  public bool IsAncestorOf(SECTR_AudioBus bus)
  {
    for (SECTR_AudioBus sectrAudioBus = bus; sectrAudioBus != null; sectrAudioBus = sectrAudioBus.Parent)
    {
      if (sectrAudioBus == this)
        return true;
    }
    return false;
  }

  public bool IsDecendentOf(SECTR_AudioBus bus)
  {
    for (SECTR_AudioBus parent = Parent; parent != null; parent = parent.Parent)
    {
      if (parent == bus)
        return true;
    }
    return false;
  }

  public void ResetUserVolume()
  {
    userVolume = 1f;
    int count = children.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_AudioBus child = children[index];
      if ((bool) (UnityEngine.Object) child)
        child.ResetUserVolume();
    }
  }

  private void OnEnable()
  {
    if (!(bool) (UnityEngine.Object) parent)
      return;
    parent._AddChild(this);
  }

  private void OnDisable()
  {
    if (!(bool) (UnityEngine.Object) parent)
      return;
    parent._RemoveChild(this);
  }

  private void _AddChild(SECTR_AudioBus child)
  {
    if (children.Contains(child))
      return;
    children.Add(child);
  }

  private void _RemoveChild(SECTR_AudioBus child) => children.Remove(child);
}
