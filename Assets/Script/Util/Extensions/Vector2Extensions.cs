// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.Vector2Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace Assets.Script.Util.Extensions
{
  public static class Vector2Extensions
  {
    public static float GetRandom(this Vector2 range) => range.GetRandom(Randoms.SHARED);

    public static float GetRandom(this Vector2 range, Randoms random) => random.GetInRange(range.x, range.y);

    public static float Lerp(this Vector2 range, float t) => Mathf.Lerp(range.x, range.y, t);
  }
}
