// Decompiled with JetBrains decompiler
// Type: SlimeGateActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeGateActivator : MonoBehaviour
{
  public AccessDoor gateDoor;

  public void Activate()
  {
    if (!SRSingleton<SceneContext>.Instance.PlayerState.SpendKey())
      return;
    gateDoor.CurrState = AccessDoor.State.OPEN;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.OPENED_SLIME_GATES, 1);
    AnalyticsUtil.CustomEvent("GateOpened", new Dictionary<string, object>()
    {
      {
        "name",
        name
      }
    });
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow();
  }
}
