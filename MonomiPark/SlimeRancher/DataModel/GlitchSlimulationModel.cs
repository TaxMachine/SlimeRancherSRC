// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchSlimulationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchSlimulationModel
  {
    public Dictionary<string, GlitchTeleportDestinationModel> teleporters = new Dictionary<string, GlitchTeleportDestinationModel>();
    public Dictionary<string, GlitchImpostoDirectorModel> impostoDirectors = new Dictionary<string, GlitchImpostoDirectorModel>();
    private readonly GameModel parent;

    public GameModel.IdContainer<GlitchImpostoModel> impostos { get; private set; }

    public GameModel.IdContainer<GlitchTarrNodeModel> nodes { get; private set; }

    public GameModel.IdContainer<GlitchStorageModel> storage { get; private set; }

    public GlitchSlimulationModel(GameModel parent)
    {
      this.parent = parent;
      nodes = new GameModel.IdContainer<GlitchTarrNodeModel>(parent);
      impostos = new GameModel.IdContainer<GlitchImpostoModel>(parent);
      storage = new GameModel.IdContainer<GlitchStorageModel>(parent);
    }

    public void Init()
    {
      nodes.Init();
      storage.Init();
      foreach (GlitchTeleportDestinationModel destinationModel in teleporters.Values)
        destinationModel.Init();
      impostos.Init();
      foreach (GlitchImpostoDirectorModel impostoDirectorModel in impostoDirectors.Values)
        impostoDirectorModel.Init();
    }

    public void NotifyParticipants()
    {
      nodes.NotifyParticipants();
      storage.NotifyParticipants();
      foreach (GlitchTeleportDestinationModel destinationModel in teleporters.Values)
        destinationModel.NotifyParticipants();
      impostos.NotifyParticipants();
      foreach (GlitchImpostoDirectorModel impostoDirectorModel in impostoDirectors.Values)
        impostoDirectorModel.NotifyParticipants();
    }

    public void Register(
      GlitchTeleportDestinationModel.Participant participant)
    {
      GlitchTeleportDestinationModel destinationModel = new GlitchTeleportDestinationModel(participant);
      teleporters[participant.id] = destinationModel;
      if (parent.expectingPush)
        return;
      destinationModel.Init();
      destinationModel.NotifyParticipants();
    }

    public void Unregister(
      GlitchTeleportDestinationModel.Participant participant)
    {
      teleporters.Remove(participant.id);
    }

    public void Register(GlitchImpostoDirectorModel.Participant participant)
    {
      GlitchImpostoDirectorModel impostoDirectorModel = new GlitchImpostoDirectorModel(participant);
      impostoDirectors[participant.id] = impostoDirectorModel;
      if (parent.expectingPush)
        return;
      impostoDirectorModel.Init();
      impostoDirectorModel.NotifyParticipants();
    }

    public void Unregister(GlitchImpostoDirectorModel.Participant participant) => impostoDirectors.Remove(participant.id);

    public void Push(GlitchSlimulationV02 persistence)
    {
      foreach (KeyValuePair<string, GlitchTeleportDestinationV01> teleporter in persistence.teleporters)
      {
        if (!teleporters.ContainsKey(teleporter.Key))
          Log.Warning("Failed to get GlitchTeleportDestinationV01 for persistence id.", "id", teleporter.Key);
        else
          teleporters[teleporter.Key].Push(teleporter.Value);
      }
      foreach (KeyValuePair<string, GlitchTarrNodeV01> node in persistence.nodes)
      {
        if (!nodes.ContainsKey(node.Key))
          Log.Warning("Failed to get GlitchTarrNodeV01 for persistence id.", "id", node.Key);
        else
          nodes[node.Key].Push(node.Value);
      }
      foreach (KeyValuePair<string, GlitchImpostoDirectorV01> impostoDirector in persistence.impostoDirectors)
      {
        if (!impostoDirectors.ContainsKey(impostoDirector.Key))
          Log.Warning("Failed to get GlitchImpostoDirectorV01 for persistence id.", "id", impostoDirector.Key);
        else
          impostoDirectors[impostoDirector.Key].Push(impostoDirector.Value);
      }
      foreach (KeyValuePair<string, GlitchImpostoV01> imposto in persistence.impostos)
      {
        if (!impostos.ContainsKey(imposto.Key))
          Log.Warning("Failed to get GlitchImpostoV01 for persistence id.", "id", imposto.Key);
        else
          impostos[imposto.Key].Push(imposto.Value);
      }
      foreach (KeyValuePair<string, GlitchStorageV01> keyValuePair in persistence.storage)
      {
        if (!storage.ContainsKey(keyValuePair.Key))
          Log.Warning("Failed to get GlitchStorageV01 for persistence id.", "id", keyValuePair.Key);
        else
          storage[keyValuePair.Key].Push(keyValuePair.Value);
      }
    }

    public void Pull(out GlitchSlimulationV02 persistence)
    {
      persistence = new GlitchSlimulationV02();
      persistence.teleporters = teleporters.ToDictionary(p => p.Key, p => p.Value.Pull());
      persistence.nodes = nodes.StaticInstances.ToDictionary(p => p.Key, p => p.Value.Pull());
      persistence.impostoDirectors = impostoDirectors.ToDictionary(p => p.Key, p => p.Value.Pull());
      persistence.impostos = impostos.StaticInstances.ToDictionary(p => p.Key, p => p.Value.Pull());
      persistence.storage = storage.StaticInstances.ToDictionary(p => p.Key, p => p.Value.Pull());
    }
  }
}
