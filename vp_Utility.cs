// Decompiled with JetBrains decompiler
// Type: vp_Utility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public static class vp_Utility
{
  private static bool usingGamepad;
  private static readonly Dictionary<Type, string> m_TypeAliases = new Dictionary<Type, string>()
  {
    {
      typeof (void),
      "void"
    },
    {
      typeof (byte),
      "byte"
    },
    {
      typeof (sbyte),
      "sbyte"
    },
    {
      typeof (short),
      "short"
    },
    {
      typeof (ushort),
      "ushort"
    },
    {
      typeof (int),
      "int"
    },
    {
      typeof (uint),
      "uint"
    },
    {
      typeof (long),
      "long"
    },
    {
      typeof (ulong),
      "ulong"
    },
    {
      typeof (float),
      "float"
    },
    {
      typeof (double),
      "double"
    },
    {
      typeof (Decimal),
      "decimal"
    },
    {
      typeof (object),
      "object"
    },
    {
      typeof (bool),
      "bool"
    },
    {
      typeof (char),
      "char"
    },
    {
      typeof (string),
      "string"
    },
    {
      typeof (Vector2),
      "Vector2"
    },
    {
      typeof (Vector3),
      "Vector3"
    },
    {
      typeof (Vector4),
      "Vector4"
    }
  };
  private static Dictionary<int, int> m_UniqueIDs = new Dictionary<int, int>();

  [Obsolete("Please use 'vp_MathUtility.NaNSafeFloat' instead.")]
  public static float NaNSafeFloat(float value, float prevValue = 0.0f) => vp_MathUtility.NaNSafeFloat(value, prevValue);

  [Obsolete("Please use 'vp_MathUtility.NaNSafeVector2' instead.")]
  public static Vector2 NaNSafeVector2(Vector2 vector, Vector2 prevVector = default (Vector2)) => vp_MathUtility.NaNSafeVector2(vector, prevVector);

  [Obsolete("Please use 'vp_MathUtility.NaNSafeVector3' instead.")]
  public static Vector3 NaNSafeVector3(Vector3 vector, Vector3 prevVector = default (Vector3)) => vp_MathUtility.NaNSafeVector3(vector, prevVector);

  [Obsolete("Please use 'vp_MathUtility.NaNSafeQuaternion' instead.")]
  public static Quaternion NaNSafeQuaternion(Quaternion quaternion, Quaternion prevQuaternion = default (Quaternion)) => vp_MathUtility.NaNSafeQuaternion(quaternion, prevQuaternion);

  [Obsolete("Please use 'vp_MathUtility.SnapToZero' instead.")]
  public static Vector3 SnapToZero(Vector3 value, float epsilon = 0.0001f) => vp_MathUtility.SnapToZero(value, epsilon);

  [Obsolete("Please use 'vp_MathUtility.SnapToZero' instead.")]
  public static float SnapToZero(float value, float epsilon = 0.0001f) => vp_MathUtility.SnapToZero(value, epsilon);

  [Obsolete("Please use 'vp_MathUtility.ReduceDecimals' instead.")]
  public static float ReduceDecimals(float value, float factor = 1000f) => vp_MathUtility.ReduceDecimals(value, factor);

  [Obsolete("Please use 'vp_3DUtility.HorizontalVector' instead.")]
  public static Vector3 HorizontalVector(Vector3 value) => vp_3DUtility.HorizontalVector(value);

  public static string GetErrorLocation(int level = 1, bool showOnlyLast = false)
  {
    StackTrace stackTrace = new StackTrace();
    string errorLocation = "";
    string str = "";
    for (int index = stackTrace.FrameCount - 1; index > level; --index)
    {
      if (index < stackTrace.FrameCount - 1)
        errorLocation += " --> ";
      StackFrame frame = stackTrace.GetFrame(index);
      if (frame.GetMethod().DeclaringType.ToString() == str)
        errorLocation = "";
      str = frame.GetMethod().DeclaringType.ToString();
      errorLocation = errorLocation + str + ":" + frame.GetMethod().Name;
    }
    if (showOnlyLast)
    {
      try
      {
        errorLocation = errorLocation.Substring(errorLocation.LastIndexOf(" --> "));
        errorLocation = errorLocation.Replace(" --> ", "");
      }
      catch
      {
      }
    }
    return errorLocation;
  }

  public static string GetTypeAlias(Type type)
  {
    string str = "";
    return !m_TypeAliases.TryGetValue(type, out str) ? type.ToString() : str;
  }

  public static void Activate(GameObject obj, bool activate = true) => obj.SetActive(activate);

  public static bool IsActive(GameObject obj) => obj.activeSelf;

  public static bool LockCursor
  {
    get => Cursor.lockState == CursorLockMode.Locked;
    set
    {
      Cursor.visible = !value && !usingGamepad;
      Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
  }

  public static void SetUsingGamepad(bool usingGamepad)
  {
    vp_Utility.usingGamepad = usingGamepad;
    LockCursor = LockCursor;
  }

  public static void RandomizeList<T>(this List<T> list)
  {
    int count = list.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      int index2 = UnityEngine.Random.Range(index1, count);
      T obj = list[index1];
      list[index1] = list[index2];
      list[index2] = obj;
    }
  }

  public static T RandomObject<T>(this List<T> list)
  {
    List<T> objList = new List<T>();
    objList.AddRange(list);
    objList.RandomizeList();
    return objList.FirstOrDefault();
  }

  public static List<T> ChildComponentsToList<T>(this Transform t) where T : Component => t.GetComponentsInChildren<T>().ToList();

  public static bool IsDescendant(Transform descendant, Transform potentialAncestor)
  {
    if (descendant == null || potentialAncestor == null || descendant.parent == descendant)
      return false;
    return descendant.parent == potentialAncestor || IsDescendant(descendant.parent, potentialAncestor);
  }

  public static Component GetParent(Component target)
  {
    if (target == null)
      return null;
    return target != target.transform ? target.transform : (Component) target.transform.parent;
  }

  public static Transform GetTransformByNameInChildren(
    Transform trans,
    string name,
    bool includeInactive = false,
    bool subString = false)
  {
    name = name.ToLower();
    foreach (Transform tran in trans)
    {
      if (!subString)
      {
        if (tran.name.ToLower() == name && (includeInactive || tran.gameObject.activeInHierarchy))
          return tran;
      }
      else if (tran.name.ToLower().Contains(name) && (includeInactive || tran.gameObject.activeInHierarchy))
        return tran;
      Transform byNameInChildren = GetTransformByNameInChildren(tran, name, includeInactive, subString);
      if (byNameInChildren != null)
        return byNameInChildren;
    }
    return null;
  }

  public static Transform GetTransformByNameInAncestors(
    Transform trans,
    string name,
    bool includeInactive = false,
    bool subString = false)
  {
    if (trans.parent == null)
      return null;
    name = name.ToLower();
    if (!subString)
    {
      if (trans.parent.name.ToLower() == name && (includeInactive || trans.gameObject.activeInHierarchy))
        return trans.parent;
    }
    else if (trans.parent.name.ToLower().Contains(name) && (includeInactive || trans.gameObject.activeInHierarchy))
      return trans.parent;
    Transform byNameInAncestors = GetTransformByNameInAncestors(trans.parent, name, includeInactive, subString);
    return byNameInAncestors != null ? byNameInAncestors : null;
  }

  public static UnityEngine.Object Instantiate(UnityEngine.Object original) => Instantiate(original, Vector3.zero, Quaternion.identity);

  public static UnityEngine.Object Instantiate(
    UnityEngine.Object original,
    Vector3 position,
    Quaternion rotation)
  {
    return vp_PoolManager.Instance == null || !vp_PoolManager.Instance.enabled ? UnityEngine.Object.Instantiate(original, position, rotation) : vp_GlobalEventReturn<UnityEngine.Object, Vector3, Quaternion, UnityEngine.Object>.Send("vp_PoolManager Instantiate", original, position, rotation);
  }

  public static void Destroy(UnityEngine.Object obj) => Destroy(obj, 0.0f);

  public static void Destroy(UnityEngine.Object obj, float t)
  {
    if (vp_PoolManager.Instance == null || !vp_PoolManager.Instance.enabled)
      Destroyer.Destroy(obj, t, "vp_Utility.Destroy");
    else
      vp_GlobalEvent<UnityEngine.Object, float>.Send("vp_PoolManager Destroy", obj, t);
  }

  public static int UniqueID
  {
    get
    {
      int key;
      while (true)
      {
        do
        {
          key = UnityEngine.Random.Range(0, 1000000000);
          if (!m_UniqueIDs.ContainsKey(key))
            goto label_3;
        }
        while (m_UniqueIDs.Count < 1000000000);
        ClearUniqueIDs();
        UnityEngine.Debug.LogWarning("Warning (vp_Utility.UniqueID) More than 1 billion unique IDs have been generated. This seems like an awful lot for a game client. Clearing dictionary and starting over!");
      }
label_3:
      m_UniqueIDs.Add(key, 0);
      return key;
    }
  }

  public static void ClearUniqueIDs() => m_UniqueIDs.Clear();

  public static int PositionToID(Vector3 position) => (int) Mathf.Abs((float) (position.x * 10000.0 + position.y * 1000.0 + position.z * 100.0));

  [Obsolete("Please use 'vp_AudioUtility.PlayRandomSound' instead.")]
  public static void PlayRandomSound(
    AudioSource audioSource,
    List<AudioClip> sounds,
    Vector2 pitchRange)
  {
    vp_AudioUtility.PlayRandomSound(audioSource, sounds, pitchRange);
  }

  [Obsolete("Please use 'vp_AudioUtility.PlayRandomSound' instead.")]
  public static void PlayRandomSound(AudioSource audioSource, List<AudioClip> sounds) => vp_AudioUtility.PlayRandomSound(audioSource, sounds);
}
