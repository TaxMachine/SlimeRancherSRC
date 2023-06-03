// Decompiled with JetBrains decompiler
// Type: VortexAdder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VortexAdder : MonoBehaviour
{
  public ActorVortexer vortexer;

  public void OnTriggerEnter(Collider col)
  {
    if (!CanAdd(col.gameObject))
      return;
    vortexer.Connect(col.gameObject);
  }

  protected virtual bool CanAdd(GameObject gameObj)
  {
    Vacuumable component = gameObj.GetComponent<Vacuumable>();
    return component != null && !component.isCaptive() && component.canCapture();
  }
}
