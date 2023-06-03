// Decompiled with JetBrains decompiler
// Type: ExchangeChatActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ExchangeChatActivator : MonoBehaviour, TechActivator
{
  public ExchangeDirector.OfferType[] offerTypes;
  public GameObject offlineGuiPrefab;
  private ExchangeDirector exchangeDir;

  public void Awake() => exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;

  public void Activate()
  {
    foreach (ExchangeDirector.OfferType offerType in offerTypes)
    {
      if (offerType == ExchangeDirector.OfferType.GENERAL && exchangeDir.TryToAcceptNewOffer() || exchangeDir.MaybeStartNext(offerType) || exchangeDir.CreateRancherChatUI(offerType, false))
        break;
    }
  }

  public GameObject GetCustomGuiPrefab()
  {
    bool flag = true;
    foreach (ExchangeDirector.OfferType offerType in offerTypes)
    {
      if (!exchangeDir.IsOffline(offerType))
      {
        flag = false;
        break;
      }
    }
    return flag ? offlineGuiPrefab : null;
  }
}
