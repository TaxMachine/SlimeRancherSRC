// Decompiled with JetBrains decompiler
// Type: StaminaRun
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRun : SRBehaviour, EventHandlerRegistrable, PlayerModel.Participant
{
  public float runningStaminaPerSecond = 30f;
  public float runThreshold = 1f;
  private vp_FPPlayerEventHandler playerEvents;
  private PlayerState playerState;
  private float runStaminaThreshold;
  private vp_FPController controller;
  private TimeDirector timeDirector;
  private PlayerModel model;
  private const float MIN_RUN_VEL = 1f;
  private const float SQR_MIN_RUN_VEL = 1f;

  protected virtual void Awake()
  {
    playerEvents = GetComponent<vp_FPPlayerEventHandler>();
    controller = GetComponent<vp_FPController>();
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
  }

  protected virtual void Start()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    runStaminaThreshold = runThreshold * runningStaminaPerSecond;
  }

  protected virtual void OnEnable()
  {
    if (!(playerEvents != null))
      return;
    Register(playerEvents);
  }

  protected virtual void OnDisable()
  {
    if (!(playerEvents != null))
      return;
    Unregister(playerEvents);
  }

  public void Update()
  {
    if (!playerEvents.Run.Active)
      return;
    bool flag = TooSlow();
    if (timeDirector.HasReached(model.runEnergyDepletionTime) && !flag)
      playerState.SpendEnergy(Time.deltaTime * runningStaminaPerSecond * model.runEfficiency);
    if (CanContinue_Run(1f))
      return;
    playerEvents.Run.TryStop();
  }

  protected virtual bool CanStart_Run() => CanContinue_Run(runStaminaThreshold);

  private bool CanContinue_Run(float threshold) => (!timeDirector.HasReached(model.runEnergyDepletionTime) || playerState.GetCurrEnergy() >= (double) threshold) && controller.Grounded && !TooSlow();

  private bool TooSlow() => playerEvents.Velocity.Get().sqrMagnitude < 1.0;

  public void Register(vp_EventHandler eventHandler) => eventHandler.RegisterActivity("Run", null, null, CanStart_Run, null, null, null);

  public void Unregister(vp_EventHandler eventHandler) => eventHandler.UnregisterActivity("Run", null, null, CanStart_Run, null, null, null);

  public void InitModel(PlayerModel model)
  {
  }

  public void SetModel(PlayerModel model) => this.model = model;

  public void RegionSetChanged(
    RegionRegistry.RegionSetId previous,
    RegionRegistry.RegionSetId current)
  {
  }

  public void TransformChanged(Vector3 pos, Quaternion rot)
  {
  }

  public void RegisteredPotentialAmmoChanged(
    Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo)
  {
  }

  public void KeyAdded()
  {
  }
}
