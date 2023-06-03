// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimator_Player
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

public class GlitchTerminalAnimator_Player : SRAnimator
{
  public const string TRIGGER_ENTER_SLIMULATION = "trigger_enter_slimulation";
  public const string TRIGGER_EXIT_SLIMULATION = "trigger_exit_slimulation";

  private event OnStateChanged onStateExit;

  public IEnumerator WaitForStateExit(GlitchTerminalAnimator_PlayerState.Id id)
  {
    bool wasTriggered = false;
    OnStateChanged listener = otherid => wasTriggered |= id == otherid;
    onStateExit += listener;
    yield return new WaitUntil(() => wasTriggered);
    onStateExit -= listener;
  }

  public void OnStateExit(GlitchTerminalAnimator_PlayerState.Id id)
  {
    if (onStateExit == null)
      return;
    onStateExit(id);
  }

  private event OnAnimationEventListener onAnimationEvent;

  public IEnumerator WaitForAnimationEvent(
    AnimationEvent eventId)
  {
    bool wasTriggered = false;
    OnAnimationEventListener listener = otherid => wasTriggered |= eventId == otherid;
    onAnimationEvent += listener;
    yield return new WaitUntil(() => wasTriggered);
    onAnimationEvent -= listener;
  }

  public void OnAnimationEvent(
    AnimationEvent eventId)
  {
    if (onAnimationEvent == null)
      return;
    onAnimationEvent(eventId);
  }

  private delegate void OnStateChanged(GlitchTerminalAnimator_PlayerState.Id id);

  public enum AnimationEvent
  {
    ENTERING_FULLY_COVERED,
  }

  private delegate void OnAnimationEventListener(
    AnimationEvent eventId);
}
