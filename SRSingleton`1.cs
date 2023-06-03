// Decompiled with JetBrains decompiler
// Type: SRSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SRSingleton<T> : SRBehaviour where T : SRSingleton<T>
{
  public virtual void Awake()
  {
    if (Nested.instance == this)
      return;
    if (Nested.instance != null)
      Log.Error("An instance of the singleton " + typeof (T) + " already exists, attempting to create additional.");
    Nested.instance = (T) this;
  }

  public virtual void OnDestroy()
  {
    if (!(Nested.instance == this))
      return;
    Nested.instance = default (T);
  }

  public static T Instance => Nested.instance;

  private class Nested
  {
    internal static T instance;
  }
}
