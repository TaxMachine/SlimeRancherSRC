// Decompiled with JetBrains decompiler
// Type: TreasurePod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePod : IdHandler, TreasurePodModel.Participant
{
  public bool needsUpgrade = true;
  public PlayerState.Upgrade requiredUpgrade;
  public SECTR_AudioCue openCue;
  [Tooltip("The FX that will be played as soon as the treasure pod is unlocked.")]
  public GameObject openFX;
  [Tooltip("The FX that will be played after the treasure pod opening animation has finished.")]
  public GameObject afterOpenFX;
  public Gadget.Id blueprint;
  public GameObject[] spawnObjs;
  public SECTR_AudioCue spawnObjCue;
  [Tooltip("The SlimeDefinition for the appearance that will be unlocked.")]
  public SlimeDefinition unlockedSlimeAppearanceDefinition;
  [Tooltip("The SlimeAppearance that will be unlocked.")]
  public SlimeAppearance unlockedSlimeAppearance;
  [Tooltip("Coins prefab awarded by the treasure pod. (TIME_LIMIT_V2)")]
  public GameObject coins;
  [Tooltip("FX played when coins are awarded. (TIME_LIMIT_V2, optional)")]
  public GameObject onCoinsFX;
  [Tooltip("SFX played when coins are awarded. (TIME_LIMIT_V2, optional)")]
  public SECTR_AudioCue onCoinsCue;
  private CellDirector cellDirector;
  private bool nextUpdateImmediate;
  private bool forceUpdate;
  private int animOpenId;
  private int animOpenImmediateId;
  private PlayerState playerState;
  private GadgetDirector gadgetDir;
  private SlimeAppearanceDirector slimeAppearanceDirector;
  private TreasurePodModel model;
  public static float ITEM_GAP_DELAY = 0.8f;
  public static float OPEN_DELAY = 3.3f;
  private static Vector3 EJECT_VEL = new Vector3(0.0f, 5f, 2.5f);
  private static Vector3 EJECT_OFF = new Vector3(0.0f, 1.75f, 1f);

  public void Awake()
  {
    cellDirector = GetComponentInParent<CellDirector>();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPod(id, gameObject);
    animOpenId = Animator.StringToHash("Open");
    animOpenImmediateId = Animator.StringToHash("OpenImmediate");
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
    slimeAppearanceDirector = SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector;
  }

  public void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterPod(id);
  }

  public void OnEnable()
  {
    forceUpdate = true;
    nextUpdateImmediate = true;
    StartCoroutine(OnEnableCoroutine());
  }

  private IEnumerator OnEnableCoroutine()
  {
    TreasurePod treasurePod = this;
    yield return new WaitForSeconds(OPEN_DELAY);
    yield return treasurePod.StartCoroutine(treasurePod.SpawnQueuedPrizeObjs());
  }

  public void InitModel(TreasurePodModel model)
  {
  }

  public void SetModel(TreasurePodModel model)
  {
    this.model = model;
    UpdateImmediate(model.state);
  }

  public State CurrState
  {
    get => model.state;
    set
    {
      model.state = value;
      forceUpdate = true;
    }
  }

  public ZoneDirector.Zone GetZoneId() => cellDirector != null ? cellDirector.GetZoneId() : ZoneDirector.Zone.NONE;

  private void UpdateImmediate(State state)
  {
    nextUpdateImmediate = true;
    CurrState = state;
  }

  public void Update()
  {
    if (!forceUpdate)
      return;
    forceUpdate = false;
    if (nextUpdateImmediate)
    {
      if (model.state == State.OPEN && model.spawnQueue.Count == 0)
        GetComponentInChildren<Animator>().SetTrigger(animOpenImmediateId);
      nextUpdateImmediate = false;
    }
    GetComponentInChildren<Animator>().SetBool(animOpenId, model.state == State.OPEN);
  }

  protected override string IdPrefix() => "pod";

  public bool HasKey() => !needsUpgrade || playerState.HasUpgrade(requiredUpgrade);

  public bool HasAnyKey() => playerState.HasUpgrade(PlayerState.Upgrade.TREASURE_CRACKER_1) || playerState.HasUpgrade(PlayerState.Upgrade.TREASURE_CRACKER_2) || playerState.HasUpgrade(PlayerState.Upgrade.TREASURE_CRACKER_3) || playerState.HasUpgrade(PlayerState.Upgrade.TREASURE_CRACKER_4);

  public void Activate()
  {
    if (!HasKey())
      return;
    CurrState = State.OPEN;
    if (openCue != null)
      SECTR_AudioSystem.Play(openCue, transform.position, false);
    if (openFX != null)
      InstantiateDynamic(openFX, transform.position, transform.rotation);
    StartCoroutine(SRSingleton<SceneContext>.Instance.GameModel.currGameMode == PlayerState.GameMode.TIME_LIMIT_V2 ? AwardPrizesRushMode() : AwardPrizesDefault());
    AnalyticsUtil.CustomEvent("PodOpened", new Dictionary<string, object>()
    {
      {
        "id",
        id
      }
    });
  }

  private IEnumerator AwardPrizesDefault()
  {
    TreasurePod treasurePod = this;
    if (treasurePod.blueprint != Gadget.Id.NONE)
      treasurePod.gadgetDir.AddBlueprint(treasurePod.blueprint);
    if (treasurePod.unlockedSlimeAppearance != null)
      treasurePod.slimeAppearanceDirector.UnlockAppearance(treasurePod.unlockedSlimeAppearanceDefinition, treasurePod.unlockedSlimeAppearance);
    if (treasurePod.spawnObjs != null && treasurePod.spawnObjs.Length != 0)
    {
      foreach (GameObject spawnObj in treasurePod.spawnObjs)
      {
        Identifiable.Id id = Identifiable.GetId(spawnObj);
        treasurePod.model.spawnQueue.Enqueue(id);
      }
    }
    yield return new WaitForSeconds(OPEN_DELAY);
    if (treasurePod.afterOpenFX != null)
      InstantiateDynamic(treasurePod.afterOpenFX, treasurePod.transform.position, treasurePod.transform.rotation);
    if (treasurePod.blueprint != Gadget.Id.NONE)
    {
      SRSingleton<SceneContext>.Instance.PopupDirector.QueueForPopup(new BlueprintPopupCreator(treasurePod.blueprint));
      SRSingleton<SceneContext>.Instance.PopupDirector.MaybePopupNext();
      yield return new WaitForSeconds(ITEM_GAP_DELAY);
    }
    if (treasurePod.unlockedSlimeAppearance != null)
    {
      treasurePod.slimeAppearanceDirector.UpdateChosenSlimeAppearance(treasurePod.unlockedSlimeAppearanceDefinition, treasurePod.unlockedSlimeAppearance);
      treasurePod.unlockedSlimeAppearance.MaybeShowPopupUI();
    }
    yield return treasurePod.StartCoroutine(treasurePod.SpawnQueuedPrizeObjs());
  }

  private IEnumerator AwardPrizesRushMode()
  {
    TreasurePod treasurePod = this;
    yield return new WaitForSeconds(OPEN_DELAY);
    if (treasurePod.onCoinsFX != null)
      InstantiateDynamic(treasurePod.onCoinsFX).transform.position = treasurePod.transform.TransformPoint(EJECT_OFF);
    SECTR_AudioSystem.Play(treasurePod.onCoinsCue, treasurePod.transform.position, false);
    InstantiateDynamic(treasurePod.coins, treasurePod.transform.position, Quaternion.identity);
  }

  private IEnumerator SpawnQueuedPrizeObjs()
  {
    TreasurePod treasurePod = this;
    yield return new WaitForSeconds(ITEM_GAP_DELAY);
    Vector3 ejectPos = treasurePod.transform.TransformPoint(EJECT_OFF);
    while (treasurePod.model.spawnQueue.Count > 0)
    {
      Rigidbody component = InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(treasurePod.model.spawnQueue.Dequeue()), treasurePod.cellDirector.region.setId, ejectPos, treasurePod.transform.rotation).GetComponent<Rigidbody>();
      if (component != null)
        component.AddForce(treasurePod.transform.TransformDirection(EJECT_VEL) + Randoms.SHARED.GetInRange(-0.1f, 0.1f) * treasurePod.transform.right, ForceMode.VelocityChange);
      if (treasurePod.spawnObjCue != null)
        SECTR_AudioSystem.Play(treasurePod.spawnObjCue, ejectPos, false);
      yield return new WaitForSeconds(ITEM_GAP_DELAY);
    }
  }

  public enum State
  {
    LOCKED,
    OPEN,
  }

  private class BlueprintPopupCreator : PopupDirector.PopupCreator
  {
    private Gadget.Id id;

    public BlueprintPopupCreator(Gadget.Id id) => this.id = id;

    public override void Create() => BlueprintPopupUI.CreateBlueprintPopup(SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(id));

    public override bool Equals(object other) => other is BlueprintPopupCreator && ((BlueprintPopupCreator) other).id == id;

    public override int GetHashCode() => id.GetHashCode();

    public override bool ShouldClear() => false;
  }
}
