// Decompiled with JetBrains decompiler
// Type: FashionPod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class FashionPod : SRBehaviour
{
  public Transform fashionItemPos;
  public Identifiable.Id fashionId;
  public GameObject spawnFX;
  private GameObject fashionPrefab;
  private Joint fashionJoint;
  private Region region;
  private const float CLEAR_RAD = 0.4f;

  public void Awake()
  {
    fashionPrefab = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(fashionId);
    region = GetComponentInParent<Region>();
  }

  public void Update()
  {
    if (fashionJoint != null && fashionJoint.connectedBody == null)
    {
      Destroyer.Destroy(fashionJoint, "FashionPod.Update");
      fashionJoint = null;
    }
    if (!(fashionJoint == null) || Physics.CheckSphere(fashionItemPos.position, 0.4f))
      return;
    GameObject toAttach = InstantiateActor(fashionPrefab, region.setId, fashionItemPos.position, fashionItemPos.rotation);
    ConfigurableJoint configurableJoint1 = fashionItemPos.gameObject.AddComponent<ConfigurableJoint>();
    ConfigurableJoint configurableJoint2 = configurableJoint1;
    SafeJointReference.AttachSafely(toAttach, configurableJoint2);
    configurableJoint1.anchor = Vector3.zero;
    configurableJoint1.autoConfigureConnectedAnchor = false;
    configurableJoint1.connectedAnchor = Vector3.zero;
    SoftJointLimitSpring jointLimitSpring = new SoftJointLimitSpring();
    jointLimitSpring.damper = 0.2f;
    jointLimitSpring.spring = 1000f;
    configurableJoint1.xMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.yMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.zMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.angularXMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.angularYMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.angularZMotion = ConfigurableJointMotion.Limited;
    configurableJoint1.linearLimitSpring = jointLimitSpring;
    configurableJoint1.angularXLimitSpring = jointLimitSpring;
    configurableJoint1.angularYZLimitSpring = jointLimitSpring;
    configurableJoint1.breakForce = 20f;
    fashionJoint = configurableJoint1;
    fashionItemPos.transform.localRotation = Quaternion.Euler(Vector3.zero);
    if (!(spawnFX != null))
      return;
    SpawnAndPlayFX(spawnFX, fashionItemPos.position, fashionItemPos.rotation);
  }

  public void FixedUpdate()
  {
    if (!(fashionJoint != null))
      return;
    fashionItemPos.transform.Rotate(Vector3.up, 90f * Time.fixedDeltaTime);
  }
}
