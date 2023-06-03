// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.ProgressModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class ProgressModel
  {
    public Dictionary<ProgressDirector.ProgressType, int> progressDict = new Dictionary<ProgressDirector.ProgressType, int>(ProgressDirector.progressTypeComparer);
    public Dictionary<ProgressDirector.ProgressTrackerId, double> delayedProgressTimeDict = new Dictionary<ProgressDirector.ProgressTrackerId, double>(ProgressDirector.progressTrackerIdComparer);
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
      progressDict.Clear();
      delayedProgressTimeDict.Clear();
    }

    public void Push(
      Dictionary<ProgressDirector.ProgressType, int> dict,
      Dictionary<ProgressDirector.ProgressTrackerId, double> delayedDict)
    {
      foreach (KeyValuePair<ProgressDirector.ProgressType, int> keyValuePair in dict)
      {
        if (!progressDict.ContainsKey(keyValuePair.Key) || progressDict[keyValuePair.Key] < keyValuePair.Value)
          progressDict[keyValuePair.Key] = keyValuePair.Value;
      }
      foreach (KeyValuePair<ProgressDirector.ProgressTrackerId, double> keyValuePair in delayedDict)
      {
        if (delayedProgressTimeDict.ContainsKey(keyValuePair.Key))
          delayedProgressTimeDict[keyValuePair.Key] = keyValuePair.Value;
        else
          Log.Warning("No delayed progress tracker for type, skipping: " + keyValuePair.Key);
      }
    }

    public void Pull(
      out Dictionary<ProgressDirector.ProgressType, int> progressDict,
      out Dictionary<ProgressDirector.ProgressTrackerId, double> delayedProgressDict)
    {
      progressDict = new Dictionary<ProgressDirector.ProgressType, int>(this.progressDict, ProgressDirector.progressTypeComparer);
      delayedProgressDict = new Dictionary<ProgressDirector.ProgressTrackerId, double>(delayedProgressTimeDict, ProgressDirector.progressTrackerIdComparer);
    }

    public double GetDelayedProgressTime(ProgressDirector.ProgressTrackerId trackerId) => delayedProgressTimeDict.ContainsKey(trackerId) ? delayedProgressTimeDict[trackerId] : double.PositiveInfinity;

    public void SetDelayedProgressTime(ProgressDirector.ProgressTrackerId trackerId, double time) => delayedProgressTimeDict[trackerId] = time;

    public bool HasProgress(ProgressDirector.ProgressType type) => progressDict.ContainsKey(type) && progressDict[type] > 0;

    public int GetProgress(ProgressDirector.ProgressType type) => progressDict.ContainsKey(type) ? progressDict[type] : 0;

    public void OnNewGameLoaded(PlayerState.GameMode currGameMode)
    {
      if (currGameMode != PlayerState.GameMode.TIME_LIMIT_V2)
        return;
      progressDict = new Dictionary<ProgressDirector.ProgressType, int>(ProgressDirector.progressTypeComparer)
      {
        {
          ProgressDirector.ProgressType.UNLOCK_DOCKS,
          1
        },
        {
          ProgressDirector.ProgressType.UNLOCK_GROTTO,
          1
        },
        {
          ProgressDirector.ProgressType.UNLOCK_LAB,
          1
        },
        {
          ProgressDirector.ProgressType.UNLOCK_OVERGROWTH,
          1
        }
      };
    }

    public interface Participant
    {
      void InitModel(ProgressModel model);

      void SetModel(ProgressModel model);
    }
  }
}
