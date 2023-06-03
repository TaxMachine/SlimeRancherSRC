// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.DroneModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;
using MonomiPark.SlimeRancher.Regions;
using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class DroneModel : GadgetModel
  {
    public AmmoModel ammo = new AmmoModel();
    public List<Identifiable.Id> fashions = new List<Identifiable.Id>();
    public bool noClip;
    public ProgramData[] programs;
    public double batteryDepleteTime;
    public Vector3 position;
    public Quaternion rotation;
    public RegionRegistry.RegionSetId currRegionSetId;

    public DroneModel(Gadget.Id gadgetId, string siteId, Transform transform)
      : base(gadgetId, siteId, transform)
    {
    }

    public void Push(
      Vector3 dronePosition,
      Vector3 droneRotation,
      Ammo.Slot[] ammoSlots,
      List<Identifiable.Id> fashions,
      bool noClip,
      double batteryDepleteTime,
      List<DroneProgramV01> programs)
    {
      position = dronePosition;
      rotation = Quaternion.Euler(droneRotation);
      ammo.Push(ammoSlots);
      this.fashions = new List<Identifiable.Id>(fashions);
      this.noClip = noClip;
      this.batteryDepleteTime = batteryDepleteTime;
      this.programs = new ProgramData[programs.Count];
      int index = 0;
      foreach (DroneProgramV01 program in programs)
      {
        this.programs[index] = new ProgramData()
        {
          target = program.target,
          source = program.source,
          destination = program.destination
        };
        ++index;
      }
    }

    public void Pull(
      out Vector3 dronePosition,
      out Vector3 droneRotation,
      out Ammo.Slot[] ammoSlots,
      out List<Identifiable.Id> fashions,
      out bool noClip,
      out double batteryDepleteTime,
      out List<DroneProgramV01> programs)
    {
      dronePosition = position;
      droneRotation = rotation.eulerAngles;
      ammo.Pull(out ammoSlots);
      fashions = new List<Identifiable.Id>(this.fashions);
      noClip = this.noClip;
      batteryDepleteTime = this.batteryDepleteTime;
      programs = new List<DroneProgramV01>();
      for (int index = 0; index < this.programs.Length; ++index)
      {
        ProgramData program = this.programs[index];
        programs.Add(new DroneProgramV01()
        {
          target = program.target,
          source = program.source,
          destination = program.destination
        });
      }
    }

    public struct ProgramData
    {
      public string target;
      public string source;
      public string destination;
    }
  }
}
