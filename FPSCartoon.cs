// Decompiled with JetBrains decompiler
// Type: FPSCartoon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FPSCartoon : MonoBehaviour
{
  private readonly GUIStyle guiStyleHeader = new GUIStyle();
  private float timeleft;
  private float fps;
  private int frames;

  private void Awake()
  {
    guiStyleHeader.fontSize = 14;
    guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
  }

  private void OnGUI() => GUI.Label(new Rect(0.0f, 0.0f, 30f, 30f), "FPS: " + (int) fps, guiStyleHeader);

  private void Update()
  {
    timeleft -= Time.deltaTime;
    ++frames;
    if (timeleft > 0.0)
      return;
    fps = frames;
    timeleft = 1f;
    frames = 0;
  }
}
