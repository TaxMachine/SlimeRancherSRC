// Decompiled with JetBrains decompiler
// Type: DroneSubbehaviourPlexer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (Drone))]
[RequireComponent(typeof (DroneSubbehaviourDumpAmmo))]
[RequireComponent(typeof (DroneSubbehaviourIdle))]
[RequireComponent(typeof (DroneSubbehaviourRest))]
public class DroneSubbehaviourPlexer : RegisteredActorBehaviour, RegistryFixedUpdateable
{
  private DroneSubbehaviourIdle subbehaviourIdle;
  private List<Program> subbehaviourPrograms;
  private DroneSubbehaviourDumpAmmo subbehaviourDumpAmmo;
  private DroneSubbehaviourRest subbehaviourRest;
  private TimeDirector timeDirector;
  private DroneSubbehaviour currBehaviour;
  private DroneSubbehaviour nextBehaviour;
  private DroneGadget gadget;
  private float activationTime;
  private const float ACTIVATION_DELAY = 3f;

  public event OnSubbehaviourSelected onSubbehaviourSelected;

  public List<Program> Programs => subbehaviourPrograms;

  public void Awake()
  {
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    subbehaviourPrograms = new List<Program>();
    subbehaviourIdle = GetRequiredComponent<DroneSubbehaviourIdle>();
    subbehaviourDumpAmmo = GetRequiredComponent<DroneSubbehaviourDumpAmmo>();
    subbehaviourRest = GetRequiredComponent<DroneSubbehaviourRest>();
    gadget = GetRequiredComponentInParent<DroneGadget>();
    gadget.onProgramsChanged += OnGadgetProgramsChanged;
  }

  public override void Start()
  {
    base.Start();
    activationTime = Time.fixedTime + 3f;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    ForceRethink();
    if (!(gadget != null))
      return;
    gadget.onProgramsChanged -= OnGadgetProgramsChanged;
    gadget = null;
  }

  private void OnGadgetProgramsChanged(DroneMetadata.Program[] programs)
  {
    if (this.currBehaviour is DroneProgramSource || this.currBehaviour is DroneProgramDestination)
      ForceRethink();
    subbehaviourPrograms.ForEach(p => p.Destroy());
    subbehaviourPrograms.Clear();
    foreach (DroneMetadata.Program program in programs.Where(p => p.IsComplete()))
    {
      List<DroneProgramDestination> destinations = new List<DroneProgramDestination>();
      for (int index = 0; index < program.destination.types.Length; ++index)
      {
        DroneProgramDestination programDestination = gameObject.AddComponent(program.destination.types[index]) as DroneProgramDestination;
        programDestination.predicate = program.target.predicate;
        destinations.Add(programDestination);
      }
      List<DroneProgramSource> sources = new List<DroneProgramSource>();
      for (int index = 0; index < program.source.types.Length; ++index)
      {
        DroneProgramSource droneProgramSource = gameObject.AddComponent(program.source.types[index]) as DroneProgramSource;
        droneProgramSource.predicate = program.target.predicate;
        droneProgramSource.destinations = destinations;
        sources.Add(droneProgramSource);
      }
      subbehaviourPrograms.Add(new Program(sources, destinations));
    }
    DroneSubbehaviourRest currBehaviour = this.currBehaviour as DroneSubbehaviourRest;
    if (!(currBehaviour != null))
      return;
    currBehaviour.ForceRethink();
  }

  public void RegistryFixedUpdate()
  {
    if (Time.fixedTime < (double) activationTime || timeDirector.IsFastForwarding() || gadget.drone.region.Hibernated)
      return;
    if (currBehaviour == null)
    {
      currBehaviour = PickNextBehaviour();
      currBehaviour.Selected();
      nextBehaviour = null;
      if (onSubbehaviourSelected != null)
        onSubbehaviourSelected(currBehaviour);
    }
    currBehaviour.Action();
  }

  public void ForceRethink(float activationDelay = 0.0f)
  {
    activationTime = Time.fixedTime + activationDelay;
    if (!(currBehaviour != null))
      return;
    currBehaviour.Deselected();
    currBehaviour = null;
  }

  public void ForceResting()
  {
    if (!(currBehaviour != subbehaviourRest))
      return;
    nextBehaviour = subbehaviourRest;
    subbehaviourPrograms.ForEach(p => p.ResetProgram());
    ForceRethink();
  }

  public void ForceDumpAmmo(bool destructive)
  {
    subbehaviourDumpAmmo.destructive |= destructive;
    if (!(currBehaviour != subbehaviourDumpAmmo))
      return;
    nextBehaviour = subbehaviourDumpAmmo;
    subbehaviourPrograms.ForEach(p => p.ResetProgram());
    ForceRethink();
  }

  public bool IsResting() => currBehaviour is DroneSubbehaviourRest;

  public void OnFastForward()
  {
    subbehaviourPrograms.ForEach(p => p.ResetProgram());
    ForceRethink();
  }

  public bool PickNextGatherBehaviour()
  {
    if (!(nextBehaviour == null))
      return false;
    nextBehaviour = PickNextProgramBehaviour();
    return nextBehaviour != null;
  }

  private DroneSubbehaviour PickNextBehaviour()
  {
    if (nextBehaviour != null)
      return nextBehaviour;
    if (subbehaviourIdle.Relevancy())
      return subbehaviourIdle;
    DroneSubbehaviour droneSubbehaviour = PickNextProgramBehaviour();
    if (droneSubbehaviour != null)
      return droneSubbehaviour;
    if (subbehaviourDumpAmmo.Relevancy())
      return subbehaviourDumpAmmo;
    return subbehaviourRest.Relevancy() ? (DroneSubbehaviour) subbehaviourRest : throw new Exception("Failed to get next drone subbehaviour.");
  }

  private DroneSubbehaviour PickNextProgramBehaviour()
  {
    if (!subbehaviourPrograms.Any())
      return null;
    int count = subbehaviourPrograms.Count;
    if (subbehaviourPrograms[0].state == Program.State.DEPOSIT)
      ++count;
    for (int index = 0; index < count; ++index)
    {
      Program subbehaviourProgram = subbehaviourPrograms[0];
      DroneSubbehaviour droneSubbehaviour = subbehaviourProgram.PickNextBehaviour();
      if (droneSubbehaviour != null)
        return droneSubbehaviour;
      if (subbehaviourPrograms.Count > 1)
      {
        subbehaviourPrograms.RemoveAt(0);
        subbehaviourPrograms.Add(subbehaviourProgram);
      }
    }
    return null;
  }

  public delegate void OnSubbehaviourSelected(DroneSubbehaviour subbehaviour);

  public class Program
  {
    private List<DroneProgramSource> sources;
    private List<DroneProgramDestination> destinations;

    public IEnumerable<DroneProgramSource> Sources => sources;

    public IEnumerable<DroneProgramDestination> Destinations => destinations;

    public State state { get; private set; }

    public Program(List<DroneProgramSource> sources, List<DroneProgramDestination> destinations)
    {
      state = State.INACTIVE;
      this.sources = sources;
      this.destinations = destinations;
    }

    public DroneSubbehaviour PickNextBehaviour()
    {
      if (state == State.INACTIVE)
        state = State.GATHER;
      if (state == State.GATHER)
      {
        DroneSubbehaviour droneSubbehaviour = PickNextGatherBehaviour();
        if (droneSubbehaviour != null)
          return droneSubbehaviour;
        state = State.DEPOSIT;
      }
      if (state == State.DEPOSIT)
      {
        DroneSubbehaviour droneSubbehaviour = PickNextDepositBehaviour();
        if (droneSubbehaviour != null)
          return droneSubbehaviour;
        state = State.INACTIVE;
      }
      return null;
    }

    public void Destroy()
    {
      foreach (UnityEngine.Object source in sources)
        Destroyer.Destroy(source, "DroneSubbehaviourPlexer.Program.Destroy#1");
      foreach (UnityEngine.Object destination in destinations)
        Destroyer.Destroy(destination, "DroneSubbehaviourPlexer.Program.Destroy#2");
    }

    public void ResetProgram() => state = State.INACTIVE;

    private DroneSubbehaviour PickNextGatherBehaviour() => sources.FirstOrDefault(b => b.Relevancy());

    private DroneSubbehaviour PickNextDepositBehaviour() => destinations.FirstOrDefault(b => b.Relevancy(false));

    public enum State
    {
      INACTIVE,
      GATHER,
      DEPOSIT,
    }
  }
}
