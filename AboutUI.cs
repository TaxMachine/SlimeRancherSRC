// Decompiled with JetBrains decompiler
// Type: AboutUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine.UI;

public class AboutUI : BaseUI
{
  public Button creditsBtn;
  public Button dataPrivacyBtn;

  public void OnEnable()
  {
    creditsBtn.gameObject.SetActive(SRSingleton<SceneContext>.Instance.AchievementsDirector.HasAchievement(AchievementsDirector.Achievement.FINISH_ADVENTURE));
    dataPrivacyBtn.gameObject.SetActive(true);
    if (creditsBtn.gameObject.activeSelf)
    {
      creditsBtn.gameObject.AddComponent<InitSelected>();
    }
    else
    {
      if (!dataPrivacyBtn.gameObject.activeSelf)
        return;
      dataPrivacyBtn.gameObject.AddComponent<InitSelected>();
    }
  }

  public void ShowCredits()
  {
    creditsBtn.interactable = false;
    SRSingleton<GameContext>.Instance.UITemplates.CreateCreditsPrefab(true).GetComponent<CreditsUI>().OnCreditsEnded += OnCreditsEnded;
  }

  private void OnCreditsEnded() => creditsBtn.interactable = true;
}
