// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameAchieveV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameAchieveV02 : PersistedDataSet
  {
    public Dictionary<AchievementsDirector.GameFloatStat, float> gameFloatStatDict = new Dictionary<AchievementsDirector.GameFloatStat, float>();
    public Dictionary<AchievementsDirector.GameIntStat, int> gameIntStatDict = new Dictionary<AchievementsDirector.GameIntStat, int>();
    public Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>> gameIdDictStatDict = new Dictionary<AchievementsDirector.GameIdDictStat, Dictionary<Identifiable.Id, int>>();

    public override string Identifier => "SRGA";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      gameFloatStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameFloatStat) re.ReadInt32(), re => re.ReadSingle());
      gameIntStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameIntStat) re.ReadInt32(), re => re.ReadInt32());
      gameIdDictStatDict = LoadDictionary(reader, re => (AchievementsDirector.GameIdDictStat) re.ReadInt32(), re => LoadDictionary(re, r => (Identifiable.Id) r.ReadInt32(), r => r.ReadInt32()));
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteDictionary(writer, gameFloatStatDict, (w, key) => w.Write((int) key), (w, val) => w.Write(val));
      WriteDictionary(writer, gameIntStatDict, (w, key) => w.Write((int) key), (w, val) => w.Write(val));
      WriteDictionary(writer, gameIdDictStatDict, (w, key) => w.Write((int) key), (w, val) => WriteDictionary(w, val, (wr, key) => wr.Write((int) key), (wr, v) => wr.Write(v)));
    }

    public static GameAchieveV02 Load(BinaryReader reader)
    {
      GameAchieveV02 gameAchieveV02 = new GameAchieveV02();
      gameAchieveV02.Load(reader.BaseStream);
      return gameAchieveV02;
    }

    public static void AssertAreEqual(GameAchieveV02 expected, GameAchieveV02 actual)
    {
      TestUtil.AssertAreEqual(expected.gameFloatStatDict, actual.gameFloatStatDict, (e, a) => { }, "gameFloatStatDict");
      TestUtil.AssertAreEqual(expected.gameIntStatDict, actual.gameIntStatDict, (e, a) => { }, "gameIntStatDict");
      TestUtil.AssertAreEqual(expected.gameIdDictStatDict, actual.gameIdDictStatDict, (e, a) => TestUtil.AssertAreEqual(e, a, (e2, a2) => { }, e.ToString()), "gameIdDictStatDict");
    }
  }
}
