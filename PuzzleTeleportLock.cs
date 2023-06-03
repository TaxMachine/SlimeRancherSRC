// Decompiled with JetBrains decompiler
// Type: PuzzleTeleportLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class PuzzleTeleportLock : PuzzleSlotLockable
{
  [Tooltip("The teleporter we control the activation on.")]
  public TeleportSource teleporter;
  public float activateDelay;
  public GameObject activateFX;
  public GameObject activateFXParent;

  public override void NotifySlotChanged(bool immediate = false)
  {
    if (ShouldUnlock())
    {
      if (immediate)
        teleporter.ExternalActivate();
      else
        StartCoroutine(DelayedActivateSequence());
    }
    base.NotifySlotChanged(immediate);
  }

  public IEnumerator DelayedActivateSequence()
  {
    InstantiateDynamic(activateFX, activateFXParent.transform.position, activateFXParent.transform.rotation);
    yield return new WaitForSeconds(activateDelay);
    teleporter.ExternalActivate();
  }
}
