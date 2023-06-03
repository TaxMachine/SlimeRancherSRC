// Decompiled with JetBrains decompiler
// Type: SlimeUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlimeUtil
{
  private static Color[] DEFAULTS = new Color[3]
  {
    Color.grey,
    Color.grey,
    Color.grey
  };
  private static int TopColorPropertyId = Shader.PropertyToID("_TopColor");
  private static int MiddleColorPropertyId = Shader.PropertyToID("_MiddleColor");
  private static int BottomColorPropertyId = Shader.PropertyToID("_BottomColor");
  private static int ColorRampPropertyId = Shader.PropertyToID("_ColorRamp");

  public static Color[] GetColors(GameObject slimeObj, Identifiable.Id identId, bool isGordo = false)
  {
    if (Identifiable.IsTarr(identId))
      return GetColors(slimeObj, "prefab_slimeBase/slime_tarr");
    if (!isGordo && identId == Identifiable.Id.GOLD_SLIME)
      return GetColors(slimeObj, "prefab_slimeBase/slime_gold");
    string transformRootPath = isGordo ? "Vibrating/slime_gordo" : "prefab_slimeBase/slime_default";
    return GetColors(slimeObj, transformRootPath);
  }

  private static Color[] GetColors(GameObject slimeObj, string transformRootPath)
  {
    Transform transform = slimeObj.transform.Find(transformRootPath);
    if (transform == null)
    {
      Log.Warning("Could not find renderer transform, returning default colors: " + slimeObj.name);
      return DEFAULTS;
    }
    Renderer component = transform.GetComponent<Renderer>();
    if (component == null)
    {
      Log.Warning("Could not get renderer, returning default colors: " + slimeObj.name);
      return DEFAULTS;
    }
    Material sharedMaterial = component.sharedMaterials[0];
    return new Color[3]
    {
      sharedMaterial.GetColor(TopColorPropertyId),
      sharedMaterial.GetColor(MiddleColorPropertyId),
      sharedMaterial.GetColor(BottomColorPropertyId)
    };
  }

  public static Material SetTarrColors(GameObject slimeObj, Color[] colors)
  {
    Transform transform = slimeObj.transform.Find("prefab_slimeBase/slime_tarr");
    if (transform == null)
    {
      Log.Warning("Could not find renderer transform, returning default colors: " + slimeObj.name);
      return null;
    }
    Renderer component = transform.GetComponent<Renderer>();
    if (component == null)
    {
      Log.Warning("Could not get renderer, returning default colors: " + slimeObj.name);
      return null;
    }
    Material material = component.material;
    material.SetColor(TopColorPropertyId, colors[0]);
    material.SetColor(MiddleColorPropertyId, colors[0]);
    material.SetColor(BottomColorPropertyId, colors[0]);
    return material;
  }

  public static Material SetTarrSterile(GameObject slimeObj, Texture rampTex)
  {
    Transform transform1 = slimeObj.transform.Find("prefab_slimeBase/slime_tarr");
    Transform transform2 = slimeObj.transform.Find("prefab_slimeBase/slime_tarr_bite");
    Transform transform3 = slimeObj.transform.Find("prefab_slimeBase/bone_root/bone_slime/slime_default_LOD1");
    Transform transform4 = slimeObj.transform.Find("prefab_slimeBase/bone_root/bone_slime/slime_default_LOD2");
    Transform transform5 = slimeObj.transform.Find("prefab_slimeBase/bone_root/bone_slime/slime_default_LOD3");
    if (transform1 == null)
    {
      Log.Warning("Could not find renderer transform, returning default colors: " + slimeObj.name);
      return null;
    }
    Renderer component1 = transform1.GetComponent<Renderer>();
    if (component1 == null)
    {
      Log.Warning("Could not get renderer, returning default colors: " + slimeObj.name);
      return null;
    }
    Material material = component1.material;
    material.SetTexture(ColorRampPropertyId, rampTex);
    Transform[] transformArray = new Transform[4]
    {
      transform2,
      transform3,
      transform4,
      transform5
    };
    foreach (Transform transform6 in transformArray)
    {
      if (transform6 != null)
      {
        Renderer component2 = transform6.GetComponent<Renderer>();
        if (component2 != null)
          component2.material = material;
      }
    }
    return material;
  }

  public static FixedJoint AttachToMouth(GameObject slimeObj, GameObject target)
  {
    Vector3 vector = Vector3.forward * (PhysicsUtil.RadiusOfObject(slimeObj) + PhysicsUtil.RadiusOfObject(target) * 0.25f) / slimeObj.transform.localScale.z;
    Rigidbody component = target.GetComponent<Rigidbody>();
    target.transform.position = slimeObj.transform.position + slimeObj.transform.localToWorldMatrix.MultiplyVector(vector);
    Vector3 zero;
    Vector3 vector3 = zero = Vector3.zero;
    component.angularVelocity = zero;
    component.velocity = vector3;
    FixedJoint mouth = slimeObj.AddComponent<FixedJoint>();
    SafeJointReference.AttachSafely(target, mouth);
    mouth.anchor = vector;
    mouth.breakForce = 1000f;
    mouth.breakTorque = 1000f;
    return mouth;
  }
}
