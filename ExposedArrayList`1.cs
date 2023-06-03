// Decompiled with JetBrains decompiler
// Type: ExposedArrayList`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class ExposedArrayList<T>
{
  private const int ARRAY_START_SIZE = 1000;
  private const int ARRAY_GROWTH_SIZE = 100;
  public T[] Data;
  private int _count;

  public ExposedArrayList()
    : this(1000)
  {
  }

  public ExposedArrayList(int startSize) => Data = new T[startSize];

  public int GetCount() => _count;

  public void Add(T item)
  {
    if (_count >= Data.Length)
      Array.Resize(ref Data, Data.Length + 100);
    Data[_count] = item;
    ++_count;
  }

  public void Remove(T item)
  {
    int index = IndexOf(item);
    if (index <= -1 || index > _count)
      return;
    Data[index] = Data[_count - 1];
    Data[_count - 1] = default (T);
    --_count;
  }

  public void Clear()
  {
    for (int index = 0; index < _count; ++index)
      Data[index] = default (T);
    _count = 0;
  }

  public int IndexOf(T item)
  {
    for (int index = 0; index < _count; ++index)
    {
      if (item.Equals(Data[index]))
        return index;
    }
    return -1;
  }
}
