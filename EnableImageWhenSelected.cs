// Decompiled with JetBrains decompiler
// Type: EnableImageWhenSelected
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class EnableImageWhenSelected : MonoBehaviour
{
  public bool gamepadModeOnly = true;
  private Selectable selectableParent;
  private Image img;

  public void Start()
  {
    img = GetComponent<Image>();
    selectableParent = GetComponentInParent<Selectable>();
  }

  public void Update() => img.enabled = (!gamepadModeOnly || InputDirector.UsingGamepad()) && selectableParent != null && selectableParent.gameObject == EventSystem.current.currentSelectedGameObject;
}
