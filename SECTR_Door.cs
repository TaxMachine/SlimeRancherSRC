// Decompiled with JetBrains decompiler
// Type: SECTR_Door
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Animator))]
[AddComponentMenu("SECTR/Audio/SECTR Door")]
public class SECTR_Door : MonoBehaviour
{
  private int controlParam;
  private int canOpenParam;
  private int closedState;
  private int waitingState;
  private int openingState;
  private int openState;
  private int closingState;
  private int lastState;
  private Animator cachedAnimator;
  private int openCount;
  [SECTR_ToolTip("The portal this door affects (if any).")]
  public SECTR_Portal Portal;
  [SECTR_ToolTip("The name of the control param in the door.")]
  public string ControlParam = "Open";
  [SECTR_ToolTip("The name of the control param that indicates if we are allowed to open.")]
  public string CanOpenParam = "CanOpen";
  [SECTR_ToolTip("The full name (layer and state) of the Open state in the Animation Controller.")]
  public string OpenState = "Base Layer.Open";
  [SECTR_ToolTip("The full name (layer and state) of the Closed state in the Animation Controller.")]
  public string ClosedState = "Base Layer.Closed";
  [SECTR_ToolTip("The full name (layer and state) of the Opening state in the Animation Controller.")]
  public string OpeningState = "Base Layer.Opening";
  [SECTR_ToolTip("The full name (layer and state) of the Closing state in the Animation Controller.")]
  public string ClosingState = "Base Layer.Closing";
  [SECTR_ToolTip("The full name (layer and state) of the Wating state in the Animation Controller.")]
  public string WaitingState = "Base Layer.Waiting";

  public void OpenDoor() => ++openCount;

  public void CloseDoor() => --openCount;

  public bool IsFullyOpen() => cachedAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == openState;

  public bool IsClosed() => cachedAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == closedState;

  protected virtual void OnEnable()
  {
    cachedAnimator = GetComponent<Animator>();
    controlParam = Animator.StringToHash(ControlParam);
    canOpenParam = Animator.StringToHash(CanOpenParam);
    closedState = Animator.StringToHash(ClosedState);
    waitingState = Animator.StringToHash(WaitingState);
    openingState = Animator.StringToHash(OpeningState);
    openState = Animator.StringToHash(OpenState);
    closingState = Animator.StringToHash(ClosingState);
  }

  private void Start()
  {
    if (controlParam != 0)
      cachedAnimator.SetBool(controlParam, false);
    if (canOpenParam != 0)
      cachedAnimator.SetBool(canOpenParam, false);
    if ((bool) (Object) Portal)
      Portal.SetFlag(SECTR_Portal.PortalFlags.Closed, true);
    openCount = 0;
    lastState = closedState;
    SendMessage("OnClose", SendMessageOptions.DontRequireReceiver);
  }

  private void Update()
  {
    bool flag = CanOpen();
    if (canOpenParam != 0)
      cachedAnimator.SetBool(canOpenParam, flag);
    if (controlParam != 0 && (flag || canOpenParam != 0))
    {
      if (openCount > 0)
        cachedAnimator.SetBool(controlParam, true);
      else
        cachedAnimator.SetBool(controlParam, false);
    }
    int fullPathHash = cachedAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
    if (fullPathHash != lastState)
    {
      if (fullPathHash == closedState)
        SendMessage("OnClose", SendMessageOptions.DontRequireReceiver);
      if (fullPathHash == waitingState)
        SendMessage("OnWaiting", SendMessageOptions.DontRequireReceiver);
      else if (fullPathHash == openingState)
        SendMessage("OnOpening", SendMessageOptions.DontRequireReceiver);
      if (fullPathHash == openState)
        SendMessage("OnOpen", SendMessageOptions.DontRequireReceiver);
      else if (fullPathHash == closingState)
        SendMessage("OnClosing", SendMessageOptions.DontRequireReceiver);
      lastState = fullPathHash;
    }
    if (!(bool) (Object) Portal)
      return;
    Portal.SetFlag(SECTR_Portal.PortalFlags.Closed, IsClosed());
  }

  protected virtual void OnTriggerEnter(Collider other) => ++openCount;

  protected virtual void OnTriggerExit(Collider other) => --openCount;

  protected virtual bool CanOpen() => true;
}
