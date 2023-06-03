// Decompiled with JetBrains decompiler
// Type: AshSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class AshSource : MonoBehaviour
{
  public static List<AshSource> allAshes = new List<AshSource>();

  public virtual void Awake() => allAshes.Add(this);

  public virtual void OnDestroy() => allAshes.Remove(this);

  public virtual bool Available() => true;

  public virtual void ConsumeAsh()
  {
  }
}
