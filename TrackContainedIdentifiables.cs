// Decompiled with JetBrains decompiler
// Type: TrackContainedIdentifiables
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackContainedIdentifiables : SRBehaviour
{
  [Tooltip("List of AirNet components to be checked during the slime integrity tracker.")]
  public List<AirNet> airNets;
  private Dictionary<Identifiable.Id, HashSet<Identifiable>> trackedObjects = new Dictionary<Identifiable.Id, HashSet<Identifiable>>(Identifiable.idComparer);
  private HashSet<Identifiable.Id> uniqueIdentifiableTypes = new HashSet<Identifiable.Id>(Identifiable.idComparer);
  public IdentifiableEntered OnIdentifiableEntered;
  public NewIdentifiableTypeEntered OnNewIdentifiableTypeEntered;
  private Dictionary<int, Destroyer.Metadata> metadataDict = new Dictionary<int, Destroyer.Metadata>();
  private List<int> wasRemoved = new List<int>();
  private int nextTrackFrameCount;
  private List<KeyValuePair<int, Destroyer.Metadata>> local_ToRemoveFromDict = new List<KeyValuePair<int, Destroyer.Metadata>>(64);

  public void Awake()
  {
  }

  public void GetTrackedItemsOfType(Identifiable.Id identId, List<Identifiable> trackedItems)
  {
    if (!trackedObjects.ContainsKey(identId))
      return;
    trackedItems.AddRange(trackedObjects[identId]);
  }

  public void GetTrackedItemsOfClass(
    HashSet<Identifiable.Id> idClass,
    List<Identifiable> trackedItems)
  {
    foreach (Identifiable.Id identId in idClass)
      GetTrackedItemsOfType(identId, trackedItems);
  }

  public void GetTrackedItemsOfType<T>(Identifiable.Id identId, List<T> trackedItems)
  {
    if (!trackedObjects.ContainsKey(identId))
      return;
    foreach (Component component1 in trackedObjects[identId])
    {
      T component2 = component1.GetComponent<T>();
      trackedItems.Add(component2);
    }
  }

  public void GetTrackedItemsOfClass<T>(HashSet<Identifiable.Id> idClass, List<T> trackedItems)
  {
    foreach (Identifiable.Id identId in idClass)
      GetTrackedItemsOfType(identId, trackedItems);
  }

  public void GetTrackedItemsOfClass(
    HashSet<Identifiable.Id> idClass,
    List<GameObject> trackedItems)
  {
    foreach (Identifiable.Id key in idClass)
    {
      if (trackedObjects.ContainsKey(key))
      {
        foreach (Identifiable identifiable in trackedObjects[key])
          trackedItems.Add(identifiable.gameObject);
      }
    }
  }

  public bool HasIdentifiableType(Identifiable.Id identId) => trackedObjects.ContainsKey(identId) && trackedObjects[identId].Count > 0;

  public void GetTrackedIdentifiableTypes(List<Identifiable.Id> identIds) => identIds.AddRange(uniqueIdentifiableTypes);

  public void GetTrackedIdentifiableTypes(HashSet<Identifiable.Id> typesToFind) => typesToFind.IntersectWith(uniqueIdentifiableTypes);

  public IEnumerable<KeyValuePair<Identifiable.Id, HashSet<Identifiable>>> GetAllTracked() => trackedObjects;

  public IEnumerable<Identifiable.Id> GetTrackedIdentifiableTypes() => uniqueIdentifiableTypes;

  public int Count(Identifiable.Id id) => trackedObjects.ContainsKey(id) ? trackedObjects[id].Count : 0;

  public bool Contains(Identifiable ident) => trackedObjects.ContainsKey(ident.id) && trackedObjects[ident.id].Contains(ident);

  public Identifiable RemoveTrackedObject(Identifiable.Id id)
  {
    Identifiable ident = trackedObjects[id].First();
    RemoveTrackedObject(ident);
    return ident;
  }

  public void OnTriggerEnter(Collider other)
  {
    if (other.isTrigger)
      return;
    Identifiable identifiable = GetIdentifiable(other);
    if (!(identifiable != null))
      return;
    AddTrackedObject(identifiable);
  }

  public void OnTriggerExit(Collider other)
  {
    if (other.isTrigger)
      return;
    Identifiable identifiable = GetIdentifiable(other);
    if (!(identifiable != null))
      return;
    RemoveTrackedObject(identifiable);
  }

  private Identifiable GetIdentifiable(Collider col)
  {
    Identifiable identifiable = null;
    if (col.gameObject != null)
      identifiable = col.gameObject.GetComponent<Identifiable>();
    return identifiable;
  }

  public void OnTrackedDestroyed(Identifiable trackedObject) => RemoveTrackedObject(trackedObject);

  private void AddTrackedObject(Identifiable ident)
  {
    if (!trackedObjects.ContainsKey(ident.id))
      trackedObjects.Add(ident.id, new HashSet<Identifiable>());
    if (!trackedObjects[ident.id].Add(ident))
      return;
    if (OnIdentifiableEntered != null)
      OnIdentifiableEntered(this, ident);
    if (!uniqueIdentifiableTypes.Contains(ident.id))
    {
      uniqueIdentifiableTypes.Add(ident.id);
      if (OnNewIdentifiableTypeEntered != null)
        OnNewIdentifiableTypeEntered(this, ident);
    }
    ident.NotifyOnDestroy += OnTrackedDestroyed;
    if (!IsTrackingIntegrity(ident))
      return;
    int instanceID = ident.gameObject.GetInstanceID();
    Destroyer.Monitor(ident.gameObject, metadata => metadataDict[instanceID] = metadata);
  }

  private void RemoveTrackedObject(Identifiable ident)
  {
    if (!trackedObjects.ContainsKey(ident.id))
    {
      Log.Debug("Request to remove object where the Identifiable.Id is not being tracked.", "Identifiable.Id", ident.id);
    }
    else
    {
      if (!trackedObjects[ident.id].Remove(ident))
        return;
      if (trackedObjects[ident.id].Count == 0)
        uniqueIdentifiableTypes.Remove(ident.id);
      ident.NotifyOnDestroy -= OnTrackedDestroyed;
      if (!IsTrackingIntegrity(ident))
        return;
      wasRemoved.Add(ident.gameObject.GetInstanceID());
    }
  }

  public void OnDestroy() => airNets.Clear();

  private bool IsTrackingIntegrity(Identifiable ident)
  {
    if (!Identifiable.IsSlime(ident.id) || Identifiable.IsTarr(ident.id) || ident.gameObject.GetComponent<QuantumSlimeSuperposition>() != null || !airNets.Any(net => net.IsNetActive()))
      return false;
    Destroyer.Metadata metadata;
    metadataDict.TryGetValue(ident.gameObject.GetInstanceID(), out metadata);
    return metadata == null || !metadata.source.Contains("DestroyOnTouching.DestroyAndWater") && !metadata.source.Contains("DestroyOutsideHoursOfDay") && !metadata.source.Contains("Vacuumable");
  }

  public void LateUpdate()
  {
    if (Time.frameCount < nextTrackFrameCount)
      return;
    if (wasRemoved.Count >= 5)
    {
      Destroyer.Metadata metadata;
      Log.Error("Found potential missing slime issue... " + new
      {
        corralID = gameObject.GetComponentInParent<IdHandler>().id,
        currentFrame = Time.frameCount,
        missingSlimes = string.Join(", ", wasRemoved.Select(id => new
        {
          instanceID = id,
          metadata = ((Func<object>) (() => metadataDict.TryGetValue(id, out metadata) ? metadata.ToString() : (object) "null"))(),
          gameObject = ((Func<object>) (() =>
          {
            foreach (Transform transform in SRSingleton<DynamicObjectContainer>.Instance.transform)
            {
              if (transform.gameObject.GetInstanceID() == id)
                return new
                {
                  name = transform.gameObject.name,
                  position = transform.gameObject.transform.position,
                  velocity = transform.gameObject.GetComponent<Rigidbody>().velocity,
                  angularVelocity = transform.gameObject.GetComponent<Rigidbody>().angularVelocity
                }.ToString();
            }
            return "null";
          }))()
        }.ToString()).ToArray())
      }.ToString());
    }
    nextTrackFrameCount += 3;
    foreach (KeyValuePair<int, Destroyer.Metadata> keyValuePair in metadataDict)
    {
      if (Time.frameCount - keyValuePair.Value.frame > 3)
        local_ToRemoveFromDict.Add(keyValuePair);
    }
    foreach (KeyValuePair<int, Destroyer.Metadata> keyValuePair in local_ToRemoveFromDict)
      metadataDict.Remove(keyValuePair.Key);
    local_ToRemoveFromDict.Clear();
    wasRemoved.Clear();
  }

  public delegate void IdentifiableEntered(
    TrackContainedIdentifiables container,
    Identifiable ident);

  public delegate void NewIdentifiableTypeEntered(
    TrackContainedIdentifiables container,
    Identifiable ident);
}
