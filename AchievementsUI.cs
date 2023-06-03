// Decompiled with JetBrains decompiler
// Type: AchievementsUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : BaseUI
{
  [Tooltip("Internal achievement content panel.")]
  public GameObject achievementListPanel;
  [Tooltip("The prefab from which to create individual achievement panels.")]
  public GameObject achievementListItemPrefab;
  [Tooltip("Internal overall achievement count text.")]
  public TMP_Text overallText;
  private MessageBundle achieveBundle;
  private AchievementsDirector achieveDir;

  public override void Awake()
  {
    base.Awake();
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    achieveBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("achieve");
    foreach (AchievementsDirector.Achievement achievement in Enum.GetValues(typeof (AchievementsDirector.Achievement)))
      AddAchievement(achievement);
    int progress;
    int outOf;
    achieveDir.GetOverallProgress(out progress, out outOf);
    overallText.text = uiBundle.Get("m.achieve_overall_progress", progress, outOf);
  }

  public void AddAchievement(AchievementsDirector.Achievement achievement) => CreateAchievement(achievement).transform.SetParent(achievementListPanel.transform, false);

  private GameObject CreateAchievement(AchievementsDirector.Achievement achievement)
  {
    bool flag = achieveDir.HasAchievement(achievement);
    int progress;
    int outOf;
    achieveDir.GetProgress(achievement, out progress, out outOf);
    GameObject achievement1 = Instantiate(achievementListItemPrefab);
    TMP_Text component1 = achievement1.transform.Find("InfoPanel/Name").GetComponent<TMP_Text>();
    TMP_Text component2 = achievement1.transform.Find("InfoPanel/Requirement").GetComponent<TMP_Text>();
    Image component3 = achievement1.transform.Find("Icon").GetComponent<Image>();
    Image component4 = achievement1.transform.Find("Complete").GetComponent<Image>();
    GameObject gameObject = achievement1.transform.Find("InfoPanel/Progress").gameObject;
    StatusBar component5 = achievement1.transform.Find("InfoPanel/Progress/ProgressMeter").GetComponent<StatusBar>();
    TMP_Text component6 = achievement1.transform.Find("InfoPanel/Progress/ProgressText").GetComponent<TMP_Text>();
    string lowerInvariant = achievement.ToString().ToLowerInvariant();
    component1.text = achieveBundle.Xlate("t." + lowerInvariant);
    component2.text = achieveBundle.Xlate("m.reqmt." + lowerInvariant);
    component3.sprite = achieveDir.GetAchievementImage(lowerInvariant, achievement);
    if (!flag)
      component3.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    component4.enabled = flag;
    if (outOf > 1 && !flag)
    {
      gameObject.SetActive(true);
      component5.maxValue = outOf;
      component5.currValue = progress;
      component6.text = uiBundle.Get("m.achieve_progress", progress, outOf);
    }
    else
      gameObject.SetActive(false);
    return achievement1;
  }
}
