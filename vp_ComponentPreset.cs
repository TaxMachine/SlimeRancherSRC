// Decompiled with JetBrains decompiler
// Type: vp_ComponentPreset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

public sealed class vp_ComponentPreset
{
  private static string m_FullPath = null;
  private static int m_LineNumber = 0;
  public static bool LogErrors = true;
  private static ReadMode m_ReadMode = ReadMode.Normal;
  private Type m_ComponentType;
  private List<Field> m_Fields = new List<Field>();
  private Dictionary<string, string[]> MovedParameters = new Dictionary<string, string[]>()
  {
    {
      "vp_FPCamera.MouseAcceleration",
      new string[2]{ "vp_FPInput", "MouseLookAcceleration" }
    },
    {
      "vp_FPCamera.MouseSensitivity",
      new string[2]{ "vp_FPInput", "MouseLookSensitivity" }
    },
    {
      "vp_FPCamera.MouseSmoothSteps",
      new string[2]{ "vp_FPInput", "MouseLookSmoothSteps" }
    },
    {
      "vp_FPCamera.MouseSmoothWeight",
      new string[2]{ "vp_FPInput", "MouseLookSmoothWeight" }
    },
    {
      "vp_FPCamera.MouseAccelerationThreshold",
      new string[2]
      {
        "vp_FPInput",
        "MouseLookAccelerationThreshold"
      }
    },
    {
      "vp_FPInput.ForceCursor",
      new string[2]{ "vp_FPInput", "MouseCursorForced" }
    }
  };

  public Type ComponentType
  {
    get => m_ComponentType;
    set => m_ComponentType = value;
  }

  public static string Save(Component component, string fullPath)
  {
    vp_ComponentPreset savePreset = new vp_ComponentPreset();
    savePreset.InitFromComponent(component);
    return Save(savePreset, fullPath);
  }

  public static string Save(vp_ComponentPreset savePreset, string fullPath, bool isDifference = false)
  {
    m_FullPath = fullPath;
    int num = LogErrors ? 1 : 0;
    LogErrors = false;
    vp_ComponentPreset vpComponentPreset = new vp_ComponentPreset();
    vpComponentPreset.LoadTextStream(m_FullPath);
    LogErrors = num != 0;
    if (vpComponentPreset != null)
    {
      if (vpComponentPreset.m_ComponentType != null)
      {
        if (vpComponentPreset.ComponentType != savePreset.ComponentType)
          return "'" + ExtractFilenameFromPath(m_FullPath) + "' has the WRONG component type: " + vpComponentPreset.ComponentType.ToString() + ".\n\nDo you want to replace it with a " + savePreset.ComponentType.ToString() + "?";
        if (File.Exists(m_FullPath))
          return isDifference ? "This will update '" + ExtractFilenameFromPath(m_FullPath) + "' with only the values modified since pressing Play or setting a state.\n\nContinue?" : "'" + ExtractFilenameFromPath(m_FullPath) + "' already exists.\n\nDo you want to replace it?";
      }
      if (File.Exists(m_FullPath))
        return "'" + ExtractFilenameFromPath(m_FullPath) + "' has an UNKNOWN component type.\n\nDo you want to replace it?";
    }
    ClearTextFile();
    Append("///////////////////////////////////////////////////////////");
    Append("// Component Preset Script");
    Append("///////////////////////////////////////////////////////////\n");
    Append("ComponentType " + savePreset.ComponentType.Name);
    foreach (Field field in savePreset.m_Fields)
    {
      string str1 = "";
      FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(field.FieldHandle);
      string str2;
      if (fieldFromHandle.FieldType == typeof (float))
        str2 = string.Format("{0:0.#######}", (float) field.Args);
      else if (fieldFromHandle.FieldType == typeof (Vector4))
      {
        Vector4 args = (Vector4) field.Args;
        str2 = string.Format("{0:0.#######}", args.x) + " " + string.Format("{0:0.#######}", args.y) + " " + string.Format("{0:0.#######}", args.z) + " " + string.Format("{0:0.#######}", args.w);
      }
      else if (fieldFromHandle.FieldType == typeof (Vector3))
      {
        Vector3 args = (Vector3) field.Args;
        str2 = string.Format("{0:0.#######}", args.x) + " " + string.Format("{0:0.#######}", args.y) + " " + string.Format("{0:0.#######}", args.z);
      }
      else if (fieldFromHandle.FieldType == typeof (Vector2))
      {
        Vector2 args = (Vector2) field.Args;
        str2 = string.Format("{0:0.#######}", args.x) + " " + string.Format("{0:0.#######}", args.y);
      }
      else if (fieldFromHandle.FieldType == typeof (int))
        str2 = ((int) field.Args).ToString();
      else if (fieldFromHandle.FieldType == typeof (bool))
        str2 = ((bool) field.Args).ToString();
      else if (fieldFromHandle.FieldType == typeof (string))
      {
        str2 = (string) field.Args;
      }
      else
      {
        str1 = "//";
        str2 = "<NOTE: Type '" + fieldFromHandle.FieldType.Name.ToString() + "' can't be saved to preset.>";
      }
      if (!string.IsNullOrEmpty(str2) && fieldFromHandle.Name != "Persist")
        Append(str1 + fieldFromHandle.Name + " " + str2);
    }
    return null;
  }

  public static string SaveDifference(
    vp_ComponentPreset initialStatePreset,
    Component modifiedComponent,
    string fullPath,
    vp_ComponentPreset diskPreset)
  {
    if (initialStatePreset.ComponentType != modifiedComponent.GetType())
    {
      Error("Tried to save difference between different type components in 'SaveDifference'");
      return null;
    }
    vp_ComponentPreset vpComponentPreset = new vp_ComponentPreset();
    vpComponentPreset.InitFromComponent(modifiedComponent);
    vp_ComponentPreset savePreset = new vp_ComponentPreset();
    savePreset.m_ComponentType = vpComponentPreset.ComponentType;
    for (int index = 0; index < vpComponentPreset.m_Fields.Count; ++index)
    {
      if (!initialStatePreset.m_Fields[index].Args.Equals(vpComponentPreset.m_Fields[index].Args))
        savePreset.m_Fields.Add(vpComponentPreset.m_Fields[index]);
    }
    foreach (Field field1 in diskPreset.m_Fields)
    {
      bool flag1 = true;
      foreach (Field field2 in savePreset.m_Fields)
      {
        if (field1.FieldHandle == field2.FieldHandle)
          flag1 = false;
      }
      bool flag2 = false;
      foreach (Field field3 in vpComponentPreset.m_Fields)
      {
        if (field1.FieldHandle == field3.FieldHandle)
          flag2 = true;
      }
      if (!flag2)
        flag1 = false;
      if (flag1)
        savePreset.m_Fields.Add(field1);
    }
    return Save(savePreset, fullPath, true);
  }

  public void InitFromComponent(Component component)
  {
  }

  public static vp_ComponentPreset CreateFromComponent(Component component) => null;

  public int TryMakeCompatibleWithComponent(vp_Component component) => 0;

  public bool LoadTextStream(string fullPath) => true;

  public static bool Load(vp_Component component, string fullPath)
  {
    vp_ComponentPreset preset = new vp_ComponentPreset();
    preset.LoadTextStream(fullPath);
    return Apply(component, preset);
  }

  public bool LoadFromResources(string resourcePath)
  {
    m_FullPath = resourcePath;
    TextAsset file = Resources.Load(m_FullPath) as TextAsset;
    if (!(file == null))
      return LoadFromTextAsset(file);
    Error("Failed to read file. '" + m_FullPath + "'");
    return false;
  }

  public static vp_ComponentPreset LoadFromResources(vp_Component component, string resourcePath)
  {
    vp_ComponentPreset preset = new vp_ComponentPreset();
    preset.LoadFromResources(resourcePath);
    Apply(component, preset);
    return preset;
  }

  public bool LoadFromTextAsset(TextAsset file)
  {
    m_FullPath = file.name;
    List<string> lines = new List<string>();
    string text = file.text;
    char[] chArray = new char[1]{ '\n' };
    foreach (string str in text.Split(chArray))
      lines.Add(str);
    if (lines == null)
    {
      Error("Preset is empty. '" + m_FullPath + "'");
      return false;
    }
    ParseLines(lines);
    return true;
  }

  public static vp_ComponentPreset LoadFromTextAsset(vp_Component component, TextAsset file)
  {
    vp_ComponentPreset preset = new vp_ComponentPreset();
    preset.LoadFromTextAsset(file);
    Apply(component, preset);
    return preset;
  }

  private static void Append(string str)
  {
  }

  private static void ClearTextFile()
  {
  }

  private void ParseLines(List<string> lines)
  {
    m_LineNumber = 0;
    foreach (string line1 in lines)
    {
      ++m_LineNumber;
      string line2 = RemoveComments(line1);
      if (!string.IsNullOrEmpty(line2) && !Parse(line2))
        return;
    }
    m_LineNumber = 0;
  }

  private bool Parse(string line) => true;

  private string[] FindMovedParameter(string type, string field)
  {
    string[] strArray;
    return !MovedParameters.TryGetValue(type + "." + field, out strArray) ? null : strArray;
  }

  public static bool Apply(vp_Component component, vp_ComponentPreset preset) => true;

  public static Type GetFileType(string fullPath)
  {
    int num = LogErrors ? 1 : 0;
    LogErrors = false;
    vp_ComponentPreset vpComponentPreset = new vp_ComponentPreset();
    vpComponentPreset.LoadTextStream(fullPath);
    LogErrors = num != 0;
    return vpComponentPreset != null && vpComponentPreset.m_ComponentType != null ? vpComponentPreset.m_ComponentType : null;
  }

  public static Type GetFileTypeFromAsset(TextAsset asset)
  {
    int num = LogErrors ? 1 : 0;
    LogErrors = false;
    vp_ComponentPreset vpComponentPreset = new vp_ComponentPreset();
    vpComponentPreset.LoadFromTextAsset(asset);
    LogErrors = num != 0;
    return vpComponentPreset != null && vpComponentPreset.m_ComponentType != null ? vpComponentPreset.m_ComponentType : null;
  }

  private static object TokensToObject(FieldInfo field, string[] tokens)
  {
    if (field.FieldType == typeof (float))
      return ArgsToFloat(tokens);
    if (field.FieldType == typeof (Vector4))
      return ArgsToVector4(tokens);
    if (field.FieldType == typeof (Vector3))
      return ArgsToVector3(tokens);
    if (field.FieldType == typeof (Vector2))
      return ArgsToVector2(tokens);
    if (field.FieldType == typeof (int))
      return ArgsToInt(tokens);
    if (field.FieldType == typeof (bool))
      return ArgsToBool(tokens);
    return field.FieldType == typeof (string) ? ArgsToString(tokens) : (object) null;
  }

  private static string RemoveComments(string str)
  {
    string str1 = "";
    for (int index = 0; index < str.Length; ++index)
    {
      switch (m_ReadMode)
      {
        case ReadMode.Normal:
          if (str[index] == '/' && str[index + 1] == '*')
          {
            m_ReadMode = ReadMode.BlockComment;
            ++index;
            break;
          }
          if (str[index] == '/' && str[index + 1] == '/')
          {
            m_ReadMode = ReadMode.LineComment;
            ++index;
            break;
          }
          str1 += str[index].ToString();
          break;
        case ReadMode.LineComment:
          if (index == str.Length - 1)
          {
            m_ReadMode = ReadMode.Normal;
            break;
          }
          break;
        case ReadMode.BlockComment:
          if (str[index] == '*' && str[index + 1] == '/')
          {
            m_ReadMode = ReadMode.Normal;
            ++index;
            break;
          }
          break;
      }
    }
    return str1;
  }

  private static Vector4 ArgsToVector4(string[] args)
  {
    if (args.Length - 1 != 4)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return Vector4.zero;
    }
    try
    {
      return new Vector4(Convert.ToSingle(args[1], CultureInfo.InvariantCulture), Convert.ToSingle(args[2], CultureInfo.InvariantCulture), Convert.ToSingle(args[3], CultureInfo.InvariantCulture), Convert.ToSingle(args[4], CultureInfo.InvariantCulture));
    }
    catch
    {
      PresetError("Illegal value: '" + args[1] + ", " + args[2] + ", " + args[3] + ", " + args[4] + "'");
      return Vector4.zero;
    }
  }

  private static Vector3 ArgsToVector3(string[] args)
  {
    if (args.Length - 1 != 3)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return Vector3.zero;
    }
    try
    {
      return new Vector3(Convert.ToSingle(args[1], CultureInfo.InvariantCulture), Convert.ToSingle(args[2], CultureInfo.InvariantCulture), Convert.ToSingle(args[3], CultureInfo.InvariantCulture));
    }
    catch
    {
      PresetError("Illegal value: '" + args[1] + ", " + args[2] + ", " + args[3] + "'");
      return Vector3.zero;
    }
  }

  private static Vector2 ArgsToVector2(string[] args)
  {
    if (args.Length - 1 != 2)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return Vector2.zero;
    }
    try
    {
      return new Vector2(Convert.ToSingle(args[1], CultureInfo.InvariantCulture), Convert.ToSingle(args[2], CultureInfo.InvariantCulture));
    }
    catch
    {
      PresetError("Illegal value: '" + args[1] + ", " + args[2] + "'");
      return Vector2.zero;
    }
  }

  private static float ArgsToFloat(string[] args)
  {
    if (args.Length - 1 != 1)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return 0.0f;
    }
    try
    {
      return Convert.ToSingle(args[1], CultureInfo.InvariantCulture);
    }
    catch
    {
      PresetError("Illegal value: '" + args[1] + "'");
      return 0.0f;
    }
  }

  private static int ArgsToInt(string[] args)
  {
    if (args.Length - 1 != 1)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return 0;
    }
    try
    {
      return Convert.ToInt32(args[1], CultureInfo.InvariantCulture);
    }
    catch
    {
      PresetError("Illegal value: '" + args[1] + "'");
      return 0;
    }
  }

  private static bool ArgsToBool(string[] args)
  {
    if (args.Length - 1 != 1)
    {
      PresetError("Wrong number of fields for '" + args[0] + "'");
      return false;
    }
    if (args[1].ToLower() == "true")
      return true;
    if (args[1].ToLower() == "false")
      return false;
    PresetError("Illegal value: '" + args[1] + "'");
    return false;
  }

  private static string ArgsToString(string[] args)
  {
    string str = "";
    for (int index = 1; index < args.Length; ++index)
    {
      str += args[index];
      if (index < args.Length - 1)
        str += " ";
    }
    return str;
  }

  public Type GetFieldType(string fieldName)
  {
    Type fieldType = null;
    foreach (Field field in m_Fields)
    {
      FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(field.FieldHandle);
      if (fieldFromHandle.Name == fieldName)
        fieldType = fieldFromHandle.FieldType;
    }
    return fieldType;
  }

  public object GetFieldValue(string fieldName)
  {
    object fieldValue = null;
    foreach (Field field in m_Fields)
    {
      if (FieldInfo.GetFieldFromHandle(field.FieldHandle).Name == fieldName)
        fieldValue = field.Args;
    }
    return fieldValue;
  }

  public static string ExtractFilenameFromPath(string path)
  {
    int num = Math.Max(path.LastIndexOf('/'), path.LastIndexOf('\\'));
    if (num == -1)
      return path;
    return num == path.Length - 1 ? "" : path.Substring(num + 1, path.Length - num - 1);
  }

  private static void PresetError(string message)
  {
    if (!LogErrors)
      return;
    Debug.LogError("Preset Error: " + m_FullPath + " (at " + m_LineNumber + ") " + message);
  }

  private static void PresetWarning(string message)
  {
    if (!LogErrors)
      return;
    Debug.LogWarning("Preset Warning: " + m_FullPath + " (at " + m_LineNumber + ") " + message);
  }

  private static void Error(string message)
  {
    if (!LogErrors)
      return;
    Debug.LogError("Error: " + message);
  }

  private enum ReadMode
  {
    Normal,
    LineComment,
    BlockComment,
  }

  private class Field
  {
    public RuntimeFieldHandle FieldHandle;
    public object Args;

    public Field(RuntimeFieldHandle fieldHandle, object args)
    {
      FieldHandle = fieldHandle;
      Args = args;
    }
  }
}
