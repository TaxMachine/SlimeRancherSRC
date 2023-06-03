// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerV13
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerV13 : VersionedPersistedDataSet<PlayerV12>
  {
    public int health;
    public int energy;
    public int radiation;
    public int currency;
    public int keys;
    public int currencyEverCollected;
    public string version = "0.3.0";
    public PlayerState.GameMode gameMode;
    public Identifiable.Id gameIconId = Identifiable.Id.CARROT_VEGGIE;
    public Vector3V02 playerPos;
    public Vector3V02 playerRotEuler;
    public List<PlayerState.Upgrade> upgrades;
    public Dictionary<PlayerState.AmmoMode, List<AmmoDataV02>> ammo;
    public List<MailV02> mail;
    public List<PlayerState.Upgrade> availUpgrades;
    public Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLocks;
    public Dictionary<ProgressDirector.ProgressType, int> progress;
    public Dictionary<ProgressDirector.ProgressTrackerId, double> delayedProgress;
    public List<Gadget.Id> blueprints;
    public List<Gadget.Id> availBlueprints;
    public Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLocks;
    public Dictionary<Gadget.Id, int> gadgets;
    public Dictionary<Identifiable.Id, int> craftMatCounts;
    public RegionRegistry.RegionSetId regionSetId;
    public List<ZoneDirector.Zone> unlockedZoneMaps;
    public double? endGameTime;

    public override string Identifier => "SRPL";

    public override uint Version => 13;

    public PlayerV13()
    {
    }

    public PlayerV13(PlayerV12 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      health = reader.ReadInt32();
      energy = reader.ReadInt32();
      radiation = reader.ReadInt32();
      currency = reader.ReadInt32();
      keys = reader.ReadInt32();
      currencyEverCollected = reader.ReadInt32();
      version = reader.ReadString();
      gameMode = (PlayerState.GameMode) reader.ReadInt32();
      gameIconId = (Identifiable.Id) reader.ReadInt32();
      playerPos = new Vector3V02();
      playerPos.Load(reader.BaseStream);
      playerRotEuler = new Vector3V02();
      playerRotEuler.Load(reader.BaseStream);
      upgrades = LoadList(reader, (Func<int, PlayerState.Upgrade>) (x => (PlayerState.Upgrade) x));
      ammo = LoadDictionary(reader, r => (PlayerState.AmmoMode) r.ReadInt32(), r => LoadList<AmmoDataV02>(r));
      mail = LoadList<MailV02>(reader);
      availUpgrades = LoadList(reader, (Func<int, PlayerState.Upgrade>) (x => (PlayerState.Upgrade) x));
      upgradeLocks = LoadDictionary(reader, r => (PlayerState.Upgrade) r.ReadInt32(), r => new PlayerState.UpgradeLockData(r.ReadBoolean(), r.ReadDouble()));
      progress = LoadDictionary(reader, r => (ProgressDirector.ProgressType) r.ReadInt32(), r => r.ReadInt32());
      delayedProgress = LoadDictionary(reader, r => (ProgressDirector.ProgressTrackerId) r.ReadInt32(), r => r.ReadDouble());
      blueprints = LoadList(reader, (Func<int, Gadget.Id>) (x => (Gadget.Id) x));
      availBlueprints = LoadList(reader, (Func<int, Gadget.Id>) (x => (Gadget.Id) x));
      blueprintLocks = LoadDictionary(reader, r => (Gadget.Id) r.ReadInt32(), r => new GadgetDirector.BlueprintLockData(r.ReadBoolean(), r.ReadDouble()));
      gadgets = LoadDictionary(reader, r => (Gadget.Id) r.ReadInt32(), r => r.ReadInt32());
      craftMatCounts = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadInt32());
      regionSetId = (RegionRegistry.RegionSetId) reader.ReadInt32();
      unlockedZoneMaps = LoadList(reader, (Func<int, ZoneDirector.Zone>) (x => (ZoneDirector.Zone) x));
      LoadNullable(reader, out endGameTime);
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(health);
      writer.Write(energy);
      writer.Write(radiation);
      writer.Write(currency);
      writer.Write(keys);
      writer.Write(currencyEverCollected);
      writer.Write(version);
      writer.Write((int) gameMode);
      writer.Write((int) gameIconId);
      playerPos.Write(writer.BaseStream);
      playerRotEuler.Write(writer.BaseStream);
      WriteList(writer, upgrades, item => (int) item);
      WriteDictionary(writer, ammo, (w, v) => w.Write((int) v), (w, v) => WriteList(w, v));
      WriteList(writer, mail);
      WriteList(writer, availUpgrades, item => (int) item);
      WriteDictionary(writer, upgradeLocks, (w, v) => w.Write((int) v), (w, v) =>
      {
        w.Write(v.timedLock);
        w.Write(v.lockedUntil);
      });
      WriteDictionary(writer, progress, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, delayedProgress, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteList(writer, blueprints, item => (int) item);
      WriteList(writer, availBlueprints, item => (int) item);
      WriteDictionary(writer, blueprintLocks, (w, v) => w.Write((int) v), (w, v) =>
      {
        w.Write(v.timedLock);
        w.Write(v.lockedUntil);
      });
      WriteDictionary(writer, gadgets, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, craftMatCounts, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      writer.Write((int) regionSetId);
      WriteList(writer, unlockedZoneMaps, item => (int) item);
      WriteNullable(writer, endGameTime);
    }

    public static PlayerV13 Load(BinaryReader reader)
    {
      PlayerV13 playerV13 = new PlayerV13();
      playerV13.Load(reader.BaseStream);
      return playerV13;
    }

    public static void AssertAreEqual(PlayerV13 expected, PlayerV13 actual)
    {
      Vector3V02.AssertAreEqual(expected.playerPos, actual.playerPos);
      Vector3V02.AssertAreApproximatelyEqual(expected.playerRotEuler, actual.playerRotEuler, 0.1f);
      TestUtil.AssertAreEqual(expected.ammo, actual.ammo, (a, b) => AmmoDataV02.AssertAreEqual(a, b), "ammo");
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num1 = 0;
      while (num1 < expected.upgrades.Count)
        ++num1;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      int num2 = 0;
      while (num2 < expected.availUpgrades.Count)
        ++num2;
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressTrackerId, double> keyValuePair in expected.delayedProgress)
        ;
      int num3 = 0;
      while (num3 < expected.blueprints.Count)
        ++num3;
      AssertAreEqual(expected.availBlueprints, actual.availBlueprints);
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in expected.blueprintLocks)
        ;
      foreach (KeyValuePair<Gadget.Id, int> gadget in expected.gadgets)
        ;
      foreach (KeyValuePair<Identifiable.Id, int> craftMatCount in expected.craftMatCounts)
        ;
      TestUtil.AssertAreEqual(expected.unlockedZoneMaps, actual.unlockedZoneMaps, "unlockedZoneMaps");
    }

    private static string UpgradesListStr(List<PlayerState.Upgrade> upgrades)
    {
      string str = "";
      foreach (PlayerState.Upgrade upgrade in upgrades)
        str = str + upgrade + ",";
      return str;
    }

    public static void AssertAreEqual(PlayerV12 expected, PlayerV13 actual)
    {
      Vector3V02.AssertAreEqual(expected.playerPos, actual.playerPos);
      Vector3V02.AssertAreEqual(expected.playerRotEuler, actual.playerRotEuler);
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num1 = 0;
      while (num1 < expected.upgrades.Count)
        ++num1;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressTrackerId, double> keyValuePair in expected.delayedProgress)
        ;
      int num2 = 0;
      while (num2 < expected.blueprints.Count)
        ++num2;
      AssertAreEqual(expected.availBlueprints, actual.availBlueprints);
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in expected.blueprintLocks)
        ;
      foreach (KeyValuePair<Gadget.Id, int> gadget in expected.gadgets)
        ;
      foreach (KeyValuePair<Identifiable.Id, int> craftMatCount in expected.craftMatCounts)
        ;
      TestUtil.AssertAreEqual(expected.unlockedZoneMaps, actual.unlockedZoneMaps, "unlockedZoneMaps");
      foreach (KeyValuePair<PlayerState.AmmoMode, List<AmmoDataV02>> keyValuePair in expected.ammo)
        AmmoDataV02.AssertAreEqual(expected.ammo[keyValuePair.Key], actual.ammo[keyValuePair.Key]);
    }

    protected override void UpgradeFrom(PlayerV12 legacyData)
    {
      health = legacyData.health;
      energy = legacyData.energy;
      radiation = legacyData.radiation;
      currency = legacyData.currency;
      keys = legacyData.keys;
      currencyEverCollected = legacyData.currencyEverCollected;
      version = legacyData.version;
      gameMode = legacyData.gameMode;
      gameIconId = legacyData.gameIconId;
      playerPos = legacyData.playerPos;
      playerRotEuler = legacyData.playerRotEuler;
      upgrades = legacyData.upgrades;
      mail = legacyData.mail;
      upgradeLocks = legacyData.upgradeLocks;
      progress = legacyData.progress;
      delayedProgress = legacyData.delayedProgress;
      blueprints = legacyData.blueprints;
      availBlueprints = legacyData.availBlueprints;
      blueprintLocks = legacyData.blueprintLocks;
      gadgets = legacyData.gadgets;
      craftMatCounts = legacyData.craftMatCounts;
      availUpgrades = legacyData.availUpgrades;
      regionSetId = legacyData.regionSetId;
      unlockedZoneMaps = legacyData.unlockedZoneMaps;
      ammo = legacyData.ammo;
      endGameTime = new double?();
    }

    private static void AssertAreEqual(List<Gadget.Id> expected, List<Gadget.Id> actual)
    {
      expected = expected.Except(GadgetsModel.AVAILABLE_BLUEPRINTS).ToList();
      actual = actual.Except(GadgetsModel.AVAILABLE_BLUEPRINTS).ToList();
      int num = 0;
      while (num < expected.Count)
        ++num;
    }
  }
}
