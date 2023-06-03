// Decompiled with JetBrains decompiler
// Type: SECTR_PriorityQueue`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class SECTR_PriorityQueue<T> where T : IComparable<T>
{
  private List<T> data;

  public SECTR_PriorityQueue() => data = new List<T>(64);

  public SECTR_PriorityQueue(int capacity) => data = new List<T>(capacity);

  public int Count
  {
    get => data.Count;
    set
    {
    }
  }

  public T this[int index]
  {
    get => index >= data.Count ? default (T) : data[index];
    set
    {
      if (index >= data.Count)
        return;
      data[index] = value;
      _Update(index);
    }
  }

  public void Enqueue(T item)
  {
    data.Add(item);
    int num;
    for (int index = data.Count - 1; index > 0; index = num)
    {
      num = (index - 1) / 2;
      if (data[index].CompareTo(data[num]) >= 0)
        break;
      _SwapElements(index, num);
    }
  }

  public T Dequeue()
  {
    int index1 = data.Count - 1;
    T obj1 = data[0];
    data[0] = data[index1];
    data.RemoveAt(index1);
    int num1 = index1 - 1;
    int num2 = 0;
    while (true)
    {
      int num3 = num2 * 2 + 1;
      if (num3 <= num1)
      {
        int index2 = num3 + 1;
        T obj2;
        if (index2 <= num1)
        {
          obj2 = data[index2];
          if (obj2.CompareTo(data[num3]) < 0)
            num3 = index2;
        }
        obj2 = data[num2];
        if (obj2.CompareTo(data[num3]) > 0)
        {
          _SwapElements(num2, num3);
          num2 = num3;
        }
        else
          break;
      }
      else
        break;
    }
    return obj1;
  }

  public T Peek() => data.Count <= 0 ? default (T) : data[0];

  public override string ToString()
  {
    string str = "";
    for (int index = 0; index < data.Count; ++index)
      str = str + data[index].ToString() + " ";
    return str + "count = " + data.Count;
  }

  public bool IsConsistent()
  {
    if (data.Count > 0)
    {
      int num = data.Count - 1;
      for (int index1 = 0; index1 < data.Count; ++index1)
      {
        int index2 = 2 * index1 + 1;
        int index3 = 2 * index1 + 2;
        if (index2 <= num && data[index1].CompareTo(data[index2]) > 0 || index3 <= num && data[index1].CompareTo(data[index3]) > 0)
          return false;
      }
    }
    return true;
  }

  public void Clear() => data.Clear();

  private void _SwapElements(int i, int j)
  {
    T obj = data[i];
    data[i] = data[j];
    data[j] = obj;
  }

  private void _Update(int i)
  {
    int num1;
    int num2;
    for (num1 = i; num1 > 0; num1 = num2)
    {
      num2 = (num1 - 1) / 2;
      if (data[num1].CompareTo(data[num2]) < 0)
        _SwapElements(num1, num2);
      else
        break;
    }
    if (num1 < i)
      return;
    while (true)
    {
      int j = num1;
      int num3 = 2 * num1 + 1;
      int num4 = 2 * num1 + 2;
      if (data.Count > num3 && data[num1].CompareTo(data[num3]) > 0)
      {
        _SwapElements(num3, j);
        num1 = num3;
      }
      else if (data.Count > num4 && data[num1].CompareTo(data[num4]) > 0)
      {
        _SwapElements(num4, j);
        num1 = num4;
      }
      else
        break;
    }
  }
}
