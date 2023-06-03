// Decompiled with JetBrains decompiler
// Type: SECTR_ImpactAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("SECTR/Audio/SECTR Impact Audio")]
public class SECTR_ImpactAudio : MonoBehaviour
{
  private float nextImpactTime;
  private Dictionary<PhysicMaterial, ImpactSound> surfaceTable;
  [SECTR_ToolTip("Default sound to play on impact.")]
  public ImpactSound DefaultSound;
  [SECTR_ToolTip("Surface specific impact sounds.")]
  public List<ImpactSound> SurfaceImpacts = new List<ImpactSound>();
  [SECTR_ToolTip("The minimum relative speed at the time of impact required to trigger this cue.")]
  public float MinImpactSpeed = 0.01f;
  [SECTR_ToolTip("The minimum amount of time between playback of this sound.")]
  public float MinImpactInterval = 0.5f;

  private void OnEnable()
  {
    int count = SurfaceImpacts.Count;
    for (int index = 0; index < count; ++index)
    {
      ImpactSound surfaceImpact = SurfaceImpacts[index];
      if (surfaceImpact.SurfaceMaterial != null)
      {
        if (surfaceTable == null)
          surfaceTable = new Dictionary<PhysicMaterial, ImpactSound>();
        surfaceTable[surfaceImpact.SurfaceMaterial] = surfaceImpact;
      }
    }
  }

  private void OnDisable() => surfaceTable = null;

  public void OnCollisionStay(Collision collision)
  {
    if (Time.time < (double) nextImpactTime || collision == null || collision.contacts.Length == 0 || collision.relativeVelocity.sqrMagnitude < MinImpactSpeed * (double) MinImpactSpeed)
      return;
    ImpactSound defaultSound;
    if (collision.collider.sharedMaterial == null || surfaceTable == null || !surfaceTable.TryGetValue(collision.collider.sharedMaterial, out defaultSound))
      defaultSound = DefaultSound;
    SECTR_AudioSystem.Play(defaultSound.ImpactCue, collision.contacts[0].point, false);
    nextImpactTime = Time.time + MinImpactInterval;
  }

  [Serializable]
  public class ImpactSound
  {
    public PhysicMaterial SurfaceMaterial;
    public SECTR_AudioCue ImpactCue;
  }
}
