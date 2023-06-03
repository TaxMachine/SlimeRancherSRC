// Decompiled with JetBrains decompiler
// Type: EchoNote
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EchoNote : MonoBehaviour
{
  [Tooltip("Note renderer; used to adjust animation when triggered.")]
  public Renderer renderer;
  [Tooltip("Clip from the onCollisionCue to play. (1-indexed)")]
  [Range(1f, 13f)]
  public int clip;

  public void OnTriggerEnter(Collider collider)
  {
    Identifiable.Id id = Identifiable.GetId(collider.gameObject);
    if (!PhysicsUtil.IsPlayerMainCollider(collider) && !Identifiable.IsSlime(id))
      return;
    renderer.material.SetFloat("_StartTime", Time.timeSinceLevelLoad);
    SECTR_AudioSystem.Play(SRSingleton<SceneContext>.Instance.InstrumentDirector.currentInstrument.cue, null, transform.position, false, new int?(clip - 1), id == Identifiable.Id.PLAYER);
  }
}
