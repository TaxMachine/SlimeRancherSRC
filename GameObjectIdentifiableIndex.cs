// Decompiled with JetBrains decompiler
// Type: GameObjectIdentifiableIndex
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectIdentifiableIndex
{
  private readonly List<Entry> EMPTY_ENTRY_LIST = new List<Entry>();
  private Dictionary<Identifiable.Id, List<Entry>> objects = new Dictionary<Identifiable.Id, List<Entry>>(Identifiable.idComparer);
  private List<Entry> slimes = new List<Entry>();
  private List<Entry> animals = new List<Entry>();
  private List<Entry> largos = new List<Entry>();
  private List<Entry> toys = new List<Entry>();

  public void Register(Identifiable.Id id, GameObject obj)
  {
    Entry entry = new Entry(id, obj);
    List<Entry> entryList;
    objects.TryGetValue(id, out entryList);
    if (entryList == null)
    {
      entryList = new List<Entry>();
      objects[id] = entryList;
    }
    entryList.Add(entry);
    if (Identifiable.IsSlime(id))
      slimes.Add(entry);
    if (Identifiable.IsAnimal(id))
      animals.Add(entry);
    if (Identifiable.IsLargo(id))
      largos.Add(entry);
    if (!Identifiable.IsToy(id))
      return;
    toys.Add(entry);
  }

  public void Deregister(Identifiable.Id id, GameObject obj)
  {
    Entry entry = new Entry(id, obj);
    List<Entry> entryList;
    objects.TryGetValue(id, out entryList);
    if (entryList != null)
    {
      entryList.Remove(entry);
      if (entryList.Count <= 0)
        objects[id] = null;
    }
    if (Identifiable.IsSlime(id))
      slimes.Remove(entry);
    if (Identifiable.IsAnimal(id))
      animals.Remove(entry);
    if (Identifiable.IsLargo(id))
      largos.Remove(entry);
    if (!Identifiable.IsToy(id))
      return;
    toys.Remove(entry);
  }

  public bool IsRegistered(Identifiable.Id id, GameObject gameObject)
  {
    Entry entry = new Entry(id, gameObject);
    List<Entry> entryList;
    return objects.TryGetValue(id, out entryList) && entryList != null && entryList.Contains(entry);
  }

  public IList<Entry> GetObjectsByIdentifiableId(Identifiable.Id id)
  {
    List<Entry> entryList;
    return objects.TryGetValue(id, out entryList) && entryList != null ? entryList : (IList<Entry>) EMPTY_ENTRY_LIST;
  }

  public IList<Entry> GetSlimes() => slimes;

  public int GetSlimeCount() => slimes.Count;

  public IList<Entry> GetToys() => toys;

  public int GetToyCount() => toys.Count;

  public IList<Entry> GetLargos() => largos;

  public int GetLargoCount() => largos.Count;

  public IList<Entry> GetAnimals() => animals;

  public int GetAnimalCount() => animals.Count;

  public IEnumerable<Entry> GetAllRegistered()
  {
    foreach (List<Entry> entries in objects.Values)
    {
      if (entries != null)
      {
        for (int ii = 0; ii < entries.Count; ++ii)
          yield return entries[ii];
      }
    }
  }

  public struct Entry : IEquatable<Entry>
  {
    private Identifiable.Id id;
    private GameObject gameObject;
    private int instanceId;

    public Entry(Identifiable.Id id, GameObject gameObject)
    {
      this.id = id;
      this.gameObject = gameObject;
      instanceId = gameObject.GetInstanceID();
    }

    public Identifiable.Id Id => id;

    public GameObject GameObject => gameObject;

    public bool Equals(Entry other) => id == other.id && instanceId == other.instanceId;
  }
}
