// Decompiled with JetBrains decompiler
// Type: MailPopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class MailPopupUI : PopupUI<MailDirector.Mail>, PopupDirector.Popup
{
  [Tooltip("If not killed before then, how long this popup will stick around.")]
  public float lifetime = 10f;
  protected float timeOfDeath;
  protected PopupDirector popupDir;

  public virtual void Awake()
  {
    timeOfDeath = Time.time + lifetime;
    popupDir = SRSingleton<SceneContext>.Instance.PopupDirector;
    popupDir.PopupActivated(this);
  }

  public override void OnBundleAvailable(MessageDirector msgDir)
  {
    TMP_Text component = transform.Find("UIContainer/MainPanel/IntroPanel/Intro").GetComponent<TMP_Text>();
    MessageBundle bundle1 = msgDir.GetBundle("mail");
    MessageBundle bundle2 = msgDir.GetBundle("ui");
    string key = idEntry.key;
    string str = bundle2.Get("m.mail_from_wrap", new string[1]
    {
      bundle1.Get("m.from." + key)
    });
    component.text = str;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    popupDir.PopupDeactivated(this);
  }

  public void Update()
  {
    if (Time.time < (double) timeOfDeath)
      return;
    Destroyer.Destroy(gameObject, "MailPopupUI.Update");
  }

  public MailDirector.Mail GetId() => idEntry;

  public bool ShouldClear() => idEntry.read;

  public static GameObject CreateMailPopup(MailDirector.Mail mail)
  {
    GameObject mailPopup = Instantiate(SRSingleton<GameContext>.Instance.UITemplates.mailPrefab);
    mailPopup.GetComponent<MailPopupUI>().Init(mail);
    return mailPopup;
  }
}
