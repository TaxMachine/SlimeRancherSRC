// Decompiled with JetBrains decompiler
// Type: ScoreUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ScoreUI : SRBehaviour
{
  public GUISkin skin;
  private PlayerState player;

  private void Start() => player = SRSingleton<GameContext>.Instance == null ? null : SRSingleton<SceneContext>.Instance.PlayerState;

  private void OnGUI()
  {
    GUI.skin = skin;
    GUI.Label(new Rect(25f, 25f, 250f, 40f), "$" + player.GetCurrency());
  }
}
