// Decompiled with JetBrains decompiler
// Type: MessageBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBundle
{
  private MessageDirector msgDir;
  private string path;
  private ResourceBundle bundle;
  private MessageBundle parent;

  public void Init(
    MessageDirector msgDir,
    string path,
    ResourceBundle bundle,
    MessageBundle parent)
  {
    this.msgDir = msgDir;
    this.path = path;
    this.bundle = bundle;
    this.parent = parent;
  }

  public string Get(string key) => MessageUtil.IsTainted(key) ? MessageUtil.Untaint(key) : GetResourceString(key) ?? key;

  public void GetAll(string prefix, ICollection<string> messages, bool includeParent)
  {
    foreach (string key in bundle.GetKeys())
    {
      if (key.StartsWith(prefix))
        messages.Add(Get(key));
    }
    if (!includeParent || parent == null)
      return;
    parent.GetAll(prefix, messages, includeParent);
  }

  public void GetAllKeys(string prefix, ICollection<string> keys, bool includeParent)
  {
    foreach (string key in bundle.GetKeys())
    {
      if (key.StartsWith(prefix))
        keys.Add(key);
    }
    if (!includeParent || parent == null)
      return;
    parent.GetAllKeys(prefix, keys, includeParent);
  }

  public bool Exists(string key) => GetResourceString(key, false) != null;

  public string GetResourceString(string key) => GetResourceString(key, true);

  public string GetResourceString(string key, bool reportMissing)
  {
    string resourceString1 = null;
    if (bundle != null)
      resourceString1 = bundle.GetString(key);
    if (resourceString1 != null)
      return resourceString1;
    if (parent != null)
    {
      string resourceString2 = parent.GetResourceString(key, false);
      if (resourceString2 != null)
        return resourceString2;
    }
    if (reportMissing)
      Log.Warning("Missing translation message", "bundle", path, nameof (key), key);
    return null;
  }

  public string Get(string key, bool reportMissing, params object[] args)
  {
    if (key.StartsWith("%"))
      return msgDir.GetBundle(MessageUtil.GetBundle(key)).Get(MessageUtil.GetUnqualifiedKey(key), args);
    string suffix = GetSuffix(args);
    string resourceString = GetResourceString(key + suffix, false);
    if (resourceString == null)
    {
      if (suffix != "")
        resourceString = GetResourceString(key, false);
      if (resourceString == null)
      {
        if (reportMissing)
          Log.Warning("Missing translation message", "bundle", path, nameof (key), key);
        return key + StringUtil.ToString(args);
      }
    }
    try
    {
      return string.Format(resourceString, args);
    }
    catch (ArgumentException ex)
    {
      Log.Warning("Translation error: '" + ex.Message + "'", "bundle", path, nameof (key), key, "msg", resourceString, nameof (args), args, ex);
      return resourceString + StringUtil.ToString(args);
    }
  }

  public string Get(string key, params object[] args) => Get(key, true, args);

  public string Get(string key, params string[] args) => Get(key, (object[]) args);

  public string GetSuffix(object[] args)
  {
    if (args.Length != 0)
    {
      if (args[0] != null)
      {
        try
        {
          int result = 0;
          if (args[0] is int)
            result = (int) args[0];
          else if (!int.TryParse(Convert.ToString(args[0]), out result))
            return "";
          if (result == 0)
            return ".0";
          return result == 1 ? ".1" : ".n";
        }
        catch (FormatException ex)
        {
          Debug.LogWarning("Format Exception in GetSuffix");
        }
      }
    }
    return "";
  }

  public string Xlate(string compoundKey)
  {
    if (compoundKey.StartsWith("%"))
      return msgDir.GetBundle(MessageUtil.GetBundle(compoundKey)).Xlate(MessageUtil.GetUnqualifiedKey(compoundKey));
    int length = compoundKey.IndexOf('|');
    if (length == -1)
      return Get(compoundKey);
    string key = compoundKey.Substring(0, length);
    string[] strArray = compoundKey.Substring(length + 1).Split('|');
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = !MessageUtil.IsTainted(strArray[index]) ? Xlate(MessageUtil.Unescape(strArray[index])) : MessageUtil.Unescape(MessageUtil.Untaint(strArray[index]));
    return Get(key, (object[]) strArray);
  }
}
