// Decompiled with JetBrains decompiler
// Type: SlimeFlee
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeFlee : SlimeSubbehaviour
{
  public GameObject disappearFX;
  public float facingStability = 1f;
  public float facingSpeed = 5f;
  public float fleeSpeedFactor = 1f;
  public SECTR_AudioCue fleeCue;
  protected TimeDirector timeDir;

  protected Vector3? fleeDir { get; private set; }

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void StartFleeing(GameObject fleeFrom)
  {
    SetFleeDirection(transform.position - GetGotoPos(fleeFrom));
    plexer.ForceRethink();
  }

  protected void SetFleeDirection(Vector3 direction)
  {
    direction.y = 0.0f;
    fleeDir = new Vector3?(direction.normalized);
  }

  public bool IsFleeing() => fleeDir.HasValue;

  public override float Relevancy(bool isGrounded) => fleeDir.HasValue ? 1f : 0.0f;

  public override void Selected()
  {
    if (!(fleeCue != null))
      return;
    SlimeAudio component = GetComponent<SlimeAudio>();
    if (!(component != null))
      return;
    component.Play(fleeCue);
  }

  public override void Action()
  {
    if (!this.fleeDir.HasValue)
      return;
    SlimeSubbehaviourPlexer plexer = this.plexer;
    Vector3? fleeDir = this.fleeDir;
    Vector3 direction = fleeDir.Value;
    if (plexer.IsBlocked(null, direction, 0, false))
    {
      SpawnAndPlayFX(disappearFX, transform.position, transform.rotation);
      Destroyer.DestroyActor(gameObject, "SlimeFlee.Action");
    }
    else
    {
      fleeDir = this.fleeDir;
      MoveTowards(fleeDir.Value);
    }
  }

  protected void MoveTowards(Vector3 dirToTarget)
  {
    RotateTowards(dirToTarget, facingSpeed, facingStability);
    slimeBody.AddForce(dirToTarget * (300f * slimeBody.mass * fleeSpeedFactor * Time.fixedDeltaTime));
    Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
    slimeBody.AddForceAtPosition(dirToTarget * (540f * slimeBody.mass * Time.fixedDeltaTime), position);
  }
}
