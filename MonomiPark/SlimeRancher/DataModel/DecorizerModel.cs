// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.DecorizerModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class DecorizerModel
  {
    private List<Participant> participants = new List<Participant>();
    public static readonly IEnumerable<HashSet<Identifiable.Id>> ITEM_CLASSES = new HashSet<Identifiable.Id>[3]
    {
      Identifiable.ECHO_CLASS,
      Identifiable.ECHO_NOTE_CLASS,
      Identifiable.ORNAMENT_CLASS
    };
    private ReferenceCount<Identifiable.Id> contents = new ReferenceCount<Identifiable.Id>(Identifiable.idComparer);
    private Dictionary<string, Settings> settings = new Dictionary<string, Settings>();

    public void AddParticipant(Participant participant) => participants.Add(participant);

    public void Init() => participants.ForEach(p => p.InitModel(this));

    public void NotifyParticipants() => participants.ForEach(p => p.SetModel(this));

    public bool Add(Identifiable.Id id)
    {
      if (!ITEM_CLASSES.Any(c => c.Contains(id)))
        return false;
      contents.Increment(id);
      return true;
    }

    public bool Remove(Identifiable.Id id)
    {
      if (contents.GetCount(id) <= 0)
        return false;
      contents.Decrement(id);
      participants.ForEach(p => p.OnDecorizerRemoved(id));
      return true;
    }

    public int GetCount(Identifiable.Id id) => contents.GetCount(id);

    public Settings GetSettings(string id)
    {
      if (!settings.ContainsKey(id))
        settings[id] = new Settings();
      return settings[id];
    }

    public void Push(DecorizerV01 persistence)
    {
      contents = new ReferenceCount<Identifiable.Id>(persistence.contents, Identifiable.idComparer);
      settings = persistence.settings.ToDictionary(kv => kv.Key, kv => new Settings()
      {
        selected = kv.Value.selected
      });
    }

    public void Pull(out DecorizerV01 persistence) => persistence = new DecorizerV01()
    {
      contents = contents.ToDictionary(kv => kv.Key, kv => kv.Value, Identifiable.idComparer),
      settings = settings.ToDictionary(kv => kv.Key, kv => new DecorizerSettingsV01()
      {
        selected = kv.Value.selected
      })
    };

    public interface Participant
    {
      void InitModel(DecorizerModel model);

      void SetModel(DecorizerModel model);

      void OnDecorizerRemoved(Identifiable.Id id);
    }

    public class Settings
    {
      public Identifiable.Id selected;
    }
  }
}
