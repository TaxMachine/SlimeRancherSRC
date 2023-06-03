// Decompiled with JetBrains decompiler
// Type: PooledPlortSwitchParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class PooledPlortSwitchParticle : PooledConfigurableSceneParticle
{
  public MinMaxGradientData coreStartColor;
  public MinMaxGradientData sparkleStartColor;
  public MinMaxGradientData wispStartColor;

  protected override void ConfigureParticles()
  {
    SetColors(null, coreStartColor);
    SetColors("FX Sparkle", sparkleStartColor);
    SetColors("Wisps", wispStartColor);
  }
}
