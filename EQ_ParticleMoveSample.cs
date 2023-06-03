// Decompiled with JetBrains decompiler
// Type: EQ_ParticleMoveSample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EQ_ParticleMoveSample : MonoBehaviour
{
  public eMoveMethod m_MoveMethod;
  private eMoveMethod m_MoveMethodOld;
  public float m_Range = 5f;
  private float m_RangeOld = 5f;
  public float m_Speed = 2f;
  private bool m_ToggleDirectionFlag;

  private void Start()
  {
  }

  private void Update()
  {
    if (m_MoveMethod != m_MoveMethodOld || m_Range != (double) m_RangeOld)
    {
      m_MoveMethodOld = m_MoveMethod;
      ResetPosition();
    }
    switch (m_MoveMethod)
    {
      case eMoveMethod.LeftRight:
        UpdateLeftRight();
        break;
      case eMoveMethod.UpDown:
        UpdateUpDown();
        break;
      case eMoveMethod.CircularXY_Clockwise:
        UpdateCircularXY_Clockwise();
        break;
      case eMoveMethod.CircularXY_CounterClockwise:
        UpdateCircularXY_CounterClockwise();
        break;
      case eMoveMethod.CircularXZ_Clockwise:
        UpdateCircularXZ_Clockwise();
        break;
      case eMoveMethod.CircularXZ_CounterClockwise:
        UpdateCircularXZ_CounterClockwise();
        break;
      case eMoveMethod.CircularYZ_Clockwise:
        UpdateCircularYZ_Clockwise();
        break;
      case eMoveMethod.CircularYZ_CounterClockwise:
        UpdateCircularYZ_CounterClockwise();
        break;
    }
  }

  private void ResetPosition()
  {
    switch (m_MoveMethod)
    {
      case eMoveMethod.LeftRight:
      case eMoveMethod.UpDown:
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        break;
      case eMoveMethod.CircularXY_Clockwise:
      case eMoveMethod.CircularXY_CounterClockwise:
      case eMoveMethod.CircularXZ_Clockwise:
      case eMoveMethod.CircularXZ_CounterClockwise:
        transform.localPosition = new Vector3(m_Range, 0.0f, 0.0f);
        break;
      case eMoveMethod.CircularYZ_Clockwise:
      case eMoveMethod.CircularYZ_CounterClockwise:
        transform.localPosition = new Vector3(0.0f, 0.0f, m_Range);
        break;
    }
    m_RangeOld = m_Range;
  }

  private void UpdateLeftRight()
  {
    if (!m_ToggleDirectionFlag)
    {
      transform.localPosition = new Vector3(transform.localPosition.x - m_Speed * Time.deltaTime, 0.0f, 0.0f);
      if (transform.localPosition.x > -(double) m_Range)
        return;
      m_ToggleDirectionFlag = true;
    }
    else
    {
      transform.localPosition = new Vector3(transform.localPosition.x + m_Speed * Time.deltaTime, 0.0f, 0.0f);
      if (transform.localPosition.x < (double) m_Range)
        return;
      m_ToggleDirectionFlag = false;
    }
  }

  private void UpdateUpDown()
  {
    if (!m_ToggleDirectionFlag)
    {
      transform.localPosition = new Vector3(0.0f, transform.localPosition.y + m_Speed * Time.deltaTime, 0.0f);
      if (transform.localPosition.y < (double) m_Range)
        return;
      m_ToggleDirectionFlag = true;
    }
    else
    {
      transform.localPosition = new Vector3(0.0f, transform.localPosition.y - m_Speed * Time.deltaTime, 0.0f);
      if (transform.localPosition.y > -(double) m_Range)
        return;
      m_ToggleDirectionFlag = false;
    }
  }

  private void UpdateCircularXY_Clockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float x = transform.localPosition.x;
    float y = transform.localPosition.y;
    transform.localPosition = new Vector3(num1 + (float) ((x - (double) num1) * Mathf.Cos(m_Speed / 360f) - (y - (double) num2) * Mathf.Sin(m_Speed / 360f)), num2 + (float) ((x - (double) num1) * Mathf.Sin(m_Speed / 360f) + (y - (double) num2) * Mathf.Cos(m_Speed / 360f)), 0.0f);
  }

  private void UpdateCircularXY_CounterClockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float x = transform.localPosition.x;
    float y = transform.localPosition.y;
    transform.localPosition = new Vector3(num1 + (float) ((x - (double) num1) * Mathf.Cos((float) (-(double) m_Speed / 360.0)) - (y - (double) num2) * Mathf.Sin((float) (-(double) m_Speed / 360.0))), num2 + (float) ((x - (double) num1) * Mathf.Sin((float) (-(double) m_Speed / 360.0)) + (y - (double) num2) * Mathf.Cos((float) (-(double) m_Speed / 360.0))), 0.0f);
  }

  private void UpdateCircularXZ_Clockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float x = transform.localPosition.x;
    float z = transform.localPosition.z;
    transform.localPosition = new Vector3(num1 + (float) ((x - (double) num1) * Mathf.Cos(m_Speed / 360f) - (z - (double) num2) * Mathf.Sin(m_Speed / 360f)), 0.0f, num2 + (float) ((x - (double) num1) * Mathf.Sin(m_Speed / 360f) + (z - (double) num2) * Mathf.Cos(m_Speed / 360f)));
  }

  private void UpdateCircularXZ_CounterClockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float x = transform.localPosition.x;
    float z = transform.localPosition.z;
    transform.localPosition = new Vector3(num1 + (float) ((x - (double) num1) * Mathf.Cos((float) (-(double) m_Speed / 360.0)) - (z - (double) num2) * Mathf.Sin((float) (-(double) m_Speed / 360.0))), 0.0f, num2 + (float) ((x - (double) num1) * Mathf.Sin((float) (-(double) m_Speed / 360.0)) + (z - (double) num2) * Mathf.Cos((float) (-(double) m_Speed / 360.0))));
  }

  private void UpdateCircularYZ_Clockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float y = transform.localPosition.y;
    float z = transform.localPosition.z;
    transform.localPosition = new Vector3(0.0f, num1 + (float) ((y - (double) num1) * Mathf.Cos(m_Speed / 360f) - (z - (double) num2) * Mathf.Sin(m_Speed / 360f)), num2 + (float) ((y - (double) num1) * Mathf.Sin(m_Speed / 360f) + (z - (double) num2) * Mathf.Cos(m_Speed / 360f)));
  }

  private void UpdateCircularYZ_CounterClockwise()
  {
    float num1 = 0.0f;
    float num2 = 0.0f;
    float y = transform.localPosition.y;
    float z = transform.localPosition.z;
    transform.localPosition = new Vector3(0.0f, num1 + (float) ((y - (double) num1) * Mathf.Cos((float) (-(double) m_Speed / 360.0)) - (z - (double) num2) * Mathf.Sin((float) (-(double) m_Speed / 360.0))), num2 + (float) ((y - (double) num1) * Mathf.Sin((float) (-(double) m_Speed / 360.0)) + (z - (double) num2) * Mathf.Cos((float) (-(double) m_Speed / 360.0))));
  }
}
