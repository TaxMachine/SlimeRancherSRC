// Decompiled with JetBrains decompiler
// Type: Sentry.SentryExceptionEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Sentry
{
  public class SentryExceptionEvent : SentryEvent
  {
    public ExceptionContainer exception;

    public SentryExceptionEvent(
      string exceptionType,
      string exceptionValue,
      List<Breadcrumb> breadcrumbs,
      List<StackTraceSpec> stackTrace)
      : base(exceptionType, breadcrumbs)
    {
      exception = new ExceptionContainer(new List<ExceptionSpec>()
      {
        new ExceptionSpec(exceptionType, exceptionValue, stackTrace)
      });
    }
  }
}
