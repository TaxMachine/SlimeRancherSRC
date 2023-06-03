// Decompiled with JetBrains decompiler
// Type: SlimenadoMoveSpiral
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimenadoMoveSpiral : MonoBehaviour
{
  private float rot;
  private float nextGroundCheck;
  private float tgtY;
  private const float SPEED = 3f;
  private const float VERT_SPEED = 5f;
  private const float INIT_ROT = 90f;
  private const float GROUND_CHECK_PERIOD = 1f;
  private const float GROUND_ADJUST_DIST = 10f;
  private const int GROUND_RAY_MASK = 268435457;

  private void Start()
  {
    rot = 90f;
    tgtY = transform.position.y;
    nextGroundCheck = Time.time + nextGroundCheck;
  }

  private void FixedUpdate()
  {
    float y = transform.position.y;
    if (tgtY > (double) y)
      transform.position = new Vector3(transform.position.x, Mathf.Min(tgtY, y + 5f * Time.fixedDeltaTime), transform.position.z);
    else if (tgtY < (double) y)
      transform.position = new Vector3(transform.position.x, Mathf.Max(tgtY, y - 5f * Time.fixedDeltaTime), transform.position.z);
    transform.Translate(transform.forward * (3f * Time.fixedDeltaTime));
    transform.Rotate(0.0f, rot * Time.fixedDeltaTime, 0.0f, Space.World);
    rot *= Mathf.Pow(0.9f, Time.fixedDeltaTime);
    if (Time.time < (double) nextGroundCheck)
      return;
    RaycastHit hitInfo;
    if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out hitInfo, 20f, 268435457))
      tgtY = hitInfo.point.y;
    else
      Destroyer.Destroy(gameObject, "SlimenadoMoveSpiral.FixedUpdate");
    nextGroundCheck = Time.time + 1f;
  }
}
