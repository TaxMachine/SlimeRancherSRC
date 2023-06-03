// Decompiled with JetBrains decompiler
// Type: ExchangeRewardItemEntryUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class ExchangeRewardItemEntryUI : MonoBehaviour
{
  public Image icon;
  public Text amountText;
  private LookupDirector lookupDir;
  private ExchangeDirector exchangeDir;

  public void Awake()
  {
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    exchangeDir = SRSingleton<SceneContext>.Instance.ExchangeDirector;
  }

  public void SetEntry(ExchangeDirector.ItemEntry entry)
  {
    if (entry == null)
    {
      gameObject.SetActive(false);
    }
    else
    {
      gameObject.SetActive(true);
      if (entry.specReward != ExchangeDirector.NonIdentReward.NONE)
      {
        icon.sprite = exchangeDir.GetSpecRewardIcon(entry.specReward);
        amountText.text = GetCountDisplayForReward(entry.specReward);
      }
      else
      {
        icon.sprite = lookupDir.GetIcon(entry.id);
        amountText.text = entry.count.ToString();
      }
    }
  }

  private string GetCountDisplayForReward(ExchangeDirector.NonIdentReward specReward)
  {
    switch (specReward)
    {
      case ExchangeDirector.NonIdentReward.NEWBUCKS_SMALL:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MEDIUM:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_LARGE:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_HUGE:
      case ExchangeDirector.NonIdentReward.NEWBUCKS_MOCHI:
        return ExchangeBreakOnImpact.GetNewbucksRewardValue(specReward).ToString();
      case ExchangeDirector.NonIdentReward.TIME_EXTENSION_12H:
        return SRSingleton<SceneContext>.Instance.TimeDirector.FormatTimeMinutes(new int?(Mathf.FloorToInt(ExchangeBreakOnImpact.GetTimeExtensionRewardValue(specReward) * 60f)));
      default:
        return "1";
    }
  }
}
