// Decompiled with JetBrains decompiler
// Type: DroneStationProgramPreview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class DroneStationProgramPreview : SRBehaviour
{
  [Tooltip("Index into DroneGadget.programs to display.")]
  public int programIndex;
  [Tooltip("Image to update with the program preview.")]
  public Image image;

  public DroneGadget gadget { get; private set; }

  public void Start()
  {
    gadget = GetComponentInParent<DroneGadget>();
    gadget.onProgramsChanged += OnProgramsChanged;
    OnProgramsChanged(gadget.programs);
  }

  private void OnProgramsChanged(DroneMetadata.Program[] programs)
  {
    Sprite sprite = programs[programIndex].IsComplete() ? programs[programIndex].target.GetImage() : gadget.metadata.imageNone;
    image.enabled = sprite != null;
    image.sprite = sprite;
  }
}
