// Decompiled with JetBrains decompiler
// Type: GordoDisplayOnMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GordoDisplayOnMap : DisplayOnMap
{
  private GordoEat gordoEat;

  public override void Awake()
  {
    base.Awake();
    gordoEat = GetComponent<GordoEat>();
  }

  public override bool ShowOnMap()
  {
    if (!base.ShowOnMap())
      return false;
    int eatenCount = gordoEat.GetEatenCount();
    if (SRSingleton<SceneContext>.Instance.GameModel.currGameMode == PlayerState.GameMode.TIME_LIMIT_V2)
    {
      GordoNearBurstOnGameMode component = gameObject.GetComponent<GordoNearBurstOnGameMode>();
      eatenCount -= component == null ? 0 : (int) (gordoEat.GetTargetCount() - component.remaining);
    }
    return eatenCount > 0;
  }
}
