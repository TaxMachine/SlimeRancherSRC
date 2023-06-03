// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GlitchStorageV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GlitchStorageV01 : PersistedDataSet
  {
    public Identifiable.Id id;
    public int count;

    public override string Identifier => "SRGLITCH_ST";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      id = (Identifiable.Id) reader.ReadInt32();
      count = reader.ReadInt32();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) id);
      writer.Write(count);
    }

    public static void AssertAreEqual(GlitchStorageV01 expected, GlitchStorageV01 actual)
    {
    }
  }
}
