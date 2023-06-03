// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.RequestedItemEntryV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class RequestedItemEntryV02 : PersistedDataSet
  {
    public Identifiable.Id id;
    public int count;
    public int progress;

    public override string Identifier => "SRRIE";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      id = (Identifiable.Id) reader.ReadInt32();
      count = reader.ReadInt32();
      progress = reader.ReadInt32();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) id);
      writer.Write(count);
      writer.Write(progress);
    }

    public static void AssertAreEqual(RequestedItemEntryV02 expected, RequestedItemEntryV02 actual)
    {
    }
  }
}
