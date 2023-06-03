// Decompiled with JetBrains decompiler
// Type: SlicedVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SlicedVolume : MonoBehaviour
{
  public Material cloudMaterial;
  public Material shadowCasterMat;
  public int sliceAmount = 25;
  public int segmentCount = 3;
  public Vector3 dimensions = new Vector3(1000f, 50f, 1000f);
  public Vector3 normalDirection = new Vector3(1f, 1f, 1f);
  public bool shadowCaster;
  public bool transferVariables = true;
  public bool unityFive;
  public bool curved;
  public float roundness = 2f;
  public float intensity = 1f / 1000f;
  public bool generateNewSlices;
  private bool updateCloudDirection = true;
  private int cameraCloudRelation = 1;
  private Color[] vertexColor;
  private Vector3[] vertices;
  private Vector2[] uvMap;
  private int[] triangleConstructor;
  private Vector3 posGainPerVertices;
  private float posGainPerUV;
  private GameObject meshSlices;
  private GameObject meshShadowCaster;

  private void OnDrawGizmos()
  {
    editorUpdate();
    if (!generateNewSlices)
      return;
    if ((bool) (Object) cloudMaterial)
    {
      integrityCheck();
      settingValuesUp(false);
      if (shadowCaster)
      {
        int sliceAmount = this.sliceAmount;
        this.sliceAmount = 1;
        settingValuesUp(true);
        this.sliceAmount = sliceAmount;
      }
    }
    generateNewSlices = false;
  }

  private void syncCloudAndShadowCaster() => shadowCasterMat.CopyPropertiesFromMaterial(cloudMaterial);

  private void editorUpdate()
  {
    sliceAmount = sliceAmount <= 1 ? 1 : sliceAmount;
    segmentCount = segmentCount > 2 ? segmentCount : 2;
    if (Camera.current.name != "PreRenderCamera" && (bool) (Object) cloudMaterial && !curved && (bool) (Object) meshSlices)
    {
      if (Camera.current.transform.position.y > (double) transform.position.y && cameraCloudRelation == -1)
      {
        cameraCloudRelation = 1;
        updateCloudDirection = true;
      }
      else if (Camera.current.transform.position.y < (double) transform.position.y && cameraCloudRelation == 1)
      {
        cameraCloudRelation = -1;
        updateCloudDirection = true;
      }
      if (updateCloudDirection)
      {
        meshSlices.transform.localScale = new Vector3(Mathf.Abs(meshSlices.transform.localScale.x), Mathf.Abs(meshSlices.transform.localScale.y) * cameraCloudRelation, Mathf.Abs(meshSlices.transform.localScale.z));
        cloudMaterial.SetVector("_CloudNormalsDirection", new Vector4(normalDirection.x, normalDirection.y * cameraCloudRelation, normalDirection.z * -1f, 0.0f));
        updateCloudDirection = false;
      }
    }
    else if (curved && (bool) (Object) cloudMaterial && (bool) (Object) meshSlices)
    {
      meshSlices.transform.localScale = new Vector3(Mathf.Abs(meshSlices.transform.localScale.x), Mathf.Abs(meshSlices.transform.localScale.y) * -1f, Mathf.Abs(meshSlices.transform.localScale.z));
      if ((bool) (Object) meshShadowCaster)
        meshShadowCaster.transform.localScale = new Vector3(Mathf.Abs(meshSlices.transform.localScale.x), Mathf.Abs(meshSlices.transform.localScale.y) * -1f, Mathf.Abs(meshSlices.transform.localScale.z));
      cloudMaterial.SetVector("_CloudNormalsDirection", new Vector4(normalDirection.x, normalDirection.y * -1f, normalDirection.z * -1f, 0.0f));
    }
    if (!transferVariables || !(bool) (Object) cloudMaterial || !(bool) (Object) shadowCasterMat)
      return;
    syncCloudAndShadowCaster();
  }

  private void integrityCheck()
  {
    if (!(bool) (Object) meshSlices)
    {
      foreach (Transform transform in this.transform)
      {
        if (transform.name == "Clouds")
          meshSlices = transform.gameObject;
      }
      if (!(bool) (Object) meshSlices)
      {
        meshSlices = new GameObject("Clouds");
        meshSlices.transform.parent = transform;
        meshSlices.transform.localPosition = Vector3.zero;
        meshSlices.AddComponent<MeshFilter>();
        meshSlices.AddComponent<MeshRenderer>();
        meshSlices.GetComponent<Renderer>().material = cloudMaterial;
      }
    }
    if (shadowCaster && !(bool) (Object) meshShadowCaster)
    {
      foreach (Transform transform in this.transform)
      {
        if (transform.name == "Shadow Caster")
          meshShadowCaster = transform.gameObject;
      }
      if (!(bool) (Object) meshShadowCaster)
      {
        meshShadowCaster = new GameObject("Shadow Caster");
        meshShadowCaster.transform.parent = transform;
        meshShadowCaster.transform.localPosition = Vector3.zero;
        meshShadowCaster.AddComponent<MeshFilter>();
        meshShadowCaster.AddComponent<MeshRenderer>();
        meshShadowCaster.GetComponent<Renderer>().material = shadowCasterMat;
      }
    }
    if (!shadowCaster)
    {
      if ((bool) (Object) meshShadowCaster)
      {
        DestroyImmediate(meshShadowCaster);
      }
      else
      {
        foreach (Transform transform in this.transform)
        {
          if (transform.name == "Shadow Caster")
            DestroyImmediate(transform.gameObject);
        }
      }
    }
    if (shadowCaster)
      meshShadowCaster.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
    meshSlices.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
    if (shadowCaster)
      meshShadowCaster.GetComponent<MeshRenderer>().receiveShadows = false;
    meshSlices.GetComponent<MeshRenderer>().receiveShadows = false;
  }

  private void settingValuesUp(bool isShadowCaster)
  {
    vertices = new Vector3[segmentCount * segmentCount * sliceAmount];
    uvMap = new Vector2[vertices.Length];
    triangleConstructor = new int[(segmentCount - 1) * (segmentCount - 1) * sliceAmount * 2 * 3];
    vertexColor = new Color[vertices.Length];
    float num = (float) (1.0 / (segmentCount - 1.0));
    posGainPerVertices = new Vector3(num * dimensions.x, 1f / Mathf.Clamp(sliceAmount - 1, 1, 999999) * dimensions.y, num * dimensions.z);
    posGainPerUV = num;
    trianglesCreation(isShadowCaster);
  }

  private void trianglesCreation(bool isShadowCaster)
  {
    int num1 = 0;
    int num2 = 0;
    int index1 = 0;
    float f = 0.0f;
    for (int index2 = 0; index2 < sliceAmount; ++index2)
    {
      float num3 = (float) (index2 * (2.0 / sliceAmount) - 1.0);
      float w = index2 >= sliceAmount * 0.5 ? (float) (2.0 - 1.0 / (sliceAmount * 0.5) * (index2 + 1)) : (float) (0.0 + 1.0 / (sliceAmount * 0.5) * index2);
      if (sliceAmount == 1)
        w = 1f;
      for (int index3 = 0; index3 < segmentCount; ++index3)
      {
        int num4 = segmentCount * num1;
        for (int index4 = 0; index4 < segmentCount; ++index4)
        {
          if (curved)
            f = Vector3.Distance(new Vector3((float) (posGainPerVertices.x * (double) index4 - dimensions.x / 2.0), 0.0f, (float) (posGainPerVertices.z * (double) index3 - dimensions.z / 2.0)), Vector3.zero);
          vertices[index4 + num4] = sliceAmount != 1 ? new Vector3((float) (posGainPerVertices.x * (double) index4 - dimensions.x / 2.0), (float) (posGainPerVertices.y * (double) index2 - dimensions.y / 2.0 + Mathf.Pow(f, roundness) * (double) intensity), (float) (posGainPerVertices.z * (double) index3 - dimensions.z / 2.0)) : new Vector3((float) (posGainPerVertices.x * (double) index4 - dimensions.x / 2.0), (float) (0.0 + Mathf.Pow(f, roundness) * (double) intensity), (float) (posGainPerVertices.z * (double) index3 - dimensions.z / 2.0));
          uvMap[index4 + num4] = new Vector2(posGainPerUV * index4, posGainPerUV * index3);
          vertexColor[index4 + num4] = new Vector4(num3, num3, num3, w);
        }
        ++num1;
        if (index3 >= 1)
        {
          for (int index5 = 0; index5 < segmentCount - 1; ++index5)
          {
            triangleConstructor[index1] = index5 + num2 + index2 * segmentCount;
            triangleConstructor[1 + index1] = segmentCount + index5 + num2 + index2 * segmentCount;
            triangleConstructor[2 + index1] = 1 + index5 + num2 + index2 * segmentCount;
            triangleConstructor[3 + index1] = segmentCount + 1 + index5 + num2 + index2 * segmentCount;
            triangleConstructor[4 + index1] = 1 + index5 + num2 + index2 * segmentCount;
            triangleConstructor[5 + index1] = segmentCount + index5 + num2 + index2 * segmentCount;
            index1 += 6;
          }
          num2 += segmentCount;
        }
      }
    }
    if (!isShadowCaster)
    {
      Mesh mesh = new Mesh();
      mesh.Clear();
      mesh.name = "GeoSlices";
      mesh.vertices = vertices;
      mesh.triangles = triangleConstructor;
      mesh.uv = uvMap;
      mesh.colors = vertexColor;
      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      calculateMeshTangents(mesh);
      meshSlices.GetComponent<MeshFilter>().mesh = mesh;
    }
    else
    {
      Mesh mesh = new Mesh();
      mesh.Clear();
      mesh.name = "GeoSlices";
      mesh.vertices = vertices;
      mesh.triangles = triangleConstructor;
      mesh.uv = uvMap;
      mesh.colors = vertexColor;
      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      calculateMeshTangents(mesh);
      meshShadowCaster.GetComponent<MeshFilter>().mesh = mesh;
    }
  }

  public static void calculateMeshTangents(Mesh mesh)
  {
    int[] triangles = mesh.triangles;
    Vector3[] vertices = mesh.vertices;
    Vector2[] uv = mesh.uv;
    Vector3[] normals = mesh.normals;
    int length1 = triangles.Length;
    int length2 = vertices.Length;
    Vector3[] vector3Array1 = new Vector3[length2];
    Vector3[] vector3Array2 = new Vector3[length2];
    Vector4[] vector4Array = new Vector4[length2];
    for (long index1 = 0; index1 < length1; index1 += 3L)
    {
      long index2 = triangles[index1];
      long index3 = triangles[index1 + 1L];
      long index4 = triangles[index1 + 2L];
      Vector3 vector3_1 = vertices[index2];
      Vector3 vector3_2 = vertices[index3];
      Vector3 vector3_3 = vertices[index4];
      Vector2 vector2_1 = uv[index2];
      Vector2 vector2_2 = uv[index3];
      Vector2 vector2_3 = uv[index4];
      float num1 = vector3_2.x - vector3_1.x;
      float num2 = vector3_3.x - vector3_1.x;
      float num3 = vector3_2.y - vector3_1.y;
      float num4 = vector3_3.y - vector3_1.y;
      float num5 = vector3_2.z - vector3_1.z;
      float num6 = vector3_3.z - vector3_1.z;
      float num7 = vector2_2.x - vector2_1.x;
      float num8 = vector2_3.x - vector2_1.x;
      float num9 = vector2_2.y - vector2_1.y;
      float num10 = vector2_3.y - vector2_1.y;
      float num11 = (float) (1.0 / (num7 * (double) num10 - num8 * (double) num9));
      Vector3 vector3_4 = new Vector3((float) (num10 * (double) num1 - num9 * (double) num2) * num11, (float) (num10 * (double) num3 - num9 * (double) num4) * num11, (float) (num10 * (double) num5 - num9 * (double) num6) * num11);
      Vector3 vector3_5 = new Vector3((float) (num7 * (double) num2 - num8 * (double) num1) * num11, (float) (num7 * (double) num4 - num8 * (double) num3) * num11, (float) (num7 * (double) num6 - num8 * (double) num5) * num11);
      vector3Array1[index2] += vector3_4;
      vector3Array1[index3] += vector3_4;
      vector3Array1[index4] += vector3_4;
      vector3Array2[index2] += vector3_5;
      vector3Array2[index3] += vector3_5;
      vector3Array2[index4] += vector3_5;
    }
    for (long index = 0; index < length2; ++index)
    {
      Vector3 normal = normals[index];
      Vector3 tangent = vector3Array1[index];
      Vector3.OrthoNormalize(ref normal, ref tangent);
      vector4Array[index].x = tangent.x;
      vector4Array[index].y = tangent.y;
      vector4Array[index].z = tangent.z;
      vector4Array[index].w = Vector3.Dot(Vector3.Cross(normal, tangent), vector3Array2[index]) < 0.0 ? -1f : 1f;
    }
    mesh.tangents = vector4Array;
  }
}
