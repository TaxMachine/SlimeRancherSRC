// Decompiled with JetBrains decompiler
// Type: RanchData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RanchData : DataModule<RanchData>
{
  public const int CURR_FORMAT_ID = 3;
  private LandPlotData[] plots;
  private Dictionary<Vector3, AccessDoor.State> accessDoorStates;
  private const float MAX_DIST_MATCH = 5f;
  private const float MAX_DIST_MATCH_SQR = 25f;

  public LandPlotData[] GetPlots() => plots;

  public Dictionary<Vector3, AccessDoor.State> GetAccessDoorStates() => accessDoorStates;

  public static void AssertEquals(RanchData dataA, RanchData dataB)
  {
    if (dataA.plots.Length != dataB.plots.Length)
      return;
    for (int index = 0; index < dataA.plots.Length; ++index)
      AssertEqualPlots(dataA.plots[index], dataB.plots[index]);
  }

  private static void AssertEqualPlots(LandPlotData plotA, LandPlotData plotB)
  {
    TestUtil.Vector3Comparer vector3Comparer = new TestUtil.Vector3Comparer();
  }

  [Serializable]
  public class LandPlotData
  {
    public Vector3 pos;
    public Vector3 rot;
    public LandPlot.Id id;
    public List<LandPlot.Upgrade> upgrades;
    public SpawnResource.Id attachedId;
    public float attachedDeathTime;
    public Dictionary<SiloStorage.StorageType, Ammo.AmmoData[]> siloAmmo = new Dictionary<SiloStorage.StorageType, Ammo.AmmoData[]>();
    public float feederNextTime;
    public int feederPendingCount;
    public float collectorNextTime;
    public float fastforwarderDisableTime;
  }
}
