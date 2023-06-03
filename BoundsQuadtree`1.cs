// Decompiled with JetBrains decompiler
// Type: BoundsQuadtree`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class BoundsQuadtree<T>
{
  private BoundsQuadtreeNode<T> rootNode;
  private readonly float looseness;
  private readonly float initialSize;
  private readonly float minSize;

  public int Count { get; private set; }

  public BoundsQuadtree(
    float initialWorldSize,
    Vector3 initialWorldPos,
    float minNodeSize,
    float loosenessVal)
  {
    if (minNodeSize > (double) initialWorldSize)
    {
      Log.Warning("Minimum node size must be at least as big as the initial world size. Was: " + minNodeSize + " Adjusted to: " + initialWorldSize);
      minNodeSize = initialWorldSize;
    }
    Count = 0;
    initialSize = initialWorldSize;
    minSize = minNodeSize;
    looseness = Mathf.Clamp(loosenessVal, 1f, 2f);
    rootNode = new BoundsQuadtreeNode<T>(initialSize, minSize, loosenessVal, initialWorldPos);
  }

  public void Add(T obj, Bounds objBounds)
  {
    int num = 0;
    while (!rootNode.Add(obj, objBounds))
    {
      Grow(objBounds.center - rootNode.Center);
      if (++num > 20)
      {
        Log.Error("Aborted Add operation as it seemed to be going on forever (" + (num - 1) + ") attempts at growing the quadtree.");
        return;
      }
    }
    ++Count;
  }

  public bool Remove(T obj)
  {
    int num = rootNode.Remove(obj) ? 1 : 0;
    if (num == 0)
      return num != 0;
    --Count;
    Shrink();
    return num != 0;
  }

  public bool IsColliding(Bounds checkBounds) => rootNode.IsColliding(checkBounds);

  public List<T> GetColliding(Bounds checkBounds, ref List<T> result)
  {
    if (rootNode.IntersectsBounds(checkBounds))
      rootNode.GetColliding(checkBounds, ref result);
    return result;
  }

  public List<T> GetColliding(Vector3 checkPoint, ref List<T> result)
  {
    if (rootNode.ContainsPoint(checkPoint))
      rootNode.GetColliding(checkPoint, ref result);
    return result;
  }

  public List<T> GetAll(ref List<T> result) => GetColliding(new Bounds(Vector3.zero, Vector3.one * float.PositiveInfinity), ref result);

  public void DrawAllBounds() => rootNode.DrawAllBounds();

  public void DrawAllObjects() => rootNode.DrawAllObjects();

  private void Grow(Vector3 direction)
  {
    int xDir = direction.x >= 0.0 ? 1 : -1;
    int zDir = direction.z >= 0.0 ? 1 : -1;
    BoundsQuadtreeNode<T> rootNode = this.rootNode;
    float num1 = this.rootNode.BaseLength / 2f;
    float baseLengthVal = this.rootNode.BaseLength * 2f;
    Vector3 centerVal = this.rootNode.Center + new Vector3(xDir * num1, 0.0f, zDir * num1);
    this.rootNode = new BoundsQuadtreeNode<T>(baseLengthVal, minSize, looseness, centerVal);
    int rootPosIndex = GetRootPosIndex(xDir, zDir);
    BoundsQuadtreeNode<T>[] childQuadtrees = new BoundsQuadtreeNode<T>[4];
    for (int index = 0; index < 4; ++index)
    {
      if (index == rootPosIndex)
      {
        childQuadtrees[index] = rootNode;
      }
      else
      {
        int num2 = index % 2 == 0 ? -1 : 1;
        int num3 = index >= 2 ? 1 : -1;
        childQuadtrees[index] = new BoundsQuadtreeNode<T>(this.rootNode.BaseLength, minSize, looseness, centerVal + new Vector3(num2 * num1, this.rootNode.Center.y, num3 * num1));
      }
    }
    this.rootNode.SetChildren(childQuadtrees);
  }

  private void Shrink() => rootNode = rootNode.ShrinkIfPossible(initialSize);

  private static int GetRootPosIndex(int xDir, int zDir)
  {
    int rootPosIndex = xDir > 0 ? 1 : 0;
    if (zDir > 0)
      rootPosIndex += 2;
    return rootPosIndex;
  }
}
