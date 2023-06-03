// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.MailV02
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace MonomiPark.SlimeRancher.Persist
{
  public class MailV02 : PersistedDataSet
  {
    public MailDirector.Type mailType;
    public string messageKey;
    public bool isRead;

    public override string Identifier => "SRMAIL";

    public override uint Version => 2;

    protected override void LoadData(BinaryReader reader)
    {
      mailType = (MailDirector.Type) reader.ReadInt32();
      messageKey = reader.ReadString();
      isRead = reader.ReadBoolean();
    }

    protected override void WriteData(BinaryWriter writer)
    {
      writer.Write((int) mailType);
      writer.Write(messageKey);
      writer.Write(isRead);
    }

    public static void AssertAreEqual(MailV02 expected, MailV02 actual)
    {
    }
  }
}
