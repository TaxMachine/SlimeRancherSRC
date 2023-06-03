// Decompiled with JetBrains decompiler
// Type: PinkSlimeFoodTypeTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;

public class PinkSlimeFoodTypeTracker : MonoBehaviour
{
  public void Start()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    AchievementsDirector achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    RegionMember member = GetComponent<RegionMember>();
    GetComponent<SlimeEat>().onEat += eatId =>
    {
      if (!Identifiable.IsFood(eatId) || !CellDirector.IsOnRanch(member))
        return;
      achieveDir.AddToStat(AchievementsDirector.EnumStat.PINK_SLIMES_FOOD_TYPES, eatId);
    };
  }
}
