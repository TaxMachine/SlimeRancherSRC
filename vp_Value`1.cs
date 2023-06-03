// Decompiled with JetBrains decompiler
// Type: vp_Value`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public class vp_Value<V> : vp_Event
{
  public Getter<V> Get;
  public Setter<V> Set;

  protected static T Empty<T>() => default (T);

  protected static void Empty<T>(T value)
  {
  }

  public V GetEmpty() => default (V);

  public void SetEmpty(V value)
  {
  }

  private FieldInfo[] Fields => m_Fields;

  public vp_Value(string name)
    : base(name, typeof (V))
  {
    InitFields();
    EventType = vp_EventType.Value;
  }

  protected override void InitFields()
  {
    m_Fields = new FieldInfo[2]
    {
      GetType().GetField("Get"),
      GetType().GetField("Set")
    };
    StoreInvokerFieldNames();
    m_DelegateTypes = new System.Type[2]
    {
      typeof (vp_Value<>.Getter<>),
      typeof (vp_Value<>.Setter<>)
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "get_OnValue_",
        0
      },
      {
        "set_OnValue_",
        1
      }
    };
  }

  public delegate T Getter<T>();

  public delegate void Setter<T>(T o);
}
