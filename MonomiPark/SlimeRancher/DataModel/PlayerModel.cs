// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.PlayerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class PlayerModel : ActorModel
  {
    public PlayerState.AmmoMode ammoMode;
    public Dictionary<PlayerState.AmmoMode, AmmoModel> ammoDict = new Dictionary<PlayerState.AmmoMode, AmmoModel>();
    public Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo;
    public List<PlayerState.Upgrade> upgrades = new List<PlayerState.Upgrade>();
    public List<PlayerState.Upgrade> availUpgrades = new List<PlayerState.Upgrade>();
    public Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLocker> upgradeLocks = new Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLocker>();
    public float currEnergy;
    public float currHealth;
    public float currRads;
    public int currency;
    public int currencyEverCollected;
    public int? currencyDisplayOverride;
    public int keys;
    public double radRecoverAfter = double.PositiveInfinity;
    public double energyRecoverAfter = double.PositiveInfinity;
    public double healthBurstAfter = double.PositiveInfinity;
    public float currHealthBurstRemaining;
    public HashSet<ZoneDirector.Zone> unlockedZoneMaps = new HashSet<ZoneDirector.Zone>(ZoneDirector.zoneComparer);
    public int maxHealth;
    public int maxEnergy;
    public int maxRads;
    public double? endGameTime;
    public Vector3 position;
    public Quaternion rotation;
    public int currencyDisplayDelta;
    public bool hasJetpack;
    public float jetpackEfficiency = 1f;
    public float runEfficiency = 1f;
    public bool hasAirBurst;
    public double runEnergyDepletionTime;
    private const int DEFAULT_MAX_HEALTH = 100;
    private const int DEFAULT_MAX_ENERGY = 100;
    private const int DEFAULT_MAX_RADS = 100;
    private const int RAD_UNIT_DAMAGE = 10;
    public static readonly int[] DEFAULT_MAX_AMMO = new int[5]
    {
      20,
      30,
      40,
      50,
      100
    };
    public int maxAmmo;
    private const float ENERGY_RECOVERED_PER_SECOND = 50f;
    private const float HEALTH_RECOVERED_PER_SECOND = 2f;
    private const float RADS_RECOVERED_PER_SECOND = 25f;
    private const float ENERGY_RECOVERY_DELAY = 5f;
    private const float FULL_RAD_RECOVERY_DELAY = 10f;
    private const float NONFULL_RAD_RECOVERY_DELAY = 5f;
    private const float HEALTH_BURST_AMOUNT = 10f;
    private const float HEALTH_BURST_RATE = 50f;
    private WorldModel worldModel;
    private List<Participant> participants = new List<Participant>();

    public PlayerModel()
      : base(1L, Identifiable.Id.PLAYER, RegionRegistry.RegionSetId.HOME, null)
    {
      ammoDict[PlayerState.AmmoMode.DEFAULT] = new AmmoModel();
      ammoDict[PlayerState.AmmoMode.NIMBLE_VALLEY] = new AmmoModel();
    }

    public override Vector3 GetPos() => position;

    public override Quaternion GetRot() => rotation;

    public void SetWorldModel(WorldModel worldModel) => this.worldModel = worldModel;

    public void AddParticipant(Participant participant) => participants.Add(participant);

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

    private void InitZoneMaps()
    {
      unlockedZoneMaps.Clear();
      unlockedZoneMaps.Add(ZoneDirector.Zone.RANCH);
    }

    public void SetTransform(Vector3 position, Vector3 rotation)
    {
      this.position = position;
      this.rotation = Quaternion.Euler(rotation);
      foreach (Participant participant in participants)
        participant.TransformChanged(this.position, this.rotation);
    }

    public void SetCurrRegionSet(RegionRegistry.RegionSetId regionSetId)
    {
      if (this.currRegionSetId == regionSetId)
        return;
      RegionRegistry.RegionSetId currRegionSetId = this.currRegionSetId;
      this.currRegionSetId = regionSetId;
      foreach (Participant participant in participants)
        participant.RegionSetChanged(currRegionSetId, this.currRegionSetId);
    }

    public void SetEnergy(float energy)
    {
      currEnergy = energy;
      if (currEnergy >= (double) maxEnergy)
        return;
      energyRecoverAfter = Math.Min(energyRecoverAfter, worldModel.worldTime + 300.0);
    }

    public void SpendEnergy(float energy)
    {
      currEnergy = Mathf.Max(0.0f, currEnergy - energy);
      energyRecoverAfter = worldModel.worldTime + 300.0;
    }

    public void LoseHealth(float health)
    {
      currHealth -= health;
      healthBurstAfter = worldModel.worldTime + 300.0;
    }

    public void SetRad(float rad)
    {
      currRads = rad;
      if (currRads <= 0.0)
        return;
      radRecoverAfter = Math.Min(radRecoverAfter, worldModel.worldTime + 60.0 * (currRads < (double) maxRads ? 5.0 : 10.0));
    }

    public int AddRads(float rads)
    {
      currRads += rads;
      radRecoverAfter = worldModel.worldTime + 60.0 * (currRads >= (double) maxRads ? 10.0 : 5.0);
      if (currRads > (double) maxRads)
      {
        int num1 = Mathf.FloorToInt((float) ((currRads - (double) maxRads) / 10.0));
        if (num1 > 0)
        {
          int num2 = 10 * num1;
          currRads -= num2;
          return num2;
        }
      }
      return 0;
    }

    public void SetCurrency(int currency) => this.currency = currency;

    public void SetCurrencyEverCollected(int currency) => currencyEverCollected = currency;

    public void SetKeys(int keys) => this.keys = keys;

    public void SetHealth(float health)
    {
      currHealth = health;
      if (currHealth >= (double) maxHealth)
        return;
      healthBurstAfter = Math.Min(healthBurstAfter, worldModel.worldTime + 300.0);
    }

    public void SetUpgrades(List<PlayerState.Upgrade> upgrades)
    {
      this.upgrades = upgrades;
      foreach (PlayerState.Upgrade upgrade in upgrades)
        ApplyUpgrade(upgrade, false);
    }

    public void SetAvailUpgrades(List<PlayerState.Upgrade> availUpgrades) => this.availUpgrades = new List<PlayerState.Upgrade>(availUpgrades);

    public void SetUpgradeLocks(
      Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLocks)
    {
      if (upgradeLocks == null)
        return;
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLock in upgradeLocks)
      {
        if (this.upgradeLocks.ContainsKey(upgradeLock.Key))
          this.upgradeLocks[upgradeLock.Key].Push(upgradeLock.Value);
        else
          Log.Debug("Skipping unknown upgrade lock key", "key", upgradeLock.Key);
      }
    }

    public void SetUnlockedZoneMaps(List<ZoneDirector.Zone> zones)
    {
      InitZoneMaps();
      foreach (ZoneDirector.Zone zone in zones)
        unlockedZoneMaps.Add(zone);
    }

    public void Recover()
    {
      MaybeRecoverHealth();
      if (worldModel.HasReachedTime(energyRecoverAfter))
        RecoverEnergy();
      if (!worldModel.HasReachedTime(radRecoverAfter))
        return;
      RecoverRads();
    }

    private void MaybeRecoverHealth()
    {
      if (currHealth < (double) maxHealth)
      {
        if (currHealthBurstRemaining <= 0.0 && Mathf.Ceil(currHealth) - (double) currHealth < 1.0 / 1000.0)
          currHealthBurstRemaining = Mathf.Ceil(currHealth) - currHealth;
        if (currHealthBurstRemaining > 0.0)
        {
          float num = Math.Min(currHealthBurstRemaining, Time.deltaTime * 50f);
          currHealthBurstRemaining = Math.Max(0.0f, currHealthBurstRemaining - num);
          currHealth = Math.Min(maxHealth, currHealth + num);
        }
        else
        {
          if (!worldModel.HasReachedTime(healthBurstAfter))
            return;
          healthBurstAfter += 300.0;
          currHealthBurstRemaining = 10f;
        }
      }
      else
      {
        healthBurstAfter = double.PositiveInfinity;
        currHealthBurstRemaining = 0.0f;
      }
    }

    public void OnNewGameLoaded(PlayerState.GameMode currGameMode)
    {
      if (currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
        return;
      SetUpgrades(((IEnumerable<PlayerState.Upgrade>) Enum.GetValues(typeof (PlayerState.Upgrade))).ToList());
      SetAvailUpgrades(new List<PlayerState.Upgrade>());
      SetHealth(maxHealth);
      SetEnergy(maxEnergy);
    }

    private void RecoverEnergy() => currEnergy = Math.Min(maxEnergy, currEnergy + Time.deltaTime * 50f);

    private void RecoverRads() => currRads = Math.Max(0.0f, currRads - Time.deltaTime * 25f);

    public void Reset(GameModeSettings modeSettings)
    {
      ResetForGameMode(modeSettings);
      maxEnergy = 100;
      maxHealth = 100;
      maxRads = 100;
      maxAmmo = DEFAULT_MAX_AMMO[0];
      currEnergy = maxEnergy;
      currHealth = maxHealth;
      currRads = 0.0f;
      upgrades.Clear();
      keys = 0;
    }

    public void ResetForGameMode(GameModeSettings modeSettings)
    {
      currency = modeSettings.initCurrency;
      currencyEverCollected = modeSettings.initCurrency;
      if (modeSettings.endAtNoonDay <= 0.0)
        return;
      endGameTime = modeSettings.EndTime();
    }

    public void RegisterPotentialAmmo(PlayerState.AmmoMode mode, GameObject prefab)
    {
      if (registeredPotentialAmmo == null)
        registeredPotentialAmmo = new Dictionary<PlayerState.AmmoMode, List<GameObject>>();
      if (!registeredPotentialAmmo.ContainsKey(mode))
        registeredPotentialAmmo[mode] = new List<GameObject>();
      registeredPotentialAmmo[mode].Add(prefab);
      foreach (Participant participant in participants)
        participant.RegisteredPotentialAmmoChanged(registeredPotentialAmmo);
    }

    public void AddKey()
    {
      ++keys;
      foreach (Participant participant in participants)
        participant.KeyAdded();
    }

    public void ApplyUpgrade(PlayerState.Upgrade upgrade, bool isFirstTime)
    {
      switch (upgrade)
      {
        case PlayerState.Upgrade.HEALTH_1:
          maxHealth = Math.Max(maxHealth, Mathf.RoundToInt(150f));
          if (currHealth >= (double) maxHealth)
            break;
          healthBurstAfter = Math.Min(healthBurstAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.HEALTH_2:
          maxHealth = Math.Max(maxHealth, Mathf.RoundToInt(200f));
          if (currHealth >= (double) maxHealth)
            break;
          healthBurstAfter = Math.Min(healthBurstAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.HEALTH_3:
          maxHealth = Math.Max(maxHealth, Mathf.RoundToInt(250f));
          if (currHealth >= (double) maxHealth)
            break;
          healthBurstAfter = Math.Min(healthBurstAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.ENERGY_1:
          maxEnergy = Math.Max(maxEnergy, Mathf.RoundToInt(150f));
          if (currEnergy >= (double) maxEnergy)
            break;
          energyRecoverAfter = Math.Min(energyRecoverAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.ENERGY_2:
          maxEnergy = Math.Max(maxEnergy, Mathf.RoundToInt(200f));
          if (currEnergy >= (double) maxEnergy)
            break;
          energyRecoverAfter = Math.Min(energyRecoverAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.ENERGY_3:
          maxEnergy = Math.Max(maxEnergy, Mathf.RoundToInt(250f));
          if (currEnergy >= (double) maxEnergy)
            break;
          energyRecoverAfter = Math.Min(energyRecoverAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.AMMO_1:
          maxAmmo = DEFAULT_MAX_AMMO[1];
          break;
        case PlayerState.Upgrade.AMMO_2:
          maxAmmo = DEFAULT_MAX_AMMO[2];
          break;
        case PlayerState.Upgrade.AMMO_3:
          maxAmmo = DEFAULT_MAX_AMMO[3];
          break;
        case PlayerState.Upgrade.JETPACK:
          hasJetpack = true;
          break;
        case PlayerState.Upgrade.JETPACK_EFFICIENCY:
          jetpackEfficiency = Math.Min(jetpackEfficiency, 0.8f);
          break;
        case PlayerState.Upgrade.AIR_BURST:
          hasAirBurst = true;
          break;
        case PlayerState.Upgrade.RUN_EFFICIENCY:
          runEfficiency = Math.Min(runEfficiency, 0.667f);
          break;
        case PlayerState.Upgrade.LIQUID_SLOT:
          ammoDict[PlayerState.AmmoMode.DEFAULT].IncreaseUsableSlots(5);
          break;
        case PlayerState.Upgrade.AMMO_4:
          maxAmmo = DEFAULT_MAX_AMMO[4];
          break;
        case PlayerState.Upgrade.HEALTH_4:
          maxHealth = Math.Max(maxHealth, Mathf.RoundToInt(350f));
          if (currHealth >= (double) maxHealth)
            break;
          healthBurstAfter = Math.Min(healthBurstAfter, worldModel.worldTime + 300.0);
          break;
        case PlayerState.Upgrade.RUN_EFFICIENCY_2:
          runEfficiency = Math.Min(runEfficiency, 0.5f);
          break;
        case PlayerState.Upgrade.GOLDEN_SURESHOT:
          break;
        case PlayerState.Upgrade.SPARE_KEY:
          if (!isFirstTime)
            break;
          AddKey();
          break;
        default:
          int num = (int) (upgrade - 100);
          break;
      }
    }

    public void Push(
      int health,
      int energy,
      int radiation,
      int currency,
      int currencyEverCollected,
      int keys,
      Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> ammoSlots,
      List<PlayerState.Upgrade> upgrades,
      List<PlayerState.Upgrade> availUpgrades,
      Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLocks,
      List<ZoneDirector.Zone> unlockedZoneMaps,
      RegionRegistry.RegionSetId regionSetId,
      Vector3 position,
      Vector3 rotation,
      double? endGameTime)
    {
      SetHealth(health);
      SetEnergy(energy);
      SetRad(radiation);
      SetCurrency(currency);
      SetCurrencyEverCollected(currencyEverCollected);
      SetKeys(keys);
      foreach (KeyValuePair<PlayerState.AmmoMode, Ammo.Slot[]> ammoSlot in ammoSlots)
        ammoDict[ammoSlot.Key].Push(ammoSlot.Value);
      SetUpgrades(upgrades);
      SetAvailUpgrades(availUpgrades);
      SetUpgradeLocks(upgradeLocks);
      SetUnlockedZoneMaps(unlockedZoneMaps);
      this.endGameTime = endGameTime;
      currRegionSetId = regionSetId;
      SetTransform(position, rotation);
    }

    public void Pull(
      out int health,
      out int energy,
      out int radiation,
      out int currency,
      out int currencyEverCollected,
      out int keys,
      out Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> ammoSlots,
      out List<PlayerState.Upgrade> upgrades,
      out List<PlayerState.Upgrade> availUpgrades,
      out Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLocks,
      out List<ZoneDirector.Zone> unlockedZoneMaps,
      out RegionRegistry.RegionSetId regionSetId,
      out Vector3 position,
      out Vector3 rotation,
      out double? endGameTime)
    {
      health = Mathf.FloorToInt(currHealth);
      energy = Mathf.FloorToInt(currEnergy);
      radiation = Mathf.CeilToInt(Mathf.Min(currRads, maxRads));
      currency = this.currency;
      currencyEverCollected = this.currencyEverCollected;
      keys = this.keys;
      ammoSlots = new Dictionary<PlayerState.AmmoMode, Ammo.Slot[]>();
      foreach (KeyValuePair<PlayerState.AmmoMode, AmmoModel> keyValuePair in ammoDict)
      {
        Ammo.Slot[] slots;
        keyValuePair.Value.Pull(out slots);
        ammoSlots[keyValuePair.Key] = slots;
      }
      upgrades = this.upgrades;
      availUpgrades = this.availUpgrades;
      upgradeLocks = new Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData>();
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLocker> upgradeLock in this.upgradeLocks)
      {
        PlayerState.UpgradeLockData data = new PlayerState.UpgradeLockData();
        upgradeLock.Value.Pull(out data);
        upgradeLocks[upgradeLock.Key] = data;
      }
      endGameTime = this.endGameTime;
      unlockedZoneMaps = new List<ZoneDirector.Zone>(this.unlockedZoneMaps);
      regionSetId = currRegionSetId;
      position = this.position;
      rotation = this.rotation.eulerAngles;
    }

    public new interface Participant
    {
      void InitModel(PlayerModel model);

      void SetModel(PlayerModel model);

      void RegionSetChanged(RegionRegistry.RegionSetId previous, RegionRegistry.RegionSetId current);

      void TransformChanged(Vector3 pos, Quaternion rot);

      void RegisteredPotentialAmmoChanged(
        Dictionary<PlayerState.AmmoMode, List<GameObject>> registeredPotentialAmmo);

      void KeyAdded();
    }
  }
}
