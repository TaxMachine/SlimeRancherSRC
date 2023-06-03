// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.GlitchImpostoDirectorV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class GlitchImpostoDirectorV01 : PersistedDataSet
  {
    public double? hibernationTime;

    public override string Identifier => "SRGLITCH_ID";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => LoadNullable(reader, out hibernationTime);

    protected override void WriteData(BinaryWriter writer) => WriteNullable(writer, hibernationTime);

    public static void AssertAreEqual(
      GlitchImpostoDirectorV01 expected,
      GlitchImpostoDirectorV01 actual)
    {
    }
  }
}
