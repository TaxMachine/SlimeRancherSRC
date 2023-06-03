// Decompiled with JetBrains decompiler
// Type: GameSummaryEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSummaryEntry : MonoBehaviour
{
  public Image gameIcon;
  public TMP_Text gameNameText;
  public string gameName;
  public string saveName;

  public void Init(GameData.Summary gameSummary)
  {
    gameIcon.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(gameSummary.iconId == Identifiable.Id.NONE ? Identifiable.Id.CARROT_VEGGIE : gameSummary.iconId);
    gameName = gameSummary.name;
    saveName = gameSummary.saveName;
    gameNameText.text = gameSummary.displayName;
  }

  public string GetGameName() => gameName;

  public string GetSaveName() => saveName;
}
