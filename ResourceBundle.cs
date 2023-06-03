// Decompiled with JetBrains decompiler
// Type: ResourceBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class ResourceBundle
{
  private Dictionary<string, string> dict;
  private static readonly Dictionary<string, string> UNESCAPE_DICTIONARY = new Dictionary<string, string>()
  {
    {
      "\\=",
      "="
    },
    {
      "\\n",
      "\n"
    },
    {
      "\\u00AD",
      "\u00AD"
    },
    {
      "&bsol;",
      "\\"
    }
  };

  public ResourceBundle(Dictionary<string, string> dict) => this.dict = dict;

  public ICollection<string> GetKeys() => dict.Keys;

  public string GetString(string key)
  {
    string str;
    dict.TryGetValue(key, out str);
    return str;
  }

  public static ResourceBundle GetBundle(
    string prefix,
    string path,
    CultureInfo culture,
    string defaultLang)
  {
    return new ResourceBundle(LoadFromResources(prefix + "/" + culture.Name + "/" + path, prefix + "/" + culture.TwoLetterISOLanguageName + "/" + path, prefix + "/" + defaultLang + "/" + path));
  }

  public static Dictionary<string, string> LoadFromResources(
    string culturePath,
    string langPath,
    string defaultPath)
  {
    TextAsset file = Resources.Load(culturePath, typeof (TextAsset)) as TextAsset;
    if (file == null)
      file = Resources.Load(langPath, typeof (TextAsset)) as TextAsset;
    if (file == null)
      file = Resources.Load(defaultPath, typeof (TextAsset)) as TextAsset;
    if (!(file == null))
      return LoadFromTextAsset(file);
    Log.Warning("Failed to read file.", nameof (culturePath), culturePath, nameof (langPath), langPath, nameof (defaultPath), defaultPath);
    return new Dictionary<string, string>();
  }

  public static Dictionary<string, string> LoadFromTextAsset(TextAsset file) => LoadFromText(file.name, file.text);

  public static Dictionary<string, string> LoadFromText(string path, string text)
  {
    List<string> stringList = new List<string>();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string str1 = Regex.Replace(text, "\\\\(\\r\\n|\\n|\\r)[ \\t]*", "");
    char[] chArray = new char[1]{ '\n' };
    foreach (string str2 in str1.Split(chArray))
      stringList.Add(str2);
    if (stringList == null)
    {
      Log.Warning("Resource is empty. '" + path + "'");
      return new Dictionary<string, string>();
    }
    foreach (string input in stringList)
    {
      if (input.Length > 1 && input[0] != '#')
      {
        string[] strArray = Regex.Split(input, "(?<!(?<!\\\\)*\\\\)\\=");
        if (strArray.Length != 2)
          Log.Warning("Illegal resource bundle line", nameof (path), path, "line", input);
        else
          dictionary[Unescape(strArray[0]).Trim()] = Unescape(strArray[1]).Trim();
      }
    }
    return dictionary;
  }

  private static string Unescape(string s)
  {
    foreach (KeyValuePair<string, string> unescape in UNESCAPE_DICTIONARY)
      s = s.Replace(unescape.Key, unescape.Value);
    return s;
  }
}
