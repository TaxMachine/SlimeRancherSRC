// Decompiled with JetBrains decompiler
// Type: OrthoLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class OrthoLineup : MonoBehaviour
{
  public SlimeAppearanceApplicator appearancePrefab;
  public SlimeDefinitions definitions;
  public Vector2 viewSpacing = new Vector2(1.5f, 1.25f);
  public float extraLabelSpacing = 1f;
  public float extraAppearanceSpacing = 1f;
  public Camera cam;
  public float cameraSpeed = 2f;
  public bool showLabels = true;
  public TextMesh labelPrefab;
  public RuntimeAnimatorController animatorController;
  public AnimationClip idle;
  public AnimationClip idleOverride;
  public bool includeLargos;
  public Quaternion[] views = new Quaternion[3]
  {
    Quaternion.Euler(0.0f, 180f, 0.0f),
    Quaternion.Euler(0.0f, 90f, 0.0f),
    Quaternion.identity
  };
  private readonly Type[] blacklistedObjectTypes = new Type[2]
  {
    typeof (RadExpandMarker),
    typeof (TrailRenderer)
  };

  public void Start()
  {
    Time.timeScale = 0.0f;
    SRQualitySettings.CurrentLevel = SRQualitySettings.Level.VERY_HIGH;
    ShowLineup();
  }

  public void Update()
  {
    float axisRaw1 = Input.GetAxisRaw("Horizontal");
    float axisRaw2 = Input.GetAxisRaw("Vertical");
    cam.transform.position += (Input.GetKey(KeyCode.LeftShift) ? 3f : 1f) * cameraSpeed * Time.unscaledDeltaTime * new Vector3(axisRaw1, axisRaw2, 0.0f).normalized;
    if (!Input.GetKeyDown(KeyCode.Space))
      return;
    string filename = string.Format("{0}orthoslimes-{1}.png", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar.ToString(), DateTime.Now.ToFileTime());
    ScreenCapture.CaptureScreenshot(filename);
    Log.Debug("Screenshot saved as " + filename);
  }

  public void ShowLineup()
  {
    List<SlimeDefinition> list = definitions.Slimes.Where(slime => includeLargos || !slime.IsLargo).ToList();
    AnimatorOverrideController overrideController = new AnimatorOverrideController(animatorController);
    overrideController.ApplyOverrides(new List<KeyValuePair<AnimationClip, AnimationClip>>(new KeyValuePair<AnimationClip, AnimationClip>[1]
    {
      new KeyValuePair<AnimationClip, AnimationClip>(idle, idleOverride)
    }));
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      SlimeDefinition slimeDefinition = list[index1];
      Vector3 vector3_1 = new Vector3(0.0f, (float) (index1 * -(double) viewSpacing.y + (showLabels ? index1 * -(double) extraLabelSpacing : 0.0)));
      GameObject gameObject1 = new GameObject(slimeDefinition.Name);
      gameObject1.transform.position = vector3_1;
      gameObject1.transform.parent = transform;
      for (int index2 = 0; index2 < slimeDefinition.Appearances.Count(); ++index2)
      {
        SlimeAppearance appearance = slimeDefinition.Appearances.ElementAt(index2);
        Vector3 vector3_2 = new Vector3(index2 * (viewSpacing.x * views.Length + extraAppearanceSpacing), 0.0f, 0.0f);
        string name = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("actor").Xlate(appearance.NameXlateKey);
        if (string.IsNullOrEmpty(name))
          name = "Classic";
        GameObject gameObject2 = new GameObject(name);
        gameObject2.transform.parent = gameObject1.transform;
        gameObject2.transform.localPosition = vector3_2;
        if (showLabels)
        {
          string str = string.Format("{0} ({1})", slimeDefinition.Name, name);
          TextMesh textMesh = Instantiate(labelPrefab, gameObject2.transform);
          textMesh.transform.localPosition = Vector3.zero;
          textMesh.transform.name = "Label";
          textMesh.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
          textMesh.text = str;
        }
        for (int index3 = 0; index3 < views.Length; ++index3)
        {
          SlimeAppearanceApplicator appearancePreview = LineupUtils.GenerateAppearancePreview(appearancePrefab, slimeDefinition, appearance);
          appearancePreview.GetComponentInChildren<Animator>().runtimeAnimatorController = overrideController;
          foreach (Type blacklistedObjectType in blacklistedObjectTypes)
          {
            foreach (Component componentsInChild in appearancePreview.GetComponentsInChildren(blacklistedObjectType))
              componentsInChild.gameObject.SetActive(false);
          }
          Vector3 vector3_3 = new Vector3(index3 * viewSpacing.x, showLabels ? -extraLabelSpacing : 0.0f, 0.0f);
          appearancePreview.transform.parent = gameObject2.transform;
          appearancePreview.transform.localPosition = vector3_3;
          appearancePreview.transform.rotation = views[index3];
        }
      }
    }
  }
}
