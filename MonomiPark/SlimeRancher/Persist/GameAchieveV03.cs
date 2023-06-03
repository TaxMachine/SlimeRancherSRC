// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameAchieveV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameAchieveV03 : VersionedPersistedDataSet<GameAchieveV02>
  {
    public Dictionary<AchievementsDirector.GameFloatStat, float> gameFloatStatDict = new Dictionary<AchievementsDirector.GameFloatStat, float>();
    public Dictionary<AchievementsDirector.GameDoubleStat, double> gameDoubleStatDict = new Dictionary<AchievementsDirector.GameDoubleStat, double>();
    public Dictionary<AchievementsDirector.GameIntStat, int> gameIntStatDict = new Dictionary<AchievementsDirector.GameIntStat, int>();
    public Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>> gameIdDictStatDict = new Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>>();

    public override string Identifier => "SRGA";

    public override uint Version => 3;

    protected override void LoadData(BinaryReader reader)
    {
      gameFloatStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameFloatStat) re.ReadInt32(), re => re.ReadSingle());
      gameDoubleStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameDoubleStat) re.ReadInt32(), re => re.ReadDouble());
      gameIntStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameIntStat) re.ReadInt32(), re => re.ReadInt32());
      gameIdDictStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameIdDictStat) re.ReadInt32(), re => LoadDictionary(re, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadInt32()));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteDictionary(writer, gameFloatStatDict, (w, key) => w.Write((int) key), (w, val) => w.Write(val));
      WriteDictionary(writer, gameDoubleStatDict, (w, key) => w.Write((int) key), (w, val) => w.Write(val));
      WriteDictionary(writer, gameIntStatDict, (w, key) => w.Write((int) key), (w, val) => w.Write(val));
      WriteDictionary(writer, gameIdDictStatDict, (w, key) => w.Write((int) key), (w, val) => WriteDictionary(w, val, (wr, key) => wr.Write((int) key), (wr, v) => wr.Write(v)));
    }

    public static GameAchieveV03 Load(BinaryReader reader)
    {
      GameAchieveV03 gameAchieveV03 = new GameAchieveV03();
      gameAchieveV03.Load(reader.BaseStream);
      return gameAchieveV03;
    }

    protected override void UpgradeFrom(GameAchieveV02 legacyData)
    {
      gameFloatStatDict = legacyData.gameFloatStatDict;
      gameIntStatDict = legacyData.gameIntStatDict;
      gameIdDictStatDict = legacyData.gameIdDictStatDict;
      gameDoubleStatDict = new Dictionary<AchievementsDirector.GameDoubleStat, double>();
      gameDoubleStatDict[AchievementsDirector.GameDoubleStat.LAST_LEFT_RANCH] = gameFloatStatDict.Get((AchievementsDirector.GameFloatStat) 0);
      gameDoubleStatDict[AchievementsDirector.GameDoubleStat.LAST_ENTERED_RANCH] = gameFloatStatDict.Get((AchievementsDirector.GameFloatStat) 1);
      gameDoubleStatDict[AchievementsDirector.GameDoubleStat.LAST_SLEPT] = gameFloatStatDict.Get((AchievementsDirector.GameFloatStat) 2);
      gameDoubleStatDict[AchievementsDirector.GameDoubleStat.LAST_AWOKE] = gameFloatStatDict.Get((AchievementsDirector.GameFloatStat) 3);
    }

    public static void AssertAreEqual(GameAchieveV03 expected, GameAchieveV03 actual)
    {
      TestUtil.AssertAreEqual(expected.gameFloatStatDict, actual.gameFloatStatDict, (e, a) => { }, "gameFloatStatDict");
      TestUtil.AssertAreEqual(expected.gameDoubleStatDict, actual.gameDoubleStatDict, (e, a) => { }, "gameDoubleStatDict");
      TestUtil.AssertAreEqual(expected.gameIntStatDict, actual.gameIntStatDict, (e, a) => { }, "gameIntStatDict");
      TestUtil.AssertAreEqual(expected.gameIdDictStatDict, actual.gameIdDictStatDict, (e, a) => TestUtil.AssertAreEqual(e, a, (e2, a2) => { }, e.ToString()), "gameIdDictStatDict");
    }

    public static void AssertAreEqual(GameAchieveV02 expected, GameAchieveV03 actual)
    {
      TestUtil.AssertAreEqual(expected.gameFloatStatDict, actual.gameFloatStatDict, (e, a) => { }, "gameFloatStatDict");
      TestUtil.AssertAreEqual(expected.gameIntStatDict, actual.gameIntStatDict, (e, a) => { }, "gameIntStatDict");
      TestUtil.AssertAreEqual(expected.gameIdDictStatDict, actual.gameIdDictStatDict, (e, a) => TestUtil.AssertAreEqual(e, a, (e2, a2) => { }, e.ToString()), "gameIdDictStatDict");
    }
  }
}
