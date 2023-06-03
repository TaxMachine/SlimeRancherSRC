// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.EchoNoteGordoV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class EchoNoteGordoV01 : PersistedDataSet
  {
    public EchoNoteGordoModel.State state;

    public override string Identifier => "SRENG";

    public override uint Version => 1;

    public static EchoNoteGordoV01 Load(BinaryReader reader)
    {
      EchoNoteGordoV01 echoNoteGordoV01 = new EchoNoteGordoV01();
      echoNoteGordoV01.Load(reader.BaseStream);
      return echoNoteGordoV01;
    }

    protected override void LoadData(BinaryReader reader) => state = (EchoNoteGordoModel.State) reader.ReadInt32();

    protected override void WriteData(BinaryWriter writer) => writer.Write((int) state);

    public static void AssertAreEqual(EchoNoteGordoV01 expected, EchoNoteGordoV01 actual)
    {
    }
  }
}
