﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.Xbox.GameSaveLoadedArgs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Microsoft.Xbox
{
  public class GameSaveLoadedArgs : EventArgs
  {
    public byte[] Data { get; private set; }

    public GameSaveLoadedArgs(byte[] data) => Data = data;
  }
}
