// Decompiled with JetBrains decompiler
// Type: FeederSpeedSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FeederSpeedSelector : MonoBehaviour, TechActivator
{
  public SlimeFeeder feeder;
  public SECTR_AudioCue pressButtonCueSlow;
  public SECTR_AudioCue pressButtonCueNormal;
  public SECTR_AudioCue pressButtonCueFast;
  private Animator buttonAnimator;
  private int buttonPressedTriggerId;
  private const float TIME_BETWEEN_ACTIVATIONS = 0.4f;
  private float nextAllowedActivationTime;

  public void Awake()
  {
    buttonAnimator = GetComponentInParent<Animator>();
    buttonPressedTriggerId = Animator.StringToHash("ButtonPressed");
  }

  public void Activate()
  {
    if (nextAllowedActivationTime > (double) Time.time)
      return;
    feeder.StepFeederSpeed();
    if (buttonAnimator != null)
      buttonAnimator.SetTrigger(buttonPressedTriggerId);
    SECTR_AudioCue pressButtonCueNormal = this.pressButtonCueNormal;
    SECTR_AudioCue audioCue;
    switch (feeder.GetFeedingCycleSpeed())
    {
      case SlimeFeeder.FeedSpeed.Normal:
        audioCue = this.pressButtonCueNormal;
        break;
      case SlimeFeeder.FeedSpeed.Slow:
        audioCue = pressButtonCueSlow;
        break;
      case SlimeFeeder.FeedSpeed.Fast:
        audioCue = pressButtonCueFast;
        break;
      default:
        audioCue = this.pressButtonCueNormal;
        Log.Error("Invalid feeder speed.");
        break;
    }
    if (audioCue != null)
      SECTR_AudioSystem.Play(audioCue, transform.position, false);
    nextAllowedActivationTime = Time.time + 0.4f;
  }

  public GameObject GetCustomGuiPrefab() => null;
}
