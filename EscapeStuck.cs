// Decompiled with JetBrains decompiler
// Type: EscapeStuck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EscapeStuck : SlimeSubbehaviour
{
  private float verticalFactor = 0.5f;
  private float maxJump = 4f;
  private float? stuckSince;
  private SlimeAudio slimeAudio;
  private const float MIN_TIME_TO_ACT = 2f;
  private const float STUCK_VEL = 0.01f;
  private const float STUCK_VEL_SQR = 0.0001f;

  public override void Awake()
  {
    base.Awake();
    slimeAudio = GetComponent<SlimeAudio>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (!isGrounded && slimeBody.velocity.sqrMagnitude < 9.9999997473787516E-05)
    {
      if (!stuckSince.HasValue)
        stuckSince = new float?(Time.time);
    }
    else
      stuckSince = new float?();
    return stuckSince.HasValue && Time.time - (double) stuckSince.Value >= 2.0 ? 1f : 0.0f;
  }

  public override void Selected()
  {
  }

  public override void Action()
  {
    if (!stuckSince.HasValue)
      return;
    float num = 0.5f * maxJump * slimeBody.mass;
    slimeBody.AddForce(Random.Range(-num, num), verticalFactor * Random.Range(num, maxJump), Random.Range(-num, num), ForceMode.Impulse);
    slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
    stuckSince = new float?();
  }
}
