// Decompiled with JetBrains decompiler
// Type: LazyState`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class LazyState<T> where T : struct
{
  private T? state;

  public static implicit operator T(LazyState<T> instance)
  {
    if (!instance.state.HasValue)
      throw new InvalidOperationException();
    return instance.state.Value;
  }

  public LazyState() => state = new T?();

  public LazyState(T initialValue) => state = new T?(initialValue);

  public bool Update(T current)
  {
    if (current.Equals(state))
      return false;
    state = new T?(current);
    return true;
  }
}
