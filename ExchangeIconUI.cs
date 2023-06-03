// Decompiled with JetBrains decompiler
// Type: ExchangeIconUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class ExchangeIconUI : MonoBehaviour
{
  public Image img;
  public Sprite defaultIcon;
  public Sprite pendingIcon;
  public ExchangeDirector.OfferType offerType;
  private ExchangeDirector exchangeDir;

  public void Awake()
  {
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    exchangeDir.onOfferChanged += OnOfferChanged;
  }

  public void Start() => OnOfferChanged();

  public void OnDestroy() => exchangeDir.onOfferChanged -= OnOfferChanged;

  public void OnOfferChanged()
  {
    string offerRancherId = exchangeDir.GetOfferRancherId(offerType);
    if (offerRancherId == null)
    {
      if (exchangeDir.HasPendingOffers(offerType) || offerType != ExchangeDirector.OfferType.GENERAL)
        img.sprite = pendingIcon;
      else
        img.sprite = defaultIcon;
    }
    else
      img.sprite = GetRancherImage(offerRancherId);
  }

  private Sprite GetRancherImage(string rancherId) => exchangeDir.GetRancherIcon(rancherId);
}
