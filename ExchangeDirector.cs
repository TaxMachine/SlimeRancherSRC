// Decompiled with JetBrains decompiler
// Type: ExchangeDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeDirector : SRBehaviour, WorldModel.Participant
{
  public OnOfferChanged onOfferChanged;
  [Tooltip("Values to be used in generating offers.")]
  public ValueEntry[] values;
  public ProgressOfferEntry[] progressOffers;
  private Dictionary<Category, Identifiable.Id[]> catDict = new Dictionary<Category, Identifiable.Id[]>()
  {
    {
      Category.FRUIT,
      new List<Identifiable.Id>(Identifiable.FRUIT_CLASS).ToArray()
    },
    {
      Category.VEGGIES,
      new List<Identifiable.Id>(Identifiable.VEGGIE_CLASS).ToArray()
    },
    {
      Category.MEAT,
      new List<Identifiable.Id>(Identifiable.MEAT_CLASS).ToArray()
    },
    {
      Category.PLORTS,
      new List<Identifiable.Id>(Identifiable.PLORT_CLASS).ToArray()
    },
    {
      Category.SLIMES,
      new List<Identifiable.Id>(Identifiable.SLIME_CLASS).ToArray()
    },
    {
      Category.CRAFT_MATS,
      new List<Identifiable.Id>(Identifiable.CRAFT_CLASS).ToArray()
    }
  };
  [Tooltip("The ranchers and what they request/reward the player with.")]
  public Rancher[] ranchers;
  public Identifiable.Id[] initUnlocked;
  public UnlockList[] unlockLists;
  public NonIdentEntry[] nonIdentRewards;
  public int ogdenRecurAmount = 3;
  public int mochiRecurAmount = 5;
  public int viktorRecurAmount = 5;
  private Dictionary<NonIdentReward, Sprite> nonIdentRewardDict = new Dictionary<NonIdentReward, Sprite>();
  private Dictionary<Identifiable.Id, float> valueDict = new Dictionary<Identifiable.Id, float>(Identifiable.idComparer);
  private TimeDirector timeDir;
  private ProgressDirector progressDir;
  private MailDirector mailDir;
  private PediaDirector pediaDir;
  private TutorialDirector tutorialDir;
  private Dictionary<string, OfferGenerator> offerGenerators = new Dictionary<string, OfferGenerator>();
  private WorldModel worldModel;
  private const float HOURS_BETWEEN_OFFERS = 0.0833333358f;
  private const float OFFER_END_HOUR = 12f;
  private const float OFFER_HOUR = 12.083333f;
  private const float DAYS_PER_DAILY_LEVEL = 3f;
  private const float OGDEN_LEVELS = 3f;
  private const float MOCHI_LEVELS = 3f;
  private const float VIKTOR_LEVELS = 3f;
  private const float EARLY_EXCHANGE_HOURS = 2f;

  public bool HasPendingOffers(OfferType offerType) => worldModel != null && offerType == OfferType.GENERAL && worldModel.pendingOfferRancherIds.Count > 0;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    mailDir = SRSingleton<SceneContext>.Instance.MailDirector;
    tutorialDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
    foreach (ValueEntry valueEntry in values)
      valueDict[valueEntry.id] = valueEntry.value;
    foreach (NonIdentEntry nonIdentReward in nonIdentRewards)
      nonIdentRewardDict[nonIdentReward.reward] = nonIdentReward.icon;
    ConfigureOfferGenerators();
  }

  public void Start()
  {
    if (progressDir.HasProgress(ProgressDirector.ProgressType.UNLOCK_WILDS))
      pediaDir.Unlock(PediaDirector.Id.WILDS_TUTORIAL);
    if (!progressDir.HasProgress(ProgressDirector.ProgressType.UNLOCK_VALLEY))
      return;
    pediaDir.Unlock(PediaDirector.Id.VALLEY_TUTORIAL);
  }

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterWorldParticipant(this);

  public void InitModel(WorldModel worldModel)
  {
    worldModel.currOffers.Clear();
    worldModel.lastOfferRancherIds.Clear();
    worldModel.pendingOfferRancherIds.Clear();
    int fullDays = SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().exchangeStartDay - 1;
    worldModel.nextDailyOfferCreateTime = TimeDirector.GetHourAfter(0.0, fullDays, 12.083333f);
  }

  public void SetModel(WorldModel worldModel) => this.worldModel = worldModel;

  private void PrepareNextDailyOffer()
  {
    SetupPendingOfferRanchers();
    worldModel.lastOfferRancherIds.Clear();
    worldModel.lastOfferRancherIds.AddRange(worldModel.pendingOfferRancherIds);
  }

  private double GetNextDailyOfferCreateTime() => SRSingleton<SceneContext>.Instance.GameModel.currGameMode == PlayerState.GameMode.TIME_LIMIT_V2 && timeDir.CurrDay() == 1 ? timeDir.GetHourAfter(1, 12.083333f) : timeDir.GetNextHour(12.083333f);

  public void Update()
  {
    Offer currOffer = worldModel.currOffers.ContainsKey(OfferType.GENERAL) ? worldModel.currOffers[OfferType.GENERAL] : null;
    if (currOffer != null && timeDir.HasReached(currOffer.expireTime))
      ClearOffer(OfferType.GENERAL);
    if (currOffer == null && timeDir.HasReached(worldModel.nextDailyOfferCreateTime))
    {
      worldModel.nextDailyOfferCreateTime = GetNextDailyOfferCreateTime();
      PrepareNextDailyOffer();
      OfferDidChange();
    }
    if (!worldModel.currOffers.ContainsKey(OfferType.OGDEN_RECUR) && (worldModel.currOffers.ContainsKey(OfferType.OGDEN) || progressDir.GetProgress(ProgressDirector.ProgressType.OGDEN_REWARDS) >= 3.0))
    {
      worldModel.currOffers[OfferType.OGDEN_RECUR] = CreateOgdenRecurOffer();
      OfferDidChange();
    }
    if (!worldModel.currOffers.ContainsKey(OfferType.MOCHI_RECUR) && (worldModel.currOffers.ContainsKey(OfferType.MOCHI) || progressDir.GetProgress(ProgressDirector.ProgressType.MOCHI_REWARDS) >= 3.0))
    {
      worldModel.currOffers[OfferType.MOCHI_RECUR] = CreateMochiRecurOffer();
      OfferDidChange();
    }
    if (worldModel.currOffers.ContainsKey(OfferType.VIKTOR_RECUR) || !worldModel.currOffers.ContainsKey(OfferType.VIKTOR) && progressDir.GetProgress(ProgressDirector.ProgressType.VIKTOR_REWARDS) < 3.0)
      return;
    worldModel.currOffers[OfferType.VIKTOR_RECUR] = CreateViktorRecurOffer();
    OfferDidChange();
  }

  public bool MaybeStartNext(OfferType offerType)
  {
    if (worldModel.currOffers.ContainsKey(offerType))
      return false;
    ProgressOfferEntry progressEntry = GetProgressEntry(offerType);
    if (progressEntry == null || worldModel.currOffers.ContainsKey(progressEntry.specialOfferType) || progressDir.GetProgress(progressEntry.progressType) >= progressEntry.rewardLevels.Length)
      return false;
    worldModel.currOffers[progressEntry.specialOfferType] = CreateProgressOffer(progressEntry.specialOfferType, progressEntry.progressType, progressEntry.rewardLevels);
    OfferDidChange();
    return CreateRancherChatUI(offerType, true);
  }

  private void SetupPendingOfferRanchers()
  {
    worldModel.pendingOfferRancherIds.Clear();
    List<string> iterable = new List<string>();
    foreach (Rancher rancher in ranchers)
    {
      if (!progressDir.HasProgress(ProgressDirector.GetRancherProgressType(rancher.name)))
      {
        worldModel.pendingOfferRancherIds.Add(rancher.name);
        mailDir.SendMail(MailDirector.Type.EXCHANGE, "exchangeintro_" + rancher.name);
        return;
      }
      if (!worldModel.lastOfferRancherIds.Contains(rancher.name))
        iterable.Add(rancher.name);
    }
    if (iterable.Count < 2)
    {
      Log.Error("Somehow do not have enough available ranchers to choose from for exchange offers.");
    }
    else
    {
      worldModel.pendingOfferRancherIds.Add(Randoms.SHARED.Pluck(iterable, null));
      worldModel.pendingOfferRancherIds.Add(Randoms.SHARED.Pluck(iterable, null));
    }
  }

  public double? GetOfferExpirationTime(OfferType type) => worldModel.currOffers.ContainsKey(type) ? new double?(worldModel.currOffers[type].expireTime - timeDir.WorldTime()) : new double?();

  public List<RequestedItemEntry> GetOfferRequests(OfferType type)
  {
    if (worldModel == null)
      return null;
    return worldModel.currOffers.ContainsKey(type) ? worldModel.currOffers[type].requests : null;
  }

  public List<ItemEntry> GetOfferRewards(OfferType type)
  {
    if (worldModel == null)
      return null;
    return worldModel.currOffers.ContainsKey(type) ? worldModel.currOffers[type].rewards : null;
  }

  public string GetOfferId(OfferType type)
  {
    if (worldModel == null)
      return null;
    return worldModel.currOffers.ContainsKey(type) ? worldModel.currOffers[type].offerId : null;
  }

  private ProgressOfferEntry GetProgressEntry(OfferType type)
  {
    foreach (ProgressOfferEntry progressOffer in progressOffers)
    {
      if (progressOffer.specialOfferType == type)
        return progressOffer;
    }
    return null;
  }

  public bool TryToAcceptNewOffer()
  {
    if (worldModel.pendingOfferRancherIds.Count == 0)
      return false;
    if (worldModel.pendingOfferRancherIds.Count == 1)
    {
      SelectDailyOffer(worldModel.pendingOfferRancherIds[0], true);
      return false;
    }
    SRSingleton<GameContext>.Instance.UITemplates.CreateRancherChoiceUI(worldModel.pendingOfferRancherIds);
    return true;
  }

  public bool SelectDailyOffer(string rancherId, bool isFirstOffer)
  {
    if (worldModel.currOffers.ContainsKey(OfferType.GENERAL))
      return false;
    Offer dailyOffer = CreateDailyOffer(rancherId, isFirstOffer);
    if (dailyOffer == null)
      return false;
    worldModel.currOffers[OfferType.GENERAL] = dailyOffer;
    progressDir.AddProgress(ProgressDirector.GetRancherProgressType(dailyOffer.rancherId));
    worldModel.pendingOfferRancherIds.Clear();
    OfferDidChange();
    return true;
  }

  public Sprite GetRancherImage(string rancherId) => GetRancher(rancherId).defaultImg;

  public Sprite GetRancherIcon(string rancherId) => GetRancher(rancherId).icon;

  private Rancher GetRancher(string rancherId)
  {
    foreach (Rancher rancher in ranchers)
    {
      if (rancher.name == rancherId)
        return rancher;
    }
    return null;
  }

  public string GetOfferRancherId(OfferType type) => worldModel.currOffers.ContainsKey(type) ? worldModel.currOffers[type].rancherId : null;

  public void RewardsDidSpawn(OfferType type) => ClearOffer(type);

  public bool TryAccept(
    OfferType type,
    Identifiable.Id id,
    Awarder[] awarders)
  {
    if (!worldModel.currOffers.ContainsKey(type) || !worldModel.currOffers[type].TryAccept(id, awarders, type))
      return false;
    OfferDidChange();
    return true;
  }

  public int GetCountForValue(Identifiable.Id id, int value) => valueDict.ContainsKey(id) ? Mathf.RoundToInt(value / valueDict[id]) : 0;

  public Sprite GetSpecRewardIcon(NonIdentReward specReward) => nonIdentRewardDict[specReward];

  private void ClearOffer(OfferType type)
  {
    worldModel.currOffers.Remove(type);
    OfferDidChange();
  }

  private Offer CreateOgdenRecurOffer() => new Offer("m.offer.ogden_recur", "ogden", double.PositiveInfinity, double.NegativeInfinity, new List<RequestedItemEntry>()
  {
    new RequestedItemEntry(Identifiable.Id.KOOKADOBA_FRUIT, ogdenRecurAmount, 0)
  }, new List<ItemEntry>()
  {
    new ItemEntry(Identifiable.Id.SPICY_TOFU, 1)
  });

  private Offer CreateMochiRecurOffer() => new Offer("m.offer.mochi_recur", "mochi", double.PositiveInfinity, double.NegativeInfinity, new List<RequestedItemEntry>()
  {
    new RequestedItemEntry(Identifiable.Id.QUICKSILVER_PLORT, mochiRecurAmount, 0)
  }, new List<ItemEntry>()
  {
    new ItemEntry(NonIdentReward.NEWBUCKS_MOCHI)
  });

  private Offer CreateViktorRecurOffer() => new Offer("m.offer.viktor_recur", "viktor", double.PositiveInfinity, double.NegativeInfinity, new List<RequestedItemEntry>()
  {
    new RequestedItemEntry(Identifiable.Id.GLITCH_BUG_REPORT, viktorRecurAmount, 0)
  }, new List<ItemEntry>()
  {
    new ItemEntry(Identifiable.Id.MANIFOLD_CUBE_CRAFT, 1)
  });

  private Offer CreateProgressOffer(
    OfferType offerType,
    ProgressDirector.ProgressType progressType,
    RewardLevel[] rewardLevels)
  {
    int num = progressDir.GetProgress(progressType) + 1;
    RewardLevel rewardLevel = rewardLevels[num - 1];
    string offerId = "m.offer." + progressType.ToString().ToLowerInvariant() + "_level" + num;
    List<RequestedItemEntry> requestedItemEntryList = new List<RequestedItemEntry>();
    requestedItemEntryList.Add(new RequestedItemEntry(rewardLevel.requestedItem, rewardLevel.count, 0));
    List<ItemEntry> itemEntryList = new List<ItemEntry>();
    itemEntryList.Add(new ItemEntry(rewardLevel.reward));
    string lowerInvariant = offerType.ToString().ToLowerInvariant();
    List<RequestedItemEntry> requests = requestedItemEntryList;
    List<ItemEntry> rewards = itemEntryList;
    return new Offer(offerId, lowerInvariant, double.PositiveInfinity, double.NegativeInfinity, requests, rewards);
  }

  private Offer CreateDailyOffer(string rancherId, bool isFirstOffer)
  {
    int retries = 10;
    if (SRSingleton<SceneContext>.Instance.GameModel.currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
      return offerGenerators[rancherId].Generate(this, CreateWhiteList(), timeDir.GetNextHourAtLeastHalfDay(12f), timeDir.HoursFromNow(2f), retries, isFirstOffer, SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().exchangeRewardsGoldPlorts);
    List<Identifiable.Id> rushModeWhiteList = CreateRushModeWhiteList(timeDir.CurrDay());
    List<RequestedItemEntry> requestedItemEntryList;
    for (requestedItemEntryList = null; requestedItemEntryList == null && retries > 0; --retries)
      requestedItemEntryList = offerGenerators[rancherId].GenerateRequestList(this, rushModeWhiteList);
    if (requestedItemEntryList == null)
      return null;
    string offerId = string.Format("m.offer_{0}.{1}", offerGenerators[rancherId].GetRandomBlurb(), rancherId);
    List<ItemEntry> itemEntryList = new List<ItemEntry>()
    {
      new ItemEntry(NonIdentReward.TIME_EXTENSION_12H),
      new ItemEntry(Identifiable.Id.GINGER_VEGGIE, 6)
    };
    string rancherId1 = rancherId;
    double hourAtLeastHalfDay = timeDir.GetNextHourAtLeastHalfDay(12f);
    double earlyExchangeTime = timeDir.HoursFromNow(2f);
    List<RequestedItemEntry> requests = requestedItemEntryList;
    List<ItemEntry> rewards = itemEntryList;
    return new Offer(offerId, rancherId1, hourAtLeastHalfDay, earlyExchangeTime, requests, rewards);
  }

  private List<Identifiable.Id> CreateRushModeWhiteList(int day)
  {
    HashSet<ProgressDirector.ProgressType> progressTypeSet = new HashSet<ProgressDirector.ProgressType>(ProgressDirector.progressTypeComparer);
    if (day >= 2)
    {
      progressTypeSet.Add(ProgressDirector.ProgressType.UNLOCK_QUARRY);
      progressTypeSet.Add(ProgressDirector.ProgressType.UNLOCK_MOSS);
    }
    if (day >= 3)
    {
      progressTypeSet.Add(ProgressDirector.ProgressType.UNLOCK_DESERT);
      progressTypeSet.Add(ProgressDirector.ProgressType.UNLOCK_RUINS);
    }
    List<Identifiable.Id> rushModeWhiteList = new List<Identifiable.Id>(initUnlocked);
    foreach (UnlockList unlockList in unlockLists)
    {
      if (progressTypeSet.Contains(unlockList.unlock))
        rushModeWhiteList.AddRange(unlockList.ids);
    }
    return rushModeWhiteList;
  }

  private List<Identifiable.Id> CreateWhiteList()
  {
    List<Identifiable.Id> whiteList = new List<Identifiable.Id>();
    whiteList.AddRange(initUnlocked);
    foreach (UnlockList unlockList in unlockLists)
    {
      if (progressDir.HasProgress(unlockList.unlock))
        whiteList.AddRange(unlockList.ids);
    }
    return whiteList;
  }

  private void OfferDidChange()
  {
    if (onOfferChanged == null)
      return;
    onOfferChanged();
  }

  private void ConfigureOfferGenerators()
  {
    offerGenerators.Clear();
    foreach (Rancher rancher in ranchers)
    {
      List<Identifiable.Id> idList1 = new List<Identifiable.Id>();
      foreach (Category requestCategory in rancher.requestCategories)
        idList1.AddRange(catDict[requestCategory]);
      idList1.AddRange(rancher.indivRequests);
      List<Identifiable.Id> idList2 = new List<Identifiable.Id>();
      foreach (Category rewardCategory in rancher.rewardCategories)
        idList2.AddRange(catDict[rewardCategory]);
      idList2.AddRange(rancher.indivRewards);
      List<Identifiable.Id> idList3 = new List<Identifiable.Id>();
      foreach (Category rareRewardCategory in rancher.rareRewardCategories)
        idList3.AddRange(catDict[rareRewardCategory]);
      idList3.AddRange(rancher.indivRareRewards);
      offerGenerators[rancher.name] = new OfferGenerator(rancher.name, rancher.numBlurbs, idList1.ToArray(), idList2.ToArray(), idList3.ToArray());
    }
  }

  public bool IsOffline(OfferType offerType) => GetOfferRequests(offerType) == null && !HasPendingOffers(offerType);

  public bool CreateRancherChatUI(OfferType offerType, bool intro)
  {
    RancherChatMetadata rancherChatMetadata = GetRancherChatMetadata(offerType, intro);
    if (rancherChatMetadata == null)
      return false;
    RancherChatUI.Instantiate(rancherChatMetadata).onDestroy = () =>
    {
      if (!(SRSingleton<SceneContext>.Instance != null))
        return;
      switch (offerType)
      {
        case OfferType.OGDEN:
          if (!progressDir.SetUniqueProgress(ProgressDirector.ProgressType.UNLOCK_WILDS))
            break;
          tutorialDir.MaybeShowPopup(TutorialDirector.Id.WILDS_SLIMEPEDIA);
          pediaDir.Unlock(PediaDirector.Id.WILDS_TUTORIAL);
          break;
        case OfferType.MOCHI:
          if (!progressDir.SetUniqueProgress(ProgressDirector.ProgressType.UNLOCK_VALLEY))
            break;
          tutorialDir.MaybeShowPopup(TutorialDirector.Id.VALLEY_SLIMEPEDIA);
          pediaDir.Unlock(PediaDirector.Id.VALLEY_TUTORIAL);
          break;
        case OfferType.VIKTOR:
          if (!progressDir.SetUniqueProgress(ProgressDirector.ProgressType.UNLOCK_SLIMULATIONS))
            break;
          tutorialDir.MaybeShowPopup(TutorialDirector.Id.SLIMULATIONS_SLIMEPEDIA);
          pediaDir.Unlock(PediaDirector.Id.SLIMULATIONS_TUTORIAL);
          break;
      }
    };
    return true;
  }

  private RancherChatMetadata GetRancherChatMetadata(
    OfferType offerType,
    bool intro)
  {
    switch (offerType)
    {
      case OfferType.OGDEN_RECUR:
        ProgressOfferEntry progressEntry1 = GetProgressEntry(OfferType.OGDEN);
        intro = progressDir.SetUniqueProgress(ProgressDirector.ProgressType.OGDEN_SEEN_FINAL_CHAT);
        return !intro ? progressEntry1.rancherChatEndRepeat : progressEntry1.rancherChatEndIntro;
      case OfferType.MOCHI_RECUR:
        ProgressOfferEntry progressEntry2 = GetProgressEntry(OfferType.MOCHI);
        intro = progressDir.SetUniqueProgress(ProgressDirector.ProgressType.MOCHI_SEEN_FINAL_CHAT);
        return !intro ? progressEntry2.rancherChatEndRepeat : progressEntry2.rancherChatEndIntro;
      case OfferType.VIKTOR_RECUR:
        ProgressOfferEntry progressEntry3 = GetProgressEntry(OfferType.VIKTOR);
        intro = progressDir.SetUniqueProgress(ProgressDirector.ProgressType.VIKTOR_SEEN_FINAL_CHAT);
        return !intro ? progressEntry3.rancherChatEndRepeat : progressEntry3.rancherChatEndIntro;
      default:
        ProgressOfferEntry progressEntry4 = GetProgressEntry(offerType);
        if (progressEntry4 != null)
        {
          int progress = progressDir.GetProgress(progressEntry4.progressType);
          if (progress < progressEntry4.rewardLevels.Length)
          {
            RewardLevel rewardLevel = progressEntry4.rewardLevels[progress];
            return !intro ? rewardLevel.rancherChatRepeat : rewardLevel.rancherChatIntro;
          }
        }
        if (!worldModel.currOffers.ContainsKey(offerType))
          return null;
        Offer currOffer = worldModel.currOffers[offerType];
        return CreateRancherChatMetadata(currOffer.rancherId, currOffer.offerId);
    }
  }

  public RancherChatMetadata CreateRancherChatMetadata(string rancherId, string message)
  {
    RancherChatMetadata instance = ScriptableObject.CreateInstance<RancherChatMetadata>();
    instance.entries = new RancherChatMetadata.Entry[1]
    {
      new RancherChatMetadata.Entry()
      {
        rancherName = (RancherChatMetadata.Entry.RancherName) Enum.Parse(typeof (RancherChatMetadata.Entry.RancherName), rancherId.ToUpperInvariant()),
        rancherImage = GetRancherImage(rancherId),
        messageBackground = GetRancher(rancherId).chatBackground,
        messageText = message
      }
    };
    return instance;
  }

  public delegate void OnAwakeDelegate(ExchangeDirector exchangeDir);

  public delegate void OnOfferChanged();

  public interface Awarder
  {
    void AwardIfType(OfferType offerType);
  }

  public enum OfferType
  {
    GENERAL,
    OGDEN,
    OGDEN_RECUR,
    MOCHI,
    MOCHI_RECUR,
    VIKTOR,
    VIKTOR_RECUR,
  }

  [Serializable]
  public class ValueEntry
  {
    public Identifiable.Id id;
    public float value;
  }

  [Serializable]
  public class Rancher
  {
    public string name;
    public Sprite defaultImg;
    public Sprite icon;
    public Material chatBackground;
    public int numBlurbs;
    public Category[] requestCategories;
    public Identifiable.Id[] indivRequests;
    public Category[] rewardCategories;
    public Identifiable.Id[] indivRewards;
    public Category[] rareRewardCategories;
    public Identifiable.Id[] indivRareRewards;
  }

  [Serializable]
  public class RewardLevel
  {
    public NonIdentReward reward;
    public Identifiable.Id requestedItem;
    public int count;
    public RancherChatMetadata rancherChatIntro;
    public RancherChatMetadata rancherChatRepeat;
  }

  [Serializable]
  public class ProgressOfferEntry
  {
    public OfferType specialOfferType;
    public ProgressDirector.ProgressType progressType;
    public RewardLevel[] rewardLevels;
    public RancherChatMetadata rancherChatEndIntro;
    public RancherChatMetadata rancherChatEndRepeat;
  }

  public enum Category
  {
    FRUIT,
    VEGGIES,
    MEAT,
    PLORTS,
    SLIMES,
    CRAFT_MATS,
  }

  [Serializable]
  public class UnlockList
  {
    public ProgressDirector.ProgressType unlock;
    public Identifiable.Id[] ids;
  }

  [Serializable]
  public class RequestedItemEntry : ItemEntry
  {
    public int progress;

    public RequestedItemEntry(
      Identifiable.Id id,
      int count,
      int progress,
      NonIdentReward specReward)
      : base(id, count, specReward)
    {
      this.progress = progress;
    }

    public RequestedItemEntry(Identifiable.Id id, int count, int progress)
      : base(id, count)
    {
      this.progress = progress;
    }

    public bool IsComplete() => progress >= count;
  }

  public enum NonIdentReward
  {
    NONE = 0,
    OGDEN_MIX = 100, // 0x00000064
    OGDEN_GARDEN = 101, // 0x00000065
    OGDEN_RANCH = 102, // 0x00000066
    MOCHI_EXTRA_MILE = 200, // 0x000000C8
    MOCHI_COOP = 201, // 0x000000C9
    MOCHI_RANCH = 202, // 0x000000CA
    VIKTOR_CHICKEN_CLONER = 300, // 0x0000012C
    VIKTOR_DELUXE_DRONES = 301, // 0x0000012D
    VIKTOR_RANCH = 302, // 0x0000012E
    NEWBUCKS_SMALL = 10000, // 0x00002710
    NEWBUCKS_MEDIUM = 10001, // 0x00002711
    NEWBUCKS_LARGE = 10002, // 0x00002712
    NEWBUCKS_HUGE = 10003, // 0x00002713
    NEWBUCKS_MOCHI = 10004, // 0x00002714
    TIME_EXTENSION_12H = 20000, // 0x00004E20
  }

  [Serializable]
  public class NonIdentEntry
  {
    public NonIdentReward reward;
    public Sprite icon;
  }

  [Serializable]
  public class ItemEntry
  {
    public Identifiable.Id id;
    public NonIdentReward specReward;
    public int count;

    public ItemEntry(Identifiable.Id id, int count, NonIdentReward specReward)
    {
      this.id = id;
      this.specReward = specReward;
      this.count = count;
    }

    public ItemEntry(Identifiable.Id id, int count)
    {
      this.id = id;
      specReward = NonIdentReward.NONE;
      this.count = count;
    }

    public ItemEntry(NonIdentReward specReward)
    {
      id = Identifiable.Id.NONE;
      this.specReward = specReward;
      count = 1;
    }
  }

  [Serializable]
  public class Offer
  {
    public List<RequestedItemEntry> requests;
    public List<ItemEntry> rewards;
    public double expireTime;
    public double earlyExchangeTime;
    public string rancherId;
    public string offerId;

    public Offer(
      string offerId,
      string rancherId,
      double expireTime,
      double earlyExchangeTime,
      List<RequestedItemEntry> requests,
      List<ItemEntry> rewards)
    {
      this.offerId = offerId;
      this.rancherId = rancherId;
      this.expireTime = expireTime;
      this.earlyExchangeTime = earlyExchangeTime;
      this.requests = requests;
      this.rewards = rewards;
    }

    public bool TryAccept(
      Identifiable.Id id,
      Awarder[] awarders,
      OfferType offerType)
    {
      foreach (RequestedItemEntry request in requests)
      {
        if (request.id == id && !request.IsComplete())
        {
          ++request.progress;
          if (IsComplete())
          {
            foreach (Awarder awarder in awarders)
              awarder.AwardIfType(offerType);
            if (offerType == OfferType.GENERAL && !SRSingleton<SceneContext>.Instance.TimeDirector.HasReached(earlyExchangeTime))
              SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.FULFILL_EXCHANGE_EARLY, 1);
            if (offerType == OfferType.GENERAL)
            {
              if (rancherId == "ogden")
                SRSingleton<SceneContext>.Instance.ProgressDirector.MaybeUnlockOgdenMissions();
              else if (rancherId == "mochi")
                SRSingleton<SceneContext>.Instance.ProgressDirector.MaybeUnlockMochiMissions();
              else if (rancherId == "viktor")
                SRSingleton<SceneContext>.Instance.ProgressDirector.MaybeUnlockViktorMissions();
            }
            AnalyticsUtil.CustomEvent("ExchangeOfferComplete", new Dictionary<string, object>()
            {
              {
                "RancherId",
                rancherId
              },
              {
                "ExchangeId",
                offerId
              }
            });
          }
          return true;
        }
      }
      return false;
    }

    public bool IsComplete()
    {
      foreach (RequestedItemEntry request in requests)
      {
        if (!request.IsComplete())
          return false;
      }
      return true;
    }
  }
}
