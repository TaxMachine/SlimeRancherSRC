// Decompiled with JetBrains decompiler
// Type: vp_Spring
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_Spring
{
  protected UpdateMode Mode;
  protected bool m_AutoUpdate = true;
  protected UpdateDelegate m_UpdateFunc;
  public Vector3 State = Vector3.zero;
  protected Vector3 m_Velocity = Vector3.zero;
  public Vector3 RestState = Vector3.zero;
  public Vector3 Stiffness = new Vector3(0.5f, 0.5f, 0.5f);
  public Vector3 Damping = new Vector3(0.75f, 0.75f, 0.75f);
  protected float m_VelocityFadeInCap = 1f;
  protected float m_VelocityFadeInEndTime;
  protected float m_VelocityFadeInLength;
  protected Vector3[] m_SoftForceFrame = new Vector3[120];
  public float MaxVelocity = 10000f;
  public float MinVelocity = 1E-07f;
  public Vector3 MaxState = new Vector3(10000f, 10000f, 10000f);
  public Vector3 MinState = new Vector3(-10000f, -10000f, -10000f);
  protected Transform m_Transform;

  public Transform Transform
  {
    get => m_Transform;
    set
    {
      m_Transform = value;
      RefreshUpdateMode();
    }
  }

  public vp_Spring(Transform transform, UpdateMode mode, bool autoUpdate = true)
  {
    Mode = mode;
    Transform = transform;
    m_AutoUpdate = autoUpdate;
  }

  public void FixedUpdate()
  {
    m_VelocityFadeInCap = m_VelocityFadeInEndTime <= (double) Time.time ? 1f : Mathf.Clamp01((float) (1.0 - (m_VelocityFadeInEndTime - (double) Time.time) / m_VelocityFadeInLength));
    if (m_SoftForceFrame[0] != Vector3.zero)
    {
      AddForceInternal(m_SoftForceFrame[0]);
      for (int index = 0; index < 120; ++index)
      {
        m_SoftForceFrame[index] = index < 119 ? m_SoftForceFrame[index + 1] : Vector3.zero;
        if (m_SoftForceFrame[index] == Vector3.zero)
          break;
      }
    }
    Calculate();
    m_UpdateFunc();
  }

  private void Position() => m_Transform.localPosition = State;

  private void Rotation() => m_Transform.localEulerAngles = State;

  private void Scale() => m_Transform.localScale = State;

  private void PositionAdditiveLocal() => m_Transform.localPosition += State;

  private void PositionAdditiveGlobal() => m_Transform.position += State;

  private void PositionAdditiveSelf() => m_Transform.Translate(State, m_Transform);

  private void RotationAdditiveLocal() => m_Transform.localEulerAngles += State;

  private void RotationAdditiveGlobal() => m_Transform.eulerAngles += State;

  private void ScaleAdditiveLocal() => m_Transform.localScale += State;

  private void None()
  {
  }

  protected void RefreshUpdateMode()
  {
    m_UpdateFunc = None;
    switch (Mode)
    {
      case UpdateMode.Position:
        State = m_Transform.localPosition;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = Position;
          break;
        }
        break;
      case UpdateMode.PositionAdditiveLocal:
        State = m_Transform.localPosition;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = PositionAdditiveLocal;
          break;
        }
        break;
      case UpdateMode.PositionAdditiveGlobal:
        State = m_Transform.position;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = PositionAdditiveGlobal;
          break;
        }
        break;
      case UpdateMode.PositionAdditiveSelf:
        State = m_Transform.position;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = PositionAdditiveSelf;
          break;
        }
        break;
      case UpdateMode.Rotation:
        State = m_Transform.localEulerAngles;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = Rotation;
          break;
        }
        break;
      case UpdateMode.RotationAdditiveLocal:
        State = m_Transform.localEulerAngles;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = RotationAdditiveLocal;
          break;
        }
        break;
      case UpdateMode.RotationAdditiveGlobal:
        State = m_Transform.eulerAngles;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = RotationAdditiveGlobal;
          break;
        }
        break;
      case UpdateMode.Scale:
        State = m_Transform.localScale;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = Scale;
          break;
        }
        break;
      case UpdateMode.ScaleAdditiveLocal:
        State = m_Transform.localScale;
        if (m_AutoUpdate)
        {
          m_UpdateFunc = ScaleAdditiveLocal;
          break;
        }
        break;
    }
    RestState = State;
  }

  protected void Calculate()
  {
    if (State == RestState)
      return;
    m_Velocity += Vector3.Scale(RestState - State, Stiffness);
    m_Velocity = Vector3.Scale(m_Velocity, Damping);
    m_Velocity = Vector3.ClampMagnitude(m_Velocity, MaxVelocity);
    if (m_Velocity.sqrMagnitude > MinVelocity * (double) MinVelocity)
      Move();
    else
      Reset();
  }

  private void AddForceInternal(Vector3 force)
  {
    force *= m_VelocityFadeInCap;
    m_Velocity += force;
    m_Velocity = Vector3.ClampMagnitude(m_Velocity, MaxVelocity);
    Move();
  }

  public void AddForce(Vector3 force)
  {
    if (Time.timeScale < 1.0)
      AddSoftForce(force, 1f);
    else
      AddForceInternal(force);
  }

  public void AddSoftForce(Vector3 force, float frames)
  {
    force /= Time.timeScale;
    frames = Mathf.Clamp(frames, 1f, 120f);
    AddForceInternal(force / frames);
    for (int index = 0; index < Mathf.RoundToInt(frames) - 1; ++index)
      m_SoftForceFrame[index] += force / frames;
  }

  protected void Move()
  {
    State += m_Velocity * Time.timeScale;
    State.x = Mathf.Clamp(State.x, MinState.x, MaxState.x);
    State.y = Mathf.Clamp(State.y, MinState.y, MaxState.y);
    State.z = Mathf.Clamp(State.z, MinState.z, MaxState.z);
  }

  public void Reset()
  {
    m_Velocity = Vector3.zero;
    State = RestState;
  }

  public void Stop(bool includeSoftForce = false)
  {
    m_Velocity = Vector3.zero;
    if (!includeSoftForce)
      return;
    StopSoftForce();
  }

  public void StopSoftForce()
  {
    for (int index = 0; index < 120; ++index)
      m_SoftForceFrame[index] = Vector3.zero;
  }

  public void ForceVelocityFadeIn(float seconds)
  {
    m_VelocityFadeInLength = seconds;
    m_VelocityFadeInEndTime = Time.time + seconds;
    m_VelocityFadeInCap = 0.0f;
  }

  public enum UpdateMode
  {
    Position,
    PositionAdditiveLocal,
    PositionAdditiveGlobal,
    PositionAdditiveSelf,
    Rotation,
    RotationAdditiveLocal,
    RotationAdditiveGlobal,
    Scale,
    ScaleAdditiveLocal,
  }

  protected delegate void UpdateDelegate();
}
