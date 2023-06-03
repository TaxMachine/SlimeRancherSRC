// Decompiled with JetBrains decompiler
// Type: TransformChanceOnReproduce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class TransformChanceOnReproduce : SRBehaviour
{
  [Tooltip("Probability we will transform on any given opportunity.")]
  public float transformChance = 0.05f;
  [Tooltip("What do we transform into.")]
  public GameObject targetPrefab;
  [Tooltip("Extra particle effect to play on transform.")]
  public GameObject transformFX;
  private RegionMember regionMember;

  public void Awake() => regionMember = GetComponent<RegionMember>();

  public void MaybeTransform()
  {
    if (!Randoms.SHARED.GetProbability(transformChance))
      return;
    SpawnAndPlayFX(transformFX, transform.position, transform.rotation);
    Destroyer.DestroyActor(gameObject, "TransformChanceOnReproduce.MaybeTransform");
    InstantiateActor(targetPrefab, regionMember.setId, transform.position, transform.rotation);
  }
}
