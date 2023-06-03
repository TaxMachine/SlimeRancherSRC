// Decompiled with JetBrains decompiler
// Type: vp_SpawnPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class vp_SpawnPoint : MonoBehaviour
{
  public bool RandomDirection;
  public float Radius;
  public float GroundSnapThreshold = 2.5f;
  public bool LockGroundSnapToRadius = true;
  protected static List<vp_SpawnPoint> m_MatchingSpawnPoints = new List<vp_SpawnPoint>(50);
  protected static List<vp_SpawnPoint> m_SpawnPoints = null;

  public static List<vp_SpawnPoint> SpawnPoints
  {
    get
    {
      if (m_SpawnPoints == null)
        m_SpawnPoints = new List<vp_SpawnPoint>(FindObjectsOfType(typeof (vp_SpawnPoint)) as vp_SpawnPoint[]);
      return m_SpawnPoints;
    }
  }

  public static vp_Placement GetRandomPlacement() => GetRandomPlacement(0.0f, null);

  public static vp_Placement GetRandomPlacement(float physicsCheckRadius) => GetRandomPlacement(physicsCheckRadius, null);

  public static vp_Placement GetRandomPlacement(string tag) => GetRandomPlacement(0.0f, tag);

  public static vp_Placement GetRandomPlacement(float physicsCheckRadius, string tag)
  {
    if (SpawnPoints == null || SpawnPoints.Count < 1)
      return null;
    vp_SpawnPoint randomSpawnPoint;
    if (string.IsNullOrEmpty(tag))
    {
      randomSpawnPoint = GetRandomSpawnPoint();
    }
    else
    {
      randomSpawnPoint = GetRandomSpawnPoint(tag);
      if (randomSpawnPoint == null)
      {
        randomSpawnPoint = GetRandomSpawnPoint();
        Debug.LogWarning("Warning (vp_SpawnPoint --> GetRandomPlacement) Could not find a spawnpoint tagged '" + tag + "'. Falling back to 'any random spawnpoint'.");
      }
    }
    if (randomSpawnPoint == null)
    {
      Debug.LogError("Error (vp_SpawnPoint --> GetRandomPlacement) Could not find a spawnpoint" + (!string.IsNullOrEmpty(tag) ? " tagged '" + tag + "'" : ".") + " Reverting to world origin.");
      return null;
    }
    vp_Placement p = new vp_Placement();
    p.Position = randomSpawnPoint.transform.position;
    if (randomSpawnPoint.Radius > 0.0)
    {
      Vector3 vector3 = UnityEngine.Random.insideUnitSphere * randomSpawnPoint.Radius;
      p.Position.x += vector3.x;
      p.Position.z += vector3.z;
    }
    if (physicsCheckRadius != 0.0)
    {
      if (!vp_Placement.AdjustPosition(p, physicsCheckRadius))
        return null;
      vp_Placement.SnapToGround(p, physicsCheckRadius, randomSpawnPoint.GroundSnapThreshold);
    }
    p.Rotation = !randomSpawnPoint.RandomDirection ? randomSpawnPoint.transform.rotation : Quaternion.Euler(Vector3.up * UnityEngine.Random.Range(0.0f, 360f));
    return p;
  }

  public static vp_SpawnPoint GetRandomSpawnPoint() => SpawnPoints.Count < 1 ? null : SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)];

  public static vp_SpawnPoint GetRandomSpawnPoint(string tag)
  {
    m_MatchingSpawnPoints.Clear();
    for (int index = 0; index < SpawnPoints.Count; ++index)
    {
      if (m_SpawnPoints[index].tag == tag)
        m_MatchingSpawnPoints.Add(m_SpawnPoints[index]);
    }
    if (m_MatchingSpawnPoints.Count < 1)
      return null;
    return m_MatchingSpawnPoints.Count == 1 ? m_MatchingSpawnPoints[0] : m_MatchingSpawnPoints[UnityEngine.Random.Range(0, m_MatchingSpawnPoints.Count)];
  }

  private void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => m_SpawnPoints = null;
}
