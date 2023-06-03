// Decompiled with JetBrains decompiler
// Type: OrbitCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
  public Transform TargetLookAt;
  public float Distance = 10f;
  public float DistanceMin = 5f;
  public float DistanceMax = 15f;
  private float startingDistance;
  private float desiredDistance;
  private float mouseX;
  private float mouseY;
  public float X_MouseSensitivity = 5f;
  public float Y_MouseSensitivity = 5f;
  public float MouseWheelSensitivity = 5f;
  public float Y_MinLimit = 15f;
  public float Y_MaxLimit = 70f;
  public float DistanceSmooth = 0.025f;
  private float velocityDistance;
  private Vector3 desiredPosition = Vector3.zero;
  public float X_Smooth = 0.05f;
  public float Y_Smooth = 0.1f;
  private float velX;
  private float velY;
  private float velZ;
  private Vector3 position = Vector3.zero;

  private void Start()
  {
    Distance = Vector3.Distance(TargetLookAt.transform.position, gameObject.transform.position);
    if (Distance > (double) DistanceMax)
      DistanceMax = Distance;
    startingDistance = Distance;
    Reset();
  }

  private void Update()
  {
  }

  private void LateUpdate()
  {
    if (TargetLookAt == null)
      return;
    HandlePlayerInput();
    CalculateDesiredPosition();
    UpdatePosition();
  }

  private void HandlePlayerInput()
  {
    float num = 0.01f;
    if (Input.GetMouseButton(0))
    {
      mouseX += Input.GetAxis("Mouse X") * X_MouseSensitivity;
      mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensitivity;
    }
    mouseY = ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);
    if (Input.GetAxis("Mouse ScrollWheel") >= -(double) num && Input.GetAxis("Mouse ScrollWheel") <= (double) num)
      return;
    desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);
  }

  private void CalculateDesiredPosition()
  {
    Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velocityDistance, DistanceSmooth);
    desiredPosition = CalculatePosition(mouseY, mouseX, Distance);
  }

  private Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
  {
    Vector3 vector3 = new Vector3(0.0f, 0.0f, -distance);
    return TargetLookAt.position + Quaternion.Euler(rotationX, rotationY, 0.0f) * vector3;
  }

  private void UpdatePosition()
  {
    position = new Vector3(Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth), Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth), Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth));
    transform.position = position;
    transform.LookAt(TargetLookAt);
  }

  private void Reset()
  {
    mouseX = 0.0f;
    mouseY = 0.0f;
    Distance = startingDistance;
    desiredDistance = Distance;
  }

  private float ClampAngle(float angle, float min, float max)
  {
    while (angle < -360.0 || angle > 360.0)
    {
      if (angle < -360.0)
        angle += 360f;
      if (angle > 360.0)
        angle -= 360f;
    }
    return Mathf.Clamp(angle, min, max);
  }
}
