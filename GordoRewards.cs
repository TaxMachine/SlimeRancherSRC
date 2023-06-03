// Decompiled with JetBrains decompiler
// Type: GordoRewards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class GordoRewards : GordoRewardsBase
{
  [Tooltip("The default rewards to provide on popping the gordo")]
  public GameObject[] rewardPrefabs;
  [Tooltip("A set of overrides for different game modes on popping the gordo")]
  public RewardOverride[] rewardOverrides;

  protected override IEnumerable<GameObject> SelectActiveRewardPrefabs()
  {
    PlayerState.GameMode currGameMode = SRSingleton<SceneContext>.Instance.GameModel.currGameMode;
    foreach (RewardOverride rewardOverride in rewardOverrides)
    {
      if (rewardOverride.gameMode == currGameMode)
        return rewardOverride.rewardPrefabs;
    }
    return rewardPrefabs;
  }

  [Serializable]
  public class RewardOverride
  {
    public PlayerState.GameMode gameMode;
    public GameObject[] rewardPrefabs;
  }
}
