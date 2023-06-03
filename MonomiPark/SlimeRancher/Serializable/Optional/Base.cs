// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Serializable.Optional.Base
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace MonomiPark.SlimeRancher.Serializable.Optional
{
  public abstract class Base
  {
    [Tooltip("Indicates if the property value should be used.")]
    public bool HasValue;

    public static bool operator true(Base instance) => instance.HasValue;

    public static bool operator false(Base instance) => !instance.HasValue;
  }
}
