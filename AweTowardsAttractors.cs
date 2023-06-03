// Decompiled with JetBrains decompiler
// Type: AweTowardsAttractors
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class AweTowardsAttractors : SlimeSubbehaviour
{
  public float facingStability = 1f;
  public float facingSpeed = 5f;
  private Attractor target;
  private List<Attractor> attractors = new List<Attractor>();
  private TimeDirector timeDir;
  private SlimeFaceAnimator sfAnimator;
  private double nextActivationTime;
  private float startTime;
  private float endTime;
  private const float SCOOT_CYCLE_TIME = 1f;
  private const float SCOOT_CYCLE_FACTOR = 6.28318548f;
  private const float SCOOT_SPEED_FACTOR = 0.5f;

  public override void Awake()
  {
    base.Awake();
    startTime = Time.time;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    sfAnimator = GetComponent<SlimeFaceAnimator>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (attractors.Count == 0 || !isGrounded || !timeDir.HasReached(nextActivationTime))
      return 0.0f;
    target = Randoms.SHARED.Pick(attractors, null);
    if (target == null)
    {
      attractors.Remove(target);
      target = null;
      return 0.0f;
    }
    return !(target == null) ? Randoms.SHARED.GetInRange(0.1f, 1f) * target.AweFactor(gameObject) : 0.0f;
  }

  public override void Action()
  {
    if (!(target != null))
      return;
    RotateTowards(GetGotoPos(target.gameObject) - transform.position, facingSpeed, facingStability);
    if (!target.CauseMoveTowards())
      return;
    ScootTowards(target.transform.position);
  }

  private void ScootTowards(Vector3 targetPos)
  {
    Vector3 normalized = (targetPos - transform.position).normalized;
    float num = ScootCycleSpeed();
    slimeBody.AddForce(normalized * ((float) (150.0 * slimeBody.mass * 0.5) * Time.fixedDeltaTime * num));
    Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
    slimeBody.AddForceAtPosition(normalized * (270f * slimeBody.mass * Time.fixedDeltaTime * num), position);
  }

  protected float ScootCycleSpeed() => Mathf.Sin((float) ((Time.time - (double) startTime) * 6.2831854820251465)) + 1f;

  public override void Selected()
  {
    sfAnimator.SetTrigger("triggerLongAwe");
    nextActivationTime = timeDir.HoursFromNow(1f);
    endTime = Time.time + 3f;
  }

  public override bool CanRethink() => Time.time >= (double) endTime;

  public void RegisterAttractor(Attractor attractor) => attractors.Add(attractor);

  public void UnregisterAttractor(Attractor attractor) => attractors.Remove(attractor);
}
