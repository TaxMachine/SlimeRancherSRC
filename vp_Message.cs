// Decompiled with JetBrains decompiler
// Type: vp_Message
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public class vp_Message : vp_Event
{
  public Sender Send;

  protected static void Empty()
  {
  }

  public vp_Message(string name, System.Type eventArgumentType = null, System.Type eventReturnType = null)
    : base(name, eventArgumentType, eventReturnType)
  {
    InitFields();
    EventType = vp_EventType.Message;
  }

  protected override void InitFields()
  {
    m_Fields = new FieldInfo[1]
    {
      GetType().GetField("Send")
    };
    StoreInvokerFieldNames();
    m_DefaultMethods = new MethodInfo[1]
    {
      GetType().GetMethod("Empty")
    };
    m_DelegateTypes = new System.Type[1]
    {
      typeof (Sender)
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "OnMessage_",
        0
      }
    };
    Send = Empty;
  }

  public void Register(Sender sender)
  {
    Send += sender;
    Refresh();
  }

  public delegate void Sender();
}
