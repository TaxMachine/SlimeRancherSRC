// Decompiled with JetBrains decompiler
// Type: WeaponVacuum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVacuum : SRBehaviour, PlayerModel.Participant
{
  public float ejectSpeed = 25f;
  public float shootCooldown = 0.24f;
  public GameObject vacRegion;
  public GameObject vacOrigin;
  public GameObject vacJointPrefab;
  public Joint lockJoint;
  public GameObject vacFX;
  public float maxVacDist = 20f;
  public float captureDist = 1f;
  public float minJointSpeed = 3f;
  public float maxJointSpeed = 6f;
  public float minSpringStrength = 5f;
  public float maxSpringStrength = 20f;
  public float airBurstPower = 1200f;
  public float airBurstRadius = 20f;
  public GameObject airBurstFX;
  public float staminaPerBurst = 20f;
  public GameObject shootFX;
  public GameObject captureFX;
  public GameObject captureFailedFX;
  public GameObject heldFaceTowards;
  public SECTR_AudioCue vacStartCue;
  public SECTR_AudioCue vacRunCue;
  public SECTR_AudioCue vacEndCue;
  public SECTR_AudioCue vacShootCue;
  public SECTR_AudioCue vacShootEmptyCue;
  public SECTR_AudioCue vacAmmoSelectCue;
  public SECTR_AudioCue vacBurstCue;
  public SECTR_AudioCue vacBurstNoEnergyCue;
  public SECTR_AudioCue vacHeldCue;
  public SECTR_AudioCue gadgetModeOnCue;
  public SECTR_AudioCue gadgetModeOffCue;
  public SiloActivator siloActivator;
  public GameObject destroyOnVacFX;
  [Tooltip("Reference to the HeldCamera GameObject.")]
  public GameObject cameraHeld;
  private float nextShot;
  private PlayerState player;
  private GameObject held;
  private double heldStartTime;
  private float heldRad;
  private bool launchedHeld;
  private SECTR_PointSource vacAudio;
  private List<Joint> joints = new List<Joint>();
  private VacAudioHandler vacAudioHandler;
  private Animator vacAnimator;
  private VacColorAnimator vacColorAnimator;
  private Vector3? lastWeaponPos;
  private RegionRegistry regionRegistry;
  private PediaDirector pediaDir;
  private TutorialDirector tutDir;
  private AchievementsDirector achieveDir;
  private TimeDirector timeDir;
  private OptionsDirector optionsDir;
  private ProgressDirector progressDir;
  private int inWaterCount;
  private Dictionary<LiquidSource, IncomingLiquid> liquidDict = new Dictionary<LiquidSource, IncomingLiquid>();
  private vp_FPPlayerEventHandler playerEvents;
  private bool slimeFilter;
  private HashSet<Vacuumable> animatingConsume = new HashSet<Vacuumable>();
  private TrackCollisions tracker;
  private PlayerModel playerModel;
  private RegionMember member;
  private VacMode vacMode;
  private const int VAC_RAY_MASK = -536887557;
  private int animActiveId;
  private int animHoldingId;
  private int animVacModeId;
  private int animSprintingId;
  private int animSwitchSlotsId;
  private const float LIQUID_UNIT_TIME = 0.25f;
  private const float SHOOT_SCALE_UP_TIME = 0.1f;
  private const float SHOOT_SCALE_FACTOR = 0.2f;
  private const float CONSUME_SCALE_DOWN_TIME = 0.2f;
  private const float CONSUME_SCALE_FACTOR = 0.1f;
  private const float HOLD_SCREEN_SHAKE = 1f;
  private const float VALLEY_EJECT_SPEED_FACTOR = 3f;
  private static LRUCache<int, Vacuumable> recentIds = new LRUCache<int, Vacuumable>(1000);
  public float facingSpeed = 10f;

  public void Awake()
  {
    vacAudio = GetComponent<SECTR_PointSource>();
    vacAudioHandler = new VacAudioHandler(this);
    regionRegistry = SRSingleton<SceneContext>.Instance.RegionRegistry;
    pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    optionsDir = SRSingleton<GameContext>.Instance.OptionsDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    playerEvents = GetComponentInParent<vp_FPPlayerEventHandler>();
    member = GetComponentInParent<RegionMember>();
    tracker = vacRegion.GetComponent<TrackCollisions>();
    animActiveId = Animator.StringToHash("active");
    animHoldingId = Animator.StringToHash("holding");
    animVacModeId = Animator.StringToHash("vacMode");
    animSprintingId = Animator.StringToHash("sprinting");
    animSwitchSlotsId = Animator.StringToHash("switchSlots");
  }

  public void Start()
  {
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    nextShot = Time.fixedTime;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
    cameraHeld.SetActive(false);
  }

  public void InitModel(PlayerModel model)
  {
  }

  public void SetModel(PlayerModel model) => playerModel = model;

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

  public void Update()
  {
    if (Time.timeScale == 0.0)
      return;
    HashSet<GameObject> inVac = tracker.CurrColliders();
    UpdateHud(inVac);
    UpdateSlotForInputs();
    UpdateVacModeForInputs();
    SRSingleton<SceneContext>.Instance.PlayerState.InGadgetMode = vacMode == VacMode.GADGET;
    if (SRInput.Actions.attack.WasPressed || SRInput.Actions.vac.WasPressed || SRInput.Actions.burst.WasPressed)
      launchedHeld = false;
    float num = 1f;
    if (Time.fixedTime >= (double) nextShot && !launchedHeld && vacMode == VacMode.SHOOT)
    {
      Expel(inVac);
      num = GetShootSpeedFactor(inVac);
      nextShot = Time.fixedTime + shootCooldown / num;
    }
    if (vacAnimator != null)
      vacAnimator.speed = num;
    if (!launchedHeld && vacMode == VacMode.VAC)
    {
      vacAudioHandler.SetActive(true);
      vacFX.SetActive(held == null);
      siloActivator.enabled = held == null;
      if (held != null)
        UpdateHeld(inVac);
      else
        Consume(inVac);
    }
    else
      ClearVac();
    UpdateVacAnimators();
  }

  private float GetShootSpeedFactor(HashSet<GameObject> inVac)
  {
    float val1 = 1f;
    foreach (GameObject gameObject in inVac)
    {
      VacShootAccelerator component = gameObject.GetComponent<VacShootAccelerator>();
      if (component != null)
        val1 = Math.Max(val1, component.GetVacShootSpeedFactor());
    }
    return val1;
  }

  private void UpdateVacAnimators()
  {
    bool flag = playerModel.hasAirBurst && SRInput.Actions.burst.WasPressed;
    bool isActive = ((vacMode == VacMode.SHOOT ? 1 : (vacMode == VacMode.VAC ? 1 : 0)) | (flag ? 1 : 0)) != 0;
    bool inVacMode = vacMode == VacMode.VAC;
    if (vacAnimator == null)
    {
      vacAnimator = GetComponentInChildren<Animator>();
      vacColorAnimator = GetComponentInChildren<VacColorAnimator>();
    }
    vacAnimator.SetBool(animActiveId, isActive);
    vacAnimator.SetBool(animVacModeId, inVacMode);
    vacAnimator.SetBool(animHoldingId, held != null);
    vacColorAnimator.SetVacActive(isActive);
    vacColorAnimator.SetVacMode(inVacMode);
    if (flag)
      AirBurst();
    vacAnimator.SetBool(animSprintingId, playerEvents.Run.Active);
  }

  private void ClearVac()
  {
    ClearLiquids();
    vacAudioHandler.SetActive(false);
    vacFX.SetActive(false);
    siloActivator.enabled = false;
    foreach (Joint joint in joints)
    {
      if (joint != null && joint.connectedBody != null)
      {
        Vacuumable component = joint.connectedBody.GetComponent<Vacuumable>();
        if (component != null)
          component.release();
      }
    }
    if (!(held != null))
      return;
    Vacuumable component1 = held.GetComponent<Vacuumable>();
    if (component1 != null)
      component1.release();
    lockJoint.connectedBody = null;
    Identifiable component2 = held.GetComponent<Identifiable>();
    held = null;
    SetHeldRad(0.0f);
    if (component2 != null && Identifiable.IsTarr(component2.id))
      achieveDir.MaybeUpdateMaxStat(AchievementsDirector.IntStat.EXTENDED_TARR_HOLD, (int) Math.Floor((timeDir.WorldTime() - heldStartTime) * 0.01666666753590107));
    heldStartTime = double.NaN;
  }

  private void SetHeldRad(float rad)
  {
    heldRad = rad;
    Vector3 localPosition = lockJoint.transform.localPosition;
    if (rad == 0.0)
      lockJoint.transform.localPosition = new Vector3(localPosition.x, 1f, localPosition.z);
    else
      lockJoint.transform.localPosition = new Vector3(localPosition.x, rad, localPosition.z);
  }

  private void UpdateHeld(HashSet<GameObject> inVac)
  {
    Rigidbody component1 = held.GetComponent<Rigidbody>();
    if (lockJoint.connectedBody != component1)
    {
      held.transform.position = lockJoint.transform.position;
      lockJoint.connectedBody = component1;
      lockJoint.anchor = Vector3.zero;
      lockJoint.connectedAnchor = Vector3.zero;
    }
    foreach (GameObject gameObject in inVac)
    {
      if (gameObject != held)
      {
        Vacuumable component2 = gameObject.GetComponent<Vacuumable>();
        if (component2 != null)
          component2.release();
      }
    }
    ClearLiquids();
  }

  public void LateUpdate()
  {
    if (!(held != null))
      return;
    held.transform.position = lockJoint.transform.position;
    held.transform.LookAt(heldFaceTowards.transform, heldFaceTowards.transform.up);
  }

  public bool InVacMode() => vacMode == VacMode.VAC;

  private void PlayTransientAudio(SECTR_AudioCue cue) => SECTR_AudioSystem.Play(cue, transform.position, false);

  private void UpdateSlotForInputs()
  {
    if (SRInput.Actions.slot1.WasPressed)
    {
      if (!player.Ammo.SetAmmoSlot(0))
        return;
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else if (SRInput.Actions.slot2.WasPressed)
    {
      if (!player.Ammo.SetAmmoSlot(1))
        return;
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else if (SRInput.Actions.slot3.WasPressed)
    {
      if (!player.Ammo.SetAmmoSlot(2))
        return;
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else if (SRInput.Actions.slot4.WasPressed)
    {
      if (!player.Ammo.SetAmmoSlot(3))
        return;
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else if (SRInput.Actions.slot5.WasPressed)
    {
      if (!player.Ammo.SetAmmoSlot(4))
        return;
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else if (SRInput.Actions.prevSlot.WasPressed)
    {
      player.Ammo.PrevAmmoSlot();
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
    else
    {
      if (!SRInput.Actions.nextSlot.WasPressed)
        return;
      player.Ammo.NextAmmoSlot();
      PlayTransientAudio(vacAmmoSelectCue);
      vacAnimator.SetTrigger(animSwitchSlotsId);
    }
  }

  private void UpdateVacModeForInputs()
  {
    if (SRInput.Actions.toggleGadgetMode.WasReleased && progressDir.HasProgress(ProgressDirector.ProgressType.UNLOCK_LAB))
    {
      if (vacMode == VacMode.GADGET)
      {
        vacMode = VacMode.NONE;
        SRSingleton<Overlay>.Instance.SetEnableGadgetMode(false);
        PlayTransientAudio(gadgetModeOnCue);
      }
      else
      {
        vacMode = VacMode.GADGET;
        SRSingleton<Overlay>.Instance.SetEnableGadgetMode(true);
        tutDir.OnGadgetModeActivated();
        PlayTransientAudio(gadgetModeOffCue);
      }
    }
    switch (vacMode)
    {
      case VacMode.NONE:
        if (SRInput.Actions.vac.WasPressed)
        {
          vacMode = VacMode.VAC;
          break;
        }
        if (!SRInput.Actions.attack.WasPressed)
          break;
        vacMode = VacMode.SHOOT;
        break;
      case VacMode.SHOOT:
        if (SRInput.Actions.vac.WasPressed)
        {
          vacMode = VacMode.VAC;
          break;
        }
        if (SRInput.Actions.attack.IsPressed)
          break;
        vacMode = VacMode.NONE;
        break;
      case VacMode.VAC:
        if (SRInput.Actions.attack.WasPressed)
        {
          vacMode = VacMode.SHOOT;
          break;
        }
        if ((!optionsDir.vacLockOnHold || !(held != null) ? (!SRInput.Actions.vac.IsPressed ? 1 : 0) : (SRInput.Actions.vac.WasPressed ? 1 : 0)) == 0)
          break;
        vacMode = VacMode.NONE;
        break;
    }
  }

  public void EnterWater() => ++inWaterCount;

  public void ExitWater() => --inWaterCount;

  public bool IsHolding() => held != null;

  public Identifiable.Id HeldIdent() => held != null ? held.GetComponent<Identifiable>().id : Identifiable.Id.NONE;

  public void DropAllVacced()
  {
    foreach (Joint joint in joints)
    {
      if (joint != null && joint.connectedBody != null)
      {
        Vacuumable component = joint.connectedBody.GetComponent<Vacuumable>();
        if (component != null)
          component.release();
      }
    }
    if (held != null)
    {
      Vacuumable component = held.GetComponent<Vacuumable>();
      if (component != null)
        component.release();
      lockJoint.connectedBody = null;
      held = null;
      SetHeldRad(0.0f);
      heldStartTime = double.NaN;
    }
    vacAudioHandler.SetActive(false);
    if (vacAnimator == null)
    {
      vacAnimator = GetComponentInChildren<Animator>();
      vacColorAnimator = GetComponentInChildren<VacColorAnimator>();
    }
    vacAnimator.SetBool(animActiveId, false);
    vacAnimator.SetBool(animVacModeId, false);
    vacAnimator.SetBool(animHoldingId, false);
    vacColorAnimator.SetVacActive(false);
    vacColorAnimator.SetVacMode(false);
  }

  private void ClearLiquids()
  {
    foreach (IncomingLiquid incomingLiquid in liquidDict.Values)
      Destroyer.Destroy(incomingLiquid.fx, "WeaponVacuum.ClearLiquids");
    liquidDict.Clear();
  }

  private void AirBurst()
  {
    if (player.GetCurrEnergy() < (double) staminaPerBurst)
    {
      PlayTransientAudio(vacBurstNoEnergyCue);
    }
    else
    {
      AnalyticsUtil.CustomEvent("PulseWaveUsed");
      PlayTransientAudio(vacBurstCue);
      Vector3 position = vacOrigin.transform.position;
      foreach (Collider collider in Physics.OverlapSphere(position, airBurstRadius))
      {
        if ((bool) (UnityEngine.Object) collider && collider.GetComponent<Rigidbody>() != null && collider.gameObject != gameObject)
        {
          Identifiable component1 = collider.gameObject.GetComponent<Identifiable>();
          if (component1 != null && Identifiable.IsSlime(component1.id) && Vector3.Dot(vacOrigin.transform.up, collider.gameObject.transform.position - vacOrigin.transform.position) > 0.0)
          {
            Rigidbody component2 = collider.GetComponent<Rigidbody>();
            PhysicsUtil.SoftExplosionForce(airBurstPower * component2.mass, position, airBurstRadius, component2);
          }
        }
      }
      player.SpendEnergy(staminaPerBurst);
      if (!(airBurstFX != null))
        return;
      Instantiate(airBurstFX, Vector3.zero, Quaternion.identity).transform.SetParent(vacOrigin.transform, false);
    }
  }

  private void Expel(HashSet<GameObject> inVac)
  {
    if (held != null)
      ExpelHeld();
    else if (player.Ammo.HasSelectedAmmo())
      ExpelAmmo(inVac);
    else
      PlayTransientAudio(vacShootEmptyCue);
  }

  private void ExpelHeld()
  {
    vp_FPController componentInParent = GetComponentInParent<vp_FPController>();
    Ray ray = new Ray(vacOrigin.transform.position, vacOrigin.transform.up);
    Vector3 origin = ray.origin;
    Vector3 vel = ray.direction * ejectSpeed + componentInParent.Velocity;
    held.transform.position = EnsureNotShootingIntoRock(origin, ray, heldRad, ref vel);
    held.GetComponent<Rigidbody>().velocity = vel;
    Vacuumable component1 = held.GetComponent<Vacuumable>();
    if (component1 != null)
    {
      component1.release();
      component1.Launch(Vacuumable.LaunchSource.PLAYER);
      SlimeEat component2 = held.GetComponent<SlimeEat>();
      if (component2 != null)
        component2.CancelChomp(SRSingleton<SceneContext>.Instance.Player);
    }
    DamagePlayerOnTouch component3 = held.GetComponent<DamagePlayerOnTouch>();
    if (component3 != null)
      component3.ResetDamageAmnesty();
    lockJoint.connectedBody = null;
    Identifiable component4 = held.GetComponent<Identifiable>();
    held = null;
    SetHeldRad(0.0f);
    if (component4 != null && Identifiable.IsTarr(component4.id))
      achieveDir.MaybeUpdateMaxStat(AchievementsDirector.IntStat.EXTENDED_TARR_HOLD, (int) Math.Floor((timeDir.WorldTime() - heldStartTime) * 0.01666666753590107));
    heldStartTime = double.NaN;
    launchedHeld = true;
    ShootEffect();
  }

  private void ExpelAmmo(HashSet<GameObject> inVac)
  {
    GameObject selectedStored = player.Ammo.GetSelectedStored();
    Identifiable component = selectedStored.GetComponent<Identifiable>();
    Expel(selectedStored);
    player.Ammo.DecrementSelectedAmmo();
    if (component != null)
      tutDir.OnShoot(component.id);
    ShootEffect();
  }

  private float GetSpeed(Identifiable.Id id)
  {
    switch (id)
    {
      case Identifiable.Id.VALLEY_AMMO_1:
      case Identifiable.Id.VALLEY_AMMO_2:
      case Identifiable.Id.VALLEY_AMMO_3:
      case Identifiable.Id.VALLEY_AMMO_4:
        return ejectSpeed * 3f;
      default:
        return ejectSpeed;
    }
  }

  public void Expel(GameObject toExpel, bool ignoreEmotions = false)
  {
    vp_FPController componentInParent = GetComponentInParent<vp_FPController>();
    Ray ray = new Ray(vacOrigin.transform.position, vacOrigin.transform.up);
    float objRad = PhysicsUtil.RadiusOfObject(toExpel);
    float num = ray.direction.y >= 0.0 ? 0.0f : -0.5f * ray.direction.y;
    Vector3 startPos = ray.origin + ray.direction * (objRad * 0.2f + num);
    Vector3 vel = ray.direction * GetSpeed(player.Ammo.GetSelectedId()) + componentInParent.Velocity;
    Vector3 position = EnsureNotShootingIntoRock(startPos, ray, objRad, ref vel);
    GameObject gameObject = InstantiateActor(toExpel, regionRegistry.GetCurrentRegionSetId(), position, Quaternion.identity);
    gameObject.transform.LookAt(transform);
    PhysicsUtil.RestoreFreezeRotationConstraints(gameObject);
    SlimeEmotions component = gameObject.GetComponent<SlimeEmotions>();
    if (component != null && player.Ammo.GetSelectedId() != Identifiable.Id.NONE && !ignoreEmotions)
      component.SetAll(player.Ammo.GetSelectedEmotions());
    gameObject.GetComponent<Rigidbody>().velocity = vel;
    gameObject.transform.DOScale(gameObject.transform.localScale, 0.1f).From(gameObject.transform.localScale * 0.2f).SetEase(Ease.Linear);
    gameObject.GetComponent<Vacuumable>().Launch(Vacuumable.LaunchSource.PLAYER);
  }

  public void OnDamageExposure(GlitchMetadata.ExposureMetadata metadata, int count)
  {
    vp_FPController controller = GetComponentInParent<vp_FPController>();
    float radius = PhysicsUtil.RadiusOfObject(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.GLITCH_SLIME));
    GlitchMetadata.ExposureMetadata exposureMetadata = metadata;
    int? nullable = new int?(count);
    Vector3? origin = new Vector3?(transform.position);
    GlitchMetadata.ExposureMetadata.GetPositionAndVelocity getPositionAndVelocity = (out Vector3 position, out Vector3 velocity) =>
    {
      Ray ray = new Ray(vacOrigin.transform.position, vacOrigin.transform.up);
      float num = ray.direction.y >= 0.0 ? 0.0f : -0.5f * ray.direction.y;
      velocity = ray.direction * metadata.velocity + controller.Velocity;
      velocity = Quaternion.Euler(metadata.velocityRotationX.GetRandom(), metadata.velocityRotationY.GetRandom(), 0.0f) * velocity;
      position = ray.origin + ray.direction * (radius * 0.2f + num);
      position = EnsureNotShootingIntoRock(position, ray, radius, ref velocity);
    };
    int? count1 = nullable;
    GlitchMetadata.ExposureMetadata.OnInstantiated onInstantiated = instance =>
    {
      instance.GetComponent<GlitchSlimeFlee>().DisableExposureChance();
      vacAnimator.SetTrigger(animSwitchSlotsId);
      ShootEffect();
    };
    exposureMetadata.OnExposed(origin: origin, getPositionAndVelocity: getPositionAndVelocity, count: count1, onInstantiated: onInstantiated);
  }

  private Vector3 EnsureNotShootingIntoRock(
    Vector3 startPos,
    Ray ray,
    float objRad,
    ref Vector3 vel)
  {
    float num = 0.5f;
    Ray ray1 = new Ray(ray.origin - ray.direction * num, ray.direction);
    float magnitude = (startPos - ray1.origin).magnitude;
    int layerMask = 270572033;
    RaycastHit hitInfo;
    Physics.Raycast(ray1, out hitInfo, magnitude, layerMask, QueryTriggerInteraction.Ignore);
    if (hitInfo.collider != null)
    {
      startPos = hitInfo.point - ray.direction * objRad;
      vel -= Vector3.Project(vel, hitInfo.normal);
    }
    return startPos;
  }

  private void ShootEffect()
  {
    if (shootFX != null)
      SpawnAndPlayFX(shootFX, vacOrigin, Vector3.zero, Quaternion.identity);
    PlayTransientAudio(vacShootCue);
  }

  private void CaptureEffect()
  {
    if (!(captureFX != null))
      return;
    SpawnAndPlayFX(captureFX, vacOrigin, Vector3.zero, Quaternion.identity);
  }

  private void CaptureFailedEffect()
  {
    if (!(captureFailedFX != null))
      return;
    SpawnAndPlayFX(captureFailedFX, vacOrigin, Vector3.zero, Quaternion.identity);
  }

  private void Consume(HashSet<GameObject> inVac)
  {
    ConsumeExistingJointed();
    lastWeaponPos = new Vector3?(vacOrigin.transform.position);
    List<LiquidSource> currLiquids = new List<LiquidSource>();
    UnityWorkarounds.SafeRemoveAllNulls(animatingConsume);
    int slimesInVac = 0;
    foreach (GameObject gameObj in inVac)
      ConsumeVacItem(gameObj, ref slimesInVac, ref currLiquids);
    if (slimesInVac > 0 && !CellDirector.IsOnRanch(member))
      SRSingleton<SceneContext>.Instance.AchievementsDirector.MaybeUpdateMaxStat(AchievementsDirector.IntStat.SLIMES_IN_VAC, slimesInVac);
    ConsumeLiquids(currLiquids);
  }

  private void ConsumeLiquids(List<LiquidSource> currLiquids)
  {
    List<LiquidSource> liquidSourceList = new List<LiquidSource>();
    foreach (LiquidSource key in liquidDict.Keys)
    {
      if (!currLiquids.Contains(key))
        liquidSourceList.Add(key);
    }
    foreach (LiquidSource key in liquidSourceList)
    {
      Destroyer.Destroy(liquidDict[key].fx, "WeaponVacuum.ConsumeLiquids");
      liquidDict.Remove(key);
    }
    foreach (LiquidSource currLiquid in currLiquids)
    {
      if (!liquidDict.ContainsKey(currLiquid))
      {
        GameObject fx = SpawnAndPlayFX(SRSingleton<GameContext>.Instance.LookupDirector.GetLiquidIncomingFX(currLiquid.liquidId));
        fx.transform.SetParent(vacOrigin.transform, false);
        liquidDict[currLiquid] = new IncomingLiquid(currLiquid.liquidId, Time.time + 0.25f, fx);
      }
      else if (Time.time >= (double) liquidDict[currLiquid].nextUnitTick)
      {
        int num = ConsumeLiquid(currLiquid) ? 1 : 0;
        liquidDict[currLiquid].nextUnitTick += 0.25f;
        if (num != 0)
        {
          currLiquid.ConsumeLiquid();
          CaptureEffect();
        }
        else
          SpawnAndPlayFX(SRSingleton<GameContext>.Instance.LookupDirector.GetLiquidVacFailFX(currLiquid.liquidId)).transform.SetParent(vacOrigin.transform, false);
      }
    }
  }

  private bool ConsumeLiquid(LiquidSource source)
  {
    if (player.Ammo.MaybeAddToSlot(source.liquidId, null))
      return true;
    if (source.ReplacesExistingLiquidAmmo())
    {
      for (int index = 0; index < player.Ammo.GetUsableSlotCount(); ++index)
      {
        Identifiable.Id slotName = player.Ammo.GetSlotName(index);
        if (slotName != source.liquidId && Identifiable.IsLiquid(slotName))
          return player.Ammo.Replace(index, source.liquidId);
      }
    }
    return false;
  }

  private void ConsumeExistingJointed()
  {
    joints.RemoveAll(joint => joint == null);
    foreach (SpringJoint joint in joints)
    {
      if (!(joint.connectedBody != null) || !joint.connectedBody.isKinematic)
      {
        float magnitude = joint.transform.localPosition.magnitude;
        float num1 = 0.0f;
        if (lastWeaponPos.HasValue)
          num1 = (joint.transform.position - lastWeaponPos.Value).magnitude - magnitude;
        float t = magnitude / maxVacDist;
        float num2 = Mathf.Lerp(maxJointSpeed, minJointSpeed, t);
        float num3 = Mathf.Lerp(maxSpringStrength, minSpringStrength, t);
        float num4 = Mathf.Max(0.0f, magnitude - num2 * Time.deltaTime - num1);
        if (magnitude > 0.0)
          joint.transform.localPosition = num4 / magnitude * joint.transform.localPosition;
        joint.spring = num3;
      }
    }
  }

  private void ConsumeVacItem(
    GameObject gameObj,
    ref int slimesInVac,
    ref List<LiquidSource> currLiquids)
  {
    Vacuumable component1 = gameObj.GetComponent<Vacuumable>();
    Identifiable component2 = gameObj.GetComponent<Identifiable>();
    LiquidSource component3 = gameObj.GetComponent<LiquidSource>();
    if (component2 != null && Identifiable.IsSlime(component2.id))
      ++slimesInVac;
    if (component1 != null && component1.enabled && (component2 == null || !Identifiable.IsLiquid(component2.id)) && !animatingConsume.Contains(component1))
    {
      Ray ray = new Ray(vacOrigin.transform.position, gameObj.transform.position - vacOrigin.transform.position);
      RaycastHit hitInfo;
      if (Physics.Raycast(ray, out hitInfo, maxVacDist, -536887557))
      {
        if (hitInfo.rigidbody != null && hitInfo.rigidbody.gameObject == gameObj)
        {
          if (component1.GetDestroyOnVac())
          {
            SpawnAndPlayFX(destroyOnVacFX, gameObj.transform.position, gameObj.transform.rotation);
            if (component2 == null)
              Destroyer.Destroy(gameObj, "WeaponVacuum.ConsumeVacItem#1");
            else
              Destroyer.DestroyActor(gameObj, "WeaponVacuum.ConsumeVacItem#2");
          }
          else if (component1.canCapture() && (!slimeFilter || component2 == null || !Identifiable.IsSlime(component2.id)))
          {
            Rigidbody component4 = component1.GetComponent<Rigidbody>();
            if (component1.isCaptive() && component1.IsTornadoed())
              component1.release();
            if (!component1.isCaptive())
            {
              if (component4.isKinematic)
                component1.Pending = true;
              else
                component1.capture(CreateJoint(new Ray(vacOrigin.transform.position, vacOrigin.transform.rotation * Vector3.up), component1));
            }
            if (!component4.isKinematic && component2 != null && (Identifiable.IsAnimal(component2.id) || Identifiable.IsSlime(component2.id)))
              RotateTowards(gameObj, heldFaceTowards.transform.position - gameObj.transform.position);
          }
        }
        if (component2 != null && component1.isCaptive() && Vector3.Distance(gameObj.transform.position, ray.origin) < (double) captureDist)
        {
          if (component2.id != Identifiable.Id.NONE && component1.enabled && component1.size == Vacuumable.Size.NORMAL)
          {
            if (component1 != null)
              StartCoroutine(StartConsuming(component1, component2.id));
          }
          else if (held == null && component1.enabled && component1.canCapture())
          {
            held = gameObj;
            SetHeldRad(PhysicsUtil.RadiusOfObject(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(component2.id)));
            component1.hold();
            if (Identifiable.IsLargo(component2.id))
            {
              tutDir.MaybeShowPopup(TutorialDirector.Id.LARGO);
              pediaDir.MaybeShowPopup(PediaDirector.Id.LARGO_SLIME);
            }
            SlimeFeral component5 = gameObj.GetComponent<SlimeFeral>();
            if (component5 != null && component5.IsFeral())
              pediaDir.MaybeShowPopup(PediaDirector.Id.FERAL_SLIME);
            heldStartTime = timeDir.WorldTime();
            SlimeEat component6 = held.GetComponent<SlimeEat>();
            if (component6 != null)
              component6.ResetEatClock();
            TentacleGrapple component7 = held.GetComponent<TentacleGrapple>();
            if (component7 != null)
              component7.Release();
            GroundVine component8 = held.GetComponent<GroundVine>();
            if (component8 != null)
              component8.Release();
            pediaDir.MaybeShowPopup(component2.id);
            PlayTransientAudio(vacHeldCue);
            SRSingleton<SceneContext>.Instance.Player.GetComponent<ScreenShaker>().ShakeFrontImpact(1f);
          }
        }
      }
    }
    if (!(component3 != null) || !component3.Available())
      return;
    if (playerEvents.Underwater.Active)
    {
      currLiquids.Add(component3);
    }
    else
    {
      float num1 = 1.5f;
      Vector3 up = vacOrigin.transform.up;
      Vector3 origin = vacOrigin.transform.position - up.normalized * num1;
      float num2 = maxVacDist + num1;
      Ray ray = new Ray(origin, up);
      float val1 = float.PositiveInfinity;
      float num3 = float.NaN;
      double maxDistance = num2;
      foreach (RaycastHit raycastHit in Physics.RaycastAll(ray, (float) maxDistance, -536887557, QueryTriggerInteraction.Collide))
      {
        if (raycastHit.collider.gameObject == gameObj)
          num3 = raycastHit.distance;
        else if (!raycastHit.collider.isTrigger)
          val1 = Math.Min(val1, raycastHit.distance);
      }
      if (num3 > (double) val1)
        return;
      currLiquids.Add(component3);
    }
  }

  private IEnumerator StartConsuming(Vacuumable vacuumable, Identifiable.Id id)
  {
    vacuumable.StartConsumeScale();
    MoveTowards moveTowards = vacuumable.gameObject.AddComponent<MoveTowards>();
    moveTowards.dest = vacOrigin.transform;
    animatingConsume.Add(vacuumable);
    yield return new WaitForSeconds(0.2f);
    Destroyer.Destroy(moveTowards, "WeaponVacuum.StartConsuming");
    if (vacuumable != null)
    {
      if (vacuumable.TryConsume())
      {
        CaptureEffect();
        pediaDir.MaybeShowPopup(id);
        tutDir.OnVac(id);
        if (Identifiable.IsPlort(id) && !CellDirector.IsOnRanch(member))
          achieveDir.AddToStat(AchievementsDirector.IntStat.DAY_COLLECT_PLORTS, 1);
      }
      else
      {
        vacuumable.release();
        CaptureFailedEffect();
        animatingConsume.Remove(vacuumable);
        vacuumable.ReverseConsumeScale();
      }
    }
  }

  public void ForceJoint(Vacuumable vacuumable) => vacuumable.capture(CreateJoint(new Ray(vacOrigin.transform.position, vacOrigin.transform.rotation * Vector3.up), vacuumable));

  private Joint CreateJoint(Ray alongRay, Vacuumable vacuumable)
  {
    Vector3 position = vacuumable.transform.position;
    GameObject gameObject = Instantiate(vacJointPrefab);
    gameObject.transform.position = position;
    gameObject.transform.SetParent(vacOrigin.transform, true);
    SpringJoint component = gameObject.GetComponent<SpringJoint>();
    component.spring = minSpringStrength;
    joints.Add(component);
    return component;
  }

  private Vector3 ClosestPointOnRay(Ray ray, Vector3 pt)
  {
    Vector3 rhs = pt - ray.origin;
    float num = Vector3.Dot(ray.direction, rhs);
    return ray.origin + ray.direction * num;
  }

  private void UpdateHud(HashSet<GameObject> inVac)
  {
    UpdateCrosshair(inVac);
    UpdateTargetingInfo();
  }

  private void UpdateCrosshair(HashSet<GameObject> inVac)
  {
    bool flag = false;
    foreach (GameObject gameObject in inVac)
    {
      if (gameObject == null)
      {
        Debug.Log("Null gameobj, skipping: " + (gameObject == null ? "null" : gameObject.name));
        if (!Application.isEditor)
          ;
      }
      else
      {
        try
        {
          Vacuumable component;
          if (!recentIds.contains(gameObject.GetInstanceID()))
          {
            component = gameObject.GetComponent<Vacuumable>();
            recentIds.put(gameObject.GetInstanceID(), component);
          }
          else
            component = recentIds.get(gameObject.GetInstanceID());
          if (component != null)
          {
            if (component.enabled)
            {
              flag = true;
              break;
            }
          }
        }
        catch (Exception ex)
        {
          Debug.Log("Got an e, skipping: " + (gameObject == null ? "null" : gameObject.name) + " msg: " + ex.Message);
          int num = Application.isEditor ? 1 : 0;
        }
      }
    }
    player.PointedAtVaccable = flag;
  }

  private void UpdateTargetingInfo()
  {
    Ray ray = new Ray(vacOrigin.transform.position, vacOrigin.transform.up);
    player.Targeting = null;
    RaycastHit raycastHit = default;
    ref RaycastHit local = ref raycastHit;
    double maxVacDist = this.maxVacDist;
    if (!Physics.Raycast(ray, out local, (float) maxVacDist, -536887557))
      return;
    player.Targeting = raycastHit.collider.gameObject;
  }

  public bool InGadgetMode() => vacMode == VacMode.GADGET;

  private void RotateTowards(GameObject gameObj, Vector3 dirToTarget)
  {
    Rigidbody component = gameObj.GetComponent<Rigidbody>();
    Vector3 vector3 = Vector3.Cross(Quaternion.AngleAxis(component.angularVelocity.magnitude * 57.29578f / facingSpeed, component.angularVelocity) * component.transform.forward, dirToTarget);
    component.AddTorque(vector3 * facingSpeed * facingSpeed);
  }

  public void FixedUpdate()
  {
    if (!(held != null))
      return;
    float heldRad = this.heldRad;
    float num1 = (float) (heldRad * 2.0 + 0.10000000149011612);
    Ray ray = new Ray(lockJoint.transform.position - num1 * lockJoint.transform.up, lockJoint.transform.up);
    float num2 = num1;
    int num3 = 270567937;
    double radius = heldRad;
    RaycastHit raycastHit = default;
    ref RaycastHit local = ref raycastHit;
    double maxDistance = num2;
    int layerMask = num3;
    if (!Physics.SphereCast(ray, (float) radius, out local, (float) maxDistance, layerMask, QueryTriggerInteraction.Ignore))
      return;
    vp_FPController componentInParent = GetComponentInParent<vp_FPController>();
    float num4 = (num2 - raycastHit.distance) / heldRad;
    Vector3 force = Vector3.Project(vacOrigin.transform.up, raycastHit.normal) * -0.2f * num4 * num4;
    componentInParent.AddDepenetrationForce(force);
  }

  private class IncomingLiquid
  {
    public Identifiable.Id id;
    public float nextUnitTick;
    public GameObject fx;

    public IncomingLiquid(Identifiable.Id id, float nextUnitTick, GameObject fx)
    {
      this.id = id;
      this.nextUnitTick = nextUnitTick;
      this.fx = fx;
    }
  }

  private enum VacMode
  {
    NONE,
    SHOOT,
    VAC,
    GADGET,
  }

  private class VacAudioHandler
  {
    private WeaponVacuum vac;
    private bool active;

    public VacAudioHandler(WeaponVacuum vac) => this.vac = vac;

    public void SetActive(bool active)
    {
      if (active && !this.active)
      {
        vac.vacAudio.Cue = vac.vacStartCue;
        vac.vacAudio.Play();
        vac.vacAudio.Cue = vac.vacRunCue;
        vac.vacAudio.Play();
      }
      else if (!active && this.active)
      {
        vac.vacAudio.Cue = vac.vacEndCue;
        vac.vacAudio.Play();
      }
      this.active = active;
    }
  }

  private class MoveTowards : SRBehaviour
  {
    public Transform dest;
    private float endTime;

    public void Awake() => endTime = Time.fixedTime + 0.2f;

    public void FixedUpdate()
    {
      float num = endTime - Time.fixedTime;
      transform.position = Vector3.Lerp(transform.position, dest.position, num <= (double) Time.fixedDeltaTime ? 1f : Time.fixedDeltaTime / num);
    }
  }
}
