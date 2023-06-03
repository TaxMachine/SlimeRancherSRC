// Decompiled with JetBrains decompiler
// Type: ToyProximityTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (Collider))]
public class ToyProximityTrigger : SRBehaviour
{
  [Tooltip("Fashion pairing. Slimes wearing this fashion will consider us a 'favorite' toy.")]
  public Identifiable.Id fashion;
  private const float CLEANUP_TIME_DELAY = 30f;
  private float nextCleanupTime;
  private Dictionary<GameObject, Metadata> registered = new Dictionary<GameObject, Metadata>();
  private Identifiable.Id id;

  public void Awake() => id = GetComponentInParent<Identifiable>().id;

  public void OnDestroy()
  {
    List<GameObject> list = registered.Keys.ToList();
    for (int index = 0; index < list.Count; ++index)
      Deregister(list[index]);
  }

  public void Update()
  {
    if (Time.time < (double) nextCleanupTime)
      return;
    List<GameObject> list = registered.Keys.Where(go => go == null).ToList();
    for (int index = 0; index < list.Count; ++index)
      registered.Remove(list[index]);
    nextCleanupTime = Time.time + 30f;
  }

  public void OnTriggerEnter(Collider collider)
  {
    if (collider.isTrigger)
      return;
    SlimeEmotions component1 = collider.gameObject.GetComponent<SlimeEmotions>();
    if (component1 == null)
      return;
    ReactToToyNearby component2 = collider.gameObject.GetComponent<ReactToToyNearby>();
    if (component2 == null || registered.ContainsKey(collider.gameObject))
      return;
    Register(collider.gameObject, component1, component2);
  }

  public void OnTriggerExit(Collider collider)
  {
    if (collider.isTrigger)
      return;
    Deregister(collider.gameObject);
  }

  private void Register(GameObject other, SlimeEmotions emotions, ReactToToyNearby reaction)
  {
    Metadata metadata = new Metadata();
    metadata.isFavorite = IsFavorite(other, reaction.slimeDefinition);
    registered[other] = metadata;
    emotions.AddNearbyToy(metadata.isFavorite);
    if (id != Identifiable.Id.RUBBER_DUCKY_TOY)
      return;
    SlimeEatWater component = other.GetComponent<SlimeEatWater>();
    if (!(component != null))
      return;
    component.EnterToyProximity();
  }

  private void Deregister(GameObject other)
  {
    Metadata metadata;
    if (!registered.TryGetValue(other, out metadata))
      return;
    registered.Remove(other);
    if (!(other != null))
      return;
    other.GetComponent<SlimeEmotions>().RemoveNearbyToy(metadata.isFavorite);
    if (id != Identifiable.Id.RUBBER_DUCKY_TOY)
      return;
    SlimeEatWater component = other.GetComponent<SlimeEatWater>();
    if (!(component != null))
      return;
    component.ExitToyProximity();
  }

  private bool IsFavorite(GameObject other, SlimeDefinition slimeDefinition)
  {
    if (slimeDefinition.FavoriteToys.Contains(id))
      return true;
    if (fashion != Identifiable.Id.NONE)
    {
      AttachFashions component = other.GetComponent<AttachFashions>();
      if (component != null && component.HasFashion(fashion))
        return true;
    }
    return false;
  }

  private struct Metadata
  {
    public bool isFavorite;
  }
}
