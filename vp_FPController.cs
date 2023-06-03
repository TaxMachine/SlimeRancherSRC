// Decompiled with JetBrains decompiler
// Type: vp_FPController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class vp_FPController : vp_CharacterController
{
  public static OnStartDelegate onStartDelegate;
  protected Vector3 m_FixedPosition = Vector3.zero;
  protected Vector3 m_SmoothPosition = Vector3.zero;
  protected bool m_IsFirstPerson = true;
  protected bool m_HeadContact;
  protected RaycastHit m_CeilingHit;
  protected RaycastHit m_WallHit;
  protected Terrain m_CurrentTerrain;
  protected vp_SurfaceIdentifier m_CurrentSurface;
  protected CapsuleCollider m_TriggerCollider;
  public bool PhysicsHasCollisionTrigger = true;
  protected GameObject m_Trigger;
  public float MotorAcceleration = 0.18f;
  public float MotorDamping = 0.17f;
  public float MotorBackwardsSpeed = 0.65f;
  public float MotorAirSpeed = 0.35f;
  public float MotorSlopeSpeedUp = 1f;
  public float MotorSlopeSpeedDown = 1f;
  protected Vector3 m_MoveDirection = Vector3.zero;
  protected Vector3 m_MotorThrottle = Vector3.zero;
  protected float m_MotorAirSpeedModifier = 1f;
  protected float m_CurrentAntiBumpOffset;
  public float MotorJumpForce = 0.18f;
  public float MotorJumpForceDamping = 0.08f;
  public float MotorJumpForceHold = 3f / 1000f;
  public float MotorJumpForceHoldDamping = 0.5f;
  protected int m_MotorJumpForceHoldSkipFrames;
  protected float m_MotorJumpForceAcc;
  protected bool m_MotorJumpDone = true;
  public float PhysicsForceDamping = 0.05f;
  public float PhysicsSlopeSlideLimit = 30f;
  public float PhysicsSlopeSlidiness = 0.15f;
  public float PhysicsWallBounce;
  public float PhysicsWallFriction;
  protected Vector3 m_ExternalForce = Vector3.zero;
  protected Vector3[] m_SmoothForceFrame = new Vector3[120];
  protected bool m_Slide;
  protected bool m_SlideFast;
  protected float m_SlideFallSpeed;
  protected float m_OnSteepGroundSince;
  protected float m_SlopeSlideSpeed;
  protected Vector3 m_PredictedPos = Vector3.zero;
  protected Vector3 m_PrevDir = Vector3.zero;
  protected Vector3 m_NewDir = Vector3.zero;
  protected float m_ForceImpact;
  protected float m_ForceMultiplier;
  protected Vector3 CapsuleBottom = Vector3.zero;
  protected Vector3 CapsuleTop = Vector3.zero;
  private Vector3 m_DepenetrationForce = Vector3.zero;
  private const float FREE_SPRINT_FACTOR = 3f;

  public Vector3 SmoothPosition => m_SmoothPosition;

  public Vector3 Velocity => CharacterController.velocity;

  public bool HeadContact => m_HeadContact;

  public Vector3 GroundNormal => m_GroundHit.normal;

  public float GroundAngle => Vector3.Angle(m_GroundHit.normal, Vector3.up);

  public Transform GroundTransform => m_GroundHitTransform.transform;

  public bool GroundedNonMountain => m_GroundedNonMountain && GroundAngle <= (double) Player.SlopeLimit.Get();

  public void AddDepenetrationForce(Vector3 force) => m_DepenetrationForce += force;

  private void ResetDepenetrationForce() => m_DepenetrationForce = Vector3.zero;

  protected override void OnEnable() => base.OnEnable();

  protected override void OnDisable() => base.OnDisable();

  protected override void Start()
  {
    base.Start();
    SetPosition(Transform.position);
    if (PhysicsHasCollisionTrigger)
    {
      m_Trigger = new GameObject("Trigger");
      m_Trigger.transform.parent = m_Transform;
      m_Trigger.layer = 8;
      m_Trigger.transform.localPosition = Vector3.zero;
      m_TriggerCollider = m_Trigger.AddComponent<CapsuleCollider>();
      m_TriggerCollider.isTrigger = true;
      m_TriggerCollider.radius = CharacterController.radius + SkinWidth;
      m_TriggerCollider.height = CharacterController.height + SkinWidth * 2f;
      m_TriggerCollider.center = CharacterController.center;
    }
    if (onStartDelegate == null)
      return;
    onStartDelegate(this);
  }

  protected override void RefreshCollider()
  {
    base.RefreshCollider();
    if (!(m_TriggerCollider != null))
      return;
    m_TriggerCollider.radius = CharacterController.radius + SkinWidth;
    m_TriggerCollider.height = CharacterController.height + SkinWidth * 2f;
    m_TriggerCollider.center = CharacterController.center;
  }

  public override void EnableCollider(bool isEnabled = true)
  {
    if (!(CharacterController != null))
      return;
    CharacterController.enabled = isEnabled;
  }

  protected override void Update()
  {
    base.Update();
    SmoothMove();
  }

  protected override void FixedUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    UpdateMotor();
    UpdateJump();
    UpdateForces();
    UpdateSliding();
    UpdateOutOfControl();
    if (MotorFreeFly)
      m_FallSpeed = 0.0f;
    FixedMove();
    UpdateCollisions();
    UpdatePlatformMove();
    UpdateVelocity();
  }

  protected virtual void UpdateMotor()
  {
    if (!MotorFreeFly)
      UpdateThrottleWalk();
    else
      UpdateThrottleFree();
    m_MotorThrottle = vp_MathUtility.SnapToZero(m_MotorThrottle);
  }

  protected virtual void UpdateThrottleWalk()
  {
    m_MotorAirSpeedModifier = m_Grounded ? 1f : MotorAirSpeed;
    Vector3 diff1 = (Player.InputMoveVector.Get().y > 0.0 ? Player.InputMoveVector.Get().y : Player.InputMoveVector.Get().y * MotorBackwardsSpeed) * Transform.TransformDirection(Vector3.forward * (MotorAcceleration * 0.1f) * m_MotorAirSpeedModifier);
    Vector3 diff2 = Player.InputMoveVector.Get().x * Transform.TransformDirection(Vector3.right * (MotorAcceleration * 0.1f * m_MotorAirSpeedModifier));
    m_MotorThrottle += diff1 * CalculateSlopeFactor(diff1) + diff2 * CalculateSlopeFactor(diff2);
    m_MotorThrottle.x /= (float) (1.0 + MotorDamping * (double) m_MotorAirSpeedModifier * Time.timeScale);
    m_MotorThrottle.z /= (float) (1.0 + MotorDamping * (double) m_MotorAirSpeedModifier * Time.timeScale);
  }

  protected virtual void UpdateThrottleFree()
  {
    bool isPressed = SRInput.Actions.run.IsPressed;
    m_MotorThrottle += Player.InputMoveVector.Get().y * Transform.TransformDirection(Transform.InverseTransformDirection(((vp_FPPlayerEventHandler) Player).CameraLookDirection.Get()) * (float) (MotorAcceleration * 0.10000000149011612 * (isPressed ? 3.0 : 1.0)));
    m_MotorThrottle += Player.InputMoveVector.Get().x * Transform.TransformDirection(Vector3.right * (float) (MotorAcceleration * 0.10000000149011612 * (isPressed ? 3.0 : 1.0)));
    m_MotorThrottle.x /= (float) (1.0 + MotorDamping * (double) Time.timeScale);
    m_MotorThrottle.z /= (float) (1.0 + MotorDamping * (double) Time.timeScale);
  }

  protected virtual void UpdateJump()
  {
    if (m_HeadContact)
      Player.Jump.Stop(1f);
    if (!MotorFreeFly)
      UpdateJumpForceWalk();
    else
      UpdateJumpForceFree();
    m_MotorThrottle.y += m_MotorJumpForceAcc * Time.timeScale;
    m_MotorJumpForceAcc /= (float) (1.0 + MotorJumpForceHoldDamping * (double) Time.timeScale);
    m_MotorThrottle.y /= (float) (1.0 + MotorJumpForceDamping * (double) Time.timeScale);
  }

  protected virtual void UpdateJumpForceWalk()
  {
    if (!Player.Jump.Active || Player.Jetpack.Active || m_Grounded)
      return;
    if (m_MotorJumpForceHoldSkipFrames > 2)
    {
      if (Player.Velocity.Get().y < 0.0)
        return;
      m_MotorJumpForceAcc += MotorJumpForceHold;
    }
    else
      ++m_MotorJumpForceHoldSkipFrames;
  }

  protected virtual void UpdateJumpForceFree()
  {
    if (Player.Jump.Active && Player.Crouch.Active)
      return;
    if (Player.Jump.Active)
    {
      m_MotorJumpForceAcc += MotorJumpForceHold;
    }
    else
    {
      if (!Player.Crouch.Active)
        return;
      m_MotorJumpForceAcc -= MotorJumpForceHold;
      if (!Grounded || CharacterController.height != (double) m_NormalHeight)
        return;
      CharacterController.height = m_CrouchHeight;
      CharacterController.center = m_CrouchCenter;
    }
  }

  protected override void UpdateForces()
  {
    base.UpdateForces();
    if (m_SmoothForceFrame[0] != Vector3.zero)
    {
      AddForceInternal(m_SmoothForceFrame[0]);
      for (int index = 0; index < 120; ++index)
      {
        m_SmoothForceFrame[index] = index < 119 ? m_SmoothForceFrame[index + 1] : Vector3.zero;
        if (m_SmoothForceFrame[index] == Vector3.zero)
          break;
      }
    }
    m_ExternalForce /= (float) (1.0 + PhysicsForceDamping * (double) vp_TimeUtility.AdjustedTimeScale);
  }

  protected virtual void UpdateSliding()
  {
    bool slideFast = m_SlideFast;
    int num1 = m_Slide ? 1 : 0;
    m_Slide = false;
    if (!m_Grounded)
    {
      m_OnSteepGroundSince = 0.0f;
      m_SlideFast = false;
    }
    else if (GroundAngle > (double) PhysicsSlopeSlideLimit || !m_GroundedNonMountain)
    {
      m_Slide = true;
      if (GroundAngle <= (double) Player.SlopeLimit.Get())
      {
        m_SlopeSlideSpeed = Mathf.Max(m_SlopeSlideSpeed, PhysicsSlopeSlidiness * 0.01f);
        m_OnSteepGroundSince = 0.0f;
        m_SlideFast = false;
        m_SlopeSlideSpeed = Mathf.Abs(m_SlopeSlideSpeed) < 9.9999997473787516E-05 ? 0.0f : m_SlopeSlideSpeed / (float) (1.0 + 0.05000000074505806 * vp_TimeUtility.AdjustedTimeScale);
      }
      else
      {
        if (m_SlopeSlideSpeed > 0.0099999997764825821)
          m_SlideFast = true;
        if (m_OnSteepGroundSince == 0.0)
          m_OnSteepGroundSince = Time.time;
        m_SlopeSlideSpeed += (float) (PhysicsSlopeSlidiness * 0.0099999997764825821 * ((Time.time - (double) m_OnSteepGroundSince) * 0.125)) * vp_TimeUtility.AdjustedTimeScale;
        m_SlopeSlideSpeed = Mathf.Max(PhysicsSlopeSlidiness * 0.01f, m_SlopeSlideSpeed);
      }
      AddForce(Vector3.Cross(Vector3.Cross(GroundNormal, Vector3.down), GroundNormal) * m_SlopeSlideSpeed * vp_TimeUtility.AdjustedTimeScale);
    }
    else
    {
      m_OnSteepGroundSince = 0.0f;
      m_SlideFast = false;
      m_SlopeSlideSpeed = 0.0f;
    }
    if (m_MotorThrottle != Vector3.zero)
      m_Slide = false;
    if (m_SlideFast)
      m_SlideFallSpeed = Transform.position.y;
    else if (slideFast && !Grounded)
      m_FallSpeed = Mathf.Min(0.0f, Transform.position.y - m_SlideFallSpeed);
    int num2 = m_Slide ? 1 : 0;
    if (num1 == num2)
      return;
    Player.SetState("Slide", m_Slide);
  }

  private void UpdateOutOfControl()
  {
    if (m_ExternalForce.magnitude > 0.20000000298023224 || m_FallSpeed < -0.20000000298023224 || m_SlideFast)
    {
      Player.OutOfControl.Start();
    }
    else
    {
      if (!Player.OutOfControl.Active)
        return;
      Player.OutOfControl.Stop();
    }
  }

  protected override void FixedMove()
  {
    m_MoveDirection = Vector3.zero;
    m_MoveDirection += m_ExternalForce;
    m_MoveDirection += m_DepenetrationForce;
    m_MoveDirection += m_MotorThrottle;
    m_MoveDirection.y += m_FallSpeed;
    ResetDepenetrationForce();
    m_CurrentAntiBumpOffset = 0.0f;
    if (m_Grounded && m_MotorThrottle.y <= 1.0 / 1000.0 && !Player.Jetpack.Active)
    {
      m_CurrentAntiBumpOffset = Mathf.Max(Player.StepOffset.Get(), Vector3.Scale(m_MoveDirection, Vector3.one - Vector3.up).magnitude);
      m_MoveDirection += m_CurrentAntiBumpOffset * Vector3.down;
    }
    m_PredictedPos = Transform.position + vp_MathUtility.NaNSafeVector3(m_MoveDirection * Delta * Time.timeScale);
    if (m_Platform != null && m_PositionOnPlatform != Vector3.zero)
      Player.Move.Send(vp_MathUtility.NaNSafeVector3(m_Platform.TransformPoint(m_PositionOnPlatform) - m_Transform.position));
    Player.Move.Send(vp_MathUtility.NaNSafeVector3(m_MoveDirection * Delta * Time.timeScale));
    if (Player.Dead.Active)
    {
      Player.InputMoveVector.Set(Vector2.zero);
    }
    else
    {
      StoreGroundInfo();
      if (!m_Grounded && Player.Velocity.Get().y > 0.0)
      {
        Physics.SphereCast(new Ray(Transform.position, Vector3.up), Player.Radius.Get(), out m_CeilingHit, (float) (Player.Height.Get() - (Player.Radius.Get() - (double) SkinWidth) + 0.0099999997764825821), -675375893);
        m_HeadContact = m_CeilingHit.collider != null;
      }
      else
        m_HeadContact = false;
      if (!(m_GroundHitTransform == null) || !(m_LastGroundHitTransform != null))
        return;
      if (m_Platform != null && m_PositionOnPlatform != Vector3.zero)
      {
        AddForce(m_Platform.position - m_LastPlatformPos);
        m_Platform = null;
      }
      if (m_CurrentAntiBumpOffset == 0.0)
        return;
      Player.Move.Send(vp_MathUtility.NaNSafeVector3(m_CurrentAntiBumpOffset * Vector3.up) * Delta * Time.timeScale);
      m_PredictedPos += vp_MathUtility.NaNSafeVector3(m_CurrentAntiBumpOffset * Vector3.up) * Delta * Time.timeScale;
      m_MoveDirection += m_CurrentAntiBumpOffset * Vector3.up;
    }
  }

  protected virtual void SmoothMove()
  {
    if (Time.timeScale == 0.0)
      return;
    m_FixedPosition = Transform.position;
    Transform.position = m_SmoothPosition;
    Player.Move.Send(vp_MathUtility.NaNSafeVector3(m_MoveDirection * Delta * Time.timeScale));
    m_SmoothPosition = Transform.position;
    Transform.position = m_FixedPosition;
    if (Vector3.Distance(Transform.position, m_SmoothPosition) > (double) Player.Radius.Get() || m_Platform != null && m_LastPlatformPos != m_Platform.position)
      m_SmoothPosition = Transform.position;
    m_SmoothPosition = Vector3.Lerp(m_SmoothPosition, Transform.position, Time.deltaTime);
  }

  protected override void UpdateCollisions()
  {
    base.UpdateCollisions();
    if (m_OnNewGround)
    {
      if (m_WasFalling && Velocity.y <= 0.0)
      {
        DeflectDownForce();
        m_SmoothPosition.y = Transform.position.y;
        m_MotorThrottle.y = 0.0f;
        m_MotorJumpForceAcc = 0.0f;
        m_MotorJumpForceHoldSkipFrames = 0;
      }
      if (m_GroundHit.collider.gameObject.layer == 28)
      {
        m_Platform = m_GroundHit.transform;
        m_LastPlatformAngle = m_Platform.eulerAngles.y;
      }
      else
        m_Platform = null;
      Terrain component1 = m_GroundHitTransform.GetComponent<Terrain>();
      m_CurrentTerrain = !(component1 != null) ? null : component1;
      vp_SurfaceIdentifier component2 = m_GroundHitTransform.GetComponent<vp_SurfaceIdentifier>();
      m_CurrentSurface = !(component2 != null) ? null : component2;
    }
    if (m_PredictedPos.y <= (double) Transform.position.y || m_ExternalForce.y <= 0.0 && m_MotorThrottle.y <= 0.0)
      return;
    DeflectUpForce();
  }

  protected virtual float CalculateSlopeFactor(Vector3 diff)
  {
    if (!m_Grounded)
      return 1f;
    float num1 = Vector3.Angle(m_GroundHit.normal, diff);
    float num2 = (float) (1.0 + (1.0 - num1 / 90.0));
    float slopeFactor;
    if (Mathf.Abs(1f - num2) < 0.0099999997764825821)
      slopeFactor = 1f;
    else if (num2 > 1.0)
    {
      slopeFactor = num2 * MotorSlopeSpeedDown;
    }
    else
    {
      slopeFactor = num2 * MotorSlopeSpeedUp;
      if (num1 > Player.SlopeLimit.Get() + 90.0)
        slopeFactor = 0.0f;
    }
    return slopeFactor;
  }

  protected override void UpdatePlatformMove()
  {
    base.UpdatePlatformMove();
    if (!(m_Platform != null))
      return;
    m_SmoothPosition = Transform.position;
  }

  protected override void UpdatePlatformRotation()
  {
    if (m_Platform == null)
      return;
    base.UpdatePlatformRotation();
  }

  public override void SetPosition(Vector3 position)
  {
    base.SetPosition(position);
    m_SmoothPosition = position;
  }

  protected virtual void AddForceInternal(Vector3 force) => m_ExternalForce += force;

  public virtual void AddForce(float x, float y, float z) => AddForce(new Vector3(x, y, z));

  public virtual void AddForce(Vector3 force)
  {
    if (Time.timeScale >= 1.0)
      AddForceInternal(force);
    else
      AddSoftForce(force, 1f);
  }

  public virtual void AddSoftForce(Vector3 force, float frames)
  {
    if (Time.timeScale == 0.0)
      return;
    force /= Time.timeScale;
    frames = Mathf.Clamp(frames, 1f, 120f);
    AddForceInternal(force / frames);
    for (int index = 0; index < Mathf.RoundToInt(frames) - 1; ++index)
      m_SmoothForceFrame[index] += force / frames;
  }

  public virtual void StopSoftForce()
  {
    for (int index = 0; index < 120 && !(m_SmoothForceFrame[index] == Vector3.zero); ++index)
      m_SmoothForceFrame[index] = Vector3.zero;
  }

  public override void Stop()
  {
    base.Stop();
    m_MotorThrottle = Vector3.zero;
    m_MotorJumpDone = true;
    m_MotorJumpForceAcc = 0.0f;
    m_ExternalForce = Vector3.zero;
    StopSoftForce();
    m_SmoothPosition = Transform.position;
    m_SlideFast = false;
    m_Slide = false;
    ResetDepenetrationForce();
    StoreGroundInfo();
  }

  public virtual void DeflectDownForce()
  {
    if (GroundAngle > (double) PhysicsSlopeSlideLimit)
      m_SlopeSlideSpeed = m_FallImpact * (0.25f * Time.timeScale);
    if (GroundAngle <= 85.0)
      return;
    m_MotorThrottle += vp_3DUtility.HorizontalVector(GroundNormal * m_FallImpact);
    m_Grounded = false;
  }

  protected virtual void DeflectUpForce()
  {
    if (!m_HeadContact)
      return;
    m_NewDir = Vector3.Cross(Vector3.Cross(m_CeilingHit.normal, Vector3.up), m_CeilingHit.normal);
    m_ForceImpact = m_MotorThrottle.y + m_ExternalForce.y;
    Vector3 vector3 = m_NewDir * (m_MotorThrottle.y + m_ExternalForce.y) * (1f - PhysicsWallFriction);
    m_ForceImpact -= vector3.magnitude;
    AddForce(vector3 * Time.timeScale);
    m_MotorThrottle.y = 0.0f;
    m_ExternalForce.y = 0.0f;
    m_FallSpeed = 0.0f;
    m_NewDir.x = Transform.InverseTransformDirection(m_NewDir).x;
    Player.HeadImpact.Send(m_NewDir.x < 0.0 || m_NewDir.x == 0.0 && Random.value < 0.5 ? -m_ForceImpact : m_ForceImpact);
  }

  protected virtual void DeflectHorizontalForce()
  {
    m_PredictedPos.y = Transform.position.y;
    m_PrevPosition.y = Transform.position.y;
    m_PrevDir = (m_PredictedPos - m_PrevPosition).normalized;
    CapsuleBottom = m_PrevPosition + Vector3.up * Player.Radius.Get();
    CapsuleTop = CapsuleBottom + Vector3.up * (Player.Height.Get() - Player.Radius.Get() * 2f);
    if (!Physics.CapsuleCast(CapsuleBottom, CapsuleTop, Player.Radius.Get(), m_PrevDir, out m_WallHit, Vector3.Distance(m_PrevPosition, m_PredictedPos), -675375893))
      return;
    m_NewDir = Vector3.Cross(m_WallHit.normal, Vector3.up).normalized;
    if (Vector3.Dot(Vector3.Cross(m_WallHit.point - Transform.position, m_PrevPosition - Transform.position), Vector3.up) > 0.0)
      m_NewDir = -m_NewDir;
    m_ForceMultiplier = Mathf.Abs(Vector3.Dot(m_PrevDir, m_NewDir)) * (1f - PhysicsWallFriction);
    if (PhysicsWallBounce > 0.0)
    {
      m_NewDir = Vector3.Lerp(m_NewDir, Vector3.Reflect(m_PrevDir, m_WallHit.normal), PhysicsWallBounce);
      m_ForceMultiplier = Mathf.Lerp(m_ForceMultiplier, 1f, PhysicsWallBounce * (1f - PhysicsWallFriction));
    }
    m_ForceImpact = 0.0f;
    float y = m_ExternalForce.y;
    m_ExternalForce.y = 0.0f;
    m_ForceImpact = m_ExternalForce.magnitude;
    m_ExternalForce = m_NewDir * m_ExternalForce.magnitude * m_ForceMultiplier;
    m_ForceImpact -= m_ExternalForce.magnitude;
    for (int index = 0; index < 120 && !(m_SmoothForceFrame[index] == Vector3.zero); ++index)
      m_SmoothForceFrame[index] = m_SmoothForceFrame[index].magnitude * m_NewDir * m_ForceMultiplier;
    m_ExternalForce.y = y;
  }

  public float CalculateMaxSpeed(string stateName = "Default", float accelDuration = 5f)
  {
    if (stateName != "Default")
    {
      bool flag = false;
      foreach (vp_State state in States)
      {
        if (state.Name == stateName)
          flag = true;
      }
      if (!flag)
      {
        Debug.LogError("Error (" + this + ") Controller has no such state: '" + stateName + "'.");
        return 0.0f;
      }
    }
    Dictionary<vp_State, bool> dictionary = new Dictionary<vp_State, bool>();
    foreach (vp_State state in States)
    {
      dictionary.Add(state, state.Enabled);
      state.Enabled = false;
    }
    StateManager.Reset();
    if (stateName != "Default")
      SetState(stateName);
    float maxSpeed = 0.0f;
    float num = 5f;
    for (int index = 0; index < 60.0 * num; ++index)
      maxSpeed = (maxSpeed + (float) (MotorAcceleration * 0.10000000149011612 * 60.0)) / (1f + MotorDamping);
    foreach (vp_State state in States)
    {
      bool flag;
      dictionary.TryGetValue(state, out flag);
      state.Enabled = flag;
    }
    return maxSpeed;
  }

  protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.gameObject.isStatic || hit.gameObject.layer == 29)
      return;
    Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
    if (attachedRigidbody == null || vp_Gameplay.isMaster && attachedRigidbody.isKinematic || Time.time < (double) m_NextAllowedPushTime)
      return;
    m_NextAllowedPushTime = Time.time + PhysicsPushInterval;
    if (vp_Gameplay.isMultiplayer)
      return;
    PushRigidbody(attachedRigidbody, hit.moveDirection, hit.point);
  }

  protected virtual bool CanStart_Jump() => Time.timeScale != 0.0 && (MotorFreeFly || m_GroundedNonMountain && m_MotorJumpDone && GroundAngle <= (double) Player.SlopeLimit.Get());

  protected virtual bool CanStart_Run() => !Player.Crouch.Active;

  protected virtual void OnStart_Jump()
  {
    m_MotorJumpDone = false;
    if (MotorFreeFly && !Grounded)
      return;
    m_MotorThrottle.y = MotorJumpForce / Time.timeScale;
    m_SmoothPosition.y = Transform.position.y;
  }

  protected virtual void OnStop_Jump() => m_MotorJumpDone = true;

  protected virtual bool CanStop_Crouch()
  {
    if (!Physics.SphereCast(new Ray(Transform.position, Vector3.up), Player.Radius.Get(), (float) (m_NormalHeight - (double) Player.Radius.Get() + 0.0099999997764825821), -675375893))
      return true;
    Player.Crouch.NextAllowedStopTime = Time.time + 1f;
    return false;
  }

  protected virtual void OnMessage_ForceImpact(Vector3 force) => AddForce(force);

  protected virtual Vector3 Get_MotorThrottle() => m_MotorThrottle;

  protected virtual void Set_MotorThrottle(Vector3 value) => m_MotorThrottle = value;

  protected virtual Vector3 OnValue_MotorThrottle
  {
    get => m_MotorThrottle;
    set => m_MotorThrottle = value;
  }

  protected virtual bool Get_MotorJumpDone() => m_MotorJumpDone;

  protected virtual bool OnValue_MotorJumpDone => m_MotorJumpDone;

  protected virtual Texture Get_GroundTexture()
  {
    if (GroundTransform == null)
      return null;
    if (GroundTransform.GetComponent<Renderer>() == null && m_CurrentTerrain == null)
      return null;
    int index = -1;
    if (m_CurrentTerrain != null)
    {
      index = vp_FootstepManager.GetMainTerrainTexture(Player.Position.Get(), m_CurrentTerrain);
      if (index > m_CurrentTerrain.terrainData.terrainLayers.Length - 1)
        return null;
    }
    return !(m_CurrentTerrain == null) ? m_CurrentTerrain.terrainData.terrainLayers[index].diffuseTexture : GroundTransform.GetComponent<Renderer>().material.mainTexture;
  }

  protected virtual Texture OnValue_GroundTexture
  {
    get
    {
      if (GroundTransform == null)
        return null;
      if (GroundTransform.GetComponent<Renderer>() == null && m_CurrentTerrain == null)
        return null;
      int index = -1;
      if (m_CurrentTerrain != null)
      {
        index = vp_FootstepManager.GetMainTerrainTexture(Player.Position.Get(), m_CurrentTerrain);
        if (index > m_CurrentTerrain.terrainData.terrainLayers.Length - 1)
          return null;
      }
      return !(m_CurrentTerrain == null) ? m_CurrentTerrain.terrainData.terrainLayers[index].diffuseTexture : GroundTransform.GetComponent<Renderer>().material.mainTexture;
    }
  }

  protected virtual vp_SurfaceIdentifier Get_SurfaceType() => m_CurrentSurface;

  protected virtual vp_SurfaceIdentifier OnValue_SurfaceType => m_CurrentSurface;

  protected virtual bool Get_IsFirstPerson() => m_IsFirstPerson;

  protected virtual void Set_IsFirstPerson(bool value) => m_IsFirstPerson = value;

  protected virtual bool OnValue_IsFirstPerson
  {
    get => m_IsFirstPerson;
    set => m_IsFirstPerson = value;
  }

  protected virtual void OnStart_Dead() => m_Platform = null;

  protected virtual void OnStop_Dead() => Player.OutOfControl.Stop();

  public override void Register(vp_EventHandler eventHandler)
  {
    base.Register(eventHandler);
    eventHandler.RegisterActivity("Jump", OnStart_Jump, OnStop_Jump, CanStart_Jump, null, null, null);
    eventHandler.RegisterActivity("Run", null, null, CanStart_Run, null, null, null);
    eventHandler.RegisterActivity("Crouch", null, null, null, CanStop_Crouch, null, null);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.RegisterValue("GroundTexture", Get_GroundTexture, null);
    eventHandler.RegisterValue("IsFirstPerson", Get_IsFirstPerson, Set_IsFirstPerson);
    eventHandler.RegisterValue("MotorJumpDone", Get_MotorJumpDone, null);
    eventHandler.RegisterValue("MotorThrottle", Get_MotorThrottle, Set_MotorThrottle);
    eventHandler.RegisterValue("Position", Get_Position, Set_Position);
    eventHandler.RegisterValue("SurfaceType", Get_SurfaceType, null);
  }

  public override void Unregister(vp_EventHandler eventHandler)
  {
    base.Unregister(eventHandler);
    eventHandler.UnregisterActivity("Jump", OnStart_Jump, OnStop_Jump, CanStart_Jump, null, null, null);
    eventHandler.UnregisterActivity("Run", null, null, CanStart_Run, null, null, null);
    eventHandler.UnregisterActivity("Crouch", null, null, null, CanStop_Crouch, null, null);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.UnregisterValue("GroundTexture", Get_GroundTexture, null);
    eventHandler.UnregisterValue("IsFirstPerson", Get_IsFirstPerson, Set_IsFirstPerson);
    eventHandler.UnregisterValue("MotorJumpDone", Get_MotorJumpDone, null);
    eventHandler.UnregisterValue("MotorThrottle", Get_MotorThrottle, Set_MotorThrottle);
    eventHandler.UnregisterValue("Position", Get_Position, Set_Position);
    eventHandler.UnregisterValue("SurfaceType", Get_SurfaceType, null);
  }

  protected override StateManager GetStateManager() => new FPControllerStateManager(this);

  public delegate void OnStartDelegate(vp_FPController controller);
}
