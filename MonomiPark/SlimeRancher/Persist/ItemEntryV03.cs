// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ItemEntryV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ItemEntryV03 : VersionedPersistedDataSet<ItemEntryV02>
  {
    public Identifiable.Id id;
    public ExchangeDirector.NonIdentReward nonIdentReward;
    public int count;

    public override string Identifier => "SRIE";

    public override uint Version => 3;

    public ItemEntryV03()
    {
    }

    public ItemEntryV03(ItemEntryV02 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      id = (Identifiable.Id) reader.ReadInt32();
      nonIdentReward = (ExchangeDirector.NonIdentReward) reader.ReadInt32();
      count = reader.ReadInt32();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) id);
      writer.Write((int) nonIdentReward);
      writer.Write(count);
    }

    public static void AssertAreEqual(ItemEntryV03 expected, ItemEntryV03 actual)
    {
    }

    public static void AssertAreEqual(ItemEntryV02 expected, ItemEntryV03 actual)
    {
    }

    protected override void UpgradeFrom(ItemEntryV02 legacyData)
    {
      id = legacyData.id;
      count = legacyData.count;
      nonIdentReward = ExchangeDirector.NonIdentReward.NONE;
    }
  }
}
