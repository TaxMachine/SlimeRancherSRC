// Decompiled with JetBrains decompiler
// Type: InitSelected
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Selectable))]
public class InitSelected : MonoBehaviour
{
  private Selectable selectable;
  public static InitSelected Current;

  public void Awake() => selectable = GetComponent<Selectable>();

  public void OnEnable()
  {
    selectable.Select();
    selectable.OnSelect(null);
    Current = this;
  }

  public void OnDisable()
  {
    selectable.OnDeselect(null);
    if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
      EventSystem.current.SetSelectedGameObject(null);
    if (!(Current == this))
      return;
    Current = null;
  }
}
