// Decompiled with JetBrains decompiler
// Type: MonomiPark.Controller.BaseRumbleHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace MonomiPark.Controller
{
  public abstract class BaseRumbleHandler : MonoBehaviour, RumbleHandler
  {
    protected bool rumbleEnabled = true;
    protected Dictionary<Rumble.Motor, List<Rumble>> rumbles = new Dictionary<Rumble.Motor, List<Rumble>>(Rumble.motorComparer);
    protected Dictionary<Rumble.Motor, int> aggregateRumblePower = new Dictionary<Rumble.Motor, int>(Rumble.motorComparer);
    protected List<Rumble> toRemove = new List<Rumble>();

    public void AddRumble(Rumble rumble) => rumbles[rumble.GetMotor()].Add(rumble);

    private void Awake()
    {
      rumbles[Rumble.Motor.LARGE] = new List<Rumble>();
      rumbles[Rumble.Motor.SMALL] = new List<Rumble>();
      rumbles[Rumble.Motor.LEFT] = new List<Rumble>();
      rumbles[Rumble.Motor.RIGHT] = new List<Rumble>();
    }

    private void Update()
    {
      AggregateRumbles();
      ApplyRumblePower();
      CleanupRumbles();
    }

    private void AggregateRumbles()
    {
      aggregateRumblePower[Rumble.Motor.RIGHT] = 0;
      aggregateRumblePower[Rumble.Motor.LEFT] = 0;
      aggregateRumblePower[Rumble.Motor.SMALL] = 0;
      aggregateRumblePower[Rumble.Motor.LARGE] = 0;
      foreach (KeyValuePair<Rumble.Motor, List<Rumble>> rumble1 in rumbles)
      {
        foreach (Rumble rumble2 in rumble1.Value)
          ApplyRumble(rumble2);
      }
    }

    private void ApplyRumble(Rumble rumble)
    {
      if (rumble.IsFinished())
        toRemove.Add(rumble);
      else
        aggregateRumblePower[rumble.GetMotor()] += rumble.CurrentPower();
    }

    private void CleanupRumbles()
    {
      foreach (Rumble rumble in toRemove)
        rumbles[rumble.GetMotor()].Remove(rumble);
      toRemove.Clear();
    }

    protected abstract void ApplyRumblePower();

    public void EnableRumble() => rumbleEnabled = true;

    public void DisableRumble() => rumbleEnabled = false;

    public bool IsRumbleEnabled() => rumbleEnabled;
  }
}
