﻿// Decompiled with JetBrains decompiler
// Type: vp_FootstepManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class vp_FootstepManager : MonoBehaviour
{
  private static vp_FootstepManager[] m_FootstepManagers;
  public static bool mIsDirty = true;
  public List<vp_SurfaceTypes> SurfaceTypes = new List<vp_SurfaceTypes>();
  protected vp_FPPlayerEventHandler m_Player;
  protected vp_FPCamera m_Camera;
  protected vp_FPController m_Controller;
  protected AudioSource m_Audio;
  protected AudioClip m_SoundToPlay;
  protected AudioClip m_LastPlayedSound;

  public static vp_FootstepManager[] FootstepManagers
  {
    get
    {
      if (mIsDirty)
      {
        mIsDirty = false;
        m_FootstepManagers = FindObjectsOfType(typeof (vp_FootstepManager)) as vp_FootstepManager[];
        if (m_FootstepManagers == null)
          m_FootstepManagers = Resources.FindObjectsOfTypeAll(typeof (vp_FootstepManager)) as vp_FootstepManager[];
      }
      return m_FootstepManagers;
    }
  }

  public bool IsDirty => mIsDirty;

  protected virtual void Awake()
  {
    m_Player = transform.root.GetComponentInChildren<vp_FPPlayerEventHandler>();
    m_Camera = transform.root.GetComponentInChildren<vp_FPCamera>();
    m_Controller = transform.root.GetComponentInChildren<vp_FPController>();
    m_Audio = gameObject.AddComponent<AudioSource>();
  }

  public virtual void SetDirty(bool dirty) => mIsDirty = dirty;

  private void Update()
  {
    if (m_Camera.BobStepCallback != null)
      return;
    m_Camera.BobStepCallback += Footstep;
  }

  protected virtual void OnEnable() => m_Camera.BobStepCallback += Footstep;

  protected virtual void OnDisable() => m_Camera.BobStepCallback -= Footstep;

  protected virtual void Footstep()
  {
    if (m_Player.Dead.Active || !m_Controller.Grounded || m_Player.GroundTexture.Get() == null && m_Player.SurfaceType.Get() == null)
      return;
    if (m_Player.SurfaceType.Get() != null)
    {
      PlaySound(SurfaceTypes[m_Player.SurfaceType.Get().SurfaceID]);
    }
    else
    {
      foreach (vp_SurfaceTypes surfaceType in SurfaceTypes)
      {
        foreach (UnityEngine.Object texture in surfaceType.Textures)
        {
          if (texture == m_Player.GroundTexture.Get())
          {
            PlaySound(surfaceType);
            break;
          }
        }
      }
    }
  }

  public virtual void PlaySound(vp_SurfaceTypes st)
  {
    if (st.Sounds == null || st.Sounds.Count == 0)
      return;
    do
    {
      m_SoundToPlay = st.Sounds[UnityEngine.Random.Range(0, st.Sounds.Count)];
      if (m_SoundToPlay == null)
        return;
    }
    while (m_SoundToPlay == m_LastPlayedSound && st.Sounds.Count > 1);
    m_Audio.pitch = UnityEngine.Random.Range(st.RandomPitch.x, st.RandomPitch.y) * Time.timeScale;
    m_Audio.clip = m_SoundToPlay;
    m_Audio.Play();
    m_LastPlayedSound = m_SoundToPlay;
  }

  public static int GetMainTerrainTexture(Vector3 worldPos, Terrain terrain)
  {
    TerrainData terrainData = terrain.terrainData;
    Vector3 position = terrain.transform.position;
    int x = (int) ((worldPos.x - (double) position.x) / terrainData.size.x * terrainData.alphamapWidth);
    int y = (int) ((worldPos.z - (double) position.z) / terrainData.size.z * terrainData.alphamapHeight);
    float[,,] alphamaps = terrainData.GetAlphamaps(x, y, 1, 1);
    float[] numArray = new float[alphamaps.GetUpperBound(2) + 1];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = alphamaps[0, 0, index];
    float num = 0.0f;
    int mainTerrainTexture = 0;
    for (int index = 0; index < numArray.Length; ++index)
    {
      if (numArray[index] > (double) num)
      {
        mainTerrainTexture = index;
        num = numArray[index];
      }
    }
    return mainTerrainTexture;
  }

  [Serializable]
  public class vp_SurfaceTypes
  {
    public Vector2 RandomPitch = new Vector2(1f, 1.5f);
    public bool Foldout = true;
    public bool SoundsFoldout = true;
    public bool TexturesFoldout = true;
    public string SurfaceName = "";
    public List<AudioClip> Sounds = new List<AudioClip>();
    public List<Texture> Textures = new List<Texture>();
  }
}
