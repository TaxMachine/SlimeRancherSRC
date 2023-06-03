// Decompiled with JetBrains decompiler
// Type: BiteEventAggregator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using UnityEngine;

public class BiteEventAggregator : MonoBehaviour
{
  private Animator bodyAnim;

  public event EnableBiteEvent OnEnableBite;

  public event DisableBiteEvent OnDisableBite;

  public event SpawnBubblesEvent OnSpawnBubbles;

  public void Awake() => bodyAnim = gameObject.GetRequiredComponentInChildren<Animator>();

  public void EnableBiteModel()
  {
    if (OnEnableBite == null)
      return;
    OnEnableBite();
  }

  public void DisableBiteModel()
  {
    if (OnDisableBite == null)
      return;
    OnDisableBite();
  }

  public void SpawnBubbles()
  {
    if (OnSpawnBubbles == null)
      return;
    OnSpawnBubbles();
  }

  public bool IsBiteAnimationStateActive() => bodyAnim.GetCurrentAnimatorStateInfo(0).IsName("Bite") || bodyAnim.GetCurrentAnimatorStateInfo(0).IsName("BiteQuick");

  public delegate void EnableBiteEvent();

  public delegate void DisableBiteEvent();

  public delegate void SpawnBubblesEvent();
}
