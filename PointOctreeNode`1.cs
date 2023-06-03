// Decompiled with JetBrains decompiler
// Type: PointOctreeNode`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PointOctreeNode<T> where T : class
{
  private float minSize;
  private Bounds bounds;
  private readonly List<OctreeObject> objects = new List<OctreeObject>();
  private PointOctreeNode<T>[] children;
  private Bounds[] childBounds;
  private const int NUM_OBJECTS_ALLOWED = 8;
  private Vector3 actualBoundsSize;

  public Vector3 Center { get; private set; }

  public float SideLength { get; private set; }

  public PointOctreeNode(float baseLengthVal, float minSizeVal, Vector3 centerVal) => SetValues(baseLengthVal, minSizeVal, centerVal);

  public bool Add(T obj, Vector3 objPos)
  {
    if (!Encapsulates(bounds, objPos))
      return false;
    SubAdd(obj, objPos);
    return true;
  }

  public bool Remove(T obj)
  {
    bool flag = false;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Obj.Equals(obj))
      {
        flag = objects.Remove(objects[index]);
        break;
      }
    }
    if (!flag && children != null)
    {
      for (int index = 0; index < 8; ++index)
      {
        flag = children[index].Remove(obj);
        if (flag)
          break;
      }
    }
    if (flag && children != null && ShouldMerge())
      Merge();
    return flag;
  }

  public T[] GetNearby(Ray ray, float maxDistance)
  {
    bounds.Expand(new Vector3(maxDistance, maxDistance, maxDistance));
    int num = bounds.IntersectRay(ray) ? 1 : 0;
    bounds.size = actualBoundsSize;
    if (num == 0)
      return new T[0];
    List<T> objList = new List<T>();
    for (int index = 0; index < objects.Count; ++index)
    {
      if (DistanceToRay(ray, objects[index].Pos) <= (double) maxDistance)
        objList.Add(objects[index].Obj);
    }
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
      {
        T[] nearby = children[index].GetNearby(ray, maxDistance);
        if (nearby != null)
          objList.AddRange(nearby);
      }
    }
    return objList.ToArray();
  }

  public void SetChildren(PointOctreeNode<T>[] childOctrees)
  {
    if (childOctrees.Length != 8)
      Log.Error("Child octree array must be length 8. Was length: " + childOctrees.Length);
    else
      children = childOctrees;
  }

  public void DrawAllBounds(float depth = 0.0f)
  {
    float r = depth / 7f;
    Gizmos.color = new Color(r, 0.0f, 1f - r);
    Bounds bounds = new Bounds(Center, new Vector3(SideLength, SideLength, SideLength));
    Gizmos.DrawWireCube(bounds.center, bounds.size);
    if (children != null)
    {
      ++depth;
      for (int index = 0; index < 8; ++index)
        children[index].DrawAllBounds(depth);
    }
    Gizmos.color = Color.white;
  }

  public void DrawAllObjects()
  {
    float b = SideLength / 20f;
    Gizmos.color = new Color(0.0f, 1f - b, b, 0.25f);
    foreach (OctreeObject octreeObject in objects)
      Gizmos.DrawIcon(octreeObject.Pos, "marker.tif", true);
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
        children[index].DrawAllObjects();
    }
    Gizmos.color = Color.white;
  }

  public PointOctreeNode<T> ShrinkIfPossible(float minLength)
  {
    if (SideLength < 2.0 * minLength || objects.Count == 0 && children.Length == 0)
      return this;
    int index1 = -1;
    for (int index2 = 0; index2 < objects.Count; ++index2)
    {
      int num = BestFitChild(objects[index2].Pos);
      if (index2 != 0 && num != index1)
        return this;
      if (index1 < 0)
        index1 = num;
    }
    if (children != null)
    {
      bool flag = false;
      for (int index3 = 0; index3 < children.Length; ++index3)
      {
        if (children[index3].HasAnyObjects())
        {
          if (flag || index1 >= 0 && index1 != index3)
            return this;
          flag = true;
          index1 = index3;
        }
      }
    }
    if (children != null)
      return children[index1];
    SetValues(SideLength / 2f, minSize, childBounds[index1].center);
    return this;
  }

  private void SetValues(float baseLengthVal, float minSizeVal, Vector3 centerVal)
  {
    SideLength = baseLengthVal;
    minSize = minSizeVal;
    Center = centerVal;
    actualBoundsSize = new Vector3(SideLength, SideLength, SideLength);
    bounds = new Bounds(Center, actualBoundsSize);
    float num1 = SideLength / 4f;
    float num2 = SideLength / 2f;
    Vector3 size = new Vector3(num2, num2, num2);
    childBounds = new Bounds[8];
    childBounds[0] = new Bounds(Center + new Vector3(-num1, num1, -num1), size);
    childBounds[1] = new Bounds(Center + new Vector3(num1, num1, -num1), size);
    childBounds[2] = new Bounds(Center + new Vector3(-num1, num1, num1), size);
    childBounds[3] = new Bounds(Center + new Vector3(num1, num1, num1), size);
    childBounds[4] = new Bounds(Center + new Vector3(-num1, -num1, -num1), size);
    childBounds[5] = new Bounds(Center + new Vector3(num1, -num1, -num1), size);
    childBounds[6] = new Bounds(Center + new Vector3(-num1, -num1, num1), size);
    childBounds[7] = new Bounds(Center + new Vector3(num1, -num1, num1), size);
  }

  private void SubAdd(T obj, Vector3 objPos)
  {
    if (objects.Count < 8 || SideLength / 2.0 < minSize)
    {
      objects.Add(new OctreeObject()
      {
        Obj = obj,
        Pos = objPos
      });
    }
    else
    {
      if (children == null)
      {
        Split();
        if (children == null)
        {
          Debug.Log("Child creation failed for an unknown reason. Early exit.");
          return;
        }
        for (int index = objects.Count - 1; index >= 0; --index)
        {
          OctreeObject octreeObject = objects[index];
          children[BestFitChild(octreeObject.Pos)].SubAdd(octreeObject.Obj, octreeObject.Pos);
          objects.Remove(octreeObject);
        }
      }
      children[BestFitChild(objPos)].SubAdd(obj, objPos);
    }
  }

  private void Split()
  {
    float num = SideLength / 4f;
    float baseLengthVal = SideLength / 2f;
    children = new PointOctreeNode<T>[8];
    children[0] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(-num, num, -num));
    children[1] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(num, num, -num));
    children[2] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(-num, num, num));
    children[3] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(num, num, num));
    children[4] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(-num, -num, -num));
    children[5] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(num, -num, -num));
    children[6] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(-num, -num, num));
    children[7] = new PointOctreeNode<T>(baseLengthVal, minSize, Center + new Vector3(num, -num, num));
  }

  private void Merge()
  {
    for (int index1 = 0; index1 < 8; ++index1)
    {
      PointOctreeNode<T> child = children[index1];
      for (int index2 = child.objects.Count - 1; index2 >= 0; --index2)
        objects.Add(child.objects[index2]);
    }
    children = null;
  }

  private static bool Encapsulates(Bounds outerBounds, Vector3 point) => outerBounds.Contains(point);

  private int BestFitChild(Vector3 objPos) => (objPos.x <= (double) Center.x ? 0 : 1) + (objPos.y >= (double) Center.y ? 0 : 4) + (objPos.z <= (double) Center.z ? 0 : 2);

  private bool ShouldMerge()
  {
    int count = objects.Count;
    if (children != null)
    {
      foreach (PointOctreeNode<T> child in children)
      {
        if (child.children != null)
          return false;
        count += child.objects.Count;
      }
    }
    return count <= 8;
  }

  private bool HasAnyObjects()
  {
    if (objects.Count > 0)
      return true;
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
      {
        if (children[index].HasAnyObjects())
          return true;
      }
    }
    return false;
  }

  public static float DistanceToRay(Ray ray, Vector3 point) => Vector3.Cross(ray.direction, point - ray.origin).magnitude;

  private class OctreeObject
  {
    public T Obj;
    public Vector3 Pos;
  }
}
