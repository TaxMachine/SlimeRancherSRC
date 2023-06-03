// Decompiled with JetBrains decompiler
// Type: BoundsOctree`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BoundsOctree<T>
{
  private BoundsOctreeNode<T> rootNode;
  private readonly float looseness;
  private readonly float initialSize;
  private readonly float minSize;

  public int Count { get; private set; }

  public BoundsOctree(
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
    rootNode = new BoundsOctreeNode<T>(initialSize, minSize, loosenessVal, initialWorldPos);
  }

  public void Add(T obj, Bounds objBounds)
  {
    int num = 0;
    while (!rootNode.Add(obj, objBounds))
    {
      Grow(objBounds.center - rootNode.Center);
      if (++num > 20)
      {
        Log.Error("Aborted Add operation as it seemed to be going on forever (" + (num - 1) + ") attempts at growing the octree.");
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

  public T[] GetColliding(Bounds checkBounds) => rootNode.GetColliding(checkBounds);

  public void DrawAllBounds() => rootNode.DrawAllBounds();

  public void DrawAllObjects() => rootNode.DrawAllObjects();

  private void Grow(Vector3 direction)
  {
    int xDir = direction.x >= 0.0 ? 1 : -1;
    int yDir = direction.y >= 0.0 ? 1 : -1;
    int zDir = direction.z >= 0.0 ? 1 : -1;
    BoundsOctreeNode<T> rootNode = this.rootNode;
    float num1 = this.rootNode.BaseLength / 2f;
    float baseLengthVal = this.rootNode.BaseLength * 2f;
    Vector3 centerVal = this.rootNode.Center + new Vector3(xDir * num1, yDir * num1, zDir * num1);
    this.rootNode = new BoundsOctreeNode<T>(baseLengthVal, minSize, looseness, centerVal);
    int rootPosIndex = GetRootPosIndex(xDir, yDir, zDir);
    BoundsOctreeNode<T>[] childOctrees = new BoundsOctreeNode<T>[8];
    for (int index = 0; index < 8; ++index)
    {
      if (index == rootPosIndex)
      {
        childOctrees[index] = rootNode;
      }
      else
      {
        int num2 = index % 2 == 0 ? -1 : 1;
        int num3 = index > 3 ? -1 : 1;
        int num4 = index < 2 || index > 3 && index < 6 ? -1 : 1;
        childOctrees[index] = new BoundsOctreeNode<T>(this.rootNode.BaseLength, minSize, looseness, centerVal + new Vector3(num2 * num1, num3 * num1, num4 * num1));
      }
    }
    this.rootNode.SetChildren(childOctrees);
  }

  private void Shrink() => rootNode = rootNode.ShrinkIfPossible(initialSize);

  private static int GetRootPosIndex(int xDir, int yDir, int zDir)
  {
    int rootPosIndex = xDir > 0 ? 1 : 0;
    if (yDir < 0)
      rootPosIndex += 4;
    if (zDir > 0)
      rootPosIndex += 2;
    return rootPosIndex;
  }
}
