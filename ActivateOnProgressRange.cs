// Decompiled with JetBrains decompiler
// Type: ActivateOnProgressRange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ActivateOnProgressRange : MonoBehaviour
{
  public ProgressDirector.ProgressType progressType = ProgressDirector.ProgressType.CORPORATE_PARTNER;
  public int minProgress = int.MinValue;
  public int maxProgress = int.MaxValue;
  private ProgressDirector progressDir;

  public void Start()
  {
    progressDir = SRSingleton<SceneContext>.Instance.ProgressDirector;
    progressDir.onProgressChanged += OnProgressChanged;
    OnProgressChanged();
  }

  public void OnDestroy()
  {
    if (!(progressDir != null))
      return;
    progressDir.onProgressChanged -= OnProgressChanged;
  }

  private void OnProgressChanged()
  {
    int progress = progressDir.GetProgress(progressType);
    gameObject.SetActive(minProgress <= progress && progress <= maxProgress);
  }
}
