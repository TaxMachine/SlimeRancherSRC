// Decompiled with JetBrains decompiler
// Type: SECTR_ToolTip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;

[AttributeUsage(AttributeTargets.Field)]
public class SECTR_ToolTip : Attribute
{
  private string tipText;
  private string dependentProperty;
  private float min;
  private float max;
  private Type enumType;
  private bool hasRange;
  private bool devOnly;
  private bool sceneObjectOverride;
  private bool allowSceneObjects;
  private bool treatAsLayer;

  public SECTR_ToolTip(string tipText) => this.tipText = tipText;

  public SECTR_ToolTip(string tipText, float min, float max)
  {
    this.tipText = tipText;
    this.min = min;
    this.max = max;
    hasRange = true;
  }

  public SECTR_ToolTip(string tipText, string dependentProperty)
  {
    this.tipText = tipText;
    this.dependentProperty = dependentProperty;
  }

  public SECTR_ToolTip(string tipText, string dependentProperty, float min, float max)
  {
    this.tipText = tipText;
    this.dependentProperty = dependentProperty;
    this.min = min;
    this.max = max;
    hasRange = true;
  }

  public SECTR_ToolTip(string tipText, bool devOnly)
  {
    this.tipText = tipText;
    this.devOnly = devOnly;
  }

  public SECTR_ToolTip(string tipText, bool devOnly, bool treatAsLayer)
  {
    this.tipText = tipText;
    this.devOnly = devOnly;
    this.treatAsLayer = treatAsLayer;
  }

  public SECTR_ToolTip(string tipText, string dependentProperty, Type enumType)
  {
    this.tipText = tipText;
    this.dependentProperty = dependentProperty;
    this.enumType = enumType;
  }

  public SECTR_ToolTip(string tipText, string dependentProperty, bool allowSceneObjects)
  {
    this.tipText = tipText;
    this.dependentProperty = dependentProperty;
    sceneObjectOverride = true;
    this.allowSceneObjects = allowSceneObjects;
  }

  public string TipText => tipText;

  public string DependentProperty => dependentProperty;

  public float Min => min;

  public float Max => max;

  public Type EnumType => enumType;

  public bool HasRange => hasRange;

  public bool DevOnly => devOnly;

  public bool SceneObjectOverride => sceneObjectOverride;

  public bool AllowSceneObjects => allowSceneObjects;

  public bool TreatAsLayer => treatAsLayer;
}
