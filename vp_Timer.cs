// Decompiled with JetBrains decompiler
// Type: vp_Timer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class vp_Timer : MonoBehaviour
{
  private static GameObject m_MainObject = null;
  private static List<Event> m_Active = new List<Event>();
  private static List<Event> m_Pool = new List<Event>();
  private static Event m_NewEvent = null;
  private static int m_EventCount = 0;
  private static int m_EventBatch = 0;
  private static int m_EventIterator = 0;
  public static int MaxEventsPerFrame = 500;

  public bool WasAddedCorrectly => Application.isPlaying && !(gameObject != m_MainObject);

  private void Awake()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
    if (WasAddedCorrectly)
      return;
    Destroyer.Destroy(this, "vp_Timer.Awake");
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    for (int index = m_Active.Count - 1; index > -1; --index)
    {
      if (m_Active[index].CancelOnLoad)
        m_Active[index].Id = 0;
    }
  }

  private void Update()
  {
    for (m_EventBatch = 0; m_Active.Count > 0 && m_EventBatch < MaxEventsPerFrame; ++m_EventBatch)
    {
      if (m_EventIterator < 0)
      {
        m_EventIterator = m_Active.Count - 1;
        break;
      }
      if (m_EventIterator > m_Active.Count - 1)
        m_EventIterator = m_Active.Count - 1;
      if (Time.time >= (double) m_Active[m_EventIterator].DueTime || m_Active[m_EventIterator].Id == 0)
        m_Active[m_EventIterator].Execute();
      else if (m_Active[m_EventIterator].Paused)
        m_Active[m_EventIterator].DueTime += Time.deltaTime;
      else
        m_Active[m_EventIterator].LifeTime += Time.deltaTime;
      --m_EventIterator;
    }
  }

  public static void In(float delay, Callback callback, Handle timerHandle = null) => Schedule(delay, callback, null, null, timerHandle, 1, -1f);

  public static void In(
    float delay,
    Callback callback,
    int iterations,
    Handle timerHandle = null)
  {
    Schedule(delay, callback, null, null, timerHandle, iterations, -1f);
  }

  public static void In(
    float delay,
    Callback callback,
    int iterations,
    float interval,
    Handle timerHandle = null)
  {
    Schedule(delay, callback, null, null, timerHandle, iterations, interval);
  }

  public static void In(
    float delay,
    ArgCallback callback,
    object arguments,
    Handle timerHandle = null)
  {
    Schedule(delay, null, callback, arguments, timerHandle, 1, -1f);
  }

  public static void In(
    float delay,
    ArgCallback callback,
    object arguments,
    int iterations,
    Handle timerHandle = null)
  {
    Schedule(delay, null, callback, arguments, timerHandle, iterations, -1f);
  }

  public static void In(
    float delay,
    ArgCallback callback,
    object arguments,
    int iterations,
    float interval,
    Handle timerHandle = null)
  {
    Schedule(delay, null, callback, arguments, timerHandle, iterations, interval);
  }

  public static void Start(Handle timerHandle) => Schedule(3.1536E+08f, () => { }, null, null, timerHandle, 1, -1f);

  private static void Schedule(
    float time,
    Callback func,
    ArgCallback argFunc,
    object args,
    Handle timerHandle,
    int iterations,
    float interval)
  {
    if (func == null && argFunc == null)
    {
      Debug.LogError("Error: (vp_Timer) Aborted event because function is null.");
    }
    else
    {
      if (m_MainObject == null)
      {
        m_MainObject = new GameObject("Timers");
        m_MainObject.AddComponent<vp_Timer>();
        DontDestroyOnLoad(m_MainObject);
      }
      time = Mathf.Max(0.0f, time);
      iterations = Mathf.Max(0, iterations);
      interval = interval == -1.0 ? time : Mathf.Max(0.0f, interval);
      m_NewEvent = null;
      if (m_Pool.Count > 0)
      {
        m_NewEvent = m_Pool[0];
        m_Pool.Remove(m_NewEvent);
      }
      else
        m_NewEvent = new Event();
      ++m_EventCount;
      m_NewEvent.Id = m_EventCount;
      if (func != null)
        m_NewEvent.Function = func;
      else if (argFunc != null)
      {
        m_NewEvent.ArgFunction = argFunc;
        m_NewEvent.Arguments = args;
      }
      m_NewEvent.StartTime = Time.time;
      m_NewEvent.DueTime = Time.time + time;
      m_NewEvent.Iterations = iterations;
      m_NewEvent.Interval = interval;
      m_NewEvent.LifeTime = 0.0f;
      m_NewEvent.Paused = false;
      m_Active.Add(m_NewEvent);
      if (timerHandle == null)
        return;
      if (timerHandle.Active)
        timerHandle.Cancel();
      timerHandle.Id = m_NewEvent.Id;
    }
  }

  private static void Cancel(Handle handle)
  {
    if (handle == null || !handle.Active)
      return;
    handle.Id = 0;
  }

  public static void CancelAll()
  {
    for (int index = m_Active.Count - 1; index > -1; --index)
      m_Active[index].Id = 0;
  }

  public static void CancelAll(string methodName)
  {
    for (int index = m_Active.Count - 1; index > -1; --index)
    {
      if (m_Active[index].MethodName == methodName)
        m_Active[index].Id = 0;
    }
  }

  public static void DestroyAll()
  {
    m_Active.Clear();
    m_Pool.Clear();
  }

  public static Stats EditorGetStats()
  {
    Stats stats;
    stats.Created = m_Active.Count + m_Pool.Count;
    stats.Inactive = m_Pool.Count;
    stats.Active = m_Active.Count;
    return stats;
  }

  public static string EditorGetMethodInfo(int eventIndex) => eventIndex < 0 || eventIndex > m_Active.Count - 1 ? "Argument out of range." : m_Active[eventIndex].MethodInfo;

  public static int EditorGetMethodId(int eventIndex) => eventIndex < 0 || eventIndex > m_Active.Count - 1 ? 0 : m_Active[eventIndex].Id;

  public delegate void Callback();

  public delegate void ArgCallback(object args);

  public struct Stats
  {
    public int Created;
    public int Inactive;
    public int Active;
  }

  private class Event
  {
    public int Id;
    public Callback Function;
    public ArgCallback ArgFunction;
    public object Arguments;
    public int Iterations = 1;
    public float Interval = -1f;
    public float DueTime;
    public float StartTime;
    public float LifeTime;
    public bool Paused;
    public bool CancelOnLoad = true;

    public void Execute()
    {
      if (Id == 0 || DueTime == 0.0)
      {
        Recycle();
      }
      else
      {
        if (Function != null)
          Function();
        else if (ArgFunction != null)
        {
          ArgFunction(Arguments);
        }
        else
        {
          Error("Aborted event because function is null.");
          Recycle();
          return;
        }
        if (Iterations > 0)
        {
          --Iterations;
          if (Iterations < 1)
          {
            Recycle();
            return;
          }
        }
        DueTime = Time.time + Interval;
      }
    }

    private void Recycle()
    {
      Id = 0;
      DueTime = 0.0f;
      StartTime = 0.0f;
      CancelOnLoad = true;
      Function = null;
      ArgFunction = null;
      Arguments = null;
      if (!m_Active.Remove(this))
        return;
      m_Pool.Add(this);
    }

    private void Destroy()
    {
      m_Active.Remove(this);
      m_Pool.Remove(this);
    }

    private void Error(string message) => Debug.LogError("Error: (" + this + ") " + message);

    public string MethodName
    {
      get
      {
        if (Function != null)
        {
          if (Function.Method != null)
            return Function.Method.Name[0] == '<' ? "delegate" : Function.Method.Name;
        }
        else if (ArgFunction != null && ArgFunction.Method != null)
          return ArgFunction.Method.Name[0] == '<' ? "delegate" : ArgFunction.Method.Name;
        return null;
      }
    }

    public string MethodInfo
    {
      get
      {
        string methodName = MethodName;
        string methodInfo;
        if (!string.IsNullOrEmpty(methodName))
        {
          string str = methodName + "(";
          if (Arguments != null)
          {
            if (Arguments.GetType().IsArray)
            {
              object[] arguments = (object[]) Arguments;
              foreach (object obj in arguments)
              {
                str += obj.ToString();
                if (Array.IndexOf(arguments, obj) < arguments.Length - 1)
                  str += ", ";
              }
            }
            else
              str += (string) Arguments;
          }
          methodInfo = str + ")";
        }
        else
          methodInfo = "(function = null)";
        return methodInfo;
      }
    }
  }

  public class Handle
  {
    private Event m_Event;
    private int m_Id;
    private int m_StartIterations = 1;
    private float m_FirstDueTime;

    public bool Paused
    {
      get => Active && m_Event.Paused;
      set
      {
        if (!Active)
          return;
        m_Event.Paused = value;
      }
    }

    public float TimeOfInitiation => Active ? m_Event.StartTime : 0.0f;

    public float TimeOfFirstIteration => Active ? m_FirstDueTime : 0.0f;

    public float TimeOfNextIteration => Active ? m_Event.DueTime : 0.0f;

    public float TimeOfLastIteration => Active ? Time.time + DurationLeft : 0.0f;

    public float Delay => Mathf.Round((float) ((m_FirstDueTime - (double) TimeOfInitiation) * 1000.0)) / 1000f;

    public float Interval => Active ? m_Event.Interval : 0.0f;

    public float TimeUntilNextIteration => Active ? m_Event.DueTime - Time.time : 0.0f;

    public float DurationLeft => Active ? TimeUntilNextIteration + (m_Event.Iterations - 1) * m_Event.Interval : 0.0f;

    public float DurationTotal => Active ? Delay + m_StartIterations * (m_StartIterations > 1 ? Interval : 0.0f) : 0.0f;

    public float Duration => Active ? m_Event.LifeTime : 0.0f;

    public int IterationsTotal => m_StartIterations;

    public int IterationsLeft => Active ? m_Event.Iterations : 0;

    public int Id
    {
      get => m_Id;
      set
      {
        m_Id = value;
        if (m_Id == 0)
        {
          m_Event.DueTime = 0.0f;
        }
        else
        {
          m_Event = null;
          for (int index = m_Active.Count - 1; index > -1; --index)
          {
            if (m_Active[index].Id == m_Id)
            {
              m_Event = m_Active[index];
              break;
            }
          }
          if (m_Event == null)
            Debug.LogError("Error: (" + this + ") Failed to assign event with Id '" + m_Id + "'.");
          m_StartIterations = m_Event.Iterations;
          m_FirstDueTime = m_Event.DueTime;
        }
      }
    }

    public bool Active => m_Event != null && Id != 0 && m_Event.Id != 0 && m_Event.Id == Id;

    public string MethodName => Active ? m_Event.MethodName : "";

    public string MethodInfo => Active ? m_Event.MethodInfo : "";

    public bool CancelOnLoad
    {
      get => !Active || m_Event.CancelOnLoad;
      set
      {
        if (Active)
          m_Event.CancelOnLoad = value;
        else
          Debug.LogWarning("Warning: (" + this + ") Tried to set CancelOnLoad on inactive timer handle.");
      }
    }

    public void Cancel() => vp_Timer.Cancel(this);

    public void Execute() => m_Event.DueTime = Time.time;
  }
}
