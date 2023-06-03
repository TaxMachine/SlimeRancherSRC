// Decompiled with JetBrains decompiler
// Type: AttackPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AttackPlayer : 
  CollidableActorBehaviour,
  ControllerCollisionListener,
  Collidable,
  RegistryUpdateable
{
  public OnFinishChompSuccessDelegate onFinishChompSuccess;
  public bool shouldAttackPlayer;
  public int damagePerAttack = 20;
  private Chomper chomper;
  private SlimeFaceAnimator faceAnim;
  private Vacuumable vacuumable;
  private SlimeAudio slimeAudio;
  private static LRUCache<int, Identifiable> recentIds = new LRUCache<int, Identifiable>(200);

  public override void Awake()
  {
    vacuumable = GetComponent<Vacuumable>();
    chomper = GetComponent<Chomper>();
    faceAnim = GetComponent<SlimeFaceAnimator>();
    slimeAudio = GetComponent<SlimeAudio>();
  }

  public void OnControllerCollision(GameObject obj) => MaybeSpinAndChomp(obj, false);

  public void ProcessCollisionEnter(Collision col) => MaybeSpinAndChomp(col.gameObject, false);

  public void ProcessCollisionExit(Collision col)
  {
  }

  public bool MaybeSpinAndChomp(GameObject obj, bool ignoreEmotions)
  {
    if (!shouldAttackPlayer || !chomper.CanChomp())
      return false;
    Identifiable.Id otherId = ExtractOtherId(obj);
    if (otherId == Identifiable.Id.PLAYER)
    {
      transform.LookAt(obj.transform);
      chomper.StartChomp(obj, otherId, false, true, null, FinishChomp);
    }
    return true;
  }

  public bool DoesAttack(GameObject other) => ExtractOtherId(other) == Identifiable.Id.PLAYER;

  public bool MaybeChomp(GameObject obj)
  {
    if (!shouldAttackPlayer || !chomper.CanChomp())
      return false;
    Identifiable.Id otherId = ExtractOtherId(obj);
    if (otherId == Identifiable.Id.PLAYER)
      chomper.StartChomp(obj, otherId, false, false, null, FinishChomp);
    return true;
  }

  private Identifiable.Id ExtractOtherId(GameObject other)
  {
    int instanceId = other.GetInstanceID();
    Identifiable.Id otherId;
    if (recentIds.contains(instanceId))
    {
      Identifiable identifiable = recentIds.get(instanceId);
      otherId = identifiable == null ? Identifiable.Id.NONE : identifiable.id;
    }
    else
    {
      Identifiable component = other.GetComponent<Identifiable>();
      recentIds.put(instanceId, component);
      otherId = component == null ? Identifiable.Id.NONE : component.id;
    }
    return otherId;
  }

  public void RegistryUpdate()
  {
    if (!shouldAttackPlayer || !(vacuumable != null) || !vacuumable.isHeld() || !chomper.CanChomp())
      return;
    chomper.StartChomp(SRSingleton<SceneContext>.Instance.Player, Identifiable.Id.PLAYER, true, false, null, FinishChomp);
  }

  public void CancelChomp(GameObject obj) => chomper.CancelChomp(obj);

  private void FinishChomp(
    GameObject chomping,
    Identifiable.Id chompingId,
    bool whileHeld,
    bool wasLaunched)
  {
    GameObject gameObject = chomping;
    slimeAudio.Play(slimeAudio.slimeSounds.attackCue);
    if (whileHeld)
      SRSingleton<Overlay>.Instance.PlayChomp();
    if (gameObject == null)
      return;
    faceAnim.SetTrigger("triggerChompClosed");
    DoDamage(gameObject, false);
    if (onFinishChompSuccess == null)
      return;
    onFinishChompSuccess(gameObject);
  }

  private bool DoDamage(GameObject other, bool immediateMode)
  {
    if (other == null)
      return true;
    if (!immediateMode)
      slimeAudio.Play(slimeAudio.slimeSounds.gulpCue);
    if (!other.GetInterfaceComponent<Damageable>().Damage(damagePerAttack, gameObject))
      return false;
    DeathHandler.Kill(other, DeathHandler.Source.SLIME_ATTACK_PLAYER, gameObject, "AttackPlayer.DoDamage");
    if (!immediateMode)
      PlayOnDeathAudio(other);
    return true;
  }

  private void PlayOnDeathAudio(GameObject other)
  {
    SlimeAudio componentInChildren = other.GetComponentInChildren<SlimeAudio>();
    if (!(componentInChildren != null) || !(componentInChildren.slimeSounds.voiceDamageCue != null))
      return;
    SECTR_AudioSystem.Play(componentInChildren.slimeSounds.voiceDamageCue, other.transform.position, false);
  }

  public delegate void OnFinishChompSuccessDelegate(GameObject gameObject);
}
