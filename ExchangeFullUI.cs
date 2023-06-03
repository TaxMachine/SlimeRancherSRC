// Decompiled with JetBrains decompiler
// Type: ExchangeFullUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeFullUI : BaseUI
{
  [Tooltip("The individual request UI elements we are managing.")]
  public ExchangeItemEntryUI[] requestItems;
  [Tooltip("the individual reward UI elements we are managing.")]
  public ExchangeItemEntryUI[] rewardItems;
  [Tooltip("The panel we will enable when we have no offer.")]
  public GameObject noRequestPanel;
  [Tooltip("The panel we will enable when we have an offer.")]
  public GameObject mainOfferPanel;
  [Tooltip("The text which shows the Rancher's name.")]
  public TMP_Text rancherText;
  [Tooltip("The image which shows the Rancher's face.")]
  public Image rancherImg;
  [Tooltip("The flavor text which goes with the offer.")]
  public TMP_Text flavorText;
  private ExchangeDirector exchangeDir;
  private MessageBundle exchangeBundle;

  public override void Awake()
  {
    base.Awake();
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    exchangeDir.onOfferChanged += OnOfferChanged;
    exchangeBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("exchange");
  }

  public void Start() => OnOfferChanged();

  public override void OnDestroy()
  {
    base.OnDestroy();
    exchangeDir.onOfferChanged -= OnOfferChanged;
  }

  public void OnOfferChanged()
  {
    List<ExchangeDirector.RequestedItemEntry> offerRequests = exchangeDir.GetOfferRequests(ExchangeDirector.OfferType.GENERAL);
    List<ExchangeDirector.ItemEntry> offerRewards = exchangeDir.GetOfferRewards(ExchangeDirector.OfferType.GENERAL);
    if (offerRequests == null || offerRewards == null)
    {
      noRequestPanel.SetActive(true);
      mainOfferPanel.SetActive(false);
      for (int index = 0; index < requestItems.Length; ++index)
        requestItems[index].SetEntry(null);
      for (int index = 0; index < rewardItems.Length; ++index)
        rewardItems[index].SetEntry(null);
    }
    else
    {
      noRequestPanel.SetActive(false);
      mainOfferPanel.SetActive(true);
      for (int index = 0; index < requestItems.Length; ++index)
        requestItems[index].SetEntry(offerRequests.Count > index ? offerRequests[index] : (ExchangeDirector.ItemEntry) null);
      for (int index = 0; index < rewardItems.Length; ++index)
        rewardItems[index].SetEntry(offerRewards.Count > index ? offerRewards[index] : null);
    }
    string offerRancherId = exchangeDir.GetOfferRancherId(ExchangeDirector.OfferType.GENERAL);
    string offerId = exchangeDir.GetOfferId(ExchangeDirector.OfferType.GENERAL);
    if (offerId != null && offerRancherId != null)
    {
      rancherText.text = exchangeBundle.Get("m.rancher." + offerRancherId);
      flavorText.text = exchangeBundle.Get(offerId);
      Sprite rancherImage = GetRancherImage(offerRancherId);
      if (!(rancherImage != null))
        return;
      rancherImg.sprite = rancherImage;
    }
    else
    {
      rancherText.text = "";
      flavorText.text = "";
    }
  }

  private Sprite GetRancherImage(string rancherId) => Resources.Load("Exchange/Ranchers/" + rancherId, typeof (Sprite)) as Sprite;
}
