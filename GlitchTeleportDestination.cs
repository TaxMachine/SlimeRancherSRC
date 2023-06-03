// Decompiled with JetBrains decompiler
// Type: GlitchTeleportDestination
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;

public class GlitchTeleportDestination : SRBehaviour, GlitchTeleportDestinationModel.Participant
{
  [Tooltip("Teleport destination transform.")]
  public Transform destinationTransform;
  [Tooltip("FX parent when the destination is activated.")]
  public GameObject exitActiveFX;
  private GlitchTeleportDestinationModel model;
  private TutorialDirector tutorialDirector;
  private TimeDirector timeDirector;

  public event OnExitTeleporterBecameActiveDelegate onExitTeleporterBecameActive;

  public string id => GetRequiredComponent<IdHandler>().id;

  public bool isPotentialExitDestination { get; set; }

  public void Awake()
  {
    tutorialDirector = SRSingleton<SceneContext>.Instance.TutorialDirector;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    isPotentialExitDestination = true;
    exitActiveFX.SetActive(false);
    SRSingleton<SceneContext>.Instance.GameModel.Glitch.Register(this);
  }

  public void InitModel(GlitchTeleportDestinationModel model) => model.activationTime = new double?();

  public void SetModel(GlitchTeleportDestinationModel model)
  {
    this.model = model;
    if (SRSingleton<SceneContext>.Instance.GameModel.GetPlayerModel().currRegionSetId != RegionRegistry.RegionSetId.SLIMULATIONS)
      return;
    Reset(this.model.activationTime);
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.Glitch.Unregister(this);
    timeDirector.RemovePassedTimeDelegate(OnExitTeleporterBecameActive);
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (!IsLinkActive() || !PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    GlitchTerminalAnimator.OnExit(null, null, gameObject.GetInstanceID());
  }

  public bool IsLinkActive() => GetCurrentState() == State.ACTIVATED;

  public void Reset(double? activationTime)
  {
    model.activationTime = activationTime;
    State currentState = GetCurrentState();
    exitActiveFX.SetActive(currentState == State.ACTIVATED);
    int num1 = isPotentialExitDestination ? 1 : 0;
    double num2 = timeDirector.WorldTime();
    double? activationTime1 = model.activationTime;
    double valueOrDefault = activationTime1.GetValueOrDefault();
    int num3 = num2 >= valueOrDefault & activationTime1.HasValue ? 1 : 0;
    isPotentialExitDestination = (num1 | num3) != 0;
    timeDirector.RemovePassedTimeDelegate(OnExitTeleporterBecameActive);
    if (currentState == State.PREACTIVATED)
    {
      timeDirector.AddPassedTimeDelegate(model.activationTime.Value, OnExitTeleporterBecameActive);
    }
    else
    {
      if (currentState != State.ACTIVATED)
        return;
      OnExitTeleporterBecameActive();
    }
  }

  private void OnExitTeleporterBecameActive()
  {
    SRSingleton<GlitchRegionHelper>.Instance.OnExitTeleporterBecameActive();
    exitActiveFX.SetActive(true);
    tutorialDirector.MaybeShowPopup(TutorialDirector.Id.SLIMULATIONS_EXIT_AVAILABLE);
    if (onExitTeleporterBecameActive == null)
      return;
    onExitTeleporterBecameActive(this);
  }

  private State GetCurrentState()
  {
    double num1 = timeDirector.WorldTime();
    double? activationTime1 = model.activationTime;
    double valueOrDefault1 = activationTime1.GetValueOrDefault();
    if (num1 < valueOrDefault1 & activationTime1.HasValue)
      return State.PREACTIVATED;
    double num2 = timeDirector.WorldTime();
    double? activationTime2 = model.activationTime;
    double valueOrDefault2 = activationTime2.GetValueOrDefault();
    return !(num2 >= valueOrDefault2 & activationTime2.HasValue) ? State.DEACTIVATED : State.ACTIVATED;
  }

  public delegate void OnExitTeleporterBecameActiveDelegate(GlitchTeleportDestination destination);

  private enum State
  {
    DEACTIVATED,
    PREACTIVATED,
    ACTIVATED,
  }
}
