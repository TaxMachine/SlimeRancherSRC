// Decompiled with JetBrains decompiler
// Type: GameData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData : Persistable
{
  public string gameName;
  public int worldFormatID = 6;
  public int playerFormatID = 3;
  public int ranchFormatID = 3;
  public int actorsFormatID = 1;
  public int pediaFormatID = 1;
  public int achieveFormatID = 1;
  public WorldData world = new WorldData();
  public PlayerData player = new PlayerData();
  public RanchData ranch = new RanchData();
  public ActorsData actors = new ActorsData();
  public PediaData pedia = new PediaData();
  public GameAchieveData achieve = new GameAchieveData();
  private const string EXTENSION = ".sav";
  private const int CURR_FORMAT_ID = 3;

  public GameData() => gameName = "";

  public GameData(string gameName) => this.gameName = gameName != null && gameName.Length > 0 ? gameName : throw new ArgumentException();

  private static BinaryFormatter CreateFormatter()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    SurrogateSelector surrogateSelector = new SurrogateSelector();
    surrogateSelector.AddSurrogate(typeof (Vector3), new StreamingContext(StreamingContextStates.All), new Vector3Surrogate());
    formatter.SurrogateSelector = surrogateSelector;
    return formatter;
  }

  public void Load(Stream stream)
  {
    BinaryFormatter formatter = CreateFormatter();
    int num = (int) formatter.Deserialize(stream);
    if (num > 3)
    {
      Debug.Log("File format newer than current version type=GameData fileVer=" + num + " currVer=" + 3);
      throw new VersionMismatchException("File format newer than current version.");
    }
    if (num < 3)
    {
      Debug.Log("Unhandled version type=GameData fileVer=" + num + " currVer=" + 3);
      throw new VersionMismatchException("File format unhandled.");
    }
    world = DataModule<WorldData>.Deserialize(formatter, stream, 6);
    player = DataModule<PlayerData>.Deserialize(formatter, stream, 3);
    ranch = DataModule<RanchData>.Deserialize(formatter, stream, 3);
    actors = DataModule<ActorsData>.Deserialize(formatter, stream, 1);
    pedia = DataModule<PediaData>.Deserialize(formatter, stream, 1);
    achieve = DataModule<GameAchieveData>.Deserialize(formatter, stream, 1);
  }

  public long Write(Stream stream) => throw new NotImplementedException();

  public static void AssertEquals(GameData dataA, GameData dataB)
  {
    WorldData.AssertEquals(dataA.world, dataB.world);
    PlayerData.AssertEquals(dataA.player, dataB.player);
    ActorsData.AssertEquals(dataA.actors, dataB.actors);
    RanchData.AssertEquals(dataA.ranch, dataB.ranch);
    PediaData.AssertEquals(dataA.pedia, dataB.pedia);
    GameAchieveData.AssertEquals(dataA.achieve, dataB.achieve);
  }

  public class Summary
  {
    public string name;
    public string displayName;
    public Identifiable.Id iconId;
    public PlayerState.GameMode gameMode;
    public string version;
    public int day;
    public int currency;
    public int pediaCount;
    public bool isInvalid;
    public bool gameOver;
    public DateTimeOffset saveTimestamp;
    public int autosaveCount;
    public string saveName;
    public ulong saveNumber;

    public Summary(
      string name,
      string displayName,
      Identifiable.Id iconId,
      PlayerState.GameMode gameMode,
      string version,
      int day,
      int currency,
      int pediaCount,
      bool gameOver,
      DateTimeOffset saveTimestamp,
      string saveName,
      ulong saveNumber)
    {
      this.name = name;
      this.displayName = displayName;
      this.iconId = iconId;
      this.gameMode = gameMode;
      this.version = version;
      this.day = day;
      this.currency = currency;
      this.pediaCount = pediaCount;
      this.gameOver = gameOver;
      isInvalid = false;
      this.saveTimestamp = saveTimestamp;
      this.saveName = saveName;
      this.saveNumber = saveNumber;
    }

    public Summary(string name)
    {
      this.name = name;
      displayName = name;
      iconId = Identifiable.Id.BOOM_PLORT;
      gameMode = PlayerState.GameMode.CLASSIC;
      version = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get("l.unknown");
      day = 0;
      currency = 0;
      pediaCount = 0;
      gameOver = false;
      isInvalid = true;
      saveTimestamp = DateTimeOffset.MinValue;
      autosaveCount = 0;
      saveName = name;
      saveNumber = 0UL;
    }
  }
}
