// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.TutorialsModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class TutorialsModel
  {
    public HashSet<TutorialDirector.Id> completedIds = new HashSet<TutorialDirector.Id>();
    public HashSet<TutorialDirector.Id> popupQueue = new HashSet<TutorialDirector.Id>();
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

    public void Push(List<TutorialDirector.Id> completedIds, List<TutorialDirector.Id> popupQueue)
    {
      this.completedIds = new HashSet<TutorialDirector.Id>(completedIds);
      this.popupQueue = new HashSet<TutorialDirector.Id>(popupQueue);
    }

    public void Pull(
      out List<TutorialDirector.Id> completedIds,
      out List<TutorialDirector.Id> popupQueue)
    {
      completedIds = new List<TutorialDirector.Id>(this.completedIds);
      popupQueue = new List<TutorialDirector.Id>(this.popupQueue);
    }

    public interface Participant
    {
      void InitModel(TutorialsModel model);

      void SetModel(TutorialsModel model);
    }
  }
}
