// Decompiled with JetBrains decompiler
// Type: SRQualitySettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public static class SRQualitySettings
{
  private static Settings currSettings = new Settings(LightingLevel.LOW, TextureLevel.LOW, AntialiasingMode.NONE, ShadowsLevel.NONE, ParticlesLevel.LOW, ModelDetailLevel.LOW, WaterDetailLevel.LOW, false, false);
  private static Dictionary<Level, Settings> defaults = new Dictionary<Level, Settings>();
  private static Level level;

  static SRQualitySettings()
  {
    defaults[Level.LOWEST] = new Settings(LightingLevel.LOW, TextureLevel.LOW, AntialiasingMode.NONE, ShadowsLevel.NONE, ParticlesLevel.LOW, ModelDetailLevel.LOW, WaterDetailLevel.LOW, false, false);
    defaults[Level.LOW] = new Settings(LightingLevel.MEDIUM, TextureLevel.LOW, AntialiasingMode.NONE, ShadowsLevel.LOW, ParticlesLevel.LOW, ModelDetailLevel.MEDIUM, WaterDetailLevel.LOW, false, false);
    defaults[Level.DEFAULT] = new Settings(LightingLevel.HIGH, TextureLevel.MEDIUM, AntialiasingMode.NONE, ShadowsLevel.MEDIUM, ParticlesLevel.MEDIUM, ModelDetailLevel.MEDIUM, WaterDetailLevel.MEDIUM, false, true);
    defaults[Level.HIGH] = new Settings(LightingLevel.HIGHEST, TextureLevel.HIGH, AntialiasingMode.MULTISAMPLING_2X, ShadowsLevel.MEDIUM, ParticlesLevel.HIGH, ModelDetailLevel.HIGH, WaterDetailLevel.HIGH, true, true);
    defaults[Level.VERY_HIGH] = new Settings(LightingLevel.HIGHEST, TextureLevel.HIGH, AntialiasingMode.MULTISAMPLING_8X, ShadowsLevel.HIGH, ParticlesLevel.HIGH, ModelDetailLevel.HIGH, WaterDetailLevel.HIGH, true, true);
    SetToDefaultLevels();
  }

  private static void SetToDefaultLevels() => CurrentLevel = Level.DEFAULT;

  public static void ResetProfile() => SetToDefaultLevels();

  public static void ForceLowQuality()
  {
    Log.Debug("Forcing Low Quality");
    CurrentLevel = Level.LOWEST;
  }

  public static Level CurrentLevel
  {
    get => level;
    set
    {
      level = value;
      SetDefaults(level);
    }
  }

  public static LightingLevel Lighting
  {
    get => currSettings.lighting;
    set
    {
      if (currSettings.lighting != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.lighting = value;
      UpdateFromLevels();
    }
  }

  public static TextureLevel Textures
  {
    get => currSettings.textures;
    set
    {
      if (currSettings.textures != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.textures = value;
      UpdateFromLevels();
    }
  }

  public static AntialiasingMode Antialiasing
  {
    get => currSettings.antialiasing;
    set
    {
      if (currSettings.antialiasing != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.antialiasing = value;
      UpdateFromLevels();
    }
  }

  public static ParticlesLevel Particles
  {
    get => currSettings.particles;
    set
    {
      if (currSettings.particles != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.particles = value;
      UpdateFromLevels();
    }
  }

  public static ShadowsLevel Shadows
  {
    get => currSettings.shadows;
    set
    {
      if (currSettings.shadows != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.shadows = value;
      UpdateFromLevels();
    }
  }

  public static ModelDetailLevel ModelDetail
  {
    get => currSettings.modelDetail;
    set
    {
      if (currSettings.modelDetail != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.modelDetail = value;
      UpdateFromLevels();
    }
  }

  public static WaterDetailLevel WaterDetail
  {
    get => currSettings.waterDetail;
    set
    {
      if (currSettings.waterDetail != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.waterDetail = value;
      UpdateFromLevels();
    }
  }

  public static bool AmbientOcclusion
  {
    get => currSettings.ambientOcclusion;
    set
    {
      if (currSettings.ambientOcclusion != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.ambientOcclusion = value;
      UpdateFromLevels();
    }
  }

  public static bool Bloom
  {
    get => currSettings.bloom;
    set
    {
      if (currSettings.bloom != value)
        CurrentLevel = Level.CUSTOM;
      currSettings.bloom = value;
      UpdateFromLevels();
    }
  }

  public static DepthTextureMode GetDepthTextureMode()
  {
    DepthTextureMode depthTextureMode = DepthTextureMode.Depth;
    if (currSettings.ambientOcclusion)
      depthTextureMode |= DepthTextureMode.DepthNormals;
    return depthTextureMode;
  }

  private static void SetLighting(LightingLevel level)
  {
    switch (level)
    {
      case LightingLevel.LOW:
        QualitySettings.pixelLightCount = 0;
        break;
      case LightingLevel.MEDIUM:
        QualitySettings.pixelLightCount = 1;
        break;
      case LightingLevel.HIGH:
        QualitySettings.pixelLightCount = 2;
        break;
      case LightingLevel.HIGHEST:
        QualitySettings.pixelLightCount = 4;
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetTextures(TextureLevel level)
  {
    switch (level)
    {
      case TextureLevel.LOW:
        QualitySettings.masterTextureLimit = 1;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        break;
      case TextureLevel.MEDIUM:
        QualitySettings.masterTextureLimit = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        break;
      case TextureLevel.HIGH:
        QualitySettings.masterTextureLimit = 0;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetAntialiasing(AntialiasingMode mode)
  {
    switch (mode)
    {
      case AntialiasingMode.NONE:
        QualitySettings.antiAliasing = 0;
        break;
      case AntialiasingMode.MULTISAMPLING_2X:
        QualitySettings.antiAliasing = 2;
        break;
      case AntialiasingMode.MULTISAMPLING_4X:
        QualitySettings.antiAliasing = 4;
        break;
      case AntialiasingMode.MULTISAMPLING_8X:
        QualitySettings.antiAliasing = 8;
        break;
      default:
        Log.Warning("Unknown mode: " + mode);
        break;
    }
  }

  private static void SetShadows(ShadowsLevel level)
  {
    switch (level)
    {
      case ShadowsLevel.NONE:
        QualitySettings.shadowProjection = ShadowProjection.CloseFit;
        QualitySettings.shadowDistance = 15f;
        QualitySettings.shadowNearPlaneOffset = 2f;
        QualitySettings.shadowCascades = 0;
        break;
      case ShadowsLevel.LOW:
        QualitySettings.shadowProjection = ShadowProjection.CloseFit;
        QualitySettings.shadowDistance = 20f;
        QualitySettings.shadowNearPlaneOffset = 2f;
        QualitySettings.shadowCascades = 0;
        break;
      case ShadowsLevel.MEDIUM:
        QualitySettings.shadowProjection = ShadowProjection.CloseFit;
        QualitySettings.shadowDistance = 100f;
        QualitySettings.shadowNearPlaneOffset = 2f;
        QualitySettings.shadowCascades = 2;
        QualitySettings.shadowCascade2Split = 0.333f;
        break;
      case ShadowsLevel.HIGH:
        QualitySettings.shadowProjection = ShadowProjection.StableFit;
        QualitySettings.shadowDistance = 150f;
        QualitySettings.shadowNearPlaneOffset = 2f;
        QualitySettings.shadowCascades = 4;
        QualitySettings.shadowCascade4Split = new Vector3(0.067f, 0.2f, 0.467f);
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetParticles(ParticlesLevel level)
  {
    switch (level)
    {
      case ParticlesLevel.LOW:
        QualitySettings.particleRaycastBudget = 16;
        break;
      case ParticlesLevel.MEDIUM:
        QualitySettings.particleRaycastBudget = 256;
        break;
      case ParticlesLevel.HIGH:
        QualitySettings.particleRaycastBudget = 1024;
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetModelDetail(ModelDetailLevel level)
  {
    switch (level)
    {
      case ModelDetailLevel.LOW:
        QualitySettings.skinWeights = SkinWeights.TwoBones;
        QualitySettings.lodBias = 0.5f;
        QualitySettings.maximumLODLevel = 1;
        break;
      case ModelDetailLevel.MEDIUM:
        QualitySettings.skinWeights = SkinWeights.FourBones;
        QualitySettings.lodBias = 1f;
        QualitySettings.maximumLODLevel = 0;
        break;
      case ModelDetailLevel.HIGH:
        QualitySettings.skinWeights = SkinWeights.FourBones;
        QualitySettings.lodBias = 2f;
        QualitySettings.maximumLODLevel = 0;
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetWaterDetail(WaterDetailLevel level)
  {
    switch (level)
    {
      case WaterDetailLevel.LOW:
        Shader.globalMaximumLOD = 400;
        break;
      case WaterDetailLevel.MEDIUM:
        Shader.globalMaximumLOD = 700;
        break;
      case WaterDetailLevel.HIGH:
        Shader.globalMaximumLOD = 900;
        break;
      default:
        Log.Warning("Unknown level: " + level);
        break;
    }
  }

  private static void SetDefaults(Level level)
  {
    if (!defaults.ContainsKey(level))
      return;
    Settings settings = defaults[level];
    currSettings.lighting = settings.lighting;
    currSettings.textures = settings.textures;
    currSettings.particles = settings.particles;
    currSettings.shadows = settings.shadows;
    currSettings.modelDetail = settings.modelDetail;
    currSettings.waterDetail = settings.waterDetail;
    currSettings.ambientOcclusion = settings.ambientOcclusion;
    currSettings.bloom = settings.bloom;
    currSettings.antialiasing = settings.antialiasing;
    UpdateFromLevels();
  }

  private static void UpdateFromLevels()
  {
    UpdateBaseQualityLevel();
    SetLighting(currSettings.lighting);
    SetTextures(currSettings.textures);
    SetParticles(currSettings.particles);
    SetShadows(currSettings.shadows);
    SetModelDetail(currSettings.modelDetail);
    SetAntialiasing(currSettings.antialiasing);
    SetWaterDetail(currSettings.waterDetail);
    UpdateVsync();
  }

  private static void UpdateVsync() => SRSingleton<GameContext>.Instance.OptionsDirector.UpdateVsync();

  private static void UpdateBaseQualityLevel()
  {
    bool flag1 = currSettings.particles == ParticlesLevel.HIGH;
    string str = Enum.GetName(typeof (ShadowsLevel), currSettings.shadows).ToLowerInvariant() + "Shadows_" + (flag1 ? "softParticles" : "hardParticles");
    string[] names = QualitySettings.names;
    bool flag2 = false;
    for (int index = 0; index < names.Length; ++index)
    {
      if (names[index] == str)
      {
        QualitySettings.SetQualityLevel(index, true);
        flag2 = true;
        break;
      }
    }
    if (flag2)
      return;
    Log.Warning("Did not find quality level: " + str);
  }

  public static void Pull(
    out Settings settings,
    out Level overallLevel)
  {
    settings = new Settings(currSettings);
    overallLevel = CurrentLevel;
  }

  public static void Push(Settings settings, Level overallLevel)
  {
    CurrentLevel = overallLevel;
    if (settings == null)
      return;
    currSettings = new Settings(settings);
    UpdateFromLevels();
  }

  public enum Level
  {
    LOWEST = 0,
    LOW = 1,
    DEFAULT = 2,
    HIGH = 3,
    VERY_HIGH = 4,
    CUSTOM = 1000, // 0x000003E8
  }

  public enum LightingLevel
  {
    LOW,
    MEDIUM,
    HIGH,
    HIGHEST,
  }

  public enum TextureLevel
  {
    LOW,
    MEDIUM,
    HIGH,
  }

  public enum AntialiasingMode
  {
    NONE,
    MULTISAMPLING_2X,
    MULTISAMPLING_4X,
    MULTISAMPLING_8X,
  }

  public enum ParticlesLevel
  {
    LOW,
    MEDIUM,
    HIGH,
  }

  public enum ShadowsLevel
  {
    NONE,
    LOW,
    MEDIUM,
    HIGH,
  }

  public enum ModelDetailLevel
  {
    LOW,
    MEDIUM,
    HIGH,
  }

  public enum WaterDetailLevel
  {
    LOW,
    MEDIUM,
    HIGH,
  }

  [Serializable]
  public class Settings
  {
    public LightingLevel lighting;
    public TextureLevel textures;
    public AntialiasingMode antialiasing;
    public ShadowsLevel shadows;
    public ParticlesLevel particles;
    public ModelDetailLevel modelDetail;
    public WaterDetailLevel waterDetail;
    public bool ambientOcclusion;
    public bool bloom;

    public Settings(
      LightingLevel lighting,
      TextureLevel textures,
      AntialiasingMode antialiasing,
      ShadowsLevel shadows,
      ParticlesLevel particles,
      ModelDetailLevel modelDetail,
      WaterDetailLevel waterDetail,
      bool ambientOcclusion,
      bool bloom)
    {
      this.lighting = lighting;
      this.textures = textures;
      this.antialiasing = antialiasing;
      this.shadows = shadows;
      this.particles = particles;
      this.modelDetail = modelDetail;
      this.waterDetail = waterDetail;
      this.ambientOcclusion = ambientOcclusion;
      this.bloom = bloom;
    }

    public Settings(Settings other)
    {
      lighting = other.lighting;
      textures = other.textures;
      antialiasing = other.antialiasing;
      shadows = other.shadows;
      particles = other.particles;
      modelDetail = other.modelDetail;
      waterDetail = other.waterDetail;
      ambientOcclusion = other.ambientOcclusion;
      bloom = other.bloom;
    }
  }
}
