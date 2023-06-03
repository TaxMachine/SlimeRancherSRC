// Decompiled with JetBrains decompiler
// Type: Microsoft.Xbox.Gdk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Microsoft.Xbox
{
  public class Gdk : MonoBehaviour
  {
    [Header("You can find the value of the scid in your MicrosoftGame.config")]
    public string scid;
    public Text gamertagLabel;
    public bool signInOnStart = true;
    private static Gdk _xboxHelpers;
    private static bool _initialized;
    private static Dictionary<int, string> _hresultToFriendlyErrorLookup;
    private const int _100PercentAchievementProgress = 100;
    private const string _GameSaveContainerName = "x_game_save_default_container";
    private const string _GameSaveBlobName = "x_game_save_default_blob";
    private const int _MaxAssociatedProductsToRetrieve = 25;

    public static Gdk Helpers
    {
      get
      {
        if (_xboxHelpers == null)
        {
          Gdk[] objectsOfType = FindObjectsOfType<Gdk>();
          if (objectsOfType.Length != 0)
          {
            _xboxHelpers = objectsOfType[0];
            _xboxHelpers._Initialize();
          }
          else
            _LogError("Error: Could not find Xbox prefab. Make sure you have added the Xbox prefab to your scene.");
        }
        return _xboxHelpers;
      }
    }

    public event OnGameSaveLoadedHandler OnGameSaveLoaded;

    public event OnErrorHandler OnError;

    private void Start() => _Initialize();

    private void _Initialize()
    {
      if (_initialized)
        return;
      _initialized = true;
      DontDestroyOnLoad(gameObject);
    }

    private void InitializeHresultToFriendlyErrorLookup() => _hresultToFriendlyErrorLookup.Add(-2143330041, "IAP_UNEXPECTED: Does the player you are signed in as have a license for the game? You can get one by downloading your game from the store and purchasing it first. If you can't find your game in the store, have you published it in Partner Center?");

    public void SignIn()
    {
    }

    public void Save(byte[] data)
    {
    }

    public void LoadSaveData()
    {
    }

    public void UnlockAchievement(string achievementId)
    {
    }

    private void Update()
    {
    }

    protected static bool Succeeded(int hresult, string operationFriendlyName)
    {
      bool flag = false;
      if (HR.SUCCEEDED(hresult))
      {
        flag = true;
      }
      else
      {
        string errorCode = hresult.ToString("X8");
        string empty = string.Empty;
        string errorMessage = !_hresultToFriendlyErrorLookup.ContainsKey(hresult) ? operationFriendlyName + " failed." : _hresultToFriendlyErrorLookup[hresult];
        _LogError(string.Format("{0} Error code: hr=0x{1}", errorMessage, errorCode));
        if (Helpers.OnError != null)
          Helpers.OnError(Helpers, new ErrorEventArgs(errorCode, errorMessage));
      }
      return flag;
    }

    private static void _LogError(string message) => Debug.Log(message);

    public delegate void OnGameSaveLoadedHandler(object sender, GameSaveLoadedArgs e);

    public delegate void OnErrorHandler(object sender, ErrorEventArgs e);
  }
}
