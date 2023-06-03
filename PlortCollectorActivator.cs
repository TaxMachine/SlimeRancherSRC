// Decompiled with JetBrains decompiler
// Type: PlortCollectorActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlortCollectorActivator : MonoBehaviour, TechActivator
{
  public PlortCollector collector;
  public SECTR_AudioCue pressButtonCue;
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
    collector.StartCollection();
    if (buttonAnimator != null)
      buttonAnimator.SetTrigger(buttonPressedTriggerId);
    if (pressButtonCue != null)
      SECTR_AudioSystem.Play(pressButtonCue, transform.position, false);
    nextAllowedActivationTime = Time.time + 0.4f;
  }

  public GameObject GetCustomGuiPrefab() => null;
}
