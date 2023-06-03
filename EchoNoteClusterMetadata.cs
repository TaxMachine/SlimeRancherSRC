// Decompiled with JetBrains decompiler
// Type: EchoNoteClusterMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EchoNoteGordo", menuName = "Echo Notes/Create Cluster")]
public class EchoNoteClusterMetadata : ScriptableObject
{
  [Tooltip("List of ordered clip indices in 'cue'; add multiple clips separated by commas. (eg. '1, 2, 3').")]
  public List<string> clips;
  [Tooltip("Distance between each clip (generation only).")]
  [Range(0.0f, 20f)]
  public float distance = 2f;
}
