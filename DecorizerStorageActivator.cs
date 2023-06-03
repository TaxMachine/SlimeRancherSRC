// Decompiled with JetBrains decompiler
// Type: DecorizerStorageActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DecorizerStorageActivator : SRBehaviour, TechActivator
{
  private Animator buttonAnimator;
  private int buttonAnimation;
  private const float TIME_BETWEEN_ACTIVATIONS = 0.4f;
  private float nextActivationTime;
  private DecorizerStorage storage;

  public void Awake()
  {
    buttonAnimator = GetComponentInParent<Animator>();
    buttonAnimation = Animator.StringToHash("ButtonPressed");
    storage = GetComponentInParent<DecorizerStorage>();
  }

  public void Activate()
  {
    if (nextActivationTime >= (double) Time.time)
      return;
    nextActivationTime = Time.time + 0.4f;
    buttonAnimator.SetTrigger(buttonAnimation);
    Instantiate(SRSingleton<GameContext>.Instance.UITemplates.decorizerUIPrefab).GetComponent<DecorizerUI>().storage = storage;
  }

  public GameObject GetCustomGuiPrefab() => null;
}
