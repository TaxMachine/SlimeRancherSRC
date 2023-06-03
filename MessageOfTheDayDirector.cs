// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class MessageOfTheDayDirector
{
  public MessageOfTheDayProvider pcProvider;
  public MessageOfTheDayProvider epicProvider;
  public MessageOfTheDayProvider steamProvider;
  public MessageOfTheDayProvider ps4Provider;
  public MessageOfTheDayProvider xboxProvider;
  public MessageOfTheDayProvider tencentProvider;
  public GameContext gameContext;
  private const string DLC_PREFIX = "dlc://";

  public MessageOfTheDayProvider GetProvider() => steamProvider;

  public void ActivateLink(string url)
  {
    if (string.IsNullOrEmpty(url))
      Log.Warning("MotD url to activate was null or empty.");
    else if (url.StartsWith("dlc://"))
    {
      try
      {
        gameContext.DLCDirector.ShowPackageInStore((DLCPackage.Id) Enum.Parse(typeof (DLCPackage.Id), url.Substring("dlc://".Length)));
      }
      catch (Exception ex)
      {
        Log.Error("Exception when trying to extract DLC ID from DLC URL in MotD.", "Message", ex.Message, "stackTrace", ex.StackTrace);
      }
    }
    else
      Application.OpenURL(url);
  }
}
