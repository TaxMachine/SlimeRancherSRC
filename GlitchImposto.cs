// Decompiled with JetBrains decompiler
// Type: GlitchImposto
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections;
using UnityEngine;

public class GlitchImposto : IdHandler<GlitchImpostoModel>, LiquidConsumer
{
  [Tooltip("Custom transform node used as the spawn position of the glitch slimes when exposed. (optional)")]
  public Transform spawnNode;
  [Tooltip("Weight used when picking which imposto to be enabled by the GlitchImpostoDirector.")]
  public float weight = 1f;
  private GlitchImpostoModel model;
  private GlitchMetadata metadata;
  private TimeDirector timeDirector;
  private GameObject player;
  private GlitchImpostoDirector impostoDirector;
  private Renderer[] renderers;
  private Visibility visibility;

  protected override string IdPrefix() => "imposto";

  public override void Awake()
  {
    base.Awake();
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    player = SRSingleton<SceneContext>.Instance.Player;
    impostoDirector = GetComponentInParent<GlitchImpostoDirector>();
    impostoDirector.Register(this);
    renderers = GetComponentsInChildren<Renderer>(true);
  }

  protected override GameModel.Unregistrant Register(GameModel game) => game.Glitch.impostos.Register(this);

  protected override void InitModel(GlitchImpostoModel model)
  {
    model.deactivateTime = new double?();
    model.cooldownTime = 0.0;
  }

  protected override void SetModel(GlitchImpostoModel model)
  {
    this.model = model;
    if (!this.model.deactivateTime.HasValue || !timeDirector.HasReached(this.model.deactivateTime.Value))
      return;
    Deactivate();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(impostoDirector != null))
      return;
    impostoDirector.Deregister(this);
    impostoDirector = null;
  }

  public void Update()
  {
    Visibility maxVisibility = GetMaxVisibility();
    if (maxVisibility == visibility)
      return;
    OnVisibilityChanged(visibility, maxVisibility);
    visibility = maxVisibility;
  }

  public void OnDrawGizmos()
  {
    if (Application.isPlaying)
    {
      Gizmos.color = visibility == Visibility.IN_RANGE ? Color.green : (visibility == Visibility.OUT_OF_RANGE ? Color.yellow : Color.red);
      Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 10f);
    }
    if (!GizmosUtil.IsThisOrChildSelected(gameObject) || !(spawnNode != null))
      return;
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(spawnNode.position, 0.5f);
  }

  public void Deactivate()
  {
    gameObject.SetActive(false);
    model.deactivateTime = new double?(0.0);
  }

  public void Activate()
  {
    visibility = Visibility.NONE;
    gameObject.SetActive(true);
    model.deactivateTime = new double?();
  }

  public bool IsReady() => timeDirector.HasReached(model.cooldownTime);

  public void AddLiquid(Identifiable.Id id, float units)
  {
    if (id != Identifiable.Id.GLITCH_DEBUG_SPRAY_LIQUID)
      return;
    metadata.impostoExposure.OnExposed(gameObject, new Vector3?(spawnNode != null ? spawnNode.position : transform.position));
    model.cooldownTime = timeDirector.HoursFromNow(metadata.impostoCooldownTime);
    Deactivate();
  }

  private void OnVisibilityChanged(
    Visibility previous,
    Visibility current)
  {
    switch (previous)
    {
      case Visibility.NONE:
        if (model.deactivateTime.HasValue && timeDirector.HasReached(model.deactivateTime.Value))
        {
          StartCoroutine(OnFailedExposedCoroutine());
          return;
        }
        break;
      case Visibility.IN_RANGE:
        model.deactivateTime = new double?(timeDirector.HoursFromNow(metadata.impostoDeactivateTime * 0.0166666675f));
        return;
    }
    if (current != Visibility.IN_RANGE)
      return;
    model.deactivateTime = new double?();
  }

  private IEnumerator OnFailedExposedCoroutine()
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    GlitchImposto glitchImposto = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      glitchImposto.metadata.impostoExposure.OnFailedExposed(glitchImposto.gameObject);
      glitchImposto.Deactivate();
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(glitchImposto.metadata.impostoFailedExposedDelayTime);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield return new WaitForSeconds(metadata.impostoFailedExposedDelayTime);
  }

  private Visibility GetMaxVisibility()
  {
    Visibility a = Visibility.NONE;
    for (int index = 0; index < renderers.Length && a < Visibility.IN_RANGE; ++index)
      a = Max(a, renderers[index].isVisible ? ((player.transform.position - renderers[index].transform.position).sqrMagnitude <= (double) metadata.impostoDetectionRange ? Visibility.IN_RANGE : Visibility.OUT_OF_RANGE) : Visibility.NONE);
    return a;
  }

  private static Visibility Max(
    Visibility a,
    Visibility b)
  {
    return a > b ? a : b;
  }

  private enum Visibility
  {
    NONE,
    OUT_OF_RANGE,
    IN_RANGE,
  }
}
