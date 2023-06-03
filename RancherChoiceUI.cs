// Decompiled with JetBrains decompiler
// Type: RancherChoiceUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class RancherChoiceUI : BaseUI
{
  public Image rancherAImg;
  public Image rancherBImg;
  public TMP_Text rancherAText;
  public TMP_Text rancherBText;
  private List<string> rancherIds;

  public void Init(List<string> rancherIds)
  {
    this.rancherIds = new List<string>(rancherIds);
    ExchangeDirector exchangeDirector = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("exchange");
    rancherAImg.sprite = exchangeDirector.GetRancherIcon(rancherIds[0]);
    rancherBImg.sprite = exchangeDirector.GetRancherIcon(rancherIds[1]);
    rancherAText.text = bundle.Get("m.rancher." + rancherIds[0]);
    rancherBText.text = bundle.Get("m.rancher." + rancherIds[1]);
  }

  public void SelectRancherA() => SelectRancher(rancherIds[0]);

  public void SelectRancherB() => SelectRancher(rancherIds[1]);

  private void SelectRancher(string rancherId)
  {
    Close();
    ExchangeDirector exchangeDirector = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    exchangeDirector.SelectDailyOffer(rancherId, false);
    exchangeDirector.CreateRancherChatUI(ExchangeDirector.OfferType.GENERAL, false);
  }
}
