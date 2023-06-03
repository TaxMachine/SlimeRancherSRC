// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayLocalProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Message Of The Day/Local Provider")]
public class MessageOfTheDayLocalProvider : MessageOfTheDayProvider
{
  public MessageOfTheDayCollection messageCollection;
  private DLCDirector dlcDirector;

  public void SetDLCDirector(DLCDirector director) => dlcDirector = director;

  protected override void RetrieveMessage(
    SuccessHandler onSuccess,
    ErrorHandler onError)
  {
    if (messageCollection != null && messageCollection.messages.Count > 0)
    {
      MessageOfTheDay messageOfTheDay;
      BundledMessageOfTheDay bundledMessageOfTheDay;
      if (dlcDirector == null)
        bundledMessageOfTheDay = (BundledMessageOfTheDay) (messageOfTheDay = messageCollection.GetRandomMessage());
      else
        messageOfTheDay = bundledMessageOfTheDay = messageCollection.GetRandomMessage(CanShowMessage);
      MessageOfTheDay message = bundledMessageOfTheDay;
      if (message != null)
        onSuccess(message);
      else
        onError();
    }
    else
      onError();
  }

  private bool CanShowMessage(BundledMessageOfTheDay msg) => msg.showForAvailableDLCPackages.Count == 0 || msg.showForAvailableDLCPackages.Any(packageId => !SRSingleton<GameContext>.Instance.AutoSaveDirector.ProfileManager.Profile.DLC.installed.Contains(packageId));
}
