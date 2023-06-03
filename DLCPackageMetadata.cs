// Decompiled with JetBrains decompiler
// Type: DLCPackageMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DLC", menuName = "DLC/Package Metadata")]
public class DLCPackageMetadata : ScriptableObject
{
  public DLCPackage.Id id;
  public Sprite icon;
  public List<Content> contents;

  [Serializable]
  public class Content
  {
    public string id;
    public Sprite image;
    public Sprite imageLarge;
  }
}
