// Decompiled with JetBrains decompiler
// Type: FearProfile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Slimes/Behaviors/FearProfile")]
public class FearProfile : ScriptableObject
{
  public List<ThreatEntry> threats;
  private Dictionary<Identifiable.Id, ThreatEntry> threatsLookup = new Dictionary<Identifiable.Id, ThreatEntry>(Identifiable.idComparer);

  private void OnEnable() => InitializeFearProfilesLookup();

  private void InitializeFearProfilesLookup()
  {
    foreach (ThreatEntry threat in threats)
      threatsLookup.Add(threat.id, threat);
  }

  public float GetSearchRadius(Identifiable.Id id, float currentFearDrive) => GetThreatEntry(id).GetSearchRadius(currentFearDrive);

  public float DistToFearAdjust(Identifiable.Id id, float dist) => GetThreatEntry(id).DistToFearAdjust(dist);

  public IEnumerable<Identifiable.Id> GetThreateningIdentifiables() => threatsLookup.Keys;

  private ThreatEntry GetThreatEntry(Identifiable.Id id) => threatsLookup[id];

  [Serializable]
  public struct ThreatEntry
  {
    public Identifiable.Id id;
    public float minSearchRadius;
    public float maxSearchRadius;
    public float minThreatFearPerSec;
    public float maxThreatFearPerSec;

    public float GetSearchRadius(float currentFearDrive) => (float) (maxSearchRadius * (double) currentFearDrive + minSearchRadius * (1.0 - currentFearDrive));

    public float DistToFearAdjust(float dist) => minThreatFearPerSec + (float) ((maxSearchRadius - (double) dist) / maxSearchRadius * (maxThreatFearPerSec - (double) minThreatFearPerSec));
  }
}
