// Decompiled with JetBrains decompiler
// Type: vp_FPWeapon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class vp_FPWeapon : vp_Weapon
{
  public GameObject WeaponPrefab;
  protected CharacterController Controller;
  public float RenderingZoomDamping = 0.5f;
  protected float m_FinalZoomTime;
  public float RenderingZScale = 1f;
  public float PositionSpringStiffness = 0.01f;
  public float PositionSpringDamping = 0.25f;
  public float PositionFallRetract = 1f;
  public float PositionPivotSpringStiffness = 0.01f;
  public float PositionPivotSpringDamping = 0.25f;
  public float PositionKneeling = 0.06f;
  public int PositionKneelingSoftness = 1;
  public Vector3 PositionWalkSlide = new Vector3(0.5f, 0.75f, 0.5f);
  public Vector3 PositionPivot = Vector3.zero;
  public Vector3 RotationPivot = Vector3.zero;
  public float PositionInputVelocityScale = 1f;
  public float PositionMaxInputVelocity = 25f;
  protected vp_Spring m_PositionSpring;
  protected vp_Spring m_PositionPivotSpring;
  protected vp_Spring m_RotationPivotSpring;
  protected GameObject m_WeaponCamera;
  protected GameObject m_WeaponGroup;
  protected GameObject m_Pivot;
  protected Transform m_WeaponGroupTransform;
  public float RotationSpringStiffness = 0.01f;
  public float RotationSpringDamping = 0.25f;
  public float RotationPivotSpringStiffness = 0.01f;
  public float RotationPivotSpringDamping = 0.25f;
  public float RotationKneeling;
  public int RotationKneelingSoftness = 1;
  public Vector3 RotationLookSway = new Vector3(1f, 0.7f, 0.0f);
  public Vector3 RotationStrafeSway = new Vector3(0.3f, 1f, 1.5f);
  public Vector3 RotationFallSway = new Vector3(1f, -0.5f, -3f);
  public float RotationSlopeSway = 0.5f;
  public float RotationInputVelocityScale = 1f;
  public float RotationMaxInputVelocity = 15f;
  protected vp_Spring m_RotationSpring;
  protected Vector3 m_SwayVel = Vector3.zero;
  protected Vector3 m_FallSway = Vector3.zero;
  public float RetractionDistance;
  public Vector2 RetractionOffset = new Vector2(0.0f, 0.0f);
  public float RetractionRelaxSpeed = 0.25f;
  protected bool m_DrawRetractionDebugLine;
  public float ShakeSpeed = 0.05f;
  public Vector3 ShakeAmplitude = new Vector3(0.25f, 0.0f, 2f);
  protected Vector3 m_Shake = Vector3.zero;
  public Vector4 BobRate = new Vector4(0.9f, 0.45f, 0.0f, 0.0f);
  public Vector4 BobAmplitude = new Vector4(0.35f, 0.5f, 0.0f, 0.0f);
  public float BobInputVelocityScale = 1f;
  public float BobMaxInputVelocity = 100f;
  public bool BobRequireGroundContact = true;
  protected float m_LastBobSpeed;
  protected Vector4 m_CurrentBobAmp = Vector4.zero;
  protected Vector4 m_CurrentBobVal = Vector4.zero;
  protected float m_BobSpeed;
  public Vector3 StepPositionForce = new Vector3(0.0f, -0.0012f, -0.0012f);
  public Vector3 StepRotationForce = new Vector3(0.0f, 0.0f, 0.0f);
  public int StepSoftness = 4;
  public float StepMinVelocity;
  public float StepPositionBalance;
  public float StepRotationBalance;
  public float StepForceScale = 1f;
  protected float m_LastUpBob;
  protected bool m_BobWasElevating;
  protected Vector3 m_PosStep = Vector3.zero;
  protected Vector3 m_RotStep = Vector3.zero;
  public bool LookDownActive;
  public float LookDownYawLimit = 60f;
  public Vector3 LookDownPositionOffsetMiddle = new Vector3(0.32f, -0.37f, 0.78f);
  public Vector3 LookDownPositionOffsetLeft = new Vector3(0.27f, -0.31f, 0.7f);
  public Vector3 LookDownPositionOffsetRight = new Vector3(0.6f, -0.41f, 0.86f);
  public float LookDownPositionSpringPower = 1f;
  public Vector3 LookDownRotationOffsetMiddle = new Vector3(-3.9f, 2.24f, 4.69f);
  public Vector3 LookDownRotationOffsetLeft = new Vector3(-7f, -10.5f, 15.6f);
  public Vector3 LookDownRotationOffsetRight = new Vector3(-9.2f, -9.8f, 48.84f);
  public float LookDownRotationSpringPower = 1f;
  protected Vector3 m_CurrentPosRestState = Vector3.zero;
  protected Vector3 m_CurrentRotRestState = Vector3.zero;
  protected AnimationCurve m_LookDownCurve = new AnimationCurve(new Keyframe[3]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(0.8f, 0.2f, 0.9f, 1.5f),
    new Keyframe(1f, 1f)
  });
  protected float m_LookDownPitch;
  protected float m_LookDownYaw;
  public AudioClip SoundWield;
  public AudioClip SoundUnWield;
  public AnimationClip AnimationWield;
  public AnimationClip AnimationUnWield;
  public List<Object> AnimationAmbient = new List<Object>();
  protected List<bool> m_AmbAnimPlayed = new List<bool>();
  public Vector2 AmbientInterval = new Vector2(2.5f, 7.5f);
  protected int m_CurrentAmbientAnimation;
  protected vp_Timer.Handle m_AnimationAmbientTimer = new vp_Timer.Handle();
  public Vector3 PositionExitOffset = new Vector3(0.0f, -1f, 0.0f);
  public Vector3 RotationExitOffset = new Vector3(40f, 0.0f, 0.0f);
  protected Vector2 m_LookInput = Vector2.zero;
  protected const float LOOKDOWNSPEED = 2f;
  private vp_FPPlayerEventHandler m_FPPlayer;

  public GameObject WeaponCamera => m_WeaponCamera;

  public GameObject WeaponModel => m_WeaponModel;

  public Vector3 DefaultPosition => (Vector3) DefaultState.Preset.GetFieldValue("PositionOffset");

  public Vector3 DefaultRotation => (Vector3) DefaultState.Preset.GetFieldValue("RotationOffset");

  public bool DrawRetractionDebugLine
  {
    get => m_DrawRetractionDebugLine;
    set => m_DrawRetractionDebugLine = value;
  }

  private vp_FPPlayerEventHandler FPPlayer
  {
    get
    {
      if (m_FPPlayer == null && EventHandler != null)
        m_FPPlayer = EventHandler as vp_FPPlayerEventHandler;
      return m_FPPlayer;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (transform.parent == null)
    {
      Debug.LogError("Error (" + this + ") Must not be placed in scene root. Disabling self.");
      vp_Utility.Activate(gameObject, false);
    }
    else
    {
      Controller = Transform.root.GetComponent<CharacterController>();
      if (Controller == null)
      {
        Debug.LogError("Error (" + this + ") Could not find CharacterController. Disabling self.");
        vp_Utility.Activate(gameObject, false);
      }
      else
      {
        Transform.eulerAngles = Vector3.zero;
        foreach (Component component1 in Transform.parent)
        {
          Camera component2 = (Camera) component1.GetComponent(typeof (Camera));
          if (component2 != null)
          {
            m_WeaponCamera = component2.gameObject;
            break;
          }
        }
        if (!(GetComponent<Collider>() != null))
          return;
        GetComponent<Collider>().enabled = false;
      }
    }
  }

  protected override void Start()
  {
    InstantiateWeaponModel();
    base.Start();
    m_WeaponGroup = new GameObject(name + "Transform");
    m_WeaponGroupTransform = m_WeaponGroup.transform;
    m_WeaponGroupTransform.parent = Transform.parent;
    m_WeaponGroupTransform.localPosition = PositionOffset;
    vp_Layer.Set(m_WeaponGroup, 31);
    Transform.parent = m_WeaponGroupTransform;
    Transform.localPosition = Vector3.zero;
    m_WeaponGroupTransform.localEulerAngles = RotationOffset;
    if (m_WeaponCamera != null && vp_Utility.IsActive(m_WeaponCamera.gameObject))
      vp_Layer.Set(gameObject, 31, true);
    m_Pivot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    m_Pivot.name = "Pivot";
    m_Pivot.GetComponent<Collider>().enabled = false;
    m_Pivot.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    m_Pivot.transform.parent = m_WeaponGroupTransform;
    m_Pivot.transform.localPosition = Vector3.zero;
    m_Pivot.layer = 31;
    vp_Utility.Activate(m_Pivot.gameObject, false);
    m_Pivot.GetComponent<Renderer>().material = new Material(Shader.Find("Transparent/Diffuse"))
    {
      color = new Color(0.0f, 0.0f, 1f, 0.5f)
    };
    m_PositionSpring = new vp_Spring(m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.Position);
    m_PositionSpring.RestState = PositionOffset;
    m_PositionPivotSpring = new vp_Spring(Transform, vp_Spring.UpdateMode.Position);
    m_PositionPivotSpring.RestState = PositionPivot;
    m_PositionSpring2 = new vp_Spring(Transform, vp_Spring.UpdateMode.PositionAdditiveLocal);
    m_PositionSpring2.MinVelocity = 1E-05f;
    m_RotationSpring = new vp_Spring(m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.Rotation);
    m_RotationSpring.RestState = RotationOffset;
    m_RotationPivotSpring = new vp_Spring(Transform, vp_Spring.UpdateMode.Rotation);
    m_RotationPivotSpring.RestState = RotationPivot;
    m_RotationSpring2 = new vp_Spring(m_WeaponGroup.gameObject.transform, vp_Spring.UpdateMode.RotationAdditiveLocal);
    m_RotationSpring2.MinVelocity = 1E-05f;
    SnapSprings();
    Refresh();
  }

  public virtual void InstantiateWeaponModel()
  {
    if (WeaponPrefab != null)
    {
      if (m_WeaponModel != null && m_WeaponModel != gameObject)
        Destroyer.Destroy(m_WeaponModel, "vp_FPWeapon.InstantiateWeaponModel");
      m_WeaponModel = Instantiate(WeaponPrefab);
      m_WeaponModel.transform.parent = transform;
      m_WeaponModel.transform.localPosition = Vector3.zero;
      m_WeaponModel.transform.localScale = new Vector3(1f, 1f, RenderingZScale);
      m_WeaponModel.transform.localEulerAngles = Vector3.zero;
      if (m_WeaponCamera != null && vp_Utility.IsActive(m_WeaponCamera.gameObject))
        vp_Layer.Set(m_WeaponModel, 31, true);
    }
    else
      m_WeaponModel = gameObject;
    CacheRenderers();
  }

  protected override void Init()
  {
    base.Init();
    ScheduleAmbientAnimation();
  }

  protected override void Update()
  {
    base.Update();
    if (Time.timeScale == 0.0)
      return;
    UpdateInput();
  }

  protected override void FixedUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    UpdateZoom();
    UpdateSwaying();
    UpdateBob();
    UpdateEarthQuake();
    UpdateStep();
    UpdateShakes();
    UpdateRetraction();
    UpdateSprings();
    UpdateLookDown();
  }

  protected override void LateUpdate()
  {
  }

  public virtual void AddForce(Vector3 force) => m_PositionSpring.AddForce(force);

  public virtual void AddForce(float x, float y, float z) => AddForce(new Vector3(x, y, z));

  public virtual void AddForce(Vector3 positional, Vector3 angular)
  {
    m_PositionSpring.AddForce(positional);
    m_RotationSpring.AddForce(angular);
  }

  public virtual void AddForce(
    float xPos,
    float yPos,
    float zPos,
    float xRot,
    float yRot,
    float zRot)
  {
    AddForce(new Vector3(xPos, yPos, zPos), new Vector3(xRot, yRot, zRot));
  }

  public virtual void AddSoftForce(Vector3 force, int frames) => m_PositionSpring.AddSoftForce(force, frames);

  public virtual void AddSoftForce(float x, float y, float z, int frames) => AddSoftForce(new Vector3(x, y, z), frames);

  public virtual void AddSoftForce(Vector3 positional, Vector3 angular, int frames)
  {
    m_PositionSpring.AddSoftForce(positional, frames);
    m_RotationSpring.AddSoftForce(angular, frames);
  }

  public virtual void AddSoftForce(
    float xPos,
    float yPos,
    float zPos,
    float xRot,
    float yRot,
    float zRot,
    int frames)
  {
    AddSoftForce(new Vector3(xPos, yPos, zPos), new Vector3(xRot, yRot, zRot), frames);
  }

  protected virtual void UpdateInput()
  {
    if (Player.Dead.Active)
      return;
    m_LookInput = FPPlayer.InputRawLook.Get() / Delta * Time.timeScale * Time.timeScale;
    m_LookInput *= RotationInputVelocityScale;
    m_LookInput = Vector3.Min(m_LookInput, Vector3.one * RotationMaxInputVelocity);
    m_LookInput = Vector3.Max(m_LookInput, Vector3.one * -RotationMaxInputVelocity);
  }

  protected virtual void UpdateZoom()
  {
    if (m_FinalZoomTime <= (double) Time.time || !m_Wielded)
      return;
    RenderingZoomDamping = Mathf.Max(RenderingZoomDamping, 0.01f);
  }

  public virtual void Zoom() => m_FinalZoomTime = Time.time + RenderingZoomDamping;

  public virtual void SnapZoom()
  {
  }

  protected virtual void UpdateShakes()
  {
    if (ShakeSpeed == 0.0)
      return;
    m_Shake = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(ShakeSpeed), ShakeAmplitude);
    m_RotationSpring.AddForce(m_Shake * Time.timeScale);
  }

  protected virtual void UpdateRetraction(bool firstIteration = true)
  {
    if (RetractionDistance == 0.0)
      return;
    Vector3 start = WeaponModel.transform.TransformPoint(RetractionOffset);
    Vector3 end = start + WeaponModel.transform.forward * RetractionDistance;
    RaycastHit hitInfo;
    if (Physics.Linecast(start, end, out hitInfo, -675375893) && !hitInfo.collider.isTrigger)
    {
      WeaponModel.transform.position = hitInfo.point - (hitInfo.point - start).normalized * (RetractionDistance * 0.99f);
      WeaponModel.transform.localPosition = Vector3.forward * Mathf.Min(WeaponModel.transform.localPosition.z, 0.0f);
    }
    else
    {
      if (!firstIteration || !(WeaponModel.transform.localPosition != Vector3.zero) || !(WeaponModel != gameObject))
        return;
      WeaponModel.transform.localPosition = Vector3.forward * Mathf.SmoothStep(WeaponModel.transform.localPosition.z, 0.0f, RetractionRelaxSpeed * Time.timeScale);
      UpdateRetraction(false);
    }
  }

  protected virtual void UpdateBob()
  {
    if (BobAmplitude == Vector4.zero || BobRate == Vector4.zero)
      return;
    m_BobSpeed = !BobRequireGroundContact || Controller.isGrounded ? Controller.velocity.sqrMagnitude : 0.0f;
    m_BobSpeed = Mathf.Min(m_BobSpeed * BobInputVelocityScale, BobMaxInputVelocity);
    m_BobSpeed = Mathf.Round(m_BobSpeed * 1000f) / 1000f;
    if (m_BobSpeed == 0.0)
      m_BobSpeed = Mathf.Min(m_LastBobSpeed * 0.93f, BobMaxInputVelocity);
    m_CurrentBobAmp.x = m_BobSpeed * (BobAmplitude.x * -0.0001f);
    m_CurrentBobVal.x = Mathf.Cos(Time.time * (BobRate.x * 10f)) * m_CurrentBobAmp.x;
    m_CurrentBobAmp.y = m_BobSpeed * (BobAmplitude.y * 0.0001f);
    m_CurrentBobVal.y = Mathf.Cos(Time.time * (BobRate.y * 10f)) * m_CurrentBobAmp.y;
    m_CurrentBobAmp.z = m_BobSpeed * (BobAmplitude.z * 0.0001f);
    m_CurrentBobVal.z = Mathf.Cos(Time.time * (BobRate.z * 10f)) * m_CurrentBobAmp.z;
    m_CurrentBobAmp.w = m_BobSpeed * (BobAmplitude.w * 0.0001f);
    m_CurrentBobVal.w = Mathf.Cos(Time.time * (BobRate.w * 10f)) * m_CurrentBobAmp.w;
    m_RotationSpring.AddForce(m_CurrentBobVal * Time.timeScale);
    m_PositionSpring.AddForce(Vector3.forward * m_CurrentBobVal.w * Time.timeScale);
    m_LastBobSpeed = m_BobSpeed;
  }

  protected virtual void UpdateEarthQuake()
  {
    if (FPPlayer == null || !FPPlayer.CameraEarthQuake.Active || !Controller.isGrounded)
      return;
    Vector3 vector3 = FPPlayer.CameraEarthQuakeForce.Get();
    AddForce(new Vector3(0.0f, 0.0f, (float) (-(double) vector3.z * 0.014999999664723873)), new Vector3(vector3.y * 2f, -vector3.x, vector3.x * 2f));
  }

  protected override void UpdateSprings()
  {
    m_PositionSpring.FixedUpdate();
    m_PositionPivotSpring.FixedUpdate();
    m_RotationPivotSpring.FixedUpdate();
    m_RotationSpring.FixedUpdate();
    m_PositionSpring2.FixedUpdate();
    m_RotationSpring2.FixedUpdate();
  }

  private void UpdateLookDown()
  {
    if (!LookDownActive || FPPlayer.Rotation.Get().x < 0.0 && m_LookDownPitch == 0.0 && m_LookDownYaw == 0.0)
      return;
    if (FPPlayer.Rotation.Get().x > 0.0)
    {
      m_LookDownPitch = Mathf.Lerp(m_LookDownPitch, vp_MathUtility.SnapToZero(Mathf.Max(0.0f, FPPlayer.Rotation.Get().x / 90f)), Time.deltaTime * 2f);
      m_LookDownYaw = Mathf.Lerp(m_LookDownYaw, vp_MathUtility.SnapToZero(Mathf.DeltaAngle(FPPlayer.Rotation.Get().y, FPPlayer.BodyYaw.Get())) / 90f * vp_MathUtility.SnapToZero(Mathf.Max(0.0f, (float) ((FPPlayer.Rotation.Get().x - (double) LookDownYawLimit) / (90.0 - LookDownYawLimit)))), Time.deltaTime * 2f);
    }
    else
    {
      m_LookDownPitch *= 0.9f;
      m_LookDownYaw *= 0.9f;
      if (m_LookDownPitch < 0.0099999997764825821)
        m_LookDownPitch = 0.0f;
      if (m_LookDownYaw < 0.0099999997764825821)
        m_LookDownYaw = 0.0f;
    }
    m_WeaponGroupTransform.localPosition = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_WeaponGroupTransform.localPosition, LookDownPositionOffsetMiddle, m_LookDownCurve.Evaluate(m_LookDownPitch)));
    m_WeaponGroupTransform.localRotation = vp_MathUtility.NaNSafeQuaternion(Quaternion.Slerp(m_WeaponGroupTransform.localRotation, Quaternion.Euler(LookDownRotationOffsetMiddle), m_LookDownPitch));
    if (m_LookDownYaw > 0.0)
    {
      m_WeaponGroupTransform.localPosition = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_WeaponGroupTransform.localPosition, LookDownPositionOffsetLeft, Mathf.SmoothStep(0.0f, 1f, m_LookDownYaw)));
      m_WeaponGroupTransform.localRotation = vp_MathUtility.NaNSafeQuaternion(Quaternion.Slerp(m_WeaponGroupTransform.localRotation, Quaternion.Euler(LookDownRotationOffsetLeft), m_LookDownYaw));
    }
    else
    {
      m_WeaponGroupTransform.localPosition = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_WeaponGroupTransform.localPosition, LookDownPositionOffsetRight, Mathf.SmoothStep(0.0f, 1f, -m_LookDownYaw)));
      m_WeaponGroupTransform.localRotation = vp_MathUtility.NaNSafeQuaternion(Quaternion.Slerp(m_WeaponGroupTransform.localRotation, Quaternion.Euler(LookDownRotationOffsetRight), -m_LookDownYaw));
    }
    m_CurrentPosRestState = Vector3.Lerp(m_CurrentPosRestState, m_PositionSpring.RestState, Time.fixedDeltaTime);
    m_CurrentRotRestState = Vector3.Lerp(m_CurrentRotRestState, m_RotationSpring.RestState, Time.fixedDeltaTime);
    m_WeaponGroupTransform.localPosition += vp_MathUtility.NaNSafeVector3((m_PositionSpring.State - m_CurrentPosRestState) * (m_LookDownPitch * LookDownPositionSpringPower));
    m_WeaponGroupTransform.localEulerAngles -= vp_MathUtility.NaNSafeVector3(new Vector3(Mathf.DeltaAngle(m_RotationSpring.State.x, m_CurrentRotRestState.x), Mathf.DeltaAngle(m_RotationSpring.State.y, m_CurrentRotRestState.y), Mathf.DeltaAngle(m_RotationSpring.State.z, m_CurrentRotRestState.z)) * (m_LookDownPitch * LookDownRotationSpringPower));
  }

  protected virtual void UpdateStep()
  {
    if (StepMinVelocity <= 0.0 || BobRequireGroundContact && !Controller.isGrounded || Controller.velocity.sqrMagnitude < (double) StepMinVelocity)
      return;
    bool flag = m_LastUpBob < (double) m_CurrentBobVal.x;
    m_LastUpBob = m_CurrentBobVal.x;
    if (flag && !m_BobWasElevating)
    {
      if (Mathf.Cos(Time.time * (BobRate.x * 5f)) > 0.0)
      {
        m_PosStep = StepPositionForce - StepPositionForce * StepPositionBalance;
        m_RotStep = StepRotationForce - StepPositionForce * StepRotationBalance;
      }
      else
      {
        m_PosStep = StepPositionForce + StepPositionForce * StepPositionBalance;
        m_RotStep = Vector3.Scale(StepRotationForce - StepPositionForce * StepRotationBalance, -Vector3.one + Vector3.right * 2f);
      }
      AddSoftForce(m_PosStep * StepForceScale, m_RotStep * StepForceScale, StepSoftness);
    }
    m_BobWasElevating = flag;
  }

  protected virtual void UpdateSwaying()
  {
    m_SwayVel = Controller.velocity * PositionInputVelocityScale;
    m_SwayVel = Vector3.Min(m_SwayVel, Vector3.one * PositionMaxInputVelocity);
    m_SwayVel = Vector3.Max(m_SwayVel, Vector3.one * -PositionMaxInputVelocity);
    m_SwayVel *= Time.timeScale;
    Vector3 vector3 = Transform.InverseTransformDirection(m_SwayVel / 60f);
    m_RotationSpring.AddForce(new Vector3(m_LookInput.y * (RotationLookSway.x * 0.025f), m_LookInput.x * (RotationLookSway.y * -0.025f), m_LookInput.x * (RotationLookSway.z * -0.025f)));
    m_FallSway = RotationFallSway * (m_SwayVel.y * 0.005f);
    if (Controller.isGrounded)
      m_FallSway *= RotationSlopeSway;
    m_FallSway.z = Mathf.Max(0.0f, m_FallSway.z);
    m_RotationSpring.AddForce(m_FallSway);
    m_PositionSpring.AddForce(Vector3.forward * -Mathf.Abs(m_SwayVel.y * (PositionFallRetract * 2.5E-05f)));
    m_PositionSpring.AddForce(new Vector3(vector3.x * (PositionWalkSlide.x * (1f / 625f)), -Mathf.Abs(vector3.x * (PositionWalkSlide.y * (1f / 625f))), (float) (-(double) vector3.z * (PositionWalkSlide.z * (1.0 / 625.0)))));
    m_RotationSpring.AddForce(new Vector3(-Mathf.Abs(vector3.x * (RotationStrafeSway.x * 0.16f)), (float) -(vector3.x * (RotationStrafeSway.y * 0.15999999642372131)), vector3.x * (RotationStrafeSway.z * 0.16f)));
  }

  public virtual void ResetSprings(
    float positionReset,
    float rotationReset,
    float positionPauseTime = 0.0f,
    float rotationPauseTime = 0.0f)
  {
    m_PositionSpring.State = Vector3.Lerp(m_PositionSpring.State, m_PositionSpring.RestState, positionReset);
    m_RotationSpring.State = Vector3.Lerp(m_RotationSpring.State, m_RotationSpring.RestState, rotationReset);
    m_PositionPivotSpring.State = Vector3.Lerp(m_PositionPivotSpring.State, m_PositionPivotSpring.RestState, positionReset);
    m_RotationPivotSpring.State = Vector3.Lerp(m_RotationPivotSpring.State, m_RotationPivotSpring.RestState, rotationReset);
    if (positionPauseTime != 0.0)
      m_PositionSpring.ForceVelocityFadeIn(positionPauseTime);
    if (rotationPauseTime != 0.0)
      m_RotationSpring.ForceVelocityFadeIn(rotationPauseTime);
    if (positionPauseTime != 0.0)
      m_PositionPivotSpring.ForceVelocityFadeIn(positionPauseTime);
    if (rotationPauseTime == 0.0)
      return;
    m_RotationPivotSpring.ForceVelocityFadeIn(rotationPauseTime);
  }

  public override void Refresh()
  {
    if (!Application.isPlaying)
      return;
    if (m_PositionSpring != null)
    {
      m_PositionSpring.Stiffness = new Vector3(PositionSpringStiffness, PositionSpringStiffness, PositionSpringStiffness);
      m_PositionSpring.Damping = Vector3.one - new Vector3(PositionSpringDamping, PositionSpringDamping, PositionSpringDamping);
      m_PositionSpring.RestState = PositionOffset - PositionPivot;
    }
    if (m_PositionPivotSpring != null)
    {
      m_PositionPivotSpring.Stiffness = new Vector3(PositionPivotSpringStiffness, PositionPivotSpringStiffness, PositionPivotSpringStiffness);
      m_PositionPivotSpring.Damping = Vector3.one - new Vector3(PositionPivotSpringDamping, PositionPivotSpringDamping, PositionPivotSpringDamping);
      m_PositionPivotSpring.RestState = PositionPivot;
    }
    if (m_RotationPivotSpring != null)
    {
      m_RotationPivotSpring.Stiffness = new Vector3(RotationPivotSpringStiffness, RotationPivotSpringStiffness, RotationPivotSpringStiffness);
      m_RotationPivotSpring.Damping = Vector3.one - new Vector3(RotationPivotSpringDamping, RotationPivotSpringDamping, RotationPivotSpringDamping);
      m_RotationPivotSpring.RestState = RotationPivot;
    }
    if (m_PositionSpring2 != null)
    {
      m_PositionSpring2.Stiffness = new Vector3(PositionSpring2Stiffness, PositionSpring2Stiffness, PositionSpring2Stiffness);
      m_PositionSpring2.Damping = Vector3.one - new Vector3(PositionSpring2Damping, PositionSpring2Damping, PositionSpring2Damping);
      m_PositionSpring2.RestState = Vector3.zero;
    }
    if (m_RotationSpring != null)
    {
      m_RotationSpring.Stiffness = new Vector3(RotationSpringStiffness, RotationSpringStiffness, RotationSpringStiffness);
      m_RotationSpring.Damping = Vector3.one - new Vector3(RotationSpringDamping, RotationSpringDamping, RotationSpringDamping);
      m_RotationSpring.RestState = RotationOffset;
    }
    if (m_RotationSpring2 != null)
    {
      m_RotationSpring2.Stiffness = new Vector3(RotationSpring2Stiffness, RotationSpring2Stiffness, RotationSpring2Stiffness);
      m_RotationSpring2.Damping = Vector3.one - new Vector3(RotationSpring2Damping, RotationSpring2Damping, RotationSpring2Damping);
      m_RotationSpring2.RestState = Vector3.zero;
    }
    if (!Rendering)
      return;
    Zoom();
  }

  public override void Activate()
  {
    base.Activate();
    SnapZoom();
    if (m_WeaponGroup != null && !vp_Utility.IsActive(m_WeaponGroup))
      vp_Utility.Activate(m_WeaponGroup);
    SetPivotVisible(false);
  }

  public override void Deactivate()
  {
    m_Wielded = false;
    if (!(m_WeaponGroup != null) || !vp_Utility.IsActive(m_WeaponGroup))
      return;
    vp_Utility.Activate(m_WeaponGroup, false);
  }

  public virtual void SnapPivot()
  {
    if (m_PositionSpring != null)
    {
      m_PositionSpring.RestState = PositionOffset - PositionPivot;
      m_PositionSpring.State = PositionOffset - PositionPivot;
    }
    if (m_WeaponGroup != null)
      m_WeaponGroupTransform.localPosition = PositionOffset - PositionPivot;
    if (m_PositionPivotSpring != null)
    {
      m_PositionPivotSpring.RestState = PositionPivot;
      m_PositionPivotSpring.State = PositionPivot;
    }
    if (m_RotationPivotSpring != null)
    {
      m_RotationPivotSpring.RestState = RotationPivot;
      m_RotationPivotSpring.State = RotationPivot;
    }
    Transform.localPosition = PositionPivot;
    Transform.localEulerAngles = RotationPivot;
  }

  public virtual void SetPivotVisible(bool visible)
  {
    if (m_Pivot == null)
      return;
    vp_Utility.Activate(m_Pivot.gameObject, visible);
  }

  public virtual void SnapToExit()
  {
    RotationOffset = RotationExitOffset;
    PositionOffset = PositionExitOffset;
    SnapSprings();
    SnapPivot();
  }

  public override void SnapSprings()
  {
    base.SnapSprings();
    if (m_PositionSpring != null)
    {
      m_PositionSpring.RestState = PositionOffset - PositionPivot;
      m_PositionSpring.State = PositionOffset - PositionPivot;
      m_PositionSpring.Stop(true);
    }
    if (m_WeaponGroup != null)
      m_WeaponGroupTransform.localPosition = PositionOffset - PositionPivot;
    if (m_PositionPivotSpring != null)
    {
      m_PositionPivotSpring.RestState = PositionPivot;
      m_PositionPivotSpring.State = PositionPivot;
      m_PositionPivotSpring.Stop(true);
    }
    Transform.localPosition = PositionPivot;
    if (m_RotationPivotSpring != null)
    {
      m_RotationPivotSpring.RestState = RotationPivot;
      m_RotationPivotSpring.State = RotationPivot;
      m_RotationPivotSpring.Stop(true);
    }
    Transform.localEulerAngles = RotationPivot;
    if (m_RotationSpring == null)
      return;
    m_RotationSpring.RestState = RotationOffset;
    m_RotationSpring.State = RotationOffset;
    m_RotationSpring.Stop(true);
  }

  public override void StopSprings()
  {
    if (m_PositionSpring != null)
      m_PositionSpring.Stop(true);
    if (m_PositionPivotSpring != null)
      m_PositionPivotSpring.Stop(true);
    if (m_RotationSpring != null)
      m_RotationSpring.Stop(true);
    if (m_RotationPivotSpring == null)
      return;
    m_RotationPivotSpring.Stop(true);
  }

  public override void Wield(bool isWielding = true)
  {
    if (isWielding)
      SnapToExit();
    PositionOffset = isWielding ? DefaultPosition : PositionExitOffset;
    RotationOffset = isWielding ? DefaultRotation : RotationExitOffset;
    m_Wielded = isWielding;
    Refresh();
    StateManager.CombineStates();
    if (Audio != null && (isWielding ? SoundWield : (Object) SoundUnWield) != null && vp_Utility.IsActive(gameObject))
    {
      Audio.pitch = Time.timeScale;
      Audio.PlayOneShot(isWielding ? SoundWield : SoundUnWield);
    }
    if (!((isWielding ? AnimationWield : (Object) AnimationUnWield) != null) || !vp_Utility.IsActive(gameObject))
      return;
    if (isWielding)
      m_WeaponModel.GetComponent<Animation>().CrossFade(AnimationWield.name);
    else
      m_WeaponModel.GetComponent<Animation>().CrossFade(AnimationUnWield.name);
  }

  public virtual void ScheduleAmbientAnimation()
  {
    if (AnimationAmbient.Count == 0 || !vp_Utility.IsActive(gameObject))
      return;
    vp_Timer.In(Random.Range(AmbientInterval.x, AmbientInterval.y), () =>
    {
      if (!vp_Utility.IsActive(gameObject))
        return;
      m_CurrentAmbientAnimation = Random.Range(0, AnimationAmbient.Count);
      if (!(AnimationAmbient[m_CurrentAmbientAnimation] != null))
        return;
      m_WeaponModel.GetComponent<Animation>().CrossFadeQueued(AnimationAmbient[m_CurrentAmbientAnimation].name);
      ScheduleAmbientAnimation();
    }, m_AnimationAmbientTimer);
  }

  protected virtual void OnMessage_FallImpact(float impact)
  {
    if (m_PositionSpring != null)
      m_PositionSpring.AddSoftForce(Vector3.down * impact * PositionKneeling, PositionKneelingSoftness);
    if (m_RotationSpring == null)
      return;
    m_RotationSpring.AddSoftForce(Vector3.right * impact * RotationKneeling, RotationKneelingSoftness);
  }

  protected virtual void OnMessage_HeadImpact(float impact) => AddForce(Vector3.zero, Vector3.forward * (impact * 20f) * Time.timeScale);

  protected virtual void OnMessage_CameraGroundStomp(float impact) => AddForce(Vector3.zero, new Vector3(-0.25f, 0.0f, 0.0f) * impact);

  protected virtual void OnMessage_CameraBombShake(float impact) => AddForce(Vector3.zero, new Vector3(-0.3f, 0.1f, 0.5f) * impact);

  protected virtual void OnMessage_CameraToggle3rdPerson() => RefreshWeaponModel();

  protected override Vector3 Get_AimDirection()
  {
    if (FPPlayer.IsFirstPerson.Get())
      return FPPlayer.HeadLookDirection.Get();
    return Weapon3rdPersonModel == null ? FPPlayer.HeadLookDirection.Get() : (Weapon3rdPersonModel.transform.position - FPPlayer.LookPoint.Get()).normalized;
  }

  protected override Vector3 OnValue_AimDirection
  {
    get
    {
      if (FPPlayer.IsFirstPerson.Get())
        return FPPlayer.HeadLookDirection.Get();
      return Weapon3rdPersonModel == null ? FPPlayer.HeadLookDirection.Get() : (Weapon3rdPersonModel.transform.position - FPPlayer.LookPoint.Get()).normalized;
    }
  }

  public override void Register(vp_EventHandler eventHandler)
  {
    base.Register(eventHandler);
    eventHandler.RegisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.RegisterMessage<float>("CameraGroundStomp", OnMessage_CameraGroundStomp);
    eventHandler.RegisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.RegisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.RegisterMessage<float>("HeadImpact", OnMessage_HeadImpact);
  }

  public override void Unregister(vp_EventHandler eventHandler)
  {
    base.Unregister(eventHandler);
    eventHandler.UnregisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.UnregisterMessage<float>("CameraGroundStomp", OnMessage_CameraGroundStomp);
    eventHandler.UnregisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.UnregisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.UnregisterMessage<float>("HeadImpact", OnMessage_HeadImpact);
  }

  protected override StateManager GetStateManager() => new FPWeaponStateManager(this);
}
