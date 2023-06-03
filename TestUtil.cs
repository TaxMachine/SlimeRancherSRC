// Decompiled with JetBrains decompiler
// Type: TestUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TestUtil
{
  public static void AreApproximatelyEqual(
    Vector3 vA,
    Vector3 vB,
    float tolerance,
    string message)
  {
  }

  public static void AssertAreEqual<K, V>(
    Dictionary<K, V> expected,
    Dictionary<K, V> actual,
    Action<V, V> valueAssertion,
    string dictName)
  {
    foreach (KeyValuePair<K, V> keyValuePair in expected)
      valueAssertion(keyValuePair.Value, actual[keyValuePair.Key]);
  }

  public static void AssertVersionedAreEqual<K, V1, V2>(
    Dictionary<K, V1> expected,
    Dictionary<K, V2> actual,
    Action<V1, V2> valueAssertion,
    string dictName)
  {
    foreach (KeyValuePair<K, V1> keyValuePair in expected)
      valueAssertion(keyValuePair.Value, actual[keyValuePair.Key]);
  }

  public static void AssertAreEqual<T>(List<T> expected, List<T> actual, string field = "") => AssertAreEqual(expected, actual, (a, b, m) => { }, field);

  public static void AssertAreEqual<T1, T2>(
    List<T1> expected,
    List<T2> actual,
    Action<T1, T2, string> valueAssertion,
    string field = "")
  {
    if (expected == null && actual == null)
      return;
    for (int index = 0; index < expected.Count; ++index)
      valueAssertion(expected[index], actual[index], string.Format("{0}[{1}]", field, index));
  }

  public static bool AssertNullness(object expected, object actual)
  {
    int num = expected != null ? 0 : (actual == null ? 1 : 0);
    return expected != null && actual != null;
  }

  public class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
  {
    private IEqualityComparer<T> elemComparer;

    public SequenceComparer(IEqualityComparer<T> elemComparer = null) => this.elemComparer = elemComparer;

    public bool Equals(IEnumerable<T> x, IEnumerable<T> y) => x.SequenceEqual(y, elemComparer);

    public int GetHashCode(IEnumerable<T> obj) => throw new NotImplementedException();
  }

  public class ListComparer<T> : IEqualityComparer<List<T>>
  {
    private IEqualityComparer<T> elemComparer;

    public ListComparer(IEqualityComparer<T> elemComparer = null) => this.elemComparer = elemComparer;

    public bool Equals(List<T> x, List<T> y) => x.SequenceEqual(y, elemComparer);

    public int GetHashCode(List<T> obj) => throw new NotImplementedException();
  }

  public class ArrayComparer<T> : IEqualityComparer<T[]>
  {
    private IEqualityComparer<T> elemComparer;

    public ArrayComparer(IEqualityComparer<T> elemComparer = null) => this.elemComparer = elemComparer;

    public bool Equals(T[] x, T[] y) => x.SequenceEqual(y, elemComparer);

    public int GetHashCode(T[] obj) => throw new NotImplementedException();
  }

  public class DictComparer<K, V> : IEqualityComparer<IEnumerable<KeyValuePair<K, V>>>
  {
    private KeyValueComparer<K, V> keyValComparer;

    public DictComparer()
      : this(null, null)
    {
    }

    public DictComparer(IEqualityComparer<K> keyComparer, IEqualityComparer<V> valComparer) => keyValComparer = new KeyValueComparer<K, V>(keyComparer, valComparer);

    public bool Equals(IEnumerable<KeyValuePair<K, V>> x, IEnumerable<KeyValuePair<K, V>> y)
    {
      if (x.Count() != y.Count())
        return false;
      List<KeyValuePair<K, V>> keyValuePairList = new List<KeyValuePair<K, V>>(y);
      foreach (KeyValuePair<K, V> x1 in x)
      {
        bool flag = false;
        foreach (KeyValuePair<K, V> y1 in keyValuePairList)
        {
          if (keyValComparer.Equals(x1, y1))
          {
            flag = true;
            keyValuePairList.Remove(y1);
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public int GetHashCode(IEnumerable<KeyValuePair<K, V>> obj) => throw new NotImplementedException();
  }

  public class KeyValueComparer<K, V> : IEqualityComparer<KeyValuePair<K, V>>
  {
    private IEqualityComparer<K> keyComparer;
    private IEqualityComparer<V> valComparer;

    public KeyValueComparer(IEqualityComparer<K> keyComparer, IEqualityComparer<V> valComparer)
    {
      this.keyComparer = keyComparer;
      this.valComparer = valComparer;
    }

    public bool Equals(KeyValuePair<K, V> x, KeyValuePair<K, V> y)
    {
      if ((keyComparer == null ? (x.Key.Equals(y.Key) ? 1 : 0) : (keyComparer.Equals(x.Key, y.Key) ? 1 : 0)) == 0)
        return false;
      return valComparer != null ? valComparer.Equals(x.Value, y.Value) : x.Value.Equals(y.Value);
    }

    public int GetHashCode(KeyValuePair<K, V> obj) => (keyComparer == null ? obj.Key.GetHashCode() : keyComparer.GetHashCode(obj.Key)) ^ (valComparer == null ? obj.Value.GetHashCode() : valComparer.GetHashCode(obj.Value));
  }

  public class Vector3Comparer : IEqualityComparer<Vector3>
  {
    private float tolerance;
    private bool wraparound360;

    public Vector3Comparer(float tolerance = 0.001f, bool wraparound360 = false)
    {
      this.tolerance = tolerance;
      this.wraparound360 = wraparound360;
    }

    public bool Equals(Vector3 v1, Vector3 v2)
    {
      float num1 = Math.Abs(v1.x - v2.x);
      float num2 = Math.Abs(v1.y - v2.y);
      float num3 = Math.Abs(v1.z - v2.z);
      if (wraparound360)
      {
        while (num1 >= 360.0 - tolerance)
          num1 -= 360f;
        while (num2 >= 360.0 - tolerance)
          num2 -= 360f;
        while (num3 >= 360.0 - tolerance)
          num3 -= 360f;
      }
      return Math.Abs(num1) <= (double) tolerance && Math.Abs(num2) <= (double) tolerance && Math.Abs(num3) <= (double) tolerance;
    }

    public int GetHashCode(Vector3 obj)
    {
      double num1 = Math.Round(obj.x);
      int hashCode1 = num1.GetHashCode();
      num1 = Math.Round(obj.y);
      int hashCode2 = num1.GetHashCode();
      int num2 = hashCode1 ^ hashCode2;
      num1 = Math.Round(obj.z);
      int hashCode3 = num1.GetHashCode();
      return num2 ^ hashCode3;
    }
  }
}
