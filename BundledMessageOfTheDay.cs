// Decompiled with JetBrains decompiler
// Type: BundledMessageOfTheDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class BundledMessageOfTheDay : MessageOfTheDay
{
  public string url;
  public string announcementTranslationId;
  public string titleTranslationId;
  public string bodyTranslationId;
  public string buttonTranslationId;
  public List<DLCPackage.Id> showForAvailableDLCPackages;

  public override string GetAnnouncementText(string lang) => SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get(announcementTranslationId);

  public override string GetTitleText(string lang) => SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get(titleTranslationId);

  public override string GetBodyText(string lang) => SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get(bodyTranslationId);

  public override string GetButtonText(string lang) => SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get(buttonTranslationId);

  public override string GetUrl(string lang) => url;
}
