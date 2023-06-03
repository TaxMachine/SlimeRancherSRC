// Decompiled with JetBrains decompiler
// Type: Microsoft.Xbox.ErrorEventArgs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

namespace Microsoft.Xbox
{
  public class ErrorEventArgs : EventArgs
  {
    public string ErrorCode { get; private set; }

    public string ErrorMessage { get; private set; }

    public ErrorEventArgs(string errorCode, string errorMessage)
    {
      ErrorCode = errorCode;
      ErrorMessage = errorMessage;
    }
  }
}
