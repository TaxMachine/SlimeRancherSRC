// Decompiled with JetBrains decompiler
// Type: vp_RagdollHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class vp_RagdollHandler : MonoBehaviour, EventHandlerRegistrable
{
  public float CameraFreezeDelay = 2.5f;
  public float VelocityMultiplier = 30f;
  public GameObject HeadBone;
  protected float m_TimeOfDeath;
  protected vp_Timer.Handle PostponeTimer = new vp_Timer.Handle();
  protected Vector3 m_HeadRotationCorrection = Vector3.zero;
  protected Vector3 m_CameraFreezeAngle = Vector3.zero;
  protected List<Collider> m_Colliders;
  protected List<Rigidbody> m_Rigidbodies;
  protected List<Transform> m_Transforms;
  protected Animator m_Animator;
  protected vp_BodyAnimator m_BodyAnimator;
  protected vp_PlayerEventHandler m_Player;
  protected vp_FPCamera m_FPCamera;
  protected vp_Controller m_Controller;
  protected Dictionary<Transform, Quaternion> TransformRotations = new Dictionary<Transform, Quaternion>();
  protected Dictionary<Transform, Vector3> TransformPositions = new Dictionary<Transform, Vector3>();
  protected Quaternion m_Rot;
  protected Vector3 m_Pos;
  private bool m_TriedToFetchPlayer;

  protected vp_PlayerEventHandler Player
  {
    get
    {
      if (m_Player == null && !m_TriedToFetchPlayer)
      {
        m_Player = transform.root.GetComponentInChildren<vp_PlayerEventHandler>();
        m_TriedToFetchPlayer = true;
      }
      return m_Player;
    }
  }

  public vp_FPCamera FPCamera
  {
    get
    {
      if (m_FPCamera == null)
        m_FPCamera = transform.root.GetComponentInChildren<vp_FPCamera>();
      return m_FPCamera;
    }
  }

  protected vp_Controller Controller
  {
    get
    {
      if (m_Controller == null)
        m_Controller = transform.root.GetComponentInChildren<vp_Controller>();
      return m_Controller;
    }
  }

  protected List<Collider> Colliders
  {
    get
    {
      if (m_Colliders == null)
      {
        m_Colliders = new List<Collider>();
        foreach (Collider componentsInChild in GetComponentsInChildren<Collider>())
          m_Colliders.Add(componentsInChild);
      }
      return m_Colliders;
    }
  }

  protected List<Rigidbody> Rigidbodies
  {
    get
    {
      if (m_Rigidbodies == null)
        m_Rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
      return m_Rigidbodies;
    }
  }

  protected List<Transform> Transforms
  {
    get
    {
      if (m_Transforms == null)
      {
        m_Transforms = new List<Transform>();
        foreach (Component rigidbody in Rigidbodies)
          m_Transforms.Add(rigidbody.transform);
      }
      return m_Transforms;
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

  protected vp_BodyAnimator BodyAnimator
  {
    get
    {
      if (m_BodyAnimator == null)
        m_BodyAnimator = GetComponent<vp_BodyAnimator>();
      return m_BodyAnimator;
    }
  }

  protected virtual void Awake()
  {
    if (Colliders != null && Colliders.Count != 0 && Rigidbodies != null && Rigidbodies.Count != 0 && Transforms != null && Transforms.Count != 0 && !(Animator == null) && !(BodyAnimator == null))
      return;
    Debug.LogError("Error (" + this + ") Could not be initialized. Please make sure hierarchy has ragdoll colliders, Animator and vp_BodyAnimator.");
    enabled = false;
  }

  protected virtual void Start() => SetRagdoll(false);

  protected virtual void SaveStartPose()
  {
    foreach (Transform transform in Transforms)
    {
      if (!TransformRotations.ContainsKey(transform))
        TransformRotations.Add(transform.transform, transform.localRotation);
      if (!TransformPositions.ContainsKey(transform))
        TransformPositions.Add(transform.transform, transform.localPosition);
    }
  }

  protected virtual void RestoreStartPose()
  {
    foreach (Transform transform in Transforms)
    {
      if (TransformRotations.TryGetValue(transform, out m_Rot))
        transform.localRotation = m_Rot;
      if (TransformPositions.TryGetValue(transform, out m_Pos))
        transform.localPosition = m_Pos;
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

  private void Update()
  {
  }

  private void LateUpdate() => UpdateDeathCamera();

  protected virtual void UpdateDeathCamera()
  {
    if (Player == null || !Player.Dead.Active || HeadBone == null || !Player.IsFirstPerson.Get())
      return;
    FPCamera.Transform.position = HeadBone.transform.position;
    m_HeadRotationCorrection = HeadBone.transform.localEulerAngles;
    if (Time.time - (double) m_TimeOfDeath < CameraFreezeDelay)
      FPCamera.Transform.localEulerAngles = m_CameraFreezeAngle = new Vector3(-m_HeadRotationCorrection.z, -m_HeadRotationCorrection.x, m_HeadRotationCorrection.y);
    else
      FPCamera.Transform.localEulerAngles = m_CameraFreezeAngle;
  }

  public virtual void SetRagdoll(bool enabled = true)
  {
    if (enabled)
    {
      if (!Player.Dead.Active)
        return;
      PostponeTimer.Cancel();
      if (!Animator.GetBool("IsGrounded"))
      {
        vp_Timer.In(0.1f, () => SetRagdoll(), PostponeTimer);
        return;
      }
    }
    if (Animator != null)
      Animator.enabled = !enabled;
    if (BodyAnimator != null)
      BodyAnimator.enabled = !enabled;
    if (Controller != null)
      Controller.EnableCollider(!enabled);
    foreach (Rigidbody rigidbody in Rigidbodies)
    {
      rigidbody.isKinematic = !enabled;
      if (enabled)
        rigidbody.AddForce(Player.Velocity.Get() * VelocityMultiplier);
    }
    foreach (Collider collider in Colliders)
      collider.enabled = enabled;
    if (enabled)
      return;
    RestoreStartPose();
  }

  protected virtual void OnStart_Dead()
  {
    m_TimeOfDeath = Time.time;
    vp_Timer.In(0.0f, () => SetRagdoll());
  }

  protected virtual void OnStop_Dead()
  {
    SetRagdoll(false);
    Player.OutOfControl.Stop();
  }

  public void Register(vp_EventHandler eventHandler) => eventHandler.RegisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);

  public void Unregister(vp_EventHandler eventHandler) => eventHandler.UnregisterActivity("Dead", OnStart_Dead, OnStop_Dead, null, null, null, null);
}
