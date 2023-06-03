// Decompiled with JetBrains decompiler
// Type: OverscanAdjustment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OverscanAdjustment : MonoBehaviour
{
  private OptionsDirector options;
  private RectTransform rectTransform;

  public void Awake()
  {
    options = SRSingleton<GameContext>.Instance.OptionsDirector;
    rectTransform = GetComponent<RectTransform>();
  }

  public void Update() => rectTransform.localScale = Vector3.one - Vector3.one * options.GetOverscanAdjustment();
}
