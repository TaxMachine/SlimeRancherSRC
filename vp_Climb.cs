// Decompiled with JetBrains decompiler
// Type: vp_Climb
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vp_Climb : vp_Interactable, EventHandlerRegistrable
{
  public float MinimumClimbSpeed = 3f;
  public float ClimbSpeed = 16f;
  public float MountSpeed = 5f;
  public float DistanceToClimbable = 1f;
  public float MinVelocityToClimb = 7f;
  public float ClimbAgainTimeout = 1f;
  public bool MountAutoRotatePitch;
  public bool SimpleClimb = true;
  public float DismountForce = 0.2f;
  public vp_ClimbingSounds Sounds;
  protected int m_LastWeaponEquipped;
  protected bool m_IsClimbing;
  protected float m_CanClimbAgain;
  protected Vector3 m_CachedDirection = Vector3.zero;
  protected Vector2 m_CachedRotation = Vector2.zero;
  protected vp_Timer.Handle m_ClimbingSoundTimer = new vp_Timer.Handle();
  protected AudioClip m_SoundToPlay;
  protected AudioClip m_LastPlayedSound;
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
    m_CanClimbAgain = Time.time;
  }

  public override bool TryInteract(vp_PlayerEventHandler player)
  {
    if (!enabled || !(player is vp_FPPlayerEventHandler) || Time.time < (double) m_CanClimbAgain)
      return false;
    if (m_IsClimbing)
    {
      m_Player.Climb.TryStop();
      return false;
    }
    if (m_Player == null)
      m_Player = player;
    if (m_Player.Interactable.Get() != null)
      return false;
    if (m_Controller == null)
      m_Controller = m_Player.GetComponent<vp_FPController>();
    if (m_Player.Velocity.Get().magnitude > (double) MinVelocityToClimb)
      return false;
    if (m_Camera == null)
      m_Camera = m_Player.GetComponentInChildren<vp_FPCamera>();
    if (Sounds.AudioSource == null)
      Sounds.AudioSource = m_Player.GetComponent<AudioSource>();
    if (m_Player != null)
      Register(m_Player);
    m_Player.Interactable.Set(this);
    return m_Player.Climb.TryStart();
  }

  protected virtual void OnStart_Climb()
  {
    m_Controller.PhysicsGravityModifier = 0.0f;
    m_Camera.SetRotation(m_Camera.Transform.eulerAngles, false);
    m_Player.Jump.Stop();
    FPPlayer.InputAllowGameplay.Set(false);
    m_Player.Stop.Send();
    m_LastWeaponEquipped = m_Player.CurrentWeaponIndex.Get();
    m_Player.SetWeapon.TryStart(0);
    m_Player.Interactable.Set(null);
    PlaySound(Sounds.MountSounds);
    if (m_Controller.Transform.GetComponent<Collider>().enabled && m_Transform.GetComponent<Collider>().enabled)
    {
      Log.Error("UFPS Ignoring Collider 1");
      Physics.IgnoreCollision(m_Controller.Transform.GetComponent<Collider>(), m_Transform.GetComponent<Collider>(), true);
    }
    StartCoroutine("LineUp");
  }

  protected virtual void PlaySound(List<AudioClip> sounds)
  {
    if (Sounds.AudioSource == null || sounds == null || sounds.Count == 0)
      return;
    do
    {
      m_SoundToPlay = sounds[UnityEngine.Random.Range(0, sounds.Count)];
      if (m_SoundToPlay == null)
        return;
    }
    while (m_SoundToPlay == m_LastPlayedSound && sounds.Count > 1);
    Sounds.AudioSource.pitch = sounds != Sounds.ClimbingSounds ? 1f : UnityEngine.Random.Range(Sounds.ClimbingPitch.x, Sounds.ClimbingPitch.y) * Time.timeScale;
    Sounds.AudioSource.PlayOneShot(m_SoundToPlay);
    m_LastPlayedSound = m_SoundToPlay;
  }

  protected virtual IEnumerator LineUp()
  {
    vp_Climb vpClimb = this;
    Vector3 startPosition = vpClimb.m_Player.Position.Get();
    Vector3 endPosition = vpClimb.GetNewPosition();
    Quaternion startingRotation = vpClimb.m_Camera.transform.rotation;
    Quaternion endRotation = Quaternion.LookRotation(-vpClimb.m_Transform.forward);
    bool flag = vpClimb.m_Controller.Transform.position.y > (double) vpClimb.m_Transform.GetComponent<Collider>().bounds.center.y;
    if (flag)
      endPosition += Vector3.down * vpClimb.m_Controller.CharacterController.height;
    else
      endPosition += vpClimb.m_Controller.Transform.up * (vpClimb.m_Controller.CharacterController.height / 2f);
    endRotation = !flag || vpClimb.m_Transform.InverseTransformDirection(-vpClimb.FPPlayer.CameraLookDirection.Get()).z <= 0.0 ? Quaternion.Euler(new Vector3(-45f, endRotation.eulerAngles.y, endRotation.eulerAngles.z)) : Quaternion.Euler(new Vector3(45f, endRotation.eulerAngles.y, endRotation.eulerAngles.z));
    endPosition = new Vector3(vpClimb.m_Transform.GetComponent<Collider>().bounds.center.x, endPosition.y, vpClimb.m_Transform.GetComponent<Collider>().bounds.center.z);
    endPosition += vpClimb.m_Transform.forward;
    float t = 0.0f;
    float duration = Vector3.Distance(vpClimb.m_Controller.Transform.position, endPosition) / (!flag ? vpClimb.MountSpeed / 1.25f : vpClimb.MountSpeed);
    while (t < 1.0)
    {
      t += Time.deltaTime / duration;
      Vector3 o = Vector3.Lerp(startPosition, endPosition, t);
      vpClimb.m_Player.Position.Set(o);
      Quaternion quaternion = Quaternion.Slerp(startingRotation, endRotation, t);
      vpClimb.m_Player.Rotation.Set(new Vector2(vpClimb.MountAutoRotatePitch ? quaternion.eulerAngles.x : vpClimb.m_Player.Rotation.Get().x, quaternion.eulerAngles.y));
      yield return new WaitForEndOfFrame();
    }
    vpClimb.m_CachedDirection = vpClimb.m_Camera.Transform.forward;
    vpClimb.m_CachedRotation = vpClimb.m_Player.Rotation.Get();
    vpClimb.m_IsClimbing = true;
  }

  protected virtual void OnStop_Climb()
  {
    m_Player.Interactable.Set(null);
    FPPlayer.InputAllowGameplay.Set(true);
    m_Player.SetWeapon.TryStart(m_LastWeaponEquipped);
    Unregister(m_Player);
    m_CanClimbAgain = Time.time + ClimbAgainTimeout;
    if (m_Controller.Transform.GetComponent<Collider>().enabled && m_Transform.GetComponent<Collider>().enabled)
    {
      Log.Error("UFPS Ignoring Collider 2");
      Physics.IgnoreCollision(m_Controller.Transform.GetComponent<Collider>(), m_Transform.GetComponent<Collider>(), false);
    }
    PlaySound(Sounds.DismountSounds);
    Vector3 vector3 = m_Controller.Transform.forward * DismountForce;
    Vector3 force;
    if (m_Transform.GetComponent<Collider>().bounds.center.y < (double) m_Player.Position.Get().y)
      force = (vector3 * 2f) with
      {
        y = DismountForce * 0.5f
      };
    else
      force = -vector3 * 0.5f;
    m_Player.Stop.Send();
    m_Controller.AddForce(force);
    m_IsClimbing = false;
    m_Player.SetState("Default");
    StartCoroutine("RestorePitch");
  }

  protected virtual IEnumerator RestorePitch()
  {
    vp_Climb vpClimb = this;
    float t = 0.0f;
    while (t < 1.0 && vpClimb.FPPlayer.InputRawLook.Get().y == 0.0)
    {
      t += Time.deltaTime;
      vpClimb.m_Player.Rotation.Set(Vector2.Lerp(vpClimb.m_Player.Rotation.Get(), new Vector2(0.0f, vpClimb.m_Player.Rotation.Get().y), t));
      yield return new WaitForEndOfFrame();
    }
  }

  protected virtual bool CanStart_Interact()
  {
    if (m_IsClimbing)
      m_Player.Climb.TryStop();
    return true;
  }

  protected virtual void FixedUpdate() => Climbing();

  protected virtual void Update() => InputJump();

  protected virtual void OnStart_Dead() => FinishInteraction();

  public override void FinishInteraction()
  {
    if (!m_IsClimbing)
      return;
    m_Player.Climb.TryStop();
  }

  protected virtual void Climbing()
  {
    if (m_Player == null || !m_IsClimbing)
      return;
    m_Controller.PhysicsGravityModifier = 0.0f;
    m_Camera.RotationYawLimit = new Vector2(m_CachedRotation.y - 90f, m_CachedRotation.y + 90f);
    m_Camera.RotationPitchLimit = new Vector2(90f, -90f);
    Vector3 newPosition = GetNewPosition();
    Vector3 vector3 = Vector3.zero;
    float f = m_Player.Rotation.Get().x / 90f;
    float num1 = MinimumClimbSpeed / ClimbSpeed;
    if (Mathf.Abs(f) < (double) num1)
      f = f > 0.0 ? num1 : num1 * -1f;
    if (f < 0.0)
      vector3 = Vector3.up * -f;
    else if (f > 0.0)
      vector3 = Vector3.down * f;
    float climbSpeed = ClimbSpeed;
    float num2 = (vector3 * m_Player.InputClimbVector.Get()).y;
    if (SimpleClimb)
    {
      vector3 = Vector3.up;
      climbSpeed *= 0.75f;
      num2 = m_Player.InputClimbVector.Get();
    }
    if (num2 > 0.0 && newPosition.y > GetTopOfCollider(m_Transform) - m_Controller.CharacterController.height * 0.25 || num2 < 0.0 && m_Controller.Grounded && m_Controller.GroundTransform.GetInstanceID() != m_Transform.GetInstanceID())
    {
      m_Player.Climb.TryStop();
    }
    else
    {
      if (m_Player.InputClimbVector.Get() == 0.0)
        m_ClimbingSoundTimer.Cancel();
      if (m_Player.InputClimbVector.Get() != 0.0 && !m_ClimbingSoundTimer.Active && Sounds.ClimbingSounds.Count > 0)
      {
        float num3 = Mathf.Abs((float) (5.0 / vector3.y * (Time.deltaTime * 5.0)) / Sounds.ClimbingSoundSpeed);
        vp_Timer.In(SimpleClimb ? num3 * 3f : num3, () => PlaySound(Sounds.ClimbingSounds), m_ClimbingSoundTimer);
      }
      m_Player.Position.Set(Vector3.Slerp(m_Controller.Transform.position, newPosition + vector3 * climbSpeed * Time.deltaTime * m_Player.InputClimbVector.Get(), Time.deltaTime * climbSpeed));
    }
  }

  protected virtual Vector3 GetNewPosition()
  {
    Vector3 newPosition = m_Controller.Transform.position;
    RaycastHit hitInfo;
    Physics.Raycast(new Ray(m_Controller.Transform.position, m_CachedDirection), out hitInfo, DistanceToClimbable * 4f);
    if (hitInfo.collider != null && hitInfo.transform.GetInstanceID() == m_Transform.GetInstanceID() && (hitInfo.distance > (double) DistanceToClimbable || hitInfo.distance < (double) DistanceToClimbable))
      newPosition = (newPosition - hitInfo.point).normalized * DistanceToClimbable + hitInfo.point;
    return newPosition;
  }

  protected virtual void InputJump()
  {
    if (!m_IsClimbing || m_Player == null || !FPPlayer.InputGetButton.Send("Jump") && !FPPlayer.InputGetButtonDown.Send("Interact"))
      return;
    m_Player.Climb.TryStop();
    if (!FPPlayer.InputGetButton.Send("Jump"))
      return;
    m_Controller.AddForce(-m_Controller.Transform.forward * m_Controller.MotorJumpForce);
  }

  public static float GetTopOfCollider(Transform t) => t.position.y + t.GetComponent<Collider>().bounds.size.y / 2f;

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("Interact", null, null, CanStart_Interact, null, null, null);
    eventHandler.RegisterActivity("Climb", OnStart_Climb, OnStop_Climb, null, null, null, null);
    eventHandler.RegisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("Interact", null, null, CanStart_Interact, null, null, null);
    eventHandler.UnregisterActivity("Climb", OnStart_Climb, OnStop_Climb, null, null, null, null);
    eventHandler.UnregisterActivity("Dead", OnStart_Dead, null, null, null, null, null);
  }

  [Serializable]
  public class vp_ClimbingSounds
  {
    public AudioSource AudioSource;
    public List<AudioClip> MountSounds = new List<AudioClip>();
    public List<AudioClip> DismountSounds = new List<AudioClip>();
    public float ClimbingSoundSpeed = 4f;
    public Vector2 ClimbingPitch = new Vector2(1f, 1.5f);
    public List<AudioClip> ClimbingSounds = new List<AudioClip>();
  }
}
