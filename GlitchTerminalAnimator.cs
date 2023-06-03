// Decompiled with JetBrains decompiler
// Type: GlitchTerminalAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Util.Extensions;
using UnityEngine;

public class GlitchTerminalAnimator : SRAnimator, PlayerModel.Participant
{
  public const string STATE_SLEEPING = "state_sleeping";
  public const string STATE_IN_SLIMULATION = "state_in_slimulation";

  public GlitchTerminalActivator activator { get; private set; }

  public override void Awake()
  {
    base.Awake();
    activator = GetRequiredComponentInChildren<GlitchTerminalActivator>();
    SRSingleton<SceneContext>.Instance.GameModel.RegisterPlayerParticipant(this);
  }

  public event OnStateChanged onStateEnter;

  public void OnStateEnter(GlitchTerminalAnimatorState.Id id)
  {
    if (onStateEnter == null)
      return;
    onStateEnter(id);
  }

  public void OnEnter(Transform destinationTransform)
  {
    GlitchTerminalAnimator_Player fx = InstantiatePlayerFX();
    SRSingleton<SceneContext>.Instance.StartCoroutine(OnEnter_Coroutine_FX(fx, destinationTransform));
    SRSingleton<SceneContext>.Instance.StartCoroutine(OnEnter_Coroutine_Region(fx));
  }

  private IEnumerator OnEnter_Coroutine_FX(
    GlitchTerminalAnimator_Player fx,
    Transform destinationTransform)
  {
    GlitchTerminalAnimator terminalAnimator = this;
    GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    using (new TemporaryLockInputMode(terminalAnimator.gameObject.GetInstanceID()))
    {
      using (new TemporaryReplaceSeaMaterial())
      {
        SECTR_AudioSystem.Play(glitch.animationOnTeleportInCue, fx.transform, Vector3.zero, false);
        fx.animator.SetTrigger("trigger_enter_slimulation");
        yield return fx.WaitForStateExit(GlitchTerminalAnimator_PlayerState.Id.ENTERING);
        TeleportTo(destinationTransform, RegionRegistry.RegionSetId.SLIMULATIONS);
        yield return fx.WaitForStateExit(GlitchTerminalAnimator_PlayerState.Id.EXITING);
        Destroyer.Destroy(fx.gameObject, "GlitchTerminalAnimator.OnEnter_Coroutine");
      }
    }
  }

  private IEnumerator OnEnter_Coroutine_Region(GlitchTerminalAnimator_Player fx)
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    GlitchTerminalAnimator terminalAnimator = this;
    Region region;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      region.OnRegionSetDeactivated();
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    terminalAnimator.animator.SetBool("state_in_slimulation", true);
    region = terminalAnimator.gameObject.GetRequiredComponentInParent<Region>();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) fx.WaitForAnimationEvent(GlitchTerminalAnimator_Player.AnimationEvent.ENTERING_FULLY_COVERED);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;*/
    yield return null;
  }

  public static void OnExit(Action onMidpoint, Action onComplete, int sourceObjectId) => SRSingleton<SceneContext>.Instance.StartCoroutine(OnExit_Coroutine_FX(onMidpoint, onComplete, sourceObjectId));

  private static IEnumerator OnExit_Coroutine_FX(
    Action onMidpoint,
    Action onComplete,
    int sourceObjectId)
  {
    GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    Transform destinationTransform = SRSingleton<GlitchRegionHelper_Viktor>.Instance.activator.destinationTransform;
    GlitchTerminalAnimator_Player fx = InstantiatePlayerFX();
    using (new TemporaryLockInputMode(sourceObjectId))
    {
      using (new TemporaryReplaceSeaMaterial())
      {
        SECTR_AudioSystem.Play(glitch.animationOnTeleportOutCue, fx.transform, Vector3.zero, false);
        fx.animator.SetTrigger("trigger_exit_slimulation");
        yield return fx.WaitForStateExit(GlitchTerminalAnimator_PlayerState.Id.EXITING);
        TeleportTo(destinationTransform, RegionRegistry.RegionSetId.VIKTOR_LAB);
        if (onMidpoint != null)
          onMidpoint();
        yield return fx.WaitForStateExit(GlitchTerminalAnimator_PlayerState.Id.ENTERING);
        Destroyer.Destroy(fx.gameObject, "GlitchTerminalAnimator.OnExit_Coroutine");
      }
    }
    if (onComplete != null)
      onComplete();
  }

  public void RegionSetChanged(
    RegionRegistry.RegionSetId previous,
    RegionRegistry.RegionSetId current)
  {
    if (current != RegionRegistry.RegionSetId.VIKTOR_LAB)
      return;
    animator.SetBool("state_in_slimulation", false);
  }

  public void InitModel(PlayerModel model)
  {
  }

  public void SetModel(PlayerModel model)
  {
  }

  public void TransformChanged(Vector3 position, Quaternion rotation)
  {
  }

  public void RegisteredPotentialAmmoChanged(
    Dictionary<PlayerState.AmmoMode, List<GameObject>> ammo)
  {
  }

  public void KeyAdded()
  {
  }

  private static void TeleportTo(
    Transform destinationTransform,
    RegionRegistry.RegionSetId regionSetId)
  {
    TeleportablePlayer requiredComponent = SRSingleton<SceneContext>.Instance.Player.GetRequiredComponent<TeleportablePlayer>();
    Vector3 position = destinationTransform.position;
    Vector3? nullable = new Vector3?(destinationTransform.rotation.eulerAngles);
    int num = (int) regionSetId;
    Vector3? rotation = nullable;
    requiredComponent.TeleportTo(position, (RegionRegistry.RegionSetId) num, rotation, audioEnabled: false);
  }

  private static GlitchTerminalAnimator_Player InstantiatePlayerFX() => Instantiate(SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch.animationFX, SRSingleton<SceneContext>.Instance.Player.transform, false).GetRequiredComponent<GlitchTerminalAnimator_Player>();

  public delegate void OnStateChanged(GlitchTerminalAnimatorState.Id id);

  private class TemporaryLockInputMode : IDisposable
  {
    private readonly int inputModeHandle;

    public TemporaryLockInputMode(int inputModeHandle)
    {
      this.inputModeHandle = inputModeHandle;
      SRInput.Instance.SetInputMode(SRInput.InputMode.LOOK_ONLY, inputModeHandle);
    }

    public void Dispose() => SRInput.Instance.ClearInputMode(inputModeHandle);
  }

  private class TemporaryReplaceSeaMaterial : IDisposable
  {
    private readonly Renderer renderer;
    private readonly Material previousMaterial;

    public TemporaryReplaceSeaMaterial()
    {
      GlitchMetadata glitch = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
      renderer = SRSingleton<GlitchRegionHelper>.Instance.seaRenderer;
      previousMaterial = renderer.sharedMaterial;
      renderer.sharedMaterial = glitch.animationSeaMaterial;
    }

    public void Dispose() => renderer.sharedMaterial = previousMaterial;
  }
}
