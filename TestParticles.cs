// Decompiled with JetBrains decompiler
// Type: TestParticles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TestParticles : MonoBehaviour
{
  public GameObject[] m_PrefabListFire;
  public GameObject[] m_PrefabListWind;
  public GameObject[] m_PrefabListWater;
  public GameObject[] m_PrefabListEarth;
  public GameObject[] m_PrefabListIce;
  public GameObject[] m_PrefabListThunder;
  public GameObject[] m_PrefabListLight;
  public GameObject[] m_PrefabListDarkness;
  private int m_CurrentElementIndex = -1;
  private int m_CurrentParticleIndex = -1;
  private string m_ElementName = "";
  private string m_ParticleName = "";
  private GameObject[] m_CurrentElementList;
  private GameObject m_CurrentParticle;

  private void Start()
  {
    if (m_PrefabListFire.Length == 0 && m_PrefabListWind.Length == 0 && m_PrefabListWater.Length == 0 && m_PrefabListEarth.Length == 0 && m_PrefabListIce.Length == 0 && m_PrefabListThunder.Length == 0 && m_PrefabListLight.Length == 0 && m_PrefabListDarkness.Length == 0)
      return;
    m_CurrentElementIndex = 0;
    m_CurrentParticleIndex = 0;
    ShowParticle();
  }

  private void Update()
  {
    if (m_CurrentElementIndex == -1 || m_CurrentParticleIndex == -1)
      return;
    if (Input.GetKeyUp(KeyCode.UpArrow))
    {
      ++m_CurrentElementIndex;
      m_CurrentParticleIndex = 0;
      ShowParticle();
    }
    else if (Input.GetKeyUp(KeyCode.DownArrow))
    {
      --m_CurrentElementIndex;
      m_CurrentParticleIndex = 0;
      ShowParticle();
    }
    else if (Input.GetKeyUp(KeyCode.LeftArrow))
    {
      --m_CurrentParticleIndex;
      ShowParticle();
    }
    else
    {
      if (!Input.GetKeyUp(KeyCode.RightArrow))
        return;
      ++m_CurrentParticleIndex;
      ShowParticle();
    }
  }

  private void OnGUI()
  {
    GUI.Window(1, new Rect(Screen.width - 260, 5f, 250f, 80f), InfoWindow, "Info");
    GUI.Window(2, new Rect(Screen.width - 260, Screen.height - 85, 250f, 80f), ParticleInformationWindow, "Help");
  }

  private void ShowParticle()
  {
    if (m_CurrentElementIndex > 7)
      m_CurrentElementIndex = 0;
    else if (m_CurrentElementIndex < 0)
      m_CurrentElementIndex = 7;
    if (m_CurrentElementIndex == 0)
    {
      m_CurrentElementList = m_PrefabListFire;
      m_ElementName = "FIRE";
    }
    else if (m_CurrentElementIndex == 1)
    {
      m_CurrentElementList = m_PrefabListWater;
      m_ElementName = "WATER";
    }
    else if (m_CurrentElementIndex == 2)
    {
      m_CurrentElementList = m_PrefabListWind;
      m_ElementName = "WIND";
    }
    else if (m_CurrentElementIndex == 3)
    {
      m_CurrentElementList = m_PrefabListEarth;
      m_ElementName = "EARTH";
    }
    else if (m_CurrentElementIndex == 4)
    {
      m_CurrentElementList = m_PrefabListThunder;
      m_ElementName = "THUNDER";
    }
    else if (m_CurrentElementIndex == 5)
    {
      m_CurrentElementList = m_PrefabListIce;
      m_ElementName = "ICE";
    }
    else if (m_CurrentElementIndex == 6)
    {
      m_CurrentElementList = m_PrefabListLight;
      m_ElementName = "LIGHT";
    }
    else if (m_CurrentElementIndex == 7)
    {
      m_CurrentElementList = m_PrefabListDarkness;
      m_ElementName = "DARKNESS";
    }
    if (m_CurrentParticleIndex >= m_CurrentElementList.Length)
      m_CurrentParticleIndex = 0;
    else if (m_CurrentParticleIndex < 0)
      m_CurrentParticleIndex = m_CurrentElementList.Length - 1;
    m_ParticleName = m_CurrentElementList[m_CurrentParticleIndex].name;
    if (m_CurrentParticle != null)
      Destroyer.Destroy(m_CurrentParticle, "TestParticles.ShowParticle");
    m_CurrentParticle = Instantiate(m_CurrentElementList[m_CurrentParticleIndex]);
  }

  private void ParticleInformationWindow(int id)
  {
    GUI.Label(new Rect(12f, 25f, 280f, 20f), "Up/Down: " + m_ElementName + " (" + (m_CurrentParticleIndex + 1) + "/" + m_CurrentElementList.Length + ")");
    GUI.Label(new Rect(12f, 50f, 280f, 20f), "Left/Right: " + m_ParticleName.ToUpper());
  }

  private void InfoWindow(int id)
  {
    GUI.Label(new Rect(15f, 25f, 240f, 20f), "Elementals 1.1.1");
    GUI.Label(new Rect(15f, 50f, 240f, 20f), "www.ge-team.com/pages");
  }
}
