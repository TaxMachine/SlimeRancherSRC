// Decompiled with JetBrains decompiler
// Type: UnityWorkarounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public static class UnityWorkarounds
{
  public static void SafeRemoveAllNulls<T>(HashSet<T> inputSet) where T : UnityEngine.Object => inputSet.RemoveWhere(o => o == null);
}
