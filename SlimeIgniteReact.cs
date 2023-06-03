// Decompiled with JetBrains decompiler
// Type: SlimeIgniteReact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeIgniteReact : SRBehaviour, Ignitable
{
  public GameObject igniteFX;
  private SlimeFaceAnimator faceAnim;
  private SlimeEmotions emotions;
  private Rigidbody body;
  private bool selfIsIgniter;
  private float throttle;
  private const float BACK_FORCE = 1f;
  private const float UP_FORCE = 3f;
  private const float IGNITE_THROTTLE_TIME = 0.2f;
  private const float IGNITE_AGITATION = 0.5f;

  public void Awake()
  {
    faceAnim = GetComponent<SlimeFaceAnimator>();
    emotions = GetComponent<SlimeEmotions>();
    body = GetComponent<Rigidbody>();
    selfIsIgniter = GetComponent<FireSlimeIgnition>() != null;
  }

  public void Ignite(GameObject igniter)
  {
    if (selfIsIgniter || Time.time < (double) throttle)
      return;
    throttle = Time.time + 0.2f;
    if (igniteFX != null)
      SpawnAndPlayFX(igniteFX, transform.position, transform.rotation);
    faceAnim.SetTrigger("triggerAlarm");
    emotions.Adjust(SlimeEmotions.Emotion.AGITATION, 0.5f);
    body.AddForce((transform.position - igniter.transform.position).normalized * 1f + Vector3.up * 3f, ForceMode.Impulse);
  }
}
