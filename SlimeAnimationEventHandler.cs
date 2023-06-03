// Decompiled with JetBrains decompiler
// Type: SlimeAnimationEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeAnimationEventHandler : MonoBehaviour
{
  private SlimeFaceAnimator sfAnimator;

  public void Awake() => sfAnimator = GetComponent<SlimeFaceAnimator>();

  public void TriggerAweFace() => TriggerFace("triggerAwe");

  public void TriggerAlarmFace() => TriggerFace("triggerAlarm");

  public void TriggerWinceFace() => TriggerFace("triggerWince");

  public void TriggerMinorWinceFace() => TriggerFace("triggerMinorWince");

  public void TriggerAttackTelegraphFace() => TriggerFace("triggerAttackTelegraph");

  public void TriggerChompOpenFace() => TriggerFace("triggerChompOpen");

  public void TriggerChompOpenQuickFace() => TriggerFace("triggerChompOpenQuick");

  public void TriggerChompClosedFace() => TriggerFace("triggerChompClosed");

  public void TriggerInvokeFace() => TriggerFace("triggerConcentrate");

  public void TriggerGrimaceFace() => TriggerFace("triggerGrimace");

  public void TriggerFriedFace() => TriggerFace("triggerFried");

  private void TriggerFace(string faceTrigger) => sfAnimator.SetTrigger(faceTrigger);
}
