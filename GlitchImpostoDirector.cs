// Decompiled with JetBrains decompiler
// Type: GlitchImpostoDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using MonomiPark.SlimeRancher.DataModel;
using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (Region))]
public class GlitchImpostoDirector : SRBehaviour, GlitchImpostoDirectorModel.Participant
{
  [Tooltip("Random range of number of impostos to enable this cell is unhibernated.")]
  public Vector2 availableCount;
  private GlitchImpostoDirectorModel model;
  private GlitchMetadata metadata;
  private TimeDirector timeDirector;
  private Region region;
  private List<GlitchImposto> registered = new List<GlitchImposto>();

  public string id => name;

  public void Awake()
  {
    metadata = SRSingleton<SceneContext>.Instance.MetadataDirector.Glitch;
    timeDirector = SRSingleton<SceneContext>.Instance.TimeDirector;
    SRSingleton<SceneContext>.Instance.GameModel.Glitch.Register(this);
  }

  public void Start()
  {
    region = GetComponent<Region>();
    region.onHibernationStateChanged += OnHibernationStateChanged;
  }

  public void InitModel(GlitchImpostoDirectorModel model) => model.hibernationTime = new double?();

  public void SetModel(GlitchImpostoDirectorModel model) => this.model = model;

  public void OnDestroy()
  {
    if (SRSingleton<SceneContext>.Instance != null)
      SRSingleton<SceneContext>.Instance.GameModel.Glitch.Unregister(this);
    if (!(region != null))
      return;
    region.onHibernationStateChanged -= OnHibernationStateChanged;
    region = null;
  }

  public void Register(GlitchImposto imposto) => registered.Add(imposto);

  public bool Deregister(GlitchImposto imposto) => registered.RemoveAll(d => d == imposto) >= 1;

  public void ResetImpostos()
  {
    registered.ForEach(imposto => imposto.Deactivate());
    foreach (GlitchImposto glitchImposto in Randoms.SHARED.Pick(registered.Where(imposto => imposto.IsReady()).ToList(), Mathf.FloorToInt(availableCount.GetRandom()), imposto => imposto.weight))
      glitchImposto.Activate();
  }

  private void OnHibernationStateChanged(bool hibernating)
  {
    if (hibernating)
    {
      if (model.hibernationTime.HasValue)
        return;
      model.hibernationTime = new double?(timeDirector.WorldTime());
    }
    else
    {
      if (model.hibernationTime.HasValue && timeDirector.TimeSince(model.hibernationTime.Value) < metadata.impostoMinHibernationTime * 3600.0)
        return;
      ResetImpostos();
    }
  }
}
