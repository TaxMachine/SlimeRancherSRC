// Decompiled with JetBrains decompiler
// Type: LuckySlimeProduceCoins
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckySlimeProduceCoins : CollidableActorBehaviour, Collidable
{
  public GameObject coinsPrefab;
  private LuckySlimeFlee flee;
  private HashSet<GameObject> colliders = new HashSet<GameObject>();
  private SlimeAudio slimeAudio;
  private SlimeFaceAnimator sfAnimator;
  private SlimeEat slimeEat;
  private int coinSetsProduced;
  private int coinPrefabsLastHit;
  private const float HIT_THRESHOLD = 0.02f;
  private const float JUMP_ON_HIT_VERT_FORCE = 450f;
  private const float JUMP_ON_HIT_MAX_HORIZ_FORCE = 225f;
  private const int MIN_INIT_COIN_PREFABS = 2;
  private const int MAX_INIT_COIN_PREFABS = 2;
  private const int MAX_TOTAL_COIN_PREFABS = 6;
  private const float DELAY_BETWEEN_COINS = 0.1f;
  private const float ADDL_DELAY = 0.1f;

  public override void Start()
  {
    base.Start();
    flee = GetComponent<LuckySlimeFlee>();
    slimeAudio = GetComponent<SlimeAudio>();
    sfAnimator = GetComponent<SlimeFaceAnimator>();
    slimeEat = GetComponent<SlimeEat>();
  }

  public void ProcessCollisionEnter(Collision collision)
  {
    Identifiable component = collision.gameObject.GetComponent<Identifiable>();
    if (!(component != null) || !Identifiable.IsAnimal(component.id) || colliders.Contains(collision.gameObject))
      return;
    float num1 = float.NegativeInfinity;
    foreach (ContactPoint contact in collision.contacts)
    {
      float num2 = Vector3.Dot(contact.normal, collision.relativeVelocity);
      if (num2 > (double) num1)
        num1 = num2;
    }
    if (num1 <= 0.019999999552965164)
      return;
    ProduceCoins(collision.gameObject);
    if (flee != null)
      flee.StartFleeing(SRSingleton<SceneContext>.Instance.Player);
    colliders.Add(collision.gameObject);
    SRSingleton<SceneContext>.Instance.PediaDirector.MaybeShowPopup(GetComponent<Identifiable>().id);
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  private IEnumerator DropCoinsAndJumpDelayed()
  {
    LuckySlimeProduceCoins slimeProduceCoins = this;
    yield return new WaitForSeconds(0.35f);
    slimeProduceCoins.GetComponent<Rigidbody>().AddForce(new Vector3(Randoms.SHARED.GetFloat(225f), 450f, Randoms.SHARED.GetFloat(225f)));
    slimeProduceCoins.slimeAudio.Play(slimeProduceCoins.slimeAudio.slimeSounds.jumpCue);
    slimeProduceCoins.slimeAudio.Play(slimeProduceCoins.slimeAudio.slimeSounds.voiceJumpCue);
    for (int ii = 0; ii < slimeProduceCoins.coinPrefabsLastHit; ++ii)
    {
      InstantiateDynamic(slimeProduceCoins.coinsPrefab, slimeProduceCoins.transform.position, slimeProduceCoins.transform.rotation);
      yield return new WaitForSeconds(0.1f);
    }
  }

  private void ProduceCoins(GameObject triggerer)
  {
    if (!slimeEat.MaybeSpinAndChomp(triggerer, true))
      return;
    coinPrefabsLastHit = coinPrefabsLastHit != 0 ? Math.Min(6, coinPrefabsLastHit * 2) : Randoms.SHARED.GetInRange(2, 3);
    StartCoroutine(DropCoinsAndJumpDelayed());
    sfAnimator.SetTrigger("triggerWince");
    ++coinSetsProduced;
  }
}
