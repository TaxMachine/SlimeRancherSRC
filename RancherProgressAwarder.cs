// Decompiled with JetBrains decompiler
// Type: RancherProgressAwarder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RancherProgressAwarder : SRBehaviour, ExchangeDirector.Awarder
{
  public ExchangeDirector.OfferType offerType;
  public ProgressDirector.ProgressType progressType;
  public GameObject awardFX;
  public Transform awardAt;

  public void AwardIfType(ExchangeDirector.OfferType offerType)
  {
    if (this.offerType != offerType)
      return;
    DoAward();
  }

  public void DoAward()
  {
    ProgressDirector progressDirector = SRSingleton<SceneContext>.Instance.ProgressDirector;
    ExchangeDirector exchangeDirector = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    progressDirector.AddProgress(progressType);
    int offerType = (int) this.offerType;
    exchangeDirector.RewardsDidSpawn((ExchangeDirector.OfferType) offerType);
    InstantiateDynamic(awardFX, awardAt.position, awardAt.rotation);
  }
}
