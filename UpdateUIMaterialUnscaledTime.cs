// Decompiled with JetBrains decompiler
// Type: UpdateUIMaterialUnscaledTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UpdateUIMaterialUnscaledTime : MonoBehaviour
{
  public Material[] mats;
  private int unscaledTimeVarId;
  private Graphic[] graphics;

  public void Awake()
  {
    unscaledTimeVarId = Shader.PropertyToID("_UnscaledTime");
    graphics = GetComponents<Graphic>();
  }

  public void Update()
  {
    foreach (Graphic graphic in graphics)
      graphic.materialForRendering.SetFloat(unscaledTimeVarId, Time.unscaledTime);
  }
}
