// Decompiled with JetBrains decompiler
// Type: PhasingSpawnResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

public class PhasingSpawnResource : PhaseableObject
{
  private SpawnResource spawnResource;
  private bool readyToPhase;
  public GameObject phaseOutFx;
  public GameObject phaseInFx;

  public void Awake()
  {
    spawnResource = GetComponent<SpawnResource>();
    spawnResource.onReachedSpawnTime += () => readyToPhase = true;
  }

  public override void PhaseIn()
  {
    spawnResource.RefreshSpawnJointObjectPositions();
    if (!gameObject.activeInHierarchy)
      return;
    SpawnAndPlayFX(phaseInFx, transform.position, transform.rotation);
  }

  public override void PhaseOut()
  {
    if (gameObject.activeInHierarchy)
      SpawnAndPlayFX(phaseOutFx, transform.position, transform.rotation);
    readyToPhase = false;
  }

  public override bool ReadyToPhase() => readyToPhase;
}
