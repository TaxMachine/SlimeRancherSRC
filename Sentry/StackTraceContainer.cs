// Decompiled with JetBrains decompiler
// Type: Sentry.StackTraceContainer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Sentry
{
  [Serializable]
  public class StackTraceContainer
  {
    public List<StackTraceSpec> frames;

    public StackTraceContainer(List<StackTraceSpec> frames) => this.frames = frames;
  }
}
