// Decompiled with JetBrains decompiler
// Type: RadMeter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine.UI;

public class RadMeter : SRBehaviour
{
  public Image icon;
  private PlayerState player;
  private StatusBar energyBar;
  private bool wasVisible;
  private bool forceUpdate;

  private void Start()
  {
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    energyBar = GetComponent<StatusBar>();
    forceUpdate = true;
    Update();
  }

  private void Update()
  {
    int currRad = player.GetCurrRad();
    energyBar.currValue = currRad;
    bool flag = currRad > 0;
    if (flag == wasVisible && !forceUpdate)
      return;
    forceUpdate = false;
    for (int index = 0; index < transform.childCount; ++index)
      transform.GetChild(index).gameObject.SetActive(flag);
    icon.enabled = flag;
    wasVisible = flag;
  }
}
