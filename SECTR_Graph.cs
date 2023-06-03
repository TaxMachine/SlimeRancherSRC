// Decompiled with JetBrains decompiler
// Type: SECTR_Graph
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public static class SECTR_Graph
{
  private static List<SECTR_Sector> initialSectors = new List<SECTR_Sector>(4);
  private static List<SECTR_Sector> goalSectors = new List<SECTR_Sector>(4);
  private static SECTR_PriorityQueue<Node> openSet = new SECTR_PriorityQueue<Node>(64);
  private static Dictionary<SECTR_Portal, Node> closedSet = new Dictionary<SECTR_Portal, Node>(64);

  public static void DepthWalk(
    ref List<Node> nodes,
    SECTR_Sector root,
    SECTR_Portal.PortalFlags stopFlags,
    int maxDepth)
  {
    nodes.Clear();
    if (root == null)
      return;
    if (maxDepth == 0)
    {
      nodes.Add(new Node() { Sector = root });
    }
    else
    {
      int count1 = SECTR_Sector.All.Count;
      for (int index = 0; index < count1; ++index)
        SECTR_Sector.All[index].Visited = false;
      Stack<Node> nodeStack = new Stack<Node>(count1);
      nodeStack.Push(new Node()
      {
        Sector = root,
        Depth = 1
      });
      root.Visited = true;
      int num = 0;
      while (nodeStack.Count > 0)
      {
        Node node = nodeStack.Pop();
        nodes.Add(node);
        ++num;
        if (maxDepth < 0 || node.Depth <= maxDepth)
        {
          int count2 = node.Sector.Portals.Count;
          for (int index = 0; index < count2; ++index)
          {
            SECTR_Portal portal = node.Sector.Portals[index];
            if ((bool) (UnityEngine.Object) portal && (portal.Flags & stopFlags) == 0)
            {
              SECTR_Sector sectrSector = portal.FrontSector == node.Sector ? portal.BackSector : portal.FrontSector;
              if ((bool) (UnityEngine.Object) sectrSector && !sectrSector.Visited)
              {
                nodeStack.Push(new Node()
                {
                  Parent = node,
                  Sector = sectrSector,
                  Portal = portal,
                  Depth = node.Depth + 1
                });
                sectrSector.Visited = true;
              }
            }
          }
        }
      }
    }
  }

  public static void BreadthWalk(
    ref List<Node> nodes,
    SECTR_Sector root,
    SECTR_Portal.PortalFlags stopFlags,
    int maxDepth)
  {
    nodes.Clear();
    if (root == null)
      return;
    if (maxDepth == 0)
    {
      nodes.Add(new Node() { Sector = root });
    }
    else
    {
      int count1 = SECTR_Sector.All.Count;
      for (int index = 0; index < count1; ++index)
        SECTR_Sector.All[index].Visited = false;
      Queue<Node> nodeQueue = new Queue<Node>(count1);
      nodeQueue.Enqueue(new Node()
      {
        Sector = root,
        Depth = 0
      });
      root.Visited = true;
      int num = 0;
      while (nodeQueue.Count > 0)
      {
        Node node = nodeQueue.Dequeue();
        nodes.Add(node);
        ++num;
        if (maxDepth < 0 || node.Depth < maxDepth)
        {
          int count2 = node.Sector.Portals.Count;
          for (int index = 0; index < count2; ++index)
          {
            SECTR_Portal portal = node.Sector.Portals[index];
            if ((bool) (UnityEngine.Object) portal && (portal.Flags & stopFlags) == 0)
            {
              SECTR_Sector sectrSector = portal.FrontSector == node.Sector ? portal.BackSector : portal.FrontSector;
              if ((bool) (UnityEngine.Object) sectrSector && !sectrSector.Visited)
              {
                nodeQueue.Enqueue(new Node()
                {
                  Parent = node,
                  Sector = sectrSector,
                  Portal = portal,
                  Depth = node.Depth + 1
                });
                node.Sector.Visited = true;
              }
            }
          }
        }
      }
    }
  }

  public static void FindShortestPath(
    ref List<Node> path,
    Vector3 start,
    Vector3 goal,
    SECTR_Portal.PortalFlags stopFlags)
  {
    path.Clear();
    openSet.Clear();
    closedSet.Clear();
    SECTR_Sector.GetContaining(ref initialSectors, start);
    SECTR_Sector.GetContaining(ref goalSectors, goal);
    int count1 = initialSectors.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      SECTR_Sector initialSector = initialSectors[index1];
      if (goalSectors.Contains(initialSector))
      {
        path.Add(new Node()
        {
          Sector = initialSector
        });
        return;
      }
      int count2 = initialSector.Portals.Count;
      for (int index2 = 0; index2 < count2; ++index2)
      {
        SECTR_Portal portal = initialSector.Portals[index2];
        if ((portal.Flags & stopFlags) == 0)
        {
          Node node = new Node();
          node.Portal = portal;
          node.Sector = initialSector;
          node.ForwardTraversal = initialSector == portal.FrontSector;
          node.Cost = Vector3.SqrMagnitude(start - portal.transform.position);
          float num = Vector3.SqrMagnitude(goal - portal.transform.position);
          node.CostPlusEstimate = node.Cost + num;
          openSet.Enqueue(node);
        }
      }
    }
    while (openSet.Count > 0)
    {
      Node currentNode = openSet.Dequeue();
      SECTR_Sector sectrSector = currentNode.ForwardTraversal ? currentNode.Portal.BackSector : currentNode.Portal.FrontSector;
      if ((bool) (UnityEngine.Object) sectrSector)
      {
        if (goalSectors.Contains(sectrSector))
        {
          Node.ReconstructPath(path, currentNode);
          break;
        }
        int count3 = sectrSector.Portals.Count;
        for (int index3 = 0; index3 < count3; ++index3)
        {
          SECTR_Portal portal = sectrSector.Portals[index3];
          if (portal != currentNode.Portal && (portal.Flags & stopFlags) == 0)
          {
            Node node1 = new Node()
            {
              Parent = currentNode,
              Portal = portal,
              Sector = sectrSector,
              ForwardTraversal = sectrSector == portal.FrontSector
            };
            node1.Cost = currentNode.Cost + Vector3.SqrMagnitude(node1.Portal.transform.position - currentNode.Portal.transform.position);
            float num = Vector3.SqrMagnitude(goal - node1.Portal.transform.position);
            node1.CostPlusEstimate = node1.Cost + num;
            Node node2 = null;
            closedSet.TryGetValue(node1.Portal, out node2);
            if (node2 == null || node2.CostPlusEstimate >= (double) node1.CostPlusEstimate)
            {
              Node node3 = null;
              for (int index4 = 0; index4 < openSet.Count; ++index4)
              {
                if (openSet[index4].Portal == node1.Portal)
                {
                  node3 = openSet[index4];
                  break;
                }
              }
              if (node3 == null || node3.CostPlusEstimate >= (double) node1.CostPlusEstimate)
                openSet.Enqueue(node1);
            }
          }
        }
        if (!closedSet.ContainsKey(currentNode.Portal))
          closedSet.Add(currentNode.Portal, currentNode);
      }
    }
  }

  public static string GetGraphAsDot(string graphName)
  {
    string str = "graph " + graphName + " {\n" + "\tlayout=neato\n";
    foreach (SECTR_Portal sectrPortal in SECTR_Portal.All)
    {
      str += "\t";
      str += (string) (object) sectrPortal.GetInstanceID();
      str += " [";
      str = str + "label=" + sectrPortal.name;
      str += ",shape=hexagon";
      str += "];\n";
    }
    foreach (SECTR_Sector sectrSector in SECTR_Sector.All)
    {
      str += "\t";
      str += (string) (object) sectrSector.GetInstanceID();
      str += " [";
      str = str + "label=" + sectrSector.name;
      str += ",shape=box";
      str += "];\n";
    }
    foreach (SECTR_Portal sectrPortal in SECTR_Portal.All)
    {
      if ((bool) (UnityEngine.Object) sectrPortal.FrontSector)
      {
        str += "\t";
        str = str + sectrPortal.GetInstanceID() + " -- " + sectrPortal.FrontSector.GetInstanceID();
        str += ";\n";
      }
      if ((bool) (UnityEngine.Object) sectrPortal.BackSector)
      {
        str += "\t";
        str = str + sectrPortal.GetInstanceID() + " -- " + sectrPortal.BackSector.GetInstanceID();
        str += ";\n";
      }
    }
    return str + "\n}";
  }

  public class Node : IComparable<Node>
  {
    public SECTR_Portal Portal;
    public SECTR_Sector Sector;
    public float CostPlusEstimate;
    public float Cost;
    public int Depth;
    public bool ForwardTraversal;
    public Node Parent;

    public int CompareTo(Node other)
    {
      if (CostPlusEstimate > (double) other.CostPlusEstimate)
        return 1;
      return CostPlusEstimate < (double) other.CostPlusEstimate ? -1 : 0;
    }

    public static void ReconstructPath(List<Node> path, Node currentNode)
    {
      if (currentNode == null)
        return;
      path.Insert(0, currentNode);
      ReconstructPath(path, currentNode.Parent);
    }
  }
}
