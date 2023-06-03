// Decompiled with JetBrains decompiler
// Type: TeleporterGadget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterGadget : TeleportDestination, Gadget.LinkDestroyer
{
  public Image destinationIcon;
  public string linkName;
  private TeleporterGadget linked;

  public override void Awake()
  {
    TeleportSource component1 = GetComponent<TeleportSource>();
    component1.waitForExternalActivation = true;
    destinationIcon.enabled = false;
    List<TeleportDestination> destinations = SRSingleton<SceneContext>.Instance.TeleportNetwork.GetDestinations(linkName);
    if (destinations.Count == 1)
    {
      linked = (TeleporterGadget) destinations[0];
      linked.linked = this;
      teleportDestinationName = string.Format("{0}_{1}", linkName, "linked");
      component1.destinationSetName = linked.teleportDestinationName;
      component1.waitForExternalActivation = false;
      TeleportSource component2 = linked.GetComponent<TeleportSource>();
      component2.destinationSetName = teleportDestinationName;
      component2.waitForExternalActivation = false;
    }
    else
      teleportDestinationName = linkName;
    base.Awake();
  }

  public void Start()
  {
    if (!(linked != null))
      return;
    destinationIcon.sprite = ZoneDirector.GetZoneIcon(linked.gameObject);
    destinationIcon.enabled = destinationIcon.sprite != null;
    linked.destinationIcon.sprite = ZoneDirector.GetZoneIcon(gameObject);
    linked.destinationIcon.enabled = linked.destinationIcon.sprite != null;
  }

  public bool ShouldDestroyPair() => linked != null;

  public Gadget.LinkDestroyer GetLinked() => linked;
}
