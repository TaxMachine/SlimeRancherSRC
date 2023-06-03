// Decompiled with JetBrains decompiler
// Type: BoomSlimeExplode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

public class BoomSlimeExplode : SlimeSubbehaviour, BoomMaterialAnimator.BoomMaterialInformer
{
  public float explodePower = 600f;
  public float explodeRadius = 7f;
  public float minPlayerDamage = 15f;
  public float maxPlayerDamage = 45f;
  private GameObject explodeFX;
  private float nextPossibleExplode;
  private float nextExplodeDelayTime = BOOM_MAX_DELAY;
  private float nextRecoverTime;
  private SlimeFaceAnimator sfAnimator;
  private CalmedByWaterSpray calmed;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  public static float BOOM_MIN_DELAY = 10f;
  public static float BOOM_MAX_DELAY = 45f;
  public static float EXPLOSION_PREP_TIME = 1.5f;
  public static float EXPLOSION_RECOVERY_TIME = 5f;
  private State state;

  public override void Awake()
  {
    base.Awake();
    sfAnimator = GetComponent<SlimeFaceAnimator>();
    calmed = GetComponent<CalmedByWaterSpray>();
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    if (slimeAppearanceApplicator.Appearance != null)
      explodeFX = slimeAppearanceApplicator.Appearance.ExplosionAppearance.explodeFx;
    slimeAppearanceApplicator.OnAppearanceChanged += appearance => explodeFX = appearance.ExplosionAppearance.explodeFx;
  }

  public override void OnEnable()
  {
    base.OnEnable();
    if (Time.time + (double) BOOM_MIN_DELAY <= nextPossibleExplode)
      return;
    nextPossibleExplode = Math.Max(nextPossibleExplode, Time.time + Randoms.SHARED.GetFloat(BOOM_MIN_DELAY));
  }

  public override void Start()
  {
    base.Start();
    nextExplodeDelayTime = BoomDelay();
    nextPossibleExplode = Time.time + nextExplodeDelayTime * Randoms.SHARED.GetInRange(0.25f, 1f);
    GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(false);
  }

  public override float Relevancy(bool isGrounded) => Time.fixedTime <= (double) nextPossibleExplode || calmed.IsCalmed() ? 0.0f : 1f;

  public override void Action()
  {
  }

  public override void Selected() => StartCoroutine(DelayedExplosion());

  public void FixedUpdate()
  {
    if (!calmed.IsCalmed())
      return;
    nextPossibleExplode += Time.fixedDeltaTime;
  }

  private float BoomDelay() => Mathf.Lerp(BOOM_MIN_DELAY, BOOM_MAX_DELAY, Mathf.Clamp(Randoms.SHARED.GetInRange(-0.1f, 0.1f) + (1f - emotions.GetCurr(SlimeEmotions.Emotion.AGITATION)), 0.0f, 1f));

  private IEnumerator DelayedExplosion()
  {
    BoomSlimeExplode boomSlimeExplode = this;
    boomSlimeExplode.state = State.PREPARING;
    boomSlimeExplode.GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(true);
    boomSlimeExplode.sfAnimator.SetTrigger("triggerGrimace");
    yield return new WaitForSeconds(EXPLOSION_PREP_TIME);
    boomSlimeExplode.GetComponentsInChildren<ExplodeIndicatorMarker>(true)[0].SetActive(false);
    boomSlimeExplode.state = State.EXPLODING;
    SpawnAndPlayFX(boomSlimeExplode.explodeFX, boomSlimeExplode.transform.position, boomSlimeExplode.transform.rotation);
    boomSlimeExplode.Explode();
    boomSlimeExplode.nextExplodeDelayTime = boomSlimeExplode.BoomDelay();
    boomSlimeExplode.nextPossibleExplode = Time.time + boomSlimeExplode.nextExplodeDelayTime;
    boomSlimeExplode.state = State.RECOVERING;
    boomSlimeExplode.sfAnimator.SetTrigger("triggerFried");
    boomSlimeExplode.nextRecoverTime = Time.time + EXPLOSION_RECOVERY_TIME;
    yield return new WaitForSeconds(EXPLOSION_RECOVERY_TIME);
    boomSlimeExplode.state = State.IDLE;
  }

  private void Explode()
  {
    PhysicsUtil.Explode(gameObject, explodeRadius, explodePower, minPlayerDamage, maxPlayerDamage);
    if (gameObject.layer != LayerMask.NameToLayer("Launched"))
      return;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.LAUNCHED_BOOM_EXPLODE, 1);
  }

  public override void OnDisable()
  {
    base.OnDisable();
    state = State.IDLE;
  }

  public override bool CanRethink() => state == State.IDLE;

  public float GetReadiness() => 1f - Mathf.Clamp((nextPossibleExplode - Time.time) / nextExplodeDelayTime, 0.0f, 1f);

  public float GetRecoveriness() => state != State.RECOVERING ? 0.0f : Mathf.Clamp((nextRecoverTime - Time.time) / EXPLOSION_RECOVERY_TIME, 0.0f, 1f);

  private enum State
  {
    IDLE,
    PREPARING,
    EXPLODING,
    RECOVERING,
  }
}
