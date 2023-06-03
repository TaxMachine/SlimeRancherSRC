// Decompiled with JetBrains decompiler
// Type: SiloStorageActivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class SiloStorageActivator : MonoBehaviour, TechActivator, LandPlotModel.Participant
{
  [Tooltip("SiloCatcher script to update on slot changed.")]
  public SiloCatcher siloCatcher;
  [Tooltip("SiloSlotUI scripts to update on slot changed.")]
  public List<SiloSlotUI> siloSlotUIs;
  [Tooltip("Animator to control the active silo slot.")]
  public Animator siloSlotAnimator;
  [Tooltip("SFX played when the button is pressed. (optional)")]
  public SECTR_AudioCue onPressButtonCue;
  [Tooltip("Where we fall in the activator order")]
  public int activatorIdx;
  private Animator buttonAnimator;
  private int buttonAnimation;
  private const float TIME_BETWEEN_ACTIVATIONS = 0.4f;
  private float nextActivationTime;
  private LandPlotModel landPlotModel;

  public void Awake()
  {
    buttonAnimator = GetComponentInParent<Animator>();
    buttonAnimation = Animator.StringToHash("ButtonPressed");
  }

  public void InitModel(LandPlotModel model)
  {
  }

  public void SetModel(LandPlotModel model)
  {
    landPlotModel = model;
    OnActiveSlotChanged();
  }

  public void OnEnable()
  {
    if (landPlotModel == null)
      return;
    OnActiveSlotChanged();
  }

  public void Activate()
  {
    if (nextActivationTime >= (double) Time.time)
      return;
    nextActivationTime = Time.time + 0.4f;
    landPlotModel.siloStorageIndices[activatorIdx] = (landPlotModel.siloStorageIndices[activatorIdx] + 1) % siloSlotUIs.Count;
    SECTR_AudioSystem.Play(onPressButtonCue, transform.position, false);
    buttonAnimator.SetTrigger(buttonAnimation);
    OnActiveSlotChanged();
  }

  public GameObject GetCustomGuiPrefab() => null;

  private void OnActiveSlotChanged()
  {
    int siloStorageIndex = landPlotModel.siloStorageIndices[activatorIdx];
    siloCatcher.slotIdx = siloSlotUIs[siloStorageIndex].slotIdx;
    if (!siloSlotAnimator.gameObject.activeInHierarchy)
      return;
    siloSlotAnimator.SetInteger("slot", siloStorageIndex);
  }
}
