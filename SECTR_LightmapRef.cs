// Decompiled with JetBrains decompiler
// Type: SECTR_LightmapRef
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class SECTR_LightmapRef : MonoBehaviour
{
  [SerializeField]
  [HideInInspector]
  private List<RefData> lightmapRefs = new List<RefData>();
  private static int[] globalLightmapRefCount;

  public List<RefData> LightmapRefs => lightmapRefs;

  public static void InitRefCounts()
  {
    int length = LightmapSettings.lightmaps.Length;
    if (globalLightmapRefCount == null || globalLightmapRefCount.Length != length)
      globalLightmapRefCount = new int[length];
    for (int index = 0; index < length; ++index)
    {
      LightmapData lightmap = LightmapSettings.lightmaps[index];
      globalLightmapRefCount[index] = (bool) (UnityEngine.Object) lightmap.lightmapColor || (bool) (UnityEngine.Object) lightmap.lightmapDir ? 1 : 0;
    }
  }

  private void Start()
  {
    if (Application.isEditor && !Application.isPlaying || globalLightmapRefCount == null)
      return;
    int length = LightmapSettings.lightmaps.Length;
    int count = lightmapRefs.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      RefData lightmapRef = lightmapRefs[index1];
      if (lightmapRef.index >= 0 && lightmapRef.index < globalLightmapRefCount.Length)
      {
        if (globalLightmapRefCount[lightmapRef.index] == 0)
        {
          LightmapData lightmapData = new LightmapData();
          lightmapData.lightmapDir = lightmapRef.NearLightmap;
          lightmapData.lightmapColor = lightmapRef.FarLightmap;
          LightmapData[] lightmapDataArray = new LightmapData[length];
          for (int index2 = 0; index2 < length; ++index2)
            lightmapDataArray[index2] = lightmapRef.index != index2 ? LightmapSettings.lightmaps[index2] : lightmapData;
          LightmapSettings.lightmaps = lightmapDataArray;
        }
        ++globalLightmapRefCount[lightmapRef.index];
      }
    }
  }

  private void OnDestroy()
  {
    if (Application.isEditor && !Application.isPlaying || globalLightmapRefCount == null)
      return;
    int length = LightmapSettings.lightmaps.Length;
    int count = lightmapRefs.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      RefData lightmapRef = lightmapRefs[index1];
      if (lightmapRef.index >= 0 && lightmapRef.index < globalLightmapRefCount.Length)
      {
        --globalLightmapRefCount[lightmapRef.index];
        if (globalLightmapRefCount[lightmapRef.index] == 0)
        {
          LightmapData[] lightmapDataArray = new LightmapData[length];
          for (int index2 = 0; index2 < length; ++index2)
            lightmapDataArray[index2] = lightmapRef.index != index2 ? LightmapSettings.lightmaps[index2] : new LightmapData();
          LightmapSettings.lightmaps = lightmapDataArray;
        }
      }
    }
  }

  [Serializable]
  public class RefData
  {
    public Texture2D FarLightmap;
    public Texture2D NearLightmap;
    public int index = -1;
  }
}
