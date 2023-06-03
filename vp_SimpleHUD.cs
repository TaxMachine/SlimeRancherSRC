// Decompiled with JetBrains decompiler
// Type: vp_SimpleHUD
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_SimpleHUD : MonoBehaviour, EventHandlerRegistrable
{
  public bool ShowHUD = true;
  protected vp_FPPlayerEventHandler m_Player;
  public Font BigFont;
  public Font SmallFont;
  public Font MessageFont;
  public float BigFontOffset = 69f;
  public float SmallFontOffset = 56f;
  public Texture Background;
  protected Vector2 m_DrawPos = Vector2.zero;
  protected Vector2 m_DrawSize = Vector2.zero;
  protected Rect m_DrawLabelRect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  protected Rect m_DrawShadowRect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
  protected float m_TargetHealthOffset;
  protected float m_CurrentHealthOffset;
  protected float m_TargetAmmoOffset = 200f;
  protected float m_CurrentAmmoOffset = 200f;
  public Texture2D HealthIcon;
  public float HealthMultiplier = 10f;
  public Color HealthColor = Color.white;
  public float HealthLowLevel = 2.5f;
  public Color HealthLowColor = new Color(0.75f, 0.0f, 0.0f, 1f);
  public AudioClip HealthLowSound;
  public float HealthLowSoundInterval = 1f;
  protected float m_FormattedHealth;
  protected float m_NextAllowedPlayHealthLowSoundTime;
  protected AudioSource m_Audio;
  public Color AmmoColor = Color.white;
  public Color AmmoLowColor = new Color(0.0f, 0.0f, 0.0f, 1f);
  protected string m_PickupMessage = "";
  protected Color m_MessageColor = new Color(1f, 1f, 1f, 2f);
  protected Color m_InvisibleColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  protected Color m_TranspBlack = new Color(0.0f, 0.0f, 0.0f, 0.5f);
  protected Color m_TranspWhite = new Color(1f, 1f, 1f, 0.5f);
  protected static GUIStyle m_MessageStyle;
  protected GUIStyle m_HealthStyle;
  protected GUIStyle m_AmmoStyle;
  protected GUIStyle m_AmmoStyleSmall;

  protected float m_HealthWidth => HealthStyle.CalcSize(new GUIContent(FormattedHealth)).x;

  public GUIStyle MessageStyle
  {
    get
    {
      if (m_MessageStyle == null)
      {
        m_MessageStyle = new GUIStyle("Label");
        m_MessageStyle.alignment = TextAnchor.MiddleCenter;
        m_MessageStyle.font = MessageFont;
      }
      return m_MessageStyle;
    }
  }

  public GUIStyle HealthStyle
  {
    get
    {
      if (m_HealthStyle == null)
      {
        m_HealthStyle = new GUIStyle("Label");
        m_HealthStyle.font = BigFont;
        m_HealthStyle.alignment = TextAnchor.MiddleRight;
        m_HealthStyle.fontSize = 28;
        m_HealthStyle.wordWrap = false;
      }
      return m_HealthStyle;
    }
  }

  public GUIStyle AmmoStyle
  {
    get
    {
      if (m_AmmoStyle == null)
      {
        m_AmmoStyle = new GUIStyle("Label");
        m_AmmoStyle.font = BigFont;
        m_AmmoStyle.alignment = TextAnchor.MiddleRight;
        m_AmmoStyle.fontSize = 28;
        m_AmmoStyle.wordWrap = false;
      }
      return m_AmmoStyle;
    }
  }

  public GUIStyle AmmoStyleSmall
  {
    get
    {
      if (m_AmmoStyleSmall == null)
      {
        m_AmmoStyleSmall = new GUIStyle("Label");
        m_AmmoStyleSmall.font = SmallFont;
        m_AmmoStyleSmall.alignment = TextAnchor.UpperLeft;
        m_AmmoStyleSmall.fontSize = 15;
        m_AmmoStyleSmall.wordWrap = false;
      }
      return m_AmmoStyleSmall;
    }
  }

  protected virtual void Awake()
  {
    m_Player = transform.GetComponent<vp_FPPlayerEventHandler>();
    m_Audio = m_Player.transform.GetComponent<AudioSource>();
  }

  protected virtual void OnEnable()
  {
    if (!(m_Player != null))
      return;
    Register(m_Player);
  }

  protected virtual void OnDisable()
  {
    if (!(m_Player != null))
      return;
    Unregister(m_Player);
  }

  private string FormattedHealth
  {
    get
    {
      m_FormattedHealth = m_Player.Health.Get() * HealthMultiplier;
      if (m_FormattedHealth < 1.0)
        m_FormattedHealth = m_Player.Dead.Active ? Mathf.Min(m_FormattedHealth, 0.0f) : 1f;
      if (m_Player.Dead.Active && m_FormattedHealth > 0.0)
        m_FormattedHealth = 0.0f;
      return ((int) m_FormattedHealth).ToString();
    }
  }

  private void Update()
  {
    m_CurrentAmmoOffset = Mathf.SmoothStep(m_CurrentAmmoOffset, m_TargetAmmoOffset, Time.deltaTime * 10f);
    m_CurrentHealthOffset = Mathf.SmoothStep(m_CurrentHealthOffset, m_TargetHealthOffset, Time.deltaTime * 10f);
    m_TargetAmmoOffset = m_Player.CurrentWeaponIndex.Get() == 0 || m_Player.CurrentWeaponType.Get() == 2 ? 200f : 10f;
    if (m_Player.Dead.Active)
      HealthColor = Color.black;
    else if (m_Player.Health.Get() < (double) HealthLowLevel)
    {
      HealthColor = Color.Lerp(Color.white, HealthLowColor, (float) (vp_MathUtility.Sinus(6f, 0.1f) * 5.0 + 0.5));
      if (HealthLowSound != null && Time.time >= (double) m_NextAllowedPlayHealthLowSoundTime)
      {
        m_NextAllowedPlayHealthLowSoundTime = Time.time + HealthLowSoundInterval;
        m_Audio.pitch = 1f;
        m_Audio.PlayOneShot(HealthLowSound);
      }
    }
    else
      HealthColor = Color.white;
    if (m_Player.CurrentWeaponAmmoCount.Get() < 1 && m_Player.CurrentWeaponType.Get() != 3)
      AmmoColor = Color.Lerp(Color.white, AmmoLowColor, (float) (vp_MathUtility.Sinus(8f, 0.1f) * 5.0 + 0.5));
    else
      AmmoColor = Color.white;
  }

  protected virtual void OnGUI()
  {
    if (!ShowHUD)
      return;
    DrawHealth();
    DrawAmmo();
    DrawText();
  }

  private void DrawHealth()
  {
    DrawLabel("", new Vector2(m_CurrentHealthOffset, Screen.height - 68), new Vector2(80f + m_HealthWidth, 52f), AmmoStyle, Color.white, m_TranspBlack, null);
    if (HealthIcon != null)
      DrawLabel("", new Vector2(m_CurrentHealthOffset + 10f, Screen.height - 58), new Vector2(32f, 32f), AmmoStyle, Color.white, HealthColor, HealthIcon);
    DrawLabel(FormattedHealth, new Vector2((float) (m_CurrentHealthOffset - 18.0 - (45.0 - m_HealthWidth)), Screen.height - BigFontOffset), new Vector2(110f, 60f), HealthStyle, HealthColor, Color.clear, null);
    DrawLabel("%", new Vector2(m_CurrentHealthOffset + 50f + m_HealthWidth, Screen.height - SmallFontOffset), new Vector2(110f, 60f), AmmoStyleSmall, HealthColor, Color.clear, null);
    GUI.color = Color.white;
  }

  private void DrawAmmo()
  {
    if (m_Player.CurrentWeaponType.Get() == 3)
    {
      DrawLabel("", new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 93.0) - AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.Get().ToString())).x, Screen.height - 68), new Vector2(200f, 52f), AmmoStyle, AmmoColor, m_TranspBlack, null);
      if (m_Player.CurrentAmmoIcon.Get() != null)
        DrawLabel("", new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 83.0) - AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.Get().ToString())).x, Screen.height - 58), new Vector2(32f, 32f), AmmoStyle, Color.white, AmmoColor, m_Player.CurrentAmmoIcon.Get());
      DrawLabel((m_Player.CurrentWeaponAmmoCount.Get() + m_Player.CurrentWeaponClipCount.Get()).ToString(), new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 145.0), Screen.height - BigFontOffset), new Vector2(110f, 60f), AmmoStyle, AmmoColor, Color.clear, null);
    }
    else
    {
      DrawLabel("", new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 115.0) - AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.Get().ToString())).x, Screen.height - 68), new Vector2(200f, 52f), AmmoStyle, AmmoColor, m_TranspBlack, null);
      if (m_Player.CurrentAmmoIcon.Get() != null)
        DrawLabel("", new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 105.0) - AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.Get().ToString())).x, Screen.height - 58), new Vector2(32f, 32f), AmmoStyle, Color.white, AmmoColor, m_Player.CurrentAmmoIcon.Get());
      int num = m_Player.CurrentWeaponAmmoCount.Get();
      DrawLabel(num.ToString(), new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 177.0), Screen.height - BigFontOffset), new Vector2(110f, 60f), AmmoStyle, AmmoColor, Color.clear, null);
      num = m_Player.CurrentWeaponClipCount.Get();
      DrawLabel("/ " + num.ToString(), new Vector2((float) (m_CurrentAmmoOffset + (double) Screen.width - 60.0), Screen.height - SmallFontOffset), new Vector2(110f, 60f), AmmoStyleSmall, AmmoColor, Color.clear, null);
    }
  }

  private void DrawText()
  {
    if (m_PickupMessage == null || m_MessageColor.a < 0.0099999997764825821)
      return;
    m_MessageColor = Color.Lerp(m_MessageColor, m_InvisibleColor, Time.deltaTime * 0.4f);
    GUI.color = m_MessageColor;
    GUI.Box(new Rect(200f, 150f, Screen.width - 400, Screen.height - 400), m_PickupMessage, MessageStyle);
    GUI.color = Color.white;
  }

  protected virtual void OnMessage_HUDText(string message)
  {
    m_MessageColor = Color.white;
    m_PickupMessage = message;
  }

  private void DrawLabel(
    string text,
    Vector2 position,
    Vector2 scale,
    GUIStyle textStyle,
    Color textColor,
    Color bgColor,
    Texture texture)
  {
    if (texture == null)
      texture = Background;
    if (scale.x == 0.0)
      scale.x = textStyle.CalcSize(new GUIContent(text)).x;
    if (scale.y == 0.0)
      scale.y = textStyle.CalcSize(new GUIContent(text)).y;
    m_DrawLabelRect.x = m_DrawPos.x = position.x;
    m_DrawLabelRect.y = m_DrawPos.y = position.y;
    m_DrawLabelRect.width = m_DrawSize.x = scale.x;
    m_DrawLabelRect.height = m_DrawSize.y = scale.y;
    if (bgColor != Color.clear)
    {
      GUI.color = bgColor;
      if (texture != null)
        GUI.DrawTexture(m_DrawLabelRect, texture);
    }
    GUI.color = textColor;
    GUI.Label(m_DrawLabelRect, text, textStyle);
    GUI.color = Color.white;
    m_DrawPos.x += m_DrawSize.x;
    m_DrawPos.y += m_DrawSize.y;
  }

  private void OnStart_SetWeapon() => m_TargetAmmoOffset = 200f;

  private void OnStop_SetWeapon() => m_TargetAmmoOffset = 10f;

  private void OnStop_Dead()
  {
    m_CurrentHealthOffset = -200f;
    m_TargetHealthOffset = 0.0f;
    HealthColor = Color.white;
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterActivity("Dead", null, OnStop_Dead, null, null, null, null);
    eventHandler.RegisterActivity("SetWeapon", OnStart_SetWeapon, OnStop_SetWeapon, null, null, null, null);
    eventHandler.RegisterMessage<string>("HUDText", OnMessage_HUDText);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterActivity("Dead", null, OnStop_Dead, null, null, null, null);
    eventHandler.UnregisterActivity("SetWeapon", OnStart_SetWeapon, OnStop_SetWeapon, null, null, null, null);
    eventHandler.UnregisterMessage<string>("HUDText", OnMessage_HUDText);
  }
}
