// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerV06
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerV06 : VersionedPersistedDataSet<PlayerV05>
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
    public List<AmmoDataV02> ammo;
    public List<MailV02> mail;
    public Dictionary<PlayerState.Upgrade, double> upgradeLocks;
    public Dictionary<ProgressDirector.ProgressType, int> progress;
    public Dictionary<ProgressDirector.ProgressType, List<double>> delayedProgress;
    public List<Gadget.Id> blueprints;
    public List<Gadget.Id> availBlueprints;
    public Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLocks;
    public Dictionary<Gadget.Id, int> gadgets;
    public Dictionary<Identifiable.Id, int> craftMatCounts;

    public override string Identifier => "SRPL";

    public override uint Version => 6;

    public PlayerV06()
    {
    }

    public PlayerV06(PlayerV05 legacyData) => UpgradeFrom(legacyData);

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
      ammo = LoadList<AmmoDataV02>(reader);
      mail = LoadList<MailV02>(reader);
      upgradeLocks = LoadDictionary(reader, r => (PlayerState.Upgrade) r.ReadInt32(), r => r.ReadDouble());
      progress = LoadDictionary(reader, r => (ProgressDirector.ProgressType) r.ReadInt32(), r => r.ReadInt32());
      delayedProgress = LoadDictionary(reader, r => (ProgressDirector.ProgressType) r.ReadInt32(), r => LoadList(r, (Func<double, double>) (val => val)));
      blueprints = LoadList(reader, (Func<int, Gadget.Id>) (x => (Gadget.Id) x));
      availBlueprints = LoadList(reader, (Func<int, Gadget.Id>) (x => (Gadget.Id) x));
      blueprintLocks = LoadDictionary(reader, r => (Gadget.Id) r.ReadInt32(), r => new GadgetDirector.BlueprintLockData(r.ReadBoolean(), r.ReadDouble()));
      gadgets = LoadDictionary(reader, r => (Gadget.Id) r.ReadInt32(), r => r.ReadInt32());
      craftMatCounts = LoadDictionary(reader, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadInt32());
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
      WriteList(writer, ammo);
      WriteList(writer, mail);
      WriteDictionary(writer, upgradeLocks, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, progress, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, delayedProgress, (w, v) => w.Write((int) v), (w, v) => WriteList(w, v, item => item));
      WriteList(writer, blueprints, item => (int) item);
      WriteList(writer, availBlueprints, item => (int) item);
      WriteDictionary(writer, blueprintLocks, (w, v) => w.Write((int) v), (w, v) =>
      {
        w.Write(v.timedLock);
        w.Write(v.lockedUntil);
      });
      WriteDictionary(writer, gadgets, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
      WriteDictionary(writer, craftMatCounts, (w, v) => w.Write((int) v), (w, v) => w.Write(v));
    }

    public static PlayerV06 Load(BinaryReader reader)
    {
      PlayerV06 playerV06 = new PlayerV06();
      playerV06.Load(reader.BaseStream);
      return playerV06;
    }

    public static void AssertAreEqual(PlayerV06 expected, PlayerV06 actual)
    {
      Vector3V02.AssertAreEqual(expected.playerPos, actual.playerPos);
      Vector3V02.AssertAreEqual(expected.playerRotEuler, actual.playerRotEuler);
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num1 = 0;
      while (num1 < expected.upgrades.Count)
        ++num1;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      foreach (KeyValuePair<PlayerState.Upgrade, double> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressType, List<double>> keyValuePair in expected.delayedProgress)
        TestUtil.AssertAreEqual(keyValuePair.Value, actual.delayedProgress[keyValuePair.Key], string.Format("Delayed Progress: {0}", keyValuePair.Key));
      int num2 = 0;
      while (num2 < expected.blueprints.Count)
        ++num2;
      int num3 = 0;
      while (num3 < expected.availBlueprints.Count)
        ++num3;
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in expected.blueprintLocks)
        ;
      foreach (KeyValuePair<Gadget.Id, int> gadget in expected.gadgets)
        ;
      foreach (KeyValuePair<Identifiable.Id, int> craftMatCount in expected.craftMatCounts)
        ;
    }

    public static void AssertAreEqual(PlayerV05 expected, PlayerV06 actual)
    {
      Vector3V02.AssertAreEqual(expected.playerPos, actual.playerPos);
      Vector3V02.AssertAreEqual(expected.playerRotEuler, actual.playerRotEuler);
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num1 = 0;
      while (num1 < expected.upgrades.Count)
        ++num1;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      foreach (KeyValuePair<PlayerState.Upgrade, float> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressType, List<float>> keyValuePair in expected.delayedProgress)
        TestUtil.AssertAreEqual(UpgradeFrom(keyValuePair.Value), actual.delayedProgress[keyValuePair.Key], string.Format("Delayed Progress: {0}", keyValuePair.Key));
      int num2 = 0;
      while (num2 < expected.blueprints.Count)
        ++num2;
      int num3 = 0;
      while (num3 < expected.availBlueprints.Count)
        ++num3;
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in expected.blueprintLocks)
        ;
      foreach (KeyValuePair<Gadget.Id, int> gadget in expected.gadgets)
        ;
      foreach (KeyValuePair<Identifiable.Id, int> craftMatCount in expected.craftMatCounts)
        ;
    }

    protected override void UpgradeFrom(PlayerV05 legacyData)
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
      ammo = legacyData.ammo;
      mail = legacyData.mail;
      upgradeLocks = UpgradeFrom(legacyData.upgradeLocks);
      progress = legacyData.progress;
      delayedProgress = UpgradeFrom(legacyData.delayedProgress);
      blueprints = legacyData.blueprints;
      availBlueprints = legacyData.availBlueprints;
      blueprintLocks = legacyData.blueprintLocks;
      gadgets = legacyData.gadgets;
      craftMatCounts = legacyData.craftMatCounts;
    }

    private static List<double> UpgradeFrom(List<float> legacyList)
    {
      List<double> doubleList = new List<double>();
      foreach (float legacy in legacyList)
        doubleList.Add(legacy);
      return doubleList;
    }

    public static Dictionary<PlayerState.Upgrade, double> UpgradeFrom(
      Dictionary<PlayerState.Upgrade, float> legacyDict)
    {
      Dictionary<PlayerState.Upgrade, double> dictionary = new Dictionary<PlayerState.Upgrade, double>();
      foreach (KeyValuePair<PlayerState.Upgrade, float> keyValuePair in legacyDict)
        dictionary[keyValuePair.Key] = keyValuePair.Value;
      return dictionary;
    }

    public static Dictionary<ProgressDirector.ProgressType, List<double>> UpgradeFrom(
      Dictionary<ProgressDirector.ProgressType, List<float>> legacyDict)
    {
      Dictionary<ProgressDirector.ProgressType, List<double>> dictionary = new Dictionary<ProgressDirector.ProgressType, List<double>>();
      foreach (KeyValuePair<ProgressDirector.ProgressType, List<float>> keyValuePair in legacyDict)
        dictionary[keyValuePair.Key] = UpgradeFrom(keyValuePair.Value);
      return dictionary;
    }
  }
}
