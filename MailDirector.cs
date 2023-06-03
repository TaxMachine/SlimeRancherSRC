// Decompiled with JetBrains decompiler
// Type: MailDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MailDirector : MonoBehaviour, MailModel.Participant
{
  public Sprite personalIcon;
  public Sprite upgradeIcon;
  public Sprite exchangeIcon;
  private MessageBundle mailBundle;
  private PopupDirector popupDir;
  private ProgressDirector progressDir;
  private PlayerState playerState;
  private TutorialDirector tutDir;
  private TimeDirector timeDirector;
  private DLCDirector dlcDirector;
  private MailModel model;
  public const string PARTNER_MAIL_KEY = "partner_rewards";
  public const float PARTNER_UNLOCK_TIME = 561600f;
  public const string CASEY_MAIL_PREFIX = "casey_";
  public const string CASEY_MAIL_FINAL = "casey_11";
  public const string HOBSON_MAIL_KEY = "hobson_1";
  public const string OGDEN_MAIL_KEY = "ogden_invite";
  public const string MOCHI_MAIL_KEY = "mochi_invite";
  public const string VIKTOR_MAIL_KEY = "viktor_invite";
  public const string SECRET_STYLE_MAIL_KEY = "secret_styles";
  public const float SECRET_STYLE_MAIL_DELAY = 600f;

  public void Awake()
  {
    dlcDirector = SRSingleton<GameContext>.Instance.DLCDirector;
    mailBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("mail");
    popupDir = SRSingleton<SceneContext>.Instance.PopupDirector;
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    playerState = SRSingleton<SceneContext>.Instance.PlayerState;
    tutDir = SRSingleton<SceneContext>.Instance.TutorialDirector;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void OnDestroy()
  {
    if (dlcDirector == null)
      return;
    dlcDirector.onPackageInstalled -= OnDLCPackageStateChanged;
    dlcDirector = null;
  }

  public void Start()
  {
    timeDirector.OnPassedTime(561600.0, () =>
    {
      if (model.hasPartnerMail || !SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().enablePartnerRewards)
        return;
      SendMail(Type.PERSONAL, "partner_rewards");
    });
    if (model.hasSecretStyleMail)
      return;
    dlcDirector.onPackageInstalled += OnDLCPackageStateChanged;
    OnDLCPackageStateChanged(DLCPackage.Id.SECRET_STYLE);
  }

  public void InitForLevel() => SRSingleton<SceneContext>.Instance.GameModel.RegisterMail(this);

  public void InitModel(MailModel model)
  {
    model.Reset();
    if (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().AllowMail() || Levels.isSpecial())
      return;
    model.AddMail(new Mail(Type.PERSONAL, "welcome"));
    model.hasNewMail = true;
  }

  public void SetModel(MailModel model)
  {
    this.model = model;
    this.model.MailListChanged();
  }

  public Sprite GetIcon(Type type)
  {
    switch (type)
    {
      case Type.PERSONAL:
        return personalIcon;
      case Type.UPGRADE:
        return upgradeIcon;
      case Type.EXCHANGE:
        return exchangeIcon;
      default:
        Log.Error("Invalid mail type: " + type);
        return null;
    }
  }

  public List<Mail> GetMailRecentFirst()
  {
    List<Mail> mailRecentFirst = new List<Mail>(model.allMail);
    mailRecentFirst.Reverse();
    return mailRecentFirst;
  }

  public bool SendMailIfExists(Type type, string key) => mailBundle.Exists("m.subj." + key) && SendMail(type, key);

  public bool SendMail(Type type, string key)
  {
    Mail mail = new Mail(type, key);
    if (model.allMailDict.ContainsKey(mail) || !SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().AllowMail() || Levels.isSpecialNonAlloc())
      return false;
    model.AddMail(mail);
    popupDir.QueueForPopup(new MailPopupCreator(mail));
    if (key == "partner_rewards")
      model.hasPartnerMail = true;
    model.hasNewMail = true;
    return true;
  }

  public void MarkRead(Mail mail)
  {
    mail.read = true;
    model.MailListChanged();
    popupDir.CheckShouldClear();
    playerState.OnMailRead();
    tutDir.OnMailRead(mail);
  }

  public void OnMailListChanged() => progressDir.CheckTrackers();

  public bool HasNewMail() => model.hasNewMail;

  public int GetNewMailCount()
  {
    int newMailCount = 0;
    foreach (Mail mail in model.allMail)
    {
      if (!mail.read)
        ++newMailCount;
    }
    return newMailCount;
  }

  public bool HasReadMail(Mail mail) => model != null && model.allMailDict.ContainsKey(mail) && model.allMailDict[mail].read;

  private void OnDLCPackageStateChanged(DLCPackage.Id package)
  {
    if (package != DLCPackage.Id.SECRET_STYLE || !dlcDirector.IsPackageInstalledAndEnabled(package))
      return;
    timeDirector.OnPassedTime(timeDirector.WorldTime() + 600.0, () =>
    {
      SendMail(Type.PERSONAL, "secret_styles");
      model.hasSecretStyleMail = true;
    });
    dlcDirector.onPackageInstalled -= OnDLCPackageStateChanged;
  }

  public enum Type
  {
    PERSONAL,
    UPGRADE,
    EXCHANGE,
  }

  [Serializable]
  public class Mail : IEquatable<Mail>
  {
    public Type type;
    public string key;
    public bool read;

    public Mail()
    {
    }

    public Mail(Type type, string key)
    {
      this.type = type;
      this.key = key;
      read = false;
    }

    public bool Equals(Mail other) => other != null && type == other.type && key == other.key;

    public override bool Equals(object obj) => Equals(obj as Mail);

    public override int GetHashCode() => type.GetHashCode() ^ key.GetHashCode();
  }

  private class MailPopupCreator : PopupDirector.PopupCreator
  {
    private Mail mail;

    public MailPopupCreator(Mail mail) => this.mail = mail;

    public override void Create() => MailPopupUI.CreateMailPopup(mail);

    public override bool Equals(object other) => other is MailPopupCreator && ((MailPopupCreator) other).mail.key == mail.key && ((MailPopupCreator) other).mail.type == mail.type;

    public override int GetHashCode() => mail.key.GetHashCode() ^ mail.type.GetHashCode();

    public override bool ShouldClear() => mail.read;
  }
}
