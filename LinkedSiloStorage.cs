// Decompiled with JetBrains decompiler
// Type: LinkedSiloStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class LinkedSiloStorage : SiloStorage, Gadget.LinkDestroyer, GadgetModel.Participant
{
  private Gadget.Id gadgetId;
  private WarpDepotModel model;
  private LinkedSiloStorage link;

  public override void Awake()
  {
    gadgetId = GetComponentInParent<Gadget>().id;
    foreach (GadgetSiteModel gadgetSiteModel in SRSingleton<SceneContext>.Instance.GameModel.AllGadgetSites().Values)
    {
      if (gadgetSiteModel.HasAttached() && gadgetSiteModel.attached.ident == gadgetId)
      {
        link = gadgetSiteModel.attached.transform.GetComponentInChildren<LinkedSiloStorage>();
        link.link = this;
        break;
      }
    }
    base.Awake();
  }

  public void InitModel(GadgetModel baseModel)
  {
    WarpDepotModel warpDepotModel = (WarpDepotModel) baseModel;
    LocalAmmo.InitModel(warpDepotModel.ammo);
    warpDepotModel.isPrimary = true;
  }

  public void SetModel(GadgetModel baseModel)
  {
    model = (WarpDepotModel) baseModel;
    LocalAmmo.SetModel(model.ammo);
    if (!model.isPrimary && link == null)
      model.isPrimary = true;
    if (!(link != null) || link.model == null || model.isPrimary != link.model.isPrimary)
      return;
    model.isPrimary = !ammo.IsEmpty();
    link.model.isPrimary = !model.isPrimary;
  }

  public override Ammo GetRelevantAmmo() => model.isPrimary ? ammo : link.ammo;

  public bool ShouldDestroyPair() => link != null;

  public Gadget.LinkDestroyer GetLinked() => link;
}
