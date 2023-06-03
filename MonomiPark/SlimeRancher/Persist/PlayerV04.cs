// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PlayerV04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PlayerV04 : PersistedDataSet
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
    public Dictionary<PlayerState.Upgrade, float> upgradeLocks;
    public Dictionary<ProgressDirector.ProgressType, int> progress;
    public Dictionary<ProgressDirector.ProgressType, List<float>> delayedProgress;

    public override string Identifier => "SRPL";

    public override uint Version => 4;

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
      upgradeLocks = LoadDictionary(reader, r => (PlayerState.Upgrade) r.ReadInt32(), r => r.ReadSingle());
      progress = LoadDictionary(reader, r => (ProgressDirector.ProgressType) r.ReadInt32(), r => r.ReadInt32());
      delayedProgress = LoadDictionary(reader, r => (ProgressDirector.ProgressType) r.ReadInt32(), r => LoadList(r, (Func<float, float>) (val => val)));
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
    }

    public static PlayerV04 Load(BinaryReader reader)
    {
      PlayerV04 playerV04 = new PlayerV04();
      playerV04.Load(reader.BaseStream);
      return playerV04;
    }

    public static void AssertAreEqual(PlayerV04 expected, PlayerV04 actual)
    {
      Vector3V02.AssertAreEqual(expected.playerPos, actual.playerPos);
      Vector3V02.AssertAreEqual(expected.playerRotEuler, actual.playerRotEuler);
      for (int index = 0; index < expected.ammo.Count; ++index)
        AmmoDataV02.AssertAreEqual(expected.ammo[index], actual.ammo[index]);
      for (int index = 0; index < expected.mail.Count; ++index)
        MailV02.AssertAreEqual(expected.mail[index], actual.mail[index]);
      int num = 0;
      while (num < expected.upgrades.Count)
        ++num;
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in expected.progress)
        ;
      foreach (KeyValuePair<PlayerState.Upgrade, float> upgradeLock in expected.upgradeLocks)
        ;
      foreach (KeyValuePair<ProgressDirector.ProgressType, List<float>> keyValuePair in expected.delayedProgress)
        TestUtil.AssertAreEqual(keyValuePair.Value, actual.delayedProgress[keyValuePair.Key], string.Format("Delayed Progress: {0}", keyValuePair.Key));
    }
  }
}
