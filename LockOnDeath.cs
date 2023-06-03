// Decompiled with JetBrains decompiler
// Type: LockOnDeath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LockOnDeath : SRSingleton<LockOnDeath>
{
  public OnLockChanged onLockChanged;
  public SECTR_AudioCue playerWakeCue;
  public SECTR_AudioCue timePassingCue;
  private bool locked;
  private double unlockWorldTime;
  private TimeDirector timeDir;
  private AchievementsDirector achieveDir;
  private vp_FPController fpController;
  private vp_FPInput fpInput;
  private vp_FPCamera fpCamera;
  private vp_FPPlayerEventHandler fpEvents;
  private WeaponVacuum vacuum;
  private SECTR_AudioCueInstance timePassingInstance;
  private UnityAction onLockComplete;
  private SRInput input;
  private SRInput.InputMode previousInputMode;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    input = SRInput.Instance;
    fpController = GetComponentInChildren<vp_FPController>();
    fpInput = GetComponentInChildren<vp_FPInput>();
    fpCamera = GetComponentInChildren<vp_FPCamera>();
    fpEvents = GetComponentInChildren<vp_FPPlayerEventHandler>();
    vacuum = GetComponentInChildren<WeaponVacuum>();
    SRSingleton<SceneContext>.Instance.PlayerState.onEndGame += () => unlockWorldTime = timeDir.WorldTime();
  }

  public void LockUntil(double unlockWorldTime, float delayTime, UnityAction onComplete = null)
  {
    achieveDir.SetStat(AchievementsDirector.GameDoubleStat.LAST_SLEPT, timeDir.WorldTime());
    this.unlockWorldTime = unlockWorldTime;
    locked = true;
    Freeze();
    if (onLockChanged != null)
      onLockChanged(true);
    onLockComplete = onComplete;
    StartCoroutine(DelayedFastForward(delayTime));
  }

  private IEnumerator DelayedFastForward(float delayTime)
  {
    if (delayTime > 0.0)
      yield return new WaitForSeconds(delayTime);
    timeDir.FastForwardTo(unlockWorldTime);
    if (timePassingInstance.Active)
      timePassingInstance.Stop(true);
    if (timePassingCue != null)
      timePassingInstance = SECTR_AudioSystem.Play(timePassingCue, Vector3.zero, true);
  }

  public void Update()
  {
    if (!locked || !timeDir.HasReached(unlockWorldTime))
      return;
    locked = false;
    Unfreeze();
    if (onLockChanged != null)
      onLockChanged(false);
    timePassingInstance.Stop(false);
    if (playerWakeCue != null)
    {
      SECTR_AudioSource component = GetComponent<SECTR_AudioSource>();
      component.Cue = playerWakeCue;
      component.Play();
    }
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow();
    achieveDir.SetStat(AchievementsDirector.GameDoubleStat.LAST_AWOKE, timeDir.WorldTime());
    if (onLockComplete == null)
      return;
    onLockComplete();
  }

  public void Freeze()
  {
    if (fpController != null)
    {
      fpController.SetState(nameof (Freeze));
      fpController.Stop();
    }
    if (fpCamera != null)
      fpCamera.SetState(nameof (Freeze));
    fpEvents.Jump.Stop();
    fpEvents.Jetpack.Stop();
    vacuum.enabled = false;
    SECTR_AudioSystem.MuteNonUISFX(true);
    previousInputMode = input.GetInputMode();
    input.SetInputMode(SRInput.InputMode.NONE, gameObject.GetInstanceID());
  }

  public void Unfreeze()
  {
    if (fpController != null)
      fpController.SetState("Freeze", false);
    if (fpCamera != null)
      fpCamera.SetState("Freeze", false);
    vacuum.enabled = true;
    SECTR_AudioSystem.MuteNonUISFX(false);
    input.ClearInputMode(gameObject.GetInstanceID());
  }

  public bool Locked() => locked;

  public delegate void OnLockChanged(bool locked);
}
