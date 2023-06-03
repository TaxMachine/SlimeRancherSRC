// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.DecorizerSettingsV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class DecorizerSettingsV01 : PersistedDataSet
  {
    public Identifiable.Id selected;

    public override string Identifier => "SRDZRSETTINGS";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader) => selected = (Identifiable.Id) reader.ReadInt32();

    protected override void WriteData(BinaryWriter writer) => writer.Write((int) selected);

    public static void AssertAreEqual(DecorizerSettingsV01 expected, DecorizerSettingsV01 actual)
    {
    }
  }
}
