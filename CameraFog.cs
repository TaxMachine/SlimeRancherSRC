// Decompiled with JetBrains decompiler
// Type: CameraFog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class CameraFog : MonoBehaviour
{
  public Color fogColor;
  public FogMode fogMode;
  [Header("Linear Mode Properties")]
  public float fogEndDistance = 6f;
  public float fogStartDistance = 3f;
  [Header("Exponential Mode Properties")]
  public float fogDensity = 100f;
  private bool revertFogState;
  private Color revertFogColor;
  private float revertFogDensity;
  private FogMode revertFogMode;
  private float revertFogStart;
  private float revertFogEnd;

  private void OnPreRender()
  {
    revertFogState = RenderSettings.fog;
    revertFogColor = RenderSettings.fogColor;
    revertFogDensity = RenderSettings.fogDensity;
    revertFogMode = RenderSettings.fogMode;
    revertFogStart = RenderSettings.fogStartDistance;
    revertFogEnd = RenderSettings.fogEndDistance;
    RenderSettings.fog = true;
    RenderSettings.fogColor = fogColor;
    RenderSettings.fogDensity = fogDensity;
    RenderSettings.fogMode = fogMode;
    RenderSettings.fogStartDistance = fogStartDistance;
    RenderSettings.fogEndDistance = fogEndDistance;
  }

  private void OnPostRender()
  {
    RenderSettings.fog = revertFogState;
    RenderSettings.fogColor = revertFogColor;
    RenderSettings.fogDensity = revertFogDensity;
    RenderSettings.fogMode = revertFogMode;
    RenderSettings.fogStartDistance = revertFogStart;
    RenderSettings.fogEndDistance = revertFogEnd;
  }
}
