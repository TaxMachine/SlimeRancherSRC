// Decompiled with JetBrains decompiler
// Type: GordoEat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordoEat : IdHandler, GadgetModel.Participant, GordoModel.Participant
{
  public SlimeDefinition slimeDefinition;
  public int targetCount = 100;
  public GameObject eatFX;
  public SECTR_AudioCue eatCue;
  public GameObject destroyFX;
  public float growthFactor = 1.5f;
  public float vibrationFactor;
  public GameObject slimePrefab;
  public GameObject slimeSpawnFXPrefab;
  public SECTR_AudioCue strainCue;
  public SECTR_AudioCue burstCue;
  public GameObject EatFavoriteFX;
  public Transform toVibrate;
  private List<Identifiable.Id> allEats = new List<Identifiable.Id>();
  protected SnareModel snareModel;
  protected GordoModel gordoModel;
  private GordoRewardsBase rewards;
  private HashSet<GameObject> eating = new HashSet<GameObject>();
  private CellDirector cellDirector;
  private float initScale;
  private float vibrateInitScale;
  protected const float EXPLODE_DELAY = 2f;
  public const int ALREADY_BURST_FLAG = -1;

  public void Awake()
  {
    rewards = GetComponent<GordoRewardsBase>();
    initScale = transform.localScale.x;
    vibrateInitScale = toVibrate.localScale.x;
    cellDirector = GetComponentInParent<CellDirector>();
    if (string.IsNullOrEmpty(id))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterGordo(id, gameObject);
  }

  public void Start()
  {
    allEats.AddRange(slimeDefinition.Diet.GetDietIdentifiableIds());
    int eatenCount = GetEatenCount();
    if (eatenCount == -1 || eatenCount < GetTargetCount())
      return;
    ImmediateReachedTarget();
  }

  public ZoneDirector.Zone GetZoneId() => cellDirector != null ? cellDirector.GetZoneId() : ZoneDirector.Zone.NONE;

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || string.IsNullOrEmpty(id))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterGordo(id);
  }

  public bool HasPopped() => GetEatenCount() == -1;

  public void InitModel(GadgetModel model) => ((SnareModel) model).gordoTargetCount = targetCount;

  public void SetModel(GadgetModel model)
  {
    snareModel = (SnareModel) model;
    if (snareModel.gordoEatenCount != -1)
      return;
    rewards.SetupActiveRewards();
    gameObject.SetActive(false);
  }

  public void InitModel(GordoModel model) => model.targetCount = targetCount;

  public virtual void SetModel(GordoModel model)
  {
    gordoModel = model;
    if (gordoModel.gordoEatenCount != -1)
      return;
    rewards.SetupActiveRewards();
    gameObject.SetActive(false);
  }

  public void OnResetEatenCount() => GetComponent<GordoFaceAnimator>().SetDefaultState();

  public int GetEatenCount()
  {
    if (gordoModel != null)
      return gordoModel.gordoEatenCount;
    return snareModel != null ? snareModel.gordoEatenCount : 0;
  }

  public int GetTargetCount()
  {
    if (gordoModel != null)
      return gordoModel.targetCount;
    return snareModel != null ? snareModel.gordoTargetCount : 0;
  }

  protected void SetEatenCount(int eatenCount)
  {
    if (gordoModel != null)
    {
      gordoModel.gordoEatenCount = eatenCount;
    }
    else
    {
      if (snareModel == null)
        return;
      snareModel.gordoEatenCount = eatenCount;
    }
  }

  public bool CanEat()
  {
    int eatenCount = GetEatenCount();
    return eatenCount != -1 && eatenCount < GetTargetCount();
  }

  public bool MaybeEat(Collider col)
  {
    if (!CanEat())
      return false;
    Identifiable component = col.GetComponent<Identifiable>();
    if (component != null && allEats.Contains(component.id) && !eating.Contains(col.gameObject))
    {
      List<SlimeDiet.EatMapEntry> eatMap = slimeDefinition.Diet.EatMap;
      for (int index = 0; index < eatMap.Count; ++index)
      {
        SlimeDiet.EatMapEntry eatMapEntry = eatMap[index];
        if (eatMapEntry.eats == component.id)
        {
          DoEat(col.gameObject);
          SetEatenCount(GetEatenCount() + eatMapEntry.NumToProduce());
          if (eatMapEntry.isFavorite)
            SpawnAndPlayFX(EatFavoriteFX, col.gameObject.transform.position, col.gameObject.transform.rotation);
          if (GetEatenCount() >= GetTargetCount())
            StartCoroutine(ReachedTarget());
          return true;
        }
      }
      SetEatenCount(gordoModel.gordoEatenCount);
    }
    return false;
  }

  public void Update()
  {
    float num1 = Noise.Noise.PerlinNoise(0.0, 0.0f, Time.time, 0.1f, vibrationFactor, 2f);
    float percentageFed = GetPercentageFed();
    float num2 = Mathf.Lerp(initScale, initScale * growthFactor, percentageFed);
    float num3 = 0.7f;
    transform.localScale = new Vector3(num2, num2, num2);
    float num4 = vibrateInitScale * (percentageFed <= (double) num3 ? 1f : (float) (1.0 + num1 * (percentageFed - (double) num3) / (1.0 - num3)));
    toVibrate.localScale = new Vector3(num4, num4, num4);
  }

  public void LateUpdate() => eating.Clear();

  public float GetPercentageFed()
  {
    int eatenCount = GetEatenCount();
    int targetCount = GetTargetCount();
    return (eatenCount == -1 ? targetCount : (float) eatenCount) / targetCount;
  }

  private void DoEat(GameObject obj)
  {
    if (eatFX != null)
      SpawnAndPlayFX(eatFX, obj.transform.position, obj.transform.localRotation);
    if (eatCue != null)
      SECTR_AudioSystem.Play(eatCue, obj.transform.position, false);
    Destroyer.DestroyActor(obj, "GordoEat.DoEat");
    eating.Add(obj);
  }

  private void ImmediateReachedTarget()
  {
    rewards.GiveRewards();
    gameObject.SetActive(false);
    SetEatenCount(-1);
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.BURST_GORDOS, 1);
    AnalyticsUtil.CustomEvent("GordoBurst", new Dictionary<string, object>()
    {
      {
        "type",
        name
      }
    });
    SRSingleton<SceneContext>.Instance.PediaDirector.MaybeShowPopup(GetPediaId());
    GordoSnare componentInParent = GetComponentInParent<GordoSnare>();
    if (!(componentInParent != null))
      return;
    componentInParent.Destroy();
    Destroyer.Destroy(gameObject, 0.0f, "GordoEat.ImmediateReachedTarget", true);
  }

  protected virtual PediaDirector.Id GetPediaId() => PediaDirector.Id.GORDO_SLIME;

  private IEnumerator ReachedTarget()
  {
    GordoEat gordoEat = this;
    gordoEat.WillStartBurst();
    gordoEat.GetComponent<GordoFaceAnimator>().SetTrigger("Strain");
    SECTR_AudioSystem.Play(gordoEat.strainCue, gordoEat.transform.position, false);
    yield return new WaitForSeconds(2f);
    SECTR_AudioSystem.Play(gordoEat.burstCue, gordoEat.transform.position, false);
    if (gordoEat.destroyFX != null)
    {
      GameObject gameObject = SpawnAndPlayFX(gordoEat.destroyFX, gordoEat.transform.position + Vector3.up * 2f, gordoEat.transform.rotation);
      Identifiable component = gordoEat.gameObject.GetComponent<Identifiable>();
      Color[] colors = SlimeUtil.GetColors(gordoEat.gameObject, component != null ? component.id : Identifiable.Id.NONE, true);
      foreach (RecolorSlimeMaterial componentsInChild in gameObject.GetComponentsInChildren<RecolorSlimeMaterial>())
        componentsInChild.SetColors(colors[0], colors[1], colors[2]);
    }
    gordoEat.DidCompleteBurst();
    gordoEat.ImmediateReachedTarget();
  }

  public bool DropsKey() => rewards.HasKeyReward();

  protected virtual void WillStartBurst()
  {
  }

  protected virtual void DidCompleteBurst()
  {
  }

  public string GetDirectFoodGroupsMsg() => slimeDefinition.Diet.GetDirectFoodGroupsMsg();

  protected override string IdPrefix() => "gordo";
}
