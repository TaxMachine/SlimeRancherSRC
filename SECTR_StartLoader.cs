// Decompiled with JetBrains decompiler
// Type: SECTR_StartLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[RequireComponent(typeof (SECTR_Member))]
[AddComponentMenu("SECTR/Stream/SECTR Start Loader")]
public class SECTR_StartLoader : SECTR_Loader
{
  private Texture2D fadeTexture;
  private float fadeAmount = 1f;
  private SECTR_Member cachedMember;
  [SECTR_ToolTip("Set to true if the scene should start at black and fade in when loaded.")]
  public bool FadeIn;
  [SECTR_ToolTip("Amount of time to fade in.", "FadeIn")]
  public float FadeTime = 2f;
  [SECTR_ToolTip("The color to fade the screen to on load.", "FadeIn")]
  public Color FadeColor = Color.black;
  [NonSerialized]
  public bool Paused;

  public override bool Loaded
  {
    get
    {
      bool loaded = true;
      int count = (bool) (UnityEngine.Object) cachedMember ? cachedMember.Sectors.Count : 0;
      for (int index = 0; index < count; ++index)
      {
        SECTR_Sector sector = cachedMember.Sectors[index];
        if (sector.Frozen)
        {
          SECTR_Chunk component = sector.GetComponent<SECTR_Chunk>();
          if ((bool) (UnityEngine.Object) component && !component.IsLoaded())
          {
            loaded = false;
            break;
          }
        }
      }
      return loaded;
    }
  }

  private void OnEnable()
  {
    cachedMember = GetComponent<SECTR_Member>();
    if (!FadeIn)
      return;
    fadeTexture = new Texture2D(1, 1);
    fadeTexture.SetPixel(0, 0, FadeColor);
    fadeTexture.Apply();
  }

  private void OnDisable()
  {
    cachedMember = null;
    fadeTexture = null;
  }

  private void Start()
  {
    cachedMember.ForceUpdate(true);
    int count = cachedMember.Sectors.Count;
    for (int index = 0; index < count; ++index)
    {
      SECTR_Chunk component = cachedMember.Sectors[index].GetComponent<SECTR_Chunk>();
      if ((bool) (UnityEngine.Object) component)
        component.AddReference();
    }
    LockSelf(true);
  }

  private void Update()
  {
    if (!Loaded)
      return;
    if (locked)
      LockSelf(false);
    if (FadeIn)
      return;
    Destroyer.Destroy(this, "SECTR_StartLoader.Update");
  }

  private void OnGUI()
  {
    if (!FadeIn || !enabled)
      return;
    if (Loaded && !Paused)
    {
      fadeAmount -= Time.deltaTime / FadeTime;
      fadeAmount = Mathf.Clamp01(fadeAmount);
    }
    GUI.color = new Color(1f, 1f, 1f, fadeAmount);
    GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), fadeTexture);
    if (fadeAmount != 0.0)
      return;
    Destroyer.Destroy(this, "SECTR_StartLoader.OnGUI");
  }
}
