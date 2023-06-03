// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Utility.InputModeStack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.Utility
{
  public class InputModeStack
  {
    private List<Entry> entryStack = new List<Entry>();

    public bool Push(SRInput.InputMode mode, int handle)
    {
      if (entryStack.Exists(entry => entry.handle == handle))
        return false;
      entryStack.Add(new Entry()
      {
        handle = handle,
        inputMode = mode
      });
      return true;
    }

    public void Pop(int handle) => entryStack.RemoveAll(entry => entry.handle == handle);

    public SRInput.InputMode Peek() => entryStack.Count != 0 ? entryStack.Last().inputMode : throw new Exception("Cannot peek at empty InputModeStack!");

    private class Entry
    {
      public int handle;
      public SRInput.InputMode inputMode;
    }
  }
}
