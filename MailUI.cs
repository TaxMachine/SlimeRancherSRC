// Decompiled with JetBrains decompiler
// Type: MailUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MailUI : BaseUI
{
  public GameObject contentPanel;
  public GameObject placeholderPanel;
  public GameObject buttonListPanel;
  public TMP_Text selectedFrom;
  public TMP_Text selectedSubj;
  public TMP_Text selectedBody;
  public GameObject buttonListItemPrefab;
  public ScrollRect contentScroll;
  public Sprite mailUnreadIcon;
  public Sprite mailReadIcon;
  public SECTR_AudioCue openCue;
  public SECTR_AudioCue closeCue;
  private MessageBundle mailBundle;
  private MailDirector mailDir;
  private ProgressDirector progressDir;

  public override void Awake()
  {
    base.Awake();
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    mailDir = SRSingleton<SceneContext>.Instance.MailDirector;
    mailBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("mail");
    contentScroll.gameObject.SetActive(false);
    placeholderPanel.SetActive(true);
    foreach (MailDirector.Mail mail in mailDir.GetMailRecentFirst())
      AddButton(mail);
    Toggle[] componentsInChildren = buttonListPanel.GetComponentsInChildren<Toggle>(true);
    if (componentsInChildren.Length != 0)
      componentsInChildren[0].gameObject.AddComponent<InitSelected>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      Navigation navigation = new Navigation();
      navigation.mode = Navigation.Mode.Explicit;
      if (index < componentsInChildren.Length - 1)
        navigation.selectOnDown = componentsInChildren[index + 1];
      if (index > 0)
        navigation.selectOnUp = componentsInChildren[index - 1];
      componentsInChildren[index].navigation = navigation;
    }
  }

  public void OnEnable() => Play(openCue);

  public void OnDisable() => Play(closeCue);

  public void AddButton(MailDirector.Mail mail)
  {
    GameObject button = CreateButton(mail);
    button.transform.SetParent(buttonListPanel.transform, false);
    button.GetComponent<Toggle>().group = buttonListPanel.GetComponentInParent<ToggleGroup>();
  }

  private GameObject CreateButton(MailDirector.Mail mail)
  {
    GameObject buttonObj = Instantiate(buttonListItemPrefab);
    Toggle component1 = buttonObj.GetComponent<Toggle>();
    TMP_Text component2 = buttonObj.transform.Find("Info/From").gameObject.GetComponent<TMP_Text>();
    TMP_Text component3 = buttonObj.transform.Find("Info/Subject").gameObject.GetComponent<TMP_Text>();
    Image iconImg = buttonObj.transform.Find("Icon").gameObject.GetComponent<Image>();
    string str = mailBundle.Xlate("m.from." + mail.key);
    component2.text = str;
    component3.text = mailBundle.Xlate("m.subj." + mail.key);
    iconImg.sprite = mail.read ? mailReadIcon : mailUnreadIcon;
    UnityAction<bool> onButton = isOn =>
    {
      if (isOn)
        Select(mail);
      iconImg.sprite = mail.read ? mailReadIcon : mailUnreadIcon;
    };
    component1.onValueChanged.AddListener(onButton);
    OnSelectDelegator.Create(buttonObj, () =>
    {
      onButton(true);
      buttonObj.GetComponent<Toggle>().isOn = true;
    });
    return buttonObj;
  }

  public void Select(MailDirector.Mail mail)
  {
    contentScroll.gameObject.SetActive(true);
    placeholderPanel.SetActive(false);
    selectedFrom.text = mailBundle.Xlate("m.from." + mail.key);
    selectedSubj.text = mailBundle.Xlate("m.subj." + mail.key);
    selectedBody.text = mailBundle.Xlate("m.body." + mail.key);
    StartCoroutine(ScrollAtEndOfFrame());
    if (!mail.read && mail.key.StartsWith("casey_"))
      progressDir.QueueRanchWistfulMusic();
    if (!mail.read && mail.key == "casey_11")
      progressDir.QueueCredits();
    mailDir.MarkRead(mail);
    if (mail.key == RanchDirector.PARTNER_MAIL_KEY)
    {
      SRSingleton<SceneContext>.Instance.PediaDirector.UnlockWithoutPopup(PediaDirector.Id.PARTNER);
      if (progressDir.HasProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER_UNLOCK))
        return;
      progressDir.AddProgress(ProgressDirector.ProgressType.CORPORATE_PARTNER_UNLOCK);
    }
    else
    {
      if (!(mail.key == "hobson_1") || progressDir.HasProgress(ProgressDirector.ProgressType.HOBSON_END_UNLOCK))
        return;
      progressDir.AddProgress(ProgressDirector.ProgressType.HOBSON_END_UNLOCK);
    }
  }

  public IEnumerator ScrollAtEndOfFrame()
  {
    yield return new WaitForEndOfFrame();
    contentScroll.verticalNormalizedPosition = 1f;
  }
}
