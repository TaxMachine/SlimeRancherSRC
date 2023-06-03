// Decompiled with JetBrains decompiler
// Type: Pather
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pather
{
  protected PathingNetworkNode[] nodes = new PathingNetworkNode[0];

  public void RecalculateNodeConnections(
    PathingNetworkNode[] nodes,
    List<NodePair> whitelist,
    List<NodePair> blacklist)
  {
    this.nodes = nodes;
    foreach (PathingNetworkNode node in nodes)
      node.connections = new List<PathingNetworkNode>();
    HashSet<NodePair> nodePairSet1 = new HashSet<NodePair>(whitelist);
    HashSet<NodePair> nodePairSet2 = new HashSet<NodePair>(blacklist);
    for (int index1 = 0; index1 < nodes.Length; ++index1)
    {
      PathingNetworkNode node1 = nodes[index1];
      for (int index2 = index1 + 1; index2 < nodes.Length; ++index2)
      {
        PathingNetworkNode node2 = nodes[index2];
        NodePair nodePair = new NodePair(nodes[index1], nodes[index2]);
        if (!nodePairSet2.Contains(nodePair) && (nodePairSet1.Contains(nodePair) || PathPredicate(node1.position, node2.position)))
        {
          node1.connections.Add(node2);
          node2.connections.Add(node1);
        }
      }
    }
  }

  public List<PathingNetworkNode> GeneratePathNodes(Vector3 start, Vector3 end)
  {
    if (PathPredicate(start, end))
      return new List<PathingNetworkNode>();
    PathingNetworkNode key1 = NearestAccessibleNode(start);
    if (key1 == null)
      return null;
    PathingNetworkNode pathingNetworkNode1 = NearestAccessibleNode(end);
    if (pathingNetworkNode1 == null)
      return null;
    HashSet<PathingNetworkNode> pathingNetworkNodeSet = new HashSet<PathingNetworkNode>();
    HashSet<PathingNetworkNode> source = new HashSet<PathingNetworkNode>();
    source.Add(key1);
    Dictionary<PathingNetworkNode, PathingNetworkNode> cameFrom = new Dictionary<PathingNetworkNode, PathingNetworkNode>();
    Dictionary<PathingNetworkNode, float> dict = new Dictionary<PathingNetworkNode, float>();
    dict[key1] = 0.0f;
    Dictionary<PathingNetworkNode, float> fScore = new Dictionary<PathingNetworkNode, float>();
    Dictionary<PathingNetworkNode, float> dictionary1 = fScore;
    PathingNetworkNode key2 = key1;
    Vector3 vector3 = key1.position - pathingNetworkNode1.position;
    double magnitude1 = vector3.magnitude;
    dictionary1[key2] = (float) magnitude1;
    while (source.Count > 0)
    {
      PathingNetworkNode pathingNetworkNode2 = source.OrderBy(node => fScore[node]).First();
      if (pathingNetworkNode2 == pathingNetworkNode1)
      {
        List<PathingNetworkNode> path = ConstructPathFromAStarResults(cameFrom, pathingNetworkNode2);
        TrimPathEnds(path, start, end);
        return path;
      }
      source.Remove(pathingNetworkNode2);
      pathingNetworkNodeSet.Add(pathingNetworkNode2);
      foreach (PathingNetworkNode connection in pathingNetworkNode2.connections)
      {
        if (!pathingNetworkNodeSet.Contains(connection))
        {
          if (!source.Contains(connection))
            source.Add(connection);
          double valueOrDefault = GetValueOrDefault(dict, pathingNetworkNode2, float.PositiveInfinity);
          vector3 = pathingNetworkNode2.position - connection.position;
          double magnitude2 = vector3.magnitude;
          float num1 = (float) (valueOrDefault + magnitude2);
          if (num1 < (double) GetValueOrDefault(dict, connection, float.PositiveInfinity))
          {
            cameFrom[connection] = pathingNetworkNode2;
            dict[connection] = num1;
            Dictionary<PathingNetworkNode, float> dictionary2 = fScore;
            PathingNetworkNode key3 = connection;
            double num2 = num1;
            vector3 = connection.position - pathingNetworkNode1.position;
            double magnitude3 = vector3.magnitude;
            double num3 = num2 + magnitude3;
            dictionary2[key3] = (float) num3;
          }
        }
      }
    }
    return null;
  }

  public Queue<Vector3> GeneratePath(Vector3 start, Vector3 end)
  {
    List<PathingNetworkNode> pathNodes = GeneratePathNodes(start, end);
    if (pathNodes == null)
      return null;
    Queue<Vector3> path = new Queue<Vector3>(pathNodes.Select(n => n.position));
    path.Enqueue(end);
    return path;
  }

  protected abstract bool PathPredicate(Vector3 start, Vector3 end);

  protected abstract bool NearestAccessibleNodePredicate(Vector3 start, Vector3 end);

  private PathingNetworkNode NearestAccessibleNode(Vector3 pos) => nodes.Where(n => NearestAccessibleNodePredicate(n.position, pos)).OrderBy(n => (n.position - pos).sqrMagnitude).FirstOrDefault();

  private V GetValueOrDefault<K, V>(Dictionary<K, V> dict, K key, V defVal) => dict.ContainsKey(key) ? dict[key] : defVal;

  private List<PathingNetworkNode> ConstructPathFromAStarResults(
    Dictionary<PathingNetworkNode, PathingNetworkNode> cameFrom,
    PathingNetworkNode goal)
  {
    List<PathingNetworkNode> pathingNetworkNodeList = new List<PathingNetworkNode>();
    pathingNetworkNodeList.Add(goal);
    PathingNetworkNode key = goal;
    while (cameFrom.ContainsKey(key))
    {
      key = cameFrom[key];
      pathingNetworkNodeList.Add(key);
    }
    pathingNetworkNodeList.Reverse();
    return pathingNetworkNodeList;
  }

  private void TrimPathEnds(List<PathingNetworkNode> path, Vector3 start, Vector3 end)
  {
    for (int index = path.Count - 1; index >= 0; --index)
    {
      if (PathPredicate(start, path[index].position))
      {
        path.RemoveRange(0, index);
        break;
      }
    }
    for (int index = 0; index < path.Count - 1; ++index)
    {
      if (PathPredicate(path[index].position, end))
      {
        path.RemoveRange(index + 1, path.Count - (index + 1));
        break;
      }
    }
  }

  [Serializable]
  public class NodePair : IEquatable<NodePair>
  {
    public PathingNetworkNode node1;
    public PathingNetworkNode node2;

    public NodePair(PathingNetworkNode node1, PathingNetworkNode node2)
    {
      this.node1 = node1;
      this.node2 = node2;
    }

    public bool Equals(NodePair other)
    {
      if (node1 == other.node1 && node2 == other.node2)
        return true;
      return node1 == other.node2 && node2 == other.node1;
    }

    public override int GetHashCode() => node1 == null || node2 == null ? 0 : node1.GetHashCode() ^ node2.GetHashCode();
  }
}
