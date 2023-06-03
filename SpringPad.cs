// Decompiled with JetBrains decompiler
// Type: SpringPad
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SpringPad : MonoBehaviour, ControllerCollisionListener
{
  private static Vector3 UP_ACCEL = Vector3.up * 5f;
  private static Vector3 UP_PLAYER_FORCE = Vector3.up * 1.667f;
  public Animator anim;
  private int springAnimId;
  private double nextPlayerHitTime;
  private TimeDirector timeDir;
  private WaitForChargeup waiter;

  public void Awake()
  {
    springAnimId = Animator.StringToHash("spring");
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    waiter = GetComponentInParent<WaitForChargeup>();
  }

  public void OnControllerCollision(GameObject gameObj)
  {
    if (waiter.IsWaiting())
      return;
    vp_FPController component = gameObj.GetComponent<vp_FPController>();
    if (!(component != null) || !timeDir.HasReached(nextPlayerHitTime))
      return;
    component.AddSoftForce(UP_PLAYER_FORCE, 5f);
    anim.SetTrigger(springAnimId);
    nextPlayerHitTime = timeDir.HoursFromNow(0.0166666675f);
  }

  public void OnCollisionEnter(Collision col)
  {
    if (waiter.IsWaiting() || col.gameObject.layer == 16)
      return;
    Rigidbody rigidbody = col.rigidbody;
    if (!(rigidbody != null))
      return;
    rigidbody.AddForce(UP_ACCEL, ForceMode.VelocityChange);
    anim.SetTrigger(springAnimId);
  }
}
