// Decompiled with JetBrains decompiler
// Type: ActorRegistry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorRegistry : MonoBehaviour
{
  private HashSet<RegistryFixedUpdateable> fixedUpdateActors = new HashSet<RegistryFixedUpdateable>();
  private ExposedArrayList<RegistryFixedUpdateable> fixedUpdateActorsList = new ExposedArrayList<RegistryFixedUpdateable>();
  private HashSet<RegistryUpdateable> updateActors = new HashSet<RegistryUpdateable>();
  private ExposedArrayList<RegistryUpdateable> updateActorsList = new ExposedArrayList<RegistryUpdateable>();
  private HashSet<RegistryLateUpdateable> lateUpdateActors = new HashSet<RegistryLateUpdateable>();
  private ExposedArrayList<RegistryLateUpdateable> lateUpdateActorsList = new ExposedArrayList<RegistryLateUpdateable>();
  private const int MIN_LOCAL_ARRAY_RESIZE_AMOUNT = 50;
  private RegistryFixedUpdateable[] FixedUpdate_localActorsData = new RegistryFixedUpdateable[50];
  private RegistryUpdateable[] Update_localActorsData = new RegistryUpdateable[50];
  private RegistryLateUpdateable[] LateUpdate_localActorsData = new RegistryLateUpdateable[50];

  public void Register(RegisteredActorBehaviour actor)
  {
    if (actor is RegistryFixedUpdateable)
      Register(actor as RegistryFixedUpdateable);
    if (actor is RegistryUpdateable)
      Register(actor as RegistryUpdateable);
    if (!(actor is RegistryLateUpdateable))
      return;
    Register(actor as RegistryLateUpdateable);
  }

  public void Deregister(RegisteredActorBehaviour actor)
  {
    if (actor is RegistryFixedUpdateable)
      Deregister(actor as RegistryFixedUpdateable);
    if (actor is RegistryUpdateable)
      Deregister(actor as RegistryUpdateable);
    if (!(actor is RegistryLateUpdateable))
      return;
    Deregister(actor as RegistryLateUpdateable);
  }

  private void Register(RegistryFixedUpdateable actor)
  {
    if (fixedUpdateActors.Contains(actor))
      return;
    fixedUpdateActors.Add(actor);
    fixedUpdateActorsList.Add(actor);
  }

  private void Deregister(RegistryFixedUpdateable actor)
  {
    if (!fixedUpdateActors.Contains(actor))
      return;
    fixedUpdateActors.Remove(actor);
    fixedUpdateActorsList.Remove(actor);
  }

  private void Register(RegistryUpdateable actor)
  {
    if (updateActors.Contains(actor))
      return;
    updateActors.Add(actor);
    updateActorsList.Add(actor);
  }

  private void Deregister(RegistryUpdateable actor)
  {
    if (!updateActors.Contains(actor))
      return;
    updateActors.Remove(actor);
    updateActorsList.Remove(actor);
  }

  private void Register(RegistryLateUpdateable actor)
  {
    if (lateUpdateActors.Contains(actor))
      return;
    lateUpdateActors.Add(actor);
    lateUpdateActorsList.Add(actor);
  }

  private void Deregister(RegistryLateUpdateable actor)
  {
    if (!lateUpdateActors.Contains(actor))
      return;
    lateUpdateActors.Remove(actor);
    lateUpdateActorsList.Remove(actor);
  }

  public void FixedUpdate()
  {
    if (fixedUpdateActorsList.Data.Length > FixedUpdate_localActorsData.Length)
      Array.Resize(ref FixedUpdate_localActorsData, Math.Max(fixedUpdateActorsList.Data.Length, 50));
    int count = fixedUpdateActorsList.GetCount();
    fixedUpdateActorsList.Data.CopyTo(FixedUpdate_localActorsData, 0);
    for (int index = 0; index < count; ++index)
    {
      RegistryFixedUpdateable registryFixedUpdateable = FixedUpdate_localActorsData[index];
      try
      {
        registryFixedUpdateable.RegistryFixedUpdate();
      }
      catch (Exception ex)
      {
        object[] objArray;
        if (registryFixedUpdateable != null)
          objArray = new object[6]
          {
            "actor.id",
            ((UnityEngine.Object) registryFixedUpdateable).GetInstanceID(),
            "actor.name",
            ((UnityEngine.Object) registryFixedUpdateable).name,
            "actor.type",
            registryFixedUpdateable.GetType()
          };
        else
          objArray = new object[0];
        Log.Error("Failed to execute ActorRegistry.FixedUpdate. (see exception below)", objArray);
        Log.Error(ex.ToString());
      }
    }
    Array.Clear(FixedUpdate_localActorsData, 0, FixedUpdate_localActorsData.Length);
  }

  public void Update()
  {
    if (updateActorsList.Data.Length > Update_localActorsData.Length)
      Array.Resize(ref Update_localActorsData, Math.Max(updateActorsList.Data.Length, 50));
    int count = updateActorsList.GetCount();
    updateActorsList.Data.CopyTo(Update_localActorsData, 0);
    for (int index = 0; index < count; ++index)
    {
      RegistryUpdateable registryUpdateable = Update_localActorsData[index];
      try
      {
        registryUpdateable.RegistryUpdate();
      }
      catch (NullReferenceException ex)
      {
        if (registryUpdateable != null)
          Log.Error("Null reference exception caught in ActorRegistry.Update", "name", ((UnityEngine.Object) registryUpdateable).name, "type", registryUpdateable.GetType(), "stacktrace", ex.StackTrace);
      }
      catch (Exception ex)
      {
        Log.Error("Unhandled exception caught in ActorRegistry.Update", "name", ((UnityEngine.Object) registryUpdateable).name, "type", registryUpdateable.GetType(), "ex", ex.Message);
      }
    }
    Array.Clear(Update_localActorsData, 0, Update_localActorsData.Length);
  }

  public void LateUpdate()
  {
    if (lateUpdateActorsList.Data.Length > LateUpdate_localActorsData.Length)
      Array.Resize(ref LateUpdate_localActorsData, Math.Max(lateUpdateActorsList.Data.Length, 50));
    int count = lateUpdateActorsList.GetCount();
    lateUpdateActorsList.Data.CopyTo(LateUpdate_localActorsData, 0);
    for (int index = 0; index < count; ++index)
    {
      RegistryLateUpdateable registryLateUpdateable = LateUpdate_localActorsData[index];
      try
      {
        registryLateUpdateable.RegistryLateUpdate();
      }
      catch (NullReferenceException ex)
      {
        if (registryLateUpdateable != null)
          Log.Error("Null reference exception caught in ActorRegistry.Update", "name", ((UnityEngine.Object) registryLateUpdateable).name, "type", registryLateUpdateable.GetType(), "stacktrace", ex.StackTrace);
      }
      catch (Exception ex)
      {
        Log.Error("Unhandled exception caught in ActorRegistry.Update", "name", ((UnityEngine.Object) registryLateUpdateable).name, "type", registryLateUpdateable.GetType(), "ex", ex.Message);
      }
    }
    Array.Clear(LateUpdate_localActorsData, 0, LateUpdate_localActorsData.Length);
  }

  private void CheckedActorBehaviour(
    Action actorUpdate,
    GameObject gObj,
    string lifeCycleName,
    object actor)
  {
    int num = CheckNanPosition(gObj, "Before", lifeCycleName, actor) ? 1 : 0;
    actorUpdate();
    if (num != 0)
      return;
    CheckNanPosition(gObj, "After", lifeCycleName, actor);
  }

  private bool CheckNanPosition(GameObject obj, string stage, string lifecycle, object behaviour)
  {
    if (!IsNanPosition(obj.transform.position))
      return false;
    Log.Error("Invalid Position Found", nameof (stage), stage, nameof (lifecycle), lifecycle, nameof (behaviour), behaviour.GetType(), "actor", obj.name);
    return true;
  }

  private bool IsNanPosition(Vector3 pos) => float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z);
}
