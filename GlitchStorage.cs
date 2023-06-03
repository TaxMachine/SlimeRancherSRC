// Decompiled with JetBrains decompiler
// Type: GlitchStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;

public class GlitchStorage : IdHandler<GlitchStorageModel>
{
  private const SiloStorage.StorageType STORAGE_TYPE = SiloStorage.StorageType.NON_SLIMES;
  private const int MAX_COUNT = 300;
  private GlitchStorageModel model;

  public Identifiable.Id selected => model.id;

  public int count => model.count;

  public override void Awake()
  {
    base.Awake();
    GetRequiredComponentInChildren<WorldStatusBar>().maxValue = 300f;
  }

  protected override string IdPrefix() => "glitchST";

  protected override GameModel.Unregistrant Register(GameModel game) => game.Glitch.storage.Register(this);

  protected override void InitModel(GlitchStorageModel model)
  {
  }

  protected override void SetModel(GlitchStorageModel model) => this.model = model;

  public bool Add(Identifiable.Id id)
  {
    if (model.count > 0 && model.id != id || model.count >= 300 || model.count == 0 && !SiloStorage.StorageType.NON_SLIMES.Contains(id))
      return false;
    ++model.count;
    model.id = id;
    return true;
  }

  public bool Remove(out Identifiable.Id id)
  {
    id = model.id;
    if (model.count <= 0)
      return false;
    --model.count;
    if (model.count == 0)
      model.id = Identifiable.Id.NONE;
    return true;
  }
}
