// Decompiled with JetBrains decompiler
// Type: EmptyMessageOfTheDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class EmptyMessageOfTheDay : MessageOfTheDay
{
  public static EmptyMessageOfTheDay Default = new EmptyMessageOfTheDay();

  private EmptyMessageOfTheDay()
  {
  }

  public override string GetUrl(string lang) => "";

  public override string GetAnnouncementText(string lang) => "";

  public override string GetTitleText(string lang) => "";

  public override string GetBodyText(string lang) => "";

  public override string GetButtonText(string lang) => "";
}
