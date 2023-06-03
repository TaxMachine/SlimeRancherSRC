// Decompiled with JetBrains decompiler
// Type: RaycastBatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RaycastBatcher : MonoBehaviour
{
  private List<KeyValuePair<RaycastCommand, Action<RaycastHit>>> m_Requests = new List<KeyValuePair<RaycastCommand, Action<RaycastHit>>>();

  private void FixedUpdate()
  {
    int count = m_Requests.Count;
    if (count <= 0)
      return;
    NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(count, Allocator.TempJob);
    NativeArray<RaycastHit> results = new NativeArray<RaycastHit>(count, Allocator.TempJob);
    for (int index = 0; index < count; ++index)
      commands[index] = m_Requests[index].Key;
    RaycastCommand.ScheduleBatch(commands, results, count / 3).Complete();
    for (int index = 0; index < count; ++index)
      m_Requests[index].Value(results[index]);
    commands.Dispose();
    results.Dispose();
    m_Requests.Clear();
  }

  public void QueueRaycast(RaycastCommand command, Action<RaycastHit> callback)
  {
    if (callback == null)
      return;
    m_Requests.Add(new KeyValuePair<RaycastCommand, Action<RaycastHit>>(command, callback));
  }
}
