// Decompiled with JetBrains decompiler
// Type: AchievementsDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsDirector : 
  MonoBehaviour,
  GameAchievesModel.Participant,
  ProfileAchievesModel.Participant
{
  public GameObject achievementAwardUIPrefab;
  public GameObject achievementsPanelPrefab;
  public Sprite tier1DefaultIcon;
  public Sprite tier2DefaultIcon;
  public Sprite tier3DefaultIcon;
  private float availableAchievementCountDivisor = 1f;
  public HashSet<Achievement> TIER_1 = new HashSet<Achievement>()
  {
    Achievement.SELL_PLORTS_A,
    Achievement.SELL_PLORTS_B,
    Achievement.FEED_SLIMES_CHICKENS,
    Achievement.FRUIT_TREE_TYPES,
    Achievement.VEGGIE_PATCH_TYPES,
    Achievement.EARN_CURRENCY_A,
    Achievement.AWAKE_UNTIL_MORNING,
    Achievement.KNOCKOUT_MORNING,
    Achievement.AWAY_FROM_RANCH,
    Achievement.FEED_AIRBORNE,
    Achievement.PINK_SLIMES_FOOD_TYPES,
    Achievement.TABBY_HEADBUTT,
    Achievement.INCINERATE_ELDER_CHICKEN,
    Achievement.FEED_FAVORITES,
    Achievement.FILLED_SILO,
    Achievement.INCINERATE_CHICK,
    Achievement.TIME_LIMIT_CURRENCY_A,
    Achievement.FABRICATE_GADGETS_A,
    Achievement.SLIME_STAGE_TARR,
    Achievement.JOIN_REWARDS_CLUB,
    Achievement.USE_CHROMAS
  };
  public HashSet<Achievement> TIER_2 = new HashSet<Achievement>()
  {
    Achievement.SELL_PLORTS_C,
    Achievement.SELL_PLORTS_D,
    Achievement.EARN_CURRENCY_B,
    Achievement.LAUNCHED_BOOM_EXPLODE,
    Achievement.MANY_SLIMES_IN_VAC,
    Achievement.DISCOVERED_QUARRY,
    Achievement.DISCOVERED_MOSS,
    Achievement.DISCOVERED_RUINS,
    Achievement.OPEN_SLIME_GATE,
    Achievement.BURST_GORDO,
    Achievement.EXTENDED_RAD_EXPOSURE,
    Achievement.DAY_COLLECT_PLORTS,
    Achievement.FULFILL_EXCHANGE_EARLY,
    Achievement.RANCH_UPGRADED_STORAGE,
    Achievement.ENTERED_CORRAL_SLIMES,
    Achievement.POND_SLIME_TYPES,
    Achievement.CORRAL_SLIME_TYPES,
    Achievement.CORRAL_LARGO_TYPES,
    Achievement.TIME_LIMIT_CURRENCY_B,
    Achievement.FABRICATE_GADGETS_B,
    Achievement.COLLECT_SLIME_TOYS,
    Achievement.SNARE_HUNTER_GORDO,
    Achievement.ACTIVATE_OASIS
  };
  public HashSet<Achievement> TIER_3 = new HashSet<Achievement>()
  {
    Achievement.SELL_PLORTS_E,
    Achievement.EARN_CURRENCY_C,
    Achievement.DAY_CURRENCY,
    Achievement.DISCOVERED_DESERT,
    Achievement.EXTENDED_TARR_HOLD,
    Achievement.GOLD_SLIME_TRIPLE_PLORT,
    Achievement.RANCH_LARGO_TYPES,
    Achievement.TIME_LIMIT_CURRENCY_C,
    Achievement.FABRICATE_GADGETS_C,
    Achievement.SLIMEBALL_SCORE,
    Achievement.FIND_HOBSONS_END,
    Achievement.FINISH_ADVENTURE,
    Achievement.COMPLETE_SLIMEPEDIA
  };
  private Dictionary<PlayerState.GameMode, HashSet<Achievement>> GAME_MODE_ACHIEVEMENTS = new Dictionary<PlayerState.GameMode, HashSet<Achievement>>(PlayerState.GameModeComparer.Instance)
  {
    {
      PlayerState.GameMode.TIME_LIMIT_V2,
      new HashSet<Achievement>(AchievementComparer.Instance)
      {
        Achievement.TIME_LIMIT_CURRENCY_A,
        Achievement.TIME_LIMIT_CURRENCY_B,
        Achievement.TIME_LIMIT_CURRENCY_C
      }
    }
  };
  private Dictionary<PlayerState.GameMode, HashSet<IntStat>> GAME_MODE_INT_STATS = new Dictionary<PlayerState.GameMode, HashSet<IntStat>>(PlayerState.GameModeComparer.Instance)
  {
    {
      PlayerState.GameMode.TIME_LIMIT_V2,
      new HashSet<IntStat>()
      {
        IntStat.TIME_LIMIT_V2_CURRENCY
      }
    }
  };
  private Dictionary<PlayerState.GameMode, HashSet<BoolStat>> GAME_MODE_BOOL_STATS = new Dictionary<PlayerState.GameMode, HashSet<BoolStat>>(PlayerState.GameModeComparer.Instance)
  {
    {
      PlayerState.GameMode.TIME_LIMIT_V2,
      new HashSet<BoolStat>()
    }
  };
  private Dictionary<PlayerState.GameMode, HashSet<EnumStat>> GAME_MODE_ENUM_STATS = new Dictionary<PlayerState.GameMode, HashSet<EnumStat>>(PlayerState.GameModeComparer.Instance)
  {
    {
      PlayerState.GameMode.TIME_LIMIT_V2,
      new HashSet<EnumStat>()
    }
  };
  private Dictionary<Achievement, Tracker> trackers = new Dictionary<Achievement, Tracker>();
  private TimeDirector timeDir;
  private GameModel gameModel;
  private AchievementAwardUI currPopup;
  private bool quitting;
  private int suppressors;
  private Queue<Achievement> popupQueue = new Queue<Achievement>();
  private List<Updatable> updatables = new List<Updatable>();
  private HashSet<Achievement> postUpdateAchievementChecks = new HashSet<Achievement>();
  private ProfileAchievesModel profileAchievesModel;
  private GameAchievesModel gameAchievesModel;

  public void Awake()
  {
    availableAchievementCountDivisor = 1f / Enum.GetNames(typeof (Achievement)).Length;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    gameModel = SRSingleton<SceneContext>.Instance.GameModel;
  }

  public void InitForLevel()
  {
    gameModel.RegisterProfileAchievements(this);
    gameModel.RegisterGameAchievements(this);
  }

  public void InitModel(GameAchievesModel gameAchievesModel) => gameAchievesModel.Reset();

  public void SetModel(GameAchievesModel gameAchievesModel)
  {
    this.gameAchievesModel = gameAchievesModel;
    if (this.gameAchievesModel == null || profileAchievesModel == null)
      return;
    InitTrackers();
  }

  public void InitModel(ProfileAchievesModel profileAchievesModel) => profileAchievesModel.Reset();

  public void SetModel(ProfileAchievesModel profileAchievesModel)
  {
    this.profileAchievesModel = profileAchievesModel;
    if (gameAchievesModel == null || this.profileAchievesModel == null)
      return;
    InitTrackers();
  }

  public static void SyncAchievements(ProfileAchievesModel profileAchievesModel)
  {
    Log.Debug("Syncing achievements", "count", profileAchievesModel.earnedAchievements.Count);
    SteamDirector.SyncAchievements(profileAchievesModel.earnedAchievements);
  }

  public void ResetProfile()
  {
    profileAchievesModel.Reset();
    SteamDirector.ClearAchievements();
    InitTrackers();
  }

  public void Update()
  {
    foreach (Updatable updatable in updatables)
      updatable.Update();
  }

  public void LateUpdate()
  {
    if (!postUpdateAchievementChecks.Any() || Levels.isSpecialNonAlloc())
      return;
    foreach (Achievement achievementCheck in postUpdateAchievementChecks)
    {
      if (HasAchievement(achievementCheck) || trackers[achievementCheck].Reached())
        AwardAchievement(achievementCheck);
    }
    postUpdateAchievementChecks.Clear();
  }

  private float CalculateGameProgress(int earnedAchievements) => earnedAchievements * availableAchievementCountDivisor;

  private void RegisterUpdatable(Updatable updatable) => updatables.Add(updatable);

  private bool AllowStatUpdate<T>(
    Dictionary<PlayerState.GameMode, HashSet<T>> gameModeStats,
    T statId)
  {
    HashSet<T> objSet;
    return !gameModeStats.TryGetValue(SRSingleton<SceneContext>.Instance.GameModel.currGameMode, out objSet) || objSet.Contains(statId);
  }

  public void AddToStat(IntStat stat, int amount)
  {
    if (!AllowStatUpdate(GAME_MODE_INT_STATS, stat))
      return;
    if (!profileAchievesModel.intStatDict.ContainsKey(stat))
      profileAchievesModel.intStatDict[stat] = amount;
    else
      profileAchievesModel.intStatDict[stat] += amount;
    CheckAchievements(stat);
  }

  public void ResetStat(IntStat stat)
  {
    if (!AllowStatUpdate(GAME_MODE_INT_STATS, stat))
      return;
    profileAchievesModel.intStatDict[stat] = 0;
  }

  public void MaybeUpdateMaxStat(IntStat stat, int val)
  {
    if (!AllowStatUpdate(GAME_MODE_INT_STATS, stat))
      return;
    if (!profileAchievesModel.intStatDict.ContainsKey(stat))
      profileAchievesModel.intStatDict[stat] = val;
    else if (val > profileAchievesModel.intStatDict[stat])
      profileAchievesModel.intStatDict[stat] = val;
    CheckAchievements(stat);
  }

  public int? GetStat(IntStat stat)
  {
    int num;
    return profileAchievesModel.intStatDict.TryGetValue(stat, out num) ? new int?(num) : new int?();
  }

  public void AddToStat(EnumStat stat, Enum val)
  {
    if (!AllowStatUpdate(GAME_MODE_ENUM_STATS, stat))
      return;
    HashSet<Enum> enumSet;
    if (profileAchievesModel.enumStatDict.ContainsKey(stat))
    {
      enumSet = profileAchievesModel.enumStatDict[stat];
    }
    else
    {
      enumSet = new HashSet<Enum>();
      profileAchievesModel.enumStatDict[stat] = enumSet;
    }
    enumSet.Add(val);
    CheckAchievements(stat);
  }

  public void SetStat(BoolStat stat)
  {
    if (!AllowStatUpdate(GAME_MODE_BOOL_STATS, stat))
      return;
    profileAchievesModel.boolStatDict[stat] = true;
    CheckAchievements(stat);
  }

  public void SetStat(GameFloatStat stat, float val)
  {
    gameAchievesModel.gameFloatStatDict[stat] = val;
    CheckAchievements(stat);
  }

  public void SetStat(GameDoubleStat stat, double val)
  {
    gameAchievesModel.gameDoubleStatDict[stat] = val;
    CheckAchievements(stat);
  }

  public void AddToStat(GameIntStat stat, int amt)
  {
    int num = gameAchievesModel.gameIntStatDict.ContainsKey(stat) ? gameAchievesModel.gameIntStatDict[stat] : 0;
    gameAchievesModel.gameIntStatDict[stat] = num + amt;
    SRSingleton<SceneContext>.Instance.PlayerState.OnGameIntStatChanged(stat, gameAchievesModel.gameIntStatDict[stat]);
    CheckAchievements(stat);
  }

  public int GetGameIntStat(GameIntStat stat) => gameAchievesModel.gameIntStatDict.Get(stat);

  public void AddToStat(GameIdDictStat stat, Identifiable.Id id, int amt)
  {
    Dictionary<Identifiable.Id, int> dictionary;
    if (gameAchievesModel.gameIdDictStatDict.ContainsKey(stat))
    {
      dictionary = gameAchievesModel.gameIdDictStatDict[stat];
    }
    else
    {
      dictionary = new Dictionary<Identifiable.Id, int>(Identifiable.idComparer);
      gameAchievesModel.gameIdDictStatDict[stat] = dictionary;
    }
    int num = dictionary.ContainsKey(id) ? dictionary[id] : 0;
    dictionary[id] = num + amt;
    CheckAchievements(stat);
  }

  public Dictionary<Identifiable.Id, int> GetGameIdDictStat(GameIdDictStat stat)
  {
    Dictionary<Identifiable.Id, int> dictionary = gameAchievesModel.gameIdDictStatDict.Get(stat);
    return dictionary == null ? new Dictionary<Identifiable.Id, int>(Identifiable.idComparer) : new Dictionary<Identifiable.Id, int>(dictionary, Identifiable.idComparer);
  }

  private void InitTrackers()
  {
    updatables.Clear();
    trackers[Achievement.SELL_PLORTS_A] = new CountTracker(this, Achievement.SELL_PLORTS_A, IntStat.PLORTS_SOLD, 100);
    trackers[Achievement.SELL_PLORTS_B] = new CountTracker(this, Achievement.SELL_PLORTS_B, IntStat.PLORTS_SOLD, 500);
    trackers[Achievement.SELL_PLORTS_C] = new CountTracker(this, Achievement.SELL_PLORTS_C, IntStat.PLORTS_SOLD, 1000);
    trackers[Achievement.SELL_PLORTS_D] = new CountTracker(this, Achievement.SELL_PLORTS_D, IntStat.PLORTS_SOLD, 2500);
    trackers[Achievement.SELL_PLORTS_E] = new CountTracker(this, Achievement.SELL_PLORTS_E, IntStat.PLORTS_SOLD, 5000);
    trackers[Achievement.DAY_CURRENCY] = new DailyCountTracker(this, Achievement.DAY_CURRENCY, IntStat.DAY_CURRENCY, 5000);
    trackers[Achievement.EARN_CURRENCY_A] = new CountTracker(this, Achievement.EARN_CURRENCY_A, IntStat.CURRENCY, 5000);
    trackers[Achievement.EARN_CURRENCY_B] = new CountTracker(this, Achievement.EARN_CURRENCY_B, IntStat.CURRENCY, 25000);
    trackers[Achievement.EARN_CURRENCY_C] = new CountTracker(this, Achievement.EARN_CURRENCY_C, IntStat.CURRENCY, 100000);
    trackers[Achievement.FEED_SLIMES_CHICKENS] = new CountTracker(this, Achievement.FEED_SLIMES_CHICKENS, IntStat.CHICKENS_FED_SLIMES, 100);
    trackers[Achievement.PINK_SLIMES_FOOD_TYPES] = new CountEnumsTracker(this, Achievement.PINK_SLIMES_FOOD_TYPES, EnumStat.PINK_SLIMES_FOOD_TYPES, 10);
    trackers[Achievement.FEED_AIRBORNE] = new CountTracker(this, Achievement.FEED_AIRBORNE, IntStat.FED_AIRBORNE, 1);
    trackers[Achievement.FEED_FAVORITES] = new CountTracker(this, Achievement.FEED_FAVORITES, IntStat.FED_FAVORITE, 50);
    trackers[Achievement.AWAY_FROM_RANCH] = new SimpleTracker(this, Achievement.AWAY_FROM_RANCH, () => gameAchievesModel.gameDoubleStatDict.ContainsKey(GameDoubleStat.LAST_LEFT_RANCH) && gameAchievesModel.gameDoubleStatDict.ContainsKey(GameDoubleStat.LAST_ENTERED_RANCH) && gameAchievesModel.gameDoubleStatDict[GameDoubleStat.LAST_ENTERED_RANCH] - gameAchievesModel.gameDoubleStatDict[GameDoubleStat.LAST_LEFT_RANCH] >= 86400.0, new GameDoubleStat[2]
    {
      GameDoubleStat.LAST_LEFT_RANCH,
      GameDoubleStat.LAST_ENTERED_RANCH
    });
    trackers[Achievement.AWAKE_UNTIL_MORNING] = new UpdatableSimpleTracker(this, Achievement.AWAKE_UNTIL_MORNING, () => gameAchievesModel.gameDoubleStatDict.ContainsKey(GameDoubleStat.LAST_SLEPT) && gameAchievesModel.gameDoubleStatDict.ContainsKey(GameDoubleStat.LAST_AWOKE) && gameAchievesModel.gameDoubleStatDict[GameDoubleStat.LAST_AWOKE] > gameAchievesModel.gameDoubleStatDict[GameDoubleStat.LAST_SLEPT] && gameAchievesModel.gameDoubleStatDict[GameDoubleStat.LAST_AWOKE] < timeDir.GetHourAfter(-2, 6f) + 3600.0, () =>
    {
      if (!timeDir.OnPassedHour(6f))
        return;
      CheckAchievement(Achievement.AWAKE_UNTIL_MORNING);
    }, new GameDoubleStat[2]
    {
      GameDoubleStat.LAST_SLEPT,
      GameDoubleStat.LAST_AWOKE
    });
    trackers[Achievement.KNOCKOUT_MORNING] = new CountTracker(this, Achievement.KNOCKOUT_MORNING, IntStat.DEATH_BEFORE_10AM, 1);
    trackers[Achievement.FRUIT_TREE_TYPES] = new SimpleTracker(this, Achievement.FRUIT_TREE_TYPES, () => SRSingleton<SceneContext>.Instance.GameModel.GetRanchResourceTypes(Identifiable.FRUIT_CLASS).Count >= 3, Array.Empty<GameDoubleStat>());
    trackers[Achievement.VEGGIE_PATCH_TYPES] = new SimpleTracker(this, Achievement.VEGGIE_PATCH_TYPES, () => SRSingleton<SceneContext>.Instance.GameModel.GetRanchResourceTypes(Identifiable.VEGGIE_CLASS).Count >= 3, Array.Empty<GameDoubleStat>());
    trackers[Achievement.DISCOVERED_QUARRY] = new CountTracker(this, Achievement.DISCOVERED_QUARRY, IntStat.VISITED_QUARRY, 1);
    trackers[Achievement.DISCOVERED_MOSS] = new CountTracker(this, Achievement.DISCOVERED_MOSS, IntStat.VISITED_MOSS, 1);
    trackers[Achievement.DISCOVERED_DESERT] = new CountTracker(this, Achievement.DISCOVERED_DESERT, IntStat.VISITED_DESERT, 1);
    trackers[Achievement.DISCOVERED_RUINS] = new CountTracker(this, Achievement.DISCOVERED_RUINS, IntStat.VISITED_RUINS, 1);
    trackers[Achievement.BURST_GORDO] = new CountTracker(this, Achievement.BURST_GORDO, IntStat.BURST_GORDOS, 1);
    trackers[Achievement.OPEN_SLIME_GATE] = new CountTracker(this, Achievement.OPEN_SLIME_GATE, IntStat.OPENED_SLIME_GATES, 1);
    trackers[Achievement.INCINERATE_ELDER_CHICKEN] = new CountTracker(this, Achievement.INCINERATE_ELDER_CHICKEN, IntStat.INCINERATED_ELDER_CHICKENS, 1);
    trackers[Achievement.INCINERATE_CHICK] = new CountTracker(this, Achievement.INCINERATE_CHICK, IntStat.INCINERATED_CHICKS, 1);
    trackers[Achievement.FILLED_SILO] = new CountTracker(this, Achievement.FILLED_SILO, IntStat.FILLED_SILO, 1);
    trackers[Achievement.RANCH_UPGRADED_STORAGE] = new SimpleTracker(this, Achievement.RANCH_UPGRADED_STORAGE, () => gameModel.IncludesFullyUpgradedCorralCoopAndSilo(), Array.Empty<GameDoubleStat>());
    trackers[Achievement.FULFILL_EXCHANGE_EARLY] = new CountTracker(this, Achievement.FULFILL_EXCHANGE_EARLY, IntStat.FULFILL_EXCHANGE_EARLY, 1);
    trackers[Achievement.DAY_COLLECT_PLORTS] = new DailyCountTracker(this, Achievement.DAY_COLLECT_PLORTS, IntStat.DAY_COLLECT_PLORTS, 50);
    trackers[Achievement.GOLD_SLIME_TRIPLE_PLORT] = new CountTracker(this, Achievement.GOLD_SLIME_TRIPLE_PLORT, IntStat.GOLD_SLIME_TRIPLE_PLORT, 1);
    trackers[Achievement.EXTENDED_RAD_EXPOSURE] = new CountTracker(this, Achievement.EXTENDED_RAD_EXPOSURE, IntStat.EXTENDED_RAD_EXPOSURE, 15);
    trackers[Achievement.EXTENDED_TARR_HOLD] = new CountTracker(this, Achievement.EXTENDED_TARR_HOLD, IntStat.EXTENDED_TARR_HOLD, 15);
    trackers[Achievement.TABBY_HEADBUTT] = new CountTracker(this, Achievement.TABBY_HEADBUTT, IntStat.TABBY_HEADBUTT, 1);
    trackers[Achievement.LAUNCHED_BOOM_EXPLODE] = new CountTracker(this, Achievement.LAUNCHED_BOOM_EXPLODE, IntStat.LAUNCHED_BOOM_EXPLODE, 1);
    trackers[Achievement.MANY_SLIMES_IN_VAC] = new CountTracker(this, Achievement.MANY_SLIMES_IN_VAC, IntStat.SLIMES_IN_VAC, 15);
    trackers[Achievement.CORRAL_SLIME_TYPES] = new CountTracker(this, Achievement.CORRAL_SLIME_TYPES, IntStat.CORRAL_SLIME_TYPES, 6);
    trackers[Achievement.CORRAL_LARGO_TYPES] = new CountTracker(this, Achievement.CORRAL_LARGO_TYPES, IntStat.CORRAL_LARGO_TYPES, 3);
    trackers[Achievement.POND_SLIME_TYPES] = new CountTracker(this, Achievement.POND_SLIME_TYPES, IntStat.POND_SLIME_TYPES, 5);
    trackers[Achievement.RANCH_LARGO_TYPES] = new CountTracker(this, Achievement.RANCH_LARGO_TYPES, IntStat.RANCH_LARGO_TYPES, 10);
    trackers[Achievement.ENTERED_CORRAL_SLIMES] = new CountTracker(this, Achievement.ENTERED_CORRAL_SLIMES, IntStat.ENTERED_CORRAL_SLIMES, 40);
    trackers[Achievement.TIME_LIMIT_CURRENCY_A] = new CountTracker(this, Achievement.TIME_LIMIT_CURRENCY_A, IntStat.TIME_LIMIT_V2_CURRENCY, 10000);
    trackers[Achievement.TIME_LIMIT_CURRENCY_B] = new CountTracker(this, Achievement.TIME_LIMIT_CURRENCY_B, IntStat.TIME_LIMIT_V2_CURRENCY, 35000);
    trackers[Achievement.TIME_LIMIT_CURRENCY_C] = new CountTracker(this, Achievement.TIME_LIMIT_CURRENCY_C, IntStat.TIME_LIMIT_V2_CURRENCY, 75000);
    trackers[Achievement.FABRICATE_GADGETS_A] = new GameCountTracker(this, Achievement.FABRICATE_GADGETS_A, GameIntStat.FABRICATED_GADGETS, 1);
    trackers[Achievement.FABRICATE_GADGETS_B] = new GameCountTracker(this, Achievement.FABRICATE_GADGETS_B, GameIntStat.FABRICATED_GADGETS, 35);
    trackers[Achievement.FABRICATE_GADGETS_C] = new GameCountTracker(this, Achievement.FABRICATE_GADGETS_C, GameIntStat.FABRICATED_GADGETS, 100);
    trackers[Achievement.SLIMEBALL_SCORE] = new CountTracker(this, Achievement.SLIMEBALL_SCORE, IntStat.SLIMEBALL_SCORE, 50);
    trackers[Achievement.SLIME_STAGE_TARR] = new CountTracker(this, Achievement.SLIME_STAGE_TARR, IntStat.SLIME_STAGE_TARRS, 1);
    trackers[Achievement.JOIN_REWARDS_CLUB] = new CountTracker(this, Achievement.JOIN_REWARDS_CLUB, IntStat.REWARD_LEVELS, 1);
    trackers[Achievement.USE_CHROMAS] = new CountEnumsTracker(this, Achievement.USE_CHROMAS, EnumStat.USE_CHROMAS, 3);
    trackers[Achievement.COLLECT_SLIME_TOYS] = new CountEnumsTracker(this, Achievement.COLLECT_SLIME_TOYS, EnumStat.SLIME_TOYS_BOUGHT, 10);
    trackers[Achievement.SNARE_HUNTER_GORDO] = new CountTracker(this, Achievement.SNARE_HUNTER_GORDO, IntStat.SNARED_HUNTER_GORDOS, 1);
    trackers[Achievement.ACTIVATE_OASIS] = new CountTracker(this, Achievement.ACTIVATE_OASIS, IntStat.ACTIVATED_OASES, 1);
    trackers[Achievement.COMPLETE_SLIMEPEDIA] = new CountTracker(this, Achievement.COMPLETE_SLIMEPEDIA, IntStat.COMPLETED_SLIMEPEDIA, 1);
    trackers[Achievement.FIND_HOBSONS_END] = new CountTracker(this, Achievement.FIND_HOBSONS_END, IntStat.FIND_HOBSONS_END, 1);
    trackers[Achievement.FINISH_ADVENTURE] = new CountTracker(this, Achievement.FINISH_ADVENTURE, IntStat.FINISH_ADVENTURE, 1);
  }

  public void CheckAchievement(Achievement achievement) => CheckAchievements(Enumerable.Repeat(achievement, 1));

  private void CheckAchievements(
    IEnumerable<Achievement> achievements)
  {
    postUpdateAchievementChecks.UnionWith(achievements);
  }

  private void CheckAchievements(BoolStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(IntStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(EnumStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(GameFloatStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(GameDoubleStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(GameIntStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private void CheckAchievements(GameIdDictStat stat) => CheckAchievements(trackers.Where(p => p.Value.IsTracking(stat)).Select(p => p.Key));

  private bool AwardAchievement(Achievement achievement)
  {
    HashSet<Achievement> achievementSet;
    if (GAME_MODE_ACHIEVEMENTS.TryGetValue(SRSingleton<SceneContext>.Instance.GameModel.currGameMode, out achievementSet) && !achievementSet.Contains(achievement))
      return false;
    int num = profileAchievesModel.earnedAchievements.Add(achievement) ? 1 : 0;
    SteamDirector.AddAchievement(achievement);
    if (num == 0)
      return num != 0;
    MaybeShowPopup(achievement);
    AnalyticsUtil.CustomEvent("Achievement", new Dictionary<string, object>()
    {
      {
        "id",
        achievement.ToString()
      }
    });
    return num != 0;
  }

  private void MaybeShowPopup(Achievement achievement)
  {
    popupQueue.Enqueue(achievement);
    MaybePopupNext();
  }

  public void RegisterSuppressor() => ++suppressors;

  public void UnregisterSuppressor()
  {
    --suppressors;
    if (suppressors > 0)
      return;
    MaybePopupNext();
  }

  private void MaybePopupNext()
  {
    if (popupQueue.Count <= 0 || !(currPopup == null) || suppressors > 0)
      return;
    Achievement idEntry = popupQueue.Dequeue();
    Instantiate(achievementAwardUIPrefab).GetComponent<AchievementAwardUI>().Init(idEntry);
  }

  public void OnApplicationQuit() => quitting = true;

  public void PopupActivated(AchievementAwardUI popup)
  {
    if (currPopup != null)
      Log.Warning("Popup arrived with already-active popup.");
    currPopup = popup;
  }

  public void PopupDeactivated(AchievementAwardUI popup)
  {
    if (currPopup == popup && !quitting)
    {
      currPopup = null;
      timeDir.OnUnpause(OnUnpause);
    }
    else
      Log.Warning("Popup deactivated, but wasn't current popup.");
  }

  public void OnDestroy() => timeDir.ClearOnUnpause(OnUnpause);

  public void OnUnpause() => MaybePopupNext();

  public bool HasAchievement(Achievement achievement) => profileAchievesModel.earnedAchievements.Contains(achievement);

  public void GetProgress(
    Achievement achievement,
    out int progress,
    out int outOf)
  {
    if (!trackers.ContainsKey(achievement))
    {
      progress = 0;
      outOf = 1;
    }
    else
      trackers[achievement].GetProgress(out progress, out outOf);
  }

  public void GetOverallProgress(out int progress, out int outOf)
  {
    progress = profileAchievesModel.earnedAchievements.Count;
    outOf = Enum.GetValues(typeof (Achievement)).Length;
  }

  public Sprite GetAchievementImage(string achievementKey, Achievement achieve)
  {
    Sprite achievementImage = Resources.Load("Achievements/" + achievementKey, typeof (Sprite)) as Sprite;
    if (!(achievementImage == null))
      return achievementImage;
    if (TIER_1.Contains(achieve))
      return tier1DefaultIcon;
    if (TIER_2.Contains(achieve))
      return tier2DefaultIcon;
    return TIER_3.Contains(achieve) ? tier3DefaultIcon : tier1DefaultIcon;
  }

  protected bool HasMissingTieredAchieves()
  {
    foreach (Achievement achievement in Enum.GetValues(typeof (Achievement)))
    {
      if (!TIER_1.Contains(achievement) && !TIER_2.Contains(achievement) && !TIER_3.Contains(achievement))
      {
        Log.Error("Missing achieve tier: " + achievement);
        return true;
      }
    }
    return false;
  }

  public enum Achievement
  {
    SELL_PLORTS_A,
    SELL_PLORTS_B,
    SELL_PLORTS_C,
    SELL_PLORTS_D,
    SELL_PLORTS_E,
    FEED_SLIMES_CHICKENS,
    FRUIT_TREE_TYPES,
    VEGGIE_PATCH_TYPES,
    EARN_CURRENCY_A,
    EARN_CURRENCY_B,
    EARN_CURRENCY_C,
    DAY_CURRENCY,
    AWAKE_UNTIL_MORNING,
    KNOCKOUT_MORNING,
    AWAY_FROM_RANCH,
    PINK_SLIMES_FOOD_TYPES,
    FEED_AIRBORNE,
    DISCOVERED_QUARRY,
    DISCOVERED_MOSS,
    DISCOVERED_DESERT,
    BURST_GORDO,
    OPEN_SLIME_GATE,
    INCINERATE_ELDER_CHICKEN,
    FEED_FAVORITES,
    FILLED_SILO,
    RANCH_UPGRADED_STORAGE,
    FULFILL_EXCHANGE_EARLY,
    DAY_COLLECT_PLORTS,
    GOLD_SLIME_TRIPLE_PLORT,
    EXTENDED_RAD_EXPOSURE,
    EXTENDED_TARR_HOLD,
    TABBY_HEADBUTT,
    LAUNCHED_BOOM_EXPLODE,
    MANY_SLIMES_IN_VAC,
    CORRAL_SLIME_TYPES,
    CORRAL_LARGO_TYPES,
    POND_SLIME_TYPES,
    RANCH_LARGO_TYPES,
    ENTERED_CORRAL_SLIMES,
    INCINERATE_CHICK,
    TIME_LIMIT_CURRENCY_A,
    TIME_LIMIT_CURRENCY_B,
    TIME_LIMIT_CURRENCY_C,
    DISCOVERED_RUINS,
    FABRICATE_GADGETS_A,
    FABRICATE_GADGETS_B,
    FABRICATE_GADGETS_C,
    SLIME_STAGE_TARR,
    SLIMEBALL_SCORE,
    JOIN_REWARDS_CLUB,
    USE_CHROMAS,
    COLLECT_SLIME_TOYS,
    FIND_HOBSONS_END,
    SNARE_HUNTER_GORDO,
    ACTIVATE_OASIS,
    FINISH_ADVENTURE,
    COMPLETE_SLIMEPEDIA,
  }

  public class AchievementComparer : IEqualityComparer<Achievement>
  {
    public static AchievementComparer Instance = new AchievementComparer();

    public bool Equals(Achievement a, Achievement b) => a == b;

    public int GetHashCode(Achievement a) => (int) a;
  }

  public enum BoolStat
  {
  }

  public enum IntStat
  {
    PLORTS_SOLD,
    CHICKENS_FED_SLIMES,
    DAY_CURRENCY,
    CURRENCY,
    DEATH_BEFORE_10AM,
    FED_AIRBORNE,
    VISITED_QUARRY,
    VISITED_MOSS,
    VISITED_DESERT,
    BURST_GORDOS,
    OPENED_SLIME_GATES,
    INCINERATED_ELDER_CHICKENS,
    FED_FAVORITE,
    FILLED_SILO,
    FULFILL_EXCHANGE_EARLY,
    DAY_COLLECT_PLORTS,
    GOLD_SLIME_TRIPLE_PLORT,
    EXTENDED_RAD_EXPOSURE,
    EXTENDED_TARR_HOLD,
    TABBY_HEADBUTT,
    LAUNCHED_BOOM_EXPLODE,
    SLIMES_IN_VAC,
    CORRAL_SLIME_TYPES,
    CORRAL_LARGO_TYPES,
    POND_SLIME_TYPES,
    RANCH_LARGO_TYPES,
    ENTERED_CORRAL_SLIMES,
    INCINERATED_CHICKS,
    [Obsolete("use TIME_LIMIT_V2_CURRENCY", true)] TIME_LIMIT_CURRENCY,
    SLIMEBALL_SCORE,
    SLIME_STAGE_TARRS,
    VISITED_RUINS,
    REWARD_LEVELS,
    SNARED_HUNTER_GORDOS,
    ACTIVATED_OASES,
    COMPLETED_SLIMEPEDIA,
    FIND_HOBSONS_END,
    FINISH_ADVENTURE,
    TIME_LIMIT_V2_CURRENCY,
  }

  public enum EnumStat
  {
    PINK_SLIMES_FOOD_TYPES,
    RANCH_FRUIT_TYPES,
    RANCH_VEGGIE_TYPES,
    SLIME_TOYS_BOUGHT,
    USE_CHROMAS,
  }

  public enum GameFloatStat
  {
  }

  public enum GameDoubleStat
  {
    LAST_LEFT_RANCH,
    LAST_ENTERED_RANCH,
    LAST_SLEPT,
    LAST_AWOKE,
  }

  public enum GameIntStat
  {
    DEATHS,
    UPGRADES_PURCHASED,
    CURRENCY_SPENT,
    FABRICATED_GADGETS,
  }

  public enum GameIdDictStat
  {
    PLORT_TYPES_SOLD,
  }

  public interface Updatable
  {
    void Update();
  }

  public abstract class Tracker
  {
    public AchievementsDirector dir { get; private set; }

    public Achievement achievement { get; private set; }

    public Tracker(AchievementsDirector dir, Achievement achievement)
    {
      this.dir = dir;
      this.achievement = achievement;
    }

    public abstract bool Reached();

    public abstract void GetProgress(out int progress, out int outOf);

    public virtual bool IsTracking(BoolStat stat) => false;

    public virtual bool IsTracking(IntStat stat) => false;

    public virtual bool IsTracking(EnumStat stat) => false;

    public virtual bool IsTracking(GameFloatStat stat) => false;

    public virtual bool IsTracking(GameDoubleStat stat) => false;

    public virtual bool IsTracking(GameIntStat stat) => false;

    public virtual bool IsTracking(GameIdDictStat stat) => false;
  }

  public class BoolTracker : Tracker
  {
    protected BoolStat stat;

    public BoolTracker(
      AchievementsDirector dir,
      Achievement achievement,
      BoolStat stat)
      : base(dir, achievement)
    {
      this.stat = stat;
    }

    public override bool Reached() => dir.profileAchievesModel.boolStatDict.ContainsKey(stat) && dir.profileAchievesModel.boolStatDict[stat];

    public override void GetProgress(out int progress, out int outOf)
    {
      progress = !dir.profileAchievesModel.boolStatDict.ContainsKey(stat) || !dir.profileAchievesModel.boolStatDict[stat] ? 0 : 1;
      outOf = 1;
    }

    public override bool IsTracking(BoolStat stat) => this.stat == stat;
  }

  public class CountTracker : Tracker
  {
    protected int count;
    protected IntStat stat;

    public CountTracker(
      AchievementsDirector dir,
      Achievement achievement,
      IntStat stat,
      int count)
      : base(dir, achievement)
    {
      this.count = count;
      this.stat = stat;
    }

    public override bool Reached() => dir.profileAchievesModel.intStatDict.ContainsKey(stat) && dir.profileAchievesModel.intStatDict[stat] >= count;

    public override void GetProgress(out int progress, out int outOf)
    {
      progress = dir.profileAchievesModel.intStatDict.ContainsKey(stat) ? Math.Min(count, dir.profileAchievesModel.intStatDict[stat]) : 0;
      outOf = count;
    }

    public override bool IsTracking(IntStat stat) => this.stat == stat;
  }

  public class GameCountTracker : Tracker
  {
    protected int count;
    protected GameIntStat stat;

    public GameCountTracker(
      AchievementsDirector dir,
      Achievement achievement,
      GameIntStat stat,
      int count)
      : base(dir, achievement)
    {
      this.count = count;
      this.stat = stat;
    }

    public override bool Reached() => dir.gameAchievesModel.gameIntStatDict.ContainsKey(stat) && dir.gameAchievesModel.gameIntStatDict[stat] >= count;

    public override void GetProgress(out int progress, out int outOf)
    {
      progress = dir.gameAchievesModel.gameIntStatDict.ContainsKey(stat) ? Math.Min(count, dir.gameAchievesModel.gameIntStatDict[stat]) : 0;
      outOf = count;
    }

    public override bool IsTracking(GameIntStat stat) => this.stat == stat;
  }

  public class DailyCountTracker : CountTracker, Updatable
  {
    private int lastDay;

    public DailyCountTracker(
      AchievementsDirector dir,
      Achievement achievement,
      IntStat stat,
      int count)
      : base(dir, achievement, stat, count)
    {
      dir.RegisterUpdatable(this);
    }

    public void Update()
    {
      int num = dir.timeDir.CurrDay();
      if (num > lastDay)
        dir.ResetStat(stat);
      lastDay = num;
    }
  }

  public class CountEnumsTracker : Tracker
  {
    protected int count;
    protected EnumStat stat;

    public CountEnumsTracker(
      AchievementsDirector dir,
      Achievement achievement,
      EnumStat stat,
      int count)
      : base(dir, achievement)
    {
      this.count = count;
      this.stat = stat;
    }

    public override bool Reached() => dir.profileAchievesModel.enumStatDict.ContainsKey(stat) && dir.profileAchievesModel.enumStatDict[stat].Count >= count;

    public override void GetProgress(out int progress, out int outOf)
    {
      progress = dir.profileAchievesModel.enumStatDict.ContainsKey(stat) ? Math.Min(count, dir.profileAchievesModel.enumStatDict[stat].Count) : 0;
      outOf = count;
    }

    public override bool IsTracking(EnumStat stat) => this.stat == stat;
  }

  public class SimpleTracker : Tracker
  {
    private ReachedDelegate reachedDel;
    private GameDoubleStat[] stats;

    public SimpleTracker(
      AchievementsDirector dir,
      Achievement achievement,
      ReachedDelegate reachedDel,
      params GameDoubleStat[] stats)
      : base(dir, achievement)
    {
      this.reachedDel = reachedDel;
      this.stats = stats;
    }

    public override bool Reached() => reachedDel();

    public override void GetProgress(out int progress, out int outOf)
    {
      progress = dir.profileAchievesModel.earnedAchievements.Contains(achievement) ? 1 : 0;
      outOf = 1;
    }

    public override bool IsTracking(GameDoubleStat stat)
    {
      foreach (GameDoubleStat stat1 in stats)
      {
        if (stat1 == stat)
          return true;
      }
      return false;
    }

    public delegate bool ReachedDelegate();
  }

  public class UpdatableSimpleTracker : 
    SimpleTracker,
    Updatable
  {
    private UpdateDelegate updateDel;

    public UpdatableSimpleTracker(
      AchievementsDirector dir,
      Achievement achievement,
      ReachedDelegate reachedDel,
      UpdateDelegate updateDel,
      params GameDoubleStat[] stats)
      : base(dir, achievement, reachedDel, stats)
    {
      dir.RegisterUpdatable(this);
      this.updateDel = updateDel;
    }

    public void Update() => updateDel();

    public delegate void UpdateDelegate();
  }
}
