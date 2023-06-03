// Decompiled with JetBrains decompiler
// Type: KeepPuddleUpright
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class KeepPuddleUpright : KeepUpright, RegistryFixedUpdateable, RegistryLateUpdateable
{
  private EnableBasedOnGrounded[] toggleOnGrounded;
  private SlimeSubbehaviourPlexer plexer;
  private Transform slimeRoot;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;

  public override void Start()
  {
    base.Start();
    plexer = GetComponent<SlimeSubbehaviourPlexer>();
    toggleOnGrounded = GetComponentsInChildren<EnableBasedOnGrounded>(true);
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += appearance => toggleOnGrounded = GetComponentsInChildren<EnableBasedOnGrounded>(true);
    slimeRoot = transform.Find("prefab_slimeBase/bone_root/bone_slime");
  }

  public void RegistryLateUpdate()
  {
    if (slimeRoot == null)
      return;
    slimeRoot.localRotation = Quaternion.identity;
  }

  public override void RegistryFixedUpdate()
  {
    if (plexer == null)
      return;
    bool flag = plexer.IsGrounded();
    if (flag)
    {
      RaycastHit raycastHit = plexer.GroundHit();
      if (raycastHit.rigidbody == null)
        DoUpright(raycastHit.normal);
      else
        DoUpright(Vector3.up);
    }
    else
      DoUpright(Vector3.up);
    foreach (EnableBasedOnGrounded enableBasedOnGrounded in toggleOnGrounded)
    {
      if (enableBasedOnGrounded != null)
        enableBasedOnGrounded.gameObject.SetActive(enableBasedOnGrounded.enableOnGrounded ^ flag);
    }
  }
}
