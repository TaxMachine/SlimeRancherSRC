// Decompiled with JetBrains decompiler
// Type: TarrBoundingSphere
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class TarrBoundingSphere : RegisteredActorBehaviour, RegistryLateUpdateable
{
  public const int TARR_BOUNDING_SPHERE_START_COUNT = 100;
  private const int ARRAY_RESIZE_STEP = 100;
  public static BoundingSphere[] allSpheres = new BoundingSphere[100];
  public static int sphereCount = 0;
  public static int nearbyTarr = 0;

  public static void ResetTarrData()
  {
    allSpheres = new BoundingSphere[100];
    sphereCount = 0;
  }

  public void RegistryLateUpdate()
  {
    if (sphereCount == allSpheres.Length)
      Array.Resize(ref allSpheres, allSpheres.Length + 100);
    allSpheres[sphereCount].position = transform.position;
    allSpheres[sphereCount].radius = 2f;
    ++sphereCount;
  }
}
