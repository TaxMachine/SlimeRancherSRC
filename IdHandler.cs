// Decompiled with JetBrains decompiler
// Type: IdHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class IdHandler : SRBehaviour
{
  private IdDirector director;

  public string id
  {
    get
    {
      if (director == null)
        director = GetRequiredComponentInParent<IdDirector>();
      return director.GetPersistenceIdentifier(this);
    }
  }

  protected abstract string IdPrefix();
}
