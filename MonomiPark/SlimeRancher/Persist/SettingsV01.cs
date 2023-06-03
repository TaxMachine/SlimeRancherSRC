// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.SettingsV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class SettingsV01 : PersistedDataSet
  {
    public OptionsV12 options = new OptionsV12();

    public override string Identifier => "SRSETTINGS";

    public override uint Version => 1;

    protected override void LoadData(BinaryReader reader)
    {
      options = new OptionsV12();
      options.Load(reader.BaseStream);
    }

    protected override void WriteData(BinaryWriter writer) => options.Write(writer.BaseStream);

    public void SetLegacyProfileOptions(ProfileV04 legacyProfile)
    {
      OptionsV09 options = legacyProfile.options;
      this.options = new OptionsV12();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        options.Write(memoryStream);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        this.options.Load(memoryStream);
      }
    }
  }
}
