// Decompiled with JetBrains decompiler
// Type: Randoms
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class Randoms
{
  public static Randoms SHARED = new Randoms();
  private readonly Random rand;
  private double nextNextGaussian;
  private bool haveNextNextGaussian;
  private object gaussLock = new object();

  public Randoms() => rand = new Random();

  public Randoms(int seed) => rand = new Random(seed);

  public int GetInt(int high) => NextInt(high);

  public int GetInt() => rand.Next();

  public int GetInRange(int low, int high) => low == high ? low : low + NextInt(high - low);

  public float GetFloat(float high) => (float) rand.NextDouble() * high;

  public float GetInRange(float low, float high) => low == (double) high ? low : low + (float) (rand.NextDouble() * (high - (double) low));

  public bool GetChance(int n) => NextInt(n) == 0;

  public bool GetProbability(float p) => rand.NextDouble() < p;

  public bool GetBoolean() => NextInt(2) == 0;

  public float GetNormal(float mean, float dev) => (float) NextGaussian() * dev + mean;

  public T Pick<T>(T[] vals) => vals[GetInt(vals.Length)];

  public T Pick<T>(IEnumerator<T> iterator, T ifEmpty)
  {
    if (!iterator.MoveNext())
      return ifEmpty;
    T obj = iterator.Current;
    int n = 2;
    while (iterator.Current != null && iterator.MoveNext())
    {
      T current = iterator.Current;
      if (NextInt(n) == 0)
        obj = current;
      ++n;
    }
    return obj;
  }

  public T Pick<T>(IEnumerable<T> enumerable, T ifEmpty) => Pick(enumerable.GetEnumerator(), ifEmpty);

  public IEnumerable<T> Pick<T>(List<T> collection, int count)
  {
    List<int> options = Enumerable.Range(0, collection.Count()).ToList();
    for (int ii = 0; ii < count && options.Any(); ++ii)
      yield return collection[Pluck(options, 0)];
  }

  public IEnumerable<T> Pick<T>(List<T> collection, int count, Func<T, float> weightFunction)
  {
    List<int> options = Enumerable.Range(0, collection.Count()).ToList();
    Func<int, float> optionsWeightFunction = idx => weightFunction(collection[idx]);
    for (int ii = 0; ii < count && options.Any(); ++ii)
    {
      int randomIndex = Pick(options, optionsWeightFunction, -1);
      if (randomIndex == -1)
        break;
      yield return collection[randomIndex];
      options.Remove(randomIndex);
    }
  }

  public IEnumerable<T> Pick<T>(List<T> collection, int min, int max) => Pick(collection, GetInRange(min, max));

  public T Pick<T>(ICollection<T> iterable, T ifEmpty) => PickPluck(iterable, ifEmpty, false);

  public T Pick<T>(IDictionary<T, float> weightMap, T ifEmpty)
  {
    T obj = ifEmpty;
    float high = 0.0f;
    foreach (KeyValuePair<T, float> weight in weightMap)
    {
      float num = weight.Value;
      if (num > 0.0)
      {
        high += num;
        if (high == (double) num || GetFloat(high) < (double) num)
          obj = weight.Key;
      }
      else if (num < 0.0)
        throw new ArgumentException(nameof (weightMap), "Weight less than 0: " + weight);
    }
    return obj;
  }

  public T Pick<T>(ICollection<T> iterable, Func<T, float> weightFunction, T ifEmpty)
  {
    T obj1 = ifEmpty;
    float high = 0.0f;
    foreach (T obj2 in iterable)
    {
      float num = weightFunction(obj2);
      if (num > 0.0)
      {
        high += num;
        if (high == (double) num || GetFloat(high) < (double) num)
          obj1 = obj2;
      }
      else if (num < 0.0)
        throw new ArgumentException("weightMap", "Weight less than 0: " + obj2);
    }
    return obj1;
  }

  public T Pluck<T>(ICollection<T> iterable, T ifEmpty) => PickPluck(iterable, ifEmpty, true);

  protected T PickPluck<T>(ICollection<T> coll, T ifEmpty, bool remove)
  {
    int count = coll.Count;
    if (count == 0)
      return ifEmpty;
    if (coll is IList<T>)
    {
      IList<T> objList = (IList<T>) coll;
      int index = NextInt(count);
      T obj = objList[index];
      if (!remove)
        return obj;
      objList.RemoveAt(index);
      return obj;
    }
    IEnumerator<T> enumerator = coll.GetEnumerator();
    enumerator.MoveNext();
    for (int index = NextInt(count); index > 0; --index)
      enumerator.MoveNext();
    try
    {
      return enumerator.Current;
    }
    finally
    {
      if (remove)
        coll.Remove(enumerator.Current);
    }
  }

  protected static void Swap<T>(IList<T> list, int ii, int jj)
  {
    T obj = list[jj];
    list[jj] = list[ii];
    list[ii] = obj;
  }

  protected static void Swap<T>(T[] array, int ii, int jj)
  {
    T obj = array[ii];
    array[ii] = array[jj];
    array[jj] = obj;
  }

  private int NextInt(int n)
  {
    if (n <= 0)
      throw new ArgumentOutOfRangeException(nameof (n), "must be positive");
    int num1;
    int num2;
    do
    {
      num1 = rand.Next();
      num2 = num1 % n;
    }
    while (num1 - num2 + (n - 1) < 0);
    return num2;
  }

  private double NextGaussian()
  {
    lock (gaussLock)
    {
      if (haveNextNextGaussian)
      {
        haveNextNextGaussian = false;
        return nextNextGaussian;
      }
      double num1;
      double num2;
      double d;
      do
      {
        num1 = 2.0 * rand.NextDouble() - 1.0;
        num2 = 2.0 * rand.NextDouble() - 1.0;
        d = num1 * num1 + num2 * num2;
      }
      while (d >= 1.0 || d == 0.0);
      double num3 = Math.Sqrt(-2.0 * Math.Log(d) / d);
      nextNextGaussian = num2 * num3;
      haveNextNextGaussian = true;
      return num1 * num3;
    }
  }
}
