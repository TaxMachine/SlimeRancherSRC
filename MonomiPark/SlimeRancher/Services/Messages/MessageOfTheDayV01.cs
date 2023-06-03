// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Services.Messages.MessageOfTheDayV01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Xml.Serialization;

namespace MonomiPark.SlimeRancher.Services.Messages
{
  public class MessageOfTheDayV01
  {
    [XmlAttribute(AttributeName = "messageId")]
    public string MessageId;
    public string ImageUrl;
    public string MessageTranslationId;
    public LocalizedMessage[] LocalizedMessages;

    public class LocalizedMessage
    {
      [XmlAttribute(AttributeName = "languageCode")]
      public string LanguageCode;
      public string AnnouncementText;
      public string TitleText;
      public string BodyText;
      public string ButtonText;
      public string Url;
    }
  }
}
