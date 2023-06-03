﻿// Decompiled with JetBrains decompiler
// Type: XboxOneContext
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using RichPresence;
using UnityEngine;

public class XboxOneContext : MonoBehaviour, Handler
{
  public TextAsset xboxEvents;
  public XboxEngagementPopupUI engagementPopupUIPrefab;
  public XboxUserChangePopupUI userChangePopupUIPrefab;

  public void SetRichPresence(MainMenuData data) => SetRichPresence("MainMenu");

  public void SetRichPresence(InZoneData data)
  {
    string id;
    if (!Director.TryGetZoneId(data.zone, out id))
      return;
    SetRichPresence(id);
  }

  private void SetRichPresence(string id)
  {
  }
}
