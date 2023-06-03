// Decompiled with JetBrains decompiler
// Type: SwitchableSlidingDoor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SwitchableSlidingDoor : SwitchHandler.Switchable
{
  public Transform upTrans;
  public Transform downTrans;
  private SECTR_PointSource slidingSound;
  private Vector3 upPos;
  private Vector3 downPos;
  private float tgtSlideDownAmt;
  private float slideDownAmt;
  private bool forceMove;
  private float MOVE_RATE = 0.5f;
  private SwitchHandler.State currentState;

  public void Awake()
  {
    upPos = upTrans.position;
    downPos = downTrans.position;
    slidingSound = gameObject.GetComponent<SECTR_PointSource>();
  }

  public override void SetState(SwitchHandler.State state, bool immediate = false)
  {
    if (state != currentState)
    {
      slidingSound.Play();
      currentState = state;
    }
    tgtSlideDownAmt = state == SwitchHandler.State.UP ? 0.0f : 1f;
    if (!immediate)
      return;
    slideDownAmt = tgtSlideDownAmt;
    forceMove = true;
  }

  public void FixedUpdate()
  {
    if (!forceMove && slideDownAmt == (double) tgtSlideDownAmt)
      return;
    if (slideDownAmt < (double) tgtSlideDownAmt)
      slideDownAmt = Mathf.Min(tgtSlideDownAmt, slideDownAmt + MOVE_RATE * Time.fixedDeltaTime);
    else if (slideDownAmt > (double) tgtSlideDownAmt)
      slideDownAmt = Mathf.Max(tgtSlideDownAmt, slideDownAmt - MOVE_RATE * Time.fixedDeltaTime);
    transform.position = Vector3.Lerp(upPos, downPos, slideDownAmt);
    forceMove = false;
  }
}
