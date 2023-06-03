// Decompiled with JetBrains decompiler
// Type: Sentry.SentryEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Sentry
{
  [Serializable]
  public class SentryEvent
  {
    public string event_id;
    public string message;
    public string timestamp;
    public string logger;
    public string level;
    public string platform = "csharp";
    public string release;
    public Context contexts;
    public SdkVersion sdk = new SdkVersion();
    public List<Breadcrumb> breadcrumbs;
    public User user = new User();
    public Tags tags;

    public SentryEvent(string message, List<Breadcrumb> breadcrumbs = null)
    {
      event_id = Guid.NewGuid().ToString("N");
      this.message = GetDescription(message);
      timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
      level = "error";
      this.breadcrumbs = breadcrumbs;
      contexts = new Context();
      release = GetVersion();
      tags = new Tags();
      tags.cultureName = CultureInfo.CurrentCulture.Name;
      if (SRSingleton<GameContext>.Instance != null && SRSingleton<GameContext>.Instance.MessageDirector != null)
        tags.gameLanguage = SRSingleton<GameContext>.Instance.MessageDirector.GetCurrentLanguageCode();
      tags.isModded = SystemContext.IsModded;
    }

    private static string GetDescription(string description)
    {
      try
      {
        string str = "NONE";
        if (SRSingleton<SceneContext>.Instance != null && SRSingleton<SceneContext>.Instance.Player != null)
        {
          Transform transform = SRSingleton<SceneContext>.Instance.Player.transform;
          str = transform.position.ToString() + " Facing: " + transform.eulerAngles;
        }
        return SRSingleton<GameContext>.Instance != null ? string.Format("{0}\n\nVersion: {1}\nPosition: {2}\n\nLog:\n{3}", description, GetVersion(), str, SRSingleton<GameContext>.Instance.LogText) : string.Format("{0}\n\nVersion: {1}\nPosition: {2}\n\nLog:\n{3}", description, GetVersion(), str, "Log text not available.");
      }
      catch (Exception ex)
      {
        return string.Format("Caught exception while getting description for Sentry: {0}", ex.Message);
      }
    }

    private static string GetVersion() => SRSingleton<GameContext>.Instance != null ? SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("build").Xlate("m.version") : Application.version;
  }
}
