// Decompiled with JetBrains decompiler
// Type: vp_DecalManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public sealed class vp_DecalManager
{
  public static readonly vp_DecalManager instance = new vp_DecalManager();
  private static List<GameObject> m_Decals = new List<GameObject>();
  private static float m_MaxDecals = 100f;
  private static float m_FadedDecals = 20f;
  private static float m_NonFadedDecals = 0.0f;
  private static float m_FadeAmount = 0.0f;

  public static float MaxDecals
  {
    get => m_MaxDecals;
    set
    {
      m_MaxDecals = value;
      Refresh();
    }
  }

  public static float FadedDecals
  {
    get => m_FadedDecals;
    set
    {
      if (value > (double) m_MaxDecals)
      {
        Debug.LogError("FadedDecals can't be larger than MaxDecals");
      }
      else
      {
        m_FadedDecals = value;
        Refresh();
      }
    }
  }

  static vp_DecalManager() => Refresh();

  private vp_DecalManager()
  {
  }

  public static void Add(GameObject decal)
  {
    if (m_Decals.Contains(decal))
      m_Decals.Remove(decal);
    Color color = decal.GetComponent<Renderer>().material.color with
    {
      a = 1f
    };
    decal.GetComponent<Renderer>().material.color = color;
    m_Decals.Add(decal);
    FadeAndRemove();
  }

  private static void FadeAndRemove()
  {
    if (m_Decals.Count > (double) m_NonFadedDecals)
    {
      for (int index = 0; index < m_Decals.Count - (double) m_NonFadedDecals; ++index)
      {
        if (m_Decals[index] != null)
        {
          Color color = m_Decals[index].GetComponent<Renderer>().material.color;
          color.a -= m_FadeAmount;
          m_Decals[index].GetComponent<Renderer>().material.color = color;
        }
      }
    }
    if (m_Decals[0] != null)
    {
      if (m_Decals[0].GetComponent<Renderer>().material.color.a > 0.0)
        return;
      vp_Utility.Destroy(m_Decals[0]);
      m_Decals.Remove(m_Decals[0]);
    }
    else
      m_Decals.RemoveAt(0);
  }

  private static void Refresh()
  {
    if (m_MaxDecals < (double) m_FadedDecals)
      m_MaxDecals = m_FadedDecals;
    m_FadeAmount = m_MaxDecals / m_FadedDecals / m_MaxDecals;
    m_NonFadedDecals = m_MaxDecals - m_FadedDecals;
  }

  private static void DebugOutput()
  {
    int num1 = 0;
    int num2 = 0;
    foreach (GameObject decal in m_Decals)
    {
      if (decal.GetComponent<Renderer>().material.color.a == 1.0)
        ++num1;
      else
        ++num2;
    }
    Debug.Log("Decal count: " + m_Decals.Count + ", Full: " + num1 + ", Faded: " + num2);
  }
}
