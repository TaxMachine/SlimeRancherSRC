// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.LinqExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Assets.Script.Util.Extensions
{
  public static class LinqExtensions
  {
    public static IEnumerable<T> ToEnumerable<T>(this T item)
    {
      yield return item;
    }

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items) => new HashSet<T>(items);

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer) => new HashSet<T>(items, comparer);
  }
}
