// Decompiled with JetBrains decompiler
// Type: GoldSlimeProducePlorts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

public class GoldSlimeProducePlorts : CollidableActorBehaviour, Collidable, ActorModel.Participant
{
  public GameObject plortPrefab;
  public GameObject plortFX;
  private GoldSlimeFlee flee;
  private RegionMember regionMember;
  private HashSet<GameObject> colliders = new HashSet<GameObject>();
  private SlimeAudio slimeAudio;
  private int plortsProduced;
  private PlayerState playerState;
  private SlimeModel model;
  private const float PLORT_THRESHOLD = 0.02f;
  private const float JUMP_ON_HIT_FORCE = 400f;

  public void InitModel(ActorModel model)
  {
  }

  public void SetModel(ActorModel model) => this.model = (SlimeModel) model;

  public override void Start()
  {
    base.Start();
    flee = GetComponent<GoldSlimeFlee>();
    regionMember = GetComponent<RegionMember>();
    slimeAudio = GetComponent<SlimeAudio>();
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
  }

  private bool IdentCausesPlorts(Identifiable.Id id) => id != Identifiable.Id.GINGER_VEGGIE && id != Identifiable.Id.GOLD_PLORT && (Identifiable.IsFood(id) || Identifiable.IsChick(id) || Identifiable.IsPlort(id));

  public void ProcessCollisionEnter(Collision collision)
  {
    Identifiable component1 = collision.gameObject.GetComponent<Identifiable>();
    if (!(component1 != null) || !IdentCausesPlorts(component1.id) || colliders.Contains(collision.gameObject))
      return;
    PlortInvulnerability component2 = collision.gameObject.GetComponent<PlortInvulnerability>();
    if (!(component2 == null) && component2.enabled)
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
    ProducePlort();
    if (flee != null)
      flee.StartFleeing(SRSingleton<SceneContext>.Instance.Player);
    colliders.Add(collision.gameObject);
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  private void ProduceAt(Vector3 dropAt)
  {
    PlortInvulnerability component = InstantiateActor(plortPrefab, regionMember.setId, dropAt, transform.rotation).GetComponent<PlortInvulnerability>();
    if (component != null)
      component.GoInvulnerable();
    ++plortsProduced;
    if (plortsProduced != 3)
      return;
    SRSingleton<SceneContext>.Instance.AchievementsDirector.AddToStat(AchievementsDirector.IntStat.GOLD_SLIME_TRIPLE_PLORT, 1);
  }

  private void ProducePlort()
  {
    if (model.isGlitch)
      return;
    Vector3 dropAt = transform.position - transform.forward;
    ProduceAt(dropAt);
    if (playerState.HasUpgrade(PlayerState.Upgrade.GOLDEN_SURESHOT))
    {
      ProduceAt(dropAt - transform.forward * 0.25f);
      ProduceAt(dropAt - transform.forward * 0.5f);
    }
    GetComponent<Rigidbody>().AddForce(Vector3.up * 400f);
    slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
    if (!(plortFX != null))
      return;
    SpawnAndPlayFX(plortFX, transform.position, transform.rotation);
  }
}
