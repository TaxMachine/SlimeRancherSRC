// Decompiled with JetBrains decompiler
// Type: PuzzleSlotLockable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PuzzleSlotLockable : SRBehaviour
{
  [Tooltip("The slots we monitor.")]
  public PuzzleSlot[] slots;
  [Tooltip("Any objects we need to activate on unlock.")]
  public GameObject[] activateOnUnlock;
  [Tooltip("The sounds to play when the slots are opened.")]
  public SECTR_AudioCue[] slotCues;
  [Tooltip("If true, the game will be automatically save when the puzzle is unlocked.")]
  public bool autoSaveOnUnlock;

  public void Awake()
  {
    foreach (PuzzleSlot slot in slots)
      slot.RegisterLock(this);
  }

  public virtual void NotifySlotChanged(bool immediate = false)
  {
    if (!ShouldUnlock())
      return;
    ActivateOnUnlock();
  }

  private void ActivateOnUnlock()
  {
    if (activateOnUnlock == null)
      return;
    foreach (GameObject gameObject in activateOnUnlock)
    {
      gameObject.SetActive(true);
      if (autoSaveOnUnlock)
        SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveAllNow();
    }
  }

  public bool ShouldUnlock()
  {
    foreach (PuzzleSlot slot in slots)
    {
      if (slot.IsLocked())
        return false;
    }
    return true;
  }

  public SECTR_AudioCue GetCueForLastSlot()
  {
    int num = 0;
    foreach (PuzzleSlot slot in slots)
    {
      if (!slot.IsLocked())
        ++num;
    }
    return slotCues[Math.Max(0, Math.Min(slotCues.Length - 1, num - 1))];
  }

  public void PlayCue(SECTR_AudioCue cue) => SECTR_AudioSystem.Play(cue, transform.position, false);
}
