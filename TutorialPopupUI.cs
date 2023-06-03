// Decompiled with JetBrains decompiler
// Type: TutorialPopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopupUI : PopupUI<TutorialDirector.IdEntry>
{
  public GameObject buttonLinesPanel;
  public TutorialButtonLine[] buttonLines;
  public TMP_Text titleText;
  public TMP_Text introText;
  public ImageCycler imgCycler;
  public Image completedImg;
  [Tooltip("SFX played when the tutorial action is completed. [2D, non-looping]")]
  public SECTR_AudioCue onCompletedCue;
  protected TutorialDirector tutorialDir;
  private Animator anim;
  private bool wasCompleted;
  private const string ANIM_COMPLETE = "Complete";
  private const string ANIM_CLOSE = "Close";
  private MessageBundle tutorialBundle;

  public virtual void Awake()
  {
    tutorialDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    SRSingleton<GameContext>.Instance.InputDirector.onKeysChanged += OnKeysChanged;
    anim = GetComponent<Animator>();
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (SRSingleton<GameContext>.Instance != null)
      SRSingleton<GameContext>.Instance.InputDirector.onKeysChanged -= OnKeysChanged;
    if (!(tutorialDir != null))
      return;
    tutorialDir.PopupDeactivated(this, wasCompleted);
  }

  public void Complete()
  {
    wasCompleted = true;
    anim.SetTrigger(nameof (Complete));
    StartCoroutine(DestroyDelayed(0.167f));
  }

  public void Hide()
  {
    wasCompleted = false;
    anim.SetTrigger("Close");
    StartCoroutine(DestroyDelayed(0.167f));
  }

  private IEnumerator DestroyDelayed(float delay)
  {
    // ISSUE: reference to a compiler-generated field
    /*int num = this.\u003C\u003E1__state;
    TutorialPopupUI tutorialPopupUi = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      Destroyer.Destroy((UnityEngine.Object) tutorialPopupUi.gameObject, "TutorialPopupUI.DestroyDelayed");
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(delay);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;*/
    yield return new WaitForSeconds(delay);
  }

  public override void Init(TutorialDirector.IdEntry tutorialIdEntry)
  {
    idEntry = tutorialIdEntry;
    base.Init(tutorialIdEntry);
    var transform1 = imgCycler.transform;
    transform1.localPosition += idEntry.imageOffset;
    transform1.localScale = idEntry.imageScale;
  }

  private void OnKeysChanged() => UpdateTutorial();

  public override void OnBundleAvailable(MessageDirector msgDir)
  {
    tutorialBundle = msgDir.GetBundle("tutorial");
    UpdateTutorial();
  }

  private void UpdateTutorial()
  {
    if (tutorialBundle == null)
      return;
    string lowerInvariant = Enum.GetName(typeof (TutorialDirector.Id), idEntry.id).ToLowerInvariant();
    UpdateTutorialInfo(lowerInvariant);
    UpdateButtonLines(lowerInvariant);
  }

  private void UpdateTutorialInfo(string lowerName)
  {
    titleText.text = tutorialBundle.Get("t." + lowerName);
    string key = "m.text." + lowerName;
    if (tutorialBundle.Exists(key))
    {
      introText.text = tutorialBundle.Get(key);
      introText.gameObject.SetActive(true);
    }
    else
      introText.gameObject.SetActive(false);
    imgCycler.SetSprites(idEntry.images);
  }

  private void UpdateButtonLines(string lowerName)
  {
    string str = InputDirector.UsingGamepad() ? "gamepad." : "";
    for (int index = 0; index < buttonLines.Length; ++index)
    {
      int num = index + 1;
      TutorialButtonLine buttonLine = buttonLines[index];
      string key1 = "m.input." + str + lowerName + "." + num;
      string key2 = "m.inputdesc." + str + lowerName + "." + num;
      if (tutorialBundle.Exists(key1))
      {
        buttonLine.gameObject.SetActive(true);
        buttonLine.Init(tutorialBundle.Get(key1), tutorialBundle.Get(key2));
      }
      else
        buttonLine.gameObject.SetActive(false);
    }
    buttonLinesPanel.SetActive(buttonLines.Length != 0);
  }

  public TutorialDirector.Id GetId() => idEntry.id;

  public void CompletedAction()
  {
    completedImg.gameObject.SetActive(true);
    if (!(onCompletedCue != null))
      return;
    SECTR_AudioSystem.Play(onCompletedCue, Vector3.zero, false);
    onCompletedCue = null;
  }

  public static GameObject CreateTutorialPopup(TutorialDirector.IdEntry tutorialIdEntry)
  {
    GameObject tutorialPopup = Instantiate(SRSingleton<SceneContext>.Instance.TutorialDirector.tutorialPopupPrefab);
    tutorialPopup.GetComponent<TutorialPopupUI>().Init(tutorialIdEntry);
    return tutorialPopup;
  }
}
