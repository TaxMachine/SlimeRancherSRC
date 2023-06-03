// Decompiled with JetBrains decompiler
// Type: DropdownScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropdownScroller : MonoBehaviour
{
  public Dropdown dropdown;
  public ScrollRect scrollRect;
  [SerializeField]
  private float scrollPosition = 1f;

  public void Awake() => dropdown.onValueChanged.AddListener(UpdateScrollPosition);

  public void Start() => scrollRect.verticalNormalizedPosition = scrollPosition;

  public void OnDestroy() => dropdown.onValueChanged.RemoveListener(UpdateScrollPosition);

  private void UpdateScrollPosition(int index) => scrollPosition = (float) (1.0 - 1.0 * index / dropdown.options.Count);
}
