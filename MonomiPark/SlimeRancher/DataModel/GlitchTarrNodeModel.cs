// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchTarrNodeModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchTarrNodeModel : IdHandlerModel
  {
    public double activationTime;

    public void Push(GlitchTarrNodeV01 persistence) => activationTime = persistence.activationTime;

    public GlitchTarrNodeV01 Pull() => new GlitchTarrNodeV01()
    {
      activationTime = activationTime
    };
  }
}
