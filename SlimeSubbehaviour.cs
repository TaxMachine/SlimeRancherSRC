// Decompiled with JetBrains decompiler
// Type: SlimeSubbehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class SlimeSubbehaviour : CollidableActorBehaviour
{
  protected SlimeEmotions emotions;
  protected Vacuumable vacuumable;
  protected SlimeSubbehaviourPlexer plexer;
  protected Rigidbody slimeBody;
  private const float STABILIZE_FACTOR = 0.1f;

  public override void Awake()
  {
    base.Awake();
    plexer = GetComponent<SlimeSubbehaviourPlexer>();
    emotions = GetComponent<SlimeEmotions>();
    vacuumable = GetComponent<Vacuumable>();
    slimeBody = GetComponent<Rigidbody>();
  }

  public abstract float Relevancy(bool isGrounded);

  public abstract void Action();

  public abstract void Selected();

  public virtual void Deselected()
  {
  }

  public virtual bool CanRethink() => true;

  public virtual bool Forbids(SlimeSubbehaviour toMaybeForbid) => false;

  protected bool IsFloating() => plexer != null && plexer.IsFloating();

  protected bool IsGrounded() => plexer != null && plexer.IsGrounded();

  protected bool IsNearGrounded(float dist) => plexer != null && plexer.IsNearGrounded(dist);

  protected bool IsBlocked(GameObject obj, int layersToIgnore = 0, bool forceCheckFullDist = false) => plexer != null && plexer.IsBlocked(obj, layersToIgnore, forceCheckFullDist);

  protected bool IsCaptive()
  {
    bool flag = false;
    if (vacuumable != null)
      flag = vacuumable.isCaptive();
    return flag;
  }

  protected void RotateTowards(Vector3 dirToTarget, float facingSpeed, float facingStability)
  {
    Vector3 angularVelocity = slimeBody.angularVelocity;
    slimeBody.AddTorque(Vector3.Cross(Quaternion.AngleAxis((float) (angularVelocity.magnitude * 57.295780181884766 * facingStability * 0.10000000149011612) / facingSpeed, angularVelocity) * transform.forward, dirToTarget) * (facingSpeed * facingSpeed) * slimeBody.mass);
  }

  public static Vector3 GetGotoPos(GameObject obj) => !(obj == SRSingleton<SceneContext>.Instance.Player) ? obj.transform.position : obj.transform.position + Vector3.up;
}
