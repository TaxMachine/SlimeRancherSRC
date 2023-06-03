// Decompiled with JetBrains decompiler
// Type: PathingNetworkNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PathingNetworkNode : SRBehaviour
{
  [Tooltip("List of other nodes connected to this node.")]
  public List<PathingNetworkNode> connections;
  [Tooltip("Location transform.")]
  public Transform nodeLoc;

  public Vector3 position => nodeLoc.position;
}
