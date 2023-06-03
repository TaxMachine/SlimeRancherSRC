// Decompiled with JetBrains decompiler
// Type: vp_Message`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public class vp_Message<V, VResult> : vp_Message
{
  public Sender<V, VResult> Send;

  protected static TResult Empty<T, TResult>(T value) => default (TResult);

  protected VResult EmptySender(V value) => default (VResult);

  public vp_Message(string name)
    : base(name, typeof (V), typeof (VResult))
  {
  }

  protected override void InitFields()
  {
    m_Fields = new FieldInfo[1]
    {
      GetType().GetField("Send")
    };
    StoreInvokerFieldNames();
    m_DelegateTypes = new System.Type[1]
    {
      typeof (vp_Message<,>.Sender<,>)
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "OnMessage_",
        0
      }
    };
    Send = EmptySender;
  }

  public delegate TResult Sender<T, TResult>(T value);
}
