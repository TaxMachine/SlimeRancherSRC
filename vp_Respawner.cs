// Decompiled with JetBrains decompiler
// Type: vp_Respawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Serializable]
public class vp_Respawner : MonoBehaviour
{
  public SpawnMode m_SpawnMode;
  public string SpawnPointTag = "";
  public ObstructionSolver m_ObstructionSolver;
  public float ObstructionRadius = 1f;
  public float MinRespawnTime = 3f;
  public float MaxRespawnTime = 3f;
  public float LastRespawnTime;
  public bool SpawnOnAwake;
  public AudioClip SpawnSound;
  public GameObject[] SpawnFXPrefabs;
  protected Vector3 m_InitialPosition = Vector3.zero;
  protected Quaternion m_InitialRotation;
  protected vp_Placement Placement = new vp_Placement();
  protected Transform m_Transform;
  protected AudioSource m_Audio;
  protected bool m_IsInitialSpawnOnAwake;
  protected vp_Timer.Handle m_RespawnTimer = new vp_Timer.Handle();
  protected static Dictionary<Collider, vp_Respawner> m_Instances;
  protected static vp_Respawner m_GetInstanceResult;
  protected Renderer m_Renderer;

  public static Dictionary<Collider, vp_Respawner> Instances
  {
    get
    {
      if (m_Instances == null)
        m_Instances = new Dictionary<Collider, vp_Respawner>(100);
      return m_Instances;
    }
  }

  public Renderer Renderer
  {
    get
    {
      if (m_Renderer == null)
        m_Renderer = GetComponent<Renderer>();
      return m_Renderer;
    }
  }

  protected virtual void Awake()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
    m_Transform = transform;
    m_Audio = GetComponent<AudioSource>();
    Placement.Position = m_InitialPosition = m_Transform.position;
    Placement.Rotation = m_InitialRotation = m_Transform.rotation;
    if (m_SpawnMode == SpawnMode.SamePosition)
      SpawnPointTag = "";
    if (!SpawnOnAwake)
      return;
    m_IsInitialSpawnOnAwake = true;
    vp_Utility.Activate(gameObject, false);
    PickSpawnPoint();
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Instances.Clear();

  protected virtual void OnEnable()
  {
    if (!(GetComponent<Collider>() != null) || Instances.ContainsValue(this))
      return;
    Instances.Add(GetComponent<Collider>(), this);
  }

  protected virtual void OnDisable()
  {
  }

  protected virtual void SpawnFX()
  {
    if (!m_IsInitialSpawnOnAwake)
    {
      if (m_Audio != null)
      {
        m_Audio.pitch = Time.timeScale;
        m_Audio.PlayOneShot(SpawnSound);
      }
      if (SpawnFXPrefabs != null && SpawnFXPrefabs.Length != 0)
      {
        foreach (GameObject spawnFxPrefab in SpawnFXPrefabs)
        {
          if (spawnFxPrefab != null)
            vp_Utility.Instantiate(spawnFxPrefab, m_Transform.position, m_Transform.rotation);
        }
      }
    }
    m_IsInitialSpawnOnAwake = false;
  }

  protected virtual void Die() => vp_Timer.In(UnityEngine.Random.Range(MinRespawnTime, MaxRespawnTime), PickSpawnPoint, m_RespawnTimer);

  public virtual void PickSpawnPoint()
  {
    if (this == null)
      return;
    if (m_SpawnMode == SpawnMode.SamePosition || vp_SpawnPoint.SpawnPoints.Count < 1)
    {
      Placement.Position = m_InitialPosition;
      Placement.Rotation = m_InitialRotation;
      if (Placement.IsObstructed(ObstructionRadius))
      {
        switch (m_ObstructionSolver)
        {
          case ObstructionSolver.Wait:
            vp_Timer.In(UnityEngine.Random.Range(MinRespawnTime, MaxRespawnTime), PickSpawnPoint, m_RespawnTimer);
            return;
          case ObstructionSolver.AdjustPlacement:
            if (!vp_Placement.AdjustPosition(Placement, ObstructionRadius))
            {
              vp_Timer.In(UnityEngine.Random.Range(MinRespawnTime, MaxRespawnTime), PickSpawnPoint, m_RespawnTimer);
              return;
            }
            break;
        }
      }
    }
    else
    {
      switch (m_ObstructionSolver)
      {
        case ObstructionSolver.Wait:
          Placement = vp_SpawnPoint.GetRandomPlacement(0.0f, SpawnPointTag);
          if (Placement == null)
          {
            Placement = new vp_Placement();
            m_SpawnMode = SpawnMode.SamePosition;
            PickSpawnPoint();
          }
          if (Placement.IsObstructed(ObstructionRadius))
          {
            vp_Timer.In(UnityEngine.Random.Range(MinRespawnTime, MaxRespawnTime), PickSpawnPoint, m_RespawnTimer);
            return;
          }
          break;
        case ObstructionSolver.AdjustPlacement:
          Placement = vp_SpawnPoint.GetRandomPlacement(ObstructionRadius, SpawnPointTag);
          if (Placement == null)
          {
            vp_Timer.In(UnityEngine.Random.Range(MinRespawnTime, MaxRespawnTime), PickSpawnPoint, m_RespawnTimer);
            return;
          }
          break;
      }
    }
    Respawn();
  }

  public virtual void PickSpawnPoint(Vector3 position, Quaternion rotation)
  {
    Placement.Position = position;
    Placement.Rotation = rotation;
    Respawn();
  }

  public virtual void Respawn()
  {
    LastRespawnTime = Time.time;
    vp_Utility.Activate(gameObject);
    SpawnFX();
    if (vp_Gameplay.isMultiplayer && vp_Gameplay.isMaster)
      vp_GlobalEvent<Transform, vp_Placement>.Send("TransmitRespawn", transform.root, Placement);
    SendMessage("Reset");
    Placement.Position = m_InitialPosition;
    Placement.Rotation = m_InitialRotation;
  }

  public virtual void Reset()
  {
    if (!Application.isPlaying)
      return;
    m_Transform.position = Placement.Position;
    m_Transform.rotation = Placement.Rotation;
    if (!(GetComponent<Rigidbody>() != null) || GetComponent<Rigidbody>().isKinematic)
      return;
    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }

  public static void ResetAll(bool reInitTimers = false)
  {
    foreach (vp_Respawner vpRespawner in Instances.Values)
    {
      if (!(vpRespawner == null) && (!vp_Utility.IsActive(vpRespawner.gameObject) || vpRespawner is vp_PlayerRespawner && (vpRespawner as vp_PlayerRespawner).Player.Dead.Active))
      {
        if (reInitTimers)
          vpRespawner.Die();
        else
          vpRespawner.PickSpawnPoint();
      }
    }
  }

  public static vp_Respawner GetByCollider(Collider col)
  {
    if (!Instances.TryGetValue(col, out m_GetInstanceResult))
    {
      m_GetInstanceResult = col.transform.root.GetComponentInChildren<vp_Respawner>();
      Instances.Add(col, m_GetInstanceResult);
    }
    return m_GetInstanceResult;
  }

  public enum SpawnMode
  {
    SamePosition,
    SpawnPoint,
  }

  public enum ObstructionSolver
  {
    Wait,
    AdjustPlacement,
  }
}
