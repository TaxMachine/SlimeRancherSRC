// Decompiled with JetBrains decompiler
// Type: DroneAmmoPreview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Drone))]
public class DroneAmmoPreview : MonoBehaviour
{
  [Tooltip("Root game object to enable/disable based off ammo state.")]
  public GameObject root;
  [Tooltip("Image to update with the ammo preview.")]
  public Image[] images;
  private Drone drone;
  private Identifiable.Id previous;
  private LookupDirector lookup;

  public void Start()
  {
    lookup = SRSingleton<GameContext>.Instance.LookupDirector;
    drone = GetComponent<Drone>();
    root.SetActive(false);
  }

  public void LateUpdate()
  {
    Identifiable.Id slotName = drone.ammo.GetSlotName();
    if (slotName == previous)
      return;
    root.SetActive(slotName != 0);
    Sprite icon = root.activeSelf ? lookup.GetIcon(slotName) : null;
    foreach (Image image in images)
      image.sprite = icon;
    previous = slotName;
  }
}
