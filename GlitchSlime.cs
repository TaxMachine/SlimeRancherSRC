// Decompiled with JetBrains decompiler
// Type: GlitchSlime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class GlitchSlime : SRBehaviour, LiquidConsumer, ActorModel.Participant
{
  private SlimeModel model;
  private RegionMember regionMember;
  private Identifiable.Id id;

  public void Awake()
  {
    regionMember = GetComponent<RegionMember>();
    id = Identifiable.GetId(gameObject);
    enabled = false;
  }

  public void InitModel(ActorModel model) => ((SlimeModel) model).isGlitch = false;

  public void SetModel(ActorModel model)
  {
    this.model = (SlimeModel) model;
    enabled = this.model.isGlitch;
  }

  public void Start()
  {
    model.isGlitch = true;
    GetRequiredComponent<SlimeFaceAnimator>().SetGlitch();
    Vacuumable requiredComponent = GetRequiredComponent<Vacuumable>();
    requiredComponent.onSetHeld += b => OnExposed();
    requiredComponent.consume += () => OnExposed();
    GetRequiredComponent<SlimeHealth>().onDamage += s => OnExposed(s);
  }

  public void AddLiquid(Identifiable.Id id, float units)
  {
    if (!enabled || id != Identifiable.Id.GLITCH_DEBUG_SPRAY_LIQUID)
      return;
    OnExposed();
  }

  private void OnExposed(GameObject source = null)
  {
    Destroyer.DestroyActor(gameObject, "GlitchSlime.OnExposed");
    SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.GetDittoExposureMetadata(id).OnExposed(gameObject, source: source);
  }
}
