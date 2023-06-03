// Decompiled with JetBrains decompiler
// Type: ClearAreaFeralsOnHit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ClearAreaFeralsOnHit : MonoBehaviour
{
  public float radius = 20f;
  public float minTimeBetween = 1f;
  public SECTR_AudioCue hitCue;
  private float nextTime;
  private WaitForChargeup waiter;

  public void Awake() => waiter = GetComponentInParent<WaitForChargeup>();

  public void OnCollisionEnter(Collision col) => MaybeHandleCollision();

  public void OnControllerCollision(GameObject gameObj) => MaybeHandleCollision();

  private void MaybeHandleCollision()
  {
    if (waiter.IsWaiting() || Time.time < (double) nextTime)
      return;
    HandleCollision();
    nextTime = Time.time + minTimeBetween;
  }

  private void HandleCollision()
  {
    SphereOverlapTrigger.CreateGameObject(transform.position, radius, colliders =>
    {
      foreach (Component collider in colliders)
      {
        SlimeFeral component = collider.GetComponent<SlimeFeral>();
        if (component != null)
          component.ClearFeral(true);
      }
    });
    if (!(hitCue != null))
      return;
    SECTR_AudioSystem.Play(hitCue, transform.position, false);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
