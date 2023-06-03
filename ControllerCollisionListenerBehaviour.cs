// Decompiled with JetBrains decompiler
// Type: ControllerCollisionListenerBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class ControllerCollisionListenerBehaviour : SRBehaviour, ControllerCollisionListener
{
  private bool isControllerColliding;
  private bool wasControllerColliding;

  public void OnControllerCollision(GameObject collision) => isControllerColliding |= Predicate(collision);

  public void LateUpdate()
  {
    if (wasControllerColliding != isControllerColliding)
    {
      if (isControllerColliding)
        OnControllerCollisionEntered();
      else
        OnControllerCollisionExited();
    }
    wasControllerColliding = isControllerColliding;
    isControllerColliding = false;
  }

  protected virtual void OnControllerCollisionEntered()
  {
  }

  protected virtual void OnControllerCollisionExited()
  {
  }

  protected virtual bool Predicate(GameObject collision) => true;
}
