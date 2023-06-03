// Decompiled with JetBrains decompiler
// Type: PooledPlortLockParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

public class PooledPlortLockParticle : PooledConfigurableSceneParticle
{
  public MinMaxGradientData coreStartColor;
  public MinMaxGradientData wispStartColor;
  public MinMaxGradientData burstStartColor;

  protected override void ConfigureParticles()
  {
    SetColors(null, coreStartColor);
    SetColors("Wisps", wispStartColor);
    SetColors("FX Bursts", burstStartColor);
  }
}
