// Decompiled with JetBrains decompiler
// Type: UiParticles.SetPropertyUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UiParticles
{
  internal static class SetPropertyUtility
  {
    public static bool SetColor(ref Color currentValue, Color newValue)
    {
      if (currentValue.r == (double) newValue.r && currentValue.g == (double) newValue.g && currentValue.b == (double) newValue.b && currentValue.a == (double) newValue.a)
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
      if (currentValue.Equals(newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
      if (currentValue == null && newValue == null || currentValue != null && currentValue.Equals(newValue))
        return false;
      currentValue = newValue;
      return true;
    }
  }
}
