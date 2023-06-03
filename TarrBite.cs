// Decompiled with JetBrains decompiler
// Type: TarrBite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using UnityEngine;

public class TarrBite : MonoBehaviour
{
  private BodyMeshMarker[] bodyMeshes;
  private BiteMeshMarker[] biteMeshes;
  private BiteEventAggregator aggregator;
  private SlimeAppearanceApplicator appearanceApplicator;

  public void Awake()
  {
    aggregator = gameObject.GetRequiredComponentInChildren<BiteEventAggregator>();
    appearanceApplicator = gameObject.GetRequiredComponentInChildren<SlimeAppearanceApplicator>();
  }

  public void Start()
  {
    aggregator.OnEnableBite += ShowBite;
    aggregator.OnDisableBite += HideBite;
    WireBodyAndBiteComponents();
    appearanceApplicator.OnAppearanceChanged += OnAppearanceChanged;
  }

  private void OnAppearanceChanged(SlimeAppearance appearance) => WireBodyAndBiteComponents();

  private void WireBodyAndBiteComponents()
  {
    bodyMeshes = GetComponentsInChildren<BodyMeshMarker>();
    biteMeshes = GetComponentsInChildren<BiteMeshMarker>();
    if (aggregator.IsBiteAnimationStateActive())
      ShowBite();
    else
      HideBite();
  }

  private void ShowBite()
  {
    foreach (Component bodyMesh in bodyMeshes)
      bodyMesh.gameObject.SetActive(false);
    foreach (Component biteMesh in biteMeshes)
      biteMesh.gameObject.SetActive(true);
  }

  private void HideBite()
  {
    foreach (Component bodyMesh in bodyMeshes)
      bodyMesh.gameObject.SetActive(true);
    foreach (Component biteMesh in biteMeshes)
      biteMesh.gameObject.SetActive(false);
  }

  public void OnDestroy()
  {
    if (aggregator != null)
    {
      aggregator.OnEnableBite -= ShowBite;
      aggregator.OnDisableBite -= HideBite;
    }
    if (!(appearanceApplicator != null))
      return;
    appearanceApplicator.OnAppearanceChanged -= OnAppearanceChanged;
  }
}
