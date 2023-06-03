// Decompiled with JetBrains decompiler
// Type: PuddleSlimeScoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PuddleSlimeScoot : SlimeSubbehaviour
{
  public float straightlineForceFactor = 1f;
  public bool canRicochet;
  private Mode mode;
  private float turnTorque;
  private float nextModeChoice;
  private Vector3 ricochetDir;
  private const float TURN_PROB = 0.2f;
  private const float MIN_TURN_TORQUE = 1f;
  private const float MAX_TURN_TORQUE = 2f;

  public override void Awake()
  {
    base.Awake();
    Collider[] components = GetComponents<Collider>();
    int index = 0;
    while (index < components.Length && components[index].isTrigger)
      ++index;
  }

  public override void Start() => base.Start();

  public override bool Forbids(SlimeSubbehaviour toMaybeForbid) => toMaybeForbid is SlimeRandomMove;

  public override float Relevancy(bool isGrounded) => 0.2f;

  public override void Selected() => SelectMode();

  public override void Deselected() => base.Deselected();

  private void SelectMode()
  {
    if (canRicochet && IsBlocked(null))
    {
      mode = Mode.RICOCHET;
      ricochetDir = -transform.forward;
    }
    else
      mode = Randoms.SHARED.GetProbability(0.2f) ? Mode.TURN : Mode.SCOOT;
    nextModeChoice = Time.fixedTime + 1f;
    if (mode == Mode.TURN)
      turnTorque = (Randoms.SHARED.GetBoolean() ? 1f : -1f) * Randoms.SHARED.GetInRange(1f, 2f);
    else
      turnTorque = 0.0f;
  }

  public override void Action()
  {
    if (Time.fixedTime >= (double) nextModeChoice)
      SelectMode();
    if (!IsGrounded())
      return;
    Rigidbody component = GetComponent<Rigidbody>();
    if (mode == Mode.RICOCHET)
    {
      RotateTowards(ricochetDir, 5f, 1f);
      component.AddForce(ricochetDir * (80f * straightlineForceFactor * component.mass * Time.fixedDeltaTime));
      Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
      component.AddForceAtPosition(ricochetDir * (240f * straightlineForceFactor * component.mass * Time.fixedDeltaTime), position);
    }
    else if (mode == Mode.TURN)
    {
      float num = IsFloating() ? 0.2f : 1f;
      component.AddForce(transform.forward * (-80f * num * component.mass * Time.fixedDeltaTime));
      Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
      component.AddForceAtPosition(transform.forward * (-240f * num * component.mass * Time.fixedDeltaTime), position);
      component.AddTorque(0.0f, turnTorque * Time.fixedDeltaTime, 0.0f);
    }
    else
    {
      component.AddForce(transform.forward * (straightlineForceFactor * 80f * component.mass * Time.fixedDeltaTime));
      Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
      component.AddForceAtPosition(transform.forward * (straightlineForceFactor * 240f * component.mass * Time.fixedDeltaTime), position);
    }
  }

  private enum Mode
  {
    SCOOT,
    TURN,
    RICOCHET,
  }
}
