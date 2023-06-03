// Decompiled with JetBrains decompiler
// Type: KookadobaPatchNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using UnityEngine;

public class KookadobaPatchNode : SRBehaviour, KookadobaNodeModel.Participant
{
  public GameObject bed;
  public FixedJoint spawnJoint;
  public GameObject kookadobaPrefab;
  public GameObject pulledFx;
  public GameObject disappearFx;
  private Transform jointTransform;
  private ZoneDirector zoneDirector;

  public void Awake()
  {
    jointTransform = spawnJoint.transform;
    zoneDirector = GetComponentInParent<ZoneDirector>();
    zoneDirector.Register(this);
    SRSingleton<SceneContext>.Instance.GameModel.RegisterKookadobaNode(this);
  }

  public void Start()
  {
    if (!(spawnJoint.connectedBody == null))
      return;
    HidePatch();
  }

  public void InitModel(KookadobaNodeModel model) => model.pos = transform.position;

  public void SetModel(KookadobaNodeModel model)
  {
  }

  public void Grow()
  {
    if (spawnJoint.connectedBody != null)
    {
      ResourceCycle component = spawnJoint.connectedBody.GetComponent<ResourceCycle>();
      if (component != null)
        component.UpdateToNow();
      if (spawnJoint.connectedBody != null)
        return;
    }
    Grow(InstantiateActor(kookadobaPrefab, zoneDirector.regionSetId, spawnJoint.transform.position, spawnJoint.transform.rotation));
  }

  public void Grow(GameObject kookadoba)
  {
    bed.SetActive(true);
    spawnJoint.gameObject.SetActive(true);
    if (spawnJoint.connectedBody != null)
      Destroyer.DestroyActor(spawnJoint.connectedBody.gameObject, "KookadobaPatchNode.Grow");
    kookadoba.GetComponent<ResourceCycle>().Attach(spawnJoint, detachmentDelegate: Harvested);
  }

  public void Harvested()
  {
    HidePatch();
    if (spawnJoint.connectedBody != null)
    {
      Destroyer.Destroy(spawnJoint.connectedBody.gameObject, "KookadobaPatchNode.Harvested");
      spawnJoint.connectedBody = null;
    }
    if (!gameObject.activeInHierarchy)
      return;
    SpawnAndPlayFX(pulledFx, jointTransform.position, jointTransform.rotation);
  }

  private void HidePatch()
  {
    bed.SetActive(false);
    spawnJoint.gameObject.SetActive(false);
  }
}
