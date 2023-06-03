// Decompiled with JetBrains decompiler
// Type: RubberBoneEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class RubberBoneEffect : MonoBehaviour
{
  public RubberType Presets;
  public float effectIntensity = 1f;
  public float gravity;
  public float damping = 0.7f;
  public float mass = 1f;
  public float stiffness = 0.2f;
  [Tooltip("All the bones we want to manipulate and how much to manipulate them.")]
  public BoneEntry[] bones;
  public SkinnedMeshRenderer skinRenderer;
  [Tooltip("If the effect should ignore time scaling.")]
  public bool unscaledTime;
  private float invMass;
  private float vacHeldFactor = 0.1f;
  private VertexRubber[] verts;
  private bool sleeping = true;
  private Vector3 lastWorldPosition;
  private Quaternion lastWorldRotation;
  private bool wasSleeping;
  private Vacuumable vacuumable;
  private Rigidbody ownBody;
  private Renderer ownRenderer;

  public void Reset()
  {
    for (int index = 0; index < verts.Length; ++index)
      verts[index].Reset();
  }

  private void Start()
  {
    ownBody = GetComponentInParent<Rigidbody>();
    ownRenderer = skinRenderer != null ? skinRenderer : GetComponentInParent<Renderer>();
    CheckPreset();
    invMass = 1f / mass;
    vacuumable = GetComponentInParent<Vacuumable>();
    verts = new VertexRubber[bones.Length];
    for (int index = 0; index < bones.Length; ++index)
      verts[index] = new VertexRubber(bones[index], this);
  }

  public void OnEnable() => wasSleeping = true;

  private void LateUpdate()
  {
    bool flag = ownBody != null && ownBody.IsSleeping() || ownRenderer != null && !ownRenderer.isVisible;
    if (flag)
    {
      if (!sleeping)
      {
        foreach (VertexRubber vert in verts)
          vert.Reset();
      }
      sleeping = true;
    }
    else
    {
      if (wasSleeping || transform.position != lastWorldPosition || transform.rotation != lastWorldRotation)
      {
        foreach (VertexRubber vert in verts)
        {
          if (wasSleeping)
            vert.Reset();
        }
        sleeping = false;
      }
      if (!sleeping)
      {
        float heldFactor = !(vacuumable != null) || !vacuumable.isHeld() ? 1f : vacHeldFactor;
        float num = (float) ((unscaledTime ? Time.unscaledDeltaTime : (double) Time.deltaTime) * 60.0);
        float timeAdjDamping = Mathf.Pow(damping, num);
        foreach (VertexRubber vert in verts)
          vert.LateUpdate(num, timeAdjDamping, heldFactor);
        lastWorldPosition = transform.position;
        lastWorldRotation = transform.rotation;
      }
    }
    wasSleeping = flag;
  }

  private void CheckPreset()
  {
    switch (Presets)
    {
      case RubberType.RubberDuck:
        gravity = 0.0f;
        mass = 2f;
        stiffness = 0.5f;
        damping = 0.85f;
        effectIntensity = 1f;
        break;
      case RubberType.HardRubber:
        gravity = 0.0f;
        mass = 8f;
        stiffness = 0.5f;
        damping = 0.9f;
        effectIntensity = 0.5f;
        break;
      case RubberType.Jelly:
        gravity = 0.0f;
        mass = 1f;
        stiffness = 0.95f;
        damping = 0.95f;
        effectIntensity = 1f;
        break;
      case RubberType.SoftLatex:
        gravity = 1f;
        mass = 0.9f;
        stiffness = 0.3f;
        damping = 0.25f;
        effectIntensity = 1f;
        break;
      case RubberType.Slime:
        gravity = 0.2f;
        mass = 6f;
        stiffness = 1f;
        damping = 0.75f;
        effectIntensity = 1f;
        break;
      case RubberType.SlimeTarr:
        gravity = 0.2f;
        mass = 8f;
        stiffness = 1f;
        damping = 0.85f;
        effectIntensity = 1f;
        break;
    }
  }

  public enum RubberType
  {
    Custom,
    RubberDuck,
    HardRubber,
    Jelly,
    SoftLatex,
    Slime,
    SlimeTarr,
  }

  [Serializable]
  public class BoneEntry
  {
    public Transform trans;
    public float intensity;
  }

  internal class VertexRubber
  {
    private RubberBoneEffect effect;
    public Vector3 pos;
    public Vector3 vel;
    public Vector3 force;
    public Vector3 rootPos;
    public float intensity;
    public Vector3 lastLocalPos;
    private BoneEntry bone;

    public VertexRubber(BoneEntry bone, RubberBoneEffect effect)
    {
      this.bone = bone;
      this.effect = effect;
      pos = bone.trans.position;
      rootPos = bone.trans.localPosition;
      lastLocalPos = rootPos;
      intensity = bone.intensity * effect.effectIntensity;
      Reset();
    }

    public void Reset()
    {
      Vector3 position = rootPos + Vector3.down * (float) ((effect.gravity * 5.0 - effect.damping) * 0.75);
      Vector3 vector3 = effect.transform.TransformPoint(position);
      lastLocalPos = position;
      pos = vector3;
      force = Vector3.zero;
      vel = Vector3.zero;
      bone.trans.localPosition = position;
    }

    public void LateUpdate(float timeFactor, float timeAdjDamping, float heldFactor)
    {
      Vector3 vector3 = effect.transform.TransformPoint(rootPos) - pos;
      force = vector3 * (effect.stiffness * Mathf.Min(1f, vector3.magnitude));
      force.y -= effect.gravity * 0.1f;
      vel = timeAdjDamping * (vel + force * (timeFactor * effect.invMass));
      pos += vel * timeFactor;
      bone.trans.localPosition = Vector3.Lerp(rootPos, effect.transform.InverseTransformPoint(pos), intensity * heldFactor);
      lastLocalPos = bone.trans.localPosition;
    }
  }
}
