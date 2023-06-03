// Decompiled with JetBrains decompiler
// Type: RancherChatMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Metadata/RancherChatMetadata", fileName = "RancherChatMetadata")]
public class RancherChatMetadata : ScriptableObject
{
  [Tooltip("List of rancher chat entries.")]
  public Entry[] entries;

  [Serializable]
  public class Entry
  {
    [Tooltip("Rancher name.")]
    public RancherName rancherName;
    [Tooltip("Rancher image.")]
    public Sprite rancherImage;
    [Tooltip("Message background material.")]
    public Material messageBackground;
    [Tooltip("Message string. (optional")]
    public string messageText;
    [Tooltip("Message prefab; overrides 'messageText'. (optional)")]
    public GameObject messagePrefab;

    public enum RancherName
    {
      UNKNOWN,
      THORA,
      VIKTOR,
      OGDEN,
      MOCHI,
      BOB,
    }
  }
}
