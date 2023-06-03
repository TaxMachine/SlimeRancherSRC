// Decompiled with JetBrains decompiler
// Type: AnalyticsUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnalyticsUtil
{
  public const string EVENT_SESSION_ENDED = "SessionEnded";
  public const string NULL = "null";
  private static List<IListener> Listeners = new List<IListener>();

  public static void CustomEvent(
    string eventName,
    IDictionary<string, object> customEventData = null,
    bool includeDefaultEventData = true)
  {
    Dictionary<string, object> eventData = customEventData != null ? new Dictionary<string, object>(customEventData) : new Dictionary<string, object>();
    if (includeDefaultEventData)
    {
      foreach (KeyValuePair<string, object> keyValuePair in GetDefaultEventData())
      {
        if (!eventData.ContainsKey(keyValuePair.Key))
          eventData[keyValuePair.Key] = keyValuePair.Value;
      }
    }
    Listeners.ForEach(instance => instance.CustomEvent(eventName, eventData));
  }

  private static Dictionary<string, object> GetDefaultEventData()
  {
    try
    {
      return new Dictionary<string, object>()
      {
        {
          "Game.Id",
          SRSingleton<GameContext>.Instance.AutoSaveDirector.SavedGame.GetName()
        },
        {
          "Game.Mode",
          SRSingleton<SceneContext>.Instance.GameModel.currGameMode
        },
        {
          "Player.Position",
          GetEventData(SRSingleton<SceneContext>.Instance.Player.transform.position)
        },
        {
          "Player.Region",
          SRSingleton<SceneContext>.Instance.RegionRegistry.GetCurrentRegionSetId()
        },
        {
          "Player.Zone",
          SRSingleton<SceneContext>.Instance.Player.GetComponent<PlayerZoneTracker>().GetCurrentZone()
        },
        {
          "Time.WorldTime",
          GetEventData(SRSingleton<SceneContext>.Instance.TimeDirector.WorldTime(), 0)
        }
      };
    }
    catch (Exception ex)
    {
      Log.Warning("Failed to get default analytics event metadata.", "exception", ex);
    }
    return new Dictionary<string, object>();
  }

  public static string GetEventData(GameObject gameObject)
  {
    if (gameObject == null)
      return "null";
    Identifiable componentInParent = gameObject.GetComponentInParent<Identifiable>();
    return componentInParent != null ? componentInParent.id.ToString() : gameObject.name;
  }

  public static string GetEventData(Vector3 vector3) => string.Format("{{\"x\":{0},\"y\":{1},\"z\":{2}}}", GetEventData(vector3.x, 2), GetEventData(vector3.y, 2), GetEventData(vector3.z, 2));

  public static string GetEventData(double value, int decimals = 2) => value.ToString(string.Format("F{0}", decimals));

  public static string GetEventData(float value, int decimals = 2) => value.ToString(string.Format("F{0}", decimals));

  private interface IListener
  {
    void CustomEvent(string eventName, IDictionary<string, object> eventData);
  }
}
