// Decompiled with JetBrains decompiler
// Type: vp_Grab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class vp_Grab : vp_Interactable, EventHandlerRegistrable
{
  public string OnGrabText = "";
  public Texture GrabStateCrosshair;
  public float FootstepForce = 0.015f;
  public float Kneeling = 5f;
  public float Stiffness = 0.5f;
  public float ShakeSpeed = 0.1f;
  public Vector3 ShakeAmplitude = Vector3.one;
  public float ThrowStrength = 6f;
  public bool AllowThrowRotation = true;
  public float Burden;
  public int MaxCollisionCount = 20;
  protected Vector3 m_CurrentShake = Vector3.zero;
  protected Vector3 m_CurrentRotationSway = Vector3.zero;
  protected Vector2 m_CurrentMouseMove;
  protected Vector3 m_CurrentSwayForce;
  protected Vector3 m_CurrentSwayTorque;
  protected float m_CurrentFootstepForce;
  protected Vector3 m_CurrentHoldAngle = Vector3.one;
  protected Collider m_LastExternalCollider;
  protected int m_CollisionCount;
  public Vector3 CarryingOffset = new Vector3(0.0f, -0.5f, 1.5f);
  public float CameraPitchDownLimit;
  protected Vector3 m_TempCarryingOffset = Vector3.zero;
  protected bool m_IsFetching;
  protected float duration;
  protected float m_FetchProgress;
  protected vp_FPInteractManager m_InteractManager;
  protected AudioSource m_Audio;
  protected int m_LastWeaponEquipped;
  protected bool m_IsGrabbed;
  protected float m_OriginalPitchDownLimit;
  protected bool m_DefaultGravity;
  protected float m_DefaultDrag;
  protected float m_DefaultAngularDrag;
  public Vector2 SoundsPitch = new Vector2(1f, 1.5f);
  public List<AudioClip> GrabSounds = new List<AudioClip>();
  public List<AudioClip> DropSounds = new List<AudioClip>();
  public List<AudioClip> ThrowSounds = new List<AudioClip>();
  protected vp_Timer.Handle m_DisableAngleSwayTimer = new vp_Timer.Handle();
  private vp_FPPlayerEventHandler m_FPPlayer;

  private vp_FPPlayerEventHandler FPPlayer
  {
    get
    {
      if (m_FPPlayer == null)
        m_FPPlayer = m_Player as vp_FPPlayerEventHandler;
      return m_FPPlayer;
    }
  }

  protected override void Start()
  {
    base.Start();
    if (!(bool) (Object) GetComponent<Rigidbody>() || !(bool) (Object) GetComponent<Collider>())
      enabled = false;
    if (GetComponent<Rigidbody>() != null)
    {
      m_DefaultGravity = GetComponent<Rigidbody>().useGravity;
      m_DefaultDrag = m_Transform.GetComponent<Rigidbody>().drag;
      m_DefaultAngularDrag = m_Transform.GetComponent<Rigidbody>().angularDrag;
    }
    InteractType = vp_InteractType.Normal;
    m_InteractManager = FindObjectOfType(typeof (vp_FPInteractManager)) as vp_FPInteractManager;
  }

  protected virtual void FixedUpdate()
  {
    if (!m_IsGrabbed || m_Transform.parent == null)
      return;
    UpdateShake();
    UpdatePosition();
    UpdateRotation();
    UpdateBurden();
    DampenForces();
  }

  protected virtual void Update()
  {
    if (!m_IsGrabbed || m_Transform.parent == null)
      return;
    UpdateInput();
  }

  protected virtual void UpdateInput()
  {
    m_CurrentMouseMove.x = FPPlayer.InputRawLook.Get().x * Time.timeScale;
    m_CurrentMouseMove.y = FPPlayer.InputRawLook.Get().y * Time.timeScale;
    if (FPPlayer.InputGetButtonDown.Send("Attack"))
    {
      FPPlayer.Interact.TryStart();
    }
    else
    {
      if (m_Player.CurrentWeaponIndex.Get() == 0)
        return;
      m_Player.SetWeapon.TryStart(0);
    }
  }

  protected virtual void UpdateShake()
  {
    m_CurrentShake = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(ShakeSpeed), ShakeAmplitude);
    m_Transform.localEulerAngles += m_CurrentShake;
  }

  protected virtual void UpdatePosition()
  {
    m_CurrentSwayForce += m_Player.Velocity.Get() * 0.005f;
    m_CurrentSwayForce.y += m_CurrentFootstepForce;
    m_CurrentSwayForce += m_Camera.Transform.TransformDirection(new Vector3(m_CurrentMouseMove.x * 0.05f, m_Player.Rotation.Get().x > (double) m_Camera.RotationPitchLimit.y ? m_CurrentMouseMove.y * 0.015f : m_CurrentMouseMove.y * 0.05f, 0.0f));
    m_TempCarryingOffset = m_Player.IsFirstPerson.Get() ? CarryingOffset : CarryingOffset - m_Camera.Position3rdPersonOffset;
    m_Transform.position = Vector3.Lerp(m_Transform.position, m_Camera.Transform.position - m_CurrentSwayForce + m_Camera.Transform.right * m_TempCarryingOffset.x + m_Camera.Transform.up * m_Transform.localScale.y * (m_TempCarryingOffset.y + m_CurrentShake.y * 0.5f) + m_Camera.Transform.forward * m_TempCarryingOffset.z, m_FetchProgress < 1.0 ? m_FetchProgress : Time.deltaTime * (Stiffness * 60f));
  }

  protected virtual void UpdateRotation()
  {
    m_Camera.RotationPitchLimit = Vector2.Lerp(m_Camera.RotationPitchLimit, new Vector2(m_Camera.RotationPitchLimit.x, CameraPitchDownLimit), m_FetchProgress);
    if (m_DisableAngleSwayTimer.Active)
      return;
    m_CurrentSwayTorque += m_Player.Velocity.Get() * 0.005f;
    m_CurrentRotationSway = m_Camera.Transform.InverseTransformDirection(m_CurrentSwayTorque * 1.5f);
    m_CurrentRotationSway.y = m_CurrentRotationSway.z;
    m_CurrentRotationSway.z = m_CurrentRotationSway.x;
    m_CurrentRotationSway.x = (float) (-(double) m_CurrentRotationSway.y * 0.5);
    m_CurrentRotationSway.y = 0.0f;
    Quaternion localRotation1 = m_Transform.localRotation;
    m_Transform.Rotate(m_Camera.transform.forward, m_CurrentRotationSway.z * -0.5f * Time.timeScale);
    m_Transform.Rotate(m_Camera.transform.right, m_CurrentRotationSway.x * -0.5f * Time.timeScale);
    Quaternion localRotation2 = m_Transform.localRotation;
    m_Transform.localRotation = localRotation1;
    m_Transform.localRotation = Quaternion.Slerp(localRotation2, Quaternion.Euler(m_CurrentHoldAngle + m_CurrentShake * 50f), Time.deltaTime * (Stiffness * 60f));
  }

  protected virtual void UpdateBurden()
  {
    if (Burden <= 0.0)
      return;
    m_Player.MotorThrottle.Set(m_Player.MotorThrottle.Get() * (1f - Mathf.Clamp01(Burden)));
  }

  protected virtual void DampenForces()
  {
    m_CurrentSwayForce *= 0.9f;
    m_CurrentSwayTorque *= 0.9f;
    m_CurrentFootstepForce *= 0.9f;
  }

  public override bool TryInteract(vp_PlayerEventHandler player)
  {
    if (!(player is vp_FPPlayerEventHandler))
      return false;
    if (m_Player == null)
      m_Player = player;
    if (player == null)
      return false;
    if (m_Controller == null)
      m_Controller = m_Player.GetComponent<vp_FPController>();
    if (m_Controller == null)
      return false;
    if (m_Camera == null)
      m_Camera = m_Player.GetComponentInChildren<vp_FPCamera>();
    if (m_Camera == null)
      return false;
    if (m_WeaponHandler == null)
      m_WeaponHandler = m_Player.GetComponentInChildren<vp_WeaponHandler>();
    if (m_Audio == null)
      m_Audio = m_Player.GetComponent<AudioSource>();
    Register(m_Player);
    if (!m_IsGrabbed)
      StartGrab();
    else
      StopGrab();
    m_Player.Interactable.Set(this);
    if (GrabStateCrosshair != null)
      FPPlayer.Crosshair.Set(GrabStateCrosshair);
    else
      FPPlayer.Crosshair.Set(new Texture2D(0, 0));
    return true;
  }

  protected virtual void StartGrab()
  {
    vp_AudioUtility.PlayRandomSound(m_Audio, GrabSounds, SoundsPitch);
    if (!string.IsNullOrEmpty(OnGrabText))
      FPPlayer.HUDText.Send(OnGrabText);
    m_Camera.BobStepCallback += Footstep;
    m_LastWeaponEquipped = m_Player.CurrentWeaponIndex.Get();
    m_OriginalPitchDownLimit = m_Camera.RotationPitchLimit.y;
    m_FetchProgress = 0.0f;
    if (m_LastWeaponEquipped != 0)
      m_Player.SetWeapon.TryStart(0);
    else if (!m_IsFetching)
      StartCoroutine("Fetch");
    if (m_Transform.GetComponent<Rigidbody>() != null)
    {
      m_Transform.GetComponent<Rigidbody>().useGravity = false;
      m_Transform.GetComponent<Rigidbody>().drag = Stiffness * 60f;
      m_Transform.GetComponent<Rigidbody>().angularDrag = Stiffness * 60f;
    }
    if (m_Controller.Transform.GetComponent<Collider>().enabled && m_Transform.GetComponent<Collider>().enabled)
    {
      Log.Error("UFPS Ignoring Collider 3");
      Physics.IgnoreCollision(m_Controller.Transform.GetComponent<Collider>(), m_Transform.GetComponent<Collider>(), true);
    }
    m_Transform.parent = m_Camera.Transform;
    m_CurrentHoldAngle = m_Transform.localEulerAngles;
    m_IsGrabbed = true;
  }

  protected virtual void StopGrab()
  {
    m_IsGrabbed = false;
    m_FetchProgress = 1f;
    m_Camera.BobStepCallback -= Footstep;
    m_Player.SetWeapon.TryStart(m_LastWeaponEquipped);
    if (m_Transform.GetComponent<Rigidbody>() != null)
    {
      m_Transform.GetComponent<Rigidbody>().useGravity = m_DefaultGravity;
      m_Transform.GetComponent<Rigidbody>().drag = m_DefaultDrag;
      m_Transform.GetComponent<Rigidbody>().angularDrag = m_DefaultAngularDrag;
    }
    if (!m_Player.Dead.Active && vp_Utility.IsActive(m_Transform.gameObject) && m_Controller.Transform.GetComponent<Collider>().enabled && m_Transform.GetComponent<Collider>().enabled)
    {
      Log.Error("UFPS Ignoring Collider 4");
      Physics.IgnoreCollision(m_Controller.Transform.GetComponent<Collider>(), m_Transform.GetComponent<Collider>(), false);
    }
    Vector3 eulerAngles = m_Transform.eulerAngles;
    m_Transform.parent = null;
    m_Transform.eulerAngles = eulerAngles;
    if (m_Transform.GetComponent<Rigidbody>() != null)
    {
      m_Transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
      m_Transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    if (FPPlayer.InputGetButtonDown.Send("Attack"))
    {
      vp_AudioUtility.PlayRandomSound(m_Audio, ThrowSounds, SoundsPitch);
      if (m_Transform.GetComponent<Rigidbody>() != null)
      {
        m_Transform.GetComponent<Rigidbody>().AddForce(m_Player.Velocity.Get() + FPPlayer.CameraLookDirection.Get() * ThrowStrength, ForceMode.Impulse);
        if (AllowThrowRotation)
          m_Transform.GetComponent<Rigidbody>().AddTorque(m_Camera.Transform.forward * (Random.value > 0.5 ? 0.5f : -0.5f) + m_Camera.Transform.right * (Random.value > 0.5 ? 0.5f : -0.5f), ForceMode.Impulse);
      }
    }
    else
    {
      vp_AudioUtility.PlayRandomSound(m_Audio, DropSounds, SoundsPitch);
      if (m_Transform.GetComponent<Rigidbody>() != null)
        m_Transform.GetComponent<Rigidbody>().AddForce(m_Player.Velocity.Get() + FPPlayer.CameraLookDirection.Get(), ForceMode.Impulse);
    }
    if (m_InteractManager == null)
      m_InteractManager = FindObjectOfType(typeof (vp_FPInteractManager)) as vp_FPInteractManager;
    m_InteractManager.CrosshairTimeoutTimer = Time.time + 0.5f;
    vp_Timer.In(0.1f, () => m_Camera.RotationPitchLimit.y = m_OriginalPitchDownLimit);
  }

  public override void FinishInteraction()
  {
    if (!m_IsGrabbed)
      return;
    StopGrab();
  }

  protected virtual IEnumerator Fetch()
  {
    vp_Grab vpGrab = this;
    vpGrab.m_IsFetching = true;
    vpGrab.m_CurrentSwayForce = Vector3.zero;
    vpGrab.m_CurrentSwayTorque = Vector3.zero;
    vpGrab.m_CurrentFootstepForce = 0.0f;
    vpGrab.m_FetchProgress = 0.0f;
    vpGrab.duration = Vector3.Distance(vpGrab.m_Camera.Transform.position, vpGrab.m_Transform.position) * 0.5f;
    vp_Timer.In(vpGrab.duration + 1f, () => { }, vpGrab.m_DisableAngleSwayTimer);
    while (vpGrab.m_FetchProgress < 1.0)
    {
      vpGrab.m_FetchProgress += Time.deltaTime / vpGrab.duration;
      yield return new WaitForEndOfFrame();
    }
    vpGrab.m_IsFetching = false;
  }

  protected virtual void OnCollisionEnter(Collision col)
  {
    if (!m_IsGrabbed)
      return;
    if (m_FetchProgress < 1.0)
      m_FetchProgress *= 1.2f;
    vp_Timer.In(2f, () => { }, m_DisableAngleSwayTimer);
  }

  protected virtual void OnCollisionStay(Collision col)
  {
    if (!m_IsGrabbed || MaxCollisionCount == 0)
      return;
    if (col.collider != m_LastExternalCollider)
    {
      if ((bool) (Object) col.collider.GetComponent<Rigidbody>() && !col.collider.GetComponent<Rigidbody>().isKinematic)
        return;
      m_LastExternalCollider = col.collider;
      m_CollisionCount = 1;
    }
    else
    {
      RaycastHit hitInfo;
      if (Physics.Raycast(m_Transform.position, m_Camera.Transform.forward, out hitInfo, 1f) && hitInfo.collider == m_LastExternalCollider && m_FetchProgress >= 1.0)
        m_CollisionCount = MaxCollisionCount;
      ++m_CollisionCount;
      if (m_CollisionCount <= MaxCollisionCount || Physics.Raycast(col.contacts[0].point + Vector3.up * 0.1f, -Vector3.up, out hitInfo, 0.2f) && hitInfo.collider == m_LastExternalCollider)
        return;
      m_CollisionCount = 0;
      m_FetchProgress = 1f;
      m_LastExternalCollider = null;
      if (!(m_Player != null))
        return;
      m_Player.Interact.TryStart();
    }
  }

  protected virtual void OnCollisionExit() => m_CurrentHoldAngle = m_Transform.localEulerAngles;

  protected virtual void Footstep() => m_CurrentFootstepForce += FootstepForce;

  protected virtual void OnMessage_FallImpact(float impact) => m_CurrentSwayForce.y += impact * Kneeling;

  protected virtual void OnStop_SetWeapon()
  {
    if (!m_IsGrabbed || m_IsFetching)
      return;
    StartCoroutine("Fetch");
  }

  protected virtual bool CanStart_SetWeapon()
  {
    int num = (int) m_Player.SetWeapon.Argument;
    return !m_IsGrabbed || num == 0;
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("SetWeapon", null, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.RegisterMessage<float>("FallImpact", OnMessage_FallImpact);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("SetWeapon", null, OnStop_SetWeapon, CanStart_SetWeapon, null, null, null);
    eventHandler.UnregisterMessage<float>("FallImpact", OnMessage_FallImpact);
  }
}
