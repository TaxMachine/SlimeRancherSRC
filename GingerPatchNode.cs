// Decompiled with JetBrains decompiler
// Type: GingerPatchNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GingerPatchNode : IdHandler
{
  public GameObject bed;
  public FixedJoint spawnJoint;
  public GameObject gingerPrefab;
  public GameObject pulledFx;
  public GameObject disappearFx;
  public static List<GingerPatchNode> allGingerPatches = new List<GingerPatchNode>();
  private Transform jointTransform;
  private ZoneDirector zoneDirector;

  public void Awake()
  {
    jointTransform = spawnJoint.transform;
    allGingerPatches.Add(this);
    zoneDirector = GetComponentInParent<ZoneDirector>();
    zoneDirector.Register(this);
  }

  public void OnDestroy() => allGingerPatches.Remove(this);

  public void Start()
  {
    if (!(spawnJoint.connectedBody == null))
      return;
    HidePatch();
  }

  public void Grow() => Grow(InstantiateActor(gingerPrefab, zoneDirector.regionSetId, spawnJoint.transform.position, spawnJoint.transform.rotation));

  public void Grow(GameObject ginger)
  {
    bed.SetActive(true);
    spawnJoint.gameObject.SetActive(true);
    if (spawnJoint.connectedBody != null)
      Destroyer.DestroyActor(spawnJoint.connectedBody.gameObject, "GingerPatchNode.Grow");
    ginger.GetComponent<ResourceCycle>().Attach(spawnJoint, detachmentDelegate: Harvested);
  }

  public void Harvested()
  {
    HidePatch();
    if (spawnJoint.connectedBody != null)
    {
      Destroyer.DestroyActor(spawnJoint.connectedBody.gameObject, "GingerPatchNode.Harvested");
      spawnJoint.connectedBody = null;
    }
    SpawnAndPlayFX(pulledFx, jointTransform.position, jointTransform.rotation);
  }

  public void HidePatchAndReset()
  {
    HidePatch();
    if (!(spawnJoint.connectedBody != null))
      return;
    Destroyer.DestroyActor(spawnJoint.connectedBody.gameObject, "GingerPatchNode.Reset");
    spawnJoint.connectedBody = null;
    SpawnAndPlayFX(disappearFx, jointTransform.position, jointTransform.rotation);
  }

  private void HidePatch()
  {
    bed.SetActive(false);
    spawnJoint.gameObject.SetActive(false);
  }

  protected override string IdPrefix() => "gingerPatch";
}
