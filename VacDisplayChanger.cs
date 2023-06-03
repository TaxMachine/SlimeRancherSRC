// Decompiled with JetBrains decompiler
// Type: VacDisplayChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class VacDisplayChanger : MonoBehaviour
{
  [Header("Default Game Mode")]
  [Tooltip("GameObject display on the default ammo mode.")]
  public List<GameObject> displayDefault;
  [Tooltip("SFX played when the display transforms to default ammo mode. (optional)")]
  public SECTR_AudioCue onTransformToDefaultCue2D;
  [Header("Nimble Valley Game Mode")]
  [Tooltip("GameObject display on the Nimble Valley ammo mode.")]
  public List<GameObject> displayNimbleValley;
  [Tooltip("Nimble Valley assembler.")]
  public ActorMatAssemble assembler;
  [Tooltip("SFX played when the display transforms to Nimble Valley ammo mode. (optional)")]
  public SECTR_AudioCue onTransformToNimbleValleyCue2D;
  private PlayerState playerState;

  public void Awake()
  {
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    playerState.onAmmoModeChanged += SetDisplayMode;
    SetDisplayMode(playerState.GetAmmoMode());
  }

  public void OnDestroy() => playerState.onAmmoModeChanged -= SetDisplayMode;

  public void SetDisplayMode(PlayerState.AmmoMode mode)
  {
    bool flag = mode == PlayerState.AmmoMode.DEFAULT;
    bool direction = mode == PlayerState.AmmoMode.NIMBLE_VALLEY;
    foreach (GameObject gameObject in displayDefault)
      gameObject.SetActive(flag);
    foreach (GameObject gameObject in displayNimbleValley)
      gameObject.SetActive(direction);
    if (!assembler.Assemble(direction))
      return;
    if (flag)
    {
      SECTR_AudioSystem.Play(onTransformToDefaultCue2D, Vector3.zero, false);
    }
    else
    {
      if (!direction)
        return;
      SECTR_AudioSystem.Play(onTransformToNimbleValleyCue2D, Vector3.zero, false);
    }
  }
}
