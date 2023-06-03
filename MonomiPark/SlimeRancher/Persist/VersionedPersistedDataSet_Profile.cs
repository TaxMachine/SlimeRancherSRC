// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.VersionedPersistedDataSet_Profile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public class VersionedPersistedDataSet_Profile
  {
    public static readonly Dictionary<uint, UpgradeAction> UpgradeActions = new Dictionary<uint, UpgradeAction>()
    {
      {
        6U,
        director =>
        {
          foreach (GameData.Summary summary in director.AvailableGamesByGameName().Select(kv => kv.Value.FirstOrDefault(s => !s.isInvalid)).Where(s => s != null))
            AnalyticsUtil.CustomEvent("SessionEnded", new Dictionary<string, object>()
            {
              {
                "Game.Id",
                summary.name
              },
              {
                "Game.Mode",
                summary.gameMode
              },
              {
                "Time.WorldTime",
                AnalyticsUtil.GetEventData(summary.day * 86400f, 0)
              }
            }, false);
        }
      }
    };

    public delegate void UpgradeAction(AutoSaveDirector director);
  }
}
