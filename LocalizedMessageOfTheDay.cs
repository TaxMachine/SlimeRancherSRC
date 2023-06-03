// Decompiled with JetBrains decompiler
// Type: LocalizedMessageOfTheDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedMessageOfTheDay : MessageOfTheDay
{
  private Dictionary<string, LocalizedEntry> localizedEntries = new Dictionary<string, LocalizedEntry>();
  private string defaultLanguageCode;

  public LocalizedMessageOfTheDay(string id, Sprite sprite, string defaultLanguageCode)
  {
    this.id = id;
    this.sprite = sprite;
    this.defaultLanguageCode = defaultLanguageCode;
  }

  public void AddEntry(
    string languageCode,
    string announcementText,
    string titleText,
    string bodyText,
    string buttonText,
    string url)
  {
    localizedEntries.Add(languageCode, new LocalizedEntry()
    {
      announcementText = announcementText,
      titleText = titleText,
      bodyText = bodyText,
      buttonText = buttonText,
      url = url
    });
  }

  public override string GetAnnouncementText(string languageCode) => GetEntryText(languageCode, entry => entry.announcementText);

  public override string GetTitleText(string languageCode) => GetEntryText(languageCode, entry => entry.titleText);

  public override string GetBodyText(string languageCode) => GetEntryText(languageCode, entry => entry.bodyText);

  public override string GetButtonText(string languageCode) => GetEntryText(languageCode, entry => entry.buttonText);

  private string GetEntryText(
    string languageCode,
    Func<LocalizedEntry, string> extractor)
  {
    LocalizedEntry entry;
    return !TryGetLocalizedValue(languageCode, out entry) ? "" : extractor(entry);
  }

  private bool TryGetLocalizedValue(
    string languageCode,
    out LocalizedEntry entry)
  {
    return localizedEntries.TryGetValue(languageCode, out entry) || localizedEntries.TryGetValue(defaultLanguageCode, out entry);
  }

  public override string GetUrl(string languageCode)
  {
    LocalizedEntry localizedEntry;
    return localizedEntries.TryGetValue(languageCode, out localizedEntry) || localizedEntries.TryGetValue(defaultLanguageCode, out localizedEntry) ? localizedEntry.url : "";
  }

  private struct LocalizedEntry
  {
    public string announcementText;
    public string titleText;
    public string bodyText;
    public string buttonText;
    public string url;
  }
}
