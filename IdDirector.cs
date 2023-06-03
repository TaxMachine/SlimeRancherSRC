// Decompiled with JetBrains decompiler
// Type: IdDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class IdDirector : SRBehaviour, ISerializationCallbackReceiver
{
  private Dictionary<IdHandler, string> persistenceDict = new Dictionary<IdHandler, string>();
  [SerializeField]
  [HideInInspector]
  private List<IdHandler> persistenceKeys = new List<IdHandler>();
  [SerializeField]
  [HideInInspector]
  private List<string> persistenceValues = new List<string>();

  public string GetPersistenceIdentifier(IdHandler instance)
  {
    string str;
    return persistenceDict.TryGetValue(instance, out str) ? str : null;
  }

  public void OnBeforeSerialize()
  {
    persistenceKeys = new List<IdHandler>(persistenceDict.Keys);
    persistenceValues = new List<string>(persistenceDict.Values);
  }

  public void OnAfterDeserialize()
  {
    persistenceDict = new Dictionary<IdHandler, string>();
    for (int index = 0; index < persistenceKeys.Count; ++index)
    {
      IdHandler persistenceKey = persistenceKeys[index];
      if (persistenceKey != null)
        persistenceDict[persistenceKey] = persistenceValues[index];
    }
    persistenceKeys = null;
    persistenceValues = null;
  }
}
