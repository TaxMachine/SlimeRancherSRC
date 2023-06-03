// Decompiled with JetBrains decompiler
// Type: Levels
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Levels
{
  public const string COMPANY_LOGO = "CompanyLogoScene";
  public const string STANDALONE_START = "StandaloneStart";
  public const string MAIN_MENU = "MainMenu";
  public const string XBOX_ONE_START = "XboxOneStart";
  public const string UWP_START = "UWPStart";
  public const string PS4_START = "PS4Start";
  public const string GAMECORE_XBOX_START = "GameCoreXboxStart";
  public const string WORLD = "worldGenerated";
  private static bool isActiveSceneSpecial = true;
  private static HashSet<string> specialScenes = new HashSet<string>()
  {
    "CompanyLogoScene",
    "StandaloneStart",
    "XboxOneStart",
    "GameCoreXboxStart",
    "UWPStart",
    "PS4Start",
    "MainMenu"
  };

  static Levels()
  {
    SceneManager.activeSceneChanged += OnActiveSceneChanged;
    isActiveSceneSpecial = isSpecial(SceneManager.GetActiveScene().name);
  }

  private static void OnActiveSceneChanged(Scene replaced, Scene next) => isActiveSceneSpecial = isSpecial(next.name);

  public static bool isSpecialNonAlloc() => isActiveSceneSpecial;

  public static bool isSpecial() => isSpecial(SceneManager.GetActiveScene().name);

  private static bool isSpecial(string name) => specialScenes.Contains(name);

  public static bool isMainMenu() => IsLevel("MainMenu");

  public static bool IsLevel(string name) => SceneManager.GetActiveScene().name == name;
}
