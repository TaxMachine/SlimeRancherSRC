// Decompiled with JetBrains decompiler
// Type: QuicksilverHoloSigns
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class QuicksilverHoloSigns : MonoBehaviour
{
  [Tooltip("Energy generator required to activate the signs.")]
  public QuicksilverEnergyGenerator generator;
  [Tooltip("Renderers expecting '_SpiralColor' to be updated.")]
  public List<Renderer> rendersToUpdate;
  [Tooltip("Objects to set active/inactive based off the generator state.")]
  public List<GameObject> partsToToggle;

  public void Awake() => generator.onStateChanged += OnGeneratorStateChanged;

  public void OnDestroy() => generator.onStateChanged -= OnGeneratorStateChanged;

  private void OnGeneratorStateChanged()
  {
    bool flag = generator.GetState() == QuicksilverEnergyGenerator.State.ACTIVE;
    foreach (GameObject gameObject in partsToToggle)
      gameObject.SetActive(flag);
    float num = flag ? 0.5f : 0.0f;
    foreach (Renderer renderer in rendersToUpdate)
      renderer.material.SetFloat("_SpiralColor", num);
  }
}
