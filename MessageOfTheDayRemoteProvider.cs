// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayRemoteProvider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Services;
using MonomiPark.SlimeRancher.Services.Messages;
using UnityEngine;

[CreateAssetMenu(menuName = "Message Of The Day/Remote Provider")]
public class MessageOfTheDayRemoteProvider : MessageOfTheDayProvider
{
  public string url;
  public int timeout;

  protected override void RetrieveMessage(
    SuccessHandler onSuccess,
    ErrorHandler onError)
  {
    MessageOfTheDayServiceRequest dayServiceRequest = new MessageOfTheDayServiceRequest(url, timeout);
    dayServiceRequest.OnError += () => onError();
    dayServiceRequest.OnSuccess += (message, image) => onSuccess(CreateLocalizedMessage(message, image));
    dayServiceRequest.Begin();
  }

  private MessageOfTheDay CreateLocalizedMessage(MessageOfTheDayV01 serviceMessage, Texture2D image)
  {
    Texture2D texture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, true);
    texture.SetPixels(image.GetPixels());
    texture.Apply();
    Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f));
    LocalizedMessageOfTheDay localizedMessage1 = new LocalizedMessageOfTheDay(serviceMessage.MessageId, sprite, "en");
    foreach (MessageOfTheDayV01.LocalizedMessage localizedMessage2 in serviceMessage.LocalizedMessages)
      localizedMessage1.AddEntry(localizedMessage2.LanguageCode, localizedMessage2.AnnouncementText, localizedMessage2.TitleText, localizedMessage2.BodyText, localizedMessage2.ButtonText, localizedMessage2.Url);
    return localizedMessage1;
  }
}
