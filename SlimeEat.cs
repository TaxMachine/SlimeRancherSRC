// Decompiled with JetBrains decompiler
// Type: SlimeEat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeEat : CollidableActorBehaviour, Collidable, ActorModel.Participant
{
  public SlimeDefinition slimeDefinition;
  public OnEatDelegate onEat;
  public Chomper.OnChompStartDelegate onStartChomp;
  public OnFinishChompSuccessDelegate onFinishChompSuccess;
  public OnProducePlortsCompleteDelegate onProducePlortsComplete;
  public float chanceToSkipProduce;
  public const float WIND_UP_TIME = 1f;
  public const float WIND_UP_TIME_QUICK = 0.25f;
  public const float DIGEST_TIME = 2f;
  public int damagePerAttack = 20;
  public GameObject EatFX;
  public GameObject EatFavoriteFX;
  public GameObject TransformFX;
  public GameObject ProduceFX;
  [Header("Food Groups")]
  [Tooltip("Types of food to ignore even if in the food groups.")]
  public Identifiable.Id[] foodGroupsExceptions;
  [Tooltip("Standard set of objects produced by anything covered by the food groups.")]
  public Identifiable.Id[] foodGroupsProduceId;
  [Tooltip("Standard object to become when eating anything covered by the food groups.")]
  public Identifiable.Id foodGroupsBecomesId;
  [Tooltip("Standard driver to use for anything covered by the food groups.")]
  public SlimeEmotions.Emotion foodGroupsDriver;
  [Tooltip("Standard extra drive to use for anything covered by the food groups.")]
  public float foodGroupsExtraDrive;
  [Tooltip("Standard minimum drive to use for anything covered by the food groups.")]
  public float foodGroupsMinDrive;
  [Space(10f)]
  public float minDriveToEat = 0.333f;
  public float drivePerEat = 0.333f;
  public float agitationPerEat = 0.15f;
  public float agitationPerFavEat = 0.3f;
  private Dictionary<Identifiable.Id, DriveCalculator> allEats = new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer);
  private SlimeEmotions emotions;
  private bool isEatingEnabled = true;
  private SlimeAudio slimeAudio;
  private RegionMember regionMember;
  private Chomper chomper;
  private SlimeFaceAnimator faceAnim;
  private Animator bodyAnim;
  private TentacleGrapple tentacleGrapple;
  private LookupDirector lookupDir;
  private ModDirector modDir;
  private static HashSet<GameObject> claimedFood = new HashSet<GameObject>();
  private static readonly Vector3 LOCAL_PRODUCE_LOC = new Vector3(0.0f, 0.5f, 0.0f);
  private static readonly Vector3 LOCAL_PRODUCE_VEL = new Vector3(0.0f, 1f, 0.0f);
  private const float TRANSFORM_SCALE_UP_TIME = 0.5f;
  private const float PRODUCE_SCALE_UP_TIME = 0.5f;
  private const float FERAL_EXTRA_DRIVE = 0.0f;
  private const float HONEY_PLORT_EXTRA_DRIVE = 0.5f;
  private static Dictionary<FoodGroup, Identifiable.Id[]> foodGroupIds = new Dictionary<FoodGroup, Identifiable.Id[]>();
  private int animDigestingId;
  private SlimeModel slimeModel;
  private SlimeAppearanceApplicator appearanceApplicator;
  private static LRUCache<int, Identifiable> recentIds = new LRUCache<int, Identifiable>(200);

  public static void ClearClaimedFood() => claimedFood.Clear();

  public List<SlimeDiet.EatMapEntry> GetEatMapById(Identifiable.Id id)
  {
    List<SlimeDiet.EatMapEntry> targetEntries = new List<SlimeDiet.EatMapEntry>();
    slimeDefinition.Diet.AddEatMapEntries(id, targetEntries);
    return targetEntries;
  }

  static SlimeEat()
  {
    foodGroupIds[FoodGroup.VEGGIES] = new List<Identifiable.Id>(Identifiable.VEGGIE_CLASS).ToArray();
    foodGroupIds[FoodGroup.FRUIT] = new List<Identifiable.Id>(Identifiable.FRUIT_CLASS).ToArray();
    foodGroupIds[FoodGroup.MEAT] = new List<Identifiable.Id>(Identifiable.MEAT_CLASS).ToArray();
    List<Identifiable.Id> idList1 = new List<Identifiable.Id>();
    foreach (Identifiable.Id id in Enum.GetValues(typeof (Identifiable.Id)))
    {
      if (Identifiable.IsSlime(id) && !Identifiable.IsTarr(id) && id != Identifiable.Id.GOLD_SLIME && id != Identifiable.Id.LUCKY_SLIME)
        idList1.Add(id);
    }
    foodGroupIds[FoodGroup.NONTARRGOLD_SLIMES] = idList1.ToArray();
    List<Identifiable.Id> idList2 = new List<Identifiable.Id>();
    foreach (Identifiable.Id id in Enum.GetValues(typeof (Identifiable.Id)))
    {
      if (Identifiable.IsPlort(id) && id != Identifiable.Id.PUDDLE_PLORT && id != Identifiable.Id.GOLD_PLORT && id != Identifiable.Id.FIRE_PLORT)
        idList2.Add(id);
    }
    foodGroupIds[FoodGroup.PLORTS] = idList2.ToArray();
    foodGroupIds[FoodGroup.GINGER] = new Identifiable.Id[1]
    {
      Identifiable.Id.GINGER_VEGGIE
    };
  }

  public static Identifiable.Id[] GetFoodGroupIds(FoodGroup group) => foodGroupIds[group];

  public override void Awake()
  {
    base.Awake();
    chomper = GetComponent<Chomper>();
    slimeAudio = GetComponent<SlimeAudio>();
    faceAnim = GetComponent<SlimeFaceAnimator>();
    regionMember = GetComponent<RegionMember>();
    bodyAnim = GetComponentInChildren<Animator>();
    emotions = GetComponent<SlimeEmotions>();
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    modDir = SRSingleton<SceneContext>.Instance.ModDirector;
    tentacleGrapple = GetComponent<TentacleGrapple>();
    animDigestingId = Animator.StringToHash("Digesting");
    appearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
  }

  public void InitModel(ActorModel actorModel)
  {
  }

  public void SetModel(ActorModel actorModel)
  {
    slimeModel = actorModel as SlimeModel;
    InitFood();
  }

  public void InitFood() => CalculateAllEats();

  private void CalculateAllEats()
  {
    List<SlimeDiet.EatMapEntry> eatMap = slimeDefinition.Diet.EatMap;
    allEats = new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer);
    PlayerState playerState = SRSingleton<SceneContext>.Instance == null ? null : SRSingleton<SceneContext>.Instance.PlayerState;
    for (int index = 0; index < eatMap.Count; ++index)
    {
      if (!(playerState != null) || !SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().preventHostiles || !Identifiable.IsTarr(eatMap[index].becomesId))
      {
        float num = eatMap[index].eats == Identifiable.Id.HONEY_PLORT ? 0.5f : 0.0f;
        allEats[eatMap[index].eats] = new DriveCalculator(eatMap[index].driver, eatMap[index].extraDrive + num, eatMap[index].minDrive);
      }
    }
  }

  public override void Start()
  {
    base.Start();
    ResetEatClock();
    if (Identifiable.IsTarr(Identifiable.GetId(gameObject)) || !regionMember.IsInRegion(RegionRegistry.RegionSetId.SLIMULATIONS))
      return;
    isEatingEnabled = false;
  }

  public void ProcessCollisionEnter(Collision col) => MaybeSpinAndChomp(col.gameObject, false);

  public void ProcessCollisionExit(Collision col)
  {
  }

  public bool MaybeSpinAndChomp(GameObject obj, bool ignoreEmotions)
  {
    if (isEatingEnabled && chomper.CanChomp())
    {
      Identifiable.Id otherId = ExtractOtherId(obj);
      if (allEats.ContainsKey(otherId) && Identifiable.IsEdible(obj) && (ignoreEmotions || slimeModel.isFeral || emotions != null && allEats[otherId].Drive(emotions, otherId) >= (double) minDriveToEat) && (tentacleGrapple == null || tentacleGrapple.IsGrappling(obj)))
      {
        transform.LookAt(obj.transform);
        chomper.StartChomp(obj, otherId, false, true, onStartChomp, FinishChomp);
        return true;
      }
    }
    return false;
  }

  public bool MaybeChomp(GameObject obj)
  {
    if (isEatingEnabled && chomper.CanChomp())
    {
      Identifiable.Id otherId = ExtractOtherId(obj);
      if ((!allEats.ContainsKey(otherId) || !Identifiable.IsEdible(obj) ? 0 : (slimeModel.isFeral ? 1 : (!(emotions != null) ? 0 : (allEats[otherId].Drive(emotions, otherId) >= (double) minDriveToEat ? 1 : 0)))) != 0)
      {
        chomper.StartChomp(obj, otherId, false, false, onStartChomp, FinishChomp);
        return true;
      }
    }
    return false;
  }

  public void CancelChomp(GameObject obj) => chomper.CancelChomp(obj);

  private Identifiable.Id ExtractOtherId(GameObject other)
  {
    int instanceId = other.GetInstanceID();
    Identifiable.Id otherId;
    if (recentIds.contains(instanceId))
    {
      Identifiable identifiable = recentIds.get(instanceId);
      otherId = identifiable == null ? Identifiable.Id.NONE : identifiable.id;
    }
    else
    {
      Identifiable component = other.GetComponent<Identifiable>();
      recentIds.put(instanceId, component);
      otherId = component == null ? Identifiable.Id.NONE : component.id;
    }
    return otherId;
  }

  private void FinishChomp(
    GameObject chomping,
    Identifiable.Id chompingId,
    bool whileHeld,
    bool wasLaunched)
  {
    GameObject gameObject = chomping;
    Identifiable.Id otherId = chompingId;
    slimeAudio.Play(slimeAudio.slimeSounds.chompCue);
    if (gameObject == null || claimedFood.Contains(gameObject))
      return;
    claimedFood.Add(gameObject);
    faceAnim.SetTrigger("triggerChompClosed");
    for (int index = 0; index < slimeDefinition.Diet.EatMap.Count; ++index)
    {
      SlimeDiet.EatMapEntry eat = slimeDefinition.Diet.EatMap[index];
      if (eat.eats == otherId)
      {
        if (eat.producesId != Identifiable.Id.NONE)
          EatAndProduce(gameObject, eat, false, false, false);
        else if (eat.becomesId != Identifiable.Id.NONE)
          EatAndTransform(gameObject, eat, false);
        else
          DoDamage(gameObject, false);
        OnEat(eat.driver, otherId, wasLaunched, eat.isFavorite);
      }
    }
    if (onFinishChompSuccess == null)
      return;
    onFinishChompSuccess(gameObject);
  }

  private void EatAndTransform(GameObject target, SlimeDiet.EatMapEntry em, bool immediateMode)
  {
    if (!immediateMode)
      SpawnAndPlayFX(TransformFX, transform.position, transform.rotation);
    if (!DoDamage(target, immediateMode))
      return;
    SlimeEmotions component1 = GetComponent<SlimeEmotions>();
    Destroyer.DestroyActor(this.gameObject, "SlimeEat.EatAndTransform");
    GameObject gameObject = InstantiateActor(lookupDir.GetPrefab(em.becomesId), regionMember.setId, transform.position, transform.rotation);
    SlimeEmotions component2 = gameObject.GetComponent<SlimeEmotions>();
    if (component2 != null)
      component2.SetAll(component1);
    gameObject.transform.DOScale(gameObject.transform.localScale, 0.5f).From(this.gameObject.transform.localScale).SetEase(Ease.OutElastic);
    gameObject.GetComponent<OnTransformed>()?.OnTransformed();
  }

  private void EatAndProduce(
    GameObject target,
    SlimeDiet.EatMapEntry em,
    bool immediateMode,
    bool skipDelays,
    bool skipProduction)
  {
    bodyAnim.SetBool(animDigestingId, true);
    if (!immediateMode)
    {
      if (em.isFavorite)
        SpawnAndPlayFX(EatFavoriteFX, target.transform.position, target.transform.rotation);
      else
        SpawnAndPlayFX(EatFX, target.transform.position, target.transform.rotation);
    }
    if (!DoDamage(target, immediateMode))
      return;
    float delay = 2f;
    int count = em.NumToProduce();
    if (immediateMode)
    {
      delay = 0.0f;
      count = 1;
    }
    if (target != null)
      Destroyer.DestroyActor(target, "SlimeEat.EatAndProduce");
    if (!skipProduction && (chanceToSkipProduce <= 0.0 || !Randoms.SHARED.GetProbability(chanceToSkipProduce)))
    {
      GameObject prefab = lookupDir.GetPrefab(em.producesId);
      if (Randoms.SHARED.GetProbability(modDir.ChanceRandomPlort()) && Identifiable.IsPlort(em.producesId))
        prefab = lookupDir.GetPrefab(Randoms.SHARED.Pick(Identifiable.PLORT_CLASS, Identifiable.Id.NONE));
      if (em.producesId == Identifiable.Id.GOLD_PLORT && count >= 3)
        SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.GOLD_SLIME_TRIPLE_PLORT, 1);
      if (skipDelays)
        Produce(count, prefab);
      else
        StartCoroutine(ProduceAfterDelay(count, prefab, delay));
    }
    else if (skipDelays)
      Digest();
    else
      StartCoroutine(DigestOnlyAfterDelay(delay));
  }

  public void EatImmediate(
    GameObject target,
    Identifiable.Id eatId,
    List<Identifiable.Id> produceIds,
    List<Identifiable.Id> alreadyCollectedIds,
    bool skipDelays)
  {
    if (target != null)
    {
      if (claimedFood.Contains(target))
        return;
      claimedFood.Add(target);
    }
    List<SlimeDiet.EatMapEntry> eatMap = slimeDefinition.Diet.EatMap;
    SlimeDiet.EatMapEntry em1 = null;
    foreach (Identifiable.Id produceId in produceIds)
    {
      for (int index = 0; index < eatMap.Count; ++index)
      {
        if (eatMap[index].eats == eatId && eatMap[index].producesId == produceId)
        {
          em1 = eatMap[index];
          bool skipProduction = alreadyCollectedIds.Remove(produceId);
          EatAndProduce(target, em1, true, skipDelays, skipProduction);
        }
      }
    }
    if (em1 != null)
      OnEat(em1.driver, eatId, false, em1.isFavorite);
    SlimeDiet.EatMapEntry em2 = null;
    for (int index = 0; index < eatMap.Count; ++index)
    {
      if (eatMap[index].eats == eatId && eatMap[index].becomesId != Identifiable.Id.NONE)
        em2 = eatMap[index];
    }
    if (em2 == null)
      return;
    EatAndTransform(target, em2, true);
    OnEat(em2.driver, eatId, false, em2.isFavorite);
  }

  public List<Identifiable.Id> GetProducedIds(
    Identifiable.Id foodId,
    List<Identifiable.Id> producedIdList)
  {
    List<SlimeDiet.EatMapEntry> eatMap = slimeDefinition.Diet.EatMap;
    producedIdList.Clear();
    for (int index = 0; index < eatMap.Count; ++index)
    {
      if (eatMap[index].eats == foodId && eatMap[index].producesId != Identifiable.Id.NONE)
      {
        producedIdList.Add(eatMap[index].producesId);
        if (eatMap[index].isFavorite)
          producedIdList.Add(eatMap[index].producesId);
      }
    }
    return producedIdList;
  }

  private IEnumerator DigestOnlyAfterDelay(float delay)
  {
    SlimeEat slimeEat = this;
    yield return new WaitForSeconds(delay);
    if (slimeEat.gameObject != null)
      slimeEat.Digest();
  }

  private void Digest() => bodyAnim.SetBool(animDigestingId, false);

  private IEnumerator ProduceAfterDelay(int count, GameObject produces, float delay)
  {
    SlimeEat slimeEat = this;
    yield return new WaitForSeconds(delay);
    if (slimeEat.gameObject != null)
      slimeEat.Produce(count, produces);
  }

  private void Produce(int count, GameObject produces)
  {
    for (int index = 0; index < count; ++index)
    {
      Vector3 position = transform.TransformPoint(LOCAL_PRODUCE_LOC);
      Vector3 vector3 = transform.TransformVector(LOCAL_PRODUCE_VEL);
      if (ProduceFX != null)
      {
        RecolorSlimeMaterial[] componentsInChildren = SpawnAndPlayFX(ProduceFX, position, transform.rotation).GetComponentsInChildren<RecolorSlimeMaterial>();
        if (componentsInChildren != null && componentsInChildren.Length != 0)
        {
          SlimeAppearance.Palette appearancePalette = appearanceApplicator.GetAppearancePalette();
          foreach (RecolorSlimeMaterial recolorSlimeMaterial in componentsInChildren)
            recolorSlimeMaterial.SetColors(appearancePalette.Top, appearancePalette.Middle, appearancePalette.Bottom);
        }
      }
      GameObject gameObject = InstantiateActor(produces, regionMember.setId, position, transform.rotation);
      Rigidbody component1 = gameObject.GetComponent<Rigidbody>();
      if (component1 != null)
        component1.velocity = vector3;
      PlortInvulnerability component2 = gameObject.GetComponent<PlortInvulnerability>();
      if (component2 != null)
        component2.GoInvulnerable();
      gameObject.transform.DOScale(gameObject.transform.localScale, 0.5f).From(new Vector3(0.01f, 0.01f, 0.01f)).SetEase(Ease.Linear);
    }
    slimeAudio.Play(slimeAudio.slimeSounds.plortCue);
    bodyAnim.SetBool(animDigestingId, false);
    if (onProducePlortsComplete == null)
      return;
    onProducePlortsComplete();
  }

  private void OnEat(
    SlimeEmotions.Emotion driver,
    Identifiable.Id otherId,
    bool eatingLaunchedFood,
    bool isFavorite)
  {
    ResetEatClock();
    emotions.Adjust(driver, -drivePerEat);
    emotions.Adjust(SlimeEmotions.Emotion.AGITATION, isFavorite ? -agitationPerFavEat : -agitationPerEat);
    if (onEat != null)
      onEat(otherId);
    if (Identifiable.IsAnimal(otherId) && CellDirector.IsOnRanch(regionMember))
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.CHICKENS_FED_SLIMES, 1);
    if (isFavorite && CellDirector.IsOnRanch(regionMember))
      SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.FED_FAVORITE, 1);
    if (!eatingLaunchedFood)
      return;
    SlimeSubbehaviourPlexer component = GetComponent<SlimeSubbehaviourPlexer>();
    if (!(component != null) || component.IsGrounded())
      return;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.FED_AIRBORNE, 1);
  }

  public bool IsChomping() => chomper.IsChomping();

  public void ResetEatClock() => chomper.ResetEatClock();

  public Dictionary<Identifiable.Id, DriveCalculator> GetAllEats() => new Dictionary<Identifiable.Id, DriveCalculator>(allEats, Identifiable.idComparer);

  public bool DoesEat(GameObject gameObject) => DoesEat(ExtractOtherId(gameObject)) && Identifiable.IsEdible(gameObject);

  public bool DoesEat(Identifiable.Id id) => allEats.ContainsKey(id);

  public bool WillNowEat(Identifiable.Id id)
  {
    if (!allEats.ContainsKey(id))
      return false;
    if (slimeModel.isFeral)
      return true;
    return emotions != null && allEats[id].Drive(emotions, id) > (double) minDriveToEat;
  }

  public bool WantsToEat()
  {
    if (slimeModel.isFeral)
      return true;
    return !(emotions == null) && allEats.Count >= 1 && emotions.GetCurr(SlimeEmotions.Emotion.HUNGER) > (double) minDriveToEat;
  }

  private bool DoDamage(GameObject other, bool immediateMode)
  {
    if (other == null)
      return true;
    if (!immediateMode)
      slimeAudio.Play(slimeAudio.slimeSounds.gulpCue);
    Damageable interfaceComponent = other.GetInterfaceComponent<Damageable>();
    if (interfaceComponent == null)
    {
      if (!immediateMode)
        PlayOnDeathAudio(other);
      Destroyer.DestroyActor(other, "SlimeEat.DoDamage#1");
      return true;
    }
    if (!interfaceComponent.Damage(damagePerAttack, gameObject))
      return false;
    DeathHandler.Kill(other, DeathHandler.Source.SLIME_ATTACK, gameObject, "SlimeEat.DoDamage#2");
    if (!immediateMode)
      PlayOnDeathAudio(other);
    return true;
  }

  private void PlayOnDeathAudio(GameObject other)
  {
    SlimeAudio componentInChildren = other.GetComponentInChildren<SlimeAudio>();
    if (!(componentInChildren != null) || !(componentInChildren.slimeSounds.voiceDamageCue != null))
      return;
    SECTR_AudioSystem.Play(componentInChildren.slimeSounds.voiceDamageCue, other.transform.position, false);
  }

  public IEnumerable<Identifiable.Id> GetProducedIds() => slimeDefinition.Diet.Produces.AsEnumerable();

  public delegate void OnEatDelegate(Identifiable.Id id);

  public delegate void OnFinishChompSuccessDelegate(GameObject gameObject);

  public delegate void OnProducePlortsCompleteDelegate();

  public enum FoodGroup
  {
    FRUIT,
    VEGGIES,
    MEAT,
    NONTARRGOLD_SLIMES,
    PLORTS,
    GINGER,
  }
}
