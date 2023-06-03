// Decompiled with JetBrains decompiler
// Type: TeleportSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSource : SRBehaviour
{
  public GameObject activationBlocker;
  [Tooltip("Required progress to activate the teleporter.")]
  public ProgressDirector.ProgressType activationProgress = ProgressDirector.ProgressType.NONE;
  [Tooltip("Progresses that are set when the teleporter becomes active.")]
  public ProgressDirector.ProgressType[] setProgressTypesOnActivate;
  [Tooltip("QuicksilverEnergyGenerator that must not be active to use the teleporter. (optional)")]
  public QuicksilverEnergyGenerator blockingGenerator;
  public GameObject departFX;
  public GameObject activeFX;
  public string destinationSetName;
  [HideInInspector]
  public bool waitForExternalActivation;
  [HideInInspector]
  public bool waitForTriggerExit;
  private TeleportNetwork network;
  private bool activated;

  public virtual void Awake() => network = SRSingleton<SceneContext>.Instance.TeleportNetwork;

  public void OnDisable() => waitForTriggerExit = false;

  public void OnTriggerEnter(Collider collider)
  {
    if (!network.IsLinkFullyActive(this) || !PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    TeleportablePlayer component = collider.gameObject.GetComponent<TeleportablePlayer>();
    if (!(component != null))
      return;
    network.TeleportToDestination(component, this, destinationSetName, PickDestination);
  }

  public void OnTriggerExit(Collider collider)
  {
    if (!PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    waitForTriggerExit = false;
  }

  public virtual void OnDepart()
  {
    if (!(departFX != null))
      return;
    Instantiate(departFX, transform.position, transform.rotation);
  }

  public void Update()
  {
    bool activated = this.activated;
    this.activated = network.IsLinkFullyActive(this);
    if (this.activated && !activated && setProgressTypesOnActivate != null)
    {
      foreach (ProgressDirector.ProgressType type in setProgressTypesOnActivate)
        SRSingleton<SceneContext>.Instance.ProgressDirector.SetProgress(type, 1);
    }
    if (!(activeFX != null))
      return;
    activeFX.SetActive(this.activated);
  }

  public void ExternalActivate() => waitForExternalActivation = false;

  public virtual bool IsLinkActive() => !waitForTriggerExit && !waitForExternalActivation && (!(bool) (UnityEngine.Object) activationBlocker || !activationBlocker.activeSelf) && (activationProgress == ProgressDirector.ProgressType.NONE || SRSingleton<SceneContext>.Instance.ProgressDirector.HasProgress(activationProgress)) && (!(blockingGenerator != null) || blockingGenerator.GetState() != QuicksilverEnergyGenerator.State.ACTIVE && blockingGenerator.GetState() != QuicksilverEnergyGenerator.State.COUNTDOWN);

  protected virtual TeleportDestination PickDestination(List<TeleportDestination> destinations) => Randoms.SHARED.Pick(destinations, null);
}
