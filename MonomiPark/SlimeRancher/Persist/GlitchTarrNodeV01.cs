// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GlitchTarrNodeV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GlitchTarrNodeV01 : PersistedDataSet
  {
    public double activationTime;

    public override string Identifier => "SRGLITCH_TS";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => activationTime = reader.ReadDouble();

    protected override void WriteData(BinaryWriter writer) => writer.Write(activationTime);

    public static void AssertAreEqual(GlitchTarrNodeV01 expected, GlitchTarrNodeV01 actual)
    {
    }
  }
}
