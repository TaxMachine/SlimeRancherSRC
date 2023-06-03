// Decompiled with JetBrains decompiler
// Type: ExchangeBreakOnImpact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeBreakOnImpact : SRBehaviour
{
  public GameObject breakFX;
  public GameObject coinPrefab;
  [Tooltip("Prefab spawned when a time extension is granted. (optional)")]
  public GameObject timeExtensionFX;
  [HideInInspector]
  public bool breakOpenOnStart = true;
  private const float COLLISION_THRESHOLD = 0.0f;
  private Rigidbody body;
  private ExchangeDirector exchangeDir;
  private LookupDirector lookupDir;
  private bool breaking;
  private const int COINS_PER_ITEM = 50;
  private const float BREAK_SPAWN_RADIUS = 1f;
  private const float PRODUCE_SCALE_UP_TIME = 0.2f;

  public void Awake()
  {
    body = GetComponent<Rigidbody>();
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
  }

  public void Start()
  {
    if (!breakOpenOnStart)
      return;
    BreakOpen();
  }

  public void OnCollisionEnter(Collision col)
  {
    if (col.collider.isTrigger || body.isKinematic)
      return;
    float a = 0.0f;
    foreach (ContactPoint contact in col.contacts)
      a = Mathf.Max(a, Vector3.Dot(contact.normal, col.relativeVelocity));
    if (a <= 0.0)
      return;
    BreakOpen();
  }

  private void BreakOpen()
  {
    if (breaking)
      return;
    breaking = true;
    SpawnAndPlayFX(breakFX, this.gameObject.transform.position, this.gameObject.transform.rotation);
    Destroyer.DestroyActor(this.gameObject, "ExchangeBreakOnImpact.BreakOpen");
    List<ExchangeDirector.ItemEntry> offerRewards = exchangeDir.GetOfferRewards(ExchangeDirector.OfferType.GENERAL);
    RegionRegistry.RegionSetId setId = GetComponent<RegionMember>().setId;
    if (offerRewards != null)
    {
      foreach (ExchangeDirector.ItemEntry itemEntry in offerRewards)
      {
        if (itemEntry.specReward != ExchangeDirector.NonIdentReward.NONE)
        {
          SpawnSpecReward(itemEntry.specReward);
        }
        else
        {
          GameObject prefab = lookupDir.GetPrefab(itemEntry.id);
          for (int index = 0; index < itemEntry.count; ++index)
          {
            Vector3 position = transform.position + Random.insideUnitSphere * 1f;
            GameObject gameObject = InstantiateActor(prefab, setId, position, Quaternion.identity);
            gameObject.transform.DOScale(gameObject.transform.localScale, 0.2f).From(0.01f).SetEase(Ease.Linear);
          }
        }
      }
    }
    exchangeDir.RewardsDidSpawn(ExchangeDirector.OfferType.GENERAL);
  }

  private void SpawnSpecReward(ExchangeDirector.NonIdentReward reward)
  {
    switch (reward)
    {
      case ExchangeDirector.NonIdentReward.NEWBUCKS_SMALL:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MEDIUM:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_LARGE:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_HUGE:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MOCHI:
        int num1 = GetNewbucksRewardValue(reward) / 50;
        for (int index = 0; index < num1; ++index)
          SpawnAndPlayFX(coinPrefab, transform.position + Random.insideUnitSphere, Quaternion.identity);
        break;
      case ExchangeDirector.NonIdentReward.TIME_EXTENSION_12H:
        PlayerState playerState = SRSingleton<SceneContext>.Instance.PlayerState;
        double num2 = GetTimeExtensionRewardValue(reward) * 3600.0;
        playerState.SetEndGameTime(playerState.GetEndGameTime().Value + num2);
        if (!(timeExtensionFX != null))
          break;
        SpawnAndPlayFX(timeExtensionFX, transform.position, Quaternion.identity);
        break;
    }
  }

  public static int GetNewbucksRewardValue(ExchangeDirector.NonIdentReward reward)
  {
    switch (reward)
    {
      case ExchangeDirector.NonIdentReward.NEWBUCKS_SMALL:
        return 250;
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MEDIUM:
        return 500;
      case ExchangeDirector.NonIdentReward.NEWBUCKS_LARGE:
        return 750;
      case ExchangeDirector.NonIdentReward.NEWBUCKS_HUGE:
        return 1000;
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MOCHI:
        return 200;
      default:
        return 0;
    }
  }

  public static int GetTimeExtensionRewardValue(ExchangeDirector.NonIdentReward reward) => reward == ExchangeDirector.NonIdentReward.TIME_EXTENSION_12H ? 12 : 0;
}
