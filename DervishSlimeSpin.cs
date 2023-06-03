// Decompiled with JetBrains decompiler
// Type: DervishSlimeSpin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DervishSlimeSpin : SlimeHover, LiquidConsumer
{
  [Tooltip("The mini-vortex we attach under ourselves")]
  public GameObject tornadoPrefab;
  [Tooltip("The mini-vortex we attach under ourselves")]
  public GameObject tornadoLargoPrefab;
  [Tooltip("The full self-moving whirlwind we spawn only when agitated")]
  public GameObject fullWhirlwindPrefab;
  private const float STD_HOVER_HEIGHT = 5f;
  private const float INV_STD_HOVER_HEIGHT = 0.2f;
  private const float LARGO_HOVER_HEIGHT = 9f;
  private const float INV_LARGO_HOVER_HEIGHT = 0.111111112f;
  private const float WHIRLWIND_CUTOFF = 0.95f;
  private const int MAX_WHIRLWINDS = 6;
  private GameObject tornado;
  private TotemLinker totemLinker;
  private CalmedByWaterSpray calmer;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private bool isLargo;
  private static Queue<GameObject> whirlwinds = new Queue<GameObject>();

  public override void Start()
  {
    base.Start();
    totemLinker = GetComponentInChildren<TotemLinker>();
    calmer = GetComponent<CalmedByWaterSpray>();
    isLargo = GetComponent<Vacuumable>().size != 0;
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateTornadoAppearance;
    if (!(slimeAppearanceApplicator.Appearance != null))
      return;
    UpdateTornadoAppearance(slimeAppearanceApplicator.Appearance);
  }

  public override float Relevancy(bool isGrounded) => calmer.IsCalmed() ? 0.0f : base.Relevancy(isGrounded);

  public override void Selected()
  {
    base.Selected();
    if (tornado == null)
      tornado = SpawnTornado();
    if (totemLinker != null)
      totemLinker.DisableToteming();
    if (emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) < 0.949999988079071)
      return;
    Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles with
    {
      x = 0.0f,
      z = 0.0f
    });
    while (whirlwinds.Count > 0 && whirlwinds.Peek() == null)
      whirlwinds.Dequeue();
    if (whirlwinds.Count > 6)
      whirlwinds.Dequeue().GetComponent<DestroyAfterTime>().SetDeathTime(0.0);
    GameObject gameObject = InstantiateDynamic(fullWhirlwindPrefab, transform.position, rotation);
    gameObject.GetComponent<DestroyAfterTime>().FizzleParticlesOnDestroy();
    whirlwinds.Enqueue(gameObject);
  }

  public override void Deselected()
  {
    base.Deselected();
    if (tornado != null)
      Destroyer.Destroy(tornado, "DervishSlimeSpin.Deselected");
    if (!(totemLinker != null))
      return;
    totemLinker.EnableToteming();
  }

  protected override float GetHoverAccel() => 600f;

  protected override float GetHoverHeight() => !isLargo ? 5f : 9f;

  protected override float GetInvHoverHeight() => !isLargo ? 0.2f : 0.111111112f;

  private void UpdateTornadoAppearance(SlimeAppearance appearance)
  {
    tornadoPrefab = appearance.TornadoAppearance.tornadoPrefab;
    tornadoLargoPrefab = appearance.TornadoAppearance.largoTornadoPrefab;
    fullWhirlwindPrefab = appearance.TornadoAppearance.fullWhirlwindPrefab;
  }

  private GameObject SpawnTornado()
  {
    GameObject gameObject = InstantiateDynamic(isLargo ? tornadoLargoPrefab : tornadoPrefab, transform.position, Quaternion.identity);
    gameObject.GetComponent<KeepAlignedUnderActor>().AlignWith(transform);
    return gameObject;
  }

  public void AddLiquid(Identifiable.Id liquidId, float units)
  {
    if (!Identifiable.IsWater(liquidId))
      return;
    plexer.ForceRethink();
  }
}
