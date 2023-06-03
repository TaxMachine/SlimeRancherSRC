// Decompiled with JetBrains decompiler
// Type: TarrSpawnFX
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using UnityEngine;

public class TarrSpawnFX : MonoBehaviour
{
  public GameObject SpawnFX;
  private BiteEventAggregator aggregator;

  public void Awake() => aggregator = gameObject.GetRequiredComponentInChildren<BiteEventAggregator>();

  public void Start() => aggregator.OnSpawnBubbles += OnSpawnBubbles;

  public void OnSpawnBubbles() => SRBehaviour.SpawnAndPlayFX(SpawnFX, gameObject);

  public void Destroy()
  {
    if (!(aggregator != null))
      return;
    aggregator.OnSpawnBubbles -= OnSpawnBubbles;
  }
}
