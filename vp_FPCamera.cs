// Decompiled with JetBrains decompiler
// Type: vp_FPCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[RequireComponent(typeof (Camera))]
[RequireComponent(typeof (AudioListener))]
public class vp_FPCamera : vp_Component
{
  public vp_FPController FPController;
  public float RenderingFieldOfView = 60f;
  public float RenderingZoomDamping = 0.2f;
  protected float m_FinalZoomTime;
  protected float m_ZoomOffset;
  public Vector3 PositionOffset = new Vector3(0.0f, 1.75f, 0.1f);
  public float PositionGroundLimit = 0.1f;
  public float PositionSpringStiffness = 0.01f;
  public float PositionSpringDamping = 0.25f;
  public float PositionSpring2Stiffness = 0.95f;
  public float PositionSpring2Damping = 0.25f;
  public float PositionKneeling = 0.025f;
  public int PositionKneelingSoftness = 1;
  public float PositionEarthQuakeFactor = 1f;
  protected vp_Spring m_PositionSpring;
  protected vp_Spring m_PositionSpring2;
  protected bool m_DrawCameraCollisionDebugLine;
  protected Vector3 PositionOnDeath = Vector3.zero;
  public Vector2 RotationPitchLimit = new Vector2(90f, -90f);
  public Vector2 RotationYawLimit = new Vector2(-360f, 360f);
  public float RotationSpringStiffness = 0.01f;
  public float RotationSpringDamping = 0.25f;
  public float RotationKneeling = 0.025f;
  public int RotationKneelingSoftness = 1;
  public float RotationStrafeRoll = 0.01f;
  public float RotationEarthQuakeFactor;
  public Vector3 LookPoint = Vector3.zero;
  protected float m_Pitch;
  protected float m_Yaw;
  protected vp_Spring m_RotationSpring;
  protected RaycastHit m_LookPointHit;
  public Vector3 Position3rdPersonOffset = new Vector3(0.5f, 0.1f, 0.75f);
  protected float m_Current3rdPersonBlend;
  protected Vector3 m_Final3rdPersonCameraOffset = Vector3.zero;
  public float ShakeSpeed;
  public Vector3 ShakeAmplitude = new Vector3(10f, 10f, 0.0f);
  protected Vector3 m_Shake = Vector3.zero;
  public Vector4 BobRate = new Vector4(0.0f, 1.4f, 0.0f, 0.7f);
  public Vector4 BobAmplitude = new Vector4(0.0f, 0.25f, 0.0f, 0.5f);
  public float BobInputVelocityScale = 1f;
  public float BobMaxInputVelocity = 100f;
  public bool BobRequireGroundContact = true;
  protected float m_LastBobSpeed;
  protected Vector4 m_CurrentBobAmp = Vector4.zero;
  protected Vector4 m_CurrentBobVal = Vector4.zero;
  protected float m_BobSpeed;
  public BobStepDelegate BobStepCallback;
  public float BobStepThreshold = 10f;
  protected float m_LastUpBob;
  protected bool m_BobWasElevating;
  public bool HasCollision = true;
  protected Vector3 m_CollisionVector = Vector3.zero;
  protected Vector3 m_CameraCollisionStartPos = Vector3.zero;
  protected Vector3 m_CameraCollisionEndPos = Vector3.zero;
  protected RaycastHit m_CameraHit;
  private vp_FPPlayerEventHandler m_Player;
  private Rigidbody m_FirstRigidbody;

  public float ZoomOffset
  {
    get => m_ZoomOffset;
    set => m_ZoomOffset = value;
  }

  public bool DrawCameraCollisionDebugLine
  {
    get => m_DrawCameraCollisionDebugLine;
    set => m_DrawCameraCollisionDebugLine = value;
  }

  public Vector3 CollisionVector => m_CollisionVector;

  private vp_FPPlayerEventHandler Player
  {
    get
    {
      if (m_Player == null && EventHandler != null)
        m_Player = (vp_FPPlayerEventHandler) EventHandler;
      return m_Player;
    }
  }

  private Rigidbody FirstRigidBody
  {
    get
    {
      if (m_FirstRigidbody == null)
        m_FirstRigidbody = Transform.root.GetComponentInChildren<Rigidbody>();
      return m_FirstRigidbody;
    }
  }

  public Vector2 Angle
  {
    get => new Vector2(m_Pitch, m_Yaw);
    set
    {
      Pitch = value.x;
      Yaw = value.y;
    }
  }

  public Vector3 Forward => m_Transform.forward;

  public float Pitch
  {
    get => m_Pitch;
    set
    {
      if (value > 90.0)
        value -= 360f;
      m_Pitch = value;
    }
  }

  public float Yaw
  {
    get => m_Yaw;
    set => m_Yaw = value;
  }

  protected override void Awake()
  {
    base.Awake();
    FPController = Root.GetComponent<vp_FPController>();
    SetRotation(new Vector2(Transform.eulerAngles.x, Transform.eulerAngles.y));
    Parent.gameObject.layer = 8;
    GetComponent<Camera>().cullingMask &= 2147483391;
    GetComponent<Camera>().depth = 0.0f;
    m_PositionSpring = new vp_Spring(Transform, vp_Spring.UpdateMode.Position, false);
    m_PositionSpring.MinVelocity = 1E-05f;
    m_PositionSpring.RestState = PositionOffset;
    m_PositionSpring2 = new vp_Spring(Transform, vp_Spring.UpdateMode.PositionAdditiveLocal, false);
    m_PositionSpring2.MinVelocity = 1E-05f;
    m_RotationSpring = new vp_Spring(Transform, vp_Spring.UpdateMode.RotationAdditiveLocal, false);
    m_RotationSpring.MinVelocity = 1E-05f;
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    vp_TargetEvent<float>.Register(m_Root, "CameraBombShake", OnMessage_CameraBombShake);
    vp_TargetEvent<float>.Register(m_Root, "CameraGroundStomp", OnMessage_CameraGroundStomp);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    vp_TargetEvent<float>.Unregister(m_Root, "CameraBombShake", OnMessage_CameraBombShake);
    vp_TargetEvent<float>.Unregister(m_Root, "CameraGroundStomp", OnMessage_CameraGroundStomp);
  }

  protected override void Start()
  {
    base.Start();
    Refresh();
    SnapSprings();
    SnapZoom();
  }

  protected override void Init() => base.Init();

  protected override void Update()
  {
    base.Update();
    if (Time.timeScale == 0.0)
      return;
    UpdateInput();
  }

  protected override void FixedUpdate()
  {
    base.FixedUpdate();
    if (Time.timeScale == 0.0)
      return;
    UpdateSwaying();
    UpdateBob();
    UpdateEarthQuake();
    UpdateShakes();
    UpdateSprings();
  }

  protected override void LateUpdate()
  {
    base.LateUpdate();
    if (Time.timeScale == 0.0)
      return;
    m_Transform.position = FPController.SmoothPosition;
    if (Player.IsFirstPerson.Get())
      m_Transform.localPosition += m_PositionSpring.State + m_PositionSpring2.State;
    else
      m_Transform.localPosition += m_PositionSpring.State + Vector3.Scale(m_PositionSpring2.State, Vector3.up);
    if (HasCollision)
      DoCameraCollision();
    Quaternion quaternion1 = Quaternion.AngleAxis(m_Yaw, Vector3.up);
    Quaternion quaternion2 = Quaternion.AngleAxis(0.0f, Vector3.left);
    Parent.rotation = vp_MathUtility.NaNSafeQuaternion(quaternion1 * quaternion2, Parent.rotation);
    Quaternion quaternion3 = Quaternion.AngleAxis(-m_Pitch, Vector3.left);
    Transform.rotation = vp_MathUtility.NaNSafeQuaternion(quaternion1 * quaternion3, Transform.rotation);
    Transform.localEulerAngles += vp_MathUtility.NaNSafeVector3(Vector3.forward * m_RotationSpring.State.z);
    Update3rdPerson();
  }

  private void Update3rdPerson()
  {
    if (Position3rdPersonOffset == Vector3.zero)
      return;
    if (PositionOnDeath != Vector3.zero)
    {
      Transform.position = PositionOnDeath;
      if (FirstRigidBody != null)
        Transform.LookAt(FirstRigidBody.transform.position + Vector3.up);
      else
        Transform.LookAt(Root.position + Vector3.up);
    }
    else if (Player.IsFirstPerson.Get())
    {
      m_Final3rdPersonCameraOffset = Vector3.zero;
      m_Current3rdPersonBlend = 0.0f;
      LookPoint = GetLookPoint();
    }
    else
    {
      m_Current3rdPersonBlend = Mathf.Lerp(m_Current3rdPersonBlend, 1f, Time.deltaTime);
      m_Final3rdPersonCameraOffset = Transform.position;
      if (Transform.localPosition.z > -0.20000000298023224)
        Transform.localPosition = new Vector3(Transform.localPosition.x, Transform.localPosition.y, -0.2f);
      Transform.position = Vector3.Lerp(Transform.position, Transform.position + m_Transform.right * Position3rdPersonOffset.x + m_Transform.up * Position3rdPersonOffset.y + m_Transform.forward * Position3rdPersonOffset.z, m_Current3rdPersonBlend);
      m_Final3rdPersonCameraOffset -= Transform.position;
      DoCameraCollision();
      LookPoint = GetLookPoint();
    }
  }

  public virtual void DoCameraCollision()
  {
    m_CameraCollisionStartPos = FPController.Transform.TransformPoint(0.0f, PositionOffset.y, 0.0f) - (m_Player.IsFirstPerson.Get() ? Vector3.zero : FPController.Transform.position - FPController.SmoothPosition);
    m_CameraCollisionEndPos = Transform.position + (Transform.position - m_CameraCollisionStartPos).normalized * FPController.CharacterController.radius;
    m_CollisionVector = Vector3.zero;
    if (Physics.Linecast(m_CameraCollisionStartPos, m_CameraCollisionEndPos, out m_CameraHit, -675375893) && !m_CameraHit.collider.isTrigger)
    {
      Transform.position = m_CameraHit.point - (m_CameraHit.point - m_CameraCollisionStartPos).normalized * FPController.CharacterController.radius;
      m_CollisionVector = m_CameraHit.point - m_CameraCollisionEndPos;
    }
    if (Transform.localPosition.y >= (double) PositionGroundLimit)
      return;
    Transform.localPosition = new Vector3(Transform.localPosition.x, PositionGroundLimit, Transform.localPosition.z);
  }

  public virtual void AddForce(Vector3 force) => m_PositionSpring.AddForce(force);

  public virtual void AddForce(float x, float y, float z) => AddForce(new Vector3(x, y, z));

  public virtual void AddForce2(Vector3 force) => m_PositionSpring2.AddForce(force);

  public void AddForce2(float x, float y, float z) => AddForce2(new Vector3(x, y, z));

  public virtual void AddRollForce(float force) => m_RotationSpring.AddForce(Vector3.forward * force);

  protected virtual void UpdateInput()
  {
    if (Player.Dead.Active || Player.InputSmoothLook.Get() == Vector2.zero)
      return;
    m_Yaw += Player.InputSmoothLook.Get().x;
    m_Pitch += Player.InputSmoothLook.Get().y;
    m_Yaw = m_Yaw < -360.0 ? (m_Yaw += 360f) : m_Yaw;
    m_Yaw = m_Yaw > 360.0 ? (m_Yaw -= 360f) : m_Yaw;
    m_Yaw = Mathf.Clamp(m_Yaw, RotationYawLimit.x, RotationYawLimit.y);
    m_Pitch = m_Pitch < -360.0 ? (m_Pitch += 360f) : m_Pitch;
    m_Pitch = m_Pitch > 360.0 ? (m_Pitch -= 360f) : m_Pitch;
    m_Pitch = Mathf.Clamp(m_Pitch, -RotationPitchLimit.x, -RotationPitchLimit.y);
  }

  protected virtual void UpdateZoom()
  {
  }

  public void RefreshZoom()
  {
  }

  public virtual void Zoom() => m_FinalZoomTime = Time.time + RenderingZoomDamping;

  public virtual void SnapZoom()
  {
  }

  protected virtual void UpdateShakes()
  {
    if (ShakeSpeed == 0.0)
      return;
    m_Yaw -= m_Shake.y;
    m_Pitch -= m_Shake.x;
    m_Shake = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(ShakeSpeed), ShakeAmplitude);
    m_Yaw += m_Shake.y;
    m_Pitch += m_Shake.x;
    m_RotationSpring.AddForce(Vector3.forward * m_Shake.z * Time.timeScale);
  }

  protected virtual void UpdateBob()
  {
    if (BobAmplitude == Vector4.zero || BobRate == Vector4.zero || !Player.IsFirstPerson.Get())
      return;
    m_BobSpeed = !BobRequireGroundContact || FPController.Grounded ? FPController.CharacterController.velocity.sqrMagnitude : 0.0f;
    m_BobSpeed = Mathf.Min(m_BobSpeed * BobInputVelocityScale, BobMaxInputVelocity);
    m_BobSpeed = Mathf.Round(m_BobSpeed * 1000f) / 1000f;
    if (m_BobSpeed == 0.0)
      m_BobSpeed = Mathf.Min(m_LastBobSpeed * 0.93f, BobMaxInputVelocity);
    m_CurrentBobAmp.y = m_BobSpeed * (BobAmplitude.y * -0.0001f);
    m_CurrentBobVal.y = Mathf.Cos(Time.time * (BobRate.y * 10f)) * m_CurrentBobAmp.y;
    m_CurrentBobAmp.x = m_BobSpeed * (BobAmplitude.x * 0.0001f);
    m_CurrentBobVal.x = Mathf.Cos(Time.time * (BobRate.x * 10f)) * m_CurrentBobAmp.x;
    m_CurrentBobAmp.z = m_BobSpeed * (BobAmplitude.z * 0.0001f);
    m_CurrentBobVal.z = Mathf.Cos(Time.time * (BobRate.z * 10f)) * m_CurrentBobAmp.z;
    m_CurrentBobAmp.w = m_BobSpeed * (BobAmplitude.w * 0.0001f);
    m_CurrentBobVal.w = Mathf.Cos(Time.time * (BobRate.w * 10f)) * m_CurrentBobAmp.w;
    m_PositionSpring.AddForce((Vector3) m_CurrentBobVal * Time.timeScale);
    AddRollForce(m_CurrentBobVal.w * Time.timeScale);
    m_LastBobSpeed = m_BobSpeed;
    DetectBobStep(m_BobSpeed, m_CurrentBobVal.y);
  }

  protected virtual void DetectBobStep(float speed, float upBob)
  {
    if (BobStepCallback == null || speed < (double) BobStepThreshold)
      return;
    bool flag = m_LastUpBob < (double) upBob;
    m_LastUpBob = upBob;
    if (flag && !m_BobWasElevating)
      BobStepCallback();
    m_BobWasElevating = flag;
  }

  protected virtual void UpdateSwaying() => AddRollForce((Transform.InverseTransformDirection(FPController.CharacterController.velocity * 0.016f) * Time.timeScale).x * RotationStrafeRoll);

  protected virtual void UpdateEarthQuake()
  {
    if (Player == null || !Player.CameraEarthQuake.Active)
      return;
    if (m_PositionSpring.State.y >= (double) m_PositionSpring.RestState.y)
    {
      Vector3 o = Player.CameraEarthQuakeForce.Get();
      o.y = -o.y;
      Player.CameraEarthQuakeForce.Set(o);
    }
    m_PositionSpring.AddForce(Player.CameraEarthQuakeForce.Get() * PositionEarthQuakeFactor);
    m_RotationSpring.AddForce(Vector3.forward * (float) (-(double) Player.CameraEarthQuakeForce.Get().x * 2.0) * RotationEarthQuakeFactor);
  }

  protected virtual void UpdateSprings()
  {
    m_PositionSpring.FixedUpdate();
    m_PositionSpring2.FixedUpdate();
    m_RotationSpring.FixedUpdate();
  }

  public virtual void DoBomb(Vector3 positionForce, float minRollForce, float maxRollForce)
  {
    AddForce2(positionForce);
    float force = UnityEngine.Random.Range(minRollForce, maxRollForce);
    if (UnityEngine.Random.value > 0.5)
      force = -force;
    AddRollForce(force);
  }

  public override void Refresh()
  {
    if (!Application.isPlaying)
      return;
    if (m_PositionSpring != null)
    {
      m_PositionSpring.Stiffness = new Vector3(PositionSpringStiffness, PositionSpringStiffness, PositionSpringStiffness);
      m_PositionSpring.Damping = Vector3.one - new Vector3(PositionSpringDamping, PositionSpringDamping, PositionSpringDamping);
      m_PositionSpring.MinState.y = PositionGroundLimit;
      m_PositionSpring.RestState = PositionOffset;
    }
    if (m_PositionSpring2 != null)
    {
      m_PositionSpring2.Stiffness = new Vector3(PositionSpring2Stiffness, PositionSpring2Stiffness, PositionSpring2Stiffness);
      m_PositionSpring2.Damping = Vector3.one - new Vector3(PositionSpring2Damping, PositionSpring2Damping, PositionSpring2Damping);
      m_PositionSpring2.MinState.y = -PositionOffset.y + PositionGroundLimit;
    }
    if (m_RotationSpring != null)
    {
      m_RotationSpring.Stiffness = new Vector3(RotationSpringStiffness, RotationSpringStiffness, RotationSpringStiffness);
      m_RotationSpring.Damping = Vector3.one - new Vector3(RotationSpringDamping, RotationSpringDamping, RotationSpringDamping);
    }
    Zoom();
  }

  public virtual void SnapSprings()
  {
    if (m_PositionSpring != null)
    {
      m_PositionSpring.RestState = PositionOffset;
      m_PositionSpring.State = PositionOffset;
      m_PositionSpring.Stop(true);
    }
    if (m_PositionSpring2 != null)
    {
      m_PositionSpring2.RestState = Vector3.zero;
      m_PositionSpring2.State = Vector3.zero;
      m_PositionSpring2.Stop(true);
    }
    if (m_RotationSpring == null)
      return;
    m_RotationSpring.RestState = Vector3.zero;
    m_RotationSpring.State = Vector3.zero;
    m_RotationSpring.Stop(true);
  }

  public virtual void StopSprings()
  {
    if (m_PositionSpring != null)
      m_PositionSpring.Stop(true);
    if (m_PositionSpring2 != null)
      m_PositionSpring2.Stop(true);
    if (m_RotationSpring != null)
      m_RotationSpring.Stop(true);
    m_BobSpeed = 0.0f;
    m_LastBobSpeed = 0.0f;
  }

  public virtual void Stop()
  {
    SnapSprings();
    SnapZoom();
    Refresh();
  }

  public virtual void SetRotation(Vector2 eulerAngles, bool stopZoomAndSprings)
  {
    Angle = eulerAngles;
    if (!stopZoomAndSprings)
      return;
    Stop();
  }

  public virtual void SetRotation(Vector2 eulerAngles)
  {
    Angle = eulerAngles;
    Stop();
  }

  public virtual void SetRotation(Vector2 eulerAngles, bool stopZoomAndSprings, bool obsolete) => SetRotation(eulerAngles, stopZoomAndSprings);

  public Vector3 GetLookPoint() => !Player.IsFirstPerson.Get() && Physics.Linecast(Transform.position, Transform.position + Transform.forward * 1000f, out m_LookPointHit, -675375893) && !m_LookPointHit.collider.isTrigger && Root.InverseTransformPoint(m_LookPointHit.point).z > 0.0 ? m_LookPointHit.point : Transform.position + Transform.forward * 1000f;

  public virtual Vector3 Get_LookPoint() => LookPoint;

  public virtual Vector3 OnValue_LookPoint => LookPoint;

  protected virtual Vector3 Get_CameraLookDirection() => (Player.LookPoint.Get() - Transform.position).normalized;

  protected virtual Vector3 OnValue_CameraLookDirection => (Player.LookPoint.Get() - Transform.position).normalized;

  protected virtual void OnMessage_FallImpact(float impact)
  {
    impact = Mathf.Abs(impact * 55f);
    float t1 = impact * PositionKneeling;
    float t2 = impact * RotationKneeling;
    float num1 = Mathf.SmoothStep(0.0f, 1f, t1);
    float num2 = Mathf.SmoothStep(0.0f, 1f, Mathf.SmoothStep(0.0f, 1f, t2));
    if (m_PositionSpring != null)
      m_PositionSpring.AddSoftForce(Vector3.down * num1, PositionKneelingSoftness);
    if (m_RotationSpring == null)
      return;
    m_RotationSpring.AddSoftForce(Vector3.forward * (UnityEngine.Random.value > 0.5 ? num2 * 2f : (float) -(num2 * 2.0)), RotationKneelingSoftness);
  }

  protected virtual void OnMessage_HeadImpact(float impact)
  {
    if (m_RotationSpring == null || Mathf.Abs(m_RotationSpring.State.z) >= 30.0)
      return;
    m_RotationSpring.AddForce(Vector3.forward * (impact * 20f) * Time.timeScale);
  }

  protected virtual void OnMessage_CameraGroundStomp(float impact) => AddForce2(new Vector3(0.0f, -1f, 0.0f) * impact);

  protected virtual void OnMessage_CameraBombShake(float impact) => DoBomb(new Vector3(1f, -10f, 1f) * impact, 1f, 2f);

  protected virtual void OnStart_Zoom()
  {
    if (Player == null)
      return;
    Player.Run.Stop();
  }

  protected virtual bool CanStart_Run() => Player == null || !Player.Zoom.Active;

  protected virtual Vector2 Get_Rotation() => Angle;

  protected virtual void Set_Rotation(Vector2 value) => Angle = value;

  protected virtual Vector2 OnValue_Rotation
  {
    get => Angle;
    set => Angle = value;
  }

  protected virtual void OnMessage_Stop() => Stop();

  protected virtual void OnStart_Dead()
  {
    if (Player.IsFirstPerson.Get())
      return;
    PositionOnDeath = Transform.position - m_Final3rdPersonCameraOffset;
  }

  protected virtual void OnStop_Dead()
  {
    if (Player.IsFirstPerson.Get())
      return;
    PositionOnDeath = Vector3.zero;
    m_Current3rdPersonBlend = 0.0f;
  }

  protected virtual bool Get_IsLocal() => true;

  protected virtual bool OnValue_IsLocal => true;

  protected virtual void OnMessage_CameraToggle3rdPerson() => m_Player.IsFirstPerson.Set(!m_Player.IsFirstPerson.Get());

  public override void Register(vp_EventHandler eventHandler)
  {
    base.Register(eventHandler);
    eventHandler.RegisterActivity("Run", null, null, CanStart_Run, null, null, null);
    eventHandler.RegisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.RegisterMessage<float>("CameraGroundStomp", OnMessage_CameraGroundStomp);
    eventHandler.RegisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.RegisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.RegisterMessage<float>("HeadImpact", OnMessage_HeadImpact);
    eventHandler.RegisterMessage("Stop", OnMessage_Stop);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.RegisterActivity("Zoom", OnStart_Zoom, null, null, null, null, null);
    eventHandler.RegisterValue("IsLocal", Get_IsLocal, null);
    eventHandler.RegisterValue("Rotation", Get_Rotation, Set_Rotation);
    eventHandler.RegisterValue("LookPoint", Get_LookPoint, null);
    eventHandler.RegisterValue("CameraLookDirection", Get_CameraLookDirection, null);
  }

  public override void Unregister(vp_EventHandler eventHandler)
  {
    base.Unregister(eventHandler);
    eventHandler.UnregisterActivity("Run", null, null, CanStart_Run, null, null, null);
    eventHandler.UnregisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.UnregisterMessage<float>("CameraGroundStomp", OnMessage_CameraGroundStomp);
    eventHandler.UnregisterMessage("CameraToggle3rdPerson", OnMessage_CameraToggle3rdPerson);
    eventHandler.UnregisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.UnregisterMessage<float>("HeadImpact", OnMessage_HeadImpact);
    eventHandler.UnregisterMessage("Stop", OnMessage_Stop);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
    eventHandler.UnregisterActivity("Zoom", OnStart_Zoom, null, null, null, null, null);
    eventHandler.UnregisterValue("IsLocal", Get_IsLocal, null);
    eventHandler.UnregisterValue("Rotation", Get_Rotation, Set_Rotation);
    eventHandler.UnregisterValue("LookPoint", Get_LookPoint, null);
    eventHandler.UnregisterValue("CameraLookDirection", Get_CameraLookDirection, null);
  }

  protected override StateManager GetStateManager() => new FPCameraStateManager(this);

  public delegate void BobStepDelegate();
}
