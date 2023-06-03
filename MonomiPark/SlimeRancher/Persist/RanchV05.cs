// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.Persist.RanchV05
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonomiPark.SlimeRancher.Persist
{
  public class RanchV05 : VersionedPersistedDataSet<RanchV04>
  {
    public List<LandPlotV04> plots;
    public Dictionary<string, AccessDoor.State> accessDoorStates;

    public override string Identifier => "SRRANCH";

    public override uint Version => 5;

    public RanchV05()
    {
    }

    public RanchV05(RanchV04 legacyData) => UpgradeFrom(legacyData);

    protected override void LoadData(BinaryReader reader)
    {
      plots = LoadList<LandPlotV04>(reader);
      accessDoorStates = LoadDictionary(reader, r => r.ReadString(), r => (AccessDoor.State) r.ReadInt32());
    }

    protected override void WriteData(BinaryWriter writer)
    {
      WriteList(writer, plots);
      WriteDictionary(writer, accessDoorStates, (w, k) => w.Write(k), (w, v) => w.Write((int) v));
    }

    public static RanchV05 Load(BinaryReader reader)
    {
      RanchV05 ranchV05 = new RanchV05();
      ranchV05.Load(reader.BaseStream);
      return ranchV05;
    }

    protected override void UpgradeFrom(RanchV04 legacyData)
    {
      plots = LandPlotV04.UpgradeList(legacyData.plots);
      accessDoorStates = UpgradeDoorsFrom(legacyData.accessDoorStates);
    }

    public static Dictionary<string, AccessDoor.State> UpgradeDoorsFrom(
      Dictionary<Vector3V02, AccessDoor.State> legacyData)
    {
      Dictionary<Vector3, string> dictionary1 = new Dictionary<Vector3, string>();
      dictionary1[new Vector3(46.9f, 12.3f, -102.2f)] = "door0308232953";
      dictionary1[new Vector3(-251.6f, 10.4f, 14.6f)] = "door1089650386";
      dictionary1[new Vector3(162.3f, 12.4f, -133.9f)] = "door0231652753";
      dictionary1[new Vector3(25.3f, 13.6f, 181.6f)] = "door0646655257";
      dictionary1[new Vector3(-173.5f, 2.3f, 331.7f)] = "door1946130400";
      Dictionary<string, AccessDoor.State> dictionary2 = new Dictionary<string, AccessDoor.State>();
      foreach (KeyValuePair<Vector3V02, AccessDoor.State> keyValuePair1 in legacyData)
      {
        Vector3 vector3 = keyValuePair1.Key.value;
        bool flag = false;
        foreach (KeyValuePair<Vector3, string> keyValuePair2 in dictionary1)
        {
          if ((vector3 - keyValuePair2.Key).sqrMagnitude < 100.0)
          {
            dictionary2[keyValuePair2.Value] = keyValuePair1.Value;
            flag = true;
            break;
          }
        }
        if (!flag)
          Log.Warning("Failed to find gordo match during upgrade: " + vector3);
      }
      return dictionary2;
    }

    public static void AssertAreEqual(RanchV05 expected, RanchV05 actual)
    {
      for (int index = 0; index < expected.plots.Count; ++index)
        LandPlotV04.AssertAreEqual(expected.plots[index], actual.plots[index]);
      TestUtil.AssertAreEqual(expected.accessDoorStates, actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
    }

    public static void AssertAreEqual(RanchV04 expected, RanchV05 actual)
    {
      for (int index = 0; index < expected.plots.Count; ++index)
        LandPlotV04.AssertAreEqual(expected.plots[index], actual.plots[index]);
      TestUtil.AssertAreEqual(UpgradeDoorsFrom(expected.accessDoorStates), actual.accessDoorStates, (e, a) => { }, "accessDoorStates");
    }
  }
}
