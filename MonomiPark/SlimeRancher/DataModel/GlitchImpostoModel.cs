// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchImpostoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchImpostoModel : IdHandlerModel
  {
    public double? deactivateTime;
    public double cooldownTime;

    public void Push(GlitchImpostoV01 persistence)
    {
      deactivateTime = persistence.deactivateTime;
      cooldownTime = persistence.cooldownTime;
    }

    public GlitchImpostoV01 Pull() => new GlitchImpostoV01()
    {
      deactivateTime = deactivateTime,
      cooldownTime = cooldownTime
    };
  }
}
