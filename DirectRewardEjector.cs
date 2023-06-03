// Decompiled with JetBrains decompiler
// Type: DirectRewardEjector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class DirectRewardEjector : SRBehaviour, ExchangeDirector.Awarder
{
  public GameObject rewardPrefab;
  public int rewardCount = 1;
  public ExchangeDirector.OfferType offerType;
  [Tooltip("SFX played when the reward is ejected. (optional)")]
  public SECTR_AudioCue onEjectCue;
  private bool rewardIsActor;
  private const float EJECT_FORCE = 60f;

  public void Start() => rewardIsActor = rewardPrefab.GetComponent<Identifiable>() != null;

  public void AwardIfType(ExchangeDirector.OfferType offerType)
  {
    if (this.offerType != offerType)
      return;
    Eject();
  }

  public void Eject()
  {
    RegionRegistry.RegionSetId setId = GetComponentInParent<Region>().setId;
    for (int index = 0; index < rewardCount; ++index)
    {
      Rigidbody component = (rewardIsActor ? InstantiateActor(rewardPrefab, setId, transform.position, transform.rotation) : InstantiateDynamic(rewardPrefab, transform.position, transform.rotation)).GetComponent<Rigidbody>();
      if (component != null)
      {
        component.isKinematic = false;
        component.AddForce(transform.forward * 60f);
      }
    }
    SRSingleton<SceneContext>.Instance.ExchangeDirector.RewardsDidSpawn(offerType);
    SECTR_AudioSystem.Play(onEjectCue, transform.position, false);
  }
}
