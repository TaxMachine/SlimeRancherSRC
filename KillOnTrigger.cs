// Decompiled with JetBrains decompiler
// Type: KillOnTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class KillOnTrigger : SRBehaviour
{
  public GameObject killFX;
  public GameObject playerKillFx;

  private void OnTriggerEnter(Collider collider)
  {
    Rigidbody component = collider.GetComponent<Rigidbody>();
    if (collider.isTrigger || !(component != null) || component.isKinematic && !PhysicsUtil.IsPlayerMainCollider(collider))
      return;
    Debug.Log("Fallthrough destroying: " + collider.gameObject.name);
    DeathHandler.Kill(collider.gameObject, DeathHandler.Source.KILL_ON_TRIGGER, gameObject, "KillOnTrigger.OnTriggerEnter");
    if (PhysicsUtil.IsPlayerMainCollider(collider) && playerKillFx != null)
    {
      SpawnAndPlayFX(playerKillFx, collider.gameObject.transform.position, Quaternion.identity);
    }
    else
    {
      if (!(killFX != null))
        return;
      SpawnAndPlayFX(killFX, collider.gameObject.transform.position, Quaternion.identity);
    }
  }
}
