// Decompiled with JetBrains decompiler
// Type: CollisionAggregator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CollisionAggregator : MonoBehaviour
{
  private List<Collidable> collidableBehaviours = new List<Collidable>();

  public void Register(CollidableActorBehaviour collidableBehaviour)
  {
    if (!(collidableBehaviour is Collidable))
      return;
    Register(collidableBehaviour as Collidable);
  }

  private void Register(Collidable collidableBehaviour)
  {
    if (collidableBehaviours.Contains(collidableBehaviour))
      return;
    collidableBehaviours.Add(collidableBehaviour);
  }

  public void Deregister(CollidableActorBehaviour collidableBehaviour)
  {
    if (!(collidableBehaviour is Collidable))
      return;
    Deregister(collidableBehaviour as Collidable);
  }

  private void Deregister(Collidable collidableBehaviour) => collidableBehaviours.Remove(collidableBehaviour);

  public void OnCollisionEnter(Collision col)
  {
    foreach (Collidable collidableBehaviour in collidableBehaviours)
      collidableBehaviour?.ProcessCollisionEnter(col);
  }
}
