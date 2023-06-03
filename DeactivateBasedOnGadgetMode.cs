// Decompiled with JetBrains decompiler
// Type: DeactivateBasedOnGadgetMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeactivateBasedOnGadgetMode : MonoBehaviour
{
  public GameObject toDeactivate;
  public bool activateOnModeOff;
  private PlayerState playerState;

  public void Awake() => playerState = SRSingleton<SceneContext>.Instance.PlayerState;

  public void Update() => toDeactivate.SetActive(playerState.InGadgetMode ^ activateOnModeOff);
}
