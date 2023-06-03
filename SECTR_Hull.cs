// Decompiled with JetBrains decompiler
// Type: SECTR_Hull
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public abstract class SECTR_Hull : MonoBehaviour
{
  private Mesh previousMesh;
  private Vector3[] vertsCW;
  private Vector3[] vertsCCW;
  private Vector3 meshCentroid = Vector3.zero;
  protected Vector3 meshNormal = Vector3.forward;
  [SECTR_ToolTip("Convex, planar mesh that defines the portal shape.")]
  public Mesh HullMesh;

  public Vector3[] VertsCW
  {
    get
    {
      ComputeVerts();
      return vertsCW;
    }
  }

  public Vector3[] VertsCCW
  {
    get
    {
      ComputeVerts();
      return vertsCCW;
    }
  }

  public Vector3 Normal
  {
    get
    {
      ComputeVerts();
      return transform.rotation * meshNormal;
    }
  }

  public Vector3 ReverseNormal
  {
    get
    {
      ComputeVerts();
      return transform.rotation * -meshNormal;
    }
  }

  public Vector3 Center
  {
    get
    {
      ComputeVerts();
      return transform.localToWorldMatrix.MultiplyPoint3x4(meshCentroid);
    }
  }

  public Plane HullPlane
  {
    get
    {
      ComputeVerts();
      return new Plane(Normal, Center);
    }
  }

  public Plane ReverseHullPlane
  {
    get
    {
      ComputeVerts();
      return new Plane(ReverseNormal, Center);
    }
  }

  public Bounds BoundingBox
  {
    get
    {
      Bounds boundingBox = new Bounds(transform.position, Vector3.zero);
      if ((bool) (UnityEngine.Object) HullMesh)
      {
        ComputeVerts();
        if (vertsCW != null)
        {
          Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
          int length = vertsCW.Length;
          for (int index = 0; index < length; ++index)
            boundingBox.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(vertsCW[index]));
        }
      }
      return boundingBox;
    }
  }

  public bool IsPointInHull(Vector3 p, float distanceTolerance)
  {
    ComputeVerts();
    Vector3 vector3_1 = transform.worldToLocalMatrix.MultiplyPoint3x4(p);
    Vector3 vector3_2 = vector3_1 - Vector3.Dot(vector3_1 - meshCentroid, meshNormal) * meshNormal;
    if (vertsCW == null || Vector3.SqrMagnitude(vector3_1 - vector3_2) >= distanceTolerance * (double) distanceTolerance)
      return false;
    float f1 = 6.28318548f;
    int length = vertsCW.Length;
    for (int index = 0; index < length; ++index)
    {
      Vector3 lhs = vertsCW[index] - vector3_2;
      Vector3 rhs = vertsCW[(index + 1) % length] - vector3_2;
      float num = lhs.magnitude * rhs.magnitude;
      if (num < 1.0 / 1000.0)
        return true;
      float f2 = Vector3.Dot(lhs, rhs) / num;
      f1 -= Mathf.Acos(f2);
    }
    return Mathf.Abs(f1) < 1.0 / 1000.0;
  }

  protected void ComputeVerts()
  {
    if (!(HullMesh != previousMesh))
      return;
    if ((bool) (UnityEngine.Object) HullMesh)
    {
      int vertexCount = HullMesh.vertexCount;
      vertsCW = new Vector3[vertexCount];
      vertsCCW = new Vector3[vertexCount];
      meshCentroid = Vector3.zero;
      for (int index = 0; index < vertexCount; ++index)
      {
        Vector3 vertex = HullMesh.vertices[index];
        vertsCW[index] = vertex;
        meshCentroid += vertex;
      }
      meshCentroid /= HullMesh.vertexCount;
      meshNormal = Vector3.zero;
      int length = HullMesh.normals.Length;
      for (int index = 0; index < length; ++index)
        meshNormal += HullMesh.normals[index];
      meshNormal /= HullMesh.normals.Length;
      meshNormal.Normalize();
      bool flag = true;
      for (int index = 0; index < vertexCount; ++index)
      {
        Vector3 vector3_1 = vertsCW[index];
        Vector3 vector3_2 = vector3_1 - Vector3.Dot(vector3_1 - meshCentroid, meshNormal) * meshNormal;
        flag = flag && Vector3.SqrMagnitude(vector3_1 - vector3_2) < 1.0 / 1000.0;
        vertsCW[index] = vector3_2;
      }
      if (!flag)
        Debug.LogWarning("Occluder mesh of " + name + " is not planar!");
      Array.Sort(vertsCW, (a, b) => SECTR_Geometry.CompareVectorsCW(a, b, meshCentroid, meshNormal));
      if (!SECTR_Geometry.IsPolygonConvex(vertsCW))
        Debug.LogWarning("Occluder mesh of " + name + " is not convex!");
      vertsCCW = vertsCW;
      Array.Reverse(SECTR_Geometry.CompareVectorsCW(vertsCW[0], vertsCW[0], meshCentroid, meshNormal) >= 0 ? vertsCCW : (Array) vertsCW);
    }
    else
    {
      meshNormal = Vector3.zero;
      meshCentroid = Vector3.zero;
      vertsCW = null;
      vertsCCW = null;
    }
    previousMesh = HullMesh;
  }
}
