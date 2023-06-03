// Decompiled with JetBrains decompiler
// Type: MonomiPark.SlimeRancher.DataModel.GlitchStorageModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Persist;

namespace MonomiPark.SlimeRancher.DataModel
{
  public class GlitchStorageModel : IdHandlerModel
  {
    public Identifiable.Id id;
    public int count;

    public void Push(GlitchStorageV01 persistence)
    {
      id = persistence.id;
      count = persistence.count;
    }

    public GlitchStorageV01 Pull() => new GlitchStorageV01()
    {
      id = id,
      count = count
    };
  }
}
