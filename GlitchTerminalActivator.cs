// Decompiled with JetBrains decompiler
// Type: GlitchTerminalActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlitchTerminalActivator : SRBehaviour, TechActivator
{
  [Tooltip("Teleport destination transform.")]
  public Transform destinationTransform;
  [Tooltip("List of GlitchTeleportDestination ids in SLIMULATIONS representing potential entrance teleporters.")]
  public string[] destinationIds;
  [Tooltip("FX played on successful button press.")]
  public GameObject onButtonPressedSuccessFX;
  [Tooltip("SFX cue on successful button press.")]
  public SECTR_AudioCue onButtonPressedSuccessCue;
  [Tooltip("FX played on unsuccessful button press.")]
  public GameObject onButtonPressedFailureFX;
  [Tooltip("SFX cue on unsuccessful button press.")]
  public SECTR_AudioCue onButtonPressedFailureCue;
  private ProgressDirector progressDirector;
  private TimeDirector timeDirector;
  private GlitchMetadata metadata;
  private GlitchTerminalAnimator animator;
  private GlitchTeleportDestination[] destinations;
  private Animator buttonAnimator;
  private int buttonAnimationId;
  private GameObject onButtonPressedFXInstance;

  public void Awake()
  {
    progressDirector = SRSingleton<SceneContext>.Instance.ProgressDirector;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    animator = GetRequiredComponentInParent<GlitchTerminalAnimator>();
    buttonAnimator = GetRequiredComponentInParent<Animator>();
    buttonAnimationId = Animator.StringToHash("ButtonPressed");
  }

  public void Start() => destinations = destinationIds.Select(id => SRSingleton<GlitchRegionHelper>.Instance.destinationsDict[id]).ToArray();

  public void Activate()
  {
    if (onButtonPressedFXInstance != null)
      return;
    bool flag = GetLinkState() == LinkState.ACTIVE;
    SECTR_AudioSystem.Play(flag ? onButtonPressedSuccessCue : onButtonPressedFailureCue, transform.position, false);
    buttonAnimator.SetTrigger(buttonAnimationId);
    if (flag && onButtonPressedSuccessFX != null)
      onButtonPressedFXInstance = SpawnAndPlayFX(onButtonPressedSuccessFX, transform.position, Quaternion.identity);
    else if (!flag && onButtonPressedFailureFX != null)
      onButtonPressedFXInstance = SpawnAndPlayFX(onButtonPressedFailureFX, transform.position, Quaternion.identity);
    if (!flag)
      return;
    if (!progressDirector.HasProgress(ProgressDirector.ProgressType.ENTER_ZONE_SLIMULATION))
      progressDirector.SetProgress(ProgressDirector.ProgressType.ENTER_ZONE_SLIMULATION, timeDirector.CurrDay());
    GlitchTeleportDestination destination = destinations[(timeDirector.CurrDay() - progressDirector.GetProgress(ProgressDirector.ProgressType.ENTER_ZONE_SLIMULATION)) % destinations.Length];
    destination.isPotentialExitDestination = false;
    animator.OnEnter(destination.destinationTransform);
  }

  public GameObject GetCustomGuiPrefab()
  {
    switch (GetLinkState())
    {
      case LinkState.INACTIVE_PROGRESS:
        return metadata.activatorGuiProgress;
      case LinkState.INACTIVE_AMMO:
        return metadata.activatorGuiAmmo;
      case LinkState.PRE_ACTIVE:
        return metadata.activatorGuiPreActive;
      default:
        return null;
    }
  }

  public LinkState GetLinkState()
  {
    if (!progressDirector.HasProgress(ProgressDirector.ProgressType.UNLOCK_SLIMULATIONS))
      return LinkState.INACTIVE_PROGRESS;
    Ammo ammo = SRSingleton<SceneContext>.Instance.PlayerState.Ammo;
    if (Enumerable.Range(0, 4).Any(ii => ammo.GetSlotCount(ii) > 0))
      return LinkState.INACTIVE_AMMO;
    return SRInput.Instance.GetInputMode() == SRInput.InputMode.DEFAULT ? LinkState.ACTIVE : LinkState.PRE_ACTIVE;
  }

  public enum LinkState
  {
    INACTIVE_PROGRESS,
    INACTIVE_AMMO,
    PRE_ACTIVE,
    ACTIVE,
  }
}
