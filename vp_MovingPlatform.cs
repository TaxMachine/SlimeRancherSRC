// Decompiled with JetBrains decompiler
// Type: vp_MovingPlatform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (AudioSource))]
[RequireComponent(typeof (Rigidbody))]
public class vp_MovingPlatform : MonoBehaviour
{
  protected Transform m_Transform;
  public PathMoveType PathType;
  public GameObject PathWaypoints;
  public Direction PathDirection;
  public int MoveAutoStartTarget = 1000;
  protected List<Transform> m_Waypoints = new List<Transform>();
  protected int m_NextWaypoint;
  protected Vector3 m_CurrentTargetPosition = Vector3.zero;
  protected Vector3 m_CurrentTargetAngle = Vector3.zero;
  protected int m_TargetedWayPoint;
  protected float m_TravelDistance;
  protected Vector3 m_OriginalAngle = Vector3.zero;
  protected int m_CurrentWaypoint;
  public float MoveSpeed = 0.1f;
  public float MoveReturnDelay;
  public float MoveCooldown;
  public MovementInterpolationMode MoveInterpolationMode;
  protected bool m_Moving;
  protected float m_NextAllowedMoveTime;
  protected float m_MoveTime;
  protected vp_Timer.Handle m_ReturnDelayTimer = new vp_Timer.Handle();
  protected Vector3 m_PrevPos = Vector3.zero;
  protected AnimationCurve m_EaseInOutCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
  protected AnimationCurve m_LinearCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
  public float RotationEaseAmount = 0.1f;
  public Vector3 RotationSpeed = Vector3.zero;
  public RotateInterpolationMode RotationInterpolationMode;
  protected Vector3 m_PrevAngle = Vector3.zero;
  public AudioClip SoundStart;
  public AudioClip SoundStop;
  public AudioClip SoundMove;
  public AudioClip SoundWaypoint;
  protected AudioSource m_Audio;
  public bool PhysicsSnapPlayerToTopOnIntersect = true;
  public float m_PhysicsPushForce = 2f;
  protected Rigidbody m_RigidBody;
  protected Collider m_Collider;
  protected Collider m_PlayerCollider;
  protected vp_PlayerEventHandler m_PlayerToPush;
  protected float m_PhysicsCurrentMoveVelocity;
  protected float m_PhysicsCurrentRotationVelocity;
  protected static Dictionary<Collider, vp_PlayerEventHandler> m_KnownPlayers = new Dictionary<Collider, vp_PlayerEventHandler>();

  public int TargetedWaypoint => m_TargetedWayPoint;

  private void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => m_KnownPlayers.Clear();

  private void Start()
  {
    m_Transform = this.transform;
    m_Collider = GetComponentInChildren<Collider>();
    m_RigidBody = GetComponent<Rigidbody>();
    m_RigidBody.useGravity = false;
    m_RigidBody.isKinematic = true;
    m_NextWaypoint = 0;
    m_Audio = GetComponent<AudioSource>();
    m_Audio.loop = true;
    m_Audio.clip = SoundMove;
    if (PathWaypoints == null)
      return;
    gameObject.layer = 28;
    foreach (Transform transform in PathWaypoints.transform)
    {
      if (vp_Utility.IsActive(transform.gameObject))
      {
        m_Waypoints.Add(transform);
        transform.gameObject.layer = 28;
      }
      if (transform.GetComponent<Renderer>() != null)
        transform.GetComponent<Renderer>().enabled = false;
      if (transform.GetComponent<Collider>() != null)
        transform.GetComponent<Collider>().enabled = false;
    }
    m_Waypoints.Sort(((IComparer) new WaypointComparer()).Compare);
    if (m_Waypoints.Count <= 0)
      return;
    m_CurrentTargetPosition = m_Waypoints[m_NextWaypoint].position;
    m_CurrentTargetAngle = m_Waypoints[m_NextWaypoint].eulerAngles;
    m_Transform.position = m_CurrentTargetPosition;
    m_Transform.eulerAngles = m_CurrentTargetAngle;
    if (MoveAutoStartTarget <= m_Waypoints.Count - 1)
      return;
    MoveAutoStartTarget = m_Waypoints.Count - 1;
  }

  private void FixedUpdate()
  {
    UpdatePath();
    UpdateMovement();
    UpdateRotation();
    UpdateVelocity();
  }

  protected void UpdatePath()
  {
    if (m_Waypoints.Count < 2 || GetDistanceLeft() >= 0.0099999997764825821 || Time.time < (double) m_NextAllowedMoveTime)
      return;
    switch (PathType)
    {
      case PathMoveType.PingPong:
        if (PathDirection == Direction.Backwards)
        {
          if (m_NextWaypoint == 0)
            PathDirection = Direction.Forward;
        }
        else if (m_NextWaypoint == m_Waypoints.Count - 1)
          PathDirection = Direction.Backwards;
        OnArriveAtWaypoint();
        GoToNextWaypoint();
        break;
      case PathMoveType.Loop:
        OnArriveAtWaypoint();
        GoToNextWaypoint();
        break;
      case PathMoveType.Target:
        if (m_NextWaypoint == m_TargetedWayPoint)
        {
          if (m_Moving)
          {
            OnStop();
            break;
          }
          if (m_NextWaypoint == 0)
            break;
          OnArriveAtDestination();
          break;
        }
        if (m_Moving)
        {
          if (m_PhysicsCurrentMoveVelocity == 0.0)
            OnStart();
          else
            OnArriveAtWaypoint();
        }
        GoToNextWaypoint();
        break;
    }
  }

  protected void OnStart()
  {
    if (!(SoundStart != null))
      return;
    m_Audio.PlayOneShot(SoundStart);
  }

  protected void OnArriveAtWaypoint()
  {
    if (!(SoundWaypoint != null))
      return;
    m_Audio.PlayOneShot(SoundWaypoint);
  }

  protected void OnArriveAtDestination()
  {
    if (MoveReturnDelay <= 0.0 || m_ReturnDelayTimer.Active)
      return;
    vp_Timer.In(MoveReturnDelay, () => GoTo(0), m_ReturnDelayTimer);
  }

  protected void OnStop()
  {
    m_Audio.Stop();
    if (SoundStop != null)
      m_Audio.PlayOneShot(SoundStop);
    m_Transform.position = m_CurrentTargetPosition;
    m_Transform.eulerAngles = m_CurrentTargetAngle;
    m_Moving = false;
    if (m_NextWaypoint != 0)
      return;
    m_NextAllowedMoveTime = Time.time + MoveCooldown;
  }

  protected void UpdateMovement()
  {
    if (m_Waypoints.Count < 2)
      return;
    switch (MoveInterpolationMode)
    {
      case MovementInterpolationMode.EaseInOut:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_Transform.position, m_CurrentTargetPosition, m_EaseInOutCurve.Evaluate(m_MoveTime)));
        break;
      case MovementInterpolationMode.EaseIn:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.MoveTowards(m_Transform.position, m_CurrentTargetPosition, m_MoveTime));
        break;
      case MovementInterpolationMode.EaseOut:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_Transform.position, m_CurrentTargetPosition, m_LinearCurve.Evaluate(m_MoveTime)));
        break;
      case MovementInterpolationMode.EaseOut2:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.Lerp(m_Transform.position, m_CurrentTargetPosition, MoveSpeed * 0.25f));
        break;
      case MovementInterpolationMode.Slerp:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.Slerp(m_Transform.position, m_CurrentTargetPosition, m_LinearCurve.Evaluate(m_MoveTime)));
        break;
      case MovementInterpolationMode.Lerp:
        m_Transform.position = vp_MathUtility.NaNSafeVector3(Vector3.MoveTowards(m_Transform.position, m_CurrentTargetPosition, MoveSpeed));
        break;
    }
  }

  protected void UpdateRotation()
  {
    switch (RotationInterpolationMode)
    {
      case RotateInterpolationMode.SyncToMovement:
        if (!m_Moving)
          break;
        m_Transform.eulerAngles = vp_MathUtility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(m_OriginalAngle.x, m_CurrentTargetAngle.x, (float) (1.0 - GetDistanceLeft() / (double) m_TravelDistance)), Mathf.LerpAngle(m_OriginalAngle.y, m_CurrentTargetAngle.y, (float) (1.0 - GetDistanceLeft() / (double) m_TravelDistance)), Mathf.LerpAngle(m_OriginalAngle.z, m_CurrentTargetAngle.z, (float) (1.0 - GetDistanceLeft() / (double) m_TravelDistance))));
        break;
      case RotateInterpolationMode.EaseOut:
        m_Transform.eulerAngles = vp_MathUtility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(m_Transform.eulerAngles.x, m_CurrentTargetAngle.x, m_LinearCurve.Evaluate(m_MoveTime)), Mathf.LerpAngle(m_Transform.eulerAngles.y, m_CurrentTargetAngle.y, m_LinearCurve.Evaluate(m_MoveTime)), Mathf.LerpAngle(m_Transform.eulerAngles.z, m_CurrentTargetAngle.z, m_LinearCurve.Evaluate(m_MoveTime))));
        break;
      case RotateInterpolationMode.CustomEaseOut:
        m_Transform.eulerAngles = vp_MathUtility.NaNSafeVector3(new Vector3(Mathf.LerpAngle(m_Transform.eulerAngles.x, m_CurrentTargetAngle.x, RotationEaseAmount), Mathf.LerpAngle(m_Transform.eulerAngles.y, m_CurrentTargetAngle.y, RotationEaseAmount), Mathf.LerpAngle(m_Transform.eulerAngles.z, m_CurrentTargetAngle.z, RotationEaseAmount)));
        break;
      case RotateInterpolationMode.CustomRotate:
        m_Transform.Rotate(RotationSpeed);
        break;
    }
  }

  protected void UpdateVelocity()
  {
    m_MoveTime += MoveSpeed * 0.01f * vp_TimeUtility.AdjustedTimeScale;
    m_PhysicsCurrentMoveVelocity = (m_Transform.position - m_PrevPos).magnitude;
    m_PhysicsCurrentRotationVelocity = (m_Transform.eulerAngles - m_PrevAngle).magnitude;
    m_PrevPos = m_Transform.position;
    m_PrevAngle = m_Transform.eulerAngles;
  }

  public void GoTo(int targetWayPoint)
  {
    if (!vp_Gameplay.isMaster || Time.time < (double) m_NextAllowedMoveTime || PathType != PathMoveType.Target)
      return;
    m_TargetedWayPoint = GetValidWaypoint(targetWayPoint);
    if (targetWayPoint > m_NextWaypoint)
    {
      if (PathDirection != Direction.Direct)
        PathDirection = Direction.Forward;
    }
    else if (PathDirection != Direction.Direct)
      PathDirection = Direction.Backwards;
    m_Moving = true;
  }

  protected float GetDistanceLeft() => m_Waypoints.Count < 2 ? 0.0f : Vector3.Distance(m_Transform.position, m_Waypoints[m_NextWaypoint].position);

  protected void GoToNextWaypoint()
  {
    if (m_Waypoints.Count < 2)
      return;
    m_MoveTime = 0.0f;
    if (!m_Audio.isPlaying)
      m_Audio.Play();
    m_CurrentWaypoint = m_NextWaypoint;
    switch (PathDirection)
    {
      case Direction.Forward:
        m_NextWaypoint = GetValidWaypoint(m_NextWaypoint + 1);
        break;
      case Direction.Backwards:
        m_NextWaypoint = GetValidWaypoint(m_NextWaypoint - 1);
        break;
      case Direction.Direct:
        m_NextWaypoint = m_TargetedWayPoint;
        break;
    }
    m_OriginalAngle = m_CurrentTargetAngle;
    m_CurrentTargetPosition = m_Waypoints[m_NextWaypoint].position;
    m_CurrentTargetAngle = m_Waypoints[m_NextWaypoint].eulerAngles;
    m_TravelDistance = GetDistanceLeft();
    m_Moving = true;
  }

  protected int GetValidWaypoint(int wayPoint)
  {
    if (wayPoint < 0)
      return m_Waypoints.Count - 1;
    return wayPoint > m_Waypoints.Count - 1 ? 0 : wayPoint;
  }

  protected void OnTriggerEnter(Collider col)
  {
    if (!GetPlayer(col))
      return;
    TryPushPlayer();
    TryAutoStart();
  }

  protected void OnTriggerStay(Collider col)
  {
    if (!PhysicsSnapPlayerToTopOnIntersect || !GetPlayer(col))
      return;
    TrySnapPlayerToTop();
  }

  public bool GetPlayer(Collider col)
  {
    if (col.gameObject.layer != 8)
      return false;
    if (!m_KnownPlayers.ContainsKey(col))
    {
      vp_PlayerEventHandler component = col.transform.root.GetComponent<vp_PlayerEventHandler>();
      if (component == null)
        return false;
      m_KnownPlayers.Add(col, component);
    }
    if (!m_KnownPlayers.TryGetValue(col, out m_PlayerToPush))
      return false;
    m_PlayerCollider = col;
    return true;
  }

  protected void TryPushPlayer()
  {
    if (m_PlayerToPush == null || m_PlayerToPush.Platform == null || m_PlayerToPush.Position.Get().y > (double) m_Collider.bounds.max.y || m_PlayerToPush.Platform.Get() == m_Transform)
      return;
    float num = m_PhysicsCurrentMoveVelocity;
    if (num == 0.0)
      num = m_PhysicsCurrentRotationVelocity * 0.1f;
  }

  protected void TrySnapPlayerToTop()
  {
    if (m_PlayerToPush == null || m_PlayerToPush.Platform == null)
      return;
    double y1 = m_PlayerToPush.Position.Get().y;
    Bounds bounds = m_Collider.bounds;
    double y2 = bounds.max.y;
    if (y1 > y2 || m_PlayerToPush.Platform.Get() == m_Transform || RotationSpeed.x != 0.0 || RotationSpeed.z != 0.0 || m_CurrentTargetAngle.x != 0.0 || m_CurrentTargetAngle.z != 0.0)
      return;
    bounds = m_Collider.bounds;
    double x1 = bounds.max.x;
    bounds = m_PlayerCollider.bounds;
    double x2 = bounds.max.x;
    if (x1 < x2)
      return;
    bounds = m_Collider.bounds;
    double z1 = bounds.max.z;
    bounds = m_PlayerCollider.bounds;
    double z2 = bounds.max.z;
    if (z1 < z2)
      return;
    bounds = m_Collider.bounds;
    double x3 = bounds.min.x;
    bounds = m_PlayerCollider.bounds;
    double x4 = bounds.min.x;
    if (x3 > x4)
      return;
    bounds = m_Collider.bounds;
    double z3 = bounds.min.z;
    bounds = m_PlayerCollider.bounds;
    double z4 = bounds.min.z;
    if (z3 > z4)
      return;
    Vector3 o = m_PlayerToPush.Position.Get();
    ref Vector3 local = ref o;
    bounds = m_Collider.bounds;
    double num = bounds.max.y - 0.10000000149011612;
    local.y = (float) num;
    m_PlayerToPush.Position.Set(o);
  }

  public void TryAutoStart()
  {
    if (!vp_Gameplay.isMaster || MoveAutoStartTarget == 0 || m_PhysicsCurrentMoveVelocity > 0.0 || m_Moving)
      return;
    GoTo(MoveAutoStartTarget);
  }

  protected class WaypointComparer : IComparer
  {
    int IComparer.Compare(object x, object y) => new CaseInsensitiveComparer().Compare(((UnityEngine.Object) x).name, ((UnityEngine.Object) y).name);
  }

  public enum PathMoveType
  {
    PingPong,
    Loop,
    Target,
  }

  public enum Direction
  {
    Forward,
    Backwards,
    Direct,
  }

  public enum MovementInterpolationMode
  {
    EaseInOut,
    EaseIn,
    EaseOut,
    EaseOut2,
    Slerp,
    Lerp,
  }

  public enum RotateInterpolationMode
  {
    SyncToMovement,
    EaseOut,
    CustomEaseOut,
    CustomRotate,
  }
}
