// Decompiled with JetBrains decompiler
// Type: GlitchSlimeSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Serializable.Optional;
using UnityEngine;

public class GlitchSlimeSpawner : DirectedSlimeSpawner
{
  [Tooltip("If enabled, overrides GlitchMetadata.dittoStandard.probability.")]
  public Float probablityStandard;
  [Tooltip("If enabled, overrides GlitchMetadata.dittoLargo.probability.")]
  public Float probablityLargo;
  private GlitchMetadata metadata;

  public override void Awake()
  {
    base.Awake();
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
  }

  protected override void OnActorSpawned(GameObject instance)
  {
    base.OnActorSpawned(instance);
    GlitchSlime component = instance.GetComponent<GlitchSlime>();
    if (!(component != null) || !Randoms.SHARED.GetProbability(metadata.GetDittoProbability(Identifiable.GetId(instance), this)))
      return;
    component.enabled = true;
  }
}
