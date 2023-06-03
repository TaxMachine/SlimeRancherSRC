// Decompiled with JetBrains decompiler
// Type: EnergyJetpack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class EnergyJetpack : SRBehaviour, EventHandlerRegistrable, PlayerModel.Participant
{
  [Tooltip("Energy used per second of jetpacking.")]
  public float energyPerSecond = 33.3f;
  [Tooltip("This much energy is used on initiation of the jetpack.")]
  public float initEnergyUsed = 10f;
  [Tooltip("Must have this many seconds of energy available to start a fresh jetpack burst.")]
  public float startThreshold = 0.45f;
  [Tooltip("Number of seconds of energy available indicating a low-energy threshold.")]
  public float lowEnergyThreshold;
  [Tooltip("The vertical velocity during a jump below which the jetpack kicks in.")]
  public float jetpackVelThreshold = 0.5f;
  [Tooltip("Where to play our jetpack audio cue(s).")]
  public SECTR_PointSource jetpackAudio;
  [Tooltip("The audio to play on jetpack startup.")]
  public SECTR_AudioCue jetpackStartCue;
  [Tooltip("The audio to look during jetpack running.")]
  public SECTR_AudioCue jetpackRunCue;
  [Tooltip("The audio to play on jetpack end.")]
  public SECTR_AudioCue jetpackEndCue;
  [Tooltip("Audio cue to replace `Jetpack Run Cue` during a low-energy state. (optional)")]
  public SECTR_AudioCue jetpackLowEnergyRunCue;
  [Tooltip("Audio cue to play if the jetpack does not successfully start. (optional)")]
  public SECTR_AudioCue jetpackNoEnergyCue;
  private vp_FPPlayerEventHandler playerEvents;
  private PlayerState playerState;
  private float jetpackEnergyThreshold;
  private float jetpackLowEnergyThreshold;
  private vp_FPController controller;
  private TimeDirector timeDir;
  private double canKickInJetpackTime;
  private float hoverY = float.PositiveInfinity;
  private PlayerModel model;
  private const float JUMP_TO_JETPACK_PAUSE = 18f;
  private const float HOVER_HEIGHT = 7f;
  private const float HOVER_SOFTNESS_FUDGE = 2.2f;
  private const float GRAV_PER_Y = 0.1f;
  private const float FALL_COMPENSATION_PER_FRAME = 0.003f;

  protected virtual void Awake()
  {
    playerEvents = GetComponent<vp_FPPlayerEventHandler>();
    controller = GetComponent<vp_FPController>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
  }

  protected virtual void Start()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    jetpackEnergyThreshold = startThreshold * energyPerSecond + initEnergyUsed;
    jetpackLowEnergyThreshold = lowEnergyThreshold * energyPerSecond;
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

  public void FixedUpdate()
  {
    if (playerEvents.Jump.Active && !playerEvents.Jetpack.Active && playerEvents.Velocity.Get().y <= (double) jetpackVelThreshold && timeDir.HasReached(canKickInJetpackTime))
      playerEvents.Jetpack.TryStart();
    if (playerEvents.Jetpack.Active)
    {
      int currEnergy1 = playerState.GetCurrEnergy();
      playerState.SpendEnergy(Time.deltaTime * energyPerSecond * model.jetpackEfficiency);
      int currEnergy2 = playerState.GetCurrEnergy();
      if (currEnergy2 <= 0)
        playerEvents.Jetpack.TryStop();
      else if (jetpackLowEnergyRunCue != null && currEnergy1 > jetpackLowEnergyThreshold * (double) model.jetpackEfficiency && currEnergy2 <= jetpackLowEnergyThreshold * (double) model.jetpackEfficiency)
      {
        jetpackAudio.Cue = jetpackLowEnergyRunCue;
        jetpackAudio.Play();
      }
    }
    if (controller.GroundedNonMountain && !playerEvents.Jump.Active && !playerEvents.Jetpack.Active)
      hoverY = float.PositiveInfinity;
    bool active = playerEvents.Jetpack.Active;
    if (controller.StateEnabled("Jetpack1") != active)
      controller.SetState("Jetpack1", active);
    if (playerEvents.Jetpack.Active)
    {
      float tempGravityModifier = DownwardExtraGravity(playerEvents.transform.position.y, playerEvents.Velocity.Get().y);
      controller.SetTempGravityModifier(tempGravityModifier);
      if (playerEvents.Velocity.Get().y >= 0.0 || tempGravityModifier > 0.0)
        return;
      controller.AdjustFallSpeed((float) (-(double) playerEvents.Velocity.Get().y * (3.0 / 1000.0)));
    }
    else
      controller.SetTempGravityModifier(0.0f);
  }

  protected float DownwardExtraGravity(float y, float yVel)
  {
    float num = y - hoverY;
    return num <= 0.0 ? 0.0f : num * 0.1f * Mathf.Max(0.5f, yVel);
  }

  private bool CanStart_Jetpack()
  {
    if (!model.hasJetpack)
      return false;
    if (playerState.GetCurrEnergy() >= jetpackEnergyThreshold * (double) model.jetpackEfficiency)
      return true;
    jetpackAudio.Cue = jetpackNoEnergyCue;
    jetpackAudio.Play();
    return false;
  }

  private void OnStart_Jump()
  {
    canKickInJetpackTime = timeDir.WorldTime() + 18.0;
    hoverY = Mathf.Min(hoverY, (float) (playerEvents.transform.position.y + 7.0 - 2.2000000476837158));
  }

  private void OnStop_Jump() => canKickInJetpackTime = 0.0;

  private void OnStart_Jetpack()
  {
    hoverY = Mathf.Min(hoverY, (float) (playerEvents.transform.position.y + 7.0 - 2.2000000476837158));
    playerState.SpendEnergy(initEnergyUsed * model.jetpackEfficiency);
    jetpackAudio.Cue = jetpackStartCue;
    jetpackAudio.Play();
    jetpackAudio.Cue = jetpackRunCue;
    jetpackAudio.Play();
  }

  private void OnStop_Jetpack()
  {
    canKickInJetpackTime = timeDir.WorldTime() + 18.0;
    jetpackAudio.Cue = jetpackEndCue;
    jetpackAudio.Play();
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("Jetpack", OnStart_Jetpack, OnStop_Jetpack, CanStart_Jetpack, null, null, null);
    eventHandler.RegisterActivity("Jump", OnStart_Jump, OnStop_Jump, null, null, null, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("Jetpack", OnStart_Jetpack, OnStop_Jetpack, CanStart_Jetpack, null, null, null);
    eventHandler.UnregisterActivity("Jump", OnStart_Jump, OnStop_Jump, null, null, null, null);
  }

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
