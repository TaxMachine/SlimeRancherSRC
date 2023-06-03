// Decompiled with JetBrains decompiler
// Type: SECTR_CharacterAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SECTR/Audio/SECTR Character Audio")]
public class SECTR_CharacterAudio : MonoBehaviour
{
  private Dictionary<PhysicMaterial, SurfaceSound> surfaceTable;
  [SECTR_ToolTip("Default sounds to play if there is no material specific sound.")]
  public SurfaceSound DefaultSounds = new SurfaceSound();
  [SECTR_ToolTip("List of surface specific sounds.")]
  public List<SurfaceSound> SurfaceSounds = new List<SurfaceSound>();

  private void OnEnable()
  {
    int count = SurfaceSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      SurfaceSound surfaceSound = SurfaceSounds[index];
      if (surfaceSound.SurfaceMaterial != null)
      {
        if (surfaceTable == null)
          surfaceTable = new Dictionary<PhysicMaterial, SurfaceSound>();
        surfaceTable[surfaceSound.SurfaceMaterial] = surfaceSound;
      }
    }
  }

  private void OnDisable() => surfaceTable = null;

  public void OnFootstep(PhysicMaterial currentMaterial) => SECTR_AudioSystem.Play(_GetCurrentSurface(currentMaterial).FootstepCue, transform.position, false);

  public void OnJump(PhysicMaterial currentMaterial) => SECTR_AudioSystem.Play(_GetCurrentSurface(currentMaterial).JumpCue, transform.position, false);

  public void OnLand(PhysicMaterial currentMaterial) => SECTR_AudioSystem.Play(_GetCurrentSurface(currentMaterial).LandCue, transform.position, false);

  private SurfaceSound _GetCurrentSurface(PhysicMaterial currentMaterial)
  {
    SurfaceSound surfaceSound;
    return currentMaterial != null && surfaceTable != null && surfaceTable.TryGetValue(currentMaterial, out surfaceSound) ? surfaceSound : DefaultSounds;
  }

  [Serializable]
  public class SurfaceSound
  {
    [SECTR_ToolTip("The material that this set applies to.")]
    public PhysicMaterial SurfaceMaterial;
    [SECTR_ToolTip("Default footstep sound. Used if no material specific sound exists.")]
    public SECTR_AudioCue FootstepCue;
    [SECTR_ToolTip("Default footstep sound. Used if no material specific sound exists.")]
    public SECTR_AudioCue JumpCue;
    [SECTR_ToolTip("Default landing sound. Used if no material specific sound exists.")]
    public SECTR_AudioCue LandCue;
  }
}
