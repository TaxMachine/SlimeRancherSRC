// Decompiled with JetBrains decompiler
// Type: vp_Attempt`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public class vp_Attempt<V> : vp_Attempt
{
  public Tryer<V> Try;

  protected static bool AlwaysOK<T>(T value) => true;

  public bool AttemptAlwaysOK(V value) => true;

  public vp_Attempt(string name)
    : base(name, typeof (V))
  {
  }

  protected override void InitFields()
  {
    m_Fields = new FieldInfo[1]
    {
      GetType().GetField("Try")
    };
    StoreInvokerFieldNames();
    m_DelegateTypes = new System.Type[1]
    {
      typeof (vp_Attempt<>.Tryer<>)
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "OnAttempt_",
        0
      }
    };
    Try = AttemptAlwaysOK;
  }

  public delegate bool Tryer<T>(T value);
}
