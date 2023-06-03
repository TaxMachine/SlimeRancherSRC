// Decompiled with JetBrains decompiler
// Type: GlareAtPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlareAtPlayer : SlimeSubbehaviour
{
  public float glareTime = 5f;
  public float minGlareDelay = 5f;
  public float minGlareDistance;
  public float maxGlareDistance = 20f;
  private bool isGlaring;
  private SlimeFeral feral;
  private float nextGlareTime;
  private float stopGlareTime;

  public override void Awake()
  {
    base.Awake();
    feral = GetComponent<SlimeFeral>();
  }

  public override float Relevancy(bool isGrounded)
  {
    if (isGrounded && feral.IsFeral() && Time.time >= (double) nextGlareTime)
    {
      float sqrMagnitude = (GetGotoPos(SRSingleton<SceneContext>.Instance.Player) - transform.position).sqrMagnitude;
      if (sqrMagnitude <= (double) maxGlareDistance && sqrMagnitude >= (double) minGlareDistance)
        return Randoms.SHARED.GetInRange(0.3f, 1f);
    }
    return 0.0f;
  }

  public override bool CanRethink() => !isGlaring;

  public override void Selected()
  {
    isGlaring = true;
    stopGlareTime = Time.time + glareTime;
  }

  public override void Action()
  {
    if (isGlaring && Time.time >= (double) stopGlareTime)
    {
      isGlaring = false;
      nextGlareTime = Time.time + minGlareDelay;
    }
    else
    {
      Vector3 vector3 = SRSingleton<SceneContext>.Instance.Player.transform.TransformPoint(new Vector3(0.0f, 0.0f, 2f)) - transform.position;
      double sqrMagnitude = vector3.sqrMagnitude;
      RotateTowards(vector3.normalized, 1f, 5f);
    }
  }
}
