// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ResourceWaterV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ResourceWaterV03 : VersionedPersistedDataSet<ResourceWaterV02>
  {
    public double spawn;
    public float water;

    public override string Identifier => "SRRW";

    public override uint Version => 3;

    public ResourceWaterV03()
    {
    }

    public ResourceWaterV03(ResourceWaterV02 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      spawn = reader.ReadDouble();
      water = reader.ReadSingle();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write(spawn);
      writer.Write(water);
    }

    public static ResourceWaterV03 Load(BinaryReader reader)
    {
      ResourceWaterV03 resourceWaterV03 = new ResourceWaterV03();
      resourceWaterV03.Load(reader.BaseStream);
      return resourceWaterV03;
    }

    protected override void UpgradeFrom(ResourceWaterV02 legacyData)
    {
      spawn = legacyData.spawn;
      water = legacyData.water;
    }

    public static void AssertAreEqual(ResourceWaterV03 expected, ResourceWaterV03 actual)
    {
    }

    public static void AssertAreEqual(ResourceWaterV02 expected, ResourceWaterV03 actual)
    {
    }
  }
}
