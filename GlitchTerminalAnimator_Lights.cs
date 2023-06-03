// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimator_Lights
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class GlitchTerminalAnimator_Lights : SRBehaviour
{
  private GlitchTerminalAnimator animator;
  private const float TRANSITION_SPEED = 0.8f;
  private readonly int PROPERTY_COLOR = Shader.PropertyToID("_SpiralColor");
  private const float PROPERTY_COLOR_RED = 0.0f;
  private const float PROPERTY_COLOR_GREEN = 0.5f;
  private readonly int PROPERTY_MULTIPLIER = Shader.PropertyToID("_GlowMultiplier");
  private const float PROPERTY_MULTIPLIER_ON = 1.25f;
  private const float PROPERTY_MULTIPLIER_OFF = 0.0f;
  private State state;
  private Renderer renderer;
  private Tweener multiplierTween;
  private Tweener colorTween;

  public void Awake()
  {
    animator = GetRequiredComponentInParent<GlitchTerminalAnimator>();
    renderer = GetRequiredComponent<Renderer>();
    renderer.sharedMaterial.SetFloat(PROPERTY_COLOR, 0.0f);
    renderer.sharedMaterial.SetFloat(PROPERTY_MULTIPLIER, 0.0f);
  }

  public void Update()
  {
    State currentState = GetCurrentState();
    if (state == currentState)
      return;
    OnStateChanged(state, currentState);
    state = currentState;
  }

  private State GetCurrentState()
  {
    if (!animator.animator.GetBool("state_sleeping"))
    {
      switch (animator.activator.GetLinkState())
      {
        case GlitchTerminalActivator.LinkState.INACTIVE_PROGRESS:
          return State.DISABLED;
        case GlitchTerminalActivator.LinkState.INACTIVE_AMMO:
          return State.ENABLED_RED;
        case GlitchTerminalActivator.LinkState.PRE_ACTIVE:
        case GlitchTerminalActivator.LinkState.ACTIVE:
          return State.ENABLED_GREEN;
      }
    }
    return State.DISABLED;
  }

  private void OnStateChanged(
    State previous,
    State current)
  {
    if (previous == State.DISABLED)
    {
      renderer.sharedMaterial.SetFloat(PROPERTY_COLOR, GetStateColor(current));
      Tweener multiplierTween = this.multiplierTween;
      if (multiplierTween != null)
        multiplierTween.Kill();
      this.multiplierTween = DOTween.To(() => renderer.sharedMaterial.GetFloat(PROPERTY_MULTIPLIER), OnUpdate_PropertyMultiplier, 1.25f, 0.8f).SetSpeedBased().SetEase(Ease.Linear);
    }
    else if (current == State.DISABLED)
    {
      Tweener multiplierTween = this.multiplierTween;
      if (multiplierTween != null)
        multiplierTween.Kill();
      this.multiplierTween = DOTween.To(() => renderer.sharedMaterial.GetFloat(PROPERTY_MULTIPLIER), OnUpdate_PropertyMultiplier, 0.0f, 0.8f).SetSpeedBased().SetEase(Ease.Linear);
    }
    else
    {
      Tweener colorTween = this.colorTween;
      if (colorTween != null)
        colorTween.Kill();
      this.colorTween = DOTween.To(() => renderer.sharedMaterial.GetFloat(PROPERTY_COLOR), OnUpdate_PropertyColor, GetStateColor(current), 0.8f).SetSpeedBased().SetEase(Ease.Linear);
    }
  }

  private void OnUpdate_PropertyMultiplier(float value) => renderer.sharedMaterial.SetFloat(PROPERTY_MULTIPLIER, value);

  private void OnUpdate_PropertyColor(float value) => renderer.sharedMaterial.SetFloat(PROPERTY_COLOR, value);

  private static float GetStateColor(State state)
  {
    if (state == State.ENABLED_RED)
      return 0.0f;
    if (state == State.ENABLED_GREEN)
      return 0.5f;
    throw new ArgumentException();
  }

  private enum State
  {
    DISABLED,
    ENABLED_RED,
    ENABLED_GREEN,
  }
}
