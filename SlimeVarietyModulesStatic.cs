// Decompiled with JetBrains decompiler
// Type: SlimeVarietyModulesStatic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class SlimeVarietyModulesStatic
{
  private static readonly string[] SKIP_PROP_NAMES = new string[3]
  {
    "sleepAngularVelocity",
    "sleepVelocity",
    "useConeFriction"
  };

  public static T GetCopyOf<T>(this Component copyInto, T copyFrom) where T : Component
  {
    Type type = copyInto.GetType();
    if (type != copyFrom.GetType())
      return default (T);
    for (; type != typeof (Component) && type != null; type = type.BaseType)
    {
      BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      foreach (PropertyInfo property in type.GetProperties(bindingAttr))
      {
        if (!SKIP_PROP_NAMES.Contains(property.Name) && property.CanWrite && property.CanRead)
        {
          if (property.Name != "material")
          {
            try
            {
              property.SetValue(copyInto, property.GetValue(copyFrom, null), null);
            }
            catch (Exception ex)
            {
              Log.Error("ZOMG! Cannot set property when copying component. " + property + " err: " + ex);
            }
          }
        }
      }
      foreach (FieldInfo field in type.GetFields(bindingAttr))
      {
        if (field.GetCustomAttributes(typeof (NonSerializedAttribute), true).Length == 0 && (field.IsPublic || field.GetCustomAttributes(typeof (SerializeField), true).Length != 0))
        {
          if (field.FieldType.IsValueType)
            field.SetValue(copyInto, field.GetValue(copyFrom));
          else if (field.FieldType.IsSerializable)
            field.SetValue(copyInto, ObjectCopier.Clone(field.GetValue(copyFrom)));
          else
            field.SetValue(copyInto, field.GetValue(copyFrom));
        }
      }
    }
    return copyInto as T;
  }
}
