// Decompiled with JetBrains decompiler
// Type: MessageOfTheDayButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageOfTheDayButton : MonoBehaviour
{
  public const string UNITY_ANALYTICS_CLICK_EVENT = "MotDClicked";
  public const string UNITY_ANALYTICS_MESSAGE_ID_KEY = "MessageId";
  public TMP_Text buttonLabel;
  private MessageOfTheDayDirector motdDirector;
  private string messageId;
  private string url;

  public void Awake() => motdDirector = SRSingleton<GameContext>.Instance.MessageOfTheDayDirector;

  public void UpdateButton(string messageId, string buttonText, string url)
  {
    this.messageId = messageId;
    buttonLabel.text = buttonText;
    this.url = url;
  }

  public void OnClick()
  {
    AnalyticsUtil.CustomEvent("MotDClicked", new Dictionary<string, object>()
    {
      {
        "MessageId",
        messageId
      }
    }, false);
    motdDirector.ActivateLink(url);
  }
}
