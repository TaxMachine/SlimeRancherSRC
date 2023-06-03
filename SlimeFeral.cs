// Decompiled with JetBrains decompiler
// Type: SlimeFeral
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class SlimeFeral : RegisteredActorBehaviour, RegistryUpdateable, ActorModel.Participant
{
  [Tooltip("The aura we use to indicate when a slime has gone feral.")]
  public GameObject auraPrefab;
  [Tooltip("Whether the feralness of the slime can be made true on the fly")]
  public bool dynamicToFeral;
  [Tooltip("Whether the feralness of the slime can be made false on the fly")]
  public bool dynamicFromFeral = true;
  [Tooltip("Hours after which a feral should poof.")]
  public float feralLifetimeHours = 3f;
  [Tooltip("The FX to play when ferality causes us to poof.")]
  public GameObject destroyFX;
  private SlimeEmotions emotions;
  private RegionMember member;
  private GameObject aura;
  private TimeDirector timeDir;
  private SlimeModel model;
  private double expireAt = double.PositiveInfinity;
  private const float AGITATION_FERAL_TRIGGER = 0.999f;
  private const float DEFERAL_AGITATION_ADJUST = -0.5f;

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    member = GetComponent<RegionMember>();
    GetComponent<SlimeEat>().onEat += DidEat;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    if (GetComponent<Vacuumable>().size != Vacuumable.Size.NORMAL)
      return;
    Destroyer.Destroy(this, "SlimeFeral.Awake");
  }

  public void InitModel(ActorModel model)
  {
  }

  public void SetModel(ActorModel model)
  {
    this.model = (SlimeModel) model;
    if (this.model.isFeral)
      MakeFeral();
    else
      MakeNotFeral(false);
  }

  public void RegistryUpdate()
  {
    if (dynamicToFeral && !model.isFeral && emotions.GetCurr(SlimeEmotions.Emotion.AGITATION) >= 0.99900001287460327)
      SetFeral();
    if (!timeDir.HasReached(expireAt))
      return;
    if (CellDirector.IsOnRanch(member) || CellDirector.IsInWilds(member))
    {
      expireAt += 3600.0;
    }
    else
    {
      if (destroyFX != null)
        SpawnAndPlayFX(destroyFX, transform.position, Quaternion.identity);
      Destroyer.DestroyActor(gameObject, "SlimeFeral.RegistryUpdate");
    }
  }

  public void DidEat(Identifiable.Id id)
  {
    if (!dynamicFromFeral || !model.isFeral || id == Identifiable.Id.PLAYER)
      return;
    ClearFeral();
  }

  public void SetFeral()
  {
    if (model.isFeral)
      return;
    MakeFeral();
  }

  private void MakeFeral()
  {
    AttackPlayer component1 = GetComponent<AttackPlayer>();
    if (component1 != null)
      component1.shouldAttackPlayer = true;
    GotoPlayer component2 = GetComponent<GotoPlayer>();
    if (component2 != null)
      component2.shouldGotoPlayer = true;
    foreach (FindConsumable component3 in GetComponents<FindConsumable>())
      component3.UpdateSearchIds();
    aura = Instantiate(auraPrefab);
    aura.transform.SetParent(transform, false);
    GetComponent<SlimeFaceAnimator>().SetFeral();
    model.isFeral = true;
    expireAt = timeDir.HoursFromNowOrStart(feralLifetimeHours);
  }

  public void ClearFeral(bool deagitate = false)
  {
    if (!model.isFeral)
      return;
    MakeNotFeral(deagitate);
  }

  private void MakeNotFeral(bool deagitate)
  {
    SlimeAudio component1 = GetComponent<SlimeAudio>();
    if (component1 != null)
      component1.Play(component1.slimeSounds.unferalCue);
    AttackPlayer component2 = GetComponent<AttackPlayer>();
    if (component2 != null)
      component2.shouldAttackPlayer = false;
    GotoPlayer component3 = GetComponent<GotoPlayer>();
    if (component3 != null)
      component3.shouldGotoPlayer = false;
    foreach (FindConsumable component4 in GetComponents<FindConsumable>())
      component4.UpdateSearchIds();
    Destroyer.Destroy(aura, "SlimeFeral.ClearFeral");
    GetComponent<SlimeFaceAnimator>().ClearFeral();
    model.isFeral = false;
    if (deagitate)
      emotions.Adjust(SlimeEmotions.Emotion.AGITATION, -0.5f);
    expireAt = double.PositiveInfinity;
  }

  public bool IsFeral() => model.isFeral;
}
