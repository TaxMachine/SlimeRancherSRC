// Decompiled with JetBrains decompiler
// Type: Discord
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using RichPresence;
using System;

public static class Discord
{
  private static readonly DiscordRpc.EventHandlers staticEventHandlers = new DiscordRpc.EventHandlers()
  {
    readyCallback = OnReadyCallback,
    disconnectedCallback = OnDisconnectedCallback,
    errorCallback = OnErrorCallback,
    joinCallback = OnJoinCallback,
    spectateCallback = OnSpectateCallback,
    requestCallback = OnRequestCallback
  };
  private const string DISCORD_ID = "443564201349218305";
  private const string STEAM_ID = null;

  public static Handler RichPresenceHandler => RichPresenceHandlerImpl.Instance;

  static Discord()
  {
    try
    {
      DiscordRpc.Initialize("443564201349218305", ref staticEventHandlers, true, null);
      SRSingleton<GameContext>.Instance.gameObject.AddComponent<UnityEventListener>();
    }
    catch (Exception ex)
    {
      Log.Error("Failed to initialize Discord.", "exception", ex);
    }
  }

  private static void OnReadyCallback(ref DiscordRpc.DiscordUser user)
  {
  }

  private static void OnDisconnectedCallback(int errorCode, string message)
  {
  }

  private static void OnErrorCallback(int errorCode, string message) => Log.Error("Discord.errorCallback", nameof (errorCode), errorCode, nameof (message), message);

  private static void OnJoinCallback(string secret)
  {
  }

  private static void OnSpectateCallback(string secret)
  {
  }

  private static void OnRequestCallback(ref DiscordRpc.DiscordUser user)
  {
  }

  private class RichPresenceHandlerImpl : Handler
  {
    public static RichPresenceHandlerImpl Instance = new RichPresenceHandlerImpl();

    public void SetRichPresence(MainMenuData data)
    {
      MessageDirector messageDirector = SRSingleton<GameContext>.Instance.MessageDirector;
      DiscordRpc.UpdatePresence(new DiscordRpc.RichPresence()
      {
        details = messageDirector.Get("global", "l.presence.in_menu"),
        largeImageKey = "main-menu-large"
      });
    }

    public void SetRichPresence(InZoneData data)
    {
      string id;
      if (!Director.TryGetZoneId(data.zone, out id))
        return;
      MessageDirector messageDirector = SRSingleton<GameContext>.Instance.MessageDirector;
      DiscordRpc.UpdatePresence(new DiscordRpc.RichPresence()
      {
        details = messageDirector.Get("global", string.Format("l.presence.{0}", id)),
        largeImageKey = string.Format("zone-{0}-large", id),
        state = string.Format("{0}, {1}", messageDirector.Get("ui", string.Format("m.gamemode_{0}", SRSingleton<SceneContext>.Instance.GameModel.currGameMode.ToString().ToLower())), SRSingleton<SceneContext>.Instance.TimeDirector.CurrDayString())
      });
    }
  }

  private class UnityEventListener : SRSingleton<UnityEventListener>
  {
    public void OnApplicationQuit()
    {
      DiscordRpc.ClearPresence();
      DiscordRpc.Shutdown();
    }

    public void Update() => DiscordRpc.RunCallbacks();
  }
}
