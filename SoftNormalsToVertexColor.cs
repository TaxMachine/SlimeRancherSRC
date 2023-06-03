// Decompiled with JetBrains decompiler
// Type: SoftNormalsToVertexColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
public class SoftNormalsToVertexColor : MonoBehaviour
{
  public Method method = Method.AngularDeviation;
  public bool generateOnAwake;
  public bool generateNow;

  private void OnDrawGizmos()
  {
    if (!generateNow)
      return;
    generateNow = false;
    TryGenerate();
  }

  private void Awake()
  {
    if (!generateOnAwake)
      return;
    TryGenerate();
  }

  private void TryGenerate()
  {
    MeshFilter component = GetComponent<MeshFilter>();
    if (component == null)
      Debug.LogError("MeshFilter missing on the vertex color generator", gameObject);
    else if (component.sharedMesh == null)
    {
      Debug.LogError("Assign a mesh to the MeshFilter before generating vertex colors", gameObject);
    }
    else
    {
      Generate(component.sharedMesh);
      Debug.Log("Vertex colors generated", gameObject);
    }
  }

  private void Generate(Mesh m)
  {
    Vector3[] normals = m.normals;
    Vector3[] vertices = m.vertices;
    Color[] colorArray = new Color[normals.Length];
    List<List<int>> intListList = new List<List<int>>();
    for (int index = 0; index < vertices.Length; ++index)
    {
      bool flag = false;
      foreach (List<int> intList in intListList)
      {
        if (vertices[intList[0]] == vertices[index])
        {
          intList.Add(index);
          flag = true;
          break;
        }
      }
      if (!flag)
        intListList.Add(new List<int>() { index });
    }
    foreach (List<int> intList in intListList)
    {
      Vector3 zero = Vector3.zero;
      foreach (int index in intList)
        zero += normals[index];
      zero.Normalize();
      if (method == Method.AngularDeviation)
      {
        float num1 = 0.0f;
        foreach (int index in intList)
          num1 += Vector3.Dot(normals[index], zero);
        float num2 = 0.5f / Mathf.Sin((float) (180.0 - Mathf.Acos(num1 / intList.Count) * 57.29578f - 90.0) * ((float) Math.PI / 180f));
        zero *= num2;
      }
      foreach (int index in intList)
        colorArray[index] = new Color(zero.x, zero.y, zero.z);
    }
    m.colors = colorArray;
  }

  public enum Method
  {
    Simple,
    AngularDeviation,
  }
}
