// Decompiled with JetBrains decompiler
// Type: FadeAndDestroySplat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FadeAndDestroySplat : MonoBehaviour
{
  public float timeBeforeFade = 5f;
  public float minQualTimeBeforeFade = 1f;
  public float fadeTime = 1f;
  private float fadeInTime = 0.125f;
  public Texture2D[] textures;
  public GameObject splatFX;
  [Tooltip("SFX played when the fade begins.")]
  public SECTR_AudioCue onFadeBeginCue;
  private bool hasBegunFade;
  private float fadeStartTime;
  private float fadeEndTime;
  private float invFadeTime;
  private float fadeInStartTime;
  private float fadeInEndTime;
  private Projector projector;
  private const float BASE_ORTHO_SIZE = 0.5f;
  private Material mat;

  public void Awake()
  {
    projector = GetComponentInChildren<Projector>();
    mat = GetMaterial();
    fadeStartTime = Time.time + TimeForParticlesLevel(SRQualitySettings.Particles);
    fadeEndTime = fadeStartTime + fadeTime;
    fadeInStartTime = Time.time;
    fadeInEndTime = fadeInStartTime + fadeInTime;
    invFadeTime = 1f / fadeTime;
    if (textures.Length == 0)
      return;
    mat.SetTexture("_DecalTex", Randoms.SHARED.Pick(textures));
    mat.SetFloat("_Alpha", 0.0f);
  }

  private float TimeForParticlesLevel(SRQualitySettings.ParticlesLevel level)
  {
    switch (level)
    {
      case SRQualitySettings.ParticlesLevel.LOW:
        return minQualTimeBeforeFade;
      case SRQualitySettings.ParticlesLevel.MEDIUM:
        return (float) ((timeBeforeFade + (double) minQualTimeBeforeFade) * 0.5);
      case SRQualitySettings.ParticlesLevel.HIGH:
        return timeBeforeFade;
      default:
        Log.Warning("Unknown particles level: " + level);
        return minQualTimeBeforeFade;
    }
  }

  public void Update()
  {
    float time = Time.time;
    if (mat == null)
    {
      Log.Error("Updating splat for destroyed material.");
      SentrySdk.CaptureMessage("Attempting to update splat with destroyed material!");
      SRSingleton<SceneContext>.Instance.fxPool.Recycle(gameObject);
    }
    else if (mat.shader == null)
    {
      Log.Error("Updating splat for material with destroyed shader.");
      SentrySdk.CaptureMessage("Attempting to update splat with destroyed shader!");
      SRSingleton<SceneContext>.Instance.fxPool.Recycle(gameObject);
    }
    else
    {
      if (time <= (double) fadeInEndTime)
        mat.SetFloat("_Alpha", Mathf.Lerp(0.0f, 1f, (time - fadeInStartTime) / fadeInTime));
      else if (time <= (double) fadeStartTime)
        mat.SetFloat("_Alpha", 1f);
      if (time >= (double) fadeEndTime)
      {
        SRSingleton<SceneContext>.Instance.fxPool.Recycle(gameObject);
      }
      else
      {
        if (time <= (double) fadeStartTime)
          return;
        mat.SetFloat("_Alpha", (float) (1.0 - (time - (double) fadeStartTime) * invFadeTime));
        if (hasBegunFade)
          return;
        SECTR_AudioSystem.Play(onFadeBeginCue, transform.position, false);
        hasBegunFade = true;
      }
    }
  }

  protected Material GetMaterial()
  {
    Material material = Instantiate(projector.material);
    projector.material = material;
    return material;
  }

  public void SetScale(float scale) => projector.orthographicSize = scale * 0.5f;

  public void SetColors(Color topColor, Color midColor, Color btmColor)
  {
    mat.SetColor("_TopColor", topColor);
    mat.SetColor("_MiddleColor", midColor);
    mat.SetColor("_BottomColor", btmColor);
  }

  public void OnDestroy() => Destroyer.Destroy(mat, "FadeAndDestroySplat.OnDestroy");
}
