// Decompiled with JetBrains decompiler
// Type: PooledConfigurableSceneParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public abstract class PooledConfigurableSceneParticle : PooledSceneParticle
{
  protected override void InitParticle()
  {
    base.InitParticle();
    if (!(particle != null))
      return;
    ConfigureParticles();
  }

  protected abstract void ConfigureParticles();

  protected void SetColors(
    string relObjPath,
    MinMaxGradientData colorData)
  {
    Transform transform = relObjPath == null ? particle.transform : particle.transform.Find(relObjPath);
    ParticleSystem component = transform == null ? null : transform.GetComponent<ParticleSystem>();
    if (!(component != null))
      return;
    SetStartColors(component, colorData);
  }

  private static void SetStartColors(
    ParticleSystem part,
    MinMaxGradientData colorData)
  {
    ParticleSystem.MainModule main = part.main;
    ParticleSystem.MinMaxGradient minMaxGradient = (ParticleSystem.MinMaxGradient) (Gradient) null;
    switch (colorData.mode)
    {
      case ParticleSystemGradientMode.Color:
        minMaxGradient = new ParticleSystem.MinMaxGradient(colorData.color);
        break;
      case ParticleSystemGradientMode.Gradient:
        minMaxGradient = new ParticleSystem.MinMaxGradient(colorData.gradient);
        break;
      case ParticleSystemGradientMode.TwoColors:
        minMaxGradient = new ParticleSystem.MinMaxGradient(colorData.colorMin, colorData.colorMax);
        break;
      case ParticleSystemGradientMode.TwoGradients:
        minMaxGradient = new ParticleSystem.MinMaxGradient(colorData.gradientMin, colorData.gradientMax);
        break;
      case ParticleSystemGradientMode.RandomColor:
        minMaxGradient = new ParticleSystem.MinMaxGradient(colorData.gradient);
        break;
    }
    minMaxGradient.mode = colorData.mode;
    main.startColor = minMaxGradient;
  }

  [Serializable]
  public class MinMaxGradientData
  {
    public ParticleSystemGradientMode mode;
    public Color color;
    public Gradient gradient;
    public Color colorMin;
    public Color colorMax;
    public Color gradientMin;
    public Color gradientMax;
  }
}
