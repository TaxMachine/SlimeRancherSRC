// Decompiled with JetBrains decompiler
// Type: DynamicBone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone : MonoBehaviour
{
  public Transform m_Root;
  public float m_UpdateRate = 60f;
  [Range(0.0f, 1f)]
  public float m_Damping = 0.1f;
  public AnimationCurve m_DampingDistrib;
  [Range(0.0f, 1f)]
  public float m_Elasticity = 0.1f;
  public AnimationCurve m_ElasticityDistrib;
  [Range(0.0f, 1f)]
  public float m_Stiffness = 0.1f;
  public AnimationCurve m_StiffnessDistrib;
  [Range(0.0f, 1f)]
  public float m_Inert;
  public AnimationCurve m_InertDistrib;
  public float m_Radius;
  public AnimationCurve m_RadiusDistrib;
  public float m_EndLength;
  public Vector3 m_EndOffset = Vector3.zero;
  public Vector3 m_Gravity = Vector3.zero;
  public Vector3 m_Force = Vector3.zero;
  public List<DynamicBoneCollider> m_Colliders;
  public List<Transform> m_Exclusions;
  public FreezeAxis m_FreezeAxis;
  private Vector3 m_LocalGravity = Vector3.zero;
  private Vector3 m_ObjectMove = Vector3.zero;
  private Vector3 m_ObjectPrevPosition = Vector3.zero;
  private float m_BoneTotalLength;
  private float m_ObjectScale = 1f;
  private float m_Time;
  private float m_Weight = 1f;
  private List<Particle> m_Particles = new List<Particle>();
  private Plane movePlane;

  private void Start() => SetupParticles();

  private void Update()
  {
    if (m_Weight <= 0.0)
      return;
    InitTransforms();
  }

  private void LateUpdate()
  {
    if (m_Weight <= 0.0)
      return;
    UpdateDynamicBones(Time.deltaTime);
  }

  private void OnEnable()
  {
    ResetParticlesPosition();
    m_ObjectPrevPosition = transform.position;
  }

  private void OnDisable() => InitTransforms();

  private void OnValidate()
  {
    m_UpdateRate = Mathf.Max(m_UpdateRate, 0.0f);
    m_Damping = Mathf.Clamp01(m_Damping);
    m_Elasticity = Mathf.Clamp01(m_Elasticity);
    m_Stiffness = Mathf.Clamp01(m_Stiffness);
    m_Inert = Mathf.Clamp01(m_Inert);
    m_Radius = Mathf.Max(m_Radius, 0.0f);
    if (!Application.isEditor || !Application.isPlaying)
      return;
    InitTransforms();
    SetupParticles();
  }

  private void OnDrawGizmosSelected()
  {
    if (!enabled || m_Root == null)
      return;
    if (Application.isEditor && !Application.isPlaying && transform.hasChanged)
    {
      InitTransforms();
      SetupParticles();
    }
    Gizmos.color = Color.white;
    foreach (Particle particle1 in m_Particles)
    {
      if (particle1.m_ParentIndex >= 0)
      {
        Particle particle2 = m_Particles[particle1.m_ParentIndex];
        Gizmos.DrawLine(particle1.m_Position, particle2.m_Position);
      }
      if (particle1.m_Radius > 0.0)
        Gizmos.DrawWireSphere(particle1.m_Position, particle1.m_Radius * m_ObjectScale);
    }
  }

  public void SetWeight(float w)
  {
    if (m_Weight == (double) w)
      return;
    if (w == 0.0)
      InitTransforms();
    else if (m_Weight == 0.0)
    {
      ResetParticlesPosition();
      m_ObjectPrevPosition = transform.position;
    }
    m_Weight = w;
  }

  public float GetWeight() => m_Weight;

  private void UpdateDynamicBones(float t)
  {
    if (m_Root == null)
      return;
    m_ObjectScale = Mathf.Abs(transform.lossyScale.x);
    m_ObjectMove = transform.position - m_ObjectPrevPosition;
    m_ObjectPrevPosition = transform.position;
    int num1 = 1;
    if (m_UpdateRate > 0.0)
    {
      float num2 = 1f / m_UpdateRate;
      m_Time += t;
      num1 = 0;
      while (m_Time >= (double) num2)
      {
        m_Time -= num2;
        if (++num1 >= 3)
        {
          m_Time = 0.0f;
          break;
        }
      }
    }
    if (num1 > 0)
    {
      for (int index = 0; index < num1; ++index)
      {
        UpdateParticles1();
        UpdateParticles2();
        m_ObjectMove = Vector3.zero;
      }
    }
    else
      SkipUpdateParticles();
    ApplyParticlesToTransforms();
  }

  private void SetupParticles()
  {
    m_Particles.Clear();
    if (m_Root == null)
      return;
    m_LocalGravity = m_Root.InverseTransformDirection(m_Gravity);
    m_ObjectScale = transform.lossyScale.x;
    m_ObjectPrevPosition = transform.position;
    m_ObjectMove = Vector3.zero;
    m_BoneTotalLength = 0.0f;
    AppendParticles(m_Root, -1, 0.0f);
    foreach (Particle particle in m_Particles)
    {
      particle.m_Damping = m_Damping;
      particle.m_Elasticity = m_Elasticity;
      particle.m_Stiffness = m_Stiffness;
      particle.m_Inert = m_Inert;
      particle.m_Radius = m_Radius;
      if (m_BoneTotalLength > 0.0)
      {
        float time = particle.m_BoneLength / m_BoneTotalLength;
        if (m_DampingDistrib.keys.Length != 0)
          particle.m_Damping *= m_DampingDistrib.Evaluate(time);
        if (m_ElasticityDistrib.keys.Length != 0)
          particle.m_Elasticity *= m_ElasticityDistrib.Evaluate(time);
        if (m_StiffnessDistrib.keys.Length != 0)
          particle.m_Stiffness *= m_StiffnessDistrib.Evaluate(time);
        if (m_InertDistrib.keys.Length != 0)
          particle.m_Inert *= m_InertDistrib.Evaluate(time);
        if (m_RadiusDistrib.keys.Length != 0)
          particle.m_Radius *= m_RadiusDistrib.Evaluate(time);
      }
      particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
      particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
      particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
      particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
      particle.m_Radius = Mathf.Max(particle.m_Radius, 0.0f);
    }
  }

  private void AppendParticles(Transform b, int parentIndex, float boneLength)
  {
    Particle particle = new Particle();
    particle.m_Transform = b;
    particle.m_ParentIndex = parentIndex;
    if (b != null)
    {
      particle.m_Position = particle.m_PrevPosition = b.position;
      particle.m_InitLocalPosition = b.localPosition;
      particle.m_InitLocalRotation = b.localRotation;
    }
    else
    {
      Transform transform = m_Particles[parentIndex].m_Transform;
      if (m_EndLength > 0.0)
      {
        Transform parent = transform.parent;
        particle.m_EndOffset = !(parent != null) ? new Vector3(m_EndLength, 0.0f, 0.0f) : transform.InverseTransformPoint(transform.position * 2f - parent.position) * m_EndLength;
      }
      else
        particle.m_EndOffset = transform.InverseTransformPoint(this.transform.TransformDirection(m_EndOffset) + transform.position);
      particle.m_Position = particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset);
    }
    if (parentIndex >= 0)
    {
      boneLength += (m_Particles[parentIndex].m_Transform.position - particle.m_Position).magnitude;
      particle.m_BoneLength = boneLength;
      m_BoneTotalLength = Mathf.Max(m_BoneTotalLength, boneLength);
    }
    int count = m_Particles.Count;
    m_Particles.Add(particle);
    if (!(b != null))
      return;
    for (int index = 0; index < b.childCount; ++index)
    {
      bool flag = false;
      if (m_Exclusions != null)
      {
        foreach (Object exclusion in m_Exclusions)
        {
          if (exclusion == b.GetChild(index))
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        AppendParticles(b.GetChild(index), count, boneLength);
    }
    if (b.childCount != 0 || m_EndLength <= 0.0 && !(m_EndOffset != Vector3.zero))
      return;
    AppendParticles(null, count, boneLength);
  }

  private void InitTransforms()
  {
    for (int index = 0; index < m_Particles.Count; ++index)
    {
      Particle particle = m_Particles[index];
      if (particle.m_Transform != null)
      {
        particle.m_Transform.localPosition = particle.m_InitLocalPosition;
        particle.m_Transform.localRotation = particle.m_InitLocalRotation;
      }
    }
  }

  private void ResetParticlesPosition()
  {
    foreach (Particle particle in m_Particles)
    {
      if (particle.m_Transform != null)
      {
        particle.m_Position = particle.m_PrevPosition = particle.m_Transform.position;
      }
      else
      {
        Transform transform = m_Particles[particle.m_ParentIndex].m_Transform;
        particle.m_Position = particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset);
      }
    }
  }

  private void UpdateParticles1()
  {
    Vector3 gravity = m_Gravity;
    Vector3 normalized = m_Gravity.normalized;
    Vector3 lhs = m_Root.TransformDirection(m_LocalGravity);
    Vector3 vector3_1 = normalized * Mathf.Max(Vector3.Dot(lhs, normalized), 0.0f);
    Vector3 vector3_2 = (gravity - vector3_1 + m_Force) * m_ObjectScale;
    for (int index = 0; index < m_Particles.Count; ++index)
    {
      Particle particle = m_Particles[index];
      if (particle.m_ParentIndex >= 0)
      {
        Vector3 vector3_3 = particle.m_Position - particle.m_PrevPosition;
        Vector3 vector3_4 = m_ObjectMove * particle.m_Inert;
        particle.m_PrevPosition = particle.m_Position + vector3_4;
        particle.m_Position += vector3_3 * (1f - particle.m_Damping) + vector3_2 + vector3_4;
      }
      else
      {
        particle.m_PrevPosition = particle.m_Position;
        particle.m_Position = particle.m_Transform.position;
      }
    }
  }

  private void UpdateParticles2()
  {
    movePlane.SetNormalAndPosition(Vector3.zero, Vector3.zero);
    for (int index = 1; index < m_Particles.Count; ++index)
    {
      Particle particle1 = m_Particles[index];
      Particle particle2 = m_Particles[particle1.m_ParentIndex];
      Vector3 vector3_1;
      float magnitude1;
      if (particle1.m_Transform != null)
      {
        vector3_1 = particle2.m_Transform.position - particle1.m_Transform.position;
        magnitude1 = vector3_1.magnitude;
      }
      else
      {
        vector3_1 = particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle1.m_EndOffset);
        magnitude1 = vector3_1.magnitude;
      }
      float num1 = Mathf.Lerp(1f, particle1.m_Stiffness, m_Weight);
      if (num1 > 0.0 || particle1.m_Elasticity > 0.0)
      {
        Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
        localToWorldMatrix.SetColumn(3, particle2.m_Position);
        Vector3 vector3_2 = !(particle1.m_Transform != null) ? localToWorldMatrix.MultiplyPoint3x4(particle1.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle1.m_Transform.localPosition);
        Vector3 vector3_3 = vector3_2 - particle1.m_Position;
        particle1.m_Position += vector3_3 * particle1.m_Elasticity;
        if (num1 > 0.0)
        {
          Vector3 vector3_4 = vector3_2 - particle1.m_Position;
          float magnitude2 = vector3_4.magnitude;
          float num2 = (float) (magnitude1 * (1.0 - num1) * 2.0);
          if (magnitude2 > (double) num2)
            particle1.m_Position += vector3_4 * ((magnitude2 - num2) / magnitude2);
        }
      }
      if (m_Colliders != null)
      {
        float particleRadius = particle1.m_Radius * m_ObjectScale;
        foreach (DynamicBoneCollider collider in m_Colliders)
        {
          if (collider != null && collider.enabled)
            collider.Collide(ref particle1.m_Position, particleRadius);
        }
      }
      if (m_FreezeAxis != FreezeAxis.None)
      {
        switch (m_FreezeAxis)
        {
          case FreezeAxis.X:
            movePlane.SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
            break;
          case FreezeAxis.Y:
            movePlane.SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
            break;
          case FreezeAxis.Z:
            movePlane.SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
            break;
        }
        particle1.m_Position -= movePlane.normal * movePlane.GetDistanceToPoint(particle1.m_Position);
      }
      Vector3 vector3_5 = particle2.m_Position - particle1.m_Position;
      float magnitude3 = vector3_5.magnitude;
      if (magnitude3 > 0.0)
        particle1.m_Position += vector3_5 * ((magnitude3 - magnitude1) / magnitude3);
    }
  }

  private void SkipUpdateParticles()
  {
    foreach (Particle particle1 in m_Particles)
    {
      if (particle1.m_ParentIndex >= 0)
      {
        Vector3 vector3_1 = m_ObjectMove * particle1.m_Inert;
        particle1.m_PrevPosition += vector3_1;
        particle1.m_Position += vector3_1;
        Particle particle2 = m_Particles[particle1.m_ParentIndex];
        float num1 = !(particle1.m_Transform != null) ? particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle1.m_EndOffset).magnitude : (particle2.m_Transform.position - particle1.m_Transform.position).magnitude;
        float num2 = Mathf.Lerp(1f, particle1.m_Stiffness, m_Weight);
        if (num2 > 0.0)
        {
          Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
          localToWorldMatrix.SetColumn(3, particle2.m_Position);
          Vector3 vector3_2 = (!(particle1.m_Transform != null) ? localToWorldMatrix.MultiplyPoint3x4(particle1.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle1.m_Transform.localPosition)) - particle1.m_Position;
          float magnitude = vector3_2.magnitude;
          float num3 = (float) (num1 * (1.0 - num2) * 2.0);
          if (magnitude > (double) num3)
            particle1.m_Position += vector3_2 * ((magnitude - num3) / magnitude);
        }
        Vector3 vector3_3 = particle2.m_Position - particle1.m_Position;
        float magnitude1 = vector3_3.magnitude;
        if (magnitude1 > 0.0)
          particle1.m_Position += vector3_3 * ((magnitude1 - num1) / magnitude1);
      }
      else
      {
        particle1.m_PrevPosition = particle1.m_Position;
        particle1.m_Position = particle1.m_Transform.position;
      }
    }
  }

  private void ApplyParticlesToTransforms()
  {
    for (int index = 1; index < m_Particles.Count; ++index)
    {
      Particle particle1 = m_Particles[index];
      Particle particle2 = m_Particles[particle1.m_ParentIndex];
      if (particle2.m_Transform.childCount <= 1)
      {
        Vector3 direction = !(particle1.m_Transform != null) ? particle1.m_EndOffset : particle1.m_Transform.localPosition;
        Quaternion rotation = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), particle1.m_Position - particle2.m_Position);
        particle2.m_Transform.rotation = rotation * particle2.m_Transform.rotation;
      }
      if ((bool) (Object) particle1.m_Transform)
        particle1.m_Transform.position = particle1.m_Position;
    }
  }

  public enum FreezeAxis
  {
    None,
    X,
    Y,
    Z,
  }

  private class Particle
  {
    public Transform m_Transform;
    public int m_ParentIndex = -1;
    public float m_Damping;
    public float m_Elasticity;
    public float m_Stiffness;
    public float m_Inert;
    public float m_Radius;
    public float m_BoneLength;
    public Vector3 m_Position = Vector3.zero;
    public Vector3 m_PrevPosition = Vector3.zero;
    public Vector3 m_EndOffset = Vector3.zero;
    public Vector3 m_InitLocalPosition = Vector3.zero;
    public Quaternion m_InitLocalRotation = Quaternion.identity;
  }
}
