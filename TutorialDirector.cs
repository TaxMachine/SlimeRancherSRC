// Decompiled with JetBrains decompiler
// Type: TutorialDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialDirector : SRBehaviour, TutorialsModel.Participant
{
  private static readonly Id[] ID_PRIORITIES = new Id[41]
  {
    Id.DEATH,
    Id.BASIC_MOVEMENT,
    Id.JUMPING,
    Id.VACCING,
    Id.SHOOTING,
    Id.FOOD,
    Id.PLORT,
    Id.MARKET,
    Id.GARDEN,
    Id.EXPLORE,
    Id.SCIENCE_BARN,
    Id.SCIENCE_SUMMARY,
    Id.SCIENCE_REFINERY,
    Id.SCIENCE_BUILDER_SHOP,
    Id.SCIENCE_FABRICATOR,
    Id.SCIENCE_GADGET_MODE,
    Id.SCIENCE_SITES,
    Id.SCIENCE_PLACE_SITE,
    Id.SCIENCE_WRAPUP,
    Id.LARGO,
    Id.MAP,
    Id.WATER,
    Id.MODE_TIME_LIMIT,
    Id.ENTER_ZONE_OGDEN_RANCH,
    Id.WILDS_SLIMEPEDIA,
    Id.ENTER_ZONE_MOCHI_RANCH,
    Id.VALLEY_SLIMEPEDIA,
    Id.RACE_START,
    Id.RACE_GENERATOR,
    Id.RACE_CHECKPOINT,
    Id.RACE_PULSESHOT,
    Id.RACE_POWERUP,
    Id.RACE_ENERGYBOOST,
    Id.ENTER_ZONE_VIKTOR_LAB,
    Id.SLIMULATIONS_SLIMEPEDIA,
    Id.SLIMULATIONS_START_1,
    Id.SLIMULATIONS_START_2,
    Id.SLIMULATIONS_DEBUG_SPRAY,
    Id.SLIMULATIONS_DAMAGE,
    Id.SLIMULATIONS_EXIT_AVAILABLE,
    Id.APPEARANCE_UI
  };
  private static readonly Id[] PRE_EXPLORE_TUTS = new Id[7]
  {
    Id.BASIC_MOVEMENT,
    Id.JUMPING,
    Id.VACCING,
    Id.SHOOTING,
    Id.FOOD,
    Id.PLORT,
    Id.MARKET
  };
  private static readonly Id[] PRE_SCIENCE_WRAPUP_TUTS = new Id[8]
  {
    Id.SCIENCE_BARN,
    Id.SCIENCE_SUMMARY,
    Id.SCIENCE_REFINERY,
    Id.SCIENCE_BUILDER_SHOP,
    Id.SCIENCE_FABRICATOR,
    Id.SCIENCE_GADGET_MODE,
    Id.SCIENCE_SITES,
    Id.SCIENCE_PLACE_SITE
  };
  private static HashSet<Id> EXPERIENCED_TUTS = new HashSet<Id>(IdComparer.Instance)
  {
    Id.MODE_TIME_LIMIT
  };
  private static HashSet<Id> ADVANCED_TUTS = new HashSet<Id>(IdComparer.Instance)
  {
    Id.SCIENCE_BARN,
    Id.SCIENCE_SUMMARY,
    Id.SCIENCE_REFINERY,
    Id.SCIENCE_BUILDER_SHOP,
    Id.SCIENCE_FABRICATOR,
    Id.SCIENCE_GADGET_MODE,
    Id.SCIENCE_SITES,
    Id.SCIENCE_PLACE_SITE,
    Id.SCIENCE_WRAPUP,
    Id.ENTER_ZONE_OGDEN_RANCH,
    Id.WILDS_SLIMEPEDIA,
    Id.ENTER_ZONE_MOCHI_RANCH,
    Id.VALLEY_SLIMEPEDIA,
    Id.RACE_START,
    Id.RACE_GENERATOR,
    Id.RACE_CHECKPOINT,
    Id.RACE_PULSESHOT,
    Id.RACE_POWERUP,
    Id.RACE_ENERGYBOOST,
    Id.ENTER_ZONE_VIKTOR_LAB,
    Id.SLIMULATIONS_SLIMEPEDIA,
    Id.SLIMULATIONS_START_1,
    Id.SLIMULATIONS_START_2,
    Id.SLIMULATIONS_DEBUG_SPRAY,
    Id.SLIMULATIONS_DAMAGE,
    Id.SLIMULATIONS_EXIT_AVAILABLE
  };
  private static HashSet<Id> FORCED_TUTS = new HashSet<Id>(IdComparer.Instance)
  {
    Id.APPEARANCE_UI
  };
  private const float CLOSE_AFTER_ACTION_DELAY = 6f;
  private const float WAIT_FOR_SHORT_READ_DELAY = 10f;
  private const float WAIT_FOR_READ_DELAY = 20f;
  private Dictionary<Id, Tutorial> trackers = new Dictionary<Id, Tutorial>();
  private Dictionary<Id, Id[]> dependencies = new Dictionary<Id, Id[]>();
  public GameObject tutorialPopupPrefab;
  public IdEntry[] entries;
  private Dictionary<Id, IdEntry> entryDict = new Dictionary<Id, IdEntry>();
  private HashSet<Id> hidden = new HashSet<Id>();
  private bool quitting;
  private TutorialPopupUI currPopup;
  private Tutorial currTracker;
  private SortedList<int, Id> popupQueue = new SortedList<int, Id>();
  private TimeDirector timeDir;
  private OptionsDirector optionsDir;
  private ProgressDirector progressDir;
  private bool inMarketArea;
  private bool inBarnArea;
  private int suppressors;
  private TutorialsModel tutModel;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    optionsDir = SRSingleton<GameContext>.Instance.OptionsDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    foreach (IdEntry entry in entries)
      entryDict[entry.id] = entry;
    InitTrackers();
    InitDependencies();
  }

  public void InitForLevel()
  {
    SRSingleton<SceneContext>.Instance.GameModel.RegisterTutorials(this);
    popupQueue.Clear();
    InitTrackers();
    InitDependencies();
    SceneContext.onSceneLoaded += ctx => MaybeShowStatusTutorials();
  }

  public void InitModel(TutorialsModel tutModel)
  {
  }

  public void SetModel(TutorialsModel tutModel) => this.tutModel = tutModel;

  public void Update()
  {
    if (currTracker == null)
      return;
    currTracker.Update();
    if (currTracker.MarkCompleted())
      currPopup.CompletedAction();
    if (currTracker.ShouldEnd())
    {
      currPopup.Complete();
    }
    else
    {
      if (!currTracker.ShouldHide())
        return;
      currPopup.Hide();
    }
  }

  public void OnVac(Identifiable.Id id)
  {
    if (currTracker is VacTutorial)
      ((VacTutorial) currTracker).OnVac();
    if (Identifiable.IsSlime(id))
      MaybeShowPopup(Id.SHOOTING);
    else if (Identifiable.IsFood(id))
    {
      MaybeShowPopup(Id.FOOD);
    }
    else
    {
      if (!Identifiable.IsPlort(id))
        return;
      MaybeShowPopup(Id.PLORT);
    }
  }

  public void OnProgress(ProgressDirector.ProgressType type)
  {
    if (type != ProgressDirector.ProgressType.UNLOCK_LAB)
      return;
    MaybeShowScienceTutorial();
  }

  public void OnShoot(Identifiable.Id id)
  {
    if (currTracker == null)
      return;
    currTracker.OnShot(id);
  }

  public void OnDelaunchedSlime()
  {
    if (!(currTracker is SlimeShootingTutorial))
      return;
    ((SlimeShootingTutorial) currTracker).OnDelaunchedSlime();
  }

  public void OnEnteredRanch()
  {
    if (currTracker is RanchOnlyTutorial)
      ((RanchOnlyTutorial) currTracker).EnteredRanch();
    List<Id> idList = new List<Id>(hidden);
    hidden.Clear();
    foreach (Id id in idList)
      MaybeShowPopup(id);
  }

  public void OnLeftRanch()
  {
    if (!(currTracker is RanchOnlyTutorial))
      return;
    ((RanchOnlyTutorial) currTracker).LeftRanch();
  }

  public void RemoveHidden(Id id) => hidden.Remove(id);

  public void SetInMarketArea(bool inArea)
  {
    inMarketArea = inArea;
    if (!inMarketArea)
      return;
    MaybeShowPopup(Id.MARKET);
  }

  public void SetInBarnArea(bool inArea)
  {
    inBarnArea = inArea;
    if (!inBarnArea)
      return;
    MaybeShowPopup(Id.SCIENCE_SUMMARY);
    MaybeShowPopup(Id.SCIENCE_REFINERY);
    MaybeShowPopup(Id.SCIENCE_BUILDER_SHOP);
    MaybeShowPopup(Id.SCIENCE_FABRICATOR);
    MaybeShowPopup(Id.SCIENCE_GADGET_MODE);
  }

  public void OnPlortSold()
  {
    if (!(currTracker is MarketTutorial))
      return;
    ((MarketTutorial) currTracker).OnPlortSold();
  }

  public void OnPlanted()
  {
    if (!(currTracker is GardenTutorial))
      return;
    ((GardenTutorial) currTracker).OnPlanted();
  }

  public void OnPlayerDeath(PlayerDeathHandler.DeathType deathType)
  {
    if (deathType != PlayerDeathHandler.DeathType.DEFAULT)
      return;
    MaybeShowPopup(Id.DEATH);
  }

  public void OnRefineryAdded()
  {
    if (!(currTracker is RefineryTutorial))
      return;
    ((RefineryTutorial) currTracker).OnRefineryAdded();
  }

  public void OnGadgetModeActivated()
  {
    if (currTracker is GadgetModeTutorial)
      ((GadgetModeTutorial) currTracker).OnGadgetModeActivated();
    MaybeShowPopup(Id.SCIENCE_SITES);
    MaybeShowPopup(Id.SCIENCE_PLACE_SITE);
  }

  public void OnLiquidSlotGained() => MaybeShowPopup(Id.WATER);

  public void OnMapDataGained() => MaybeShowPopup(Id.MAP);

  public void OnFabricatorOpen()
  {
    if (!(currTracker is FabricatorTutorial))
      return;
    ((FabricatorTutorial) currTracker).OnFabricatorOpen();
  }

  public void OnBuilderShopOpen()
  {
    if (!(currTracker is BuilderShopTutorial))
      return;
    ((BuilderShopTutorial) currTracker).OnBuilderShopOpen();
  }

  public void OnPlaceGadgetOpen()
  {
    if (!(currTracker is PlaceGadgetTutorial))
      return;
    ((PlaceGadgetTutorial) currTracker).OnPlaceGadgetOpen();
  }

  public void OnMailRead(MailDirector.Mail mail)
  {
    if (mail.key == "ogden_invite")
      MaybeShowPopup(Id.ENTER_ZONE_OGDEN_RANCH);
    else if (mail.key == "mochi_invite")
    {
      MaybeShowPopup(Id.ENTER_ZONE_MOCHI_RANCH);
    }
    else
    {
      if (!(mail.key == "viktor_invite"))
        return;
      MaybeShowPopup(Id.ENTER_ZONE_VIKTOR_LAB);
    }
  }

  public void OnQuicksilverRaceActivated()
  {
    if (!(currTracker is QuicksilverRaceTutorial))
      return;
    ((QuicksilverRaceTutorial) currTracker).OnQuicksilverRaceActivated();
  }

  public void SuppressTutorials() => ++suppressors;

  public void UnsuppressTutorials()
  {
    --suppressors;
    if (suppressors > 0)
      return;
    MaybePopupNext();
  }

  private void InitTrackers()
  {
    trackers.Clear();
    trackers[Id.BASIC_MOVEMENT] = new MoveTutorial();
    trackers[Id.JUMPING] = new JumpTutorial();
    trackers[Id.VACCING] = new VacTutorial();
    trackers[Id.SHOOTING] = new SlimeShootingTutorial();
    trackers[Id.FOOD] = new WaitForShotTutorial(Identifiable.FOOD_CLASS);
    trackers[Id.PLORT] = new PlortTutorial(this);
    trackers[Id.MARKET] = new MarketTutorial();
    trackers[Id.DEATH] = new TimeOnlyTutorial(20f);
    trackers[Id.GARDEN] = new GardenTutorial();
    trackers[Id.EXPLORE] = new TimeOnlyTutorial(20f);
    trackers[Id.LARGO] = new TimeOnlyTutorial(20f);
    trackers[Id.SCIENCE_BARN] = new BarnTutorial(this);
    trackers[Id.SCIENCE_SUMMARY] = new TimeOnlyTutorial(20f);
    trackers[Id.SCIENCE_REFINERY] = new RefineryTutorial();
    trackers[Id.SCIENCE_BUILDER_SHOP] = new BuilderShopTutorial();
    trackers[Id.SCIENCE_FABRICATOR] = new FabricatorTutorial();
    trackers[Id.SCIENCE_GADGET_MODE] = new GadgetModeTutorial();
    trackers[Id.SCIENCE_SITES] = new TimeOnlyTutorial(20f);
    trackers[Id.SCIENCE_PLACE_SITE] = new PlaceGadgetTutorial();
    trackers[Id.SCIENCE_WRAPUP] = new TimeOnlyTutorial(20f);
    trackers[Id.MAP] = new MapTutorial(20f);
    trackers[Id.WATER] = new TimeOnlyTutorial(20f);
    trackers[Id.ENTER_ZONE_OGDEN_RANCH] = new HasProgressTutorial(Id.ENTER_ZONE_OGDEN_RANCH, ProgressDirector.ProgressType.ENTER_ZONE_OGDEN_RANCH);
    trackers[Id.WILDS_SLIMEPEDIA] = new TimeOnlyTutorial(10f);
    trackers[Id.ENTER_ZONE_MOCHI_RANCH] = new HasProgressTutorial(Id.ENTER_ZONE_MOCHI_RANCH, ProgressDirector.ProgressType.ENTER_ZONE_MOCHI_RANCH);
    trackers[Id.VALLEY_SLIMEPEDIA] = new TimeOnlyTutorial(10f);
    trackers[Id.RACE_START] = new QuicksilverRaceTutorial();
    trackers[Id.RACE_GENERATOR] = new TimeOnlyTutorial(10f);
    trackers[Id.RACE_CHECKPOINT] = new TimeOnlyTutorial(10f);
    trackers[Id.RACE_PULSESHOT] = new TimeOnlyTutorial(10f);
    trackers[Id.RACE_POWERUP] = new TimeOnlyTutorial(10f);
    trackers[Id.RACE_ENERGYBOOST] = new TimeOnlyTutorial(10f);
    trackers[Id.ENTER_ZONE_VIKTOR_LAB] = new HasProgressTutorial(Id.ENTER_ZONE_VIKTOR_LAB, ProgressDirector.ProgressType.ENTER_ZONE_VIKTOR_LAB);
    trackers[Id.SLIMULATIONS_SLIMEPEDIA] = new TimeOnlyTutorial(10f);
    trackers[Id.SLIMULATIONS_START_1] = new TimeOnlyTutorial(10f);
    trackers[Id.SLIMULATIONS_START_2] = new TimeOnlyTutorial(10f);
    trackers[Id.SLIMULATIONS_DEBUG_SPRAY] = new TimeOnlyTutorial(10f);
    trackers[Id.SLIMULATIONS_DAMAGE] = new TimeOnlyTutorial(10f);
    trackers[Id.SLIMULATIONS_EXIT_AVAILABLE] = new TimeOnlyTutorial(10f);
    trackers[Id.MODE_TIME_LIMIT] = new TimeOnlyTutorial(20f);
  }

  private void InitDependencies()
  {
    dependencies.Clear();
    dependencies[Id.MARKET] = new Id[1]
    {
      Id.PLORT
    };
    dependencies[Id.SCIENCE_SUMMARY] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_REFINERY] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_BUILDER_SHOP] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_FABRICATOR] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_GADGET_MODE] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_SITES] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
    dependencies[Id.SCIENCE_PLACE_SITE] = new Id[1]
    {
      Id.SCIENCE_BARN
    };
  }

  public void MaybeShowStatusTutorials()
  {
    if (!IsCompleted(Id.VACCING))
    {
      MaybeShowInitTutorial();
    }
    else
    {
      if (IsCompleted(Id.SCIENCE_BARN))
        return;
      MaybeShowScienceTutorial();
    }
  }

  private void MaybeShowInitTutorial()
  {
    if (Levels.isSpecial())
      return;
    MaybeShowPopup(Id.BASIC_MOVEMENT);
    MaybeShowPopup(Id.JUMPING);
    MaybeShowPopup(Id.VACCING);
    if (SRSingleton<SceneContext>.Instance.GameModel.currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
      return;
    MaybeShowPopup(Id.MODE_TIME_LIMIT);
  }

  private void MaybeShowScienceTutorial()
  {
    if (!progressDir.HasProgress(ProgressDirector.ProgressType.UNLOCK_LAB))
      return;
    MaybeShowPopup(Id.SCIENCE_BARN);
  }

  public IdEntry Get(Id id) => entryDict[id];

  public List<Id> GetPopupQueue()
  {
    List<Id> popupQueue = new List<Id>(this.popupQueue.Values);
    if (currTracker != null)
    {
      foreach (KeyValuePair<Id, Tutorial> tracker in trackers)
      {
        if (currTracker == tracker.Value)
        {
          popupQueue.Insert(0, tracker.Key);
          break;
        }
      }
    }
    return popupQueue;
  }

  public void SetPopupQueue(IEnumerable<Id> ids) => StartCoroutine(SetPopupQueueCoroutine(ids));

  private IEnumerator SetPopupQueueCoroutine(IEnumerable<Id> ids)
  {
    yield return new WaitForEndOfFrame();
    foreach (Id id in ids)
      MaybeShowPopup(id);
  }

  private int Priority(Id id)
  {
    int num = Array.IndexOf(ID_PRIORITIES, id);
    return num == -1 ? int.MaxValue : num;
  }

  public void MaybeShowPopup(Id id)
  {
    if (!ShouldPopupDisplay(id))
      return;
    popupQueue.Add(Priority(id), id);
    MaybePopupNext();
  }

  private bool ShouldPopupDisplay(Id id) => !Levels.isSpecial() && IsEnabled(id) && (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().assumeExperiencedUser || EXPERIENCED_TUTS.Contains(id)) && !IsCompleted(id) && !IsHidden(id) && !popupQueue.ContainsValue(id) && (currPopup == null || currPopup.GetId() != id) && DependenciesSatisfied(id);

  public bool IsCompleted(Id tut) => tutModel.completedIds.Contains(tut);

  public bool IsEnabled(Id id)
  {
    switch (optionsDir.enabledTutorials)
    {
      case OptionsDirector.EnabledTutorials.ADVANCED_ONLY:
        return ADVANCED_TUTS.Contains(id) || FORCED_TUTS.Contains(id);
      case OptionsDirector.EnabledTutorials.NONE:
        return FORCED_TUTS.Contains(id);
      default:
        return true;
    }
  }

  public bool IsCompletedOrDisabled(Id id) => IsCompleted(id) || !IsEnabled(id);

  private bool IsHidden(Id tut) => hidden.Contains(tut);

  public void OnApplicationQuit() => quitting = true;

  public void PopupDeactivated(TutorialPopupUI popup, bool doComplete)
  {
    if (currPopup == popup && !quitting)
    {
      if (doComplete)
        Complete(currPopup.GetId());
      else
        Hide(popup.GetId());
      currPopup = null;
      currTracker.End();
      currTracker = null;
      timeDir.OnUnpause(OnUnpause);
    }
    else
      Log.Warning("Popup deactivated, but wasn't current popup.");
  }

  private void Hide(Id id) => hidden.Add(id);

  public void OnDestroy() => timeDir.ClearOnUnpause(OnUnpause);

  public void OnUnpause()
  {
    MaybeShowStatusTutorials();
    MaybePopupNext();
  }

  private bool DependenciesSatisfied(Id id)
  {
    if (!dependencies.ContainsKey(id))
      return true;
    foreach (Id tut in dependencies[id])
    {
      if (!IsCompleted(tut) && (currPopup == null || currPopup.GetId() != tut))
        return false;
    }
    return true;
  }

  private void MaybePopupNext()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || popupQueue.Count <= 0 || !(currPopup == null) || suppressors > 0)
      return;
    Id id = popupQueue.Values[0];
    popupQueue.RemoveAt(0);
    if (!ShouldPopupDisplay(id))
      return;
    if (trackers[id].HideInsteadOfPopup())
    {
      Hide(id);
    }
    else
    {
      currPopup = TutorialPopupUI.CreateTutorialPopup(Get(id)).GetComponent<TutorialPopupUI>();
      currTracker = trackers[id];
      currTracker.Start();
    }
  }

  public bool MarkTutorialCompleted(Id id)
  {
    if (!tutModel.completedIds.Add(id))
      return false;
    AnalyticsUtil.CustomEvent("TutorialComplete", new Dictionary<string, object>()
    {
      {
        nameof (id),
        id.ToString()
      }
    });
    return true;
  }

  private void Complete(Id id)
  {
    if (!MarkTutorialCompleted(id))
      return;
    bool flag1 = true;
    foreach (Id id1 in PRE_EXPLORE_TUTS)
    {
      if (!tutModel.completedIds.Contains(id1))
      {
        flag1 = false;
        break;
      }
    }
    if (flag1)
      MaybeShowPopup(Id.EXPLORE);
    bool flag2 = true;
    foreach (Id id2 in PRE_SCIENCE_WRAPUP_TUTS)
    {
      if (!tutModel.completedIds.Contains(id2))
      {
        flag2 = false;
        break;
      }
    }
    if (!flag2)
      return;
    MaybeShowPopup(Id.SCIENCE_WRAPUP);
  }

  public enum Id
  {
    BASIC_MOVEMENT,
    JUMPING,
    VACCING,
    SHOOTING,
    FOOD,
    PLORT,
    MARKET,
    DEATH,
    GARDEN,
    EXPLORE,
    LARGO,
    SCIENCE_BARN,
    SCIENCE_SUMMARY,
    SCIENCE_REFINERY,
    SCIENCE_BUILDER_SHOP,
    SCIENCE_FABRICATOR,
    SCIENCE_GADGET_MODE,
    SCIENCE_SITES,
    SCIENCE_PLACE_SITE,
    SCIENCE_WRAPUP,
    MAP,
    WATER,
    ENTER_ZONE_OGDEN_RANCH,
    ENTER_ZONE_MOCHI_RANCH,
    RACE_START,
    RACE_GENERATOR,
    RACE_CHECKPOINT,
    RACE_PULSESHOT,
    RACE_POWERUP,
    RACE_ENERGYBOOST,
    MODE_TIME_LIMIT,
    ENTER_ZONE_VIKTOR_LAB,
    SLIMULATIONS_SLIMEPEDIA,
    SLIMULATIONS_START_1,
    SLIMULATIONS_START_2,
    SLIMULATIONS_DEBUG_SPRAY,
    SLIMULATIONS_DAMAGE,
    SLIMULATIONS_EXIT_AVAILABLE,
    WILDS_SLIMEPEDIA,
    VALLEY_SLIMEPEDIA,
    APPEARANCE_UI,
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public static IdComparer Instance = new IdComparer();

    public bool Equals(Id a, Id b) => a == b;

    public int GetHashCode(Id a) => (int) a;
  }

  [Serializable]
  public class IdEntry
  {
    public Id id;
    public Sprite[] images;
    [Tooltip("Additional x/y offset added to the image's initial position.")]
    public Vector3 imageOffset = Vector3.zero;
    [Tooltip("Absolute scale of the image.")]
    public Vector3 imageScale = Vector3.one;
  }

  private interface Tutorial
  {
    void Start();

    void Update();

    bool ShouldEnd();

    bool ShouldHide();

    bool HideInsteadOfPopup();

    bool MarkCompleted();

    void End();

    void OnShot(Identifiable.Id id);
  }

  private class BaseTutorial : Tutorial
  {
    protected float endTime = float.PositiveInfinity;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual bool ShouldEnd() => Time.time >= (double) endTime;

    public virtual bool ShouldHide() => false;

    public virtual bool HideInsteadOfPopup() => false;

    public virtual bool MarkCompleted() => !float.IsPositiveInfinity(endTime);

    public virtual void End()
    {
    }

    public virtual void OnShot(Identifiable.Id id)
    {
    }
  }

  private class RanchOnlyTutorial : BaseTutorial
  {
    private float hideTime = float.PositiveInfinity;
    private const float HIDE_DELAY = 10f;

    public override void Start()
    {
      if (!HideInsteadOfPopup())
        hideTime = float.PositiveInfinity;
      else
        hideTime = Time.time + 10f;
    }

    public override bool ShouldHide() => Time.time >= (double) hideTime;

    public override bool HideInsteadOfPopup() => !CellDirector.IsOnHomeRanch(SRSingleton<SceneContext>.Instance.Player.GetComponent<RegionMember>());

    public void LeftRanch() => hideTime = Time.time + 10f;

    public void EnteredRanch() => hideTime = float.PositiveInfinity;
  }

  private class WaitForShotTutorial : BaseTutorial
  {
    private HashSet<Identifiable.Id> waitForIds;

    public WaitForShotTutorial(HashSet<Identifiable.Id> ids) => waitForIds = ids;

    public override void OnShot(Identifiable.Id id)
    {
      if (!waitForIds.Contains(id))
        return;
      endTime = Time.time + 6f;
    }
  }

  private class TimeOnlyTutorial : BaseTutorial
  {
    private float timeToLive;

    public TimeOnlyTutorial(float timeToLive) => this.timeToLive = timeToLive;

    public override void Start() => endTime = Time.time + timeToLive;

    public override bool MarkCompleted() => false;
  }

  private class MoveTutorial : BaseTutorial
  {
    public override void Update()
    {
      if (!float.IsPositiveInfinity(endTime) || SRInput.Actions.vertical.RawValue == 0.0 && SRInput.Actions.horizontal.RawValue == 0.0)
        return;
      endTime = Time.time + 6f;
    }
  }

  private class JumpTutorial : BaseTutorial
  {
    public override void Update()
    {
      if (!float.IsPositiveInfinity(endTime) || !SRInput.Actions.jump.IsPressed)
        return;
      endTime = Time.time + 6f;
    }
  }

  private class VacTutorial : BaseTutorial
  {
    public void OnVac()
    {
      if (!float.IsPositiveInfinity(endTime))
        return;
      endTime = Time.time + 6f;
    }
  }

  private class MapTutorial : BaseTutorial
  {
    private bool mapOpened;
    private float tutorialTimeout;

    public MapTutorial(float tutorialTimeout) => this.tutorialTimeout = tutorialTimeout;

    public override void Start() => endTime = Time.time + tutorialTimeout;

    public override void Update()
    {
      base.Update();
      if (!SRInput.Actions.openMap.IsPressed)
        return;
      mapOpened = true;
      endTime = Time.time + 6f;
    }

    public override bool MarkCompleted() => mapOpened;
  }

  private abstract class MarkerTutorial : RanchOnlyTutorial
  {
    private Id areaId;

    public MarkerTutorial(Id areaId) => this.areaId = areaId;

    public override void Start()
    {
      base.Start();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == areaId)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = true;
      }
    }

    public override void End()
    {
      base.End();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == areaId)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = false;
      }
    }
  }

  private class PlortTutorial : MarkerTutorial
  {
    protected TutorialDirector dir;

    public PlortTutorial(TutorialDirector dir)
      : base(Id.PLORT)
    {
      this.dir = dir;
    }

    public override bool ShouldEnd() => dir.inMarketArea;
  }

  private class BarnTutorial : MarkerTutorial
  {
    protected TutorialDirector dir;

    public BarnTutorial(TutorialDirector dir)
      : base(Id.SCIENCE_BARN)
    {
      this.dir = dir;
    }

    public override bool ShouldEnd() => dir.inBarnArea;
  }

  private class MarketTutorial : RanchOnlyTutorial
  {
    public void OnPlortSold() => endTime = Time.time + 6f;
  }

  private class RefineryTutorial : MarkerTutorial
  {
    public RefineryTutorial()
      : base(Id.SCIENCE_REFINERY)
    {
    }

    public void OnRefineryAdded() => endTime = Time.time + 6f;
  }

  private class GadgetModeTutorial : BaseTutorial
  {
    public void OnGadgetModeActivated() => endTime = Time.time + 6f;
  }

  private class FabricatorTutorial : MarkerTutorial
  {
    public FabricatorTutorial()
      : base(Id.SCIENCE_FABRICATOR)
    {
    }

    public void OnFabricatorOpen() => endTime = Time.time + 6f;
  }

  private class BuilderShopTutorial : MarkerTutorial
  {
    public BuilderShopTutorial()
      : base(Id.SCIENCE_BUILDER_SHOP)
    {
    }

    public void OnBuilderShopOpen() => endTime = Time.time + 6f;
  }

  private class PlaceGadgetTutorial : BaseTutorial
  {
    public void OnPlaceGadgetOpen() => endTime = Time.time + 6f;
  }

  private class GardenTutorial : RanchOnlyTutorial
  {
    public void OnPlanted()
    {
      endTime = Time.time + 6f;
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == Id.GARDEN)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = false;
      }
    }

    public override void Start()
    {
      base.Start();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == Id.GARDEN)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = true;
      }
      if (!SRSingleton<SceneContext>.Instance.GameModel.AllLandPlots().Values.Any(p => p.typeId == LandPlot.Id.GARDEN && p.HasAttached()))
        return;
      OnPlanted();
    }

    public override void End()
    {
      base.End();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == Id.GARDEN)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = false;
      }
    }
  }

  private class SlimeShootingTutorial : RanchOnlyTutorial
  {
    public void OnDelaunchedSlime() => endTime = Time.time + 6f;

    public override void Start()
    {
      base.Start();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == Id.SHOOTING)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = true;
      }
    }

    public override void End()
    {
      base.End();
      foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
      {
        if (allRadar.tutorialId == Id.SHOOTING)
          allRadar.gameObject.GetComponent<RadarTrackedObject>().enabled = false;
      }
    }
  }

  private class HasProgressTutorial : MarkerTutorial
  {
    private ProgressDirector.ProgressType progress;

    public HasProgressTutorial(Id id, ProgressDirector.ProgressType progress)
      : base(id)
    {
      this.progress = progress;
    }

    public override bool ShouldEnd() => SRSingleton<SceneContext>.Instance.ProgressDirector.HasProgress(progress);
  }

  private class QuicksilverRaceTutorial : BaseTutorial
  {
    private bool isActivated;
    private RadarTrackedObject marker;

    public override void Start()
    {
      base.Start();
      if (marker != null)
      {
        marker.enabled = false;
        marker = null;
      }
      if (IsEnergyGeneratorActivated())
      {
        isActivated = true;
      }
      else
      {
        Vector3 position = SRSingleton<SceneContext>.Instance.Player.transform.position;
        float num = float.MaxValue;
        foreach (TutorialRadar allRadar in TutorialRadar.allRadars)
        {
          if (allRadar.tutorialId == Id.RACE_START)
          {
            float sqrMagnitude = (position - allRadar.gameObject.transform.position).sqrMagnitude;
            if (sqrMagnitude < (double) num)
            {
              marker = allRadar.GetComponent<RadarTrackedObject>();
              num = sqrMagnitude;
            }
          }
        }
        if (!(marker != null))
          return;
        marker.enabled = true;
      }
    }

    public override bool ShouldEnd() => base.ShouldEnd() || isActivated;

    private bool IsEnergyGeneratorActivated()
    {
      foreach (QuicksilverEnergyGenerator allGenerator in QuicksilverEnergyGenerator.allGenerators)
      {
        if (allGenerator.GetState() != QuicksilverEnergyGenerator.State.INACTIVE)
          return true;
      }
      return false;
    }

    public override void End()
    {
      base.End();
      if (!(marker != null))
        return;
      marker.enabled = false;
      marker = null;
    }

    public override bool ShouldHide() => !SRSingleton<SceneContext>.Instance.Player.GetComponent<RegionMember>().IsInZone(ZoneDirector.Zone.VALLEY);

    public void OnQuicksilverRaceActivated() => isActivated = true;
  }
}
