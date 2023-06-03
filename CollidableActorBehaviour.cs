// Decompiled with JetBrains decompiler
// Type: CollidableActorBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CollidableActorBehaviour : RegisteredActorBehaviour
{
  private CollisionAggregator collisionBehaviour;

  public virtual void Awake() => collisionBehaviour = GetComponent<CollisionAggregator>();

  public override void Start()
  {
    base.Start();
    if (!(collisionBehaviour != null) || !enabled)
      return;
    collisionBehaviour.Register(this);
  }

  public override void OnEnable()
  {
    if (collisionBehaviour != null)
      collisionBehaviour.Register(this);
    base.OnEnable();
  }

  public override void OnDisable()
  {
    if (collisionBehaviour != null)
      collisionBehaviour.Deregister(this);
    base.OnDisable();
  }
}
