// Decompiled with JetBrains decompiler
// Type: WorldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldData : DataModule<WorldData>
{
  public const int CURR_FORMAT_ID = 6;
  public float worldTime;
  public float econSeed;
  public Dictionary<Identifiable.Id, float> econSaturations;
  public Dictionary<Vector3, ResourceWater> resourceSpawnerWater;
  public Dictionary<Vector3, float> spawnerTriggerTimes;
  public Dictionary<string, bool> teleportNodeActivations;
  public Dictionary<Vector3, float> animalSpawnerTimes;
  public ExchangeDirector.Offer offer;
  public float dailyOfferCreateTime;
  public string lastRancherOfferId;
  public Dictionary<Vector3, float> liquidSourceUnits;
  public AmbianceDirector.Weather weather;
  public float weatherUntil;
  public Dictionary<Vector3, int> gordoEatenCounts;
  private const float MAX_DIST_MATCH = 5f;
  private const float MAX_DIST_MATCH_SQR = 25f;
  private const float MAX_DIST_CLOSE_MATCH = 0.1f;
  private const float MAX_DIST_CLOSE_MATCH_SQR = 0.0100000007f;

  public static void AssertEquals(WorldData dataA, WorldData dataB)
  {
  }

  private static string PrintResourceSpawnerWater(
    Dictionary<Vector3, ResourceWater> resourceSpawnerWater)
  {
    string str = "ResourceSpawnerWater: ";
    foreach (KeyValuePair<Vector3, ResourceWater> keyValuePair in resourceSpawnerWater)
      str = str + keyValuePair.Key + ":" + keyValuePair.Value.spawn + ":" + keyValuePair.Value.water + ",";
    return str;
  }

  [Serializable]
  public class ResourceWater : IEquatable<ResourceWater>
  {
    public float spawn;
    public float water;

    public ResourceWater(float spawn, float water)
    {
      this.spawn = spawn;
      this.water = water;
    }

    public bool Equals(ResourceWater that) => spawn == (double) that.spawn && water == (double) that.water;

    public override bool Equals(object o) => o is ResourceWater && Equals((ResourceWater) o);

    public override int GetHashCode() => spawn.GetHashCode() ^ water.GetHashCode();
  }
}
