// Decompiled with JetBrains decompiler
// Type: ScorePlort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ScorePlort : SRBehaviour, VacShootAccelerator
{
  public GameObject ExplosionFX;
  private PlayerState player;
  private SECTR_AudioSource scoreAudio;
  private EconomyDirector econDir;
  private TutorialDirector tutDir;
  private ModDirector modDir;
  private AchievementsDirector achieveDir;
  private int soldCount;
  private float lastUpdateTime;
  private ProgressDirector progressDir;
  private DroneNetwork droneNetwork;
  private VacAccelerationHelper accelerationInput = VacAccelerationHelper.CreateInput();
  private const float MOCHI_CHANCE = 0.05f;
  private const float MOCHI_FACTOR = 2f;
  private const float TIME_BETWEEN_UPDATES = 300f;

  public void Awake()
  {
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    econDir = SRSingleton<SceneContext>.Instance.EconomyDirector;
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    modDir = SRSingleton<SceneContext>.Instance.ModDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    scoreAudio = GetRequiredComponent<SECTR_AudioSource>();
    droneNetwork = GetComponentInParent<DroneNetwork>();
    if (!(droneNetwork != null))
      return;
    droneNetwork.Register(this);
  }

  public void OnDestroy()
  {
    UpdateSoldPlorts(true);
    if (!(droneNetwork != null))
      return;
    droneNetwork.Deregister(this);
    droneNetwork = null;
  }

  public bool CanDeposit(Identifiable.Id id, bool ignoreMarketShutdown = false) => GetMarketValue(id, ignoreMarketShutdown).HasValue;

  public Deposit_Response Deposit(
    Identifiable.Id id,
    int count = 1,
    PlayerState.CoinsType? coinsTypeOverride = null,
    bool ignoreMarketShutdown = false)
  {
    if (count <= 0)
      return new Deposit_Response();
    int? nullable1 = GetMarketValue(id, ignoreMarketShutdown);
    if (!nullable1.HasValue)
      return new Deposit_Response();
    Deposit_Response depositResponse = new Deposit_Response()
    {
      deposits = count
    };
    PlayerState.CoinsType? nullable2 = coinsTypeOverride;
    PlayerState.CoinsType coinsType = nullable2 ?? 0;
    nullable1 = new int?(Mathf.RoundToInt(nullable1.Value * modDir.PlortPriceFactor(id)));
    for (int index = 0; index < count; ++index)
    {
      if (progressDir.GetProgress(ProgressDirector.ProgressType.MOCHI_REWARDS) >= 1 && Randoms.SHARED.GetProbability(0.05f))
      {
        nullable2 = coinsTypeOverride;
        if (nullable2 != null) coinsType = (PlayerState.CoinsType)nullable2;
        depositResponse.currency += Mathf.RoundToInt(nullable1.Value * 2f);
      }
      else
        depositResponse.currency += nullable1.Value;
    }
    player.AddCurrency(depositResponse.currency, coinsType);
    achieveDir.AddToStat(AchievementsDirector.IntStat.PLORTS_SOLD, count);
    achieveDir.AddToStat(AchievementsDirector.GameIdDictStat.PLORT_TYPES_SOLD, id, count);
    scoreAudio.Play();
    econDir.RegisterSold(id, count);
    tutDir.OnPlortSold();
    soldCount += count;
    accelerationInput.OnTriggered();
    return depositResponse;
  }

  private void OnTriggerEnter(Collider col)
  {
    if (Deposit(Identifiable.GetId(col.gameObject)))
    {
      SpawnAndPlayFX(ExplosionFX, col.gameObject.transform.position, col.gameObject.transform.rotation);
      Destroyer.DestroyActor(col.gameObject, "ScorePlort.OnTriggerEnter");
      DestroyOnTouching component = col.gameObject.GetComponent<DestroyOnTouching>();
      if (!(component != null))
        return;
      component.NoteDestroying();
    }
  }

  public void Update()
  {
    if (lastUpdateTime + 300.0 >= Time.time)
      return;
    UpdateSoldPlorts(false);
    lastUpdateTime = Time.time;
  }

  private void UpdateSoldPlorts(bool isDestroyed)
  {
    if (soldCount <= 0)
      return;
    AnalyticsUtil.CustomEvent("PlortsSold", new Dictionary<string, object>()
    {
      {
        "count",
        soldCount
      }
    }, (!isDestroyed ? 1 : 0) != 0);
    soldCount = 0;
  }

  public float GetVacShootSpeedFactor() => accelerationInput.Factor;

  private int? GetMarketValue(Identifiable.Id id, bool ignoreMarketShutdown) => !ignoreMarketShutdown && econDir.IsMarketShutdown() ? new int?() : econDir.GetCurrValue(id);

  public class Deposit_Response
  {
    public int deposits;
    public int currency;

    public static bool operator true(Deposit_Response response) => response.deposits > 0;

    public static bool operator false(Deposit_Response response) => response.deposits <= 0;
  }
}
