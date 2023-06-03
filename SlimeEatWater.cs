// Decompiled with JetBrains decompiler
// Type: SlimeEatWater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using MonomiPark.SlimeRancher.Regions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEatWater : SRBehaviour
{
  [Tooltip("The plort to produce when we eat.")]
  public GameObject plort;
  public GameObject produceFX;
  public float slimeDensityDistance = 10f;
  public int maxSlimeDensity;
  public float plortDensityDistance = 10f;
  public int maxPlortDensity = 8;
  public float eatRate = 3f;
  private SlimeEmotions emotions;
  private SlimeEat slimeEat;
  private float nextChompTime;
  private HashSet<LiquidSource> waters = new HashSet<LiquidSource>();
  private SlimeAudio slimeAudio;
  private RegionMember regionMember;
  private SlimeFaceAnimator faceAnim;
  private bool tooDenseToProducePlort;
  private float nextDensityCheck;
  private static readonly Vector3 LOCAL_PRODUCE_LOC = new Vector3(0.0f, 0.5f, 0.0f);
  private static readonly Vector3 LOCAL_PRODUCE_VEL = new Vector3(0.0f, 1f, 0.0f);
  private const float DENSITY_CHECK_PERIOD = 2f;
  private const float PRODUCE_SCALE_UP_TIME = 0.5f;
  private int nearbyFavoriteToys;
  private List<GameObject> objectsInCell = new List<GameObject>();

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    slimeEat = GetComponent<SlimeEat>();
    slimeAudio = GetComponent<SlimeAudio>();
    faceAnim = GetComponent<SlimeFaceAnimator>();
    regionMember = GetComponent<RegionMember>();
    ResetEatClock();
  }

  public void Update()
  {
    if (Time.time >= (double) nextDensityCheck)
    {
      tooDenseToProducePlort = IsSlimeDensityTooHigh();
      faceAnim.SetShouldBlush(tooDenseToProducePlort);
      if (!tooDenseToProducePlort)
        tooDenseToProducePlort = IsPlortDensityTooHigh();
      nextDensityCheck = Time.time + 2f;
    }
    if (waters.Count <= 0 || Time.time < (double) nextChompTime || emotions.GetCurr(SlimeEmotions.Emotion.HUNGER) <= (double) slimeEat.minDriveToEat)
      return;
    LiquidSource liquidSource = Randoms.SHARED.Pick(waters, null);
    liquidSource.ConsumeLiquid();
    if (!tooDenseToProducePlort)
      StartCoroutine(ProduceAfterDelay(1, plort, 2f));
    OnEat(SlimeEmotions.Emotion.HUNGER, liquidSource.liquidId);
  }

  private bool IsPlortDensityTooHigh()
  {
    int num1 = 0;
    float num2 = plortDensityDistance * plortDensityDistance;
    int num3 = CalcMaximumPlortDensity();
    objectsInCell.Clear();
    CellDirector.Get(Identifiable.Id.PUDDLE_PLORT, regionMember, objectsInCell);
    int count = objectsInCell.Count;
    for (int index = 0; index < count && num1 <= num3; ++index)
    {
      if ((objectsInCell[index].transform.position - transform.position).sqrMagnitude <= (double) num2)
        ++num1;
    }
    objectsInCell.Clear();
    return num1 > num3;
  }

  private bool IsSlimeDensityTooHigh()
  {
    int num1 = 0;
    float num2 = slimeDensityDistance * slimeDensityDistance;
    int num3 = CalcMaximumSlimeDensity();
    objectsInCell.Clear();
    CellDirector.GetSlimes(regionMember, objectsInCell);
    int count = objectsInCell.Count;
    for (int index = 0; index < count && num1 <= num3; ++index)
    {
      if ((objectsInCell[index].transform.position - transform.position).sqrMagnitude <= (double) num2)
        ++num1;
    }
    objectsInCell.Clear();
    return num1 > num3;
  }

  public int CalcMaximumSlimeDensity() => nearbyFavoriteToys > 0 ? maxSlimeDensity + 1 : maxSlimeDensity;

  public int CalcMaximumPlortDensity() => maxPlortDensity;

  public void OnTriggerEnter(Collider col)
  {
    LiquidSource component = col.gameObject.GetComponent<LiquidSource>();
    if (!(component != null) || !Identifiable.IsWater(component.liquidId))
      return;
    waters.Add(component);
  }

  public void OnTriggerExit(Collider col)
  {
    LiquidSource component = col.gameObject.GetComponent<LiquidSource>();
    if (!(component != null) || !Identifiable.IsWater(component.liquidId))
      return;
    waters.Remove(component);
  }

  public void ResetEatClock() => nextChompTime = Time.time + eatRate;

  public void EnterToyProximity() => ++nearbyFavoriteToys;

  public void ExitToyProximity() => --nearbyFavoriteToys;

  private void OnEat(SlimeEmotions.Emotion driver, Identifiable.Id otherId)
  {
    ResetEatClock();
    emotions.Adjust(driver, -slimeEat.drivePerEat);
    if (otherId == Identifiable.Id.PLAYER)
      return;
    emotions.Adjust(SlimeEmotions.Emotion.AGITATION, -slimeEat.agitationPerEat);
  }

  private IEnumerator ProduceAfterDelay(int count, GameObject produces, float delay)
  {
    yield return new WaitForSeconds(delay);
    Produce(count, produces, false);
  }

  private void Produce(int count, GameObject produces, bool immediate)
  {
    if (!(this.gameObject != null))
      return;
    for (int index = 0; index < count; ++index)
    {
      Vector3 position = transform.TransformPoint(LOCAL_PRODUCE_LOC);
      Vector3 vector3 = transform.TransformVector(LOCAL_PRODUCE_VEL);
      GameObject gameObject = InstantiateActor(produces, regionMember.setId, position, transform.rotation);
      Rigidbody component1 = gameObject.GetComponent<Rigidbody>();
      if (component1 != null)
        component1.velocity = vector3;
      PlortInvulnerability component2 = gameObject.GetComponent<PlortInvulnerability>();
      if (component2 != null)
        component2.GoInvulnerable();
      gameObject.transform.DOScale(gameObject.transform.localScale, 0.5f).From(1f / 1000f);
      if (!immediate && produceFX != null)
        SpawnAndPlayFX(produceFX, position, transform.rotation);
    }
    slimeAudio.Play(slimeAudio.slimeSounds.plortCue);
  }

  public bool WillNowEat(Identifiable.Id id) => Identifiable.IsWater(id) && emotions.GetCurr(SlimeEmotions.Emotion.HUNGER) > (double) slimeEat.minDriveToEat;

  public List<Identifiable.Id> GetProducedIds(Identifiable.Id id, List<Identifiable.Id> produced)
  {
    produced.Clear();
    if (Identifiable.IsWater(id))
      produced.Add(Identifiable.Id.PUDDLE_PLORT);
    return produced;
  }

  public void EatImmediate(
    GameObject target,
    Identifiable.Id id,
    List<Identifiable.Id> produced,
    List<Identifiable.Id> collected,
    bool skipDelays)
  {
    LiquidSource component = target.GetComponent<LiquidSource>();
    component.ConsumeLiquid();
    if (!tooDenseToProducePlort)
      Produce(1, plort, true);
    OnEat(SlimeEmotions.Emotion.HUNGER, component.liquidId);
  }
}
