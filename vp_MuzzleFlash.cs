// Decompiled with JetBrains decompiler
// Type: vp_MuzzleFlash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_MuzzleFlash : MonoBehaviour
{
  protected float m_FadeSpeed = 0.075f;
  protected bool m_ForceShow;
  protected Color m_Color = new Color(1f, 1f, 1f, 0.0f);
  protected Transform m_Transform;
  protected Light m_Light;
  protected float m_LightIntensity;
  protected Renderer m_Renderer;
  protected Material m_Material;

  public float FadeSpeed
  {
    get => m_FadeSpeed;
    set => m_FadeSpeed = value;
  }

  public bool ForceShow
  {
    get => m_ForceShow;
    set => m_ForceShow = value;
  }

  private void Awake()
  {
    m_Transform = transform;
    m_ForceShow = false;
    m_Light = GetComponent<Light>();
    if (m_Light != null)
    {
      m_LightIntensity = m_Light.intensity;
      m_Light.intensity = 0.0f;
    }
    m_Renderer = GetComponent<Renderer>();
    if (!(m_Renderer != null))
      return;
    m_Material = GetComponent<Renderer>().material;
    if (!(m_Material != null))
      return;
    m_Color = m_Material.GetColor("_TintColor");
    m_Color.a = 0.0f;
  }

  private void Start()
  {
    GameObject gameObject = GameObject.Find("WeaponCamera");
    if (!(gameObject != null) || !(gameObject.transform.parent == m_Transform.parent))
      return;
    this.gameObject.layer = 31;
  }

  private void Update()
  {
    if (m_ForceShow)
      Show();
    else if (m_Color.a > 0.0)
    {
      m_Color.a -= m_FadeSpeed * (Time.deltaTime * 60f);
      if (m_Light != null)
        m_Light.intensity = m_LightIntensity * (m_Color.a * 2f);
    }
    if (m_Material != null)
      m_Material.SetColor("_TintColor", m_Color);
    if (m_Color.a >= 0.0099999997764825821)
      return;
    m_Renderer.enabled = false;
    if (!(m_Light != null))
      return;
    m_Light.enabled = false;
  }

  public void Show()
  {
    m_Renderer.enabled = true;
    if (m_Light != null)
    {
      m_Light.enabled = true;
      m_Light.intensity = m_LightIntensity;
    }
    m_Color.a = 0.5f;
  }

  public void Shoot() => ShootInternal(true);

  public void ShootLightOnly() => ShootInternal(false);

  public void ShootInternal(bool showMesh)
  {
    m_Color.a = 0.5f;
    if (showMesh)
    {
      m_Transform.Rotate(0.0f, 0.0f, Random.Range(0, 360));
      m_Renderer.enabled = true;
    }
    if (!(m_Light != null))
      return;
    m_Light.enabled = true;
    m_Light.intensity = m_LightIntensity;
  }

  public void SetFadeSpeed(float fadeSpeed) => FadeSpeed = fadeSpeed;
}
