// Decompiled with JetBrains decompiler
// Type: GlitchBreadcrumbNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

public class GlitchBreadcrumbNetwork : PathingNetwork
{
  private GlitchBreadcrumbNetworkPather pather = new GlitchBreadcrumbNetworkPather();
  private List<GlitchBreadcrumbNetworkNode> activeBreadcrumbs;
  private GlitchTeleportDestination exitDestination;

  public override Pather Pather => pather;

  public void Update()
  {
    List<PathingNetworkNode> breadcrumbs = null;
    if (exitDestination != null && exitDestination.IsLinkActive())
      breadcrumbs = Pather.GeneratePathNodes(SRSingleton<SceneContext>.Instance.Player.transform.position, exitDestination.transform.position);
    if (breadcrumbs == null == (activeBreadcrumbs == null) && (breadcrumbs == null || !(breadcrumbs[0] != activeBreadcrumbs[0])))
      return;
    OnBreadcrumbsChanged(breadcrumbs);
  }

  public void OnDisable() => OnBreadcrumbsChanged(null);

  public void OnGlitchRegionLoaded()
  {
    foreach (GlitchTeleportDestination destination in SRSingleton<GlitchRegionHelper>.Instance.destinations)
      destination.onExitTeleporterBecameActive += OnExitTeleporterBecameActive;
  }

  private void OnExitTeleporterBecameActive(GlitchTeleportDestination destination) => exitDestination = destination;

  private void OnBreadcrumbsChanged(List<PathingNetworkNode> breadcrumbs)
  {
    if (activeBreadcrumbs != null)
    {
      activeBreadcrumbs.ForEach(b => b.Deactivate());
      activeBreadcrumbs = null;
    }
    if (breadcrumbs == null || !breadcrumbs.Any())
      return;
    activeBreadcrumbs = breadcrumbs.Cast<GlitchBreadcrumbNetworkNode>().ToList();
    for (int index = 0; index < activeBreadcrumbs.Count; ++index)
      activeBreadcrumbs[index].Activate(index + 1 >= activeBreadcrumbs.Count ? exitDestination.transform.position : activeBreadcrumbs[index + 1].position);
  }
}
