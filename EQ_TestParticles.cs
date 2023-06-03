// Decompiled with JetBrains decompiler
// Type: EQ_TestParticles
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class EQ_TestParticles : MonoBehaviour
{
  public Transform[] m_CategoryList;
  private int m_CurrentCategoryIndex;
  private int m_CurrentCategoryIndexOld = -1;
  private int m_CurrentCategoryChildCount;
  private int m_CurrentParticleIndex;
  private int m_CurrentParticleIndexOld = -1;
  private ParticleSystem m_CurrentParticle;
  private string m_CurrentCategoryName = "";
  private string m_CurrentParticleName = "";

  private void Start()
  {
    if (m_CategoryList.Length == 0)
      return;
    m_CurrentCategoryIndex = 0;
    m_CurrentCategoryIndexOld = -1;
    m_CurrentParticleIndex = 0;
    m_CurrentParticleIndexOld = -1;
    ShowParticle();
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.UpArrow))
    {
      m_CurrentCategoryIndexOld = m_CurrentCategoryIndex;
      ++m_CurrentCategoryIndex;
      m_CurrentParticleIndex = 0;
      ShowParticle();
    }
    else if (Input.GetKeyUp(KeyCode.DownArrow))
    {
      m_CurrentCategoryIndexOld = m_CurrentCategoryIndex;
      --m_CurrentCategoryIndex;
      m_CurrentParticleIndex = 0;
      ShowParticle();
    }
    else if (Input.GetKeyUp(KeyCode.LeftArrow))
    {
      m_CurrentParticleIndexOld = m_CurrentParticleIndex;
      --m_CurrentParticleIndex;
      ShowParticle();
    }
    else
    {
      if (!Input.GetKeyUp(KeyCode.RightArrow))
        return;
      m_CurrentParticleIndexOld = m_CurrentParticleIndex;
      ++m_CurrentParticleIndex;
      ShowParticle();
    }
  }

  private void OnGUI()
  {
    GUI.Window(1, new Rect(Screen.width - 260, 5f, 250f, 105f), AppNameWindow, "FX Quest 0.3.0");
    GUI.Window(2, new Rect(Screen.width - 300, Screen.height - 150, 290f, 60f), SceneWindow, "Scenes");
    GUI.Window(3, new Rect(Screen.width - 410, Screen.height - 85, 400f, 80f), ParticleInformationWindow, "Information");
  }

  private void ShowParticle()
  {
    if (m_CurrentCategoryIndex >= m_CategoryList.Length)
      m_CurrentCategoryIndex = 0;
    else if (m_CurrentCategoryIndex < 0)
      m_CurrentCategoryIndex = m_CategoryList.Length - 1;
    if (m_CurrentCategoryIndex != m_CurrentCategoryIndexOld)
    {
      if (m_CurrentCategoryIndexOld >= 0)
      {
        int num = 0;
        foreach (Component component in m_CategoryList[m_CurrentCategoryIndexOld])
        {
          m_CurrentParticle = component.gameObject.GetComponent<ParticleSystem>();
          if (m_CurrentParticle != null)
          {
            m_CurrentParticle.Stop();
            m_CurrentParticle.gameObject.SetActive(false);
          }
          ++num;
        }
      }
      if (m_CurrentCategoryIndex >= 0)
      {
        int num = 0;
        foreach (Component component in m_CategoryList[m_CurrentCategoryIndex])
        {
          m_CurrentParticle = component.gameObject.GetComponent<ParticleSystem>();
          if (m_CurrentParticle != null)
          {
            m_CurrentParticle.Stop();
            m_CurrentParticle.gameObject.SetActive(false);
          }
          ++num;
        }
      }
      if (m_CurrentCategoryIndexOld >= 0)
        m_CategoryList[m_CurrentCategoryIndexOld].gameObject.SetActive(false);
      if (m_CurrentCategoryIndex >= 0)
        m_CategoryList[m_CurrentCategoryIndex].gameObject.SetActive(true);
      m_CurrentCategoryName = m_CategoryList[m_CurrentCategoryIndex].name;
      m_CurrentCategoryChildCount = m_CategoryList[m_CurrentCategoryIndex].childCount;
    }
    if (m_CurrentParticleIndex >= m_CurrentCategoryChildCount)
      m_CurrentParticleIndex = 0;
    else if (m_CurrentParticleIndex < 0)
      m_CurrentParticleIndex = m_CurrentCategoryChildCount - 1;
    if (m_CurrentParticleIndex == m_CurrentParticleIndexOld && m_CurrentCategoryIndex == m_CurrentCategoryIndexOld)
      return;
    if (m_CurrentParticle != null)
    {
      m_CurrentParticle.Stop();
      m_CurrentParticle.gameObject.SetActive(false);
    }
    int num1 = 0;
    foreach (Transform transform in m_CategoryList[m_CurrentCategoryIndex])
    {
      if (num1 == m_CurrentParticleIndex)
      {
        m_CurrentParticle = transform.gameObject.GetComponent<ParticleSystem>();
        if (!(m_CurrentParticle != null))
          break;
        m_CurrentParticle.gameObject.SetActive(true);
        m_CurrentParticle.Play();
        m_CurrentParticleName = m_CurrentParticle.name;
        break;
      }
      ++num1;
    }
  }

  private void AppNameWindow(int id)
  {
    if (GUI.Button(new Rect(15f, 25f, 220f, 20f), "www.ge-team.com"))
      Application.OpenURL("http://ge-team.com/pages/unity-3d/");
    if (GUI.Button(new Rect(15f, 50f, 220f, 20f), "geteamdev@gmail.com"))
      Application.OpenURL("mailto:geteamdev@gmail.com");
    if (!GUI.Button(new Rect(15f, 75f, 220f, 20f), "Tutorial"))
      return;
    Application.OpenURL("http://youtu.be/TWpKPCGYEyI");
  }

  private void SceneWindow(int id)
  {
    if (m_CurrentParticleIndex < 0)
      return;
    GUILayout.BeginHorizontal();
    GUI.enabled = !(SceneManager.GetActiveScene().name == "2D_Demo");
    if (GUI.Button(new Rect(12f, 25f, 125f, 25f), "2D Demo scene"))
      SceneManager.LoadScene("2D_Demo");
    GUI.enabled = !(SceneManager.GetActiveScene().name == "3D_Demo");
    if (GUI.Button(new Rect(155f, 25f, 125f, 25f), "3D Demo scene"))
      SceneManager.LoadScene("3D_Demo");
    GUILayout.EndHorizontal();
  }

  private void ParticleInformationWindow(int id)
  {
    if (m_CurrentParticleIndex < 0)
      return;
    GUI.Label(new Rect(12f, 25f, 400f, 20f), "Up/Down: Change Type (" + (m_CurrentCategoryIndex + 1) + " of " + m_CategoryList.Length + " " + m_CurrentCategoryName + ")");
    GUI.Label(new Rect(12f, 50f, 400f, 20f), "Left/Right: Change Particle (" + (m_CurrentParticleIndex + 1) + " of " + m_CurrentCategoryChildCount + " " + m_CurrentParticleName + ")");
  }
}
