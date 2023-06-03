// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceCarousel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlimeAppearanceCarousel : SRBehaviour
{
  public Camera cam;
  public Transform root;
  public SlimeAppearanceApplicator appearancePrefab;
  public GameObject appearanceSpritePrefab;
  public float spacing = 1.5f;
  public float transitionTime = 0.25f;
  public SlimeAppearanceDirector slimeAppearanceDirector;
  public GameObject shadowPrefab;
  public Transform spotlight;
  public GameObject selectFx;
  public float unselectedMoveBack = 1f;
  public float unselectedXScaling = 1.2f;
  public float jumpAmount = 0.25f;
  public int maxAppearancesToShow = 2;
  private const string unscaledTimeKeyword = "_UNSCALEDTIME_ON";
  private readonly System.Type[] blacklistedObjectTypes = new System.Type[2]
  {
    typeof (RadExpandMarker),
    typeof (TrailRenderer)
  };
  private List<GameObject> currentSlimeAppearancePreviews = new List<GameObject>();
  private SlimeDefinition currentSlime;
  private SlimeAppearance[] currentAppearances;
  private int unscaledTimePropertyId;

  public event OnSlimeAppearanceSelectedDelegate onSlimeAppearanceConfirmed = (_param1, _param2) => { };

  private void Awake() => unscaledTimePropertyId = Shader.PropertyToID("_UnscaledTime");

  private IEnumerator ResetExpressionAfterTime(SlimeAppearanceApplicator appearanceApplicator)
  {
    yield return new WaitForSecondsRealtime(0.5f);
    if (appearanceApplicator != null && appearanceApplicator.gameObject != null && appearanceApplicator.gameObject.activeInHierarchy)
      appearanceApplicator.SetExpression(SlimeFace.SlimeExpression.Happy);
  }

  public void ShowSlime(SlimeDefinition slime)
  {
    currentSlime = slime;
    foreach (Object appearancePreview in currentSlimeAppearancePreviews)
      Destroy(appearancePreview);
    currentSlimeAppearancePreviews.Clear();
    currentAppearances = slime.Appearances.ToArray();
    SlimeAppearance chosenSlimeAppearance = slimeAppearanceDirector.GetChosenSlimeAppearance(slime);
    for (int index = 0; index < Mathf.Min(maxAppearancesToShow, currentAppearances.Length); ++index)
    {
      SlimeAppearance currentAppearance = currentAppearances[index];
      GameObject appearancePreview = CreateAppearancePreview(slime, currentAppearance, currentAppearance == chosenSlimeAppearance);
      appearancePreview.transform.localPosition = new Vector3((float) (index * (double) spacing - spacing / 2.0), 0.0f, 0.0f);
      appearancePreview.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
      if (currentAppearance != chosenSlimeAppearance)
        appearancePreview.transform.localPosition = GetUnfocusedPosition(appearancePreview.transform.position);
      else
        spotlight.localPosition = new Vector3((float) (index * (double) spacing - spacing / 2.0), spotlight.localPosition.y, spotlight.localPosition.z);
    }
  }

  private Vector3 GetUnfocusedPosition(Vector3 focusedPosition) => new Vector3(focusedPosition.x * unselectedXScaling, 0.0f, unselectedMoveBack);

  public void ConfirmSlimeAppearance(int index)
  {
    if (slimeAppearanceDirector.GetChosenSlimeAppearance(currentSlime) == currentAppearances[index])
      return;
    onSlimeAppearanceConfirmed(currentSlime, currentAppearances[index]);
    GameObject appearancePreview1 = currentSlimeAppearancePreviews[index];
    SpriteRenderer componentInChildren1 = appearancePreview1.GetComponentInChildren<SpriteRenderer>();
    SlimeAppearanceApplicator component1 = appearancePreview1.GetComponent<SlimeAppearanceApplicator>();
    if (component1 != null)
    {
      component1.SetExpression(SlimeFace.SlimeExpression.Elated);
      StartCoroutine(ResetExpressionAfterTime(component1));
    }
    else if (componentInChildren1 != null)
      componentInChildren1.color = Color.white;
    if (selectFx != null)
      SpawnAndPlayFX(selectFx, appearancePreview1);
    for (int index1 = 0; index1 < currentSlimeAppearancePreviews.Count; ++index1)
    {
      if (index1 != index)
      {
        GameObject appearancePreview2 = currentSlimeAppearancePreviews[index1];
        SlimeAppearanceApplicator component2 = appearancePreview2.GetComponent<SlimeAppearanceApplicator>();
        SpriteRenderer componentInChildren2 = appearancePreview2.GetComponentInChildren<SpriteRenderer>();
        if (component2 == null && componentInChildren2 != null)
          componentInChildren2.color = Color.grey;
        appearancePreview2.transform.DOLocalJump(GetUnfocusedPosition(new Vector3((float) (index1 * (double) spacing - spacing / 2.0), 0.0f, 0.0f)), jumpAmount, 1, transitionTime).SetUpdate(true);
      }
    }
    currentSlimeAppearancePreviews[index].transform.DOLocalJump(new Vector3((float) (index * (double) spacing - spacing / 2.0), 0.0f, 0.0f), jumpAmount, 1, transitionTime).SetUpdate(true);
    spotlight.transform.DOLocalMove(new Vector3((float) (index * (double) spacing - spacing / 2.0), spotlight.localPosition.y, spotlight.localPosition.z), transitionTime).SetUpdate(true);
  }

  private bool UseSpriteForSlime(SlimeDefinition slime) => slime.IdentifiableId == Identifiable.Id.SABER_SLIME;

  private GameObject CreateAppearancePreview(
    SlimeDefinition slime,
    SlimeAppearance appearance,
    bool isSelected)
  {
    GameObject gameObject;
    if (UseSpriteForSlime(slime))
    {
      gameObject = Instantiate(appearanceSpritePrefab, root);
      SpriteRenderer componentInChildren = gameObject.GetComponentInChildren<SpriteRenderer>();
      SetLayerInChildren(gameObject);
      currentSlimeAppearancePreviews.Add(gameObject);
      componentInChildren.sprite = appearance.Icon ?? slimeAppearanceDirector.missingIcon;
      componentInChildren.color = isSelected ? Color.white : Color.grey;
    }
    else
    {
      SlimeAppearanceApplicator appearanceApplicator = Instantiate(appearancePrefab, root);
      appearanceApplicator.Appearance = appearance;
      appearanceApplicator.ApplyAppearance();
      currentSlimeAppearancePreviews.Add(appearanceApplicator.gameObject);
      SetLayerInChildren(appearanceApplicator.gameObject);
      foreach (System.Type blacklistedObjectType in blacklistedObjectTypes)
      {
        foreach (Component componentsInChild in appearanceApplicator.GetComponentsInChildren(blacklistedObjectType))
          componentsInChild.gameObject.SetActive(false);
      }
      foreach (EnableBasedOnGrounded componentsInChild in appearanceApplicator.GetComponentsInChildren<EnableBasedOnGrounded>())
      {
        if (componentsInChild.enableOnGrounded)
          componentsInChild.gameObject.SetActive(false);
      }
      foreach (Behaviour componentsInChild in appearanceApplicator.GetComponentsInChildren<DeactivateOnHeld>())
        componentsInChild.enabled = false;
      foreach (Animator componentsInChild in appearanceApplicator.GetComponentsInChildren<Animator>())
        componentsInChild.updateMode = AnimatorUpdateMode.UnscaledTime;
      foreach (ParticleSystem componentsInChild in appearanceApplicator.GetComponentsInChildren<ParticleSystem>())
      {
        var mainModule = componentsInChild.main;
        mainModule.useUnscaledTime = true;
      }

      foreach (Renderer componentsInChild in appearanceApplicator.GetComponentsInChildren<Renderer>())
      {
        foreach (Material material in componentsInChild.materials)
        {
          material.SetInt(unscaledTimePropertyId, 1);
          material.EnableKeyword("_UNSCALEDTIME_ON");
        }
      }
      appearanceApplicator.GetComponentInChildren<RubberBoneEffect>().unscaledTime = true;
      gameObject = appearanceApplicator.gameObject;
    }
    Instantiate(shadowPrefab, gameObject.transform);
    return gameObject;
  }

  private void SetLayerInChildren(GameObject gameObject)
  {
    foreach (Component componentsInChild in gameObject.GetComponentsInChildren<Transform>())
      componentsInChild.gameObject.layer = this.gameObject.layer;
  }

  public delegate void OnSlimeAppearanceSelectedDelegate(
    SlimeDefinition slime,
    SlimeAppearance appearance);
}
