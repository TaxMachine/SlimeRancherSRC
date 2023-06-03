// Decompiled with JetBrains decompiler
// Type: SlimeHover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeHover : SlimeSubbehaviour, Collidable
{
  private const float HOVER_MIN_DELAY = 10f;
  private const float HOVER_MAX_DELAY = 25f;
  private float nextHoverTime;
  private float endTime;
  private Vector3 floatDir;
  private CalmedByWaterSpray calmed;
  private bool cancelHover;
  private const float HOVER_TIME = 6f;
  private const float HOVER_HEIGHT = 5f;
  private const float INV_HOVER_HEIGHT = 0.2f;

  public override void Awake()
  {
    base.Awake();
    calmed = GetComponent<CalmedByWaterSpray>();
  }

  public override void Start()
  {
    base.Start();
    cancelHover = false;
    nextHoverTime = Time.fixedTime + Randoms.SHARED.GetFloat(HoverDelay());
  }

  public override float Relevancy(bool isGrounded) => nextHoverTime <= (double) Time.fixedTime ? 0.3f : 0.0f;

  public void ProcessCollisionEnter(Collision coll)
  {
    if (!(coll.rigidbody == null))
      return;
    foreach (ContactPoint contact in coll.contacts)
    {
      if (contact.point.y > transform.position.y + 0.25 * transform.lossyScale.y)
        cancelHover = true;
    }
  }

  public void ProcessCollisionExit(Collision col)
  {
  }

  public override void Selected()
  {
    endTime = Time.time + 6f;
    nextHoverTime = endTime + HoverDelay();
    floatDir = new Vector3(Randoms.SHARED.GetInRange(-1f, 1f), 0.0f, Randoms.SHARED.GetInRange(-1f, 1f));
  }

  public override void Deselected() => base.Deselected();

  public void FixedUpdate()
  {
    if (!calmed.IsCalmed())
      return;
    nextHoverTime += Time.fixedDeltaTime;
  }

  public override void Action()
  {
    if (cancelHover)
      return;
    RaycastHit hitInfo;
    if (Physics.Raycast(slimeBody.position, -Vector3.up, out hitInfo, GetHoverHeight()))
      slimeBody.AddForce(Vector3.up * (GetHoverAccel() * slimeBody.mass * Time.fixedDeltaTime) * (float) (1.0 - hitInfo.distance * (double) GetInvHoverHeight()));
    slimeBody.AddForce(floatDir * (100f * slimeBody.mass * Time.fixedDeltaTime));
  }

  protected virtual float GetHoverAccel() => 1200f;

  protected virtual float GetHoverHeight() => 5f;

  protected virtual float GetInvHoverHeight() => 0.2f;

  public override bool CanRethink() => cancelHover || Time.time >= (double) endTime;

  private float HoverDelay() => Mathf.Lerp(10f, 25f, Mathf.Clamp(Randoms.SHARED.GetInRange(-0.1f, 0.1f) + (1f - emotions.GetCurr(SlimeEmotions.Emotion.AGITATION)), 0.0f, 1f));
}
