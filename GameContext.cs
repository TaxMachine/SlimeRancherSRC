// Decompiled with JetBrains decompiler
// Type: GameContext
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using RichPresence;
using SRML;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

public class GameContext : SRSingleton<GameContext>
{
  public MessageOfTheDayDirector MessageOfTheDayDirector;
  public SlimeDefinitions SlimeDefinitions;
  private const int LOG_MAX_CHARACTERS = 51200;
  private const int LOG_TRUNCATE_AMOUNT = 25600;
  private StringBuilder partialLogText = new StringBuilder();
  private const int MAX_FRAME_RATE = 120;

  public LookupDirector LookupDirector { get; private set; }

  public AutoSaveDirector AutoSaveDirector { get; private set; }

  public SlimeShaders SlimeShaders { get; private set; }

  public MessageDirector MessageDirector { get; private set; }

  public UITemplates UITemplates { get; private set; }

  public InputDirector InputDirector { get; private set; }

  public MusicDirector MusicDirector { get; private set; }

  public OptionsDirector OptionsDirector { get; private set; }

  public GifRecorder GifRecorder { get; private set; }

  public PerformanceTracker PerformanceTracker { get; private set; }

  public GalaxyDirector GalaxyDirector { get; private set; }

  public RailDirector RailDirector { get; private set; }

  public Director RichPresenceDirector { get; private set; }

  public DLCDirector DLCDirector { get; private set; }

  public RaycastBatcher RaycastBatcher { get; private set; }

  public ToyDirector ToyDirector { get; private set; }

  public string LogText => partialLogText.ToString();

  public override void Awake()
  {
    LoadSRModLoader();
    if (Instance == null)
    {
      base.Awake();
      LookupDirector = GetComponent<LookupDirector>();
      AutoSaveDirector = GetComponent<AutoSaveDirector>();
      SlimeShaders = GetComponent<SlimeShaders>();
      UITemplates = GetComponent<UITemplates>();
      InputDirector = GetComponent<InputDirector>();
      MessageDirector = GetComponent<MessageDirector>();
      MusicDirector = GetComponent<MusicDirector>();
      OptionsDirector = GetComponent<OptionsDirector>();
      GifRecorder = GetComponent<GifRecorder>();
      PerformanceTracker = GetComponent<PerformanceTracker>();
      GalaxyDirector = GetComponent<GalaxyDirector>();
      RailDirector = GetComponent<RailDirector>();
      RaycastBatcher = GetComponent<RaycastBatcher>();
      RichPresenceDirector = new Director();
      DLCDirector = new DLCDirector();
      ToyDirector = new ToyDirector();
      DontDestroyOnLoad(gameObject);
      Debug.Log(string.Format("Joystick Names: {0}", string.Join(",", Input.GetJoystickNames())));
      Application.logMessageReceived += LogReceived;
      Application.targetFrameRate = 120;
    }
    else
    {
      if (!(Instance != this))
        return;
      Destroyer.Destroy(gameObject, "GameContext.Awake");
    }
  }

  public void Start()
  {
    RichPresenceDirector.Register(SRSingleton<SystemContext>.Instance.GameCoreXboxContext);
    RichPresenceDirector.Register(SRSingleton<SystemContext>.Instance.UWPContext);
    RichPresenceDirector.Register(SRSingleton<SystemContext>.Instance.PS4Context);
    RichPresenceDirector.Register(Discord.RichPresenceHandler);
    DLCDirector.SetProvider(new SteamDLCProvider());
    MessageOfTheDayProvider provider = MessageOfTheDayDirector.GetProvider();
    if (!(provider is MessageOfTheDayLocalProvider))
      return;
    ((MessageOfTheDayLocalProvider) provider).SetDLCDirector(DLCDirector);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    Application.logMessageReceived -= LogReceived;
  }

  public void TakeGifScreenshot() => GifRecorder.MaybeSaveGif();

  public void TakeScreenshot() => TakeScreenshot(new TakeScreenshot_Params());

  public void TakeScreenshot(TakeScreenshot_Params args) => StartCoroutine(TakeScreenshotAsync(args));

  private IEnumerator TakeScreenshotAsync(TakeScreenshot_Params args)
  {
    string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), args.directory ?? string.Empty), args.name ?? string.Format("SlimeRancher-{0:yyyy-MM-dd-hh-mm-ss-ff}.png", DateTime.Now));
    Directory.CreateDirectory(Path.GetDirectoryName(path));
    File.Delete(path);
    yield return null;
    GameObject hudRoot = null;
    if (SRSingleton<HudUI>.Instance != null)
      hudRoot = SRSingleton<HudUI>.Instance.transform.parent.gameObject;
    if (hudRoot != null)
      hudRoot.SetActive(false);
    yield return new WaitForEndOfFrame();
    ScreenCapture.CaptureScreenshot(path);
    if (hudRoot != null)
      hudRoot.SetActive(true);
  }

  private void LogReceived(string message, string stacktrace, LogType type)
  {
    partialLogText.AppendLine(string.Format("{0} +0000;[{1}];{2}", DateTime.UtcNow.ToString(), type.ToString().ToUpperInvariant(), message));
    if (!string.IsNullOrEmpty(stacktrace))
    {
      stacktrace = stacktrace.Replace("\n", "\n\t");
      stacktrace = stacktrace.TrimEnd('\t');
      if (stacktrace != "" && type != LogType.Log)
        partialLogText.Append(string.Format("\t{0}", stacktrace));
    }
    if (!Truncate(partialLogText, 51200, 25600))
      return;
    partialLogText.Insert(0, string.Format("{0} +0000;[LOG];Truncated logs due to string size limit of approximately {1}KB\n", DateTime.UtcNow, 51200));
  }

  private bool Truncate(StringBuilder sb, int maxLength, int amount)
  {
    if (sb.Length <= maxLength)
      return false;
    sb.Remove(0, amount);
    return true;
  }

  public static void LoadSRModLoader()
  {
    try
    {
      foreach (string file in Directory.GetFiles("SRML/Libs", "*.dll", SearchOption.AllDirectories))
        Assembly.LoadFrom(file);
      Main.PreLoad();
    }
    catch (Exception ex)
    {
      Debug.Log(ex);
      Application.Quit();
    }
  }

  public struct TakeScreenshot_Params
  {
    public string directory;
    public string name;
  }
}
