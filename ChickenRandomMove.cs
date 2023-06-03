// Decompiled with JetBrains decompiler
// Type: ChickenRandomMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChickenRandomMove : SlimeSubbehaviour, RegistryUpdateable
{
  public float maxJump = 1f;
  public float walkForwardForce = 0.05f;
  public SECTR_AudioCue flapCue;
  private const float JUMP_PROB = 0.5f;
  private const float JUMP_TORQUE = 0.2f;
  private SlimeAudio slimeAudio;
  private Mode mode;
  private float nextModeChoice;
  private Animator animator;
  private int animGroundedId;
  private int animWalkId;
  private int animPeckId;
  private const float MAX_VEL_TO_BOUNCE = 0.1f;
  private const float SQR_MAX_VEL_TO_BOUNCE = 0.0100000007f;

  public override void Awake()
  {
    base.Awake();
    slimeAudio = GetComponent<SlimeAudio>();
    animator = GetComponentInChildren<Animator>();
    animGroundedId = Animator.StringToHash("grounded");
    animWalkId = Animator.StringToHash("walk");
    animPeckId = Animator.StringToHash("peck");
  }

  public override void Start() => base.Start();

  public override float Relevancy(bool isGrounded) => 0.2f;

  public override void Selected() => SelectMode();

  private void SelectMode()
  {
    mode = Randoms.SHARED.GetProbability(0.5f) ? Mode.JUMP : (Randoms.SHARED.GetChance(2) ? Mode.PECK : Mode.WALK);
    nextModeChoice = Time.time + 1f;
  }

  public override void Action()
  {
    if (Time.fixedTime >= (double) nextModeChoice)
      SelectMode();
    if (IsGrounded())
    {
      if (mode == Mode.JUMP)
      {
        if (slimeBody.velocity.sqrMagnitude <= 0.010000000707805157)
        {
          slimeBody.AddForce(0.0f, Random.Range(0.5f * maxJump * slimeBody.mass, maxJump), 0.0f, ForceMode.Impulse);
          slimeBody.AddTorque(0.0f, Random.Range(-0.2f, 0.2f), 0.0f, ForceMode.Impulse);
          slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
          slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
          mode = Mode.WAIT;
        }
      }
      else if (mode != Mode.PECK && mode == Mode.WALK)
      {
        float num = 1f;
        slimeBody.AddForce(transform.forward * (walkForwardForce * slimeBody.mass * num * Time.fixedDeltaTime), ForceMode.Impulse);
        Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
        slimeBody.AddForceAtPosition(transform.forward * (2f * walkForwardForce * slimeBody.mass * num * Time.fixedDeltaTime), position, ForceMode.Impulse);
      }
    }
    animator.SetBool(animWalkId, mode == Mode.WALK);
    animator.SetBool(animPeckId, mode == Mode.PECK);
  }

  public void RegistryUpdate()
  {
    bool flag = IsGrounded();
    if (animator.GetBool(animGroundedId) && !flag)
      slimeAudio.Play(flapCue);
    animator.SetBool(animGroundedId, flag);
  }

  private enum Mode
  {
    JUMP,
    PECK,
    WALK,
    WAIT,
  }
}
