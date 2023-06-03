// Decompiled with JetBrains decompiler
// Type: CannotStoreGifUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class CannotStoreGifUI : SRBehaviour
{
  public Toggle bufferForGifToggle;

  public void Awake() => bufferForGifToggle.isOn = SRSingleton<GameContext>.Instance.OptionsDirector.bufferForGif;

  public void OK() => Destroyer.Destroy(gameObject, "CannotStoreGifUI.OK");

  public void ToggleBufferForGif()
  {
    SRSingleton<GameContext>.Instance.OptionsDirector.bufferForGif = bufferForGifToggle.isOn;
    SRSingleton<GameContext>.Instance.AutoSaveDirector.SaveProfile();
  }
}
