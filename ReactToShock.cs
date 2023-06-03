// Decompiled with JetBrains decompiler
// Type: ReactToShock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using UnityEngine;

public class ReactToShock : SRBehaviour
{
  public Identifiable.Id plortId;
  public GameObject produceFX;
  [Tooltip("Duration, in game hours, to cooldown before a reaction is available again.")]
  public float cooldownHours;
  [Tooltip("FX to show while the cooldown is active. (optional)")]
  public GameObject cooldownFX;
  [Tooltip("Prefab to instantiate an electric field.")]
  public GameObject electricFieldPrefab;
  private GameObject electricField;
  private DamagePlayerOnTouch damagePlayer;
  [Tooltip("SFX played when the slime is hit and successfully produces a plort.")]
  public SECTR_AudioCue onHitSuccessCue;
  [Tooltip("SFX played when the slime is hit and fails to produce a plort.")]
  public SECTR_AudioCue onHitFailureCue;
  private double nextReactionTime;
  private GameObject cooldownFXInstance;
  private bool checkAppearance;
  private RegionMember regionMember;
  private SlimeAppearanceApplicator slimeAppearanceApplicator;
  private GameObject plortObj;
  private TimeDirector timeDirector;
  private SlimeAppearance normalAppearance;
  private SlimeAppearance shockedAppearance;
  private static Vector3 LOCAL_PRODUCE_LOC = new Vector3(0.0f, 0.5f, 0.0f);

  public void Awake()
  {
    damagePlayer = GetComponent<DamagePlayerOnTouch>();
    regionMember = GetComponent<RegionMember>();
    slimeAppearanceApplicator = GetComponent<SlimeAppearanceApplicator>();
    slimeAppearanceApplicator.OnAppearanceChanged += UpdateAppearances;
    if (slimeAppearanceApplicator.Appearance != null)
      UpdateAppearances(slimeAppearanceApplicator.Appearance);
    plortObj = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(plortId);
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
  }

  public void Update()
  {
    if (timeDirector.HasReached(nextReactionTime))
    {
      if (cooldownFXInstance != null)
      {
        RecycleAndStopFX(cooldownFXInstance);
        cooldownFXInstance = null;
      }
      damagePlayer.SetBlocked(true);
    }
    if (!checkAppearance || !timeDirector.HasReached(nextReactionTime))
      return;
    slimeAppearanceApplicator.Appearance = normalAppearance;
    slimeAppearanceApplicator.ApplyAppearance();
    checkAppearance = false;
  }

  public void DoShock(Identifiable.Id id)
  {
    switch (id)
    {
      case Identifiable.Id.VALLEY_AMMO_1:
        MaybeCreatePlorts(1);
        break;
      case Identifiable.Id.VALLEY_AMMO_2:
        MaybeCreatePlorts(2, PlortSounds.SUCCESS);
        break;
      case Identifiable.Id.VALLEY_AMMO_4:
        MaybeCreatePlorts(1, PlortSounds.SUCCESS);
        if (electricField == null)
        {
          electricField = Instantiate(electricFieldPrefab);
          electricField.transform.SetParent(transform, false);
        }
        electricField.GetComponent<QuicksilverElectricField>().ResetDeathTime();
        break;
    }
  }

  public bool MaybeCreatePlorts(int count)
  {
    PlortSounds mask = PlortSounds.SUCCESS | PlortSounds.FAILURE;
    return MaybeCreatePlorts(count, mask);
  }

  public bool MaybeCreatePlorts(int count, PlortSounds mask)
  {
    if (timeDirector.HasReached(nextReactionTime))
    {
      for (int index = 0; index < count; ++index)
      {
        Vector3 position = transform.TransformPoint(LOCAL_PRODUCE_LOC);
        InstantiateActor(plortObj, regionMember.setId, position, transform.rotation);
        if (produceFX != null)
        {
          RecolorSlimeMaterial[] componentsInChildren = SpawnAndPlayFX(produceFX, position, transform.rotation).GetComponentsInChildren<RecolorSlimeMaterial>();
          if (componentsInChildren != null && componentsInChildren.Length != 0)
          {
            SlimeAppearance.Palette appearancePalette = slimeAppearanceApplicator.GetAppearancePalette();
            foreach (RecolorSlimeMaterial recolorSlimeMaterial in componentsInChildren)
              recolorSlimeMaterial.SetColors(appearancePalette.Top, appearancePalette.Middle, appearancePalette.Bottom);
          }
        }
      }
      if (cooldownFX != null)
      {
        if (cooldownFXInstance != null)
          RecycleAndStopFX(cooldownFXInstance);
        cooldownFXInstance = SpawnAndPlayFX(cooldownFX, gameObject);
      }
      damagePlayer.SetBlocked(false);
      slimeAppearanceApplicator.Appearance = shockedAppearance;
      slimeAppearanceApplicator.ApplyAppearance();
      checkAppearance = true;
      PlaySFX(onHitSuccessCue, PlortSounds.SUCCESS, mask);
      nextReactionTime = timeDirector.HoursFromNow(cooldownHours);
      return true;
    }
    PlaySFX(onHitFailureCue, PlortSounds.FAILURE, mask);
    return false;
  }

  private void UpdateAppearances(SlimeAppearance baseAppearance)
  {
    if (baseAppearance == normalAppearance || baseAppearance == shockedAppearance)
      return;
    normalAppearance = baseAppearance;
    shockedAppearance = baseAppearance.ShockedAppearance;
    if (!checkAppearance)
      return;
    slimeAppearanceApplicator.Appearance = shockedAppearance;
    slimeAppearanceApplicator.ApplyAppearance();
  }

  private bool PlaySFX(
    SECTR_AudioCue cue,
    PlortSounds expected,
    PlortSounds mask)
  {
    if ((mask & expected) == PlortSounds.NONE)
      return false;
    SECTR_AudioSystem.Play(cue, transform.position, false);
    return true;
  }

  [Flags]
  public enum PlortSounds
  {
    NONE = 0,
    SUCCESS = 1,
    FAILURE = 2,
  }
}
