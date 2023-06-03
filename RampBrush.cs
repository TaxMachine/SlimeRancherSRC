// Decompiled with JetBrains decompiler
// Type: RampBrush
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Terrain/Ramp Brush")]
public class RampBrush : MonoBehaviour
{
  private bool VERBOSE;
  public bool brushOn;
  public bool turnBrushOnVar;
  public bool isBrushHidden;
  public Vector3 brushPosition;
  public Vector3 beginRamp;
  public Vector3 endRamp;
  public float brushSize = 50f;
  public float brushOpacity = 1f;
  public float brushSoftness = 0.5f;
  public float brushSampleDensity = 4f;
  public bool shiftProcessed = true;
  public Vector3 backupVector;
  public int numSubDivPerSeg = 10;
  public float spacingJitter;
  public float sizeJitter;
  public bool multiPoint;
  public List<Vector3> controlPoints = new List<Vector3>();
  private List<float> _distBetweenPoints = new List<float>();
  private float _totalLength;
  private float _totalLengthPixels;

  public void OnDrawGizmos()
  {
    if (!turnBrushOnVar || GetComponent(typeof (Terrain)) == null)
      return;
    Gizmos.color = Color.cyan;
    float num1 = brushSize / 4f;
    Gizmos.DrawLine(brushPosition + new Vector3(-num1, 0.0f, 0.0f), brushPosition + new Vector3(num1, 0.0f, 0.0f));
    Gizmos.DrawLine(brushPosition + new Vector3(0.0f, -num1, 0.0f), brushPosition + new Vector3(0.0f, num1, 0.0f));
    Gizmos.DrawLine(brushPosition + new Vector3(0.0f, 0.0f, -num1), brushPosition + new Vector3(0.0f, 0.0f, num1));
    Gizmos.DrawWireCube(brushPosition, new Vector3(brushSize, 0.0f, brushSize));
    Gizmos.DrawWireSphere(brushPosition, brushSize / 2f);
    if (!multiPoint)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(beginRamp, brushSize / 2f);
    }
    else
    {
      Gizmos.color = Color.magenta;
      for (int index = 0; index < controlPoints.Count; ++index)
        Gizmos.DrawWireSphere(controlPoints[index], brushSize / 2f);
      if (controlPoints.Count <= 2)
        return;
      double num2 = 1.0 / ((controlPoints.Count - 1.0) * 8.0) - 1E-14;
      calculateDistBetweenPoints(controlPoints);
      Ray ray1 = parameterizedLine(0.0f, controlPoints);
      double t = num2;
      for (int index = 0; t <= 1.0 && index < 1000; ++index)
      {
        Ray ray2 = parameterizedLine((float) t, controlPoints);
        Gizmos.DrawLine(ray1.origin, ray2.origin);
        ray1 = ray2;
        t += num2;
      }
    }
  }

  public int[] terrainCordsToBitmap(TerrainData terData, Vector3 v)
  {
    double heightmapResolution1 = terData.heightmapResolution;
    float heightmapResolution2 = terData.heightmapResolution;
    Vector3 size = terData.size;
    double x = size.x;
    int num = (int) Mathf.Floor((float) (heightmapResolution1 / x) * v.x);
    return new int[2]
    {
      (int) Mathf.Floor(heightmapResolution2 / size.z * v.z),
      num
    };
  }

  public float[] bitmapCordsToTerrain(TerrainData terData, int x, int y)
  {
    int heightmapResolution1 = terData.heightmapResolution;
    int heightmapResolution2 = terData.heightmapResolution;
    Vector3 size = terData.size;
    float num = x * (size.z / heightmapResolution2);
    return new float[2]
    {
      y * (size.x / heightmapResolution1),
      num
    };
  }

  public void toggleBrushOn()
  {
    if (turnBrushOnVar)
      turnBrushOnVar = false;
    else
      turnBrushOnVar = true;
  }

  public void rampBrush()
  {
    Terrain component = (Terrain) GetComponent(typeof (Terrain));
    if (component == null)
    {
      Debug.LogError("No terrain component on this GameObject");
    }
    else
    {
      try
      {
        TerrainData terrainData = component.terrainData;
        int heightmapResolution1 = terrainData.heightmapResolution;
        int heightmapResolution2 = terrainData.heightmapResolution;
        Vector3 size = terrainData.size;
        if (VERBOSE)
          Debug.Log("terrainData heightmapHeight/heightmapWidth:" + heightmapResolution1 + " " + heightmapResolution1);
        if (VERBOSE)
          Debug.Log("terrainData heightMapResolution:" + terrainData.heightmapResolution);
        if (VERBOSE)
          Debug.Log("terrainData size:" + terrainData.size);
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 v1 = transform.InverseTransformPoint(beginRamp);
        Vector3 v2 = transform.InverseTransformPoint(endRamp);
        transform.localScale = localScale;
        int length1 = (int) Mathf.Floor(heightmapResolution1 / size.z * brushSize);
        int length2 = (int) Mathf.Floor(heightmapResolution2 / size.x * brushSize);
        int[] bitmap1 = terrainCordsToBitmap(terrainData, v1);
        int[] bitmap2 = terrainCordsToBitmap(terrainData, v2);
        if (bitmap1[0] < 0 || bitmap2[0] < 0 || bitmap1[1] < 0 || bitmap2[1] < 0 || bitmap1[0] >= heightmapResolution1 || bitmap2[0] >= heightmapResolution1 || bitmap1[1] >= heightmapResolution2 || bitmap2[1] >= heightmapResolution2)
        {
          Debug.LogError("The start point or the end point was out of bounds. Make sure the gizmo is over the terrain before setting the start and end points.Note: that sometimes Unity does not update the collider after changing settings in the 'Set Resolution' dialog. Entering play mode should reset the collider.");
        }
        else
        {
          double num1 = Math.Sqrt((bitmap2[0] - bitmap1[0]) * (bitmap2[0] - bitmap1[0]) + (bitmap2[1] - bitmap1[1]) * (bitmap2[1] - bitmap1[1]));
          float[,] heights = terrainData.GetHeights(0, 0, heightmapResolution1, heightmapResolution2);
          v2.y = heights[bitmap2[0], bitmap2[1]];
          v1.y = heights[bitmap1[0], bitmap1[1]];
          Vector3 lhs = v2 - v1;
          Vector3 rhs = new Vector3(-lhs.z, 0.0f, lhs.x);
          Vector3 vector3_1 = Vector3.Cross(lhs, rhs);
          vector3_1.Normalize();
          Vector3 vector3_2 = new Vector3(lhs.x, 0.0f, lhs.z);
          float num2 = brushSize >= 15.0 ? (float) (1.0 / num1) * brushSampleDensity : brushSize / 6f / lhs.magnitude;
          if (VERBOSE)
          {
            float[] terrain1 = bitmapCordsToTerrain(terrainData, bitmap1[0], bitmap1[1]);
            Debug.Log("Local Begin Pos:" + v1);
            Debug.Log("pixel begin coord:" + bitmap1[0] + " " + bitmap1[0]);
            Debug.Log("Local begin Pos Rev Transformed:" + terrain1[0] + " " + terrain1[1]);
            float[] terrain2 = bitmapCordsToTerrain(terrainData, bitmap2[0], bitmap2[1]);
            Debug.Log("Local End Pos:" + v2);
            Debug.Log("pixel End coord:" + bitmap2[0] + " " + bitmap2[1]);
            Debug.Log("Local End Pos Rev Transformed:" + terrain2[0] + " " + terrain2[1]);
            Debug.Log("Sample Width/height: " + length1 + " " + length2);
            Debug.Log("Brush Width: " + num2);
          }
          for (float num3 = 0.0f; num3 <= 1.0; num3 += num2)
          {
            Vector3 v3 = v1 + num3 * lhs;
            int[] bitmap3 = terrainCordsToBitmap(terrainData, v3);
            int num4 = bitmap3[0] - length1 / 2;
            int num5 = bitmap3[1] - length2 / 2;
            float[,] numArray1 = new float[length1, length2];
            for (int index1 = 0; index1 < length1; ++index1)
            {
              for (int index2 = 0; index2 < length2; ++index2)
                numArray1[index1, index2] = num4 + index1 < 0 || num5 + index2 < 0 || num4 + index1 >= heightmapResolution1 || num5 + index2 >= heightmapResolution2 ? 0.0f : heights[num4 + index1, num5 + index2];
            }
            length1 = numArray1.GetLength(0);
            length2 = numArray1.GetLength(1);
            float[,] numArray2 = (float[,]) numArray1.Clone();
            for (int index3 = 0; index3 < length1; ++index3)
            {
              for (int index4 = 0; index4 < length2; ++index4)
              {
                float[] terrain = bitmapCordsToTerrain(terrainData, num4 + index3, num5 + index4);
                bool flag = false;
                if (vector3_2.x * (terrain[0] - (double) v1.x) + vector3_2.z * (terrain[1] - (double) v1.z) < 0.0)
                  flag = true;
                else if (-(double) vector3_2.x * (terrain[0] - (double) v2.x) - vector3_2.z * (terrain[1] - (double) v2.z) < 0.0)
                  flag = true;
                if (!flag)
                  numArray2[index3, index4] = v1.y - (float) (vector3_1.x * (terrain[0] - (double) v1.x) + vector3_1.z * (terrain[1] - (double) v1.z)) / vector3_1.y;
              }
            }
            float num6 = length1 / 2f;
            for (int x = 0; x < length1; ++x)
            {
              for (int y = 0; y < length2; ++y)
              {
                double num7 = numArray2[x, y];
                float num8 = numArray1[x, y];
                float num9 = (float) (1.0 - (Vector2.Distance(new Vector2(x, y), new Vector2(num6, num6)) - (num6 - num6 * (double) brushSoftness)) / (num6 * (double) brushSoftness));
                if (num9 < 0.0)
                  num9 = 0.0f;
                else if (num9 > 1.0)
                  num9 = 1f;
                float num10 = num9 * brushOpacity;
                double num11 = num10;
                float num12 = (float) (num7 * num11 + num8 * (1.0 - num10));
                numArray1[x, y] = num12;
              }
            }
            for (int index5 = 0; index5 < length1; ++index5)
            {
              for (int index6 = 0; index6 < length2; ++index6)
              {
                if (num4 + index5 >= 0 && num5 + index6 >= 0 && num4 + index5 < heightmapResolution1 && num5 + index6 < heightmapResolution2)
                  heights[num4 + index5, num5 + index6] = numArray1[index5, index6];
              }
            }
          }
          terrainData.SetHeights(0, 0, heights);
        }
      }
      catch (Exception ex)
      {
        Debug.LogError("A brush error occurred: " + ex);
      }
    }
  }

  public void StrokePath() => _StrokePath();

  public void _StrokePath()
  {
    Terrain component = (Terrain) GetComponent(typeof (Terrain));
    if (component == null)
    {
      Debug.LogError("No terrain component on this GameObject");
    }
    else
    {
      try
      {
        TerrainData terrainData = component.terrainData;
        int heightmapResolution1 = terrainData.heightmapResolution;
        int heightmapResolution2 = terrainData.heightmapResolution;
        Vector3 size = terrainData.size;
        if (VERBOSE)
          Debug.Log("terrainData heightmapHeight/heightmapWidth:" + heightmapResolution1 + " " + heightmapResolution1);
        if (VERBOSE)
          Debug.Log("terrainData heightMapResolution:" + terrainData.heightmapResolution);
        if (VERBOSE)
          Debug.Log("terrainData size:" + terrainData.size);
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(1f, 1f, 1f);
        List<Vector3> cps = new List<Vector3>();
        for (int index = 0; index < controlPoints.Count; ++index)
          cps.Add(transform.InverseTransformPoint(controlPoints[index]));
        transform.localScale = localScale;
        for (int index = 0; index < cps.Count; ++index)
        {
          int[] bitmap = terrainCordsToBitmap(terrainData, cps[index]);
          if (bitmap[0] < 0 || bitmap[1] < 0 || bitmap[0] >= heightmapResolution1 || bitmap[1] >= heightmapResolution2)
          {
            Debug.LogError("The start point or the end point was out of bounds. Make sure the gizmo is over the terrain before setting the start and end points.Note: that sometimes Unity does not update the collider after changing settings in the 'Set Resolution' dialog. Entering play mode should reset the collider.");
            return;
          }
        }
        int length1 = (int) Mathf.Floor(heightmapResolution1 / size.z * brushSize);
        int length2 = (int) Mathf.Floor(heightmapResolution2 / size.x * brushSize);
        float[,] heights = terrainData.GetHeights(0, 0, heightmapResolution1, heightmapResolution2);
        for (int index = 0; index < cps.Count; ++index)
        {
          int[] bitmap = terrainCordsToBitmap(terrainData, cps[index]);
          Vector3 vector3 = cps[index] with
          {
            y = heights[bitmap[0], bitmap[1]]
          };
          cps[index] = vector3;
        }
        calculateDistBetweenPoints(cps);
        calculateDistBetweenPointsInPixels(cps, terrainData);
        float num1 = brushSampleDensity / _totalLengthPixels;
        float num2 = brushSize / _totalLengthPixels;
        Debug.Log("Sample w " + length1 + " h " + length2);
        Debug.Log("parameterized brush width " + num2);
        if (num2 > 0.5)
          num2 = 0.5f;
        if (VERBOSE)
        {
          for (int index = 0; index < cps.Count; ++index)
          {
            int[] bitmap = terrainCordsToBitmap(terrainData, cps[index]);
            float[] terrain = bitmapCordsToTerrain(terrainData, bitmap[0], bitmap[1]);
            Debug.Log(index.ToString() + " Local control Pos:" + cps[index]);
            Debug.Log(index.ToString() + " pixel begin coord:" + bitmap[0] + " " + bitmap[0]);
            Debug.Log(index.ToString() + " Local begin Pos Rev Transformed:" + terrain[0] + " " + terrain[1]);
          }
          Debug.Log("parameterized brush width " + num2);
        }
        StringBuilder message = new StringBuilder();
        for (float t = 0.0f; t <= 1.0; t += num1)
        {
          Ray ray1 = parameterizedLine(t, cps);
          Vector3 inNormal = Vector3.Cross(new Vector3(-ray1.direction.z, 0.0f, ray1.direction.x), ray1.direction);
          inNormal.Normalize();
          if (spacingJitter > 0.0)
          {
            float f = 6.28318548f * UnityEngine.Random.value;
            float num3 = UnityEngine.Random.value + UnityEngine.Random.value;
            float num4 = (num3 <= 1.0 ? num3 : 2f - num3) * (spacingJitter * brushSize);
            Vector3 vector3_1 = new Vector3(num4 * Mathf.Cos(f), 0.0f, num4 * Mathf.Sin(f));
            if (VERBOSE)
              Debug.Log("jittering by " + vector3_1 + " dir " + ray1.direction + " n " + inNormal);
            Plane plane = new Plane(inNormal, ray1.origin);
            Ray ray2 = new Ray(ray1.origin + vector3_1, Vector3.up);
            float enter;
            if (plane.Raycast(ray2, out enter))
            {
              Vector3 vector3_2 = ray2.origin + ray2.direction * enter;
              ray1.origin = vector3_2;
            }
          }
          Plane plane1 = default;
          ref Plane local1 = ref plane1;
          Vector3 vector3_3 = cps[0] - cps[1];
          Vector3 normalized1 = vector3_3.normalized;
          Vector3 inPoint1 = cps[0];
          local1 = new Plane(normalized1, inPoint1);
          Plane plane2 = default;
          ref Plane local2 = ref plane2;
          vector3_3 = cps[cps.Count - 1] - cps[cps.Count - 2];
          Vector3 normalized2 = vector3_3.normalized;
          Vector3 inPoint2 = cps[cps.Count - 1];
          local2 = new Plane(normalized2, inPoint2);
          int[] bitmap = terrainCordsToBitmap(terrainData, ray1.origin);
          int num5 = bitmap[0] - length1 / 2;
          int num6 = bitmap[1] - length2 / 2;
          float[,] numArray1 = new float[length1, length2];
          for (int index1 = 0; index1 < length1; ++index1)
          {
            for (int index2 = 0; index2 < length2; ++index2)
              numArray1[index1, index2] = num5 + index1 < 0 || num6 + index2 < 0 || num5 + index1 >= heightmapResolution1 || num6 + index2 >= heightmapResolution2 ? 0.0f : heights[num5 + index1, num6 + index2];
          }
          float[,] numArray2 = (float[,]) numArray1.Clone();
          for (int index3 = 0; index3 < length1; ++index3)
          {
            for (int index4 = 0; index4 < length2; ++index4)
            {
              float[] terrain = bitmapCordsToTerrain(terrainData, num5 + index3, num6 + index4);
              Vector3 vector3_4 = new Vector3(terrain[0], 0.0f, terrain[1]);
              bool flag = false;
              if (plane1.GetSide(vector3_4) && t < num2 / 2.0)
                flag = true;
              else if (plane2.GetSide(vector3_4) && t > 1.0 - num2 / 2.0)
                flag = true;
              if (!flag)
              {
                Plane plane3 = new Plane(inNormal, ray1.origin);
                Ray ray3 = new Ray(vector3_4, Vector3.up);
                float enter;
                if (plane3.Raycast(ray3, out enter))
                  numArray2[index3, index4] = ray3.origin.y + ray3.direction.y * enter;
              }
            }
          }
          float num7 = Mathf.Min(length2 / 2f, length1 / 2f);
          for (int x = 0; x < length1; ++x)
          {
            for (int y = 0; y < length2; ++y)
            {
              double num8 = numArray2[x, y];
              float num9 = numArray1[x, y];
              float num10 = (float) (1.0 - Vector2.Distance(new Vector2(x, y), new Vector2(num7, num7)) / (double) num7) / brushSoftness;
              if (num10 < 0.0)
                num10 = 0.0f;
              else if (num10 > 1.0)
                num10 = 1f;
              float num11 = num10 * brushOpacity;
              double num12 = num11;
              float num13 = (float) (num8 * num12 + num9 * (1.0 - num11));
              numArray1[x, y] = num13;
            }
          }
          for (int index5 = 0; index5 < length1; ++index5)
          {
            for (int index6 = 0; index6 < length2; ++index6)
            {
              if (num5 + index5 >= 0 && num6 + index6 >= 0 && num5 + index5 < heightmapResolution1 && num6 + index6 < heightmapResolution2)
                heights[num5 + index5, num6 + index6] = numArray1[index5, index6];
            }
          }
        }
        Debug.Log(message);
        terrainData.SetHeights(0, 0, heights);
      }
      catch (Exception ex)
      {
        Debug.LogError("A brush error occurred: " + ex);
      }
    }
  }

  private void calculateDistBetweenPoints(List<Vector3> cps)
  {
    _distBetweenPoints.Clear();
    _totalLength = 0.0f;
    for (int index = 1; index < cps.Count; ++index)
    {
      _distBetweenPoints.Add((cps[index] - cps[index - 1]).magnitude);
      _totalLength += _distBetweenPoints[_distBetweenPoints.Count - 1];
    }
  }

  private void calculateDistBetweenPointsInPixels(List<Vector3> cps, TerrainData terData)
  {
    _totalLengthPixels = 0.0f;
    int[] numArray = terrainCordsToBitmap(terData, cps[0]);
    for (int index = 1; index < cps.Count; ++index)
    {
      int[] bitmap = terrainCordsToBitmap(terData, cps[index]);
      _totalLengthPixels += Mathf.Sqrt((bitmap[0] - numArray[0]) * (bitmap[0] - numArray[0]) + (bitmap[1] - numArray[1]) * (bitmap[1] - numArray[1]));
      numArray = bitmap;
    }
  }

  private Ray parameterizedLine(float t, List<Vector3> cps, StringBuilder sb = null)
  {
    if (cps.Count < 2)
    {
      Debug.LogError("Less than two control points.");
      return new Ray();
    }
    if (t < 0.0)
      t = 0.0f;
    if (t >= 1.0)
      t = 1f;
    Vector3[] vector3Array = new Vector3[cps.Count + 2];
    for (int index = 0; index < cps.Count; ++index)
      vector3Array[index + 1] = cps[index];
    vector3Array[0] = 2f * cps[0] - cps[1];
    vector3Array[vector3Array.Length - 1] = 2f * cps[cps.Count - 1] - cps[cps.Count - 2];
    float num1 = t * _totalLength;
    int index1 = 0;
    float num2 = 0.0f;
    bool flag = false;
    float num3 = 0.0f;
    while (!flag)
    {
      if (num3 + (double) _distBetweenPoints[index1] < num1)
      {
        num3 += _distBetweenPoints[index1];
        if (index1 < controlPoints.Count - 2)
        {
          ++index1;
        }
        else
        {
          flag = true;
          num2 = 1f;
        }
      }
      else
      {
        flag = true;
        num2 = (num1 - num3) / _distBetweenPoints[index1];
      }
    }
    if (index1 >= controlPoints.Count - 1)
      --index1;
    if (num2 > 1.0)
      num2 = 1f;
    int index2 = index1 + 1;
    if (index2 + 2 > vector3Array.Length - 1)
      Debug.LogError("Off end=" + t);
    sb?.AppendFormat("t={0} cpIdx={1} nt={2}\n", t, index2, num2);
    Vector3 vector3_1 = vector3Array[index2 - 1];
    Vector3 vector3_2 = vector3Array[index2];
    Vector3 vector3_3 = vector3Array[index2 + 1];
    Vector3 vector3_4 = vector3Array[index2 + 2];
    float num4 = num2 * num2;
    float num5 = num2 * num2 * num2;
    return new Ray(0.5f * (2f * vector3_2 + (-vector3_1 + vector3_3) * num2 + (2f * vector3_1 - 5f * vector3_2 + 4f * vector3_3 - vector3_4) * num4 + (-vector3_1 + 3f * vector3_2 - 3f * vector3_3 + vector3_4) * num5), (0.5f * (-vector3_1 + vector3_3 + num2 * (4f * vector3_1 - 10f * vector3_2 + 8f * vector3_3 - 2f * vector3_4) + num4 * (-3f * vector3_1 + 9f * vector3_2 - 9f * vector3_3 + 3f * vector3_4))).normalized);
  }
}
