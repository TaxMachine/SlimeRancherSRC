// Decompiled with JetBrains decompiler
// Type: EmptyStateManager`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class EmptyStateManager<VComp> : BaseStateManager<EmptyState, VComp> where VComp : vp_Component
{
  public EmptyStateManager(VComp managedComponent)
    : base(managedComponent)
  {
    states = new EmptyState[1];
    AddState(new EmptyState("Default"), 0);
  }

  public override void ApplyState(EmptyState state)
  {
  }
}
