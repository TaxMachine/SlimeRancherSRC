// Decompiled with JetBrains decompiler
// Type: Tangentify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Tangentify
{
  public static void AddTangents(Mesh mesh)
  {
    int num1 = mesh.triangles.Length / 3;
    int length = mesh.vertices.Length;
    Vector3[] vector3Array1 = new Vector3[length];
    Vector3[] vector3Array2 = new Vector3[length];
    Vector4[] vector4Array = new Vector4[length];
    for (long index = 0; index < num1; index += 3L)
    {
      long triangle1 = mesh.triangles[index];
      long triangle2 = mesh.triangles[index + 1L];
      long triangle3 = mesh.triangles[index + 2L];
      Vector3 vertex1 = mesh.vertices[triangle1];
      Vector3 vertex2 = mesh.vertices[triangle2];
      Vector3 vertex3 = mesh.vertices[triangle3];
      Vector2 vector2_1 = mesh.uv[triangle1];
      Vector2 vector2_2 = mesh.uv[triangle2];
      Vector2 vector2_3 = mesh.uv[triangle3];
      float num2 = vertex2.x - vertex1.x;
      float num3 = vertex3.x - vertex1.x;
      float num4 = vertex2.y - vertex1.y;
      float num5 = vertex3.y - vertex1.y;
      float num6 = vertex2.z - vertex1.z;
      float num7 = vertex3.z - vertex1.z;
      float num8 = vector2_2.x - vector2_1.x;
      float num9 = vector2_3.x - vector2_1.x;
      float num10 = vector2_2.y - vector2_1.y;
      float num11 = vector2_3.y - vector2_1.y;
      float num12 = (float) (1.0 / (num8 * (double) num11 - num9 * (double) num10));
      Vector3 vector3_1 = new Vector3((float) (num11 * (double) num2 - num10 * (double) num3) * num12, (float) (num11 * (double) num4 - num10 * (double) num5) * num12, (float) (num11 * (double) num6 - num10 * (double) num7) * num12);
      Vector3 vector3_2 = new Vector3((float) (num8 * (double) num3 - num9 * (double) num2) * num12, (float) (num8 * (double) num5 - num9 * (double) num4) * num12, (float) (num8 * (double) num7 - num9 * (double) num6) * num12);
      vector3Array1[triangle1] += vector3_1;
      vector3Array1[triangle2] += vector3_1;
      vector3Array1[triangle3] += vector3_1;
      vector3Array2[triangle1] += vector3_2;
      vector3Array2[triangle2] += vector3_2;
      vector3Array2[triangle3] += vector3_2;
    }
    for (long index = 0; index < length; ++index)
    {
      Vector3 normal = mesh.normals[index];
      Vector3 rhs = vector3Array1[index];
      Vector3 normalized = (rhs - normal * Vector3.Dot(normal, rhs)).normalized;
      vector4Array[index] = new Vector4(normalized.x, normalized.y, normalized.z);
      vector4Array[index].w = Vector3.Dot(Vector3.Cross(normal, rhs), vector3Array2[index]) < 0.0 ? -1f : 1f;
    }
    mesh.tangents = vector4Array;
  }
}
