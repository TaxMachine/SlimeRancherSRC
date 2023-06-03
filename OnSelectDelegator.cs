// Decompiled with JetBrains decompiler
// Type: OnSelectDelegator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnSelectDelegator : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
  private UnityAction onSelectDel;

  public void SetDelegate(UnityAction onSelectDel) => this.onSelectDel = onSelectDel;

  public void OnSelect(BaseEventData data) => onSelectDel();

  public static OnSelectDelegator Create(GameObject obj, UnityAction onSelectDel)
  {
    OnSelectDelegator onSelectDelegator = obj.AddComponent<OnSelectDelegator>();
    onSelectDelegator.SetDelegate(onSelectDel);
    return onSelectDelegator;
  }
}
