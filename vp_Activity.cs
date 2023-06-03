// Decompiled with JetBrains decompiler
// Type: vp_Activity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class vp_Activity : vp_Event
{
  public Callback StartCallbacks;
  public Callback StopCallbacks;
  public Condition StartConditions;
  public Condition StopConditions;
  public Callback FailStartCallbacks;
  public Callback FailStopCallbacks;
  protected vp_Timer.Handle m_ForceStopTimer = new vp_Timer.Handle();
  protected object m_Argument;
  protected bool m_Active;
  public float NextAllowedStartTime;
  public float NextAllowedStopTime;
  private float m_MinPause;
  private float m_MinDuration;
  private float m_MaxDuration = -1f;

  protected static void Empty()
  {
  }

  protected static bool AlwaysOK() => true;

  public vp_Activity(string name)
    : base(name)
  {
    EventType = vp_EventType.Activity;
    InitFields();
  }

  public float MinPause
  {
    get => m_MinPause;
    set => m_MinPause = Mathf.Max(0.0f, value);
  }

  public float MinDuration
  {
    get => m_MinDuration;
    set
    {
      m_MinDuration = Mathf.Max(1f / 1000f, value);
      if (m_MaxDuration == -1.0 || m_MinDuration <= (double) m_MaxDuration)
        return;
      m_MinDuration = m_MaxDuration;
      Debug.LogWarning("Warning: (vp_Activity) Tried to set MinDuration longer than MaxDuration for '" + EventName + "'. Capping at MaxDuration.");
    }
  }

  public float AutoDuration
  {
    get => m_MaxDuration;
    set
    {
      if (value == -1.0)
      {
        m_MaxDuration = value;
      }
      else
      {
        m_MaxDuration = Mathf.Max(1f / 1000f, value);
        if (m_MaxDuration >= (double) m_MinDuration)
          return;
        m_MaxDuration = m_MinDuration;
        Debug.LogWarning("Warning: (vp_Activity) Tried to set MaxDuration shorter than MinDuration for '" + EventName + "'. Capping at MinDuration.");
      }
    }
  }

  public object Argument
  {
    get
    {
      if (!(m_ArgumentType == null))
        return m_Argument;
      Debug.LogError("Error: (" + this + ") Tried to fetch argument from '" + EventName + "' but this activity takes no parameters.");
      return null;
    }
    set
    {
      if (m_ArgumentType == null)
        Debug.LogError("Error: (" + this + ") Tried to set argument for '" + EventName + "' but this activity takes no parameters.");
      else
        m_Argument = value;
    }
  }

  protected override void InitFields()
  {
    m_DelegateTypes = new System.Type[6]
    {
      typeof (Callback),
      typeof (Callback),
      typeof (Condition),
      typeof (Condition),
      typeof (Callback),
      typeof (Callback)
    };
    m_Fields = new FieldInfo[6]
    {
      GetType().GetField("StartCallbacks"),
      GetType().GetField("StopCallbacks"),
      GetType().GetField("StartConditions"),
      GetType().GetField("StopConditions"),
      GetType().GetField("FailStartCallbacks"),
      GetType().GetField("FailStopCallbacks")
    };
    StoreInvokerFieldNames();
    m_DefaultMethods = new MethodInfo[6]
    {
      GetType().GetMethod("Empty"),
      GetType().GetMethod("Empty"),
      GetType().GetMethod("AlwaysOK"),
      GetType().GetMethod("AlwaysOK"),
      GetType().GetMethod("Empty"),
      GetType().GetMethod("Empty")
    };
    Prefixes = new Dictionary<string, int>()
    {
      {
        "OnStart_",
        0
      },
      {
        "OnStop_",
        1
      },
      {
        "CanStart_",
        2
      },
      {
        "CanStop_",
        3
      },
      {
        "OnFailStart_",
        4
      },
      {
        "OnFailStop_",
        5
      }
    };
    StartCallbacks = Empty;
    StopCallbacks = Empty;
    StartConditions = AlwaysOK;
    StopConditions = AlwaysOK;
    FailStartCallbacks = Empty;
    FailStopCallbacks = Empty;
  }

  public bool TryStart(bool startIfAllowed = true)
  {
    if (m_Active)
      return false;
    if (Time.time < (double) NextAllowedStartTime)
    {
      m_Argument = null;
      return false;
    }
    foreach (Condition invocation in StartConditions.GetInvocationList())
    {
      if (!invocation())
      {
        m_Argument = null;
        if (startIfAllowed)
          FailStartCallbacks();
        return false;
      }
    }
    if (startIfAllowed)
      Active = true;
    return true;
  }

  public bool TryStop(bool stopIfAllowed = true)
  {
    if (!m_Active || Time.time < (double) NextAllowedStopTime)
      return false;
    foreach (Condition invocation in StopConditions.GetInvocationList())
    {
      if (!invocation())
      {
        if (stopIfAllowed)
          FailStopCallbacks();
        return false;
      }
    }
    if (stopIfAllowed)
      Active = false;
    return true;
  }

  public bool Active
  {
    set
    {
      if (value && !m_Active)
      {
        m_Active = true;
        StartCallbacks();
        NextAllowedStopTime = Time.time + m_MinDuration;
        if (m_MaxDuration <= 0.0)
          return;
        vp_Timer.In(m_MaxDuration, () => Stop(), m_ForceStopTimer);
      }
      else
      {
        if (value || !m_Active)
          return;
        m_Active = false;
        StopCallbacks();
        NextAllowedStartTime = Time.time + m_MinPause;
        m_Argument = null;
      }
    }
    get => m_Active;
  }

  public void Start(float forcedActiveDuration = 0.0f)
  {
    Active = true;
    if (forcedActiveDuration <= 0.0)
      return;
    NextAllowedStopTime = Time.time + forcedActiveDuration;
  }

  public void Stop(float forcedPauseDuration = 0.0f)
  {
    Active = false;
    if (forcedPauseDuration <= 0.0)
      return;
    NextAllowedStartTime = Time.time + forcedPauseDuration;
  }

  public void Disallow(float duration) => NextAllowedStartTime = Time.time + duration;

  public delegate void Callback();

  public delegate bool Condition();
}
