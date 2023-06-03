// Decompiled with JetBrains decompiler
// Type: AchievementAwardUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementAwardUI : PopupUI<AchievementsDirector.Achievement>
{
  public Image img;
  public TMP_Text titleText;
  [Tooltip("If not killed before then, how long this popup will stick around.")]
  public float lifetime = 10f;
  protected float timeOfDeath;
  private AchievementsDirector achieveDir;

  public virtual void Awake()
  {
    timeOfDeath = Time.time + lifetime;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    achieveDir.PopupActivated(this);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    achieveDir.PopupDeactivated(this);
  }

  public void Update()
  {
    if (Time.time < (double) timeOfDeath)
      return;
    Destroyer.Destroy(gameObject, "AchievementAwardUI.Update");
  }

  public override void OnBundleAvailable(MessageDirector msgDir)
  {
    string lowerInvariant = Enum.GetName(typeof (AchievementsDirector.Achievement), idEntry).ToLowerInvariant();
    Sprite achievementImage = achieveDir.GetAchievementImage(lowerInvariant, idEntry);
    MessageBundle bundle = msgDir.GetBundle("achieve");
    img.sprite = achievementImage;
    titleText.text = bundle.Get("t." + lowerInvariant);
  }
}
