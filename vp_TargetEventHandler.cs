// Decompiled with JetBrains decompiler
// Type: vp_TargetEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

internal static class vp_TargetEventHandler
{
  private static List<Dictionary<object, Dictionary<string, Delegate>>> m_TargetDict;

  private static List<Dictionary<object, Dictionary<string, Delegate>>> TargetDict
  {
    get
    {
      if (m_TargetDict == null)
      {
        m_TargetDict = new List<Dictionary<object, Dictionary<string, Delegate>>>(100);
        for (int index = 0; index < 8; ++index)
          m_TargetDict.Add(new Dictionary<object, Dictionary<string, Delegate>>(100));
      }
      return m_TargetDict;
    }
  }

  public static void Register(object target, string eventName, Delegate callback, int dictionary)
  {
    if (target == null)
      Debug.LogWarning("Warning: (" + vp_Utility.GetErrorLocation(2) + " -> vp_TargetEvent.Register) Target object is null.");
    else if (string.IsNullOrEmpty(eventName))
      Debug.LogWarning("Warning: (" + vp_Utility.GetErrorLocation(2) + " -> vp_TargetEvent.Register) Event name is null or empty.");
    else if ((object) callback == null)
      Debug.LogWarning("Warning: (" + vp_Utility.GetErrorLocation(2) + " -> vp_TargetEvent.Register) Callback is null.");
    else if (callback.Method.Name.StartsWith("<"))
    {
      Debug.LogWarning("Warning: (" + vp_Utility.GetErrorLocation(2) + " -> vp_TargetEvent.Register) Target events can only be registered to declared methods.");
    }
    else
    {
      if (!TargetDict[dictionary].ContainsKey(target))
        TargetDict[dictionary].Add(target, new Dictionary<string, Delegate>(100));
      Dictionary<string, Delegate> dictionary1;
      TargetDict[dictionary].TryGetValue(target, out dictionary1);
      Delegate a;
      while (true)
      {
        dictionary1.TryGetValue(eventName, out a);
        if ((object) a != null)
        {
          if (a.GetType() != callback.GetType())
            eventName += "_";
          else
            goto label_15;
        }
        else
          break;
      }
      dictionary1.Add(eventName, callback);
      return;
label_15:
      callback = Delegate.Combine(a, callback);
      if ((object) callback == null)
        return;
      dictionary1.Remove(eventName);
      dictionary1.Add(eventName, callback);
    }
  }

  public static void Unregister(object target, string eventName = null, Delegate callback = null)
  {
    if (eventName == null && (object) callback != null || (object) callback == null && eventName != null)
      return;
    for (int dictionary = 0; dictionary < 8; ++dictionary)
      Unregister(target, dictionary, eventName, callback);
  }

  public static void Unregister(Component component)
  {
    if (component == null)
      return;
    for (int dictionary = 0; dictionary < 8; ++dictionary)
      Unregister(dictionary, component);
  }

  private static void Unregister(int dictionary, Component component)
  {
    if (component == null)
      return;
    Dictionary<string, Delegate> dictionary1;
    if (TargetDict[dictionary].TryGetValue(component, out dictionary1))
      TargetDict[dictionary].Remove(component);
    object transform = component.transform;
    if (transform == null || !TargetDict[dictionary].TryGetValue(transform, out dictionary1))
      return;
    foreach (string key in new List<string>(dictionary1.Keys))
    {
      Delegate source;
      if (key != null && dictionary1.TryGetValue(key, out source) && (object) source != null)
      {
        Delegate[] invocationList = source.GetInvocationList();
        if (invocationList != null && invocationList.Length >= 1)
        {
          for (int index = invocationList.Length - 1; index > -1; --index)
          {
            if (invocationList[index].Target == component)
            {
              dictionary1.Remove(key);
              Delegate @delegate = Delegate.Remove(source, invocationList[index]);
              if (@delegate.GetInvocationList().Length != 0)
                dictionary1.Add(key, @delegate);
            }
          }
        }
      }
    }
  }

  private static void Unregister(
    object target,
    int dictionary,
    string eventName,
    Delegate callback)
  {
    Dictionary<string, Delegate> key;
    if (target == null || !TargetDict[dictionary].TryGetValue(target, out key) || key == null || key.Count == 0)
      return;
    if (eventName == null && (object) callback == null)
    {
      TargetDict[dictionary].Remove(key);
    }
    else
    {
      Delegate source;
      if (!key.TryGetValue(eventName, out source))
        return;
      if ((object) source != null)
      {
        key.Remove(eventName);
        source = Delegate.Remove(source, callback);
        if ((object) source != null && source.GetInvocationList() != null)
          key.Add(eventName, source);
      }
      else
        key.Remove(eventName);
      if (key.Count > 0)
        return;
      TargetDict[dictionary].Remove(target);
    }
  }

  public static void UnregisterAll() => m_TargetDict = null;

  public static Delegate GetCallback(
    object target,
    string eventName,
    bool upwards,
    int d,
    vp_TargetEventOptions options)
  {
    if (target == null)
      return null;
    if (string.IsNullOrEmpty(eventName))
      return null;
    Delegate callback;
    do
    {
      callback = null;
      if ((options & vp_TargetEventOptions.IncludeInactive) != vp_TargetEventOptions.IncludeInactive)
      {
        GameObject gameObject = target as GameObject;
        if (gameObject != null)
        {
          if (!vp_Utility.IsActive(gameObject))
          {
            if (!upwards)
              return null;
            goto label_17;
          }
        }
        else
        {
          Behaviour behaviour = target as Behaviour;
          if (behaviour != null && (!behaviour.enabled || !vp_Utility.IsActive(behaviour.gameObject)))
          {
            if (!upwards)
              return null;
            goto label_17;
          }
        }
      }
      Dictionary<string, Delegate> dictionary = null;
      if (!TargetDict[d].TryGetValue(target, out dictionary))
      {
        if (!upwards)
          return null;
      }
      else if (!dictionary.TryGetValue(eventName, out callback) && !upwards)
        return null;
label_17:
      if ((object) callback == null & upwards)
        target = vp_Utility.GetParent(target as Component);
      else
        break;
    }
    while (target != null);
    return callback;
  }

  public static void OnNoReceiver(string eventName, vp_TargetEventOptions options)
  {
    if ((options & vp_TargetEventOptions.RequireReceiver) != vp_TargetEventOptions.RequireReceiver)
      return;
    Debug.LogError("Error: (" + vp_Utility.GetErrorLocation(2) + ") vp_TargetEvent '" + eventName + "' has no receiver!");
  }

  public static string Dump()
  {
    Dictionary<object, string> dictionary1 = new Dictionary<object, string>();
    foreach (Dictionary<object, Dictionary<string, Delegate>> dictionary2 in TargetDict)
    {
      foreach (object key1 in dictionary2.Keys)
      {
        string str1 = "";
        if (key1 != null)
        {
          Dictionary<string, Delegate> dictionary3;
          if (dictionary2.TryGetValue(key1, out dictionary3))
          {
            foreach (string key2 in dictionary3.Keys)
            {
              str1 = str1 + "        \"" + key2 + "\" -> ";
              bool flag = false;
              Delegate @delegate;
              if (!string.IsNullOrEmpty(key2) && dictionary3.TryGetValue(key2, out @delegate))
              {
                if (@delegate.GetInvocationList().Length > 1)
                {
                  flag = true;
                  str1 += "\n";
                }
                foreach (Delegate invocation in @delegate.GetInvocationList())
                {
                  str1 = str1 + (flag ? "                        " : (object) "") + invocation.Method.ReflectedType + ".cs -> ";
                  string str2 = "";
                  foreach (ParameterInfo parameter in invocation.Method.GetParameters())
                    str2 = str2 + vp_Utility.GetTypeAlias(parameter.ParameterType) + " " + parameter.Name + ", ";
                  if (str2.Length > 0)
                    str2 = str2.Remove(str2.LastIndexOf(", "));
                  str1 = str1 + vp_Utility.GetTypeAlias(invocation.Method.ReturnType) + " ";
                  if (invocation.Method.Name.Contains("m_"))
                  {
                    string str3 = invocation.Method.Name.TrimStart('<');
                    string str4 = str3.Remove(str3.IndexOf('>'));
                    str1 = str1 + str4 + " -> delegate";
                  }
                  else
                    str1 += invocation.Method.Name;
                  str1 = str1 + "(" + str2 + ")\n";
                }
              }
            }
          }
          string str5;
          if (!dictionary1.TryGetValue(key1, out str5))
          {
            dictionary1.Add(key1, str1);
          }
          else
          {
            dictionary1.Remove(key1);
            dictionary1.Add(key1, str5 + str1);
          }
        }
      }
    }
    string str6 = "--- TARGET EVENT DUMP ---\n\n";
    foreach (object key in dictionary1.Keys)
    {
      if (key != null)
      {
        str6 = str6 + key.ToString() + ":\n";
        string str7;
        if (dictionary1.TryGetValue(key, out str7))
          str6 += str7;
      }
    }
    return str6;
  }
}
