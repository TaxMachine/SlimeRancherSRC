// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.WorldModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class WorldModel
  {
    public float econSeed;
    public Dictionary<Identifiable.Id, float> marketSaturation = new Dictionary<Identifiable.Id, float>(Identifiable.idComparer);
    public double worldTime;
    public double? fastForwardUntil;
    public bool pauseWorldTime;
    public double? lastWorldTime;
    public Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer> currOffers = new Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer>();
    public double nextDailyOfferCreateTime = double.PositiveInfinity;
    public List<string> lastOfferRancherIds = new List<string>();
    public List<string> pendingOfferRancherIds = new List<string>();
    public AmbianceDirector.Weather currWeather;
    public double weatherUntil;
    public FirestormActivator.Mode currFirestormMode;
    public FirestormActivator.Mode currFirestormAmbianceMode;
    public double nextFirestormTime;
    public double endFirestormTime;
    public double endFirecolumnsTime;
    public double nextFirecolumnTime = double.PositiveInfinity;
    public List<string> currentGingerPatchIds = new List<string>();
    public HashSet<string> occupiedPhaseSites = new HashSet<string>();
    private List<Participant> participants = new List<Participant>();

    public void RegisterParticipant(Participant participant) => participants.Add(participant);

    public void Init()
    {
      foreach (Participant participant in participants)
        participant.InitModel(this);
    }

    public void NotifyParticipants()
    {
      foreach (Participant participant in participants)
        participant.SetModel(this);
    }

    public bool HasReachedTime(double time) => worldTime >= time;

    public void Push(
      float econSeed,
      Dictionary<Identifiable.Id, float> saturations,
      double worldTime,
      Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer> offers,
      double dailyOfferCreateTime,
      List<string> lastOfferRancherIds,
      List<string> pendingOfferRancherIds,
      AmbianceDirector.Weather weather,
      double weatherUntil,
      double endStormTime,
      double nextStormTime,
      bool stormPreparing,
      List<string> currentGingerPatchIds,
      Dictionary<string, bool> occupiedPhaseSites)
    {
      this.econSeed = econSeed;
      foreach (KeyValuePair<Identifiable.Id, float> saturation in saturations)
        marketSaturation[saturation.Key] = saturation.Value;
      this.worldTime = worldTime;
      lastWorldTime = new double?(worldTime);
      currOffers = new Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer>();
      foreach (KeyValuePair<ExchangeDirector.OfferType, ExchangeDirector.Offer> offer in offers)
        currOffers[offer.Key] = offer.Value;
      nextDailyOfferCreateTime = dailyOfferCreateTime;
      this.lastOfferRancherIds = new List<string>(lastOfferRancherIds);
      this.pendingOfferRancherIds = new List<string>(pendingOfferRancherIds);
      currWeather = weather;
      this.weatherUntil = weatherUntil;
      nextFirecolumnTime = stormPreparing ? worldTime + 1800.0 : worldTime;
      currFirestormMode = stormPreparing ? FirestormActivator.Mode.PREPARING : FirestormActivator.Mode.IDLE;
      nextFirestormTime = nextStormTime;
      endFirestormTime = endStormTime;
      endFirecolumnsTime = endFirestormTime - 1800.0;
      this.currentGingerPatchIds = currentGingerPatchIds;
      this.occupiedPhaseSites = new HashSet<string>();
      foreach (KeyValuePair<string, bool> occupiedPhaseSite in occupiedPhaseSites)
      {
        if (occupiedPhaseSite.Value)
          this.occupiedPhaseSites.Add(occupiedPhaseSite.Key);
      }
    }

    public void Pull(
      out float econSeed,
      out Dictionary<Identifiable.Id, float> saturations,
      out double worldTime,
      out Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer> offers,
      out double dailyOfferCreateTime,
      out List<string> lastOfferRancherIds,
      out List<string> pendingOfferRancherIds,
      out AmbianceDirector.Weather weather,
      out double weatherUntil,
      out double endStormTime,
      out double nextStormTime,
      out bool stormPreparing,
      out List<string> currentGingerPatchIds,
      out Dictionary<string, bool> occupiedPhaseSites)
    {
      econSeed = this.econSeed;
      saturations = new Dictionary<Identifiable.Id, float>(marketSaturation, Identifiable.idComparer);
      worldTime = this.worldTime;
      lastWorldTime = new double?(worldTime);
      offers = new Dictionary<ExchangeDirector.OfferType, ExchangeDirector.Offer>();
      foreach (KeyValuePair<ExchangeDirector.OfferType, ExchangeDirector.Offer> currOffer in currOffers)
        offers[currOffer.Key] = currOffer.Value;
      dailyOfferCreateTime = nextDailyOfferCreateTime;
      lastOfferRancherIds = new List<string>(this.lastOfferRancherIds);
      pendingOfferRancherIds = new List<string>(this.pendingOfferRancherIds);
      weather = currWeather;
      weatherUntil = this.weatherUntil;
      stormPreparing = currFirestormMode == FirestormActivator.Mode.PREPARING;
      nextStormTime = nextFirestormTime;
      endStormTime = endFirestormTime;
      currentGingerPatchIds = this.currentGingerPatchIds;
      occupiedPhaseSites = new Dictionary<string, bool>();
      foreach (string occupiedPhaseSite in this.occupiedPhaseSites)
        occupiedPhaseSites[occupiedPhaseSite] = true;
    }

    public interface Participant
    {
      void InitModel(WorldModel model);

      void SetModel(WorldModel model);
    }
  }
}
