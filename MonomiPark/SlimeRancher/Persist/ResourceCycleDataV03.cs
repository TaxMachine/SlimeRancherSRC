// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ResourceCycleDataV03
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ResourceCycleDataV03 : VersionedPersistedDataSet<ResourceCycleDataV02>
  {
    public ResourceCycle.State state;
    public double progressTime;

    public ResourceCycleDataV03()
    {
    }

    public ResourceCycleDataV03(ResourceCycleDataV02 legacyData) => UpgradeFrom(legacyData);

    public override string Identifier => "SRRCD";

    public override uint Version => 3;

    protected override void LoadData(BinaryReader reader)
    {
      state = (ResourceCycle.State) reader.ReadInt32();
      progressTime = reader.ReadDouble();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) state);
      writer.Write(progressTime);
    }

    protected override void UpgradeFrom(ResourceCycleDataV02 legacyData)
    {
      state = legacyData.state;
      progressTime = legacyData.progressTime;
    }

    public static void AssertAreEqual(ResourceCycleDataV03 expected, ResourceCycleDataV03 actual)
    {
    }

    public static void AssertAreEqual(ResourceCycleDataV02 expected, ResourceCycleDataV03 actual)
    {
    }
  }
}
