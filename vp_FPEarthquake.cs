// Decompiled with JetBrains decompiler
// Type: vp_FPEarthquake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class vp_FPEarthquake : MonoBehaviour, EventHandlerRegistrable
{
  protected Vector3 m_CameraEarthQuakeForce;
  protected float m_Endtime;
  protected Vector2 m_Magnitude = Vector2.zero;
  private vp_FPPlayerEventHandler m_FPPlayer;

  private vp_FPPlayerEventHandler FPPlayer
  {
    get
    {
      if (m_FPPlayer == null)
        m_FPPlayer = FindObjectOfType(typeof (vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;
      return m_FPPlayer;
    }
  }

  protected virtual void OnEnable()
  {
    if (!(FPPlayer != null))
      return;
    Register(FPPlayer);
  }

  protected virtual void OnDisable()
  {
    if (!(FPPlayer != null))
      return;
    Unregister(FPPlayer);
  }

  protected void FixedUpdate()
  {
    if (Time.timeScale == 0.0)
      return;
    UpdateEarthQuake();
  }

  protected void UpdateEarthQuake()
  {
    if (!FPPlayer.CameraEarthQuake.Active)
    {
      m_CameraEarthQuakeForce = Vector3.zero;
    }
    else
    {
      m_CameraEarthQuakeForce = Vector3.Scale(vp_SmoothRandom.GetVector3Centered(1f), m_Magnitude.x * (Vector3.right + Vector3.forward) * Mathf.Min(m_Endtime - Time.time, 1f) * Time.timeScale);
      m_CameraEarthQuakeForce.y = 0.0f;
      if (Random.value >= 0.30000001192092896 * Time.timeScale)
        return;
      m_CameraEarthQuakeForce.y = Random.Range(0.0f, m_Magnitude.y * 0.35f) * Mathf.Min(m_Endtime - Time.time, 1f);
    }
  }

  protected virtual void OnStart_CameraEarthQuake()
  {
    Vector3 vector3 = (Vector3) FPPlayer.CameraEarthQuake.Argument;
    m_Magnitude.x = vector3.x;
    m_Magnitude.y = vector3.y;
    m_Endtime = Time.time + vector3.z;
    FPPlayer.CameraEarthQuake.AutoDuration = vector3.z;
  }

  protected virtual void OnMessage_CameraBombShake(float impact) => FPPlayer.CameraEarthQuake.TryStart(new Vector3(impact * 0.5f, impact * 0.5f, 1f));

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.RegisterActivity("CameraEarthQuake", OnStart_CameraEarthQuake, null, null, null, null, null);
    eventHandler.RegisterValue("CameraEarthQuakeForce", Get_CameraEarthQuakeForce, Set_CameraEarthQuakeForce);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterMessage<float>("CameraBombShake", OnMessage_CameraBombShake);
    eventHandler.UnregisterActivity("CameraEarthQuake", OnStart_CameraEarthQuake, null, null, null, null, null);
    eventHandler.UnregisterValue("CameraEarthQuakeForce", Get_CameraEarthQuakeForce, Set_CameraEarthQuakeForce);
  }

  protected virtual Vector3 Get_CameraEarthQuakeForce() => m_CameraEarthQuakeForce;

  protected virtual void Set_CameraEarthQuakeForce(Vector3 value) => m_CameraEarthQuakeForce = value;

  protected virtual Vector3 OnValue_CameraEarthQuakeForce
  {
    get => m_CameraEarthQuakeForce;
    set => m_CameraEarthQuakeForce = value;
  }
}
