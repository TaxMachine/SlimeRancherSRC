// Decompiled with JetBrains decompiler
// Type: DroneMovement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Drone))]
[RequireComponent(typeof (Rigidbody))]
public class DroneMovement : MonoBehaviour
{
  [Tooltip("Movement: speed")]
  public float movementSpeed;
  [Tooltip("Rotation: facing speed")]
  public float rotationFacingSpeed;
  [Tooltip("Rotation: facing stability")]
  public float rotationFacingStability;
  [Tooltip("Avoidance: min/max strength of normal adjustment to collision")]
  public Vector2 avoidanceStrength;
  public const int AVOIDANCE_MASK = -537968901;
  private static readonly float SQRT_TWO = Mathf.Sqrt(2f);

  public Rigidbody rigidbody { get; private set; }

  public Drone drone { get; private set; }

  public void Awake()
  {
    rigidbody = GetComponent<Rigidbody>();
    drone = GetComponent<Drone>();
  }

  public bool MoveTowards(Vector3 position) => ApproximatelyEquals(position, rigidbody.position = Vector3.MoveTowards(rigidbody.position, position, Time.fixedDeltaTime * 0.25f * rigidbody.mass), 0.05f);

  public bool RotateTowards(Quaternion rotation) => ApproximatelyEquals(rotation, rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, rotation, Time.fixedDeltaTime * 20f * rigidbody.mass), 0.5f);

  public static bool ApproximatelyEquals(Vector3 v1, Vector3 v2, float range) => (v2 - v1).sqrMagnitude <= range * (double) range;

  public static bool ApproximatelyEquals(Quaternion q1, Quaternion q2, float range) => Quaternion.Angle(q1, q2) < (double) range;

  public void PathTowards(Vector3 position)
  {
    Vector3 forceMoveTowards = GetForceMoveTowards(position);
    Vector3 vector3 = forceMoveTowards + GetForceAvoidance();
    vector3 = vector3.normalized;
    rigidbody.AddTorque(Vector3.Cross(Quaternion.AngleAxis((float) (rigidbody.angularVelocity.magnitude * 57.295780181884766 * rotationFacingStability * 0.10000000149011612) / rotationFacingSpeed, rigidbody.angularVelocity) * transform.forward, forceMoveTowards) * rotationFacingSpeed * rotationFacingSpeed * rigidbody.mass);
    rigidbody.AddForce(vector3 * movementSpeed * Time.fixedDeltaTime * rigidbody.mass);
  }

  private Vector3 GetForceMoveTowards(Vector3 target) => (target - rigidbody.position).normalized;

  private Vector3 GetForceAvoidance()
  {
    Vector3 vector3 = Vector3.zero;
    if (!drone.noClip.enabled)
    {
      Vector3 forward = transform.forward;
      Quaternion rotation = transform.rotation;
      float maxDistance1 = Mathf.Max(0.6f, rigidbody.velocity.sqrMagnitude * 0.4f);
      float maxDistance2 = maxDistance1 * 2f;
      Vector3 position = transform.position;
      float radius = 0.5f;
      if (Physics.SphereCast(position - forward * 0.1f, radius, forward, out RaycastHit _, maxDistance1, -537968901))
      {
        Ray ray1 = new Ray(position + transform.right * 0.5f - transform.up * 0.25f, forward + transform.right * 0.1f);
        Ray ray2 = new Ray(position - transform.right * 0.5f - transform.up * 0.25f, forward - transform.right * 0.1f);
        Ray ray3 = new Ray(position + transform.up * 0.5f, forward + transform.up);
        RaycastHit hitInfo1;
        Physics.Raycast(ray1, out hitInfo1, maxDistance2, -537968901);
        RaycastHit hitInfo2;
        Physics.Raycast(ray2, out hitInfo2, maxDistance2, -537968901);
        RaycastHit raycastHit = default;
        ref RaycastHit local = ref raycastHit;
        double maxDistance3 = maxDistance2 * (double) SQRT_TWO;
        Physics.Raycast(ray3, out local, (float) maxDistance3, -537968901);
        float num1 = hitInfo2.collider == null ? float.PositiveInfinity : hitInfo2.distance;
        float num2 = hitInfo1.collider == null ? float.PositiveInfinity : hitInfo1.distance;
        float num3 = raycastHit.collider == null ? float.PositiveInfinity : raycastHit.distance;
        vector3 = num1 >= (double) maxDistance1 || num2 >= (double) maxDistance1 || num3 <= (double) maxDistance1 ? (num1 <= (double) num2 ? transform.right : -transform.right) : transform.up;
      }
    }
    return vector3.normalized;
  }
}
