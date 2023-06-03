// Decompiled with JetBrains decompiler
// Type: GoldSlimeFlee
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GoldSlimeFlee : SlimeFlee
{
  private bool currentlyChomping;
  private double chompingUntil;
  private SlimeEat eat;
  private const float CHOMPING_MAX_DELAY = 0.0166666675f;

  public override void Awake()
  {
    base.Awake();
    eat = GetComponent<SlimeEat>();
    eat.onStartChomp = OnStartChomp;
    eat.onProducePlortsComplete = OnPlortsProduced;
  }

  public override void OnDestroy()
  {
    eat.onStartChomp = null;
    eat.onProducePlortsComplete = null;
    eat = null;
    base.OnDestroy();
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (IsFleeing() || collider.isTrigger || !PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    StartFleeing(collider.gameObject);
  }

  private void OnStartChomp()
  {
    currentlyChomping = true;
    chompingUntil = timeDir.HoursFromNow(0.0166666675f);
    slimeBody.velocity = Vector3.zero;
    slimeBody.angularVelocity = Vector3.zero;
  }

  private void OnPlortsProduced() => currentlyChomping = false;

  public override void Action()
  {
    if (currentlyChomping && !timeDir.HasReached(chompingUntil))
      return;
    base.Action();
  }
}
