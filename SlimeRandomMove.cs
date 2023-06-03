// Decompiled with JetBrains decompiler
// Type: SlimeRandomMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SlimeRandomMove : SlimeSubbehaviour
{
  public float verticalFactor = 1f;
  public float scootSpeedFactor = 1f;
  private float maxJump = 6f;
  private SlimeAudio slimeAudio;
  private float nextJumpTime;
  private Mode mode;
  private float modeChangeTime;
  private Vector3 scootDir;
  private const float TIME_BETWEEN_JUMPS = 1f;
  private const float MODE_CHANGE_LENGTH = 10f;
  private const float SCOOT_CYCLE_TIME = 1f;
  private const float SCOOT_CYCLE_FACTOR = 6.28318548f;
  private static Dictionary<Mode, float> MODE_WEIGHTS = new Dictionary<Mode, float>();
  private const float MAX_VEL_TO_BOUNCE = 5f;
  private const float SQR_MAX_VEL_TO_BOUNCE = 25f;

  static SlimeRandomMove()
  {
    MODE_WEIGHTS[Mode.IDLE] = 0.2f;
    MODE_WEIGHTS[Mode.SCOOT] = 0.3f;
    MODE_WEIGHTS[Mode.JUMP] = 0.5f;
  }

  public override void Awake()
  {
    base.Awake();
    slimeAudio = GetComponent<SlimeAudio>();
  }

  public override void Start() => base.Start();

  public override float Relevancy(bool isGrounded) => 0.2f;

  public override void Selected()
  {
  }

  public override void Action()
  {
    if (!IsGrounded())
      return;
    if (Time.fixedTime > (double) modeChangeTime)
    {
      mode = Randoms.SHARED.Pick(MODE_WEIGHTS, Mode.IDLE);
      modeChangeTime = Time.time + 10f;
      float f = Mathf.Atan2(transform.forward.z, transform.forward.x) + Randoms.SHARED.GetInRange(-0.5f, 0.5f);
      scootDir = new Vector3(Mathf.Cos(f), 0.0f, Mathf.Sin(f));
    }
    switch (mode)
    {
      case Mode.SCOOT:
        RotateTowards(scootDir, 1f, 1f);
        float num1 = ScootCycleSpeed();
        slimeBody.AddForce(transform.forward * (150f * slimeBody.mass * scootSpeedFactor * Time.fixedDeltaTime * num1));
        Vector3 position = transform.position + Vector3.down * (0.5f * transform.localScale.y);
        slimeBody.AddForceAtPosition(transform.forward * (270f * slimeBody.mass * Time.fixedDeltaTime * num1), position);
        break;
      case Mode.JUMP:
        if (Time.time <= (double) nextJumpTime || slimeBody.velocity.sqrMagnitude > 25.0 || !IsGrounded())
          break;
        float num2 = 0.5f * maxJump * slimeBody.mass;
        slimeBody.AddForce(Randoms.SHARED.GetInRange(-num2, num2), verticalFactor * Randoms.SHARED.GetInRange(num2, maxJump * slimeBody.mass), Randoms.SHARED.GetInRange(-num2, num2), ForceMode.Impulse);
        slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
        slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
        nextJumpTime = Time.fixedTime + 1f;
        break;
    }
  }

  protected float ScootCycleSpeed() => Mathf.Sin(Time.fixedTime * 6.28318548f) + 1f;

  private enum Mode
  {
    IDLE,
    SCOOT,
    JUMP,
  }
}
