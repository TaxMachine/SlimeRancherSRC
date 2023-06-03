// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.MailModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class MailModel
  {
    public List<MailDirector.Mail> allMail = new List<MailDirector.Mail>();
    public Dictionary<MailDirector.Mail, MailDirector.Mail> allMailDict = new Dictionary<MailDirector.Mail, MailDirector.Mail>();
    public bool hasPartnerMail;
    public bool hasSecretStyleMail;
    public bool hasNewMail;
    private Participant participant;

    public void SetParticipant(Participant participant) => this.participant = participant;

    public void Init()
    {
      if (participant == null)
        return;
      participant.InitModel(this);
    }

    public void NotifyParticipants()
    {
      if (participant == null)
        return;
      participant.SetModel(this);
    }

    public void Reset()
    {
      allMail.Clear();
      allMailDict.Clear();
      hasPartnerMail = false;
      hasSecretStyleMail = false;
      hasNewMail = false;
    }

    public void AddMail(MailDirector.Mail mail)
    {
      allMail.Add(mail);
      allMailDict[mail] = mail;
      MailListChanged();
    }

    public void RefreshNewMail()
    {
      hasPartnerMail = false;
      hasSecretStyleMail = false;
      foreach (MailDirector.Mail key in allMail)
      {
        allMailDict[key] = key;
        if (key.key == "partner_rewards")
          hasPartnerMail = true;
        else if (key.key == "secret_styles")
          hasSecretStyleMail = true;
      }
      hasNewMail = false;
      foreach (MailDirector.Mail mail in allMail)
      {
        if (!mail.read)
        {
          hasNewMail = true;
          break;
        }
      }
    }

    public void MailListChanged()
    {
      RefreshNewMail();
      participant.OnMailListChanged();
    }

    public void Push(List<MailDirector.Mail> mail)
    {
      allMail.Clear();
      allMailDict.Clear();
      allMail.AddRange(mail);
    }

    public void Pull(out List<MailDirector.Mail> mail) => mail = new List<MailDirector.Mail>(allMail);

    public interface Participant
    {
      void InitModel(MailModel model);

      void SetModel(MailModel model);

      void OnMailListChanged();
    }
  }
}
