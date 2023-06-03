// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.PediaModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class PediaModel
  {
    private Participant participant;
    public HashSet<PediaDirector.Id> unlocked = new HashSet<PediaDirector.Id>();
    public int progressGivenForCount;

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

    public void ResetUnlocked(PediaDirector.Id[] initUnlocked)
    {
      unlocked.Clear();
      foreach (PediaDirector.Id id in initUnlocked)
        Unlock(id);
      if (!SRSingleton<SceneContext>.Instance.GameModeConfig.GetModeSettings().preventHostiles)
        return;
      Unlock(PediaDirector.Id.TARR_SLIME);
    }

    public void Unlock(params PediaDirector.Id[] ids)
    {
      int count1 = unlocked.Count;
      unlocked.UnionWith(ids);
      int count2 = unlocked.Count;
      if (count1 == count2)
        return;
      participant.OnUnlockedChanged(unlocked);
    }

    public void Push(int progressGivenForCount, IEnumerable<PediaDirector.Id> unlocked)
    {
      this.progressGivenForCount = progressGivenForCount;
      foreach (PediaDirector.Id id in unlocked)
        this.unlocked.Add(id);
    }

    public void Pull(out int progressGivenForCount, out IEnumerable<PediaDirector.Id> unlocked)
    {
      progressGivenForCount = this.progressGivenForCount;
      unlocked = new List<PediaDirector.Id>(this.unlocked);
    }

    public interface Participant
    {
      void InitModel(PediaModel model);

      void SetModel(PediaModel model);

      void OnUnlockedChanged(HashSet<PediaDirector.Id> unlocked);
    }
  }
}
