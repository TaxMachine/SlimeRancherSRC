// Decompiled with JetBrains decompiler
// Type: GlitchSlimeFlee
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class GlitchSlimeFlee : SlimeFlee, ActorModel.Participant
{
  private GlitchSlimeModel model;
  private GlitchMetadata metadata;
  private bool requiresForceBlockCheck = true;

  public override void Awake()
  {
    base.Awake();
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    plexer.activationDelayOverride = new float?(metadata.slimeFleeDelay);
    vacuumable.onSetLaunched += OnSetLaunched;
    SetFleeDirection(Quaternion.Euler(0.0f, Randoms.SHARED.GetInRange(0, 360), 0.0f) * Vector3.one);
  }

  public void InitModel(ActorModel model)
  {
    GlitchSlimeModel glitchSlimeModel = (GlitchSlimeModel) model;
    glitchSlimeModel.deathTime = timeDir.HoursFromNow(metadata.slimeLifetime.GetRandom() * 0.0166666675f);
    glitchSlimeModel.exposureChance = metadata.slimeBaseExposureChance;
  }

  public void SetModel(ActorModel model) => this.model = (GlitchSlimeModel) model;

  public override void OnDestroy()
  {
    base.OnDestroy();
    vacuumable.onSetLaunched -= OnSetLaunched;
  }

  public override void Action()
  {
    if (timeDir.HasReached(model.deathTime))
    {
      if (Randoms.SHARED.GetProbability(model.exposureChance))
        metadata.slimeExposure.OnExposed(gameObject, onInstantiated: instance => instance.GetRequiredComponent<GlitchSlimeFlee>().model.exposureChance = model.exposureChance * (1f - metadata.slimeExposureChanceDegradation));
      SpawnAndPlayFX(disappearFX, transform.position, transform.rotation);
      Destroyer.DestroyActor(gameObject, "GlitchSlimeFlee.Action");
    }
    else
    {
      SlimeSubbehaviourPlexer plexer = this.plexer;
      Vector3? fleeDir = this.fleeDir;
      Vector3 direction = fleeDir.Value;
      int num = requiresForceBlockCheck ? 1 : 0;
      if (plexer.IsBlocked(null, direction, 0, num != 0))
      {
        Quaternion quaternion = Quaternion.Euler(0.0f, Randoms.SHARED.GetInRange(90, 270), 0.0f);
        fleeDir = this.fleeDir;
        Vector3 vector3 = fleeDir.Value;
        SetFleeDirection(quaternion * vector3);
        requiresForceBlockCheck = true;
      }
      else
        requiresForceBlockCheck = false;
      fleeDir = this.fleeDir;
      MoveTowards(fleeDir.Value);
    }
  }

  public void OnDrawGizmosSelected()
  {
    if (!fleeDir.HasValue)
      return;
    Gizmos.color = Color.magenta;
    Gizmos.DrawLine(transform.position, transform.position + fleeDir.Value * 1.5f);
  }

  public void DisableExposureChance() => model.exposureChance = 0.0f;

  private void OnSetLaunched(bool launched)
  {
    if (!launched)
      return;
    DisableExposureChance();
  }
}
