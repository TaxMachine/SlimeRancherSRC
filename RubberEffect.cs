// Decompiled with JetBrains decompiler
// Type: RubberEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class RubberEffect : MonoBehaviour
{
  public RubberType Presets;
  public float effectIntensity = 1f;
  public float gravity;
  public float damping = 0.7f;
  public float mass = 1f;
  public float stiffness = 0.2f;
  private float invMass;
  private float vacHeldFactor = 0.33f;
  private Mesh workingMesh;
  private Mesh originalMesh;
  private VertexRubber[] verts;
  private Vector3[] workingMeshVectors;
  private bool sleeping = true;
  private Vector3 lastWorldPosition;
  private Quaternion lastWorldRotation;
  private bool wasVisible;
  private Vacuumable vacuumable;

  private void Start()
  {
    CheckPreset();
    invMass = 1f / mass;
    vacuumable = GetComponentInParent<Vacuumable>();
    MeshFilter component = (MeshFilter) GetComponent(typeof (MeshFilter));
    originalMesh = component.sharedMesh;
    workingMesh = Instantiate(component.sharedMesh);
    component.sharedMesh = workingMesh;
    List<int> intList = new List<int>();
    Color[] colors = originalMesh.colors;
    Vector3[] vertices = originalMesh.vertices;
    for (int index = 0; index < vertices.Length; ++index)
    {
      if (colors[index].grayscale != 1.0)
        intList.Add(index);
    }
    verts = new VertexRubber[intList.Count];
    for (int index1 = 0; index1 < intList.Count; ++index1)
    {
      int index2 = intList[index1];
      verts[index1] = new VertexRubber(transform.TransformPoint(vertices[index2]), this);
      verts[index1].colorIntensity = (1f - colors[index2].grayscale) * effectIntensity;
      verts[index1].indexId = index2;
    }
    workingMeshVectors = originalMesh.vertices;
  }

  public void OnEnable() => wasVisible = false;

  private void LateUpdate()
  {
    if (!GetComponent<Renderer>().isVisible)
    {
      if (!sleeping)
      {
        workingMesh.vertices = originalMesh.vertices;
        workingMesh.RecalculateBounds();
        sleeping = true;
        foreach (VertexRubber vert in verts)
          vert.vertSleeping = true;
      }
    }
    else
    {
      if (!wasVisible || transform.position != lastWorldPosition || transform.rotation != lastWorldRotation)
      {
        foreach (VertexRubber vert in verts)
        {
          if (vert.vertSleeping || !wasVisible)
            vert.Reset();
        }
        sleeping = false;
      }
      if (!sleeping)
      {
        float num1 = vacuumable.isHeld() ? vacHeldFactor : 1f;
        workingMeshVectors = originalMesh.vertices;
        int num2 = 0;
        float num3 = Time.deltaTime / 0.016667f;
        float timeAdjDamping = Mathf.Pow(damping, num3);
        foreach (VertexRubber vert in verts)
        {
          if (vert.vertSleeping)
          {
            ++num2;
          }
          else
          {
            Vector3 target = transform.TransformPoint(workingMeshVectors[vert.indexId]);
            vert.Update(target, num3, timeAdjDamping);
          }
          Vector3 b = transform.InverseTransformPoint(vert.pos);
          workingMeshVectors[vert.indexId] = Vector3.Lerp(workingMeshVectors[vert.indexId], b, vert.colorIntensity * num1);
          if (vert.vertSleeping)
            ++num2;
        }
        workingMesh.vertices = workingMeshVectors;
        workingMesh.RecalculateBounds();
        if (transform.position == lastWorldPosition && transform.rotation == lastWorldRotation)
        {
          if (num2 == verts.Length)
            sleeping = true;
        }
        else
        {
          lastWorldPosition = transform.position;
          lastWorldRotation = transform.rotation;
        }
      }
    }
    wasVisible = GetComponent<Renderer>().isVisible;
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
        gravity = 0.9f;
        mass = 6f;
        stiffness = 0.333f;
        damping = 0.85f;
        effectIntensity = 1f;
        break;
      case RubberType.SlimeTarr:
        gravity = 0.9f;
        mass = 8f;
        stiffness = 0.333f;
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

  internal class VertexRubber
  {
    public int indexId;
    private RubberEffect effect;
    public Vector3 pos;
    public Vector3 vel;
    public Vector3 force;
    public Vector3 lastPos;
    public Vector3 lastVel;
    public Vector3 lastForce;
    public bool vertSleeping;
    public float colorIntensity;

    public VertexRubber(Vector3 target, RubberEffect effect)
    {
      this.effect = effect;
      pos = target;
    }

    public void Reset()
    {
      Vector3 vector3 = effect.transform.TransformPoint(effect.originalMesh.vertices[indexId]);
      lastVel = Vector3.zero;
      lastForce = Vector3.zero;
      lastPos = vector3;
      pos = vector3;
      force = Vector3.zero;
      vel = Vector3.zero;
      vertSleeping = false;
    }

    public void Update(Vector3 target, float timeFactor, float timeAdjDamping)
    {
      if (vertSleeping)
        return;
      force = (target - pos) * effect.stiffness;
      force.y -= effect.gravity * 0.1f;
      vel = timeAdjDamping * (vel + force * (timeFactor * effect.invMass));
      pos += vel * timeFactor;
      if (pos == lastPos && vel == lastVel && force == lastForce)
      {
        vertSleeping = true;
      }
      else
      {
        lastPos = pos;
        lastVel = vel;
        lastForce = force;
      }
    }
  }
}
