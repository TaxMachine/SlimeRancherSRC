// Decompiled with JetBrains decompiler
// Type: vp_EventDump
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;

public class vp_EventDump
{
  public static string Dump(vp_EventHandler handler, string[] eventTypes)
  {
    string str = "";
    foreach (string eventType in eventTypes)
    {
      switch (eventType)
      {
        case "vp_Message":
          str += DumpEventsOfType("vp_Message", eventTypes.Length > 1 ? "MESSAGES:\n\n" : "", handler);
          break;
        case "vp_Attempt":
          str += DumpEventsOfType("vp_Attempt", eventTypes.Length > 1 ? "ATTEMPTS:\n\n" : "", handler);
          break;
        case "vp_Value":
          str += DumpEventsOfType("vp_Value", eventTypes.Length > 1 ? "VALUES:\n\n" : "", handler);
          break;
        case "vp_Activity":
          str += DumpEventsOfType("vp_Activity", eventTypes.Length > 1 ? "ACTIVITIES:\n\n" : "", handler);
          break;
      }
    }
    return str;
  }

  private static string DumpEventsOfType(string type, string caption, vp_EventHandler handler) => "Dumping Disabled";

  private static string DumpEventListeners(object e, string[] invokers)
  {
    Type type = e.GetType();
    string str1 = "";
    foreach (string invoker in invokers)
    {
      FieldInfo field = type.GetField(invoker);
      if (field == null)
        return "";
      Delegate @delegate = (Delegate) field.GetValue(e);
      string[] array = null;
      if ((object) @delegate != null)
        array = GetMethodNames(@delegate.GetInvocationList());
      string str2 = str1 + "\t\t\t\t";
      if (type.ToString().Contains("vp_Value"))
      {
        switch (invoker)
        {
          case "Get":
            str1 = str2 + "Get";
            break;
          case "Set":
            str1 = str2 + "Set";
            break;
          default:
            str1 = str2 + "Unsupported listener: ";
            break;
        }
      }
      else if (type.ToString().Contains("vp_Attempt"))
        str1 = str2 + "Try";
      else if (type.ToString().Contains("vp_Message"))
        str1 = str2 + "Send";
      else if (type.ToString().Contains("vp_Activity"))
      {
        switch (invoker)
        {
          case "StartConditions":
            str1 = str2 + "TryStart";
            break;
          case "StopConditions":
            str1 = str2 + "TryStop";
            break;
          case "StartCallbacks":
            str1 = str2 + "Start";
            break;
          case "StopCallbacks":
            str1 = str2 + "Stop";
            break;
          case "FailStartCallbacks":
            str1 = str2 + "FailStart";
            break;
          case "FailStopCallbacks":
            str1 = str2 + "FailStop";
            break;
          default:
            str1 = str2 + "Unsupported listener: ";
            break;
        }
      }
      else
        str1 = str2 + "Unsupported listener";
      if (array != null)
        str1 = (array.Length <= 2 ? str1 + ": " : str1 + ":\n") + DumpDelegateNames(array);
    }
    return str1;
  }

  private static string[] GetMethodNames(Delegate[] list)
  {
    list = RemoveDelegatesFromList(list);
    string[] methodNames = new string[list.Length];
    if (list.Length == 1)
    {
      methodNames[0] = (list[0].Target == null ? "" : "(" + list[0].Target + ") ") + list[0].Method.Name;
    }
    else
    {
      for (int index = 1; index < list.Length; ++index)
        methodNames[index] = (list[index].Target == null ? "" : "(" + list[index].Target + ") ") + list[index].Method.Name;
    }
    return methodNames;
  }

  private static Delegate[] RemoveDelegatesFromList(Delegate[] list)
  {
    List<Delegate> delegateList = new List<Delegate>(list);
    for (int index = delegateList.Count - 1; index > -1; --index)
    {
      if ((object) delegateList[index] != null && delegateList[index].Method.Name.Contains("m_"))
        delegateList.RemoveAt(index);
    }
    return delegateList.ToArray();
  }

  private static string DumpDelegateNames(string[] array)
  {
    string str1 = "";
    foreach (string str2 in array)
    {
      if (!string.IsNullOrEmpty(str2))
        str1 = str1 + (array.Length > 2 ? "\t\t\t\t\t\t\t" : "") + str2 + "\n";
    }
    return str1;
  }
}
