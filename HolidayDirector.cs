// Decompiled with JetBrains decompiler
// Type: HolidayDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HolidayDirector : MonoBehaviour, HolidayModel.Participant
{
  public List<OrnamentEntry> ornaments = new List<OrnamentEntry>();
  private Dictionary<MonthAndDay, OrnamentEntry> ornamentDict = new Dictionary<MonthAndDay, OrnamentEntry>();
  private HolidayModel model;

  public void InitForLevel()
  {
    SRSingleton<SceneContext>.Instance.GameModel.RegisterHoliday(this);
    SceneContext.onSceneLoaded += OnSceneLoaded_EventGordos;
    SceneContext.onSceneLoaded += OnSceneLoaded_EchoNoteGordo;
  }

  public void Awake()
  {
    foreach (OrnamentEntry ornament in ornaments)
    {
      ornament.Init();
      ornamentDict[ornament.date] = ornament;
    }
  }

  public void InitModel(HolidayModel model)
  {
  }

  public void SetModel(HolidayModel model) => this.model = model;

  public IEnumerable<Identifiable.Id> GetCurrOrnament()
  {
    DateTime today = DateTime.Today;
    if (today.Year == 2017)
    {
      today = DateTime.Today;
      int month = today.Month;
      today = DateTime.Today;
      int day = today.Day;
      MonthAndDay key = new MonthAndDay(month, day);
      if (ornamentDict.ContainsKey(key))
        yield return Randoms.SHARED.Pick(ornamentDict[key].weightDict, Identifiable.Id.NONE);
    }
  }

  private void OnSceneLoaded_EventGordos(SceneContext ctx)
  {
    SceneContext.onSceneLoaded -= OnSceneLoaded_EventGordos;
    if (Levels.isSpecial() || !ctx.GameModeConfig.GetModeSettings().enableEventGordos)
    {
      model.eventGordos.Clear();
    }
    else
    {
      DateTime currentDate = SRSingleton<SystemContext>.Instance.DateProvider.GetToday();
      Log.Debug("Current System Date For Events", "Date", currentDate.ToString("yyyy-MM-dd"));
      bool flag = false | model.eventGordos.RemoveWhere(e => !e.IsLiveAsOf(currentDate)) > 0;
      foreach (HolidayModel.EventGordo eventGordo in HolidayModel.EventGordo.INSTANCES.Where(e => e.IsLiveAsOf(currentDate)))
      {
        GordoModel gordoModel = SRSingleton<SceneContext>.Instance.GameModel.GetGordoModel(eventGordo.objectId);
        if (gordoModel == null)
        {
          Log.Error("Failed to active EventGordo.", "event", eventGordo);
          SentrySdk.CaptureMessage("Failed to active EventGordo!");
        }
        else
        {
          bool isFirstActivation = model.eventGordos.Add(eventGordo);
          gordoModel.EventGordoActivate(isFirstActivation);
          flag |= isFirstActivation;
        }
      }
      if (!flag)
        return;
      StartCoroutine(ResetCratesAfterFrame());
    }
  }

  private IEnumerator ResetCratesAfterFrame()
  {
    yield return new WaitForEndOfFrame();
    ZoneDirector.Zone currentZone = SRSingleton<SceneContext>.Instance.Player.GetComponent<PlayerZoneTracker>().GetCurrentZone();
    ZoneDirector zoneDirector = ZoneDirector.zones.Get(currentZone);
    if (zoneDirector != null)
      zoneDirector.ResetCrates();
  }

  private void OnSceneLoaded_EchoNoteGordo(SceneContext ctx)
  {
    SceneContext.onSceneLoaded -= OnSceneLoaded_EchoNoteGordo;
    if (Levels.isSpecial() || !ctx.GameModeConfig.GetModeSettings().enableEchoNoteGordos)
    {
      model.eventEchoNoteGordos.Clear();
    }
    else
    {
      DateTime currentDate = SRSingleton<SystemContext>.Instance.DateProvider.GetToday();
      Log.Debug("Current System Date For Wiggly Events", "Date", currentDate.ToString("yyyy-MM-dd"));
      model.eventEchoNoteGordos.RemoveWhere(e => !e.IsLiveAsOf(currentDate));
      foreach (HolidayModel.EventEchoNoteGordo eventEchoNoteGordo in HolidayModel.EventEchoNoteGordo.INSTANCES.Where(e => e.IsLiveAsOf(currentDate)))
      {
        EchoNoteGordoModel echoNoteGordoModel = SRSingleton<SceneContext>.Instance.GameModel.GetEchoNoteGordoModel(eventEchoNoteGordo.objectId);
        if (echoNoteGordoModel == null)
        {
          Log.Error("Failed to active EchoNoteGordo.", "id", eventEchoNoteGordo.objectId);
          SentrySdk.CaptureMessage("Failed to active EchoNoteGordo!");
        }
        else
        {
          bool isFirstActivation = model.eventEchoNoteGordos.Add(eventEchoNoteGordo);
          echoNoteGordoModel.Activate(isFirstActivation);
        }
      }
    }
  }

  [Serializable]
  public class MonthAndDay : IEquatable<MonthAndDay>
  {
    public int month;
    public int day;

    public MonthAndDay(int month, int day)
    {
      this.month = month;
      this.day = day;
    }

    public bool Equals(MonthAndDay other) => month == other.month && day == other.day;

    public override int GetHashCode() => month << 8 ^ day;
  }

  [Serializable]
  public class OrnamentEntry
  {
    public MonthAndDay date;
    public List<WeightEntry> weights;
    public Dictionary<Identifiable.Id, float> weightDict = new Dictionary<Identifiable.Id, float>(Identifiable.idComparer);

    public void Init()
    {
      foreach (WeightEntry weight in weights)
        weightDict[weight.id] = weight.weight;
    }

    [Serializable]
    public class WeightEntry
    {
      public float weight;
      public Identifiable.Id id;
    }
  }
}
