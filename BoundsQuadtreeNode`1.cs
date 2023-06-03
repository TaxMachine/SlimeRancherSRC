// Decompiled with JetBrains decompiler
// Type: BoundsQuadtreeNode`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class BoundsQuadtreeNode<T>
{
  private float looseness;
  private float minSize;
  private float adjLength;
  private Bounds bounds;
  private readonly List<QuadtreeObject> objects = new List<QuadtreeObject>();
  private BoundsQuadtreeNode<T>[] children;
  private Bounds[] childBounds;
  private const int numObjectsAllowed = 4;
  private const float DRAW_AS_HEIGHT = 1000f;

  public Vector3 Center { get; private set; }

  public float BaseLength { get; private set; }

  public BoundsQuadtreeNode(
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
      for (int index = 0; index < children.Length; ++index)
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
    if (objects.Count == 0 && (children == null || children.Length == 0) || !bounds.Intersects(checkBounds))
      return false;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Bounds.Intersects(checkBounds))
        return true;
    }
    if (children != null)
    {
      for (int index = 0; index < children.Length; ++index)
      {
        if (children[index].IsColliding(checkBounds))
          return true;
      }
    }
    return false;
  }

  public bool IntersectsBounds(Bounds checkBounds) => bounds.Intersects(checkBounds);

  public bool ContainsPoint(Vector3 checkPoint) => bounds.Contains(checkPoint);

  public void GetColliding(Bounds checkBounds, ref List<T> result)
  {
    if (objects.Count == 0 && (children == null || children.Length == 0))
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Bounds.Intersects(checkBounds))
        result.Add(objects[index].Obj);
    }
    if (children == null)
      return;
    bool flag1 = checkBounds.min.y <= (double) children[0].bounds.max.y;
    bool flag2 = checkBounds.max.y >= (double) children[2].bounds.min.y;
    int num = checkBounds.min.x <= (double) children[0].bounds.max.x ? 1 : 0;
    bool flag3 = checkBounds.max.x >= (double) children[1].bounds.min.x;
    if (num != 0)
    {
      if (flag1)
        children[0].GetColliding(checkBounds, ref result);
      if (flag2)
        children[2].GetColliding(checkBounds, ref result);
    }
    if (!flag3)
      return;
    if (flag1)
      children[1].GetColliding(checkBounds, ref result);
    if (!flag2)
      return;
    children[3].GetColliding(checkBounds, ref result);
  }

  public void GetColliding(Vector3 checkPoint, ref List<T> result)
  {
    if (objects.Count == 0 && (children == null || children.Length == 0))
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].Bounds.Contains(checkPoint))
        result.Add(objects[index].Obj);
    }
    if (children == null)
      return;
    bool flag1 = checkPoint.y <= (double) children[0].bounds.max.y;
    bool flag2 = checkPoint.y >= (double) children[2].bounds.min.y;
    int num = checkPoint.x <= (double) children[0].bounds.max.x ? 1 : 0;
    bool flag3 = checkPoint.x >= (double) children[1].bounds.min.x;
    if (num != 0)
    {
      if (flag1)
        children[0].GetColliding(checkPoint, ref result);
      if (flag2)
        children[2].GetColliding(checkPoint, ref result);
    }
    if (!flag3)
      return;
    if (flag1)
      children[1].GetColliding(checkPoint, ref result);
    if (!flag2)
      return;
    children[3].GetColliding(checkPoint, ref result);
  }

  public void SetChildren(BoundsQuadtreeNode<T>[] childQuadtrees)
  {
    if (childQuadtrees.Length != 4)
      Log.Error("Child quadtree array must be length 4. Was length: " + childQuadtrees.Length);
    else
      children = childQuadtrees;
  }

  public void DrawAllBounds(float depth = 0.0f)
  {
    float r = depth / 7f;
    Gizmos.color = new Color(r, 0.0f, 1f - r);
    Bounds bounds = new Bounds(Center, new Vector3(adjLength, 1000f, adjLength));
    Gizmos.DrawWireCube(bounds.center, bounds.size);
    if (children != null)
    {
      ++depth;
      for (int index = 0; index < children.Length; ++index)
        children[index].DrawAllBounds(depth);
    }
    Gizmos.color = Color.white;
  }

  public void DrawAllObjects()
  {
    float b = BaseLength / 20f;
    Gizmos.color = new Color(0.0f, 1f - b, b, 0.25f);
    foreach (QuadtreeObject quadtreeObject in objects)
    {
      Gizmos.DrawCube(quadtreeObject.Bounds.center, quadtreeObject.Bounds.size);
      Gizmos.color = Color.magenta;
      Gizmos.DrawLine(quadtreeObject.Bounds.center, bounds.center);
    }
    if (children != null)
    {
      for (int index = 0; index < children.Length; ++index)
        children[index].DrawAllObjects();
    }
    Gizmos.color = Color.white;
  }

  public BoundsQuadtreeNode<T> ShrinkIfPossible(float minLength)
  {
    if (BaseLength < 2.0 * minLength || objects.Count == 0 && (children == null || children.Length == 0))
      return this;
    int index1 = -1;
    for (int index2 = 0; index2 < objects.Count; ++index2)
    {
      QuadtreeObject quadtreeObject = objects[index2];
      int index3 = BestFitChild(quadtreeObject.Bounds);
      if (index2 != 0 && index3 != index1 || !Encapsulates(childBounds[index3], quadtreeObject.Bounds))
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
    bounds = new Bounds(Center, new Vector3(adjLength, float.PositiveInfinity, adjLength));
    float num1 = BaseLength / 4f;
    float num2 = BaseLength / 2f * looseness;
    Vector3 size = new Vector3(num2, float.PositiveInfinity, num2);
    childBounds = new Bounds[4];
    childBounds[0] = new Bounds(Center + new Vector3(-num1, 0.0f, -num1), size);
    childBounds[1] = new Bounds(Center + new Vector3(num1, 0.0f, -num1), size);
    childBounds[2] = new Bounds(Center + new Vector3(-num1, 0.0f, num1), size);
    childBounds[3] = new Bounds(Center + new Vector3(num1, 0.0f, num1), size);
  }

  private void SubAdd(T obj, Bounds objBounds)
  {
    if (children == null && (objects.Count < 4 || BaseLength / 2.0 < minSize))
    {
      objects.Add(new QuadtreeObject()
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
          QuadtreeObject quadtreeObject = objects[index1];
          int index2 = BestFitChild(quadtreeObject.Bounds);
          if (Encapsulates(children[index2].bounds, quadtreeObject.Bounds))
          {
            children[index2].SubAdd(quadtreeObject.Obj, quadtreeObject.Bounds);
            objects.Remove(quadtreeObject);
          }
        }
      }
      int index = BestFitChild(objBounds);
      if (Encapsulates(children[index].bounds, objBounds))
        children[index].SubAdd(obj, objBounds);
      else
        objects.Add(new QuadtreeObject()
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
    children = new BoundsQuadtreeNode<T>[4];
    children[0] = new BoundsQuadtreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, 0.0f, -num));
    children[1] = new BoundsQuadtreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, 0.0f, -num));
    children[2] = new BoundsQuadtreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(-num, 0.0f, num));
    children[3] = new BoundsQuadtreeNode<T>(baseLengthVal, minSize, looseness, Center + new Vector3(num, 0.0f, num));
  }

  private void Merge()
  {
    for (int index1 = 0; index1 < children.Length; ++index1)
    {
      BoundsQuadtreeNode<T> child = children[index1];
      for (int index2 = child.objects.Count - 1; index2 >= 0; --index2)
        objects.Add(child.objects[index2]);
    }
    children = null;
  }

  private static bool Encapsulates(Bounds outerBounds, Bounds innerBounds) => outerBounds.Contains(innerBounds.min) && outerBounds.Contains(innerBounds.max);

  private int BestFitChild(Bounds objBounds) => (objBounds.center.x <= (double) Center.x ? 0 : 1) + (objBounds.center.z <= (double) Center.z ? 0 : 2);

  private bool ShouldMerge()
  {
    int count = objects.Count;
    if (children != null)
    {
      foreach (BoundsQuadtreeNode<T> child in children)
      {
        if (child.children != null)
          return false;
        count += child.objects.Count;
      }
    }
    return count <= 4;
  }

  private bool HasAnyObjects()
  {
    if (objects.Count > 0)
      return true;
    if (children != null)
    {
      for (int index = 0; index < children.Length; ++index)
      {
        if (children[index].HasAnyObjects())
          return true;
      }
    }
    return false;
  }

  private class QuadtreeObject
  {
    public T Obj;
    public Bounds Bounds;
  }
}
