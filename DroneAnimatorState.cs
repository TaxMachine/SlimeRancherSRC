// Decompiled with JetBrains decompiler
// Type: DroneAnimatorState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class DroneAnimatorState : SRAnimatorState<DroneAnimator>
{
  [Tooltip("Looping state identifier.")]
  public Id id;
  private Drone drone;
  private DroneAudioOnActive audio;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateEnter(animator, state, layerIndex);
    Drone drone = GetDrone(animator);
    Destroyer.Destroy(audio, "DroneAnimatorState.OnStateEnter");
    audio = drone.SFX(GetAudioCue(id, drone.metadata));
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo state, int layerIndex)
  {
    base.OnStateExit(animator, state, layerIndex);
    Destroyer.Destroy(audio, "DroneAnimatorState.OnStateExit");
    GetAnimatorWrapper(animator).OnStateExit(id);
  }

  public void OnDestroy() => Destroyer.Destroy(audio, "DroneAnimatorState.OnStateExit");

  private Drone GetDrone(Animator animator)
  {
    if (drone == null)
      drone = animator.gameObject.GetComponentInParent<Drone>();
    return drone;
  }

  private static SECTR_AudioCue GetAudioCue(Id id, DroneMetadata metadata)
  {
    switch (id)
    {
      case Id.GATHER_BEGIN:
        return metadata.onGatherBeginCue;
      case Id.GATHER_LOOP:
        return metadata.onGatherLoopCue;
      case Id.GATHER_END:
        return metadata.onGatherEndCue;
      case Id.DEPOSIT_BEGIN:
        return metadata.onDepositBeginCue;
      case Id.DEPOSIT_LOOP:
        return metadata.onDepositLoopCue;
      case Id.DEPOSIT_END:
        return metadata.onDepositEndCue;
      case Id.REST_BEGIN:
        return metadata.onRestBeginCue;
      case Id.REST_LOOP:
        return metadata.onRestLoopCue;
      case Id.REST_END:
        return metadata.onRestEndCue;
      case Id.IDLE_CELEBRATE:
        return metadata.onHappyCue;
      case Id.IDLE_GRUMP:
        return metadata.onGrumpyCue;
      default:
        return null;
    }
  }

  public enum Id
  {
    NONE = 0,
    GATHER_BEGIN = 10, // 0x0000000A
    GATHER_LOOP = 11, // 0x0000000B
    GATHER_END = 12, // 0x0000000C
    DEPOSIT_BEGIN = 20, // 0x00000014
    DEPOSIT_LOOP = 21, // 0x00000015
    DEPOSIT_END = 22, // 0x00000016
    REST_BEGIN = 30, // 0x0000001E
    REST_LOOP = 31, // 0x0000001F
    REST_END = 32, // 0x00000020
    IDLE_CELEBRATE = 100, // 0x00000064
    IDLE_GRUMP = 200, // 0x000000C8
  }

  public class IdComparer : IEqualityComparer<Id>
  {
    public static IdComparer Instance = new IdComparer();

    public bool Equals(Id a, Id b) => a == b;

    public int GetHashCode(Id a) => (int) a;
  }
}
