// Decompiled with JetBrains decompiler
// Type: GameCoreXboxStartScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using InControl;
using UnityEngine;
using UnityEngine.UI;

public class GameCoreXboxStartScreen : MonoBehaviour
{
  public Text actionText;
  public Text gamerNameText;
  public GameObject happySlime;
  private EngagementScreenActions engagementActions;

  private class EngagementScreenActions : PlayerActionSet
  {
    public PlayerAction Engage;

    public EngagementScreenActions() => Engage = CreatePlayerAction(nameof (Engage));
  }
}
