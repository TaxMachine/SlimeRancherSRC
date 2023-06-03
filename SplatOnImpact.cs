// Decompiled with JetBrains decompiler
// Type: SplatOnImpact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SplatOnImpact : CollidableActorBehaviour, Collidable
{
  public GameObject splatPrefab;
  public GameObject splatFXPrefab;
  private SlimeAppearanceApplicator appearanceApplicator;
  private SlimeAudio slimeAudio;
  private SlimeFaceAnimator slimeFaceAnimator;
  private int penWallLayer;
  private const float SPLAT_THRESHOLD = 6f;
  private const float COLLISION_AUDIO_THRESHOLD = 6f;
  private const float COLLISION_VO_THRESHOLD = 10f;
  private const float MIN_SCALE_FACTOR = 0.75f;
  private const float MAX_SCALE_FACTOR = 2.25f;
  private const float SPEED_SCALE_POW = 0.25f;
  private const float MAX_SPEED_SCALE = 2.5f;
  private static ContactPoint[] local_contactResults = new ContactPoint[10];

  public override void Awake()
  {
    base.Awake();
    slimeAudio = GetComponent<SlimeAudio>();
    slimeFaceAnimator = GetComponent<SlimeFaceAnimator>();
    appearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    penWallLayer = LayerMask.NameToLayer("Pen Walls");
  }

  public void ProcessCollisionEnter(Collision col)
  {
    if (!(col.rigidbody == null))
      return;
    float num1 = float.NegativeInfinity;
    ContactPoint? nullable = new ContactPoint?();
    int contacts = col.GetContacts(local_contactResults);
    for (int index = 0; index < contacts; ++index)
    {
      ContactPoint localContactResult = local_contactResults[index];
      float num2 = Vector3.Dot(localContactResult.normal, col.relativeVelocity);
      if (num2 > (double) num1)
      {
        num1 = num2;
        nullable = new ContactPoint?(localContactResult);
      }
    }
    if (num1 > 6.0)
    {
      bool flag = col.gameObject.layer == penWallLayer;
      GameObject gameObject = !flag ? InstantiateDynamic(splatPrefab, nullable.Value.point, Quaternion.LookRotation(nullable.Value.normal)) : SpawnAndPlayFX(splatFXPrefab, nullable.Value.point, Quaternion.LookRotation(nullable.Value.normal));
      gameObject.transform.Rotate(Vector3.forward, Randoms.SHARED.GetFloat(360f), Space.Self);
      SlimeAppearance.Palette appearancePalette = appearanceApplicator.GetAppearancePalette();
      foreach (RecolorSlimeMaterial componentsInChild in gameObject.GetComponentsInChildren<RecolorSlimeMaterial>())
        componentsInChild.SetColors(appearancePalette.Top, appearancePalette.Middle, appearancePalette.Bottom);
      if (!flag)
      {
        float num3 = Mathf.Min(2.5f, Mathf.Pow(num1 / 6f, 0.25f));
        float inRange = Randoms.SHARED.GetInRange(0.75f, 2.25f);
        FadeAndDestroySplat component = gameObject.GetComponent<FadeAndDestroySplat>();
        component.SetScale(transform.localScale.x * inRange * num3);
        component.SetColors(appearancePalette.Top, appearancePalette.Middle, appearancePalette.Bottom);
      }
      if (slimeAudio != null)
        slimeAudio.Play(slimeAudio.slimeSounds.splatCue);
    }
    if (num1 <= 10.0)
      return;
    if (slimeAudio != null)
      slimeAudio.Play(slimeAudio.slimeSounds.voiceSplatCue);
    if (!(slimeFaceAnimator != null))
      return;
    slimeFaceAnimator.SetTrigger("triggerMinorWince");
  }

  public void ProcessCollisionExit(Collision col)
  {
  }
}
