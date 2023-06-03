// Decompiled with JetBrains decompiler
// Type: GardenCountdownUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class GardenCountdownUI : MonoBehaviour
{
  public GameObject mainPanel;
  public Image cropImg;
  public LandPlot plot;

  public void Update()
  {
    Identifiable.Id attachedCropId = plot.GetAttachedCropId();
    mainPanel.SetActive(attachedCropId != 0);
    if (attachedCropId == Identifiable.Id.NONE)
      return;
    cropImg.sprite = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(attachedCropId);
  }
}
