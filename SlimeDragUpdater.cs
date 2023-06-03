// Decompiled with JetBrains decompiler
// Type: SlimeDragUpdater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeDragUpdater : RegisteredActorBehaviour, RegistryFixedUpdateable
{
  private static float DRAG_VAC = 0.0f;
  private static float DRAG_NORM = 0.5f;
  private Vacuumable vacuumable;
  private Rigidbody body;

  public void Awake()
  {
    vacuumable = GetComponent<Vacuumable>();
    body = GetComponent<Rigidbody>();
  }

  public void RegistryFixedUpdate()
  {
    if (vacuumable != null && vacuumable.isCaptive() && body != null)
    {
      body.drag = DRAG_VAC;
    }
    else
    {
      if (!(body != null))
        return;
      body.drag = DRAG_NORM;
    }
  }
}
