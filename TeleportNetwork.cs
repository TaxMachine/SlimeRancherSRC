// Decompiled with JetBrains decompiler
// Type: TeleportNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeleportNetwork : MonoBehaviour
{
  private Dictionary<string, Destination> destinationLookup = new Dictionary<string, Destination>();

  public void Register(TeleportDestination exit) => GetOrCreateDestinationSet(exit.teleportDestinationName).exits.Add(exit);

  public void Deregister(TeleportDestination exit)
  {
    Destination destination;
    if (!destinationLookup.TryGetValue(exit.teleportDestinationName, out destination))
    {
      Log.Warning("Tried to remove a teleport exit from a non-existent destination.", "exit.teleportDestinationName", exit.teleportDestinationName);
    }
    else
    {
      destination.exits.Remove(exit);
      if (destination.exits.Count != 0)
        return;
      destinationLookup.Remove(exit.teleportDestinationName);
    }
  }

  public List<TeleportDestination> GetDestinations(string destinationName) => destinationLookup.ContainsKey(destinationName) ? destinationLookup[destinationName].exits : new List<TeleportDestination>();

  private Destination GetOrCreateDestinationSet(string destinationName)
  {
    Destination destinationSet;
    if (!destinationLookup.TryGetValue(destinationName, out destinationSet))
    {
      destinationSet = new Destination();
      destinationSet.name = destinationName;
      destinationSet.exits = new List<TeleportDestination>();
      destinationLookup.Add(destinationName, destinationSet);
    }
    return destinationSet;
  }

  public bool TeleportToDestination(
    TeleportablePlayer toTeleport,
    TeleportSource source,
    string destinationName,
    Func<List<TeleportDestination>, TeleportDestination> pickFunction)
  {
    List<TeleportDestination> source1 = new List<TeleportDestination>(GetDestinations(source.destinationSetName));
    source1.RemoveAll(destination => !destination.IsLinkActive());
    if (!source1.Any())
      return false;
    TeleportDestination destination1 = pickFunction(source1);
    if (destination1 == null)
      return false;
    TeleportToDestination(toTeleport, source, destination1);
    return true;
  }

  private void TeleportToDestination(
    TeleportablePlayer toTeleport,
    TeleportSource source,
    TeleportDestination destination)
  {
    source.OnDepart();
    destination.OnDepart();
    TeleportablePlayer teleportablePlayer = toTeleport;
    Vector3 position = destination.GetPosition();
    Vector3? eulerAngles = destination.GetEulerAngles();
    int regionSetId = (int) destination.regionSetId;
    Vector3? rotation = eulerAngles;
    teleportablePlayer.TeleportTo(position, (RegionRegistry.RegionSetId) regionSetId, rotation);
    destination.OnArrive();
  }

  public void OnDestroy() => destinationLookup.Clear();

  public bool IsLinkFullyActive(TeleportSource source)
  {
    if (source.IsLinkActive())
    {
      foreach (TeleportDestination destination in GetDestinations(source.destinationSetName))
      {
        if (destination.IsLinkActive())
          return true;
      }
    }
    return false;
  }

  [Serializable]
  private struct Destination
  {
    public string name;
    public List<TeleportDestination> exits;
  }
}
