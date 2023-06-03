// Decompiled with JetBrains decompiler
// Type: EQ_CloudFlow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EQ_CloudFlow : MonoBehaviour
{
  [HideInInspector]
  public Cloud[] m_CloudList;
  public bool m_EnableLargeCloudLoop;
  public eCloudFlowBehavior m_Behavior = eCloudFlowBehavior.FlowTheSameWay;
  public float m_MinSpeed = 0.05f;
  public float m_MaxSpeed = 0.3f;
  public Camera m_Camera;
  private Vector3 LeftMostOfScreen;
  private Vector3 RightMostOfScreen;

  private void Start()
  {
    m_CloudList = new Cloud[this.transform.childCount];
    int num = Random.Range(0, 2);
    int index = 0;
    foreach (Transform transform in this.transform)
    {
      m_CloudList[index] = new Cloud();
      m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed, m_MaxSpeed);
      if (num == 0)
      {
        m_CloudList[index].m_MoveSpeed *= -1f;
        if (m_Behavior == eCloudFlowBehavior.SwitchLeftRight)
          num = 1;
      }
      else if (m_Behavior == eCloudFlowBehavior.SwitchLeftRight)
        num = 0;
      m_CloudList[index].m_Cloud = transform.gameObject;
      if (m_EnableLargeCloudLoop)
        m_CloudList[index].m_CloudFollower = Instantiate(transform.gameObject);
      m_CloudList[index].m_OriginalLocalPos = m_CloudList[index].m_Cloud.transform.localPosition;
      ++index;
    }
    if (m_EnableLargeCloudLoop)
    {
      foreach (Cloud cloud in m_CloudList)
        cloud.m_CloudFollower.transform.parent = transform;
    }
    FindTheOrthographicCamera();
  }

  private void Update()
  {
    if (m_Camera == null)
      FindTheOrthographicCamera();
    if (m_Camera == null)
    {
      Debug.LogWarning("There is no Orthographic camera in the scene.");
    }
    else
    {
      int index = 0;
      foreach (Cloud cloud1 in m_CloudList)
      {
        if (cloud1.m_Cloud.activeSelf)
        {
          m_CloudList[index].m_Cloud.transform.localPosition = new Vector3(m_CloudList[index].m_Cloud.transform.localPosition.x + m_CloudList[index].m_MoveSpeed * Time.deltaTime, m_CloudList[index].m_Cloud.transform.localPosition.y, m_CloudList[index].m_Cloud.transform.localPosition.z);
          Bounds bounds;
          if (m_CloudList[index].m_MoveSpeed > 0.0)
          {
            if (m_CloudList[index].m_CloudFollower != null)
            {
              Transform transform = m_CloudList[index].m_CloudFollower.transform;
              double x1 = m_CloudList[index].m_Cloud.transform.localPosition.x;
              bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
              double x2 = bounds.size.x;
              Vector3 vector3 = new Vector3((float) (x1 - x2), m_CloudList[index].m_Cloud.transform.localPosition.y, m_CloudList[index].m_Cloud.transform.localPosition.z);
              transform.localPosition = vector3;
            }
            double x3 = m_CloudList[index].m_Cloud.transform.localPosition.x;
            double x4 = RightMostOfScreen.x;
            bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
            double num1 = bounds.size.x / 2.0;
            double num2 = x4 + num1;
            if (x3 > num2)
            {
              if (m_EnableLargeCloudLoop)
              {
                GameObject cloud2 = m_CloudList[index].m_Cloud;
                m_CloudList[index].m_Cloud = m_CloudList[index].m_CloudFollower;
                m_CloudList[index].m_CloudFollower = cloud2;
              }
              else
              {
                m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed, m_MaxSpeed);
                Transform transform = m_CloudList[index].m_Cloud.transform;
                double x5 = LeftMostOfScreen.x;
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double x6 = bounds.size.x;
                double x7 = x5 - x6;
                double y = Random.Range((float) (-(double) m_Camera.orthographicSize / 2.0), m_Camera.orthographicSize / 2f);
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double z = bounds.size.z;
                Vector3 vector3 = new Vector3((float) x7, (float) y, (float) z);
                transform.localPosition = vector3;
              }
            }
          }
          else
          {
            if (m_CloudList[index].m_CloudFollower != null)
            {
              Transform transform = m_CloudList[index].m_CloudFollower.transform;
              double x8 = m_CloudList[index].m_Cloud.transform.localPosition.x;
              bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
              double x9 = bounds.size.x;
              Vector3 vector3 = new Vector3((float) (x8 + x9), m_CloudList[index].m_Cloud.transform.localPosition.y, m_CloudList[index].m_Cloud.transform.localPosition.z);
              transform.localPosition = vector3;
            }
            double x10 = m_CloudList[index].m_Cloud.transform.localPosition.x;
            double x11 = LeftMostOfScreen.x;
            bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
            double num3 = bounds.size.x / 2.0;
            double num4 = x11 - num3;
            if (x10 < num4)
            {
              if (m_EnableLargeCloudLoop)
              {
                GameObject cloud3 = m_CloudList[index].m_Cloud;
                m_CloudList[index].m_Cloud = m_CloudList[index].m_CloudFollower;
                m_CloudList[index].m_CloudFollower = cloud3;
              }
              else
              {
                m_CloudList[index].m_MoveSpeed = -Random.Range(m_MinSpeed, m_MaxSpeed);
                Transform transform = m_CloudList[index].m_Cloud.transform;
                double x12 = RightMostOfScreen.x;
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double x13 = bounds.size.x;
                double x14 = x12 + x13;
                double y1 = m_CloudList[index].m_OriginalLocalPos.y;
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double y2 = bounds.size.y;
                double min = y1 - y2;
                double y3 = m_CloudList[index].m_OriginalLocalPos.y;
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double y4 = bounds.size.y;
                double max = y3 + y4;
                double y5 = Random.Range((float) min, (float) max);
                bounds = m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds;
                double z = bounds.size.z;
                Vector3 vector3 = new Vector3((float) x14, (float) y5, (float) z);
                transform.localPosition = vector3;
              }
            }
          }
        }
        ++index;
      }
    }
  }

  private void FindTheOrthographicCamera()
  {
    if (m_Camera == null)
    {
      foreach (Camera camera in FindObjectsOfType<Camera>())
      {
        if (camera.orthographic)
        {
          m_Camera = camera;
          break;
        }
      }
    }
    if (!(m_Camera != null))
      return;
    LeftMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
    RightMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
  }
}
