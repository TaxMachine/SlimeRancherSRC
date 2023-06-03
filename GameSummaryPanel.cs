// Decompiled with JetBrains decompiler
// Type: GameSummaryPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSummaryPanel : MonoBehaviour
{
  public Image gameIcon;
  public TMP_Text gameNameText;
  public TMP_Text modeText;
  public TMP_Text modeDescText;
  public TMP_Text dayText;
  public TMP_Text currencyText;
  public TMP_Text pediaText;
  public TMP_Text versionText;
  public GameObject validPanel;
  public GameObject invalidPanel;

  public void Init(GameData.Summary gameSummary)
  {
    MessageBundle bundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui");
    gameIcon.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(gameSummary.iconId == Identifiable.Id.NONE ? Identifiable.Id.CARROT_VEGGIE : gameSummary.iconId);
    gameNameText.text = gameSummary.displayName;
    string lowerInvariant = gameSummary.gameMode.ToString().ToLowerInvariant();
    modeText.text = bundle.Xlate("m.gamemode_" + lowerInvariant);
    modeDescText.text = bundle.Xlate("m.desc.gamemode_" + lowerInvariant);
    dayText.text = bundle.Xlate(MessageUtil.Tcompose("m.day", gameSummary.day + 1));
    currencyText.text = gameSummary.currency.ToString();
    int num = Math.Max(0, gameSummary.pediaCount - SRSingleton<SceneContext>.Instance.PediaDirector.GetUnlockedCount());
    pediaText.text = bundle.Xlate(MessageUtil.Tcompose("l.pedia_count", num));
    versionText.text = gameSummary.version == null ? "pre-0.3.0" : gameSummary.version;
    validPanel.SetActive(!gameSummary.isInvalid);
    invalidPanel.SetActive(gameSummary.isInvalid);
  }

  public string GetGameName() => gameNameText.text;
}
