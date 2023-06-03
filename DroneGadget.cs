// Decompiled with JetBrains decompiler
// Type: DroneGadget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneGadget : Gadget, GadgetModel.Participant
{
  [Tooltip("Drone metadata.")]
  public DroneMetadata metadata;
  [Tooltip("Drone prefab.")]
  public GameObject prefab;
  [Tooltip("Number of programs accessible to the drone.")]
  public int programCount;
  private DroneModel droneModel;

  public Drone drone { get; private set; }

  public DroneStation station { get; private set; }

  public Region region { get; private set; }

  public event OnProgramsChanged onProgramsChanged;

  public DroneMetadata.Program[] programs { get; private set; }

  public override void Awake()
  {
    base.Awake();
    station = GetComponentInChildren<DroneStation>();
    region = GetComponentInParent<Region>();
    rotationTransform = station.transform;
    InstantiateDrone();
  }

  public void OnDestroy() => drone.onDestroyed -= InstantiateDrone;

  public void InitModel(GadgetModel model)
  {
    DroneModel droneModel = (DroneModel) model;
    droneModel.programs = new DroneModel.ProgramData[programCount];
    for (int index = 0; index < droneModel.programs.Length; ++index)
      droneModel.programs[index] = new DroneModel.ProgramData()
      {
        target = "drone.target.none",
        source = "drone.behaviour.none",
        destination = "drone.behaviour.none"
      };
  }

  public void SetModel(GadgetModel model)
  {
    droneModel = (DroneModel) model;
    SetPrograms(ProgramsFromData(droneModel.programs));
  }

  public DroneMetadata.Program[] ProgramsFromData(DroneModel.ProgramData[] programData)
  {
    DroneMetadata.Program[] programArray = new DroneMetadata.Program[programData.Length];
    for (int index = 0; index < programData.Length; ++index)
    {
      DroneModel.ProgramData programDataItem = programData[index];
      programArray[index] = new DroneMetadata.Program()
      {
        target = metadata.targets.FirstOrDefault(c => c.id == programDataItem.target) ?? metadata.GetDefaultTarget(),
        source = metadata.sources.FirstOrDefault(c => c.id == programDataItem.source) ?? metadata.GetDefaultBehaviour(),
        destination = metadata.destinations.FirstOrDefault(c => c.id == programDataItem.destination) ?? metadata.GetDefaultBehaviour()
      };
    }
    return programArray;
  }

  public DroneModel.ProgramData[] DataFromPrograms(DroneMetadata.Program[] programs)
  {
    DroneModel.ProgramData[] programDataArray = new DroneModel.ProgramData[programs.Length];
    for (int index = 0; index < programs.Length; ++index)
    {
      DroneMetadata.Program program = programs[index];
      programDataArray[index] = new DroneModel.ProgramData()
      {
        target = program.target.id,
        source = program.source.id,
        destination = program.destination.id
      };
    }
    return programDataArray;
  }

  public void SetPrograms(DroneMetadata.Program[] programs)
  {
    this.programs = programs;
    droneModel.programs = DataFromPrograms(programs);
    if (onProgramsChanged == null)
      return;
    onProgramsChanged(programs);
  }

  public override void OnUserDestroyed()
  {
    base.OnUserDestroyed();
    drone.OnGadgetDestroyed();
  }

  private void InstantiateDrone()
  {
    drone = Instantiate(prefab, transform).GetComponent<Drone>();
    drone.onDestroyed += InstantiateDrone;
    drone.TeleportToStation(false);
  }

  public delegate void OnProgramsChanged(DroneMetadata.Program[] programs);
}
