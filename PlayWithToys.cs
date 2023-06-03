// Decompiled with JetBrains decompiler
// Type: PlayWithToys
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayWithToys : FindConsumable
{
  [Tooltip("We use the SlimeDefinition to find the slime's favorite toys.")]
  public SlimeDefinition slimeDefinition;
  [Tooltip("Should we only play with toys that are floating")]
  public bool onlyFloatingToys;
  private GameObject target;
  private float currDrive;
  private float nextPlayTime;
  private Dictionary<Identifiable.Id, DriveCalculator> toysDict;
  private const float POUNCE_DIST = 8f;
  private const float POUNCE_DIST_SQR = 64f;
  private const float PLAY_RESET_TIME = 5f;
  private bool neverPlayWithToys;
  private static List<GameObjectActorModelIdentifiableIndex.Entry> localStaticToyEntries = new List<GameObjectActorModelIdentifiableIndex.Entry>();

  public override void Awake()
  {
    base.Awake();
    toysDict = new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer);
    DriveCalculator driveCalculator = new ToyDriveCalculator(slimeDefinition.FavoriteToys);
    foreach (Identifiable.Id key in Identifiable.TOY_CLASS)
      toysDict[key] = driveCalculator;
    DestroyOnTouching component = GetComponent<DestroyOnTouching>();
    if (!(component != null) || component.touchingWaterOkay || component.touchingAshOkay)
      return;
    neverPlayWithToys = true;
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds() => toysDict;

  public override float Relevancy(bool isGrounded)
  {
    if (Time.time < (double) nextPlayTime || neverPlayWithToys)
      return 0.0f;
    localStaticToyEntries.Clear();
    CellDirector.GetToysNearMember(member, localStaticToyEntries);
    target = FindNearestConsumable(localStaticToyEntries, out currDrive);
    if (target == null)
      return 0.0f;
    currDrive = Randoms.SHARED.GetFloat(0.8f);
    return !(target == null) && (!onlyFloatingToys || DragFloatReactor.IsFloating(target)) ? currDrive * currDrive : 0.0f;
  }

  public override void Action()
  {
    if (target == null || onlyFloatingToys && !DragFloatReactor.IsFloating(target) || !IsGrounded())
      return;
    Vector3 vector3 = GetGotoPos(target) - transform.position;
    double sqrMagnitude = vector3.sqrMagnitude;
    Vector3 normalized = vector3.normalized;
    RotateTowards(normalized);
    float num1 = 1.2f;
    float num2 = Mathf.Sqrt(Mathf.Sqrt((float) sqrMagnitude) * Physics.gravity.magnitude) * num1;
    GetComponent<Rigidbody>().AddForce((normalized + Vector3.up).normalized * num2, ForceMode.VelocityChange);
    slimeAudio.Play(slimeAudio.slimeSounds.jumpCue);
    slimeAudio.Play(slimeAudio.slimeSounds.voiceJumpCue);
    target = null;
    nextPlayTime = Time.fixedTime + 5f;
  }

  public override void Selected()
  {
  }

  private class ToyDriveCalculator : DriveCalculator
  {
    private Identifiable.Id[] favoriteToys;

    public ToyDriveCalculator(Identifiable.Id[] favoriteToys)
      : base(SlimeEmotions.Emotion.NONE, 0.0f, 0.0f)
    {
      this.favoriteToys = favoriteToys;
    }

    public override float Drive(SlimeEmotions emotions, Identifiable.Id id) => base.Drive(emotions, id) * (favoriteToys.Contains(id) ? 1f : 0.5f);
  }
}
