// Decompiled with JetBrains decompiler
// Type: Sentry.ExceptionSpec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Sentry
{
  [Serializable]
  public class ExceptionSpec
  {
    public string type;
    public string value;
    public StackTraceContainer stacktrace;

    public ExceptionSpec(string type, string value, List<StackTraceSpec> stacktrace)
    {
      this.type = type;
      this.value = value;
      this.stacktrace = new StackTraceContainer(stacktrace);
    }
  }
}
