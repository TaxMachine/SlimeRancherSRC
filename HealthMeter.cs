// Decompiled with JetBrains decompiler
// Type: HealthMeter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class HealthMeter : SRBehaviour
{
  private PlayerState player;
  private StatusBar statusBar;

  public void Start()
  {
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    statusBar = GetComponent<StatusBar>();
    Update();
  }

  public void Update()
  {
    statusBar.currValue = player.GetCurrHealth();
    statusBar.maxValue = player.GetMaxHealth();
  }
}
