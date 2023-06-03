// Decompiled with JetBrains decompiler
// Type: Sentry.Breadcrumb
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Sentry
{
  [Serializable]
  public class Breadcrumb
  {
    public const int MaxBreadcrumbs = 100;
    public string timestamp;
    public string message;

    public Breadcrumb(string timestamp, string message)
    {
      this.timestamp = timestamp;
      this.message = message;
    }

    public static List<Breadcrumb> CombineBreadcrumbs(
      Breadcrumb[] breadcrumbs,
      int index,
      int number)
    {
      List<Breadcrumb> breadcrumbList = new List<Breadcrumb>(number);
      int num = (index + 100 - number) % 100;
      for (int index1 = 0; index1 < number; ++index1)
        breadcrumbList.Add(breadcrumbs[(index1 + num) % 100]);
      return breadcrumbList;
    }
  }
}
