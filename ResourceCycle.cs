// Decompiled with JetBrains decompiler
// Type: ResourceCycle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCycle : 
  RegisteredActorBehaviour,
  RegistryUpdateable,
  Ignitable,
  ActorModel.Participant
{
  public float unripeGameHours = 6f;
  public float ripeGameHours = 6f;
  public float edibleGameHours = 12f;
  public float rottenGameHours = 6f;
  public Material rottenMat;
  public GameObject destroyFX;
  public SECTR_AudioCue releaseCue;
  public Transform toShake;
  public bool vacuumableWhenRipe = true;
  private DetachmentEvent onDetachment;
  public bool addEjectionForce = true;
  public float releasePrepTime = 1f;
  public GameObject igniteFX;
  private Vacuumable vacuumable;
  private Renderer mainRenderer;
  private TimeDirector timeDir;
  private SafeJointReference joint;
  private Vector3 defaultScale;
  private AdditionalRipeness additionalRipenessDelegate;
  private bool preparingToRelease;
  private float releaseAt;
  private Material rotInProgressMat;
  private const float EJECT_TORQUE = 5f;
  private const float EJECT_FORCE = 80f;
  private const float UNRIPE_SCALE = 0.33f;
  private const float RIPEN_SCALE_TIME = 4f;
  private const float VIBRATE_AMPLITUDE = 0.033f;
  private const float ROT_PROGRESS_PER_SEC = 0.5f;
  private const string ROT_SHADER_PARAM = "_Rot";
  private bool hasVacuumable;
  private Rigidbody body;
  private Vector3 toShakeDefaultPos;
  private float vibrateAmplitude;
  private Identifiable ident;
  private ProduceModel model;
  private MiracleMix preservative;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    vacuumable = GetComponent<Vacuumable>();
    hasVacuumable = vacuumable != null;
    mainRenderer = GetComponentInChildren<Renderer>();
    defaultScale = transform.localScale;
    vibrateAmplitude = 0.033f / defaultScale.x;
    body = GetComponent<Rigidbody>();
    toShakeDefaultPos = toShake.localPosition;
    ident = GetComponent<Identifiable>();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    onDetachment = null;
    ident = null;
    if (rotInProgressMat != null)
      Destroyer.Destroy(rotInProgressMat, "ResourceCycle.OnDestroy");
    if (!(preservative != null))
      return;
    preservative.RemoveResourceCycle(this);
  }

  public void InitModel(ActorModel model) => ((ProduceModel) model).progressTime = timeDir.HoursFromNowOrStart(Vary(edibleGameHours));

  public void SetModel(ActorModel model)
  {
    this.model = (ProduceModel) model;
    SetInitState(this.model.state, this.model.progressTime);
  }

  public void Attach(
    Joint joint,
    AdditionalRipeness additionalRipenessDelegate = null,
    DetachmentEvent detachmentDelegate = null)
  {
    model.state = State.UNRIPE;
    DetachFromJoint();
    this.joint = SafeJointReference.AttachSafely(body.gameObject, joint, false);
    joint.anchor = Vector3.zero;
    joint.connectedAnchor = Vector3.zero;
    this.additionalRipenessDelegate = additionalRipenessDelegate;
    onDetachment = detachmentDelegate;
    body.isKinematic = true;
    transform.localScale = defaultScale * 0.33f;
    model.progressTime = timeDir.HoursFromNowOrStart(Vary(unripeGameHours));
  }

  public void Reattach(Joint joint)
  {
    DetachFromJoint();
    this.joint = SafeJointReference.AttachSafely(body.gameObject, joint, false);
    joint.anchor = Vector3.zero;
    joint.connectedAnchor = Vector3.zero;
  }

  public void Detach(
    AdditionalRipeness additionalRipenessDelegate)
  {
    this.additionalRipenessDelegate -= additionalRipenessDelegate;
    if (model.state == State.RIPE)
      MakeEdible();
    DetachFromJoint();
  }

  private void DetachFromJoint()
  {
    if (!(joint != null))
      return;
    if (joint.joint != null)
      joint.joint.connectedBody = null;
    Destroyer.Destroy(joint, "ResourceCycle.DetachFromJoint");
    joint = null;
    if (onDetachment == null)
      return;
    onDetachment();
    onDetachment = null;
  }

  public void AttachPreservative(MiracleMix preservative)
  {
    this.preservative = preservative;
    if (model.state != State.EDIBLE)
      return;
    additionalRipenessDelegate = preservative.PreservativeRipenessModifier;
  }

  public void DetachPreservative(MiracleMix preservative)
  {
    if (model.state == State.EDIBLE)
      additionalRipenessDelegate = null;
    this.preservative = null;
  }

  public void Ignite(GameObject igniter)
  {
    if (model.state != State.EDIBLE && model.state != State.ROTTEN)
      return;
    if (igniteFX != null)
      SpawnAndPlayFX(igniteFX);
    if (model.state == State.EDIBLE)
      Destroyer.DestroyActor(gameObject, "ResourceCycle.Ignite");
    else
      Destroyer.Destroy(gameObject, "ResourceCycle.Ignite");
  }

  public void ImmediatelyRipen(float bonusRipenessHours)
  {
    if (model.state != State.UNRIPE)
      Debug.Log("Trying to ripen already-ripe resource?");
    else
      model.progressTime = timeDir.HoursFromNowOrStart(-bonusRipenessHours);
  }

  public void ImmediatelyRot()
  {
    if (model == null || model.state != State.EDIBLE)
      return;
    Rot();
    SetRotten(true);
  }

  public static float Vary(float val) => Randoms.SHARED.GetInRange(0.9f, 1.1f) * val;

  public void RegistryUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    if (additionalRipenessDelegate != null)
      model.progressTime -= additionalRipenessDelegate() * timeDir.DeltaWorldTime();
    Rigidbody body = this.body;
    if (model.state == State.UNRIPE && joint == null)
      Destroyer.DestroyActor(gameObject, "ResourceCycle.RegistryUpdate#1");
    else if (joint == null && body.isKinematic)
      body.isKinematic = false;
    if (hasVacuumable && vacuumableWhenRipe && model.state == State.RIPE && vacuumable.Pending && !preparingToRelease)
    {
      preparingToRelease = true;
      releaseAt = Time.time + releasePrepTime;
    }
    if (preparingToRelease)
    {
      if (hasVacuumable && vacuumable.Pending)
      {
        toShake.localPosition = toShakeDefaultPos + UnityEngine.Random.insideUnitSphere * vibrateAmplitude;
      }
      else
      {
        preparingToRelease = false;
        releaseAt = 0.0f;
        toShake.localPosition = toShakeDefaultPos;
      }
    }
    ProgressResource(model.progressTime);
    if (!(rotInProgressMat != null))
      return;
    float num = Mathf.Min(1f, rotInProgressMat.GetFloat("_Rot") + 0.5f * Time.deltaTime);
    if (num < 1.0)
    {
      rotInProgressMat.SetFloat("_Rot", num);
    }
    else
    {
      mainRenderer.sharedMaterial = rottenMat;
      Destroyer.Destroy(rotInProgressMat, "ResourceCycle.RegistryUpdate#2");
      rotInProgressMat = null;
    }
  }

  public void UpdateToNow() => ProgressResource(model.progressTime);

  public bool WouldProgressToRotten(double spawnTime, double worldTime)
  {
    double targetWorldTime = spawnTime + (unripeGameHours + (double) ripeGameHours + edibleGameHours) * 3600.0;
    return TimeUtil.HasReached(worldTime, targetWorldTime);
  }

  public void ProgressResource(double nextProgressionTime)
  {
    Rigidbody body = this.body;
    bool flag = timeDir.HasReached(model.progressTime + 3600.0);
    model.progressTime = nextProgressionTime;
    while (timeDir.HasReached(model.progressTime) || preparingToRelease && Time.time >= (double) releaseAt)
    {
      if (model.state == State.UNRIPE && timeDir.HasReached(model.progressTime))
      {
        Ripen();
        if (vacuumableWhenRipe)
          vacuumable.enabled = true;
        if (gameObject.transform.localScale.x < defaultScale.x * 0.33000001311302185)
          gameObject.transform.localScale = defaultScale * 0.33f;
        TweenUtil.ScaleTo(gameObject, defaultScale, 4f);
      }
      else if (model.state == State.RIPE && (preparingToRelease && Time.time >= (double) releaseAt || timeDir.HasReached(model.progressTime)))
      {
        MakeEdible();
        additionalRipenessDelegate = null;
        body.isKinematic = false;
        if (preparingToRelease)
        {
          preparingToRelease = false;
          releaseAt = 0.0f;
          toShake.localPosition = toShakeDefaultPos;
          if (releaseCue != null)
          {
            SECTR_PointSource component = GetComponent<SECTR_PointSource>();
            component.Cue = releaseCue;
            component.Play();
          }
        }
        body.WakeUp();
        Eject(body);
        DetachFromJoint();
        if (hasVacuumable)
          vacuumable.Pending = false;
      }
      else if (model.state == State.EDIBLE && timeDir.HasReached(model.progressTime))
      {
        Rot();
        SetRotten(false);
      }
      else if (model.state == State.ROTTEN && timeDir.HasReached(model.progressTime))
      {
        if (destroyFX != null && !flag)
          SpawnAndPlayFX(destroyFX, transform.position, transform.rotation);
        Destroyer.Destroy(gameObject, 0.0f, "ResourceCycle.ProgressResource", true);
        model.progressTime = double.MaxValue;
      }
    }
  }

  private void Ripen()
  {
    model.state = State.RIPE;
    AdvanceProgressTime(ripeGameHours);
  }

  private void MakeEdible()
  {
    model.state = State.EDIBLE;
    if (preservative != null)
      additionalRipenessDelegate = preservative.PreservativeRipenessModifier;
    AdvanceProgressTime(edibleGameHours);
  }

  private void Rot()
  {
    if (preservative != null)
      preservative.RemoveResourceCycle(this);
    model.state = State.ROTTEN;
    AdvanceProgressTime(rottenGameHours);
  }

  internal State GetState() => model.state;

  private void AdvanceProgressTime(float progressBaseAmount) => model.progressTime = Math.Min(timeDir.WorldTime(), model.progressTime) + Vary(progressBaseAmount) * 3600.0;

  public void Eject(Rigidbody rigidbody)
  {
    rigidbody.MoveRotation(Quaternion.Euler(5f, Randoms.SHARED.GetFloat(360f), 0.0f));
    rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 5f);
    if (!(joint != null) || !(joint.joint != null) || !addEjectionForce)
      return;
    rigidbody.AddForce(joint.joint.transform.up * 80f);
  }

  private void SetInitState(State state, double progressTime)
  {
    if ((state == State.UNRIPE || state == State.RIPE) && !AttachToNearest())
    {
      state = State.EDIBLE;
      progressTime = timeDir.HoursFromNow(Vary(edibleGameHours));
      Log.Debug("Could not find joint within patch", "resource", gameObject);
    }
    model.progressTime = progressTime;
    model.state = state;
    if (hasVacuumable && vacuumableWhenRipe && state != State.UNRIPE)
      vacuumable.enabled = true;
    transform.localScale = defaultScale * (state == State.UNRIPE ? 0.33f : 1f);
    if (state != State.ROTTEN)
      return;
    SetRotten(true);
  }

  private void SetRotten(bool immediate)
  {
    if (hasVacuumable)
      vacuumable.SetDestroyOnVac(true);
    SRSingleton<SceneContext>.Instance.GameModel.DestroyActorModel(gameObject);
    if (immediate)
    {
      mainRenderer.sharedMaterial = rottenMat;
    }
    else
    {
      mainRenderer.material = rottenMat;
      rotInProgressMat = mainRenderer.material;
      rotInProgressMat.SetFloat("_Rot", 0.0f);
    }
  }

  private bool AttachToNearest()
  {
    ZoneDirector zoneDirector;
    GingerPatchNode picked1;
    if (ident != null && ident.id == Identifiable.Id.GINGER_VEGGIE && ZoneDirector.zones.TryGetValue(ZoneDirector.Zone.DESERT, out zoneDirector) && GetNearestIdHandler(zoneDirector.GetCurrentGingerPatches(), 10f, out picked1))
    {
      picked1.Grow(gameObject);
      return true;
    }
    KookadobaNodeModel picked2;
    if (ident != null && ident.id == Identifiable.Id.KOOKADOBA_FRUIT && GetNearest(SRSingleton<SceneContext>.Instance.GameModel.AllKookadobaNodes(), 10f, out picked2))
    {
      picked2.Grow(gameObject);
      return true;
    }
    SpawnResourceModel picked3;
    if (GetNearest(SRSingleton<SceneContext>.Instance.GameModel.AllResourceSpawners(), 10f, out picked3))
    {
      Joint joint = picked3.NearestJoint(transform.position, 0.1f);
      if (joint != null)
      {
        Attach(joint);
        return true;
      }
    }
    return false;
  }

  private bool GetNearest<T>(IEnumerable<T> items, float distance, out T picked) where T : PositionalModel
  {
    picked = default (T);
    float num = distance * distance;
    foreach (T obj in items)
    {
      float sqrMagnitude = (obj.pos - transform.position).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        num = sqrMagnitude;
        picked = obj;
      }
    }
    return picked != null;
  }

  private bool GetNearestIdHandler<T>(IEnumerable<T> items, float distance, out T picked) where T : IdHandler
  {
    picked = default (T);
    float num = distance * distance;
    foreach (T obj in items)
    {
      float sqrMagnitude = (obj.transform.position - transform.position).sqrMagnitude;
      if (sqrMagnitude < (double) num)
      {
        num = sqrMagnitude;
        picked = obj;
      }
    }
    return picked != null;
  }

  [Serializable]
  public enum State
  {
    UNRIPE,
    RIPE,
    EDIBLE,
    ROTTEN,
  }

  [Serializable]
  public class CycleData
  {
    public State state;
    public float progressTime;

    public CycleData(State state, float progressTime)
    {
      this.state = state;
      this.progressTime = progressTime;
    }

    public override bool Equals(object o)
    {
      if (!(o is CycleData))
        return false;
      CycleData cycleData = (CycleData) o;
      return state == cycleData.state && progressTime == (double) cycleData.progressTime;
    }

    public override int GetHashCode() => state.GetHashCode() ^ progressTime.GetHashCode();

    public override string ToString() => state.ToString() + ":" + progressTime;
  }

  public delegate float AdditionalRipeness();

  public delegate void DetachmentEvent();
}
