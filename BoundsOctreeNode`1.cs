// Decompiled with JetBrains decompiler
// Type: BoundsOctreeNode`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class BoundsOctreeNode<T>
{
  private float looseness;
  private float minSize;
  private float adjLength;
  private Bounds bounds;
  private readonly List<OctreeObject> objects = new List<OctreeObject>();
  private BoundsOctreeNode<T>[] children;
  private Bounds[] childBounds;
  private const int numObjectsAllowed = 8;

  public Vector3 Center { get; private set; }

  public float BaseLength { get; private set; }

  public BoundsOctreeNode(
    float baseLengthVal,
    float minSizeVal,
    float loosenessVal,
    Vector3 centerVal)
  {
    SetValues(baseLengthVal, minSizeVal, loosenessVal, centerVal);
  }

  public bool Add(T obj, Bounds objBounds)
  {
    if (!Encapsulates(bounds, objBounds))
      return false;
    SubAdd(obj, objBounds);
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

  public bool IsColliding(Bounds checkBounds)
  {
    if (!bounds.Intersects(checkBounds))
      return false;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Bounds.Intersects(checkBounds))
        return true;
    }
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
      {
        if (children[index].IsColliding(checkBounds))
          return true;
      }
    }
    return false;
  }

  public T[] GetColliding(Bounds checkBounds)
  {
    List<T> objList = new List<T>();
    if (!bounds.Intersects(checkBounds))
      return objList.ToArray();
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Bounds.Intersects(checkBounds))
        objList.Add(objects[index].Obj);
    }
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
      {
        T[] colliding = children[index].GetColliding(checkBounds);
        if (colliding != null)
          objList.AddRange(colliding);
      }
    }
    return objList.ToArray();
  }

  public void SetChildren(BoundsOctreeNode<T>[] childOctrees)
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
    Bounds bounds = new Bounds(Center, new Vector3(adjLength, adjLength, adjLength));
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
    float b = BaseLength / 20f;
    Gizmos.color = new Color(0.0f, 1f - b, b, 0.25f);
    foreach (OctreeObject octreeObject in objects)
    {
      Gizmos.DrawCube(octreeObject.Bounds.center, octreeObject.Bounds.size);
      Gizmos.color = Color.magenta;
      Gizmos.DrawLine(octreeObject.Bounds.center, bounds.center);
    }
    if (children != null)
    {
      for (int index = 0; index < 8; ++index)
        children[index].DrawAllObjects();
    }
    Gizmos.color = Color.white;
  }

  public BoundsOctreeNode<T> ShrinkIfPossible(float minLength)
  {
    if (BaseLength < 2.0 * minLength || objects.Count == 0 && children.Length == 0)
      return this;
    int index1 = -1;
    for (int index2 = 0; index2 < objects.Count; ++index2)
    {
      OctreeObject octreeObject = objects[index2];
      int index3 = BestFitChild(octreeObject.Bounds);
      if (index2 != 0 && index3 != index1 || !Encapsulates(childBounds[index3], octreeObject.Bounds))
        return this;
      if (index1 < 0)
        index1 = index3;
    }
    if (children != null)
    {
      bool flag = false;
      for (int index4 = 0; index4 < children.Length; ++index4)
      {
        if (children[index4].HasAnyObjects())
        {
          if (flag || index1 >= 0 && index1 != index4)
            return this;
          flag = true;
          index1 = index4;
        }
      }
    }
    if (children != null)
      return children[index1];
    SetValues(BaseLength / 2f, minSize, looseness, childBounds[index1].center);
    return this;
  }

  private void SetValues(
    float baseLengthVal,
    float minSizeVal,
    float loosenessVal,
    Vector3 centerVal)
  {
    BaseLength = baseLengthVal;
    minSize = minSizeVal;
    looseness = loosenessVal;
    Center = centerVal;
    adjLength = looseness * baseLengthVal;
    bounds = new Bounds(Center, new Vector3(adjLength, adjLength, adjLength));
    float num1 = BaseLength / 4f;
    float num2 = BaseLength / 2f * looseness;
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

  private void SubAdd(T obj, Bounds objBounds)
  {
    if (objects.Count < 8 || BaseLength / 2.0 < minSize)
    {
      objects.Add(new OctreeObject()
      {
        Obj = obj,
        Bounds = objBounds
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
        for (int index1 = objects.Count - 1; index1 >= 0; --index1)
        {
          OctreeObject octreeObject = objects[index1];
          int index2 = BestFitChild(octreeObject.Bounds);
          if (Encapsulates(children[index2].bounds, octreeObject.Bounds))
          {
            children[index2].SubAdd(octreeObject.Obj, octreeObject.Bounds);
            objects.Remove(octreeObject);
          }
        }
      }
      int index = BestFitChild(objBounds);
      if (Encapsulates(children[index].bounds, objBounds))
        children[index].SubAdd(obj, objBounds);
      else
        objects.Add(new OctreeObject()
        {
          Obj = obj,
          Bounds = objBounds
        });
    }
  }

  private void Split()
  {
    float num = BaseLength / 4f;
    float baseLengthVal = BaseLength / 2f;
    children = new BoundsOctreeNode<T>[8];
    children[0] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, num, -num));
    children[1] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, num, -num));
    children[2] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, num, num));
    children[3] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, num, num));
    children[4] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, -num, -num));
    children[5] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, -num, -num));
    children[6] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, -num, num));
    children[7] = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, -num, num));
  }

  private void Merge()
  {
    for (int index1 = 0; index1 < 8; ++index1)
    {
      BoundsOctreeNode<T> child = children[index1];
      for (int index2 = child.objects.Count - 1; index2 >= 0; --index2)
        objects.Add(child.objects[index2]);
    }
    children = null;
  }

  private static bool Encapsulates(Bounds outerBounds, Bounds innerBounds) => outerBounds.Contains(innerBounds.min) && outerBounds.Contains(innerBounds.max);

  private int BestFitChild(Bounds objBounds) => (objBounds.center.x <= (double) Center.x ? 0 : 1) + (objBounds.center.y >= (double) Center.y ? 0 : 4) + (objBounds.center.z <= (double) Center.z ? 0 : 2);

  private bool ShouldMerge()
  {
    int count = objects.Count;
    if (children != null)
    {
      foreach (BoundsOctreeNode<T> child in children)
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

  private class OctreeObject
  {
    public T Obj;
    public Bounds Bounds;
  }
}
