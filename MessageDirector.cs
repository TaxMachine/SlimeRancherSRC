// Decompiled with JetBrains decompiler
// Type: MessageDirector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MessageDirector : MonoBehaviour
{
  private BundlesListener bundlesListeners;
  public const string GLOBAL_BUNDLE = "global";
  public string msgPath = "I18n";
  public Lang defaultLang;
  public string fallbackLang = "en";
  private CultureInfo culture;
  private Dictionary<string, MessageBundle> cache = new Dictionary<string, MessageBundle>();
  private MessageBundle global;
  private const string MBUNDLE_CLASS_KEY = "msgbundle_class";

  public static Lang GetLang(string code)
  {
    string upperInvariant = code.ToUpperInvariant();
    return Enum.IsDefined(typeof (Lang), upperInvariant) ? (Lang) Enum.Parse(typeof (Lang), upperInvariant) : Lang.EN;
  }

  public static Lang GetLang(CultureInfo culture) => GetLang(culture.TwoLetterISOLanguageName);

  public void Awake() => SetCulture(GetLang(CultureInfo.CurrentCulture));

  public CultureInfo GetCulture() => culture;

  private void SetCulture(SystemLanguage systemLanguage)
  {
    Debug.Log("Setting Culture given SystemLanguage: " + systemLanguage);
    switch (Application.systemLanguage)
    {
      case SystemLanguage.Chinese:
      case SystemLanguage.ChineseSimplified:
      case SystemLanguage.ChineseTraditional:
        SetCulture(Lang.ZH);
        break;
      case SystemLanguage.French:
        SetCulture(Lang.FR);
        break;
      case SystemLanguage.German:
        SetCulture(Lang.DE);
        break;
      case SystemLanguage.Russian:
        SetCulture(Lang.RU);
        break;
      case SystemLanguage.Spanish:
        SetCulture(Lang.ES);
        break;
      case SystemLanguage.Swedish:
        SetCulture(Lang.SV);
        break;
      default:
        SetCulture(Lang.EN);
        break;
    }
  }

  private void SetCulture(CultureInfo culture) => SetCulture(culture, true);

  public string GetCurrentLanguageCode() => GetCulture().TwoLetterISOLanguageName;

  public void SetCulture(Lang lang) => SetCulture(GetCultureInfo(lang));

  public static CultureInfo GetCultureInfo(Lang lang)
  {
    string name = lang.ToString();
    if (name == "ZH")
      name = "ZH-HANS";
    return CultureInfo.GetCultureInfo(name);
  }

  private void SetCulture(CultureInfo culture, bool updateGlobal)
  {
    if (this.culture == culture)
      return;
    this.culture = culture;
    cache.Clear();
    if (updateGlobal)
      global = GetBundle("global");
    Log.Info("", "Culture", culture);
    if (bundlesListeners == null)
      return;
    bundlesListeners(this);
  }

  public Lang GetCultureLang() => GetLang(culture);

  public void RegisterBundlesListener(BundlesListener avail)
  {
    bundlesListeners += avail;
    avail(this);
  }

  public void UnregisterBundlesListener(BundlesListener avail) => bundlesListeners -= avail;

  public MessageBundle GetBundle(string path)
  {
    if (cache.ContainsKey(path))
      return cache[path];
    ResourceBundle rbundle = LoadBundle(path);
    MessageBundle customBundle = null;
    if (rbundle != null)
    {
      string typeName = null;
      try
      {
        typeName = rbundle.GetString("msgbundle_class");
        if (typeName != null)
        {
          typeName = typeName.Trim();
          if (typeName != "")
            customBundle = Type.GetType(typeName).GetConstructor(new Type[0]).Invoke(new object[0]) as MessageBundle;
        }
      }
      catch (Exception ex)
      {
        Log.Warning("Failure instantiating custom message bundle", "mbclass", typeName, "error", ex);
      }
    }
    MessageBundle bundle = CreateBundle(path, rbundle, customBundle);
    cache[path] = bundle;
    return bundle;
  }

  public string Get(string path, string key) => GetBundle(path).Get(key);

  public string Get(string path, string key, params object[] args) => GetBundle(path).Get(key, args);

  protected MessageBundle CreateBundle(
    string path,
    ResourceBundle rbundle,
    MessageBundle customBundle)
  {
    if (customBundle == null)
      customBundle = new MessageBundle();
    InitBundle(customBundle, path, rbundle);
    return customBundle;
  }

  protected void InitBundle(MessageBundle bundle, string path, ResourceBundle rbundle)
  {
    MessageBundle parent = global;
    if (rbundle != null)
    {
      string path1 = rbundle.GetString("__parent");
      if (path1 != null)
        parent = GetBundle(path1);
    }
    bundle.Init(this, path, rbundle, parent);
  }

  protected ResourceBundle LoadBundle(string path)
  {
    try
    {
      return ResourceBundle.GetBundle(msgPath, path, culture, fallbackLang);
    }
    catch (MissingResourceException ex)
    {
      Log.Warning("Unable to resolve resource bundle", nameof (path), path, "culture", culture, ex);
      return null;
    }
  }

  public enum Lang
  {
    EN,
    DE,
    ES,
    FR,
    RU,
    SV,
    ZH,
    JA,
    PT,
    KO,
  }

  public delegate void BundlesListener(MessageDirector msgDir);
}
