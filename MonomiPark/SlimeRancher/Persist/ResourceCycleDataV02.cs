// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.ResourceCycleDataV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class ResourceCycleDataV02 : PersistedDataSet
  {
    public ResourceCycle.State state;
    public float progressTime;

    public override string Identifier => "SRRCD";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      state = (ResourceCycle.State) reader.ReadInt32();
      progressTime = reader.ReadSingle();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) state);
      writer.Write(progressTime);
    }

    public static void AssertAreEqual(ResourceCycleDataV02 expected, ResourceCycleDataV02 actual)
    {
    }
  }
}
