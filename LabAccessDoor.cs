// Decompiled with JetBrains decompiler
// Type: LabAccessDoor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class LabAccessDoor : AccessDoor
{
  private PediaDirector pediaDir;
  private bool firstUpdate = true;

  public override void Awake()
  {
    base.Awake();
    pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
  }

  public override void Update()
  {
    base.Update();
    if (!firstUpdate)
      return;
    MaybeRecountProgress();
    firstUpdate = false;
  }

  public override bool MaybeRecountProgress()
  {
    if (!base.MaybeRecountProgress())
      return false;
    pediaDir.UnlockScience();
    return true;
  }
}
