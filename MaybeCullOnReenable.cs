// Decompiled with JetBrains decompiler
// Type: MaybeCullOnReenable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

public class MaybeCullOnReenable : SRBehaviour, ActorModel.Participant
{
  private SlimeEmotions emotions;
  private SlimeModel model;

  public void Awake()
  {
    emotions = GetComponent<SlimeEmotions>();
    if (enabled)
      return;
    model.disabledAtTime = new double?(SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNowOrStart(0.0f));
  }

  public void Start()
  {
    if (!enabled)
      return;
    DoCullCheck();
  }

  public void InitModel(ActorModel model)
  {
  }

  public void SetModel(ActorModel model) => this.model = (SlimeModel) model;

  public void OnDisable()
  {
    if (!(SRSingleton<SceneContext>.Instance != null) || model == null)
      return;
    model.disabledAtTime = new double?(SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNowOrStart(0.0f));
  }

  public void OnEnable() => DoCullCheck();

  private void DoCullCheck()
  {
    if (model == null)
      return;
    if (model.disabledAtTime.HasValue && !CellDirector.IsOnRanch(GetComponent<RegionMember>()))
      MaybeDestroy((float) ((SRSingleton<SceneContext>.Instance.TimeDirector.WorldTime() - model.disabledAtTime.Value) / 86400.0));
    model.disabledAtTime = new double?();
  }

  private void MaybeDestroy(float daysPassed)
  {
    float p = Mathf.Pow(DestroyProbabilityPerDay(), 1f / daysPassed);
    if (!Randoms.SHARED.GetProbability(p))
      return;
    Destroyer.DestroyActor(gameObject, "MaybeCullOnReenable.MaybeDestroy");
  }

  private float DestroyProbabilityPerDay() => (float) (0.5 + emotions.GetMax() * 0.40000000596046448);
}
