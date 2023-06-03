// Decompiled with JetBrains decompiler
// Type: vp_Event
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public abstract class vp_Event
{
  protected string m_Name;
  protected System.Type m_ArgumentType;
  protected System.Type m_ReturnType;
  protected FieldInfo[] m_Fields;
  protected System.Type[] m_DelegateTypes;
  protected MethodInfo[] m_DefaultMethods;
  public string[] InvokerFieldNames;
  public Dictionary<string, int> Prefixes;
  public vp_EventType EventType;

  public string EventName => m_Name;

  public System.Type ArgumentType => m_ArgumentType;

  public System.Type ReturnType => m_ReturnType;

  protected abstract void InitFields();

  public vp_Event(string name = "", System.Type eventArgumentType = null, System.Type eventReturnType = null)
  {
    EventType = vp_EventType.Event;
    m_ArgumentType = eventArgumentType;
    m_ReturnType = !(eventReturnType == null) ? eventReturnType : typeof (void);
    m_Name = name;
  }

  protected void StoreInvokerFieldNames()
  {
    InvokerFieldNames = new string[m_Fields.Length];
    for (int index = 0; index < m_Fields.Length; ++index)
      InvokerFieldNames[index] = m_Fields[index].Name;
  }

  protected void Refresh()
  {
  }
}
