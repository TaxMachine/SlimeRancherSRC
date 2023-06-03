// Decompiled with JetBrains decompiler
// Type: TeleportDestination
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class TeleportDestination : MonoBehaviour
{
  public Transform destLoc;
  public GameObject arriveFX;
  public string teleportDestinationName;
  public bool reorient = true;

  public RegionRegistry.RegionSetId regionSetId { get; private set; }

  public virtual void Awake()
  {
    SRSingleton<SceneContext>.Instance.TeleportNetwork.Register(this);
    regionSetId = GetComponentInParent<Region>().setId;
  }

  public virtual void OnDepart()
  {
    TeleportSource component = gameObject.GetComponent<TeleportSource>();
    if (!(component != null))
      return;
    component.waitForTriggerExit = true;
  }

  public void OnArrive()
  {
    if (arriveFX != null)
      Instantiate(arriveFX, transform.position, transform.rotation);
    GetComponent<SECTR_AudioSource>().Play();
  }

  public Vector3 GetPosition() => gameObject.transform.position;

  public Vector3? GetEulerAngles() => reorient ? new Vector3?(gameObject.transform.eulerAngles) : new Vector3?();

  public virtual void OnDestroy()
  {
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.TeleportNetwork.Deregister(this);
  }

  public virtual bool IsLinkActive()
  {
    TeleportSource component = gameObject.GetComponent<TeleportSource>();
    return component == null || component.IsLinkActive();
  }
}
