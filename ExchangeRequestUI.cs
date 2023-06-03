// Decompiled with JetBrains decompiler
// Type: ExchangeRequestUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeRequestUI : MonoBehaviour
{
  public Text noRequestText;
  public Text pendingRequestText;
  public ExchangeDirector.OfferType offerType;
  [SerializeField]
  private ExchangeProgressItemEntryUI[] items;
  public SlimeAppearanceDirector slimeAppearanceDirector;
  private ExchangeDirector exchangeDir;

  public void Awake()
  {
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    exchangeDir.onOfferChanged += OnOfferChanged;
  }

  public void OnEnable()
  {
    slimeAppearanceDirector.onSlimeAppearanceChanged += OnSlimeAppearanceUpdated;
    OnOfferChanged();
  }

  public void OnDisable() => slimeAppearanceDirector.onSlimeAppearanceChanged -= OnSlimeAppearanceUpdated;

  public void OnSlimeAppearanceUpdated(SlimeDefinition slime, SlimeAppearance appearance) => OnOfferChanged();

  public void Start() => OnOfferChanged();

  public void OnDestroy() => exchangeDir.onOfferChanged -= OnOfferChanged;

  public void OnOfferChanged()
  {
    List<ExchangeDirector.RequestedItemEntry> offerRequests = exchangeDir.GetOfferRequests(offerType);
    if (offerRequests == null)
    {
      if (exchangeDir.HasPendingOffers(offerType))
      {
        noRequestText.enabled = false;
        pendingRequestText.enabled = true;
      }
      else
      {
        noRequestText.enabled = true;
        pendingRequestText.enabled = false;
      }
      for (int index = 0; index < items.Length; ++index)
        items[index].SetEntry(null);
    }
    else
    {
      noRequestText.enabled = false;
      pendingRequestText.enabled = false;
      for (int index = 0; index < items.Length; ++index)
        items[index].SetEntry(offerRequests.Count > index ? offerRequests[index] : null);
    }
  }
}
