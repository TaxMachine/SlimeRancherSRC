// Decompiled with JetBrains decompiler
// Type: PlayerState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerState : SRBehaviour, PlayerModel.Participant
{
  private GameState gameState;
  public GameObject[] potentialAmmo;
  private PlayerModel model;
  [Tooltip("SFX played each time the player's energy is depleted. (optional)")]
  public SECTR_AudioCue onEnergyDepletedCue;
  public OnAmmoModeChanged onAmmoModeChanged;
  private Dictionary<AmmoMode, Ammo> ammoDict;
  private AmmoMode ammoMode;
  public static UpgradeComparer upgradeComparer = new UpgradeComparer();
  private TimeDirector timeDir;
  private PopupDirector popupDir;
  private AchievementsDirector achieveDir;
  private MailDirector mailDir;
  private MetadataDirector metadataDirector;
  private static readonly Predicate<Identifiable.Id> NO_LIQUID = id => !Identifiable.IsLiquid(id);
  private static readonly Predicate<Identifiable.Id> ONLY_LIQUID = id => Identifiable.IsLiquid(id);
  public static readonly Predicate<Identifiable.Id>[] PLAYER_AMMO_PREDS = new Predicate<Identifiable.Id>[5]
  {
    NO_LIQUID,
    NO_LIQUID,
    NO_LIQUID,
    NO_LIQUID,
    ONLY_LIQUID
  };

  public event OnEndGame onEndGame = () => { };

  public event OnEndGameTimeChanged onEndGameTimeChanged = () => { };

  public bool PointedAtVaccable { get; set; }

  public GameObject Targeting { get; set; }

  public bool InGadgetMode { get; set; }

  public Ammo Ammo => ammoDict[ammoMode];

  public double nextAmmoLossDamageTime { get; set; }

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    popupDir = SRSingleton<SceneContext>.Instance.PopupDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    mailDir = SRSingleton<SceneContext>.Instance.MailDirector;
    metadataDirector = SRSingleton<SceneContext>.Instance.MetadataDirector;
  }

  public void InitModel(PlayerModel model)
  {
    Reset(model);
    model.ammoDict[AmmoMode.DEFAULT] = new AmmoModel();
    model.ammoDict[AmmoMode.NIMBLE_VALLEY] = new AmmoModel();
    ammoDict[AmmoMode.DEFAULT].InitModel(model.ammoDict[AmmoMode.DEFAULT]);
    ammoDict[AmmoMode.NIMBLE_VALLEY].InitModel(model.ammoDict[AmmoMode.NIMBLE_VALLEY]);
  }

  public void SetModel(PlayerModel model)
  {
    this.model = model;
    RegisteredPotentialAmmoChanged(this.model.registeredPotentialAmmo);
    ammoDict[AmmoMode.DEFAULT].SetModel(model.ammoDict[AmmoMode.DEFAULT]);
    ammoDict[AmmoMode.NIMBLE_VALLEY].SetModel(model.ammoDict[AmmoMode.NIMBLE_VALLEY]);
    CheckAllUpgradeLockers();
  }

  public void RegisteredPotentialAmmoChanged(
    Dictionary<AmmoMode, List<GameObject>> registeredPotentialAmmo)
  {
    if (ammoDict == null || registeredPotentialAmmo == null)
      return;
    foreach (KeyValuePair<AmmoMode, List<GameObject>> keyValuePair in registeredPotentialAmmo)
    {
      KeyValuePair<AmmoMode, List<GameObject>> pair = keyValuePair;
      pair.Value.ForEach(p => ammoDict[pair.Key].RegisterPotentialAmmo(p));
    }
  }

  public void KeyAdded() => SRSingleton<SceneContext>.Instance.PediaDirector.MaybeShowPopup(PediaDirector.Id.KEYS);

  public void RegionSetChanged(
    RegionRegistry.RegionSetId previous,
    RegionRegistry.RegionSetId current)
  {
  }

  public void TransformChanged(Vector3 pos, Quaternion rot)
  {
  }

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);

  public HashSet<Identifiable.Id> GetPotentialAmmo() => new HashSet<Identifiable.Id>(potentialAmmo.Select(go => Identifiable.GetId(go)), Identifiable.idComparer);

  private void Reset(PlayerModel model)
  {
    model.Reset(SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings());
    ammoDict = new Dictionary<AmmoMode, Ammo>(AmmoModeComparer.Instance)
    {
      {
        AmmoMode.DEFAULT,
        new Ammo(GetPotentialAmmo(), 5, 4, PLAYER_AMMO_PREDS, GetMaxAmmo_Default)
      },
      {
        AmmoMode.NIMBLE_VALLEY,
        new Ammo(new HashSet<Identifiable.Id>(Identifiable.idComparer)
        {
          Identifiable.Id.QUICKSILVER_PLORT,
          Identifiable.Id.VALLEY_AMMO_1,
          Identifiable.Id.VALLEY_AMMO_2,
          Identifiable.Id.VALLEY_AMMO_3,
          Identifiable.Id.VALLEY_AMMO_4
        }, 3, 3, new Predicate<Identifiable.Id>[3]
        {
          id => id == Identifiable.Id.QUICKSILVER_PLORT,
          id => id == Identifiable.Id.VALLEY_AMMO_1,
          id => id == Identifiable.Id.VALLEY_AMMO_2 || id == Identifiable.Id.VALLEY_AMMO_3 || id == Identifiable.Id.VALLEY_AMMO_4
        }, GetMaxAmmo_NimbleValley)
      }
    };
    SetAmmoMode(AmmoMode.DEFAULT);
    InitUpgradeLocks(model);
    model.upgrades.Clear();
    InitZoneMaps(model);
  }

  private int GetMaxAmmo_Default(Identifiable.Id id, int index)
  {
    switch (id)
    {
      case Identifiable.Id.GLITCH_SLIME:
      case Identifiable.Id.GLITCH_BUG_REPORT:
        return SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.slimeMaxAmmo;
      case Identifiable.Id.GLITCH_DEBUG_SPRAY_LIQUID:
        return SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.debugSprayMaxAmmo;
      default:
        return model.maxAmmo;
    }
  }

  private int GetMaxAmmo_NimbleValley(Identifiable.Id id, int index)
  {
    switch (index)
    {
      case 0:
        return 250;
      case 1:
        return 100;
      case 2:
        return 3;
      default:
        throw new ArgumentException();
    }
  }

  private void InitZoneMaps(PlayerModel model)
  {
    model.unlockedZoneMaps.Clear();
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.RANCH);
  }

  public void UnlockMap(ZoneDirector.Zone zone) => model.unlockedZoneMaps.Add(zone);

  public void LockAllMaps() => model.unlockedZoneMaps.Clear();

  public void UnlockAllMaps()
  {
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.MOSS);
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.DESERT);
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.QUARRY);
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.REEF);
    model.unlockedZoneMaps.Add(ZoneDirector.Zone.RUINS);
  }

  public bool HasUnlockedMap(ZoneDirector.Zone zone) => model.unlockedZoneMaps.Contains(zone);

  private void InitUpgradeLocks(PlayerModel model)
  {
    model.availUpgrades.Clear();
    model.availUpgrades.Add(Upgrade.HEALTH_1);
    model.availUpgrades.Add(Upgrade.ENERGY_1);
    model.availUpgrades.Add(Upgrade.AMMO_1);
    model.availUpgrades.Add(Upgrade.JETPACK);
    model.availUpgrades.Add(Upgrade.LIQUID_SLOT);
    model.upgradeLocks.Clear();
    model.upgradeLocks[Upgrade.RUN_EFFICIENCY] = CreateBasicLock(new Upgrade?(), null, 48f);
    model.upgradeLocks[Upgrade.AIR_BURST] = CreateBasicLock(new Upgrade?(), null, 72f);
    model.upgradeLocks[Upgrade.JETPACK_EFFICIENCY] = CreateBasicLock(new Upgrade?(Upgrade.JETPACK), null, 120f);
    model.upgradeLocks[Upgrade.HEALTH_2] = CreateBasicLock(new Upgrade?(Upgrade.HEALTH_1), null, 48f);
    model.upgradeLocks[Upgrade.HEALTH_3] = CreateBasicLock(new Upgrade?(Upgrade.HEALTH_2), null, 72f);
    model.upgradeLocks[Upgrade.ENERGY_2] = CreateBasicLock(new Upgrade?(Upgrade.ENERGY_1), null, 48f);
    model.upgradeLocks[Upgrade.ENERGY_3] = CreateBasicLock(new Upgrade?(Upgrade.ENERGY_2), null, 72f);
    model.upgradeLocks[Upgrade.AMMO_2] = CreateBasicLock(new Upgrade?(Upgrade.AMMO_1), null, 48f);
    model.upgradeLocks[Upgrade.AMMO_3] = CreateBasicLock(new Upgrade?(Upgrade.AMMO_2), null, 72f);
    model.upgradeLocks[Upgrade.TREASURE_CRACKER_1] = CreateBasicLock(new Upgrade?(), () => achieveDir.GetGameIntStat(AchievementsDirector.GameIntStat.FABRICATED_GADGETS) >= 1, 5f);
    model.upgradeLocks[Upgrade.TREASURE_CRACKER_2] = CreateBasicLock(new Upgrade?(Upgrade.TREASURE_CRACKER_1), () => achieveDir.GetGameIntStat(AchievementsDirector.GameIntStat.FABRICATED_GADGETS) >= 20, 1f);
    model.upgradeLocks[Upgrade.TREASURE_CRACKER_3] = CreateBasicLock(new Upgrade?(Upgrade.TREASURE_CRACKER_2), () => achieveDir.GetGameIntStat(AchievementsDirector.GameIntStat.FABRICATED_GADGETS) >= 50, 1f);
    model.upgradeLocks[Upgrade.SPARE_KEY] = CreateBasicLock(new Upgrade?(), () => mailDir.HasReadMail(new MailDirector.Mail(MailDirector.Type.PERSONAL, "casey_11")), 3f);
  }

  private void CheckAllUpgradeLockers()
  {
    foreach (KeyValuePair<Upgrade, UpgradeLocker> upgradeLock in model.upgradeLocks)
    {
      if (upgradeLock.Value.CheckUnlockCondition())
        upgradeLock.Value.Unlock();
    }
  }

  private UpgradeLocker CreateBasicLock(
    Upgrade? waitForUpgrade,
    UnlockCondition extraCondition,
    float delayHrs)
  {
    return new UpgradeLocker(this, () =>
    {
      if (waitForUpgrade.HasValue && !HasUpgrade(waitForUpgrade.Value))
        return false;
      return extraCondition == null || extraCondition();
    }, delayHrs);
  }

  public double? GetEndGameTimeRemaining()
  {
    if (!model.endGameTime.HasValue)
      return new double?();
    double num = model.endGameTime.Value - timeDir.WorldTime();
    return new double?(num > 0.0 ? num : 0.0);
  }

  public double? GetEndGameTime() => model.endGameTime;

  public void SetEndGameTime(double time)
  {
    model.endGameTime = new double?(time);
    onEndGameTimeChanged();
  }

  public bool IsGameOver() => model.endGameTime.HasValue && timeDir.HasReached(model.endGameTime.Value);

  public void Update()
  {
    model.Recover();
    List<Upgrade> upgradeList = new List<Upgrade>();
    foreach (KeyValuePair<Upgrade, UpgradeLocker> upgradeLock in model.upgradeLocks)
    {
      if (upgradeLock.Value.ReachedUnlockTime())
      {
        upgradeList.Add(upgradeLock.Key);
        if (!HasUpgrade(upgradeLock.Key) && !model.availUpgrades.Contains(upgradeLock.Key))
        {
          model.availUpgrades.Add(upgradeLock.Key);
          popupDir.QueueForPopup(new AvailUpgradePopupCreator(upgradeLock.Key));
          popupDir.MaybePopupNext();
        }
      }
    }
    foreach (Upgrade key in upgradeList)
      model.upgradeLocks.Remove(key);
    if (gameState != GameState.DEFAULT || !IsGameOver())
      return;
    gameState = GameState.GAME_OVER;
    onEndGame();
    Instantiate(SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().endGameUIPrefab);
  }

  public int GetCurrEnergy() => Mathf.FloorToInt(model.currEnergy);

  public int GetCurrHealth() => Mathf.FloorToInt(model.currHealth);

  public int GetMaxHealth() => model.maxHealth;

  public int GetMaxEnergy() => model.maxEnergy;

  public int GetCurrRad() => Mathf.CeilToInt(Mathf.Min(model.currRads, model.maxRads));

  public AmmoMode GetAmmoMode() => ammoMode;

  public IEnumerable<KeyValuePair<AmmoMode, Ammo>> GetAmmoDict() => ammoDict;

  public Ammo GetAmmo(AmmoMode mode) => ammoDict[mode];

  public void SetEnergy(int energy) => model.SetEnergy(energy);

  public void SetRad(int rad) => model.SetRad(rad);

  public void SetHealth(int health) => model.SetHealth(health);

  public void SetAmmoMode(AmmoMode mode)
  {
    if (ammoMode == mode)
      return;
    ammoMode = mode;
    if (onAmmoModeChanged == null)
      return;
    onAmmoModeChanged(mode);
  }

  public int AddRads(float rads) => model.AddRads(rads);

  public void RemoveRads(float rads)
  {
    model.currRads -= rads;
    model.radRecoverAfter = timeDir.WorldTime();
  }

  public bool CanBeDamaged() => !SRSingleton<SceneContext>.Instance.TimeDirector.IsFastForwarding() && SRInput.Instance.GetInputMode() == SRInput.InputMode.DEFAULT;

  public bool Damage(int healthLoss, GameObject source)
  {
    if (!CanBeDamaged())
      return false;
    model.LoseHealth(healthLoss);
    if (timeDir.HasReached(nextAmmoLossDamageTime))
      metadataDirector.Glitch.MaybeDamageExposure(source);
    if (model.currHealth > 0.0)
      return false;
    model.currHealth = 0.0f;
    model.healthBurstAfter = double.PositiveInfinity;
    return true;
  }

  public void Heal(int healthGain)
  {
    model.currHealth = Mathf.Clamp(model.currHealth + healthGain, 0.0f, model.maxHealth);
    model.healthBurstAfter = timeDir.WorldTime();
  }

  public void SpendEnergy(float energy)
  {
    model.SpendEnergy(energy);
    if (GetCurrEnergy() > 0)
      return;
    SECTR_AudioSystem.Play(onEnergyDepletedCue, transform.position, false);
  }

  public void AddCurrency(int adjust, CoinsType coinsType = CoinsType.NORM)
  {
    model.currency += adjust;
    model.currencyEverCollected += adjust;
    if (adjust > 0)
    {
      achieveDir.AddToStat(AchievementsDirector.IntStat.DAY_CURRENCY, adjust);
      achieveDir.AddToStat(AchievementsDirector.IntStat.CURRENCY, adjust);
    }
    SRSingleton<PopupElementsUI>.Instance.CreateCoinsPopup(adjust, coinsType);
  }

  public void AddCurrencyDisplayDelta(int adjust) => model.currencyDisplayDelta += adjust;

  public void SetCurrencyDisplay(int? currencyDisplay) => model.currencyDisplayOverride = currencyDisplay;

  public int GetDisplayedCurrency() => model.currencyDisplayOverride.HasValue ? model.currencyDisplayOverride.Value : model.currency + model.currencyDisplayDelta;

  public void SpendCurrency(int adjust, bool forcedLoss = false)
  {
    if (model.currency < adjust)
      throw new ArgumentException("Attempting to spend more currency than we have.");
    model.currency -= adjust;
    if (!forcedLoss)
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.GameIntStat.CURRENCY_SPENT, adjust);
    SRSingleton<PopupElementsUI>.Instance.CreateCoinsPopup(-adjust, CoinsType.NORM);
  }

  public int GetCurrency() => model.currency;

  public void AddKey() => model.AddKey();

  public bool SpendKey()
  {
    if (model.keys < 1)
      return false;
    --model.keys;
    return true;
  }

  public int GetKeys() => model.keys;

  public void OnGameIntStatChanged(AchievementsDirector.GameIntStat stat, int val)
  {
    if (stat != AchievementsDirector.GameIntStat.FABRICATED_GADGETS)
      return;
    CheckAllUpgradeLockers();
  }

  public void OnMailRead() => CheckAllUpgradeLockers();

  public void AddUpgrade(Upgrade upgrade, bool isFirstTime = false)
  {
    if (model.upgrades.Contains(upgrade))
      return;
    model.upgrades.Add(upgrade);
    model.ApplyUpgrade(upgrade, isFirstTime);
    CheckAllUpgradeLockers();
    if (upgrade != Upgrade.LIQUID_SLOT)
      return;
    SRSingleton<SceneContext>.Instance.TutorialDirector.OnLiquidSlotGained();
  }

  public bool HasOrCanGetUpgrade(Upgrade upgrade) => HasUpgrade(upgrade) || CanGetUpgrade(upgrade);

  public bool HasUpgrade(Upgrade upgrade) => model.upgrades.Contains(upgrade);

  public bool CanGetUpgrade(Upgrade upgrade) => !HasUpgrade(upgrade) && model.availUpgrades.Contains(upgrade);

  public void OnEnteredZone(ZoneDirector.Zone zone) => ammoDict[AmmoMode.NIMBLE_VALLEY].Clear(ii =>
  {
    switch (ammoDict[AmmoMode.NIMBLE_VALLEY].GetSlotName(ii))
    {
      case Identifiable.Id.VALLEY_AMMO_1:
      case Identifiable.Id.VALLEY_AMMO_2:
      case Identifiable.Id.VALLEY_AMMO_3:
      case Identifiable.Id.VALLEY_AMMO_4:
        return true;
      default:
        return false;
    }
  });

  public void OnExitedZone(ZoneDirector.Zone zone)
  {
  }

  public delegate bool UnlockCondition();

  public enum CoinsType
  {
    NONE = -1, // 0xFFFFFFFF
    NORM = 0,
    MOCHI = 1,
    DRONE = 2,
  }

  public class UpgradeLockData
  {
    public bool timedLock;
    public double lockedUntil;

    public UpgradeLockData(bool timedLock, double lockedUntil)
    {
      this.timedLock = timedLock;
      this.lockedUntil = lockedUntil;
    }

    public UpgradeLockData()
    {
    }
  }

  public class UpgradeLocker
  {
    private PlayerState playerState;
    private UnlockCondition unlockCondition;
    private float unlockDelayHrs;
    private bool timedLock;
    private double lockedUntil;

    public UpgradeLocker(
      PlayerState playerState,
      UnlockCondition unlockCondition,
      float unlockDelayHrs)
    {
      this.playerState = playerState;
      this.unlockCondition = unlockCondition;
      this.unlockDelayHrs = unlockDelayHrs;
    }

    public bool CheckUnlockCondition() => !timedLock && unlockCondition();

    public bool ReachedUnlockTime() => timedLock && playerState.timeDir.HasReached(lockedUntil);

    public void Unlock()
    {
      timedLock = true;
      lockedUntil = playerState.timeDir.HoursFromNow(unlockDelayHrs);
    }

    public void Push(UpgradeLockData data)
    {
      timedLock = data.timedLock;
      lockedUntil = data.lockedUntil;
    }

    public void Pull(out UpgradeLockData data) => data = new UpgradeLockData(timedLock, lockedUntil);
  }

  public delegate void OnEndGame();

  private enum GameState
  {
    DEFAULT,
    GAME_OVER,
  }

  public delegate void OnEndGameTimeChanged();

  public enum AmmoMode
  {
    DEFAULT,
    NIMBLE_VALLEY,
  }

  public class AmmoModeComparer : IEqualityComparer<AmmoMode>
  {
    public static AmmoModeComparer Instance = new AmmoModeComparer();

    public bool Equals(AmmoMode a, AmmoMode b) => a == b;

    public int GetHashCode(AmmoMode a) => (int) a;
  }

  public delegate void OnAmmoModeChanged(AmmoMode mode);

  public enum Upgrade
  {
    HEALTH_1 = 0,
    HEALTH_2 = 1,
    HEALTH_3 = 2,
    ENERGY_1 = 3,
    ENERGY_2 = 4,
    ENERGY_3 = 5,
    AMMO_1 = 6,
    AMMO_2 = 7,
    AMMO_3 = 8,
    JETPACK = 9,
    JETPACK_EFFICIENCY = 10, // 0x0000000A
    AIR_BURST = 11, // 0x0000000B
    RUN_EFFICIENCY = 12, // 0x0000000C
    LIQUID_SLOT = 13, // 0x0000000D
    AMMO_4 = 14, // 0x0000000E
    HEALTH_4 = 15, // 0x0000000F
    RUN_EFFICIENCY_2 = 16, // 0x00000010
    GOLDEN_SURESHOT = 17, // 0x00000011
    SPARE_KEY = 18, // 0x00000012
    TREASURE_CRACKER_1 = 100, // 0x00000064
    TREASURE_CRACKER_2 = 101, // 0x00000065
    TREASURE_CRACKER_3 = 102, // 0x00000066
    TREASURE_CRACKER_4 = 103, // 0x00000067
  }

  public class UpgradeComparer : IEqualityComparer<Upgrade>
  {
    public bool Equals(Upgrade x, Upgrade y) => x == y;

    public int GetHashCode(Upgrade obj) => (int) obj;
  }

  public enum GameMode
  {
    CLASSIC,
    TIME_LIMIT,
    CASUAL,
    TIME_LIMIT_V2,
  }

  public class GameModeComparer : IEqualityComparer<GameMode>
  {
    public static GameModeComparer Instance = new GameModeComparer();

    public bool Equals(GameMode a, GameMode b) => a == b;

    public int GetHashCode(GameMode a) => (int) a;
  }

  private class AvailUpgradePopupCreator : PopupDirector.PopupCreator
  {
    private Upgrade id;

    public AvailUpgradePopupCreator(Upgrade id) => this.id = id;

    public override void Create() => AvailUpgradePopupUI.CreateAvailUpgradePopup(id);

    public override bool Equals(object other) => other is AvailUpgradePopupCreator && ((AvailUpgradePopupCreator) other).id == id;

    public override int GetHashCode() => id.GetHashCode();

    public override bool ShouldClear() => false;
  }
}
