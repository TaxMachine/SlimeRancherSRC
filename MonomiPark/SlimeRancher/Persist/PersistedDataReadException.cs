﻿// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.PersistedDataReadException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace MonomiPark.SlimeRancher.Persist
{
  public class PersistedDataReadException : Exception
  {
    public PersistedDataReadException(string message)
      : base(message)
    {
    }

    public PersistedDataReadException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
