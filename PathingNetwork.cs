// Decompiled with JetBrains decompiler
// Type: PathingNetwork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public abstract class PathingNetwork : SRBehaviour
{
  [Tooltip("GameObject parenting the PathingNetworkNode.")]
  public GameObject nodesParent;
  [Tooltip("List of node pairings on the whitelist.")]
  public List<Pather.NodePair> whitelistConnections;
  [Tooltip("List of node pairings on the blacklist.")]
  public List<Pather.NodePair> blacklistConnections;
  [Tooltip("Enable/disable drawing of the network node gizmos.")]
  public bool drawNodeGizmos;
  [Tooltip("Enable/disable drawing of the network connection override gizmos.")]
  public bool drawOverrideGizmos;

  public abstract Pather Pather { get; }

  public PathingNetworkNode[] Nodes => !(nodesParent != null) ? new PathingNetworkNode[0] : nodesParent.GetComponentsInChildren<PathingNetworkNode>(true);

  public virtual void Awake() => RecalculateNodeConnections();

  public void RecalculateNodeConnections() => Pather.RecalculateNodeConnections(Nodes, whitelistConnections, blacklistConnections);

  public Queue<Vector3> GeneratePath(Vector3 start, Vector3 end) => Pather.GeneratePath(start, end);
}
