// Decompiled with JetBrains decompiler
// Type: PuzzleDoorLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PuzzleDoorLock : PuzzleSlotLockable
{
  [Tooltip("The door we control the lock on.")]
  public AccessDoor door;

  public override void NotifySlotChanged(bool immediate = false)
  {
    if (ShouldUnlock() && door.CurrState == AccessDoor.State.LOCKED)
      door.CurrState = AccessDoor.State.CLOSED;
    base.NotifySlotChanged(immediate);
  }
}
