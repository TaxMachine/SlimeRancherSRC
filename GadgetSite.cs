// Decompiled with JetBrains decompiler
// Type: GadgetSite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class GadgetSite : IdHandler, GadgetSiteModel.Participant
{
  public GameObject placeGadgetUIPrefab;
  private GameObject attached;
  private GadgetDirector gadgetDir;
  private GadgetSiteModel model;
  private static float ROT_SPEED = 360f;

  public void Awake()
  {
    gadgetDir = SRSingleton<SceneContext>.Instance.GadgetDirector;
    SRSingleton<SceneContext>.Instance.GameModel.RegisterGadgetSite(id, gameObject, this);
  }

  protected override string IdPrefix() => "site";

  public void OnDestroy()
  {
    if (!(attached != null))
      return;
    Gadget component = attached.GetComponent<Gadget>();
    if (!(component != null))
      return;
    gadgetDir.DecrementPlacedGadgetCount(component.id);
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.GameModel.UnregisterGadgetSite(id);
  }

  public void InitModel(GadgetSiteModel model)
  {
  }

  public void SetModel(GadgetSiteModel model) => this.model = model;

  public void SetAttached(GadgetModel gadgetModel)
  {
    if (gadgetModel == null)
    {
      attached = null;
    }
    else
    {
      attached = gadgetModel.transform.gameObject;
      Gadget component = attached.GetComponent<Gadget>();
      if (!(component != null))
        return;
      gadgetDir.IncrementPlacedGadgetCount(component.id);
    }
  }

  public virtual void Activate()
  {
    PlaceGadgetUI component = Instantiate(placeGadgetUIPrefab).GetComponent<PlaceGadgetUI>();
    if (!(component != null))
      return;
    component.SetSite(this, model);
  }

  public bool HasAttached() => attached != null;

  public Gadget.Id GetAttachedId()
  {
    if (attached == null)
      return Gadget.Id.NONE;
    Gadget component = attached.GetComponent<Gadget>();
    return !(component == null) ? component.id : Gadget.Id.NONE;
  }

  public GameObject GetAttached() => attached;

  public void DestroyAttached()
  {
    Gadget component = attached.GetComponent<Gadget>();
    if (component != null)
    {
      gadgetDir.DecrementPlacedGadgetCount(component.id);
      component.OnUserDestroyed();
    }
    Destroyer.DestroyGadget(id, attached, "GadgetSite.DestroyAttached");
    attached = null;
  }

  public void DestroyAttachedWithPair()
  {
    GadgetSite componentInParent = ((Component) attached.GetComponentInChildren<Gadget.LinkDestroyer>().GetLinked()).GetComponentInParent<GadgetSite>(true);
    DestroyAttached();
    componentInParent.DestroyAttached();
  }

  public void OnRotateCW()
  {
    if (!(attached != null))
      return;
    attached.GetComponent<Gadget>().AddRotation(ROT_SPEED * Time.deltaTime);
  }

  public void OnRotateCCW()
  {
    if (!(attached != null))
      return;
    attached.GetComponent<Gadget>().AddRotation(-ROT_SPEED * Time.deltaTime);
  }

  public void RotateToPlayer()
  {
    if (!(attached != null))
      return;
    Vector3 vector3 = SRSingleton<SceneContext>.Instance.Player.transform.position - transform.position;
    attached.transform.rotation = Quaternion.LookRotation(new Vector3(vector3.x, 0.0f, vector3.z), Vector3.up);
  }

  public bool DestroysLinkedPairOnRemoval()
  {
    Gadget component = attached.GetComponent<Gadget>();
    return component != null && component.DestroysLinkedPairOnRemoval();
  }

  public bool DestroysOnRemoval()
  {
    Gadget component = attached.GetComponent<Gadget>();
    return component != null && component.DestroysOnRemoval();
  }

  public bool DestroyingWillDestroyContents()
  {
    if (!(attached != null))
      return false;
    LinkedSiloStorage component = attached.GetComponent<LinkedSiloStorage>();
    return component != null && !component.GetRelevantAmmo().IsEmpty();
  }
}
