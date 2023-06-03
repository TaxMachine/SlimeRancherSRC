// Decompiled with JetBrains decompiler
// Type: GlitchBreadcrumbNetworkPather
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GlitchBreadcrumbNetworkPather : Pather
{
  protected override bool PathPredicate(Vector3 start, Vector3 end) => false;

  protected override bool NearestAccessibleNodePredicate(Vector3 start, Vector3 end) => true;
}
