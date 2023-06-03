// Decompiled with JetBrains decompiler
// Type: QuicksilverEnergyGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuicksilverEnergyGenerator : 
  IdHandler,
  QuicksilverEnergyGeneratorModel.Participant,
  VacDisplayTimer.TimeSource
{
  [Tooltip("Duration, in game minutes, that the game will countdown until the race begins.")]
  public float countdownMinutes;
  [Tooltip("Duration, in game hours, that the generator will stay active.")]
  public float activeHours;
  [Tooltip("Duration, in game hours, that the generator will cooldown.")]
  public float cooldownHours;
  [Tooltip("FX to display when the generator is inactive. (optional)")]
  public GameObject inactiveFX;
  [Tooltip("FX to display when the generator is active. (optional)")]
  public GameObject activeFX;
  [Tooltip("FX to display when the generator is cooling down. (optional)")]
  public GameObject cooldownFX;
  [Tooltip("CountdownUI prefab.")]
  public GameObject countdownUIPrefab;
  private GameObject countdownUI;
  [Tooltip("SFX played when the generator is ready to be activated. (optional)")]
  public SECTR_AudioCue onInactiveCue;
  [Tooltip("SFX played when the countdown timer begins. (optional)")]
  public SECTR_AudioCue onCountdownCue;
  [Tooltip("SFX played when the cooldown timer begins. (optional)")]
  public SECTR_AudioCue onCooldownCue;
  [Tooltip("SFX played when the cooldown timer begins. (2D, optional)")]
  public SECTR_AudioCue onCooldownCue2D;
  public static List<QuicksilverEnergyGenerator> allGenerators = new List<QuicksilverEnergyGenerator>();
  public OnStateChanged onStateChanged;
  private TimeDirector timeDirector;
  private PlayerDeathHandler deathHandler;
  private TutorialDirector tutDirector;
  private QuicksilverEnergyGeneratorModel model;

  public void Awake()
  {
    tutDirector = SRSingleton<SceneContext>.Instance.TutorialDirector;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterGenerator(id, this);
  }

  public void Start()
  {
    allGenerators.Add(this);
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    deathHandler = SRSingleton<SceneContext>.Instance.Player.GetComponent<PlayerDeathHandler>();
    deathHandler.onPlayerDeath += OnPlayerDeath;
  }

  public void OnDestroy()
  {
    allGenerators.Remove(this);
    if (deathHandler != null)
      deathHandler.onPlayerDeath -= OnPlayerDeath;
    Destroyer.Destroy(countdownUI, "QuicksilverEnergyGenerator.OnDestroy");
  }

  public void InitModel(QuicksilverEnergyGeneratorModel model)
  {
    model.state = State.INACTIVE;
    model.timer = new double?();
  }

  public void SetModel(QuicksilverEnergyGeneratorModel model)
  {
    this.model = model;
    if (this.model.state == State.ACTIVE || this.model.state == State.COUNTDOWN)
    {
      SetState(State.COOLDOWN, false);
    }
    else
    {
      double? timer = model.timer;
      SetState(this.model.state, false);
      this.model.timer = timer;
    }
  }

  public bool Activate()
  {
    if (model.state != State.INACTIVE)
      return false;
    tutDirector.MaybeShowPopup(TutorialDirector.Id.RACE_GENERATOR);
    tutDirector.OnQuicksilverRaceActivated();
    SRSingleton<GameContext>.Instance.MusicDirector.SetValleyRaceMode(true);
    onStateChanged += DisableRaceMusicOnStateChanged;
    SetState(State.COUNTDOWN, true);
    return true;
  }

  private void DisableRaceMusicOnStateChanged()
  {
    if (model.state != State.COOLDOWN && model.state != State.INACTIVE)
      return;
    SRSingleton<GameContext>.Instance.MusicDirector.SetValleyRaceMode(false);
    onStateChanged -= DisableRaceMusicOnStateChanged;
  }

  public bool ExtendActiveDuration(float hours)
  {
    if (model.state != State.ACTIVE)
      return false;
    tutDirector.MaybeShowPopup(TutorialDirector.Id.RACE_CHECKPOINT);
    model.timer = new double?(TimeDirector.HoursFromTime(hours, model.timer.Value));
    return true;
  }

  public void Update()
  {
    if (!model.timer.HasValue || !timeDirector.HasReached(model.timer.Value))
      return;
    SetState((State) ((int) (model.state + 1) % Enum.GetNames(typeof (State)).Length), true);
  }

  public State GetState() => model.state;

  private void SetState(State state, bool enableSFX)
  {
    Destroyer.Destroy(countdownUI, "QuicksilverEnergyGenerator.SetState");
    model.state = state;
    if (model.state == State.COUNTDOWN)
    {
      model.timer = new double?(timeDirector.HoursFromNow(countdownMinutes * 0.0166666675f));
      if (enableSFX)
        SECTR_AudioSystem.Play(onCountdownCue, transform.position, false);
      SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>().GetComponentInChildren<VacDisplayTimer>().SetQuicksilverEnergyGenerator(this);
      countdownUI = Instantiate(countdownUIPrefab);
      countdownUI.GetComponent<HUDCountdownUI>().SetCountdownTime(countdownMinutes);
    }
    else if (model.state == State.ACTIVE)
      model.timer = new double?(timeDirector.HoursFromNow(activeHours));
    else if (model.state == State.COOLDOWN)
    {
      model.timer = new double?(timeDirector.HoursFromNow(cooldownHours));
      if (enableSFX)
      {
        SECTR_AudioSystem.Play(onCooldownCue, transform.position, false);
        SECTR_AudioSystem.Play(onCooldownCue2D, Vector3.zero, false);
      }
    }
    else
    {
      model.timer = new double?();
      if (enableSFX)
        SECTR_AudioSystem.Play(onInactiveCue, transform.position, false);
    }
    if (inactiveFX != null)
      inactiveFX.SetActive(model.state == State.INACTIVE);
    if (activeFX != null)
      activeFX.SetActive(model.state == State.ACTIVE);
    if (cooldownFX != null)
      cooldownFX.SetActive(model.state == State.COOLDOWN);
    if (onStateChanged == null)
      return;
    onStateChanged();
  }

  private void OnPlayerDeath(PlayerDeathHandler.DeathType deathType)
  {
    if (model.state != State.ACTIVE && model.state != State.COUNTDOWN)
      return;
    SetState(State.COOLDOWN, false);
  }

  public double? GetTimeRemaining() => model.timer.HasValue ? new double?(model.timer.Value - timeDirector.WorldTime()) : new double?();

  public double? GetMaxTimeRemaining()
  {
    switch (model.state)
    {
      case State.COUNTDOWN:
        return new double?(countdownMinutes * 60.0);
      case State.ACTIVE:
        return new double?(activeHours * 3600.0);
      case State.COOLDOWN:
        return new double?(cooldownHours * 3600.0);
      default:
        return new double?();
    }
  }

  public double? GetWarningTimeSeconds() => new double?();

  protected override string IdPrefix() => "qseg";

  public enum State
  {
    INACTIVE,
    COUNTDOWN,
    ACTIVE,
    COOLDOWN,
  }

  public class StateComparer : IEqualityComparer<State>
  {
    public static StateComparer Instance = new StateComparer();

    public bool Equals(State a, State b) => a == b;

    public int GetHashCode(State state) => (int) state;
  }

  public delegate void OnStateChanged();
}
