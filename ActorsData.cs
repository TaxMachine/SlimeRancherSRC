// Decompiled with JetBrains decompiler
// Type: ActorsData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorsData : DataModule<ActorsData>
{
  public const int CURR_FORMAT_ID = 1;
  private ActorData[] actors;

  public ActorData[] GetActors() => actors;

  public static void AssertEquals(ActorsData dataA, ActorsData dataB)
  {
    TestUtil.Vector3Comparer vector3Comparer = new TestUtil.Vector3Comparer(0.1f);
    if (dataA.actors.Length != dataB.actors.Length)
      return;
    List<ActorData> actorDataList = new List<ActorData>(dataB.actors);
    foreach (ActorData actor in dataA.actors)
    {
      foreach (ActorData actorB in actorDataList)
      {
        if (vector3Comparer.Equals(actor.pos, actorB.pos) && actor.id == actorB.id)
        {
          AssertEqualActors(actor, actorB);
          actorDataList.Remove(actorB);
          break;
        }
      }
    }
  }

  private static void AssertEqualActors(ActorData actorA, ActorData actorB)
  {
  }

  [Serializable]
  public class ActorData
  {
    public Vector3 pos;
    public Vector3 rot;
    public Identifiable.Id id;
    public SlimeEmotionData emotions;
    public float transformTime;
    public float reproduceTime;
    public ResourceCycle.CycleData cycleData;
    public float? disabledAtTime;
  }
}
