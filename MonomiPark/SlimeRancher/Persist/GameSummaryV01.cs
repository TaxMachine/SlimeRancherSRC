// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GameSummaryV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GameSummaryV01 : PersistedDataSet
  {
    public Identifiable.Id iconId;
    public PlayerState.GameMode gameMode;
    public string version;
    public int currency;
    public int pediaCount;
    public double worldTime;

    public override string Identifier => "SRGSUMM";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      version = reader.ReadString();
      gameMode = (PlayerState.GameMode) reader.ReadInt32();
      iconId = (Identifiable.Id) reader.ReadInt32();
      currency = reader.ReadInt32();
      pediaCount = reader.ReadInt32();
      worldTime = reader.ReadDouble();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(version);
      writer.Write((int) gameMode);
      writer.Write((int) iconId);
      writer.Write(currency);
      writer.Write(pediaCount);
      writer.Write(worldTime);
    }

    public static GameSummaryV01 Load(BinaryReader reader)
    {
      GameSummaryV01 gameSummaryV01 = new GameSummaryV01();
      gameSummaryV01.Load(reader.BaseStream);
      return gameSummaryV01;
    }

    public static void AssertAreEqual(GameSummaryV01 expected, GameSummaryV01 actual)
    {
    }
  }
}
