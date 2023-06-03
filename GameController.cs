﻿// Decompiled with JetBrains decompiler
// Type: GameController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GameController : MonoBehaviour
{
  public GameObject m_Player;

  private void Update()
  {
    m_Player.transform.Rotate(new Vector3(0.0f, (float) (Input.GetAxis("Horizontal") * (double) Time.deltaTime * 200.0), 0.0f));
    m_Player.transform.Translate(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 4f);
  }

  private void OnGUI()
  {
    GUI.Label(new Rect(50f, 50f, 200f, 20f), "Press arrow key to move");
    Animation componentInChildren = m_Player.GetComponentInChildren<Animation>();
    componentInChildren.enabled = GUI.Toggle(new Rect(50f, 70f, 200f, 20f), componentInChildren.enabled, "Play Animation");
    DynamicBone[] components = m_Player.GetComponents<DynamicBone>();
    GUI.Label(new Rect(50f, 100f, 200f, 20f), "Choose dynamic bone:");
    components[0].enabled = components[1].enabled = GUI.Toggle(new Rect(50f, 120f, 100f, 20f), components[0].enabled, "Breasts");
    components[2].enabled = GUI.Toggle(new Rect(50f, 140f, 100f, 20f), components[2].enabled, "Tail");
  }
}
