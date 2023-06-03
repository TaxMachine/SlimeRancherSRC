// Decompiled with JetBrains decompiler
// Type: HudUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : SRSingleton<HudUI>
{
  [Tooltip("UIContainer parent GameObject.")]
  public GameObject uiContainer;
  [Tooltip("Reference to the EnergyMeter child.")]
  public EnergyMeter energyMeter;
  public TMP_Text currencyText;
  public TMP_Text keysText;
  public TMP_Text dayText;
  public TMP_Text timeText;
  public TMP_Text debugText;
  public Image timeIcon;
  public Image mailIcon;
  public Image keysIcon;
  public GameObject partnerArea;
  public TMP_Text partnerLevelText;
  public Image autosaveImg;
  public GameObject keyGainFX;
  private PlayerState player;
  private TimeDirector timeDir;
  private MailDirector mailDir;
  private ProgressDirector progressDir;
  private AutoSaveDirector autosaveDir;
  private OptionsDirector optionsDir;
  private Hashtable scaleToTweenArgs;
  private Hashtable scaleBackTweenArgs;
  private float mailInitY;
  private const float OFFSET_SPACING = 50f;
  private const float GAME_SAVED_NOTIFICATION_DURATION = 5f;
  private bool priorShowMinimalHud;
  [Tooltip("Game objects in this list will be hidden when Minimal HUD is enabled.")]
  [SerializeField]
  private GameObject[] disabledOnMinimalHud;
  private int lastTime = -1;
  private int lastCurrency = -1;
  private int lastKeys = -1;
  private int lastPartnerLevel = -1;

  public override void Awake()
  {
    base.Awake();
    optionsDir = SRSingleton<GameContext>.Instance.OptionsDirector;
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    mailDir = SRSingleton<SceneContext>.Instance.MailDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    autosaveDir = SRSingleton<GameContext>.Instance.AutoSaveDirector;
    mailInitY = mailIcon.rectTransform.anchoredPosition.y;
  }

  public void Start()
  {
    Update();
    debugText.gameObject.SetActive(false);
    debugText.text = string.Empty;
  }

  public void Update()
  {
    bool showMinimalHud = optionsDir.GetShowMinimalHUD();
    if (priorShowMinimalHud != showMinimalHud)
    {
      bool flag = !showMinimalHud;
      for (int index = 0; index < disabledOnMinimalHud.Length; ++index)
      {
        GameObject gameObject = disabledOnMinimalHud[index];
        if (gameObject != null)
          gameObject.SetActive(flag);
      }
      priorShowMinimalHud = showMinimalHud;
    }
    if (!showMinimalHud)
    {
      int num = timeDir.CurrTime();
      if (num != lastTime)
      {
        dayText.text = timeDir.CurrDayString();
        timeText.text = timeDir.CurrTimeString();
        timeIcon.sprite = timeDir.CurrTimeIcon();
        lastTime = num;
      }
      int displayedCurrency = player.GetDisplayedCurrency();
      if (displayedCurrency != lastCurrency)
      {
        currencyText.text = displayedCurrency.ToString();
        lastCurrency = displayedCurrency;
      }
      int keys = player.GetKeys();
      if (keys != lastKeys)
      {
        if (keys > 0)
        {
          string str = keys.ToString();
          ScaleUpAndBack(keysIcon.gameObject);
          ScaleUpAndBack(keysText.gameObject);
          keysText.text = str;
        }
        keysIcon.enabled = keys > 0;
        keysText.enabled = keys > 0;
        lastKeys = keys;
      }
      bool flag1 = false;
      int progress = progressDir.GetProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER);
      if (progress != lastPartnerLevel)
      {
        partnerArea.SetActive(progress > 0);
        partnerLevelText.text = progress.ToString();
        lastPartnerLevel = progress;
        flag1 = true;
      }
      bool flag2 = mailDir.HasNewMail();
      if (flag2 != mailIcon.enabled)
      {
        mailIcon.enabled = flag2;
        flag1 = true;
      }
      if (flag1)
        mailIcon.rectTransform.anchoredPosition = (Vector3) mailIcon.rectTransform.anchoredPosition with
        {
          y = (mailInitY + (progress > 0 ? 0.0f : 50f))
        };
    }
    autosaveImg.enabled = Time.time - (double) autosaveDir.GetLastSaveTime() < 5.0;
  }

  private void ScaleUpAndBack(GameObject gameObject) => DOTween.Sequence().Append(gameObject.transform.DOScale(2f, 0.25f).SetEase(Ease.Linear)).Append(gameObject.transform.DOScale(1f, 0.25f).SetEase(Ease.Linear));
}
