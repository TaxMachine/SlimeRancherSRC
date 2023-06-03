// Decompiled with JetBrains decompiler
// Type: PlayerRanchnessTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanchnessTracker : MonoBehaviour
{
  private bool lastOnHomeRanch;
  private RegionMember member;
  private AchievementsDirector achieveDir;
  private TimeDirector timeDir;
  private TutorialDirector tutDir;

  public void Awake()
  {
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    member = GetComponent<RegionMember>();
    member.regionsChanged += InitSectorsChanged;
  }

  public void OnDestroy() => member.regionsChanged -= InitSectorsChanged;

  private void InitSectorsChanged(List<Region> left, List<Region> joined)
  {
    lastOnHomeRanch = CellDirector.IsOnHomeRanch(member);
    member.regionsChanged += (left2, joined2) =>
    {
      bool flag = CellDirector.IsOnHomeRanch(member);
      if (!flag && lastOnHomeRanch)
      {
        achieveDir.SetStat(AchievementsDirector.GameDoubleStat.LAST_LEFT_RANCH, timeDir.WorldTime());
        tutDir.OnLeftRanch();
      }
      else if (flag && !lastOnHomeRanch)
      {
        achieveDir.SetStat(AchievementsDirector.GameDoubleStat.LAST_ENTERED_RANCH, timeDir.WorldTime());
        tutDir.OnEnteredRanch();
      }
      lastOnHomeRanch = flag;
    };
    member.regionsChanged -= InitSectorsChanged;
  }
}
