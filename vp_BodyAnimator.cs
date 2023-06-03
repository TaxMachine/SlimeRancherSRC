// Decompiled with JetBrains decompiler
// Type: vp_BodyAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class vp_BodyAnimator : MonoBehaviour, EventHandlerRegistrable
{
  protected bool m_IsValid = true;
  protected Vector3 m_ValidLookPoint = Vector3.zero;
  protected float m_ValidLookPointForward;
  protected bool HeadPointDirty = true;
  public GameObject HeadBone;
  public GameObject LowestSpineBone;
  [Range(0.0f, 90f)]
  public float HeadPitchCap = 45f;
  [Range(2f, 20f)]
  public float HeadPitchSpeed = 7f;
  [Range(0.2f, 20f)]
  public float HeadYawSpeed = 2f;
  [Range(0.0f, 1f)]
  public float LeaningFactor = 0.25f;
  protected List<GameObject> m_HeadLookBones = new List<GameObject>();
  protected List<Vector3> m_ReferenceUpDirs;
  protected List<Vector3> m_ReferenceLookDirs;
  protected float m_CurrentHeadLookYaw;
  protected float m_CurrentHeadLookPitch;
  protected List<float> m_HeadLookFalloffs = new List<float>();
  protected List<float> m_HeadLookCurrentFalloffs;
  protected List<float> m_HeadLookTargetFalloffs;
  protected Vector3 m_HeadLookTargetWorldDir;
  protected Vector3 m_HeadLookCurrentWorldDir;
  protected Vector3 m_HeadLookBackup = Vector3.zero;
  protected Vector3 m_LookPoint = Vector3.zero;
  public float FeetAdjustAngle = 80f;
  public float FeetAdjustSpeedStanding = 10f;
  public float FeetAdjustSpeedMoving = 12f;
  protected float m_PrevBodyYaw;
  protected float m_BodyYaw;
  protected float m_CurrentBodyYawTarget;
  protected float m_LastYaw;
  public Vector3 ClimbOffset = Vector3.forward * 0.6f;
  public Vector3 ClimbRotationOffset = Vector3.zero;
  protected float m_CurrentForward;
  protected float m_CurrentStrafe;
  protected float m_CurrentTurn;
  protected float m_CurrentTurnTarget;
  protected float m_MaxWalkSpeed = 1f;
  protected float m_MaxRunSpeed = 1f;
  protected float m_MaxCrouchSpeed = 1f;
  protected bool m_WasMoving;
  protected RaycastHit m_GroundHit;
  protected bool m_Grounded = true;
  protected vp_Timer.Handle m_AttackDoneTimer = new vp_Timer.Handle();
  protected float m_NextAllowedUpdateTurnTargetTime;
  protected const float TURNMODIFIER = 0.2f;
  protected const float CROUCHTURNMODIFIER = 100f;
  protected const float MOVEMODIFIER = 100f;
  public bool ShowDebugObjects;
  protected int ForwardAmount;
  protected int PitchAmount;
  protected int StrafeAmount;
  protected int TurnAmount;
  protected int VerticalMoveAmount;
  protected int IsAttacking;
  protected int IsClimbing;
  protected int IsCrouching;
  protected int IsGrounded;
  protected int IsMoving;
  protected int IsOutOfControl;
  protected int IsReloading;
  protected int IsRunning;
  protected int IsSettingWeapon;
  protected int IsZooming;
  protected int IsFirstPerson;
  protected int StartClimb;
  protected int StartOutOfControl;
  protected int StartReload;
  protected int WeaponGripIndex;
  protected int WeaponTypeIndex;
  protected vp_WeaponHandler m_WeaponHandler;
  protected Transform m_Transform;
  protected vp_PlayerEventHandler m_Player;
  protected SkinnedMeshRenderer m_Renderer;
  protected Animator m_Animator;
  protected GameObject m_HeadPoint;
  protected GameObject m_DebugLookTarget;
  protected GameObject m_DebugLookArrow;

  protected vp_WeaponHandler WeaponHandler
  {
    get
    {
      if (m_WeaponHandler == null)
        m_WeaponHandler = (vp_WeaponHandler) transform.root.GetComponentInChildren(typeof (vp_WeaponHandler));
      return m_WeaponHandler;
    }
  }

  protected Transform Transform
  {
    get
    {
      if (m_Transform == null)
        m_Transform = transform;
      return m_Transform;
    }
  }

  protected vp_PlayerEventHandler Player
  {
    get
    {
      if (m_Player == null)
        m_Player = (vp_PlayerEventHandler) transform.root.GetComponentInChildren(typeof (vp_PlayerEventHandler));
      return m_Player;
    }
  }

  protected SkinnedMeshRenderer Renderer
  {
    get
    {
      if (m_Renderer == null)
        m_Renderer = transform.root.GetComponentInChildren<SkinnedMeshRenderer>();
      return m_Renderer;
    }
  }

  protected Animator Animator
  {
    get
    {
      if (m_Animator == null)
        m_Animator = GetComponent<Animator>();
      return m_Animator;
    }
  }

  protected Vector3 m_LocalVelocity => vp_MathUtility.SnapToZero(Transform.root.InverseTransformDirection(Player.Velocity.Get()) / m_MaxSpeed);

  protected float m_MaxSpeed
  {
    get
    {
      if (Player.Run.Active)
        return m_MaxRunSpeed;
      return Player.Crouch.Active ? m_MaxCrouchSpeed : m_MaxWalkSpeed;
    }
  }

  protected GameObject HeadPoint
  {
    get
    {
      if (m_HeadPoint == null)
      {
        m_HeadPoint = new GameObject(nameof (HeadPoint));
        m_HeadPoint.transform.parent = m_HeadLookBones[0].transform;
        m_HeadPoint.transform.localPosition = Vector3.zero;
        HeadPoint.transform.eulerAngles = Player.Rotation.Get();
      }
      return m_HeadPoint;
    }
  }

  protected GameObject DebugLookTarget
  {
    get
    {
      if (m_DebugLookTarget == null)
        m_DebugLookTarget = vp_3DUtility.DebugBall();
      return m_DebugLookTarget;
    }
  }

  protected GameObject DebugLookArrow
  {
    get
    {
      if (!(m_DebugLookArrow == null))
        return m_DebugLookArrow;
      m_DebugLookArrow = vp_3DUtility.DebugPointer();
      m_DebugLookArrow.transform.parent = HeadPoint.transform;
      m_DebugLookArrow.transform.localPosition = Vector3.zero;
      m_DebugLookArrow.transform.localRotation = Quaternion.identity;
      return m_DebugLookArrow;
    }
  }

  protected virtual void OnEnable()
  {
    if (!(Player != null))
      return;
    Register(Player);
  }

  protected virtual void OnDisable()
  {
    if (!(Player != null))
      return;
    Unregister(Player);
  }

  protected virtual void Awake()
  {
    if (!IsValidSetup())
      return;
    InitHashIDs();
    InitHeadLook();
    InitMaxSpeeds();
  }

  protected virtual void LateUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    if (!m_IsValid)
    {
      enabled = false;
    }
    else
    {
      UpdatePosition();
      UpdateGrounding();
      UpdateBody();
      UpdateSpine();
      UpdateAnimationSpeeds();
      UpdateAnimator();
      UpdateDebugInfo();
      UpdateHeadPoint();
    }
  }

  protected virtual void UpdateAnimationSpeeds()
  {
    if (Time.time > (double) m_NextAllowedUpdateTurnTargetTime)
    {
      m_CurrentTurnTarget = Mathf.DeltaAngle(m_PrevBodyYaw, m_BodyYaw) * (Player.Crouch.Active ? 100f : 0.2f);
      m_NextAllowedUpdateTurnTargetTime = Time.time + 0.1f;
    }
    if (Player.Platform.Get() == null || !Player.IsLocal.Get())
    {
      m_CurrentTurn = Mathf.Lerp(m_CurrentTurn, m_CurrentTurnTarget, Time.deltaTime);
      if (Mathf.Round(Transform.root.eulerAngles.y) == (double) Mathf.Round(m_LastYaw))
        m_CurrentTurn *= 0.6f;
      m_LastYaw = Transform.root.eulerAngles.y;
      m_CurrentTurn = vp_MathUtility.SnapToZero(m_CurrentTurn);
    }
    else
      m_CurrentTurn = 0.0f;
    m_CurrentForward = Mathf.Lerp(m_CurrentForward, m_LocalVelocity.z, Time.deltaTime * 100f);
    m_CurrentForward = Mathf.Abs(m_CurrentForward) > 0.029999999329447746 ? m_CurrentForward : 0.0f;
    m_CurrentStrafe = !Player.Crouch.Active ? Mathf.Lerp(m_CurrentStrafe, GetStrafeDirection(), Time.deltaTime * 5f) : (Mathf.Abs(GetStrafeDirection()) >= (double) Mathf.Abs(m_CurrentTurn) ? Mathf.Lerp(m_CurrentStrafe, GetStrafeDirection(), Time.deltaTime * 5f) : Mathf.Lerp(m_CurrentStrafe, m_CurrentTurn, Time.deltaTime * 5f));
    m_CurrentStrafe = Mathf.Abs(m_CurrentStrafe) > 0.029999999329447746 ? m_CurrentStrafe : 0.0f;
  }

  protected virtual float GetStrafeDirection()
  {
    if (Player.InputMoveVector.Get().x < 0.0)
      return -1f;
    return Player.InputMoveVector.Get().x > 0.0 ? 1f : 0.0f;
  }

  protected virtual void UpdateAnimator()
  {
    Animator.SetBool(IsRunning, Player.Run.Active && GetIsMoving());
    Animator.SetBool(IsCrouching, Player.Crouch.Active);
    Animator.SetInteger(WeaponTypeIndex, Player.CurrentWeaponType.Get());
    Animator.SetInteger(WeaponGripIndex, Player.CurrentWeaponGrip.Get());
    Animator.SetBool(IsSettingWeapon, Player.SetWeapon.Active);
    Animator.SetBool(IsReloading, Player.Reload.Active);
    Animator.SetBool(IsOutOfControl, Player.OutOfControl.Active);
    Animator.SetBool(IsClimbing, Player.Climb.Active);
    Animator.SetBool(IsZooming, Player.Zoom.Active);
    Animator.SetBool(IsGrounded, m_Grounded);
    Animator.SetBool(IsMoving, GetIsMoving());
    Animator.SetBool(IsFirstPerson, Player.IsFirstPerson.Get());
    Animator.SetFloat(TurnAmount, m_CurrentTurn);
    Animator.SetFloat(ForwardAmount, m_CurrentForward);
    Animator.SetFloat(StrafeAmount, m_CurrentStrafe);
    Animator.SetFloat(PitchAmount, (float) (-(double) Player.Rotation.Get().x / 90.0));
    if (m_Grounded)
      Animator.SetFloat(VerticalMoveAmount, 0.0f);
    else if (Player.Velocity.Get().y < 0.0)
      Animator.SetFloat(VerticalMoveAmount, Mathf.Lerp(Animator.GetFloat(VerticalMoveAmount), -1f, Time.deltaTime * 3f));
    else
      Animator.SetFloat(VerticalMoveAmount, Player.MotorThrottle.Get().y * 10f);
  }

  protected virtual void UpdateDebugInfo()
  {
    if (ShowDebugObjects)
    {
      DebugLookTarget.transform.position = m_HeadLookBones[0].transform.position + HeadPoint.transform.forward * 1000f;
      DebugLookArrow.transform.LookAt(DebugLookTarget.transform.position);
      if (!vp_Utility.IsActive(m_DebugLookTarget))
        vp_Utility.Activate(m_DebugLookTarget);
      if (vp_Utility.IsActive(m_DebugLookArrow))
        return;
      vp_Utility.Activate(m_DebugLookArrow);
    }
    else
    {
      if (m_DebugLookTarget != null)
        vp_Utility.Activate(m_DebugLookTarget, false);
      if (!(m_DebugLookArrow != null))
        return;
      vp_Utility.Activate(m_DebugLookArrow, false);
    }
  }

  protected virtual void UpdateHeadPoint()
  {
    if (!HeadPointDirty)
      return;
    HeadPoint.transform.eulerAngles = Player.Rotation.Get();
    HeadPointDirty = false;
  }

  protected virtual void UpdatePosition()
  {
    if (Player.IsFirstPerson.Get() || !Player.Climb.Active)
      return;
    Transform.localPosition += ClimbOffset;
  }

  protected virtual void UpdateBody()
  {
    m_PrevBodyYaw = m_BodyYaw;
    m_BodyYaw = Mathf.LerpAngle(m_BodyYaw, m_CurrentBodyYawTarget, Time.deltaTime * (Player.Velocity.Get().magnitude > 0.10000000149011612 ? FeetAdjustSpeedMoving : FeetAdjustSpeedStanding));
    m_BodyYaw = m_BodyYaw < -360.0 ? (m_BodyYaw += 360f) : m_BodyYaw;
    m_BodyYaw = m_BodyYaw > 360.0 ? (m_BodyYaw -= 360f) : m_BodyYaw;
    Transform.eulerAngles = m_BodyYaw * Vector3.up;
    m_CurrentHeadLookYaw = Mathf.DeltaAngle(Player.Rotation.Get().y, Transform.eulerAngles.y);
    if (Mathf.Max(0.0f, m_CurrentHeadLookYaw - 90f) > 0.0)
    {
      Transform.eulerAngles = Vector3.up * (Transform.root.eulerAngles.y + 90f);
      m_BodyYaw = m_CurrentBodyYawTarget = Transform.eulerAngles.y;
    }
    else if (Mathf.Min(0.0f, m_CurrentHeadLookYaw - -90f) < 0.0)
    {
      Transform.eulerAngles = Vector3.up * (Transform.root.eulerAngles.y - 90f);
      m_BodyYaw = m_CurrentBodyYawTarget = Transform.eulerAngles.y;
    }
    if (Mathf.Abs(Player.Rotation.Get().y - m_BodyYaw) > 180.0)
    {
      if (m_BodyYaw > 0.0)
      {
        m_BodyYaw -= 360f;
        m_PrevBodyYaw -= 360f;
      }
      else if (m_BodyYaw < 0.0)
      {
        m_BodyYaw += 360f;
        m_PrevBodyYaw += 360f;
      }
    }
    if (m_CurrentHeadLookYaw <= (double) FeetAdjustAngle && m_CurrentHeadLookYaw >= -(double) FeetAdjustAngle && Player.Velocity.Get().magnitude <= 0.10000000149011612 && (!Player.Crouch.Active || !Player.Attack.Active && !Player.Zoom.Active))
      return;
    m_CurrentBodyYawTarget = Mathf.LerpAngle(m_CurrentBodyYawTarget, Transform.root.eulerAngles.y, 0.1f);
  }

  protected virtual void UpdateSpine()
  {
    if (Player.Climb.Active)
      return;
    for (int index = 0; index < m_HeadLookBones.Count; ++index)
    {
      m_HeadLookTargetFalloffs[index] = !Player.IsFirstPerson.Get() && !Animator.GetBool(IsAttacking) && !Animator.GetBool(IsZooming) || Animator.GetBool(IsCrouching) ? m_HeadLookFalloffs[index] : m_HeadLookFalloffs[m_HeadLookFalloffs.Count - 1 - index];
      if (m_WasMoving && !Animator.GetBool(IsMoving))
        m_HeadLookCurrentFalloffs[index] = m_HeadLookTargetFalloffs[index];
      m_HeadLookCurrentFalloffs[index] = Mathf.SmoothStep(m_HeadLookCurrentFalloffs[index], Mathf.LerpAngle(m_HeadLookCurrentFalloffs[index], m_HeadLookTargetFalloffs[index], Time.deltaTime * 10f), Time.deltaTime * 20f);
      if (Player.IsFirstPerson.Get())
      {
        m_HeadLookTargetWorldDir = GetLookPoint() - m_HeadLookBones[0].transform.position;
        m_HeadLookCurrentWorldDir = Vector3.Slerp(m_HeadLookTargetWorldDir, vp_3DUtility.HorizontalVector(m_HeadLookTargetWorldDir), m_HeadLookCurrentFalloffs[index] / m_HeadLookFalloffs[0]);
      }
      else
      {
        m_ValidLookPoint = GetLookPoint();
        m_ValidLookPointForward = Transform.InverseTransformDirection(m_ValidLookPoint - m_HeadLookBones[0].transform.position).z;
        if (m_ValidLookPointForward < 0.0)
          m_ValidLookPoint += Transform.forward * -m_ValidLookPointForward;
        m_HeadLookTargetWorldDir = Vector3.Slerp(m_HeadLookTargetWorldDir, m_ValidLookPoint - m_HeadLookBones[0].transform.position, Time.deltaTime * HeadYawSpeed);
        m_HeadLookCurrentWorldDir = Vector3.Slerp(m_HeadLookCurrentWorldDir, vp_3DUtility.HorizontalVector(m_HeadLookTargetWorldDir), m_HeadLookCurrentFalloffs[index] / m_HeadLookFalloffs[0]);
      }
      m_HeadLookBones[index].transform.rotation = vp_3DUtility.GetBoneLookRotationInWorldSpace(m_HeadLookBones[index].transform.rotation, m_HeadLookBones[m_HeadLookBones.Count - 1].transform.parent.rotation, m_HeadLookCurrentWorldDir, m_HeadLookCurrentFalloffs[index], m_ReferenceLookDirs[index], m_ReferenceUpDirs[index], Quaternion.identity);
      if (!Player.IsFirstPerson.Get())
      {
        m_CurrentHeadLookPitch = Mathf.SmoothStep(m_CurrentHeadLookPitch, Mathf.Clamp(Player.Rotation.Get().x, -HeadPitchCap, HeadPitchCap), Time.deltaTime * HeadPitchSpeed);
        m_HeadLookBones[index].transform.Rotate(HeadPoint.transform.right, m_CurrentHeadLookPitch * Mathf.Lerp(m_HeadLookFalloffs[index], m_HeadLookCurrentFalloffs[index], LeaningFactor), Space.World);
      }
    }
    m_WasMoving = Animator.GetBool(IsMoving);
  }

  protected virtual bool GetIsMoving() => Vector3.Scale(Player.Velocity.Get(), Vector3.right + Vector3.forward).magnitude > 0.0099999997764825821;

  protected virtual Vector3 GetLookPoint()
  {
    m_HeadLookBackup = HeadPoint.transform.eulerAngles;
    HeadPoint.transform.eulerAngles = vp_MathUtility.NaNSafeVector3(Player.Rotation.Get());
    m_LookPoint = HeadPoint.transform.position + HeadPoint.transform.forward * 1000f;
    HeadPoint.transform.eulerAngles = vp_MathUtility.NaNSafeVector3(m_HeadLookBackup);
    return m_LookPoint;
  }

  protected virtual List<float> CalculateBoneFalloffs(List<GameObject> boneList)
  {
    List<float> boneFalloffs = new List<float>();
    float num1 = 0.0f;
    for (int index = boneList.Count - 1; index > -1; --index)
    {
      if (boneList[index] == null)
      {
        boneList.RemoveAt(index);
      }
      else
      {
        float num2 = Mathf.Lerp(0.0f, 1f, (index + 1) / (float) boneList.Count);
        boneFalloffs.Add(num2 * num2 * num2);
        num1 += num2 * num2 * num2;
      }
    }
    if (boneList.Count == 0)
      return boneFalloffs;
    for (int index = 0; index < boneFalloffs.Count; ++index)
      boneFalloffs[index] *= 1f / num1;
    return boneFalloffs;
  }

  protected virtual void StoreReferenceDirections()
  {
    for (int index = 0; index < m_HeadLookBones.Count; ++index)
    {
      Quaternion quaternion = Quaternion.Inverse(m_HeadLookBones[m_HeadLookBones.Count - 1].transform.parent.rotation);
      m_ReferenceLookDirs.Add(quaternion * Transform.rotation * Vector3.forward);
      m_ReferenceUpDirs.Add(quaternion * Transform.rotation * Vector3.up);
    }
  }

  protected virtual void UpdateGrounding()
  {
    Physics.SphereCast(new Ray(Transform.position + Vector3.up * 0.5f, Vector3.down), 0.4f, out m_GroundHit, 1f, -675375893);
    m_Grounded = m_GroundHit.collider != null;
  }

  protected virtual void RefreshWeaponStates()
  {
    if (WeaponHandler == null || WeaponHandler.CurrentWeapon == null)
      return;
    WeaponHandler.CurrentWeapon.SetState("Attack", Player.Attack.Active);
    WeaponHandler.CurrentWeapon.SetState("Zoom", Player.Zoom.Active);
  }

  protected virtual void InitMaxSpeeds()
  {
    if (Player.IsLocal.Get())
    {
      vp_FPController componentInChildren = Transform.root.GetComponentInChildren<vp_FPController>();
      m_MaxWalkSpeed = componentInChildren.CalculateMaxSpeed();
      m_MaxRunSpeed = componentInChildren.CalculateMaxSpeed("Run");
      m_MaxCrouchSpeed = componentInChildren.CalculateMaxSpeed("Crouch");
    }
    else
    {
      m_MaxWalkSpeed = 3.999999f;
      m_MaxRunSpeed = 10.08f;
      m_MaxCrouchSpeed = 1.44f;
    }
  }

  protected virtual void InitHashIDs()
  {
    ForwardAmount = Animator.StringToHash("Forward");
    PitchAmount = Animator.StringToHash("Pitch");
    StrafeAmount = Animator.StringToHash("Strafe");
    TurnAmount = Animator.StringToHash("Turn");
    VerticalMoveAmount = Animator.StringToHash("VerticalMove");
    IsAttacking = Animator.StringToHash("IsAttacking");
    IsClimbing = Animator.StringToHash("IsClimbing");
    IsCrouching = Animator.StringToHash("IsCrouching");
    IsGrounded = Animator.StringToHash("IsGrounded");
    IsMoving = Animator.StringToHash("IsMoving");
    IsOutOfControl = Animator.StringToHash("IsOutOfControl");
    IsReloading = Animator.StringToHash("IsReloading");
    IsRunning = Animator.StringToHash("IsRunning");
    IsSettingWeapon = Animator.StringToHash("IsSettingWeapon");
    IsZooming = Animator.StringToHash("IsZooming");
    IsFirstPerson = Animator.StringToHash("IsFirstPerson");
    StartClimb = Animator.StringToHash("StartClimb");
    StartOutOfControl = Animator.StringToHash("StartOutOfControl");
    StartReload = Animator.StringToHash("StartReload");
    WeaponGripIndex = Animator.StringToHash("WeaponGrip");
    WeaponTypeIndex = Animator.StringToHash("WeaponType");
  }

  protected virtual void InitHeadLook()
  {
    if (!m_IsValid)
      return;
    m_HeadLookBones.Clear();
    for (GameObject gameObject = HeadBone; gameObject != LowestSpineBone.transform.parent.gameObject; gameObject = gameObject.transform.parent.gameObject)
      m_HeadLookBones.Add(gameObject);
    m_ReferenceUpDirs = new List<Vector3>();
    m_ReferenceLookDirs = new List<Vector3>();
    m_HeadLookFalloffs = CalculateBoneFalloffs(m_HeadLookBones);
    m_HeadLookCurrentFalloffs = new List<float>(m_HeadLookFalloffs);
    m_HeadLookTargetFalloffs = new List<float>(m_HeadLookFalloffs);
    StoreReferenceDirections();
  }

  protected virtual bool IsValidSetup()
  {
    if (HeadBone == null)
      Debug.LogError("Error (" + this + ") No gameobject has been assigned for 'HeadBone'.");
    else if (LowestSpineBone == null)
      Debug.LogError("Error (" + this + ") No gameobject has been assigned for 'LowestSpineBone'.");
    else if (!vp_Utility.IsDescendant(HeadBone.transform, transform.root))
      NotInSameHierarchyError(HeadBone);
    else if (!vp_Utility.IsDescendant(LowestSpineBone.transform, transform.root))
    {
      NotInSameHierarchyError(LowestSpineBone);
    }
    else
    {
      if (vp_Utility.IsDescendant(HeadBone.transform, LowestSpineBone.transform))
        return true;
      Debug.LogError("Error (" + this + ") 'HeadBone' must be a child or descendant of 'LowestSpineBone'.");
    }
    m_IsValid = false;
    enabled = false;
    return false;
  }

  protected virtual void NotInSameHierarchyError(GameObject o) => Debug.LogError("Error '" + o + "' can not be used as a bone for  " + this + " because it is not part of the same hierarchy.");

  protected virtual float Get_BodyYaw() => Transform.eulerAngles.y;

  protected virtual void Set_BodyYaw(float value) => m_BodyYaw = value;

  protected virtual float OnValue_BodyYaw
  {
    get => Transform.eulerAngles.y;
    set => m_BodyYaw = value;
  }

  protected virtual void OnStart_Attack()
  {
    if (Player.CurrentWeaponType.Get() == 3)
      return;
    Animator.SetBool(IsAttacking, true);
  }

  protected virtual void OnStop_Attack() => vp_Timer.In(0.5f, () =>
  {
    Animator.SetBool(IsAttacking, false);
    RefreshWeaponStates();
  }, m_AttackDoneTimer);

  protected virtual void OnStop_Zoom() => vp_Timer.In(0.5f, () =>
  {
    if (!Player.Attack.Active)
      Animator.SetBool(IsAttacking, false);
    RefreshWeaponStates();
  }, m_AttackDoneTimer);

  protected virtual void OnStart_Reload() => Animator.SetTrigger(StartReload);

  protected virtual void OnStart_OutOfControl() => Animator.SetTrigger(StartOutOfControl);

  protected virtual void OnStart_Climb() => Animator.SetTrigger(StartClimb);

  protected virtual void OnStart_Dead()
  {
    if (!m_AttackDoneTimer.Active)
      return;
    m_AttackDoneTimer.Execute();
  }

  protected virtual void OnStop_Dead() => HeadPointDirty = true;

  protected virtual void OnMessage_CameraToggle3rdPerson()
  {
    m_WasMoving = !m_WasMoving;
    HeadPointDirty = true;
  }

  protected virtual Vector3 Get_HeadLookDirection() => (Player.LookPoint.Get() - HeadPoint.transform.position).normalized;

  protected virtual Vector3 OnValue_HeadLookDirection => (Player.LookPoint.Get() - HeadPoint.transform.position).normalized;

  protected virtual Vector3 OnValue_LookPoint => GetLookPoint();

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.RegisterActivity("Attack", OnStart_Attack, OnStop_Attack, null, null, null, null);
    eventHandler.RegisterActivity("Climb", OnStart_Climb, null, null, null, null, null);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.RegisterActivity("OutOfControl", OnStart_OutOfControl, null, null, null, null, null);
    eventHandler.RegisterActivity("Reload", OnStart_Reload, null, null, null, null, null);
    eventHandler.RegisterActivity("Zoom", null, OnStop_Zoom, null, null, null, null);
    eventHandler.RegisterValue("HeadLookLocation", Get_HeadLookDirection, null);
    eventHandler.RegisterValue("LookPoint", GetLookPoint, null);
    eventHandler.RegisterValue("BodyYaw", Get_BodyYaw, Set_BodyYaw);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.UnregisterActivity("Attack", OnStart_Attack, OnStop_Attack, null, null, null, null);
    eventHandler.UnregisterActivity("Climb", OnStart_Climb, null, null, null, null, null);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.UnregisterActivity("OutOfControl", OnStart_OutOfControl, null, null, null, null, null);
    eventHandler.UnregisterActivity("Reload", OnStart_Reload, null, null, null, null, null);
    eventHandler.UnregisterActivity("Zoom", null, OnStop_Zoom, null, null, null, null);
    eventHandler.UnregisterValue("HeadLookLocation", Get_HeadLookDirection, null);
    eventHandler.UnregisterValue("LookPoint", GetLookPoint, null);
    eventHandler.UnregisterValue("BodyYaw", Get_BodyYaw, Set_BodyYaw);
  }
}
