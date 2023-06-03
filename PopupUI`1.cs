// Decompiled with JetBrains decompiler
// Type: PopupUI`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public abstract class PopupUI<T> : SRBehaviour
{
  protected T idEntry;

  public virtual void Init(T idEntry)
  {
    this.idEntry = idEntry;
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundleAvailable);
  }

  public abstract void OnBundleAvailable(MessageDirector msgDir);

  public virtual void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundleAvailable);
  }
}
