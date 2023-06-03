// Decompiled with JetBrains decompiler
// Type: SRComparer`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public abstract class SRComparer<T> : Comparer<T>
{
  private List<Func<T, T, int>> comparisons = new List<Func<T, T, int>>();

  public override int Compare(T a, T b)
  {
    foreach (Func<T, T, int> comparison in comparisons)
    {
      int num = comparison(a, b);
      if (num != 0)
        return num;
    }
    return 0;
  }

  public SRComparer<T> OrderBy<K>(Func<T, K> keyFunction)
  {
    comparisons.Add((a, b) => Comparer<K>.Default.Compare(keyFunction(a), keyFunction(b)));
    return this;
  }

  public SRComparer<T> OrderByDescending<K>(Func<T, K> keyFunction)
  {
    comparisons.Add((a, b) => Comparer<K>.Default.Compare(keyFunction(b), keyFunction(a)));
    return this;
  }
}
