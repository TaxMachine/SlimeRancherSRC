// Decompiled with JetBrains decompiler
// Type: MessageUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using System.Text.RegularExpressions;

public class MessageUtil
{
  public const string QUAL_PREFIX = "%";
  public const string QUAL_SEP = ":";
  private const string TAINT_CHAR = "~";

  public static string Taint(object text) => "~" + text;

  public static bool IsTainted(string text) => text != null && text.StartsWith("~");

  public static string Untaint(string text) => !IsTainted(text) ? text : text.Substring("~".Length);

  public static string Compose(string key, params object[] args)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(key);
    stringBuilder.Append('|');
    for (int index1 = 0; index1 < args.Length; ++index1)
    {
      if (index1 > 0)
        stringBuilder.Append('|');
      string str = args[index1] == null ? "" : Convert.ToString(args[index1]);
      int length = str.Length;
      for (int index2 = 0; index2 < length; ++index2)
      {
        char ch = str[index2];
        switch (ch)
        {
          case '\\':
            stringBuilder.Append("\\\\");
            break;
          case '|':
            stringBuilder.Append("\\!");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public static string Compose(string key, params string[] args) => Compose(key, (object[]) args);

  public static string Unescape(string value)
  {
    if (value.IndexOf('\\') == -1)
      return value;
    StringBuilder stringBuilder = new StringBuilder();
    int length = value.Length;
    for (int index = 0; index < length; ++index)
    {
      char ch1 = value[index];
      if (ch1 != '\\' || index == length - 1)
      {
        stringBuilder.Append(ch1);
      }
      else
      {
        char ch2 = value[++index];
        stringBuilder.Append(ch2 == '!' ? '|' : ch2);
      }
    }
    return stringBuilder.ToString();
  }

  public static string Tcompose(string key, params object[] args)
  {
    int length = args.Length;
    string[] strArray = new string[length];
    for (int index = 0; index < length; ++index)
      strArray[index] = Taint(args[index]);
    return Compose(key, (object[]) strArray);
  }

  public static string Tcompose(string key, params string[] args)
  {
    int index = 0;
    for (int length = args.Length; index < length; ++index)
      args[index] = Taint(args[index]);
    return Compose(key, args);
  }

  public static string[] decompose(string compoundKey)
  {
    string[] strArray = Regex.Split(compoundKey, "\\|");
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = Unescape(strArray[index]);
    return strArray;
  }

  public static string Qualify(string bundle, string key)
  {
    if (bundle.IndexOf("%") != -1 || bundle.IndexOf(":") != -1)
      throw new ArgumentException("Message bundle may not contain '%' or ':' [bundle=" + bundle + ", key=" + key + "]");
    return "%" + bundle + ":" + key;
  }

  public static string GetBundle(string qualifiedKey)
  {
    int num = qualifiedKey.StartsWith("%") ? qualifiedKey.IndexOf(":") : throw new ArgumentException(qualifiedKey + " is not a fully qualified message key.");
    if (num == -1)
      throw new ArgumentException(qualifiedKey + " is not a valid fully qualified key.");
    return qualifiedKey.Substring("%".Length, num - "%".Length);
  }

  public static string GetUnqualifiedKey(string qualifiedKey)
  {
    int num = qualifiedKey.StartsWith("%") ? qualifiedKey.IndexOf(":") : throw new ArgumentException(qualifiedKey + " is not a fully qualified message key.");
    return num != -1 ? qualifiedKey.Substring(num + 1) : throw new ArgumentException(qualifiedKey + " is not a valid fully qualified key.");
  }
}
