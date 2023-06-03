// Decompiled with JetBrains decompiler
// Type: SlimeAppearanceApplicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Assets.Script.Util.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class SlimeAppearanceApplicator : MonoBehaviour
{
  public SlimeAppearanceDirector SlimeAppearanceDirector;
  public SlimeAppearance Appearance;
  public SlimeDefinition SlimeDefinition;
  public BoneMapping[] Bones;
  public SlimeAppearanceObjectProvider AppearanceObjectProvider;
  public LODGroup LODGroup;
  public GameObject RootAppearanceObject;
  private Dictionary<SlimeAppearance.SlimeBone, GameObject> _boneLookup;
  private List<AppearanceObjectPair> _currentAppearanceObjects = new List<AppearanceObjectPair>();
  private List<FaceRenderer> _faceRenderers = new List<FaceRenderer>();
  private const int EYES_MATERIAL_INDEX_OFFSET = 2;
  private const int MOUTH_MATERIAL_INDEX_OFFSET = 1;
  private const int LOD_GROUP_LEVEL_COUNT = 4;
  private bool _isInitialized;
  public SlimeFace.SlimeExpression SlimeExpression = SlimeFace.SlimeExpression.Happy;
  private Animator animator;
  private RecalculateBoundsHelper recalculateBoundsHelper;
  private SlimeAnimatorStateIdle animatorState;

  public event OnAppearanceChangedDelegate OnAppearanceChanged = _param1 => { };

  public void Initialize(bool force = false)
  {
    if (_isInitialized && !force)
      return;
    if (_boneLookup == null)
      _boneLookup = new Dictionary<SlimeAppearance.SlimeBone, GameObject>(SlimeAppearance.DefaultBoneComparer);
    else
      _boneLookup.Clear();
    foreach (BoneMapping bone in Bones)
    {
      if (_boneLookup.ContainsKey(bone.Bone))
        Log.Error("Duplicate bone in SlimeAppearanceApplicator: {0}", bone.Bone);
      else
        _boneLookup.Add(bone.Bone, bone.BoneObject);
    }
  }

  public void Awake()
  {
    if (!(SlimeDefinition != null) || !(SlimeAppearanceDirector != null))
      return;
    SlimeAppearance chosenSlimeAppearance = SlimeAppearanceDirector.GetChosenSlimeAppearance(SlimeDefinition);
    if (Appearance != chosenSlimeAppearance)
      Appearance = chosenSlimeAppearance;
    SlimeAppearanceDirector.onSlimeAppearanceChanged += HandleChosenAppearanceChanged;
    animator = RootAppearanceObject.GetRequiredComponent<Animator>();
    ApplyAppearance();
  }

  public void OnDestroy()
  {
    if (AppearanceObjectProvider != null)
    {
      foreach (AppearanceObjectPair appearanceObject in _currentAppearanceObjects)
      {
        if (appearanceObject.AppearanceObject != null)
          AppearanceObjectProvider.Put(appearanceObject.Prefab, appearanceObject.AppearanceObject);
      }
    }
    if (!(SlimeAppearanceDirector != null))
      return;
    SlimeAppearanceDirector.onSlimeAppearanceChanged -= HandleChosenAppearanceChanged;
  }

  public void ApplyAppearance()
  {
    Initialize();
    if (AppearanceObjectProvider == null)
      AppearanceObjectProvider = new PooledSlimeAppearanceObjectProvider(SRSingleton<SceneContext>.Instance.appearanceObjectPool);
    ClearAppearance();
    if (Appearance == null)
      return;
    if (animator != null)
      animator.runtimeAnimatorController = !(Appearance.AnimatorOverride != null) ? SlimeAppearanceDirector.defaultAnimatorController : Appearance.AnimatorOverride;
    List<Renderer>[] lods = new List<Renderer>[4];
    for (int index = 0; index < lods.Length; ++index)
      lods[index] = new List<Renderer>();
    foreach (SlimeAppearanceStructure structure in Appearance.Structures)
      ApplyAppearanceStructure(structure, lods);
    LOD[] loDs = LODGroup.GetLODs();
    for (int index = 0; index < loDs.Length; ++index)
      loDs[index].renderers = lods[index].ToArray();
    LODGroup.SetLODs(loDs);
    RecalculateBoundsHelper.RecalculateBounds(this);
    SetExpression(SlimeFace.SlimeExpression.Happy);
    OnAppearanceChanged(Appearance);
  }

  public Transform GetFashionParent(Fashion.Slot fashionSlot)
  {
    if (fashionSlot == Fashion.Slot.TOP)
      return _boneLookup[SlimeAppearance.SlimeBone.JiggleTop].transform;
    if (fashionSlot == Fashion.Slot.FRONT)
      return _boneLookup[SlimeAppearance.SlimeBone.JiggleBack].transform;
    Log.Error("Unhandled fashion slot", "slot", fashionSlot);
    return null;
  }

  private void HandleChosenAppearanceChanged(
    SlimeDefinition definition,
    SlimeAppearance newAppearance)
  {
    if (!(SlimeDefinition == definition))
      return;
    Appearance = newAppearance;
    ApplyAppearance();
  }

  private void ClearAppearance()
  {
    foreach (AppearanceObjectPair appearanceObject in _currentAppearanceObjects)
      AppearanceObjectProvider.Put(appearanceObject.Prefab, appearanceObject.AppearanceObject);
    _currentAppearanceObjects.Clear();
    _faceRenderers.Clear();
  }

  private void ApplyAppearanceStructure(
    SlimeAppearanceStructure appearanceStructure,
    List<Renderer>[] lods)
  {
    for (int objectIndex = 0; objectIndex < appearanceStructure.Element.Prefabs.Length; ++objectIndex)
      ApplyAppearanceObject(appearanceStructure, appearanceStructure.Element, appearanceStructure.Element.Prefabs[objectIndex], objectIndex, lods);
  }

  private void ApplyAppearanceObject(
    SlimeAppearanceStructure structure,
    SlimeAppearanceElement element,
    SlimeAppearanceObject appearancePrefab,
    int objectIndex,
    List<Renderer>[] lods)
  {
    GameObject appearanceObject1 = RootAppearanceObject;
    if (appearancePrefab.ParentBone != SlimeAppearance.SlimeBone.None)
    {
      appearanceObject1 = _boneLookup.Get(appearancePrefab.ParentBone);
      if (appearanceObject1 == null)
      {
        Log.Error("Unable to find ParentBone for element.", "ParentBone", appearancePrefab.ParentBone, "AppearanceObject", appearancePrefab.name);
        return;
      }
    }
    SlimeAppearanceObject appearanceObject2;
    try
    {
      appearanceObject2 = AppearanceObjectProvider.Get(appearancePrefab, appearanceObject1);
    }
    catch (Exception ex)
    {
      Log.Error("caught exception e", "prefab", appearancePrefab, "exception", ex);
      throw;
    }
    _currentAppearanceObjects.Add(new AppearanceObjectPair(appearancePrefab, appearanceObject2));
    Renderer component1 = appearanceObject2.GetComponent<Renderer>();
    if (component1 != null)
    {
      int num = 0;
      if (structure.SupportsFaces)
      {
        SlimeFaceRules faceRule = structure.FaceRules[objectIndex];
        if (faceRule.ShowEyes || faceRule.ShowMouth)
          _faceRenderers.Add(new FaceRenderer()
          {
            Renderer = component1,
            ShowEyes = structure.FaceRules[objectIndex].ShowEyes,
            ShowMouth = structure.FaceRules[objectIndex].ShowMouth
          });
        num += (faceRule.ShowEyes ? 1 : 0) + (faceRule.ShowMouth ? 1 : 0);
      }
      Material[] sourceArray = structure.ElementMaterials[objectIndex].OverrideDefaults ? structure.ElementMaterials[objectIndex].Materials : structure.DefaultMaterials;
      Material[] destinationArray = new Material[num + sourceArray.Length];
      Array.Copy(sourceArray, destinationArray, sourceArray.Length);
      component1.materials = destinationArray;
      if (component1 is SkinnedMeshRenderer)
      {
        SkinnedMeshRenderer skinnedMeshRenderer = component1 as SkinnedMeshRenderer;
        Transform[] transformArray = new Transform[appearancePrefab.AttachedBones.Length];
        for (int index = 0; index < appearancePrefab.AttachedBones.Length; ++index)
          transformArray[index] = _boneLookup[appearancePrefab.AttachedBones[index]].transform;
        skinnedMeshRenderer.bones = transformArray;
        skinnedMeshRenderer.rootBone = _boneLookup[appearancePrefab.RootBone].transform;
      }
      if (!appearancePrefab.IgnoreLODIndex)
        lods[appearancePrefab.LODIndex].Add(component1);
    }
    if (!appearancePrefab.AttachRubberBoneEffect || !(component1 is SkinnedMeshRenderer))
      return;
    RubberBoneEffect component2 = RootAppearanceObject.GetComponent<RubberBoneEffect>();
    component2.skinRenderer = component1 as SkinnedMeshRenderer;
    component2.Presets = appearancePrefab.RubberType;
  }

  public void SetExpression(SlimeFace.SlimeExpression slimeExpression)
  {
    SlimeExpressionFace expressionFace = Appearance.Face.GetExpressionFace(slimeExpression);
    foreach (FaceRenderer faceRenderer in _faceRenderers)
    {
      Material[] sharedMaterials = faceRenderer.Renderer.sharedMaterials;
      int index1 = sharedMaterials.Length - 2;
      int index2 = sharedMaterials.Length - 1;
      if (faceRenderer.ShowEyes != faceRenderer.ShowMouth)
        index1 = index2;
      if (faceRenderer.ShowEyes && expressionFace.Eyes != null)
        sharedMaterials[index1] = expressionFace.Eyes;
      if (faceRenderer.ShowMouth && expressionFace.Mouth != null)
        sharedMaterials[index2] = expressionFace.Mouth;
      faceRenderer.Renderer.sharedMaterials = sharedMaterials;
    }
  }

  public SlimeAppearance.Palette GetAppearancePalette()
  {
    if (!(Appearance == null))
      return Appearance.ColorPalette;
    Log.Warning("Appearance was null when retrieving appearance palette. Returning default palette");
    return SlimeAppearance.Palette.Default;
  }

  private void SetHideFlags(GameObject gameObject)
  {
    gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
    if (gameObject.transform.childCount <= 0)
      return;
    foreach (Component componentsInChild in gameObject.GetComponentsInChildren<Transform>())
      componentsInChild.gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
  }

  public delegate void OnAppearanceChangedDelegate(SlimeAppearance newAppearance);

  [Serializable]
  public struct AppearanceObjectPair
  {
    public SlimeAppearanceObject Prefab;
    public SlimeAppearanceObject AppearanceObject;

    public AppearanceObjectPair(
      SlimeAppearanceObject prefab,
      SlimeAppearanceObject appearanceObject)
    {
      Prefab = prefab;
      AppearanceObject = appearanceObject;
    }
  }

  [Serializable]
  public struct BoneMapping
  {
    public SlimeAppearance.SlimeBone Bone;
    public GameObject BoneObject;
  }

  public struct FaceRenderer
  {
    public Renderer Renderer;
    public bool ShowEyes;
    public bool ShowMouth;
  }

  private class RecalculateBoundsHelper : MonoBehaviour
  {
    private SlimeAppearanceApplicator parent;

    public static void RecalculateBounds(SlimeAppearanceApplicator parent)
    {
      if (parent.recalculateBoundsHelper != null)
        return;
      if (!Application.isPlaying || parent.animator == null)
      {
        parent.LODGroup.RecalculateBounds();
      }
      else
      {
        if (TryRecalculateBounds(parent))
          return;
        parent.recalculateBoundsHelper = parent.gameObject.AddComponent<RecalculateBoundsHelper>();
        parent.recalculateBoundsHelper.parent = parent;
      }
    }

    private static bool TryRecalculateBounds(SlimeAppearanceApplicator parent)
    {
      if (!parent.gameObject.activeInHierarchy)
        return false;
      if (parent.animatorState == null)
        parent.animatorState = parent.animator.GetBehaviour<SlimeAnimatorStateIdle>();
      if (!parent.animatorState.IsInitialized)
      {
        parent.LODGroup.RecalculateBounds();
        return true;
      }
      if (!parent.animatorState.IsCurrentState)
        return false;
      parent.LODGroup.RecalculateBounds();
      return true;
    }

    public void Update()
    {
      if (!TryRecalculateBounds(parent))
        return;
      Destroyer.Destroy(this, "RecalculateBoundsHelper.Update");
    }
  }
}
