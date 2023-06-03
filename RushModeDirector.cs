// Decompiled with JetBrains decompiler
// Type: RushModeDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class RushModeDirector : SRBehaviour
{
  private const float WARNING_THRESHOLD_1 = 10800f;
  private const float WARNING_THRESHOLD_2 = 3600f;
  private MusicDirector musicDirector;
  private TimeDirector timeDirector;
  private PlayerState playerState;

  public void Awake()
  {
    musicDirector = SRSingleton<GameContext>.Instance.MusicDirector;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
  }

  public void Start()
  {
    if (SRSingleton<SceneContext>.Instance.GameModel.currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
    {
      enabled = false;
    }
    else
    {
      WeaponVacuum componentInChildren = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<WeaponVacuum>();
      componentInChildren.GetComponentInChildren<VacDisplayChanger>(true).SetDisplayMode(PlayerState.AmmoMode.NIMBLE_VALLEY);
      componentInChildren.GetComponentInChildren<VacDisplayTimer>(true).SetTimeSource(new TimeSourceWrapper(playerState));
      playerState.onEndGameTimeChanged += OnEndGameTimeChanged;
      OnEndGameTimeChanged();
    }
  }

  private void OnEndGameTimeChanged()
  {
    timeDirector.RemovePassedTimeDelegate(SetWarningMode_0);
    timeDirector.RemovePassedTimeDelegate(SetWarningMode_1);
    timeDirector.RemovePassedTimeDelegate(SetWarningMode_2);
    SetWarningMode_0();
    double time = playerState.GetEndGameTime().Value;
    timeDirector.OnPassedTime(time - 10800.0, SetWarningMode_1);
    timeDirector.OnPassedTime(time - 3600.0, SetWarningMode_2);
    timeDirector.OnPassedTime(time, SetWarningMode_0);
  }

  private void SetWarningMode_2() => musicDirector.SetRushWarningMode(musicDirector.rushModeWarning2Music);

  private void SetWarningMode_1() => musicDirector.SetRushWarningMode(musicDirector.rushModeWarning1Music);

  private void SetWarningMode_0() => musicDirector.SetRushWarningMode(null);

  private class TimeSourceWrapper : VacDisplayTimer.TimeSource
  {
    private PlayerState state;

    public TimeSourceWrapper(PlayerState state) => this.state = state;

    public double? GetTimeRemaining() => state.GetEndGameTimeRemaining();

    public double? GetMaxTimeRemaining()
    {
      double num = 32400.0;
      return new double?(state.GetEndGameTime().Value - num);
    }

    public double? GetWarningTimeSeconds() => new double?(3600.0);
  }
}
