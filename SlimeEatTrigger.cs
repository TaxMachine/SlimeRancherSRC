// Decompiled with JetBrains decompiler
// Type: SlimeEatTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeEatTrigger : RegisteredActorBehaviour, RegistryUpdateable
{
  private const float NEXT_CHOMP_COOLDOWN = 0.0500000045f;
  private TimeDirector timeDirector;
  private SlimeEat eat;
  private AttackPlayer attack;
  private List<EatTarget> targets = new List<EatTarget>();

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    eat = GetComponentInParent<SlimeEat>();
    eat.onFinishChompSuccess += OnFinishChompSuccess;
    attack = GetComponentInParent<AttackPlayer>();
    if (!(attack != null))
      return;
    attack.onFinishChompSuccess += OnFinishChompSuccess;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    eat.onFinishChompSuccess -= OnFinishChompSuccess;
    if (!(attack != null))
      return;
    attack.onFinishChompSuccess -= OnFinishChompSuccess;
  }

  public void RegistryUpdate()
  {
    for (int index = targets.Count - 1; index >= 0; --index)
    {
      EatTarget target = targets[index];
      if (target.gameObject == null)
        targets.RemoveAt(index);
      else if (timeDirector.HasReached(target.time))
      {
        if (!target.isColliding)
          targets.RemoveAt(index);
        else if (eat.MaybeChomp(target.gameObject) || attack != null && attack.MaybeChomp(target.gameObject))
          break;
      }
    }
  }

  public void OnTriggerEnter(Collider collider) => SetColliding(collider, true);

  public void OnTriggerExit(Collider collider)
  {
    SetColliding(collider, false);
    eat.CancelChomp(collider.gameObject);
    if (!(attack != null))
      return;
    attack.CancelChomp(collider.gameObject);
  }

  private void SetColliding(Collider collider, bool colliding)
  {
    if (!eat.DoesEat(collider.gameObject) && (attack == null || !attack.DoesAttack(collider.gameObject)))
      return;
    EatTarget target = FindTarget(collider.gameObject);
    if (target != null)
    {
      target.isColliding = colliding;
    }
    else
    {
      if (!colliding)
        return;
      targets.Insert(0, new EatTarget()
      {
        gameObject = collider.gameObject,
        time = timeDirector.WorldTime(),
        isColliding = true
      });
    }
  }

  private EatTarget FindTarget(GameObject gameObject) => targets.FirstOrDefault(t => t.gameObject == gameObject);

  private void OnFinishChompSuccess(GameObject gameObject)
  {
    EatTarget target = FindTarget(gameObject);
    if (target == null)
      return;
    target.time = timeDirector.HoursFromNow(71f / (452f * Math.PI));
  }

  private class EatTarget
  {
    public GameObject gameObject;
    public bool isColliding;
    public double time;
  }
}
