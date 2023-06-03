// Decompiled with JetBrains decompiler
// Type: SystemContext
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.Controller;
using System.Globalization;
using UnityEngine;

public class SystemContext : SRSingleton<SystemContext>
{
  public static bool IsModded;
  public IDateProvider DateProvider = new StandardDateProvider();

  public GameCoreXboxContext GameCoreXboxContext { get; private set; }

  public UWPContext UWPContext { get; private set; }

  public PS4Context PS4Context { get; private set; }

  public RumbleHandler RumbleHandler { get; private set; }

  public override void Awake()
  {
    if (Instance == null)
    {
      base.Awake();
      if (Application.unityVersion == "_never_POSSIBLE_")
      {
        ChineseLunisolarCalendar lunisolarCalendar1 = new ChineseLunisolarCalendar();
        GregorianCalendar gregorianCalendar = new GregorianCalendar();
        HebrewCalendar hebrewCalendar = new HebrewCalendar();
        HijriCalendar hijriCalendar = new HijriCalendar();
        JapaneseCalendar japaneseCalendar = new JapaneseCalendar();
        JapaneseLunisolarCalendar lunisolarCalendar2 = new JapaneseLunisolarCalendar();
        JulianCalendar julianCalendar = new JulianCalendar();
        KoreanCalendar koreanCalendar = new KoreanCalendar();
        KoreanLunisolarCalendar lunisolarCalendar3 = new KoreanLunisolarCalendar();
        PersianCalendar persianCalendar = new PersianCalendar();
        TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
        TaiwanLunisolarCalendar lunisolarCalendar4 = new TaiwanLunisolarCalendar();
        ThaiBuddhistCalendar buddhistCalendar = new ThaiBuddhistCalendar();
        UmAlQuraCalendar umAlQuraCalendar = new UmAlQuraCalendar();
      }
      GameCoreXboxContext = GetComponent<GameCoreXboxContext>();
      UWPContext = GetComponent<UWPContext>();
      PS4Context = GetComponent<PS4Context>();
      DontDestroyOnLoad(gameObject);
      RumbleHandler = new EmptyRumbleHandler();
    }
    else
    {
      if (!(Instance != this))
        return;
      Destroyer.Destroy(gameObject, "SystemContext.Awake");
    }
  }
}
