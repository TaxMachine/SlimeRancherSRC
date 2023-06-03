// Decompiled with JetBrains decompiler
// Type: MessageOfTheDay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public abstract class MessageOfTheDay
{
  public string id;
  public Sprite sprite;

  public virtual string GetId() => id;

  public Sprite GetSprite() => sprite;

  public abstract string GetUrl(string lang);

  public abstract string GetAnnouncementText(string lang);

  public abstract string GetTitleText(string lang);

  public abstract string GetBodyText(string lang);

  public abstract string GetButtonText(string lang);
}
