// Decompiled with JetBrains decompiler
// Type: AccessDoor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections;
using UnityEngine;

public class AccessDoor : IdHandler, AccessDoorModel.Participant
{
  public DoorPurchaseItem doorPurchase;
  public PediaDirector.Id lockedRegionId;
  public AccessDoor[] linkedDoors;
  [Tooltip("Progress to record when the door is unlocked.")]
  public ProgressDirector.ProgressType[] progress;
  public SECTR_AudioCue openCue;
  public float openCueDelay = 3f;
  [Tooltip("Other elements to include in the open/close animation.")]
  public Animator[] externalAnimators;
  public GameObject[] deactivateOnImmediateOpen;
  private int animOpenId;
  private int animOpenImmediateId;
  private AccessDoorModel model;
  private bool updateRequest = true;
  private bool updateRequestImmediate;
  private int delayTimeout = -1;
  private object timeoutSeconds;

  public virtual void Awake()
  {
    animOpenId = Animator.StringToHash("Open");
    animOpenImmediateId = Animator.StringToHash("OpenImmediate");
    foreach (Component externalAnimator in externalAnimators)
      externalAnimator.gameObject.AddComponent<DoorAnimatorUpdater>().Init(this);
    SRSingleton<SceneContext>.Instance.GameModel.RegisterDoor(id, gameObject);
  }

  public virtual void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterDoor(id);
  }

  public void OnEnable()
  {
    if (model == null)
      return;
    ForceUpdate(true);
  }

  public void InitModel(AccessDoorModel model) => model.progress = progress;

  public void SetModel(AccessDoorModel model)
  {
    this.model = model;
    MaybeRecountProgress();
    ForceUpdate(true);
  }

  public State CurrState
  {
    get => model.state;
    set
    {
      if (openCue != null && value == State.OPEN && model.state != State.OPEN)
        StartCoroutine(DelayedPlayCue(openCueDelay));
      model.state = value;
      MaybeRecountProgress();
      ForceUpdate(false);
    }
  }

  public IEnumerator DelayedPlayCue(float delay)
  {
    // ISSUE: reference to a compiler-generated field
    int num = delayTimeout;
    AccessDoor accessDoor = this;
    if (num != 0)
    {
      if (num != 1)
        yield return false;
      // ISSUE: reference to a compiler-generated field
      delayTimeout = -1;
      SECTR_AudioSystem.Play(accessDoor.openCue, accessDoor.transform.position, false);
      yield return false;
    }
    // ISSUE: reference to a compiler-generated field
    delayTimeout = -1;
    // ISSUE: reference to a compiler-generated field
    timeoutSeconds = new WaitForSeconds(delay);
    // ISSUE: reference to a compiler-generated field
    delayTimeout = 1;
    yield return true;
  }

  public virtual bool MaybeRecountProgress()
  {
    if (CurrState == State.LOCKED)
      return false;
    ProgressDirector progressDirector = SRSingleton<SceneContext>.Instance.ProgressDirector;
    foreach (ProgressDirector.ProgressType type in progress)
    {
      int count = 0;
      foreach (AccessDoorModel accessDoorModel in SRSingleton<SceneContext>.Instance.GameModel.AllDoors().Values)
      {
        if (accessDoorModel.state != State.LOCKED && Array.IndexOf(accessDoorModel.progress, type) != -1)
          ++count;
      }
      progressDirector.SetProgress(type, count);
    }
    return true;
  }

  private void ForceUpdate(bool immediate)
  {
    updateRequest = true;
    updateRequestImmediate |= immediate;
    ForceUpdateBarrierController();
    foreach (Component externalAnimator in externalAnimators)
      externalAnimator.GetComponent<DoorAnimatorUpdater>().ForceUpdate();
  }

  public virtual void Update()
  {
    if (!updateRequest)
      return;
    ForceUpdateBarrierController();
    ForceUpdateAnimator();
    updateRequest = false;
    updateRequestImmediate = false;
  }

  private void ForceUpdateBarrierController()
  {
    BarrierController componentInChildren = GetComponentInChildren<BarrierController>();
    if (updateRequestImmediate && CurrState == State.OPEN)
    {
      if (componentInChildren != null)
        componentInChildren.SetIsOpen(true);
      if (deactivateOnImmediateOpen != null)
      {
        foreach (GameObject gameObject in deactivateOnImmediateOpen)
          gameObject.SetActive(false);
      }
    }
    if (!(componentInChildren != null))
      return;
    componentInChildren.SetIsOpen(CurrState == State.OPEN);
  }

  private void ForceUpdateAnimator()
  {
    Animator componentInChildren = GetComponentInChildren<Animator>();
    if (updateRequestImmediate && CurrState == State.OPEN && componentInChildren != null)
      componentInChildren.SetTrigger(animOpenImmediateId);
    if (!(componentInChildren != null))
      return;
    componentInChildren.SetBool(animOpenId, CurrState == State.OPEN);
  }

  protected override string IdPrefix() => "door";

  public enum State
  {
    LOCKED,
    OPEN,
    CLOSED,
  }

  [Serializable]
  public class DoorPurchaseItem
  {
    public Sprite icon;
    public Sprite img;
    public int cost;
  }

  private class DoorAnimatorUpdater : MonoBehaviour
  {
    private AccessDoor door;
    private int animOpenId;
    private bool updateRequest = true;

    public void Init(AccessDoor door)
    {
      this.door = door;
      animOpenId = Animator.StringToHash("Open");
      ForceUpdate();
    }

    public void OnEnable() => ForceUpdate();

    public void Update()
    {
      if (!updateRequest)
        return;
      ForceUpdateBarrierController();
      ForceUpdateAnimator();
      updateRequest = false;
    }

    public void ForceUpdate()
    {
      updateRequest = true;
      ForceUpdateBarrierController();
    }

    private void ForceUpdateBarrierController()
    {
      if (!(door != null) || door.model == null)
        return;
      BarrierController componentInChildren = GetComponentInChildren<BarrierController>();
      if (!(componentInChildren != null))
        return;
      componentInChildren.SetIsOpen(door.model.state == State.OPEN);
    }

    private void ForceUpdateAnimator()
    {
      if (!(door != null) || door.model == null)
        return;
      Animator componentInChildren = GetComponentInChildren<Animator>();
      if (!(componentInChildren != null))
        return;
      componentInChildren.SetBool(animOpenId, door.model.state == State.OPEN);
    }
  }
}
