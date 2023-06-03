// Decompiled with JetBrains decompiler
// Type: OfferGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class OfferGenerator
{
  public string rancherId;
  private int numBlurbs;
  private ItemGenerator requests;
  private ItemGenerator rewards;
  private ItemGenerator rareRewards;
  private const int MIN_OFFER_VAL = 50;
  private const int MAX_OFFER_VAL = 100;
  private const float BONUS_PROB = 0.125f;
  private const float RARE_PROB = 0.5f;
  private const float REWARD_VAL_MULT = 1.75f;
  private const float REWARD_BONUS_MULT = 2.5f;
  private const ExchangeDirector.NonIdentReward BONUS_CASH_REWARD = ExchangeDirector.NonIdentReward.NEWBUCKS_HUGE;
  private readonly Dictionary<ExchangeDirector.NonIdentReward, float> NORM_CASH_REWARDS = new Dictionary<ExchangeDirector.NonIdentReward, float>()
  {
    {
      ExchangeDirector.NonIdentReward.NEWBUCKS_SMALL,
      3f
    },
    {
      ExchangeDirector.NonIdentReward.NEWBUCKS_MEDIUM,
      2f
    },
    {
      ExchangeDirector.NonIdentReward.NEWBUCKS_LARGE,
      1f
    }
  };

  public OfferGenerator(
    string rancherId,
    int numBlurbs,
    Identifiable.Id[] requests,
    Identifiable.Id[] rewards,
    Identifiable.Id[] rareRewards)
  {
    this.rancherId = rancherId;
    this.numBlurbs = numBlurbs;
    this.requests = new ItemGenerator(requests);
    this.rewards = new ItemGenerator(rewards);
    this.rareRewards = new ItemGenerator(rareRewards);
  }

  public ExchangeDirector.Offer Generate(
    ExchangeDirector exchangeDir,
    List<Identifiable.Id> whitelist,
    double expireTime,
    double earlyExchangeTime,
    int retries,
    bool isFirstOffer,
    bool isGoldPlortOffer)
  {
    for (int index = 0; index < retries; ++index)
    {
      ExchangeDirector.Offer oneOffer = GenerateOneOffer(exchangeDir, whitelist, expireTime, earlyExchangeTime, isFirstOffer, isGoldPlortOffer);
      if (oneOffer != null)
        return oneOffer;
    }
    return null;
  }

  public List<ExchangeDirector.RequestedItemEntry> GenerateRequestList(
    ExchangeDirector exchangeDir,
    List<Identifiable.Id> whitelist)
  {
    return GenerateRequestList(exchangeDir, whitelist, Randoms.SHARED.GetInRange(50, 100), new List<Identifiable.Id>());
  }

  public int GetRandomBlurb() => Randoms.SHARED.GetInRange(1, numBlurbs + 1);

  private List<ExchangeDirector.RequestedItemEntry> GenerateRequestList(
    ExchangeDirector exchangeDir,
    List<Identifiable.Id> whitelist,
    int requestValue,
    List<Identifiable.Id> used)
  {
    List<ExchangeDirector.RequestedItemEntry> requestList = new List<ExchangeDirector.RequestedItemEntry>();
    int inRange = Randoms.SHARED.GetInRange(2, 4);
    float[] numArray = new float[inRange];
    float num1 = 0.0f;
    for (int index = 0; index < inRange; ++index)
    {
      numArray[index] = Randoms.SHARED.GetInRange(0.5f, 1.5f);
      num1 += numArray[index];
    }
    for (int index = 0; index < inRange; ++index)
    {
      int num2 = Mathf.RoundToInt(requestValue * numArray[index] / num1);
      if (!(requests.Generate(exchangeDir, used, whitelist, num2, true) is ExchangeDirector.RequestedItemEntry requestedItemEntry))
        return null;
      requestList.Add(requestedItemEntry);
      used.Add(requestedItemEntry.id);
    }
    return requestList;
  }

  private ExchangeDirector.Offer GenerateOneOffer(
    ExchangeDirector exchangeDir,
    List<Identifiable.Id> whitelist,
    double expireTime,
    double earlyExchangeTime,
    bool isFirstOffer,
    bool isGoldPlortOffer)
  {
    List<Identifiable.Id> idList = new List<Identifiable.Id>();
    bool flag1 = !isFirstOffer && Randoms.SHARED.GetProbability(0.125f);
    bool flag2 = flag1 && rareRewards.ContainsAny(whitelist) && Randoms.SHARED.GetProbability(0.5f);
    int inRange = Randoms.SHARED.GetInRange(50, 100);
    int num1 = Mathf.RoundToInt(inRange * (flag1 ? 2.5f : 1.75f));
    List<ExchangeDirector.RequestedItemEntry> requestList = GenerateRequestList(exchangeDir, whitelist, inRange, idList);
    if (requestList == null)
      return null;
    List<ExchangeDirector.ItemEntry> itemEntryList = new List<ExchangeDirector.ItemEntry>();
    if (isGoldPlortOffer)
    {
      itemEntryList.Add(new ExchangeDirector.ItemEntry(Identifiable.Id.GOLD_PLORT, 3));
      idList.Add(Identifiable.Id.GOLD_PLORT);
    }
    else
    {
      int length = flag2 ? 1 : Randoms.SHARED.GetInRange(2, 4);
      float[] numArray = new float[length];
      float num2 = 0.0f;
      for (int index = 0; index < length; ++index)
      {
        numArray[index] = Randoms.SHARED.GetInRange(0.5f, 1.5f);
        num2 += numArray[index];
      }
      for (int index = 0; index < length; ++index)
      {
        int num3 = flag2 ? 100 : Mathf.RoundToInt(num1 * numArray[index] / num2);
        ExchangeDirector.ItemEntry itemEntry = (flag2 ? rareRewards : this.rewards).Generate(exchangeDir, idList, whitelist, num3, false);
        if (itemEntry == null)
          return null;
        itemEntryList.Add(itemEntry);
        idList.Add(itemEntry.id);
      }
      ExchangeDirector.ItemEntry itemEntry1 = new ExchangeDirector.ItemEntry(flag1 ? ExchangeDirector.NonIdentReward.NEWBUCKS_HUGE : Randoms.SHARED.Pick(NORM_CASH_REWARDS, ExchangeDirector.NonIdentReward.NEWBUCKS_SMALL));
      itemEntryList.Add(itemEntry1);
    }
    int num4 = isFirstOffer ? 1 : GetRandomBlurb();
    string offerId;
    if (!(flag1 | isGoldPlortOffer))
      offerId = "m.offer_" + num4 + "." + this.rancherId;
    else
      offerId = "m.bonusoffer." + this.rancherId;
    string rancherId = this.rancherId;
    double expireTime1 = expireTime;
    double earlyExchangeTime1 = earlyExchangeTime;
    List<ExchangeDirector.RequestedItemEntry> requests = requestList;
    List<ExchangeDirector.ItemEntry> rewards = itemEntryList;
    return new ExchangeDirector.Offer(offerId, rancherId, expireTime1, earlyExchangeTime1, requests, rewards);
  }

  private class ItemGenerator
  {
    public ICollection<Identifiable.Id> ids;

    public ItemGenerator(ICollection<Identifiable.Id> ids) => this.ids = ids;

    public ExchangeDirector.ItemEntry Generate(
      ExchangeDirector exchangeDir,
      List<Identifiable.Id> disallowed,
      List<Identifiable.Id> whitelist,
      int value,
      bool isRequest)
    {
      List<Identifiable.Id> iterable = new List<Identifiable.Id>(ids);
      iterable.RemoveAll(id => disallowed.Contains(id) || !whitelist.Contains(id));
      if (iterable.Count <= 0)
        return null;
      Identifiable.Id id1 = Randoms.SHARED.Pick(iterable, Identifiable.Id.NONE);
      int countForValue = exchangeDir.GetCountForValue(id1, value);
      if (countForValue == 0)
        return null;
      return isRequest ? new ExchangeDirector.RequestedItemEntry(id1, countForValue, 0) : new ExchangeDirector.ItemEntry(id1, countForValue);
    }

    public bool ContainsAny(List<Identifiable.Id> whitelist)
    {
      foreach (Identifiable.Id id in whitelist)
      {
        if (ids.Contains(id))
          return true;
      }
      return false;
    }
  }
}
