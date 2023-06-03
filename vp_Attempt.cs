// Decompiled with JetBrains decompiler
// Type: vp_Attempt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;

public class vp_Attempt : vp_Event
{
  public Tryer Try;

  public static bool AlwaysOK() => true;

  public vp_Attempt(string name, System.Type eventArgumentType = null)
    : base(name, eventArgumentType, typeof (bool))
  {
    InitFields();
    EventType = vp_EventType.Attempt;
  }

  protected override void InitFields()
  {
    m_Fields = new FieldInfo[1]
    {
      GetType().GetField("Try")
    };
    StoreInvokerFieldNames();
    m_DefaultMethods = new MethodInfo[1]
    {
      GetType().GetMethod("AlwaysOK")
    };
    m_DelegateTypes = new System.Type[1]
    {
      typeof (Tryer)
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "OnAttempt_",
        0
      }
    };
    Try = AlwaysOK;
  }

  public delegate bool Tryer();
}
