// Decompiled with JetBrains decompiler
// Type: DLCPurgedException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

public class DLCPurgedException : Exception
{
  public readonly DLCPackage.Id[] packages;

  public DLCPurgedException(DLCPackage.Id[] packages) => this.packages = packages;
}
