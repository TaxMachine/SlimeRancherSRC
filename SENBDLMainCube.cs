// Decompiled with JetBrains decompiler
// Type: SENBDLMainCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SENBDLMainCube : MonoBehaviour
{
  private Color[] glowColors = new Color[4];
  public GameObject orbitingCube;
  public GameObject glowingOrbitingCube;
  public GameObject cubeEmissivePart;
  public GameObject particles;
  private const float newColorFrequency = 8f;
  private float newColorCounter;
  private Color currentColor;
  private Color previousColor;
  [HideInInspector]
  public Color glowColor;
  private int currentColorIndex;
  private float bloomAmount = 0.04f;
  private float lensDirtAmount = 0.3f;
  private float fps;
  private float deltaTime;
  private SENaturalBloomAndDirtyLens bloomShader;

  private void Start()
  {
    glowColors[0] = new Color(1f, 0.470588237f, 0.0509803928f);
    glowColors[2] = new Color(0.329411775f, 0.6392157f, 1f);
    glowColors[1] = new Color(0.607843161f, 1f, 0.117647059f);
    glowColors[3] = new Color(1f, 0.184313729f, 0.0f);
    currentColor = glowColors[0];
    SENBDLGlobal.sphereOfCubesRotation = Quaternion.identity;
    for (int index = 0; index < 150; ++index)
      Instantiate(orbitingCube, Vector3.zero, Quaternion.identity);
    for (int index = 0; index < 19; ++index)
      Instantiate(glowingOrbitingCube, Vector3.zero, Quaternion.identity);
    Camera.main.backgroundColor = new Color(0.08f, 0.08f, 0.08f);
    SENBDLGlobal.mainCube = this;
    bloomShader = Camera.main.GetComponent<SENaturalBloomAndDirtyLens>();
  }

  private void OnGUI()
  {
  }

  private void Update()
  {
    deltaTime = Time.deltaTime / Time.timeScale;
    AnimateColor();
    RotateSphereOfCubes();
    float num = 40f;
    Vector3 vector3 = Vector3.up * num;
    transform.Rotate(Quaternion.Euler(Vector3.right * Time.time * num * 0.5f) * vector3 * Time.deltaTime);
    IncrementCounters();
    GetInput();
    UpdateShaderValues();
    SmoothFPSCounter();
  }

  private void AnimateColor()
  {
    if (newColorCounter >= 8.0)
    {
      newColorCounter = 0.0f;
      currentColorIndex = (currentColorIndex + 1) % glowColors.Length;
      previousColor = currentColor;
      currentColor = glowColors[currentColorIndex];
    }
    glowColor = Color.Lerp(previousColor, currentColor, Mathf.Clamp01((float) (newColorCounter / 8.0 * 5.0)));
    Color color1 = glowColor * Mathf.Pow((float) (Mathf.Sin(Time.time) * 0.47999998927116394 + 0.51999998092651367), 4f);
    cubeEmissivePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", color1);
    GetComponent<Light>().color = color1;
    Color color2 = Color.Lerp(new Color()
    {
      r = 1f - glowColor.r,
      g = 1f - glowColor.g,
      b = 1f - glowColor.b
    }, Color.white, 0.1f);
    particles.GetComponent<Renderer>().material.SetColor("_TintColor", color2);
  }

  private void RotateSphereOfCubes() => SENBDLGlobal.sphereOfCubesRotation = Quaternion.Euler(Vector3.up * Time.time * 20f);

  private void IncrementCounters() => newColorCounter += Time.deltaTime;

  private void GetInput()
  {
    if (Input.GetKey(KeyCode.RightArrow))
      bloomAmount += 0.2f * deltaTime;
    if (Input.GetKey(KeyCode.LeftArrow))
      bloomAmount -= 0.2f * deltaTime;
    if (Input.GetKey(KeyCode.UpArrow))
      lensDirtAmount += 0.4f * deltaTime;
    if (Input.GetKey(KeyCode.DownArrow))
      lensDirtAmount -= 0.4f * deltaTime;
    if (Input.GetKey(KeyCode.Period))
      Time.timeScale += 0.5f * deltaTime;
    if (Input.GetKey(KeyCode.Comma))
      Time.timeScale -= 0.5f * deltaTime;
    bloomAmount = Mathf.Clamp(bloomAmount, 0.0f, 0.4f);
    lensDirtAmount = Mathf.Clamp(lensDirtAmount, 0.0f, 0.95f);
    Time.timeScale = Mathf.Clamp(Time.timeScale, 0.1f, 1f);
    if (!Input.GetKeyDown(KeyCode.Space))
      return;
    bloomAmount = 0.05f;
    lensDirtAmount = 0.1f;
    Time.timeScale = 1f;
  }

  private void UpdateShaderValues()
  {
    bloomShader.bloomIntensity = bloomAmount;
    bloomShader.lensDirtIntensity = lensDirtAmount;
  }

  private void SmoothFPSCounter() => fps = Mathf.Lerp(fps, 1f / deltaTime, 5f * deltaTime);
}
