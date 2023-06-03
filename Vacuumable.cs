// Decompiled with JetBrains decompiler
// Type: Vacuumable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Vacuumable : CollidableActorBehaviour, Collidable
{
  private const float CONSUME_SCALE_DOWN_TIME = 0.2f;
  private const float CONSUME_SCALE_FACTOR = 0.1f;
  public Size size;
  [Tooltip("Audio played when this object is shot out of the vacuum.")]
  public SECTR_AudioCue onLaunchCue;
  private bool ignoresGravity;
  private Joint captiveToJoint;
  private bool held;
  private bool launched;
  private float nextVacTime;
  private bool destroyOnVac;
  private const float VAC_RELEASE_DELAY = 1f;
  private const float HELD_OPACITY = 0.75f;
  private const string LAUNCHED_LAYER = "Launched";
  private const string CAPTIVE_LAYER = "ActorIgnorePlayer";
  private const string TORNADOED_LAYER = "ActorEchoes";
  private const string HELD_LAYER = "Held";
  private int defaultLayer;
  private SlimeFaceAnimator sfAnimator;
  protected Rigidbody body;
  private VacDelaunchTrigger delaunchTrigger;
  private Identifiable identifiable;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private bool isTornadoed;
  private double lastPending = double.NaN;
  private int heldShaderPropertyId;
  private TweenerCore<Vector3, Vector3, VectorOptions> consumeScaleTween;
  private float consumeStartScale;

  public event OnSetHeld onSetHeld;

  public event OnSetLaunched onSetLaunched;

  public event Consume consume;

  public override void Awake()
  {
    base.Awake();
    defaultLayer = gameObject.layer;
    sfAnimator = GetComponent<SlimeFaceAnimator>();
    body = GetComponent<Rigidbody>();
    identifiable = GetComponent<Identifiable>();
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    if (slimeAppearanceApplicator != null)
      slimeAppearanceApplicator.OnAppearanceChanged += appearance => ForceUpdateLayer();
    ignoresGravity = !body.useGravity;
    VacDelaunchTrigger[] componentsInChildren = GetComponentsInChildren<VacDelaunchTrigger>(true);
    if (componentsInChildren != null && componentsInChildren.Length != 0)
      delaunchTrigger = componentsInChildren[0];
    heldShaderPropertyId = Shader.PropertyToID("_HeldInVac");
    consumeStartScale = transform.localScale.x;
  }

  public bool TryConsume()
  {
    if (consume != null)
    {
      consume();
      return true;
    }
    if (!SRSingleton<SceneContext>.Instance.PlayerState.Ammo.MaybeAddToSlot(identifiable.id, identifiable))
      return false;
    Destroyer.DestroyActor(transform.gameObject, "Vacuumable.consume");
    return true;
  }

  public void PreventCaptureFor(float seconds) => nextVacTime = Time.time + seconds;

  public bool canCapture() => Time.time >= (double) nextVacTime;

  public void capture(Joint toJoint)
  {
    if (captiveToJoint == null && sfAnimator != null)
      sfAnimator.SetTrigger("triggerAwe");
    if (body != null)
      body.isKinematic = false;
    SetCaptive(toJoint);
  }

  public bool Pending
  {
    get
    {
      if (double.IsNaN(lastPending))
        return false;
      if (Time.time <= lastPending + Time.deltaTime + 1.0 / 1000.0)
        return true;
      lastPending = double.NaN;
      return false;
    }
    set
    {
      if (value)
        lastPending = Time.time;
      else
        lastPending = double.NaN;
    }
  }

  public void release()
  {
    if (isCaptive() || isHeld())
      PreventCaptureFor(1f);
    SetCaptive(null);
    SetHeld(false);
    SetTornadoed(false);
    Pending = false;
  }

  public void hold()
  {
    if (isCaptive())
      SetCaptive(null);
    SetHeld(true);
  }

  public bool isCaptive() => captiveToJoint != null;

  public void SetTornadoed(bool tornadoed)
  {
    isTornadoed = tornadoed;
    UpdateLayer();
  }

  public bool IsTornadoed() => isTornadoed;

  public bool isHeld() => held;

  public bool isLaunched() => launched;

  public void Launch(LaunchSource source)
  {
    if (source == LaunchSource.PLAYER)
      SECTR_AudioSystem.Play(onLaunchCue, transform.position, false);
    SetLaunched(true);
  }

  public bool delaunch()
  {
    if (!launched)
      return false;
    SetLaunched(false);
    return true;
  }

  public void SetDestroyOnVac(bool destroy) => destroyOnVac = destroy;

  public bool GetDestroyOnVac() => destroyOnVac;

  protected virtual void SetCaptive(Joint toJoint)
  {
    if (captiveToJoint != null)
      Destroyer.Destroy(captiveToJoint.gameObject, "Vacuumable.SetCaptive");
    captiveToJoint = toJoint;
    body.useGravity = captiveToJoint == null && !ignoresGravity;
    if (captiveToJoint != null)
      captiveToJoint.connectedBody = body;
    UpdateLayer();
  }

  private void SetHeld(bool held)
  {
    if (this.held == held)
      return;
    this.held = held;
    if (this.held)
      delaunch();
    SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>().cameraHeld.SetActive(this.held);
    UpdateLayer();
    UpdateMaterialsHeldState();
    if (onSetHeld == null)
      return;
    onSetHeld(this.held);
  }

  public void ProcessCollisionEnter(Collision col)
  {
    if (!launched || col.collider.isTrigger || col.collider is CharacterController)
      return;
    SetLaunched(false);
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  private void UpdateMaterialsHeldState()
  {
    foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
    {
      for (int materialIndex = 0; materialIndex < componentsInChild.sharedMaterials.Length; ++materialIndex)
      {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        componentsInChild.GetPropertyBlock(properties, materialIndex);
        properties.SetInt(heldShaderPropertyId, held ? 1 : 0);
        componentsInChild.SetPropertyBlock(properties, materialIndex);
      }
    }
  }

  private void UpdateLayer(bool isForced = false)
  {
    if (launched && delaunchTrigger != null)
      SetLayerRecursively(LayerMask.NameToLayer("Launched"), isForced);
    else if (isHeld())
      SetLayerRecursively(LayerMask.NameToLayer("Held"), isForced);
    else if (IsTornadoed())
      SetLayerRecursively(LayerMask.NameToLayer("ActorEchoes"), isForced);
    else if (isCaptive())
      SetLayerRecursively(LayerMask.NameToLayer("ActorIgnorePlayer"), isForced);
    else
      SetLayerRecursively(defaultLayer, isForced);
  }

  public void ForceUpdateLayer() => UpdateLayer(true);

  private void SetLayerRecursively(int layerNumber, bool isForced)
  {
    if (!isForced && gameObject.layer == layerNumber)
      return;
    foreach (Component componentsInChild in gameObject.GetComponentsInChildren<Transform>(true))
      componentsInChild.gameObject.layer = layerNumber;
  }

  private void SetLaunched(bool launched)
  {
    if (launched == this.launched)
      return;
    this.launched = launched;
    UpdateLayer();
    if (delaunchTrigger != null)
      delaunchTrigger.SetTriggerEnabled(launched);
    if (launched)
    {
      gameObject.AddComponent<DontGoThroughThings>();
    }
    else
    {
      DontGoThroughThings component = gameObject.GetComponent<DontGoThroughThings>();
      if (component != null)
        component.AllowDestroy();
    }
    if (onSetLaunched == null)
      return;
    onSetLaunched(this.launched);
  }

  public Joint CaptiveToJoint() => captiveToJoint;

  public void StartConsumeScale()
  {
    if (consumeScaleTween != null && consumeScaleTween.IsActive())
      return;
    consumeScaleTween = gameObject.transform.DOScale(consumeStartScale * 0.1f, 0.2f).SetEase(Ease.Linear);
  }

  public void ReverseConsumeScale()
  {
    if (consumeScaleTween != null && consumeScaleTween.IsPlaying())
      consumeScaleTween.Flip();
    else
      gameObject.transform.DOScale(consumeStartScale, 0.2f).SetEase(Ease.Linear);
    consumeScaleTween = null;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (captiveToJoint != null)
      Destroyer.Destroy(captiveToJoint.gameObject, "Vacuumable.OnDestroy");
    if (!held || !(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.Player != null))
      return;
    SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>().cameraHeld.SetActive(false);
  }

  public enum Size
  {
    NORMAL,
    LARGE,
    GIANT,
  }

  public enum LaunchSource
  {
    PLAYER,
    CHICKEN_CLONER,
  }

  public delegate void OnSetHeld(bool held);

  public delegate void OnSetLaunched(bool launched);

  public delegate void Consume();
}
