// Decompiled with JetBrains decompiler
// Type: LuckySlimeFlee
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class LuckySlimeFlee : SlimeSubbehaviour
{
  public GameObject disappearFX;
  public SECTR_AudioCue disappearCue;
  private double? fleeProximityDisappearTime;
  private double? fleeTriggeredDisappearTime;
  private TimeDirector timeDir;
  private const float FLEE_PROXIMITY_WORLD_TIME = 600f;
  private const float FLEE_TRIGGERED_WORLD_TIME = 600f;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void OnTriggerEnter(Collider collider)
  {
    Identifiable component = collider.gameObject.GetComponent<Identifiable>();
    if (fleeProximityDisappearTime.HasValue || fleeTriggeredDisappearTime.HasValue || !(component != null) || component.id != Identifiable.Id.PLAYER)
      return;
    Flee(collider.gameObject, false);
  }

  public void StartFleeing(GameObject fleeFrom)
  {
    if (fleeTriggeredDisappearTime.HasValue)
      return;
    Flee(fleeFrom, true);
  }

  private void Flee(GameObject fleeFrom, bool triggered)
  {
    if (triggered)
    {
      fleeTriggeredDisappearTime = new double?(timeDir.WorldTime() + 600.0);
      fleeProximityDisappearTime = new double?();
    }
    else
      fleeProximityDisappearTime = new double?(timeDir.WorldTime() + 600.0);
    plexer.ForceRethink();
  }

  public override float Relevancy(bool isGrounded) => fleeProximityDisappearTime.HasValue || fleeTriggeredDisappearTime.HasValue ? 1f : 0.0f;

  public override void Selected()
  {
  }

  public override void Action()
  {
    if ((!fleeProximityDisappearTime.HasValue || !timeDir.HasReached(fleeProximityDisappearTime.Value)) && (!fleeTriggeredDisappearTime.HasValue || !timeDir.HasReached(fleeTriggeredDisappearTime.Value)))
      return;
    SpawnAndPlayFX(disappearFX, transform.position, transform.rotation);
    if (disappearCue != null)
      SECTR_AudioSystem.Play(disappearCue, transform.position, false);
    Destroyer.DestroyActor(gameObject, "LuckySlimeFlee.Action");
  }
}
