// Decompiled with JetBrains decompiler
// Type: ExchangeEjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class ExchangeEjector : SRBehaviour, ExchangeDirector.Awarder
{
  public GameObject cratePrefab;
  public ExchangeDirector.OfferType offerType;
  public GameObject awardFX;
  public Transform awardAt;
  private const float EJECT_FORCE = 100f;

  public void AwardIfType(ExchangeDirector.OfferType offerType)
  {
    if (this.offerType != offerType)
      return;
    Eject();
  }

  private void Eject()
  {
    GameObject gameObject = InstantiateActor(cratePrefab, GetComponentInParent<Region>().setId, transform.position, transform.rotation);
    Rigidbody component = gameObject.GetComponent<Rigidbody>();
    component.isKinematic = false;
    component.AddForce(transform.forward * 100f);
    gameObject.GetComponent<ExchangeBreakOnImpact>().breakOpenOnStart = false;
    InstantiateDynamic(awardFX, awardAt.position, awardAt.rotation);
  }
}
