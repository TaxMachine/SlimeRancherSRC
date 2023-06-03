// Decompiled with JetBrains decompiler
// Type: PhaseableObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public abstract class PhaseableObject : SRBehaviour
{
  public abstract bool ReadyToPhase();

  public abstract void PhaseOut();

  public abstract void PhaseIn();
}
