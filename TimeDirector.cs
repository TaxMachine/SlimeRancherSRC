// Decompiled with JetBrains decompiler
// Type: TimeDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDirector : SRBehaviour, WorldModel.Participant
{
  public float secsPerGameDay = 1440f;
  public float ffSecsPerGameDay = 5f;
  public const float START_HOUR = 9f;
  public Sprite daySprite;
  public Sprite nightSprite;
  public Sprite dawnSprite;
  public Sprite duskSprite;
  private float timeFactor;
  private float ffTimeFactor;
  private MessageBundle uiBundle;
  private float savedTimeScale;
  private int pauserCount;
  private int specialPauserCount;
  private static OnUnpauseDelegate onUnpauseDelegate;
  private vp_FPInput input;
  public const float SECS_PER_MIN = 60f;
  public const float MINS_PER_HOUR = 60f;
  public const float HOURS_PER_DAY = 24f;
  public const float MINS_PER_DAY = 1440f;
  public const float SECS_PER_DAY = 86400f;
  public const float SECS_PER_HOUR = 3600f;
  public const float MINS_PER_SEC = 0.0166666675f;
  public const float HOURS_PER_SEC = 0.000277777785f;
  public const float HOURS_PER_MIN = 0.0166666675f;
  public const float DAYS_PER_SEC = 1.15740741E-05f;
  private string dayFormatString;
  private WorldModel worldModel;
  private List<PassedTimeDelegate> passedTimeDelegates = new List<PassedTimeDelegate>();

  public event OnFastForwardChanged onFastForwardChanged;

  public void Awake()
  {
    if (pauserCount == 0)
      Time.timeScale = 1f;
    timeFactor = 86400f / secsPerGameDay;
    ffTimeFactor = 86400f / ffSecsPerGameDay;
    input = FindObjectOfType<vp_FPInput>();
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(InitBundles);
    SRSingleton<SceneContext>.Instance.GameModel.RegisterWorldParticipant(this);
  }

  public void InitModel(WorldModel worldModel)
  {
    worldModel.fastForwardUntil = new double?();
    worldModel.pauseWorldTime = false;
    ResetToStartTime(worldModel);
  }

  public void SetModel(WorldModel worldModel) => this.worldModel = worldModel;

  private void ResetToStartTime(WorldModel worldModel)
  {
    worldModel.worldTime = 32400.0;
    worldModel.lastWorldTime = new double?(worldModel.worldTime);
  }

  public void InitBundles(MessageDirector msgDir)
  {
    uiBundle = msgDir.GetBundle("ui");
    if (uiBundle == null)
      return;
    dayFormatString = uiBundle.Get("m.day");
  }

  public void OnUnpause(OnUnpauseDelegate del)
  {
    if (Time.timeScale != 0.0)
      del();
    else
      onUnpauseDelegate += del;
  }

  public void ClearOnUnpause(OnUnpauseDelegate del)
  {
    if (onUnpauseDelegate == null)
      return;
    onUnpauseDelegate -= del;
  }

  public bool ExactlyOnePauser() => pauserCount == 1;

  public bool IsFastForwarding() => worldModel.fastForwardUntil.HasValue;

  public bool HasPauser() => pauserCount > 0;

  public void InitForLevel()
  {
    if (pauserCount != 0)
      return;
    SetCursorInUnpausedState();
  }

  public void OnApplicationFocus(bool focused)
  {
    if (!focused || pauserCount != 0)
      return;
    SetCursorInUnpausedState();
  }

  private void SetCursorInUnpausedState()
  {
    if (Levels.isSpecial())
      EnableCursor(input);
    else
      DisableCursor(input);
  }

  public void Update()
  {
    if (Time.timeScale == 0.0 || Levels.isSpecialNonAlloc() || worldModel.pauseWorldTime)
      return;
    if (worldModel.fastForwardUntil.HasValue)
    {
      worldModel.worldTime += Time.deltaTime * (double) ffTimeFactor;
      if (worldModel.worldTime < worldModel.fastForwardUntil.Value)
        return;
      worldModel.worldTime = worldModel.fastForwardUntil.Value;
      worldModel.fastForwardUntil = new double?();
      if (onFastForwardChanged == null)
        return;
      onFastForwardChanged(false);
    }
    else
      worldModel.worldTime += Time.deltaTime * (double) timeFactor;
  }

  public void LateUpdate()
  {
    for (int index = passedTimeDelegates.Count - 1; index >= 0; --index)
    {
      if (OnPassedTime(passedTimeDelegates[index].time))
      {
        passedTimeDelegates[index].action();
        passedTimeDelegates.RemoveAt(index);
      }
    }
    worldModel.lastWorldTime = new double?(worldModel.worldTime);
  }

  public double DeltaWorldTime() => worldModel.lastWorldTime.HasValue ? worldModel.worldTime - worldModel.lastWorldTime.Value : 0.0;

  public void Pause(bool pauseSFX = true, bool pauseSpecialScenes = false)
  {
    int num1 = pauserCount <= 0 || Levels.isSpecial() ? (specialPauserCount > 0 ? 1 : 0) : 1;
    ++pauserCount;
    if (pauseSpecialScenes)
      ++specialPauserCount;
    bool flag = pauserCount > 0 && !Levels.isSpecial() || specialPauserCount > 0;
    if (num1 == 0 & flag)
    {
      int num2 = pauseSFX ? 1 : 0;
      savedTimeScale = Time.timeScale;
      Time.timeScale = 0.0f;
    }
    if (pauserCount <= 0)
      return;
    EnableCursor(input);
  }

  public void DisableCursor(vp_FPInput input)
  {
    if (input != null)
      input.MouseCursorForced = false;
    vp_Utility.LockCursor = true;
  }

  public void EnableCursor(vp_FPInput input)
  {
    if (input != null)
      input.MouseCursorForced = true;
    vp_Utility.LockCursor = false;
  }

  public void Unpause(bool unpauseSFX = true, bool pauseSpecialScenes = false) => StartCoroutine(DelayedUnpause(unpauseSFX, pauseSpecialScenes));

  private IEnumerator DelayedUnpause(bool unpauseSFX, bool pauseSpecialScenes = false)
  {
    yield return new WaitForEndOfFrame();
    bool flag1 = pauserCount > 0 && !Levels.isSpecial() || specialPauserCount > 0;
    --pauserCount;
    if (pauseSpecialScenes)
      --specialPauserCount;
    bool flag2 = pauserCount > 0 && !Levels.isSpecial() || specialPauserCount > 0;
    if (pauserCount < 0)
      Log.Warning("Unpause() called while already unpaused.");
    else if (flag1 && !flag2)
    {
      Time.timeScale = savedTimeScale;
      int num = unpauseSFX ? 1 : 0;
      if (onUnpauseDelegate != null)
      {
        onUnpauseDelegate();
        onUnpauseDelegate = null;
      }
    }
    if (pauserCount <= 0)
      DisableCursor(input);
  }

  public double WorldTime() => worldModel.worldTime;

  public double HoursFromNow(double hours) => worldModel.worldTime + hours * 3600.0;

  public static double HoursFromTime(float hours, double time) => time + hours * 3600.0;

  public double HoursFromNowOrStart(float hours) => Math.Max(worldModel == null ? 0.0 : worldModel.worldTime, 32400.0) + hours * 3600.0;

  public bool HasReached(double targetWorldTime) => TimeUtil.HasReached(worldModel.worldTime, targetWorldTime);

  public double TimeSince(double time) => worldModel.worldTime - time;

  public double HoursUntil(double targetWorldTime) => (targetWorldTime - worldModel.worldTime) * 0.00027777778450399637;

  public float CurrDayFraction() => DayFraction(worldModel.worldTime);

  public static float DayFraction(double time) => (float) (time % 86400.0) * 1.15740741E-05f;

  public int CurrDay() => 1 + (int) Math.Floor(worldModel.worldTime * 1.1574074051168282E-05);

  public int CurrDayAfterHour(float hour) => 1 + (int) Math.Floor((worldModel.worldTime - hour * 3600.0) * 1.1574074051168282E-05);

  public float CurrHour() => CurrDayFraction() * 24f;

  public float CurrHourOrStart() => (float) (Math.Max(worldModel.worldTime, 32400.0) % 86400.0) * 0.000277777785f;

  public int CurrTime() => (int) Math.Floor(CurrDayFraction() * 1440.0);

  public string CurrDayString() => string.Format(dayFormatString, CurrDay());

  public string CurrTimeString() => FormatTime(CurrTime());

  public Sprite CurrTimeIcon()
  {
    double num = CurrDayFraction();
    if (num < 0.20000000298023224 || num > 0.800000011920929)
      return nightSprite;
    if (num > 0.30000001192092896 && num < 0.699999988079071)
      return daySprite;
    return num > 0.5 ? duskSprite : dawnSprite;
  }

  public double GetNextHour(float hour) => GetHourAfter(0, hour);

  public double GetNextHourAtLeastHalfDay(float hour)
  {
    float num = hour - CurrHourOrStart();
    if (num < 0.0)
      num += 24f;
    return GetHourAfter(num < 12.0 ? 1 : 0, hour);
  }

  public double GetNextDawn() => GetHourAfter(0, 6f);

  public double GetNextDawnAfterNextDusk()
  {
    float num = CurrHour();
    return GetHourAfter(num < 6.0 || num > 18.0 ? 1 : 0, 6f);
  }

  public double GetHourAfter(int fullDays, float hour) => GetHourAfter(worldModel.worldTime, fullDays, hour);

  public static double GetHourAfter(double fromTime, int fullDays, float hour)
  {
    float num1 = hour / 24f;
    float num2 = DayFraction(fromTime);
    float num3 = num2 >= (double) num1 ? (float) (num1 - (double) num2 + fullDays + 1.0) : num1 - num2 + fullDays;
    return fromTime + num3 * 86400.0;
  }

  public void FastForwardTo(double fastForwardUntil)
  {
    worldModel.fastForwardUntil = new double?((long) (fastForwardUntil + 0.5));
    if (onFastForwardChanged == null)
      return;
    onFastForwardChanged(true);
  }

  public bool IsAtStart() => worldModel != null && worldModel.worldTime == 32400.0;

  public static double HoursFromStart(double hours) => (9.0 + hours) * 3600.0;

  public bool OnPassedHour(float hour)
  {
    if (worldModel.lastWorldTime.HasValue)
    {
      double? lastWorldTime = worldModel.lastWorldTime;
      double worldTime = worldModel.worldTime;
      if (!(lastWorldTime.GetValueOrDefault() >= worldTime & lastWorldTime.HasValue))
      {
        double num1 = worldModel.worldTime * 0.00027777778450399637 % 24.0;
        double num2 = worldModel.lastWorldTime.Value * 0.00027777778450399637 % 24.0;
        if (num1 >= hour && num2 < hour)
          return true;
        if (num1 >= num2)
          return false;
        return num2 < hour || hour <= num1;
      }
    }
    return false;
  }

  public bool OnPassedTime(double worldTime)
  {
    if (worldModel.lastWorldTime.HasValue)
    {
      double? lastWorldTime = worldModel.lastWorldTime;
      double num = worldTime;
      if (lastWorldTime.GetValueOrDefault() < num & lastWorldTime.HasValue)
        return worldTime <= worldModel.worldTime;
    }
    return false;
  }

  public void OnPassedTime(double time, Action action)
  {
    if (HasReached(time))
      action();
    else
      AddPassedTimeDelegate(time, action);
  }

  public void AddPassedTimeDelegate(double time, Action action) => passedTimeDelegates.Add(new PassedTimeDelegate()
  {
    time = time,
    action = action
  });

  public void RemovePassedTimeDelegate(Action action)
  {
    for (int index = passedTimeDelegates.Count - 1; index >= 0; --index)
    {
      if (passedTimeDelegates[index].action == action)
        passedTimeDelegates.RemoveAt(index);
    }
  }

  public string FormatTime(int totalMins)
  {
    int val1 = totalMins / 60;
    int val2 = totalMins % 60;
    return uiBundle.Get("l.time_hours_mins", new string[2]
    {
      StringUtil.Pad(val1, 2),
      StringUtil.Pad(val2, 2)
    });
  }

  public string FormatTimeMinutes(int? minutes) => minutes.HasValue ? FormatTime(minutes.Value) : uiBundle.Get("l.time_hours_mins_unset");

  public string FormatTimeSeconds(double? seconds) => seconds.HasValue ? FormatTime(Mathf.CeilToInt((float) seconds.Value * 0.0166666675f)) : uiBundle.Get("l.time_hours_mins_unset");

  public delegate void OnUnpauseDelegate();

  public delegate void OnFastForwardChanged(bool isFastForwarding);

  private class PassedTimeDelegate
  {
    public double time;
    public Action action;
  }
}
