// Decompiled with JetBrains decompiler
// Type: DemoGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DemoGUI : MonoBehaviour
{
  public Texture HUETexture;
  public Material mat;
  public Position[] Positions;
  public GameObject[] Prefabs;
  private int currentNomber;
  private GameObject currentInstance;
  private GUIStyle guiStyleHeader = new GUIStyle();
  private float colorHUE;
  private float dpiScale;

  private void Start()
  {
    if (Screen.dpi < 1.0)
      dpiScale = 1f;
    dpiScale = Screen.dpi >= 200.0 ? Screen.dpi / 200f : 1f;
    guiStyleHeader.fontSize = (int) (15.0 * dpiScale);
    guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
    currentInstance = Instantiate(Prefabs[currentNomber], transform.position, new Quaternion());
  }

  private void OnGUI()
  {
    if (GUI.Button(new Rect(10f * dpiScale, 15f * dpiScale, 105f * dpiScale, 30f * dpiScale), "Previous Effect"))
      ChangeCurrent(-1);
    if (GUI.Button(new Rect(130f * dpiScale, 15f * dpiScale, 105f * dpiScale, 30f * dpiScale), "Next Effect"))
      ChangeCurrent(1);
    GUI.Label(new Rect(300f * dpiScale, 15f * dpiScale, 100f * dpiScale, 20f * dpiScale), "Prefab name is \"" + Prefabs[currentNomber].name + "\"  \r\nHold any mouse button that would move the camera", guiStyleHeader);
    GUI.DrawTexture(new Rect(12f * dpiScale, 80f * dpiScale, 220f * dpiScale, 15f * dpiScale), HUETexture, ScaleMode.StretchToFill, false, 0.0f);
    double colorHue1 = colorHUE;
    colorHUE = GUI.HorizontalSlider(new Rect(12f * dpiScale, 105f * dpiScale, 220f * dpiScale, 15f * dpiScale), colorHUE, 0.0f, 1530f);
    double colorHue2 = colorHUE;
    if (Mathf.Abs((float) (colorHue1 - colorHue2)) > 0.001)
      ChangeColor();
    GUI.Label(new Rect(240f * dpiScale, 105f * dpiScale, 30f * dpiScale, 30f * dpiScale), "Effect color", guiStyleHeader);
  }

  private void ChangeColor()
  {
    Color color1 = Hue(colorHUE / byte.MaxValue);
    foreach (Renderer componentsInChild in currentInstance.GetComponentsInChildren<Renderer>())
    {
      Material material = componentsInChild.material;
      if (!(material == null) && material.HasProperty("_TintColor"))
      {
        Color color2 = material.GetColor("_TintColor");
        color1.a = color2.a;
        material.SetColor("_TintColor", color1);
      }
    }
    Light componentInChildren = currentInstance.GetComponentInChildren<Light>();
    if (!(componentInChildren != null))
      return;
    componentInChildren.color = color1;
  }

  private Color Hue(float H)
  {
    Color color = new Color(1f, 0.0f, 0.0f);
    if (H >= 0.0 && H < 1.0)
      color = new Color(1f, 0.0f, H);
    if (H >= 1.0 && H < 2.0)
      color = new Color(2f - H, 0.0f, 1f);
    if (H >= 2.0 && H < 3.0)
      color = new Color(0.0f, H - 2f, 1f);
    if (H >= 3.0 && H < 4.0)
      color = new Color(0.0f, 1f, 4f - H);
    if (H >= 4.0 && H < 5.0)
      color = new Color(H - 4f, 1f, 0.0f);
    if (H >= 5.0 && H < 6.0)
      color = new Color(1f, 6f - H, 0.0f);
    return color;
  }

  private void ChangeCurrent(int delta)
  {
    currentNomber += delta;
    if (currentNomber > Prefabs.Length - 1)
      currentNomber = 0;
    else if (currentNomber < 0)
      currentNomber = Prefabs.Length - 1;
    if (currentInstance != null)
      Destroyer.Destroy(currentInstance, "DemoGUI.ChangeCurrent");
    Vector3 position = transform.position;
    if (Positions[currentNomber] == Position.Bottom)
      --position.y;
    if (Positions[currentNomber] == Position.Bottom02)
      position.y -= 0.8f;
    currentInstance = Instantiate(Prefabs[currentNomber], position, new Quaternion());
  }
}
