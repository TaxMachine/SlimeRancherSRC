// Decompiled with JetBrains decompiler
// Type: RegisteredActorBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class RegisteredActorBehaviour : SRBehaviour
{
  protected bool hasStarted;

  public virtual void Start() => hasStarted = true;

  public virtual void OnEnable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.ActorRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.ActorRegistry.Register(this);
  }

  public virtual void OnDisable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.ActorRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.ActorRegistry.Deregister(this);
  }

  public virtual void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || !(SRSingleton<SceneContext>.Instance.ActorRegistry != null))
      return;
    SRSingleton<SceneContext>.Instance.ActorRegistry.Deregister(this);
  }
}
