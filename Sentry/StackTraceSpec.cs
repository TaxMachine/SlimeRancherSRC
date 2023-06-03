// Decompiled with JetBrains decompiler
// Type: Sentry.StackTraceSpec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Sentry
{
  [Serializable]
  public class StackTraceSpec
  {
    public string filename;
    public string function;
    public string module = "";
    public int lineno;
    public bool in_app;

    public StackTraceSpec(string filename, string function, int lineNo, bool inApp)
    {
      this.filename = filename;
      this.function = function;
      lineno = lineNo;
      in_app = inApp;
    }
  }
}
