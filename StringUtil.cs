// Decompiled with JetBrains decompiler
// Type: StringUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class StringUtil
{
  public static string ToString(object arg)
  {
    switch (arg)
    {
      case string[] _:
        return string.Join(",", (string[]) arg);
      case object[] _:
        return string.Join(",", ((object[]) arg).Select(XlateText => XlateText.ToString()).ToArray());
      default:
        return Convert.ToString(arg);
    }
  }

  public static string Pad(int val, int numDigits)
  {
    string str = string.Concat(val);
    while (str.Length < numDigits)
      str = "0" + str;
    return str;
  }
}
