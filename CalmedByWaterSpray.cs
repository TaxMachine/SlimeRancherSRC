// Decompiled with JetBrains decompiler
// Type: CalmedByWaterSpray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CalmedByWaterSpray : MonoBehaviour, LiquidConsumer
{
  public float agitationReduction = 0.1f;
  public float calmedHours = 0.3333f;
  private double calmedUntil;
  private SlimeEmotions emotions;
  private TimeDirector timeDir;

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    emotions.Adjust(SlimeEmotions.Emotion.AGITATION, -agitationReduction * units);
    calmedUntil = timeDir.HoursFromNow(calmedHours);
  }

  public bool IsCalmed() => !timeDir.HasReached(calmedUntil);
}
