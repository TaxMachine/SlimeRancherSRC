// Decompiled with JetBrains decompiler
// Type: SentrySdk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Sentry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SentrySdk : MonoBehaviour
{
  public static readonly string SESSION_USER_ID = Guid.NewGuid().ToString();
  private float _timeLastError;
  private const float MinTime = 10f;
  private Breadcrumb[] _breadcrumbs;
  private int _lastBreadcrumbPos;
  private int _noBreadcrumbs;
  [Header("Sentry Authentication")]
  public string Dsn;
  [Header("Debug Settings")]
  public bool showDebugMessagesInLog = true;
  [Tooltip("Enable to log exceptions from the editor.")]
  public bool postExceptionsFromEditor;
  private Dsn _dsn;
  private bool _initialized;
  private readonly HashSet<string> previouslySentErrors = new HashSet<string>();
  private static SentrySdk _instance;

  public void Start()
  {
    if (Dsn == string.Empty)
      Debug.LogWarning("No DSN defined. The Sentry SDK will be disabled.");
    else if (_instance == null)
    {
      try
      {
        _dsn = new Dsn(Dsn);
      }
      catch (Exception ex)
      {
        Debug.LogError(string.Format("Error parsing DSN: {0}", ex.Message));
        return;
      }
      _breadcrumbs = new Breadcrumb[100];
      DontDestroyOnLoad(this);
      _instance = this;
      _initialized = true;
    }
    else
      Destroy(this);
  }

  public static void AddBreadcrumb(string message)
  {
    if (_instance == null)
      return;
    _instance.DoAddBreadcrumb(message);
  }

  public static void CaptureMessage(string message)
  {
    if (_instance == null)
      return;
    _instance.DoCaptureMessage(message);
  }

  public static void CaptureEvent(
    SentryEvent @event,
    OnCaptureCompleteDelegate onCaptureComplete = null)
  {
    if (_instance == null)
      return;
    _instance.DoCaptureEvent(@event, onCaptureComplete);
  }

  public static void CaptureFeedback(
    string summary,
    string description,
    OnCaptureCompleteDelegate onCaptureComplete = null)
  {
    if (_instance == null)
      return;
    _instance.DoCaptureFeedback(summary, description, onCaptureComplete);
  }

  private void DoCaptureFeedback(
    string summary,
    string description,
    OnCaptureCompleteDelegate onCaptureComplete = null)
  {
    StartCoroutine(ContinueSendingEvent(new SentryEvent(summary + "\n\n" + description, GetBreadcrumbs())
    {
      tags = {
        isUserFeedback = true
      },
      level = "info"
    }, onCaptureComplete));
  }

  private void DoCaptureMessage(
    string message,
    OnCaptureCompleteDelegate onCaptureComplete = null)
  {
    if (showDebugMessagesInLog)
      Debug.Log("sending message to sentry.");
    DoCaptureEvent(new SentryEvent(message, GetBreadcrumbs())
    {
      level = "info"
    }, onCaptureComplete);
  }

  private void DoCaptureEvent(
    SentryEvent @event,
    OnCaptureCompleteDelegate onCaptureComplete)
  {
    if (showDebugMessagesInLog)
      Debug.Log("sending event to sentry.");
    StartCoroutine(ContinueSendingEvent(@event, onCaptureComplete));
  }

  private void DoAddBreadcrumb(string message)
  {
    if (!_initialized)
    {
      Debug.LogError("Cannot AddBreadcrumb if we are not initialized");
    }
    else
    {
      _breadcrumbs[_lastBreadcrumbPos] = new Breadcrumb(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss"), message);
      ++_lastBreadcrumbPos;
      _lastBreadcrumbPos %= 100;
      if (_noBreadcrumbs >= 100)
        return;
      ++_noBreadcrumbs;
    }
  }

  private List<Breadcrumb> GetBreadcrumbs() => Breadcrumb.CombineBreadcrumbs(_breadcrumbs, _lastBreadcrumbPos, _noBreadcrumbs);

  public void OnEnable() => Application.logMessageReceived += OnLogMessageReceived;

  public void OnDisable() => Application.logMessageReceived -= OnLogMessageReceived;

  public void ScheduleException(string condition, string stackTrace)
  {
    if (showDebugMessagesInLog)
      Debug.Log("sending exception to sentry.");
    List<StackTraceSpec> stackTrace1 = new List<StackTraceSpec>();
    string[] strArray = condition.Split(new char[1]{ ':' }, 2);
    string exceptionType = strArray[0];
    string exceptionValue = strArray.Length > 1 ? strArray[1].Trim() : strArray[0];
    foreach (StackTraceSpec stackTrace2 in GetStackTraces(stackTrace))
      stackTrace1.Add(stackTrace2);
    StartCoroutine(ContinueSendingEvent(new SentryExceptionEvent(exceptionType, exceptionValue, GetBreadcrumbs(), stackTrace1)));
  }

  private static IEnumerable<StackTraceSpec> GetStackTraces(string stackTrace)
  {
    string[] stackList = stackTrace.Split('\n');
    for (int i = stackList.Length - 1; i >= 0; --i)
    {
      string str = stackList[i];
      if (!(str == string.Empty))
      {
        int num1 = str.IndexOf(')');
        if (num1 != -1)
        {
          string function;
          string filename;
          int lineNo;
          try
          {
            function = str.Substring(0, num1 + 1);
            if (str.Length < num1 + 6)
            {
              filename = string.Empty;
              lineNo = -1;
            }
            else if (str.Substring(num1 + 1, 5) != " (at ")
            {
              Debug.Log("failed parsing " + str);
              function = str;
              lineNo = -1;
              filename = string.Empty;
            }
            else
            {
              int num2 = str.LastIndexOf(':', str.Length - 1, str.Length - num1);
              if (num1 == str.Length - 1)
              {
                filename = string.Empty;
                lineNo = -1;
              }
              else if (num2 == -1)
              {
                filename = str.Substring(num1 + 6, str.Length - num1 - 7);
                lineNo = -1;
              }
              else
              {
                filename = str.Substring(num1 + 6, num2 - num1 - 6);
                lineNo = Convert.ToInt32(str.Substring(num2 + 1, str.Length - 2 - num2));
              }
            }
          }
          catch
          {
            continue;
          }
          bool inApp;
          if (filename == string.Empty || filename[0] == '<' && filename[filename.Length - 1] == '>')
          {
            filename = string.Empty;
            inApp = true;
            if (function.Contains("UnityEngine."))
              inApp = false;
          }
          else
            inApp = filename.Contains("Assets/");
          yield return new StackTraceSpec(filename, function, lineNo, inApp);
        }
      }
    }
  }

  public void OnLogMessageReceived(string condition, string stackTrace, LogType type)
  {
    if (!_initialized || type != LogType.Error && type != LogType.Exception && type != LogType.Assert || previouslySentErrors.Contains(condition) || Time.time - (double) _timeLastError <= 10.0)
      return;
    _timeLastError = Time.time;
    previouslySentErrors.Add(condition);
    ScheduleException(condition, stackTrace);
  }

  private IEnumerator ContinueSendingEvent<T>(
    T @event,
    OnCaptureCompleteDelegate onCaptureComplete = null)
    where T : SentryEvent
  {
    yield return new WaitForSecondsRealtime(5f);
    string json = JsonUtility.ToJson(@event);
    string publicKey = _dsn.publicKey;
    string secretKey = _dsn.secretKey;
    string str = "Sentry sentry_version=5,sentry_client=Unity0.1,sentry_timestamp=" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss") + ",sentry_key=" + publicKey + ",sentry_secret=" + secretKey;
    UnityWebRequest www = new UnityWebRequest(_dsn.callUri.ToString());
    www.method = "POST";
    www.SetRequestHeader("X-Sentry-Auth", str);
    www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
    www.downloadHandler = new DownloadHandlerBuffer();
    yield return www.SendWebRequest();
    while (!www.isDone)
      yield return null;
    if (www.isNetworkError || www.isHttpError || www.responseCode != 200L)
    {
      Debug.LogWarning("error sending request to sentry: " + www.error);
      OnCaptureCompleteDelegate completeDelegate = onCaptureComplete;
      if (completeDelegate != null)
        completeDelegate(new Exception(www.error));
    }
    else if (showDebugMessagesInLog)
    {
      Debug.Log("Sentry sent back: " + www.downloadHandler.text);
      OnCaptureCompleteDelegate completeDelegate = onCaptureComplete;
      if (completeDelegate != null)
        completeDelegate(null);
    }
  }

  public delegate void OnCaptureCompleteDelegate(Exception exception);
}
