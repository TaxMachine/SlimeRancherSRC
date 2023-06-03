// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GadgetPortableSlimeBaitV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GadgetPortableSlimeBaitV01 : PersistedDataSet
  {
    public Identifiable.Id id;

    public override string Identifier => "SRPSB";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => id = (Identifiable.Id) reader.ReadInt32();

    protected override void WriteData(BinaryWriter writer) => writer.Write((int) id);

    public static void AssertAreEqual(
      GadgetPortableSlimeBaitV01 expected,
      GadgetPortableSlimeBaitV01 actual)
    {
      TestUtil.AssertNullness(expected, actual);
    }
  }
}
