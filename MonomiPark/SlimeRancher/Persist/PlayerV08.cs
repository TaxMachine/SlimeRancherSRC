﻿// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerV08
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerV08 : VersionedPersistedDataSet<PlayerV07>
  {
    public static readonly Vector3 RESET_POS = new Vector3(89.29f, 14.98f, -144.46f);
    public static readonly Vector3 RESET_ROT = new Vector3(0.0f, 24.736f, 0.0f);
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
    public List<PlayerState.Upgrade> availUpgrades;
    public Dictionary<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLocks;
    public Dictionary<ProgressDirector.ProgressType, int> progress;
    public Dictionary<ProgressDirector.ProgressType, List<double>> delayedProgress;
    public List<Gadget.Id> blueprints;
    public List<Gadget.Id> availBlueprints;
    public Dictionary<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLocks;
    public Dictionary<Gadget.Id, int> gadgets;
    public Dictionary<Identifiable.Id, int> craftMatCounts;

    public override string Identifier => "SRPL";

    public override uint Version => 8;

    public PlayerV08()
    {
    }

    public PlayerV08(PlayerV07 legacyData) => UpgradeFrom(legacyData);

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
      availUpgrades = LoadList(reader, (Func<int, PlayerState.Upgrade>) (x => (PlayerState.Upgrade) x));
      upgradeLocks = LoadDictionary(reader, r => (PlayerState.Upgrade) r.ReadInt32(), r => new PlayerState.UpgradeLockData(r.ReadBoolean(), r.ReadDouble()));
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
      WriteList(writer, availUpgrades, item => (int) item);
      WriteDictionary(writer, upgradeLocks, (w, v) => w.Write((int) v), (w, v) =>
      {
        w.Write(v.timedLock);
        w.Write(v.lockedUntil);
      });
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

    public static PlayerV08 Load(BinaryReader reader)
    {
      PlayerV08 playerV08 = new PlayerV08();
      playerV08.Load(reader.BaseStream);
      return playerV08;
    }

    public static void AssertAreEqual(PlayerV08 expected, PlayerV08 actual)
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
      int num2 = 0;
      while (num2 < expected.availUpgrades.Count)
        ++num2;
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressType, List<double>> keyValuePair in expected.delayedProgress)
        TestUtil.AssertAreEqual(keyValuePair.Value, actual.delayedProgress[keyValuePair.Key], string.Format("Delayed Progress: {0}", keyValuePair.Key));
      int num3 = 0;
      while (num3 < expected.blueprints.Count)
        ++num3;
      int num4 = 0;
      while (num4 < expected.availBlueprints.Count)
        ++num4;
      foreach (KeyValuePair<Gadget.Id, GadgetDirector.BlueprintLockData> blueprintLock in expected.blueprintLocks)
        ;
      foreach (KeyValuePair<Gadget.Id, int> gadget in expected.gadgets)
        ;
      foreach (KeyValuePair<Identifiable.Id, int> craftMatCount in expected.craftMatCounts)
        ;
    }

    private static string UpgradesListStr(List<PlayerState.Upgrade> upgrades)
    {
      string str = "";
      foreach (PlayerState.Upgrade upgrade in upgrades)
        str = str + upgrade + ",";
      return str;
    }

    public static void AssertAreEqual(PlayerV07 expected, PlayerV08 actual)
    {
      Vector3V02 expected1 = new Vector3V02();
      expected1.value = RESET_POS;
      Vector3V02 expected2 = new Vector3V02();
      expected2.value = RESET_ROT;
      Vector3V02.AssertAreEqual(expected1, actual.playerPos);
      Vector3V02.AssertAreEqual(expected2, actual.playerRotEuler);
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num1 = 0;
      while (num1 < expected.upgrades.Count)
        ++num1;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      foreach (KeyValuePair<PlayerState.Upgrade, PlayerState.UpgradeLockData> upgradeLock in expected.upgradeLocks)
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

    protected override void UpgradeFrom(PlayerV07 legacyData)
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
      upgradeLocks = legacyData.upgradeLocks;
      progress = legacyData.progress;
      delayedProgress = legacyData.delayedProgress;
      blueprints = legacyData.blueprints;
      availBlueprints = legacyData.availBlueprints;
      blueprintLocks = legacyData.blueprintLocks;
      gadgets = legacyData.gadgets;
      craftMatCounts = legacyData.craftMatCounts;
      availUpgrades = legacyData.availUpgrades;
      playerPos = new Vector3V02();
      playerPos.value = RESET_POS;
      playerRotEuler = new Vector3V02();
      playerRotEuler.value = RESET_ROT;
    }
  }
}
