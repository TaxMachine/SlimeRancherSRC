// Decompiled with JetBrains decompiler
// Type: RanchHouseUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RanchHouseUI : BaseUI, LocationalUI
{
  public GameObject mailUI;
  public GameObject partnerUI;
  public GameObject manageDLCPrefab;
  public GameObject appearancePrefab;
  public TMP_Text dayText;
  public TMP_Text timeText;
  public Image timeIcon;
  public Image mailIcon;
  public GameObject mailHighlight;
  public TMP_Text mailHighlightText;
  public GameObject partnerArea;
  public TMP_Text partnerLevelText;
  public GameObject mainUI;
  public Image obscurer;
  public GameObject buttonPanel;
  public GameObject mailButton;
  public Button sleepButton;
  public GameObject partnerButton;
  public GameObject DLCButton;
  public GameObject appearanceButton;
  public Image backgroundImg;
  public Sprite dayBg;
  public Sprite nightBg;
  public Sprite dawnBg;
  public Sprite duskBg;
  public RawImage beatrixImg;
  public SECTR_AudioCue openCue;
  public SECTR_AudioCue closeCue;
  public GameObject beatrixPrefab;
  public bool isClosing;
  private FadeMode fadeMode;
  private TimeDirector timeDir;
  private MailDirector mailDir;
  private ProgressDirector progressDir;
  private MailUI currMailUI;
  private CorporatePartnerUI currPartnerUI;
  private DLCManageUI currDLCManageUI;
  private SlimeAppearanceUI currAppearanceUI;
  private bool sleeping;
  private Material bgMat;
  private Vector3 worldPos;
  private MusicDirector musicDir;
  private float mailInitY;
  private GameObject beatrixObj;
  private CameraDisabler camDisabler;
  private const float OFFSET_SPACING = 50f;
  private const float FADE_RATE = 2f;
  private const float BEATRIX_FADE_TIME = 0.5f;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    mailDir = SRSingleton<SceneContext>.Instance.MailDirector;
    musicDir = SRSingleton<GameContext>.Instance.MusicDirector;
    camDisabler = SRSingleton<SceneContext>.Instance.Player.GetComponentInChildren<CameraDisabler>();
    mainUI.SetActive(false);
    fadeMode = FadeMode.IN_FADE_IN;
    mailButton.SetActive(SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().AllowMail());
    mailInitY = mailIcon.rectTransform.anchoredPosition.y;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    SRSingleton<SceneContext>.Instance.PlayerState.onEndGame += OnEndGame;
    SRSingleton<SceneContext>.Instance.PopupDirector.RegisterSuppressor();
    bgMat = new Material(backgroundImg.material);
    backgroundImg.material = bgMat;
    OnButtonsEnabled();
    SECTR_AudioSystem.PauseNonUISFX(true);
    DLCButton.SetActive(DLCManageUI.IsEnabled());
    appearanceButton.SetActive(SlimeAppearanceUI.IsEnabled());
  }

  public void OnEnable()
  {
    musicDir.SetHouseMode(true);
    SRSingleton<PopupElementsUI>.Instance.RegisterBlocker(gameObject);
    beatrixObj = Instantiate(beatrixPrefab, Vector3.zero, Quaternion.identity);
    SECTR_AudioSystem.PauseNonUISFX(true);
  }

  public void OnDisable()
  {
    musicDir.SetHouseMode(false);
    if (SRSingleton<PopupElementsUI>.Instance != null)
      SRSingleton<PopupElementsUI>.Instance.DeregisterBlocker(gameObject);
    Destroyer.Destroy(beatrixObj, "RanchHouseUI.OnDisable");
    if (camDisabler != null)
    {
      camDisabler.RemoveBlocker(this);
      camDisabler = null;
    }
    SECTR_AudioSystem.PauseNonUISFX(false);
  }

  public void OnButtonsEnabled() => partnerButton.SetActive(SRSingleton<SceneContext>.Instance.RanchDirector.IsPartnerUnlocked());

  public override void OnDestroy()
  {
    base.OnDestroy();
    SECTR_AudioSystem.PauseNonUISFX(false);
    Destroyer.Destroy(bgMat, "RanchHouseUI.onDestroy");
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.PlayerState.onEndGame -= OnEndGame;
    SRSingleton<SceneContext>.Instance.PopupDirector.UnregisterSuppressor();
  }

  private void OnEndGame()
  {
    Close();
    sleepButton.interactable = false;
  }

  public override void Update()
  {
    base.Update();
    timeText.text = timeDir.CurrTimeString();
    dayText.text = timeDir.CurrDayString();
    timeIcon.sprite = timeDir.CurrTimeIcon();
    mailIcon.enabled = mailDir.HasNewMail();
    mailHighlightText.text = mailDir.GetNewMailCount().ToString();
    mailHighlight.SetActive(mailDir.HasNewMail());
    int progress = progressDir.GetProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER);
    partnerArea.SetActive(progress > 0);
    partnerLevelText.text = progress.ToString();
    mailIcon.rectTransform.anchoredPosition = (Vector3) mailIcon.rectTransform.anchoredPosition with
    {
      y = (mailInitY + (progress > 0 ? 0.0f : 50f))
    };
    switch (fadeMode)
    {
      case FadeMode.IN_FADE_IN:
      case FadeMode.OUT_FADE_IN:
        obscurer.color = new Color(obscurer.color.r, obscurer.color.g, obscurer.color.b, Math.Min(1f, obscurer.color.a + Time.unscaledDeltaTime * 2f));
        break;
      case FadeMode.IN_FADE_OUT:
      case FadeMode.OUT_FADE_OUT:
        obscurer.color = new Color(obscurer.color.r, obscurer.color.g, obscurer.color.b, Math.Max(0.0f, obscurer.color.a - Time.unscaledDeltaTime * 2f));
        break;
    }
    if (fadeMode == FadeMode.IN_FADE_IN && obscurer.color.a == 1.0)
    {
      mainUI.SetActive(true);
      fadeMode = FadeMode.IN_FADE_OUT;
      camDisabler.AddBlocker(this);
    }
    else if (fadeMode == FadeMode.IN_FADE_OUT && obscurer.color.a == 0.0)
      fadeMode = FadeMode.NONE;
    else if (fadeMode == FadeMode.OUT_FADE_IN && obscurer.color.a == 1.0)
    {
      mainUI.SetActive(false);
      fadeMode = FadeMode.OUT_FADE_OUT;
      camDisabler.RemoveBlocker(this);
    }
    else if (fadeMode == FadeMode.OUT_FADE_OUT && obscurer.color.a == 0.0)
    {
      fadeMode = FadeMode.NONE;
      base.Close();
    }
    obscurer.gameObject.SetActive(obscurer.color.a > 0.0);
    UpdateBackgroundMaterial();
  }

  public void Mail()
  {
    currMailUI = Instantiate(mailUI).GetComponent<MailUI>();
    buttonPanel.SetActive(false);
    MailUI currMailUi = currMailUI;
    currMailUi.onDestroy = currMailUi.onDestroy + (() =>
    {
      if (!(buttonPanel != null))
        return;
      buttonPanel.SetActive(true);
      OnButtonsEnabled();
    });
  }

  public void CorporatePartner()
  {
    currPartnerUI = Instantiate(partnerUI).GetComponent<CorporatePartnerUI>();
    buttonPanel.SetActive(false);
    CorporatePartnerUI currPartnerUi = currPartnerUI;
    currPartnerUi.onDestroy = currPartnerUi.onDestroy + (() =>
    {
      if (!(buttonPanel != null))
        return;
      buttonPanel.SetActive(true);
      OnButtonsEnabled();
    });
  }

  public void SleepUntilMorning()
  {
    if (sleeping)
    {
      Debug.Log("Attempted to sleep while sleeping. Ignore.");
    }
    else
    {
      AnalyticsUtil.CustomEvent("PlayerSlept");
      sleeping = true;
      timeDir.Unpause(false);
      beatrixImg.DOFade(0.0f, 0.5f).SetUpdate(true);
      SRSingleton<LockOnDeath>.Instance.LockUntil(timeDir.GetNextDawn(), 0.0f, () =>
      {
        beatrixImg.DOFade(1f, 0.5f).SetUpdate(true);
        timeDir.Pause(false);
        sleeping = false;
      });
    }
  }

  public void OnButtonDLC()
  {
    currDLCManageUI = Instantiate(manageDLCPrefab).GetComponent<DLCManageUI>();
    buttonPanel.SetActive(false);
    DLCManageUI currDlcManageUi = currDLCManageUI;
    currDlcManageUi.onDestroy = currDlcManageUi.onDestroy + (() =>
    {
      if (!(buttonPanel != null))
        return;
      buttonPanel.SetActive(true);
      OnButtonsEnabled();
    });
  }

  public void OnButtonAppearances()
  {
    currAppearanceUI = Instantiate(appearancePrefab).GetComponent<SlimeAppearanceUI>();
    buttonPanel.SetActive(false);
    SlimeAppearanceUI currAppearanceUi = currAppearanceUI;
    currAppearanceUi.onDestroy = currAppearanceUi.onDestroy + (() =>
    {
      if (!(buttonPanel != null))
        return;
      buttonPanel.SetActive(true);
      OnButtonsEnabled();
    });
  }

  protected override bool Closeable() => !sleeping && !(currMailUI != null) && !(currPartnerUI != null) && !(currDLCManageUI != null) && !(currAppearanceUI != null) && !isClosing && base.Closeable();

  public override void Close()
  {
    progressDir.NoteReturnedToRanch();
    SECTR_AudioSystem.Play(closeCue, worldPos, false);
    SRSingleton<SceneContext>.Instance.Player.GetComponent<PlayerDeathHandler>().ResetPlayerLocation(0.0f, () => fadeMode = FadeMode.OUT_FADE_IN);
    isClosing = true;
  }

  private void UpdateBackgroundMaterial() => bgMat.SetFloat("_DayFraction", timeDir.CurrDayFraction());

  public void SetPosition(Vector3 pos)
  {
    worldPos = pos;
    SECTR_AudioSystem.Play(openCue, worldPos, false);
  }

  private enum FadeMode
  {
    NONE,
    IN_FADE_IN,
    IN_FADE_OUT,
    OUT_FADE_IN,
    OUT_FADE_OUT,
  }
}
