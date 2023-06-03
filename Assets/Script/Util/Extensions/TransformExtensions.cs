// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.TransformExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Util.Extensions
{
  public static class TransformExtensions
  {
    public static string GetHierarchyString(this Transform transform) => string.Join("/", transform.GetHierarchy().Select(it => it.name).ToArray());

    public static IEnumerable<Transform> GetHierarchy(this Transform transform) => transform.GetAscendents().Reverse<Transform>();

    private static IEnumerable<Transform> GetAscendents(this Transform transform)
    {
      for (Transform current = transform; current != null; current = current.parent)
        yield return current;
    }
  }
}
