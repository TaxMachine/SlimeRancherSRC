// Decompiled with JetBrains decompiler
// Type: Assets.Script.Util.Extensions.Vector3Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace Assets.Script.Util.Extensions
{
  public static class Vector3Extensions
  {
    public static bool IsNaN(this Vector3 instance) => float.IsNaN(instance.x) || float.IsNaN(instance.y) || float.IsNaN(instance.z);
  }
}
