// Decompiled with JetBrains decompiler
// Type: DecorizerStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using System.Collections.Generic;
using UnityEngine;

public class DecorizerStorage : IdHandler, DecorizerModel.Participant
{
  private static List<GameObject> CLEANUP_RESULTS = new List<GameObject>();
  private DecorizerModel model;
  private DecorizerModel.Settings settings;

  public Identifiable.Id selected
  {
    get => settings.selected;
    set => settings.selected = value;
  }

  protected override string IdPrefix() => "decorizer";

  public void Awake() => SRSingleton<SceneContext>.Instance.GameModel.RegisterDecorizer(this);

  public void InitModel(DecorizerModel model)
  {
  }

  public void SetModel(DecorizerModel model)
  {
    this.model = model;
    settings = model.GetSettings(id);
  }

  public void OnDecorizerRemoved(Identifiable.Id id)
  {
    if (id != selected || model.GetCount(id) != 0)
      return;
    selected = Identifiable.Id.NONE;
  }

  public bool Add(Identifiable.Id id)
  {
    if (!model.Add(id))
      return false;
    if (selected == Identifiable.Id.NONE)
      selected = id;
    return true;
  }

  public bool Remove(out Identifiable.Id id)
  {
    id = selected;
    if (model.Remove(selected))
      return true;
    id = Identifiable.Id.NONE;
    return false;
  }

  public int GetCount() => model.GetCount(selected);

  public void Cleanup(IEnumerable<Identifiable.Id> ids)
  {
    GetRequiredComponentInParent<CellDirector>().Get(ids, CLEANUP_RESULTS);
    for (int index = 0; index < CLEANUP_RESULTS.Count; ++index)
    {
      GameObject gameObject = CLEANUP_RESULTS[index];
      if (Add(Identifiable.GetId(gameObject)))
        Destroyer.DestroyActor(gameObject, "DecorizerStorage.Cleanup");
    }
    CLEANUP_RESULTS.Clear();
  }
}
