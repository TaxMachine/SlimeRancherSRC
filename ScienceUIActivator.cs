// Decompiled with JetBrains decompiler
// Type: ScienceUIActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class ScienceUIActivator : UIActivator
{
  public override bool CanActivate() => SRSingleton<SceneContext>.Instance.ProgressDirector.HasProgress(ProgressDirector.ProgressType.UNLOCK_LAB);
}
