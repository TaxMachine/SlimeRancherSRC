// Decompiled with JetBrains decompiler
// Type: SlimeHoop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHoop : Attractor
{
  public Transform hoopBone;
  public GameObject scoreFX;
  public Transform scoreFxTransform;
  public GameObject endFX;
  public SECTR_AudioCue scoreCue;
  public SECTR_AudioCue endCue;
  public Text scoreText;
  public Text timeText;
  private Mode mode;
  private TimeDirector timeDir;
  private MusicDirector musicDir;
  private AchievementsDirector achieveDir;
  private float defaultVert;
  private float defaultRot;
  private double startTime;
  private double endTime;
  private int currScore;
  private int scoreToDisplay = 999;
  private int timeLeftToDisplay;
  private const float VERT_PERIOD = 660f;
  private const float VERT_FACTOR = 0.009519978f;
  private const float VERT_RANGE = 1f;
  private const float VERT_RESET_SPD = 1f;
  private const float ROT_PERIOD = 780f;
  private const float ROT_FACTOR = 0.008055366f;
  private const float ROT_RANGE = 45f;
  private const float ROT_RESET_SPD = 90f;
  private const float DURATION = 3600f;
  private const float DOWN_FORCE = 5f;
  private const float MAX_AWE_SCORE = 10f;

  public void Awake()
  {
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    musicDir = SRSingleton<GameContext>.Instance.MusicDirector;
    achieveDir = SRSingleton<SceneContext>.Instance.AchievementsDirector;
    defaultVert = hoopBone.transform.localPosition.x;
    defaultRot = hoopBone.transform.localRotation.eulerAngles.x;
  }

  public void FixedUpdate()
  {
    double num1 = timeDir.WorldTime();
    if (mode == Mode.ACTIVE)
    {
      hoopBone.transform.localPosition = new Vector3(CurrVert(num1 - startTime), hoopBone.transform.localPosition.y, hoopBone.transform.localPosition.z);
      hoopBone.transform.localRotation = Quaternion.Euler(CurrRot(num1 - startTime), 0.0f, 90f);
      if (num1 >= endTime)
      {
        mode = Mode.RESETTING;
        SetAweFactor(0.0f);
        if (endFX != null)
          SpawnAndPlayFX(endFX, scoreFxTransform.position, scoreFxTransform.rotation);
        if (endCue != null)
          SECTR_AudioSystem.Play(endCue, scoreFxTransform.position, false);
      }
    }
    else if (mode == Mode.RESETTING)
    {
      bool flag = false;
      Vector3 localPosition = hoopBone.transform.localPosition;
      if (localPosition.x - (double) defaultVert > 0.0099999997764825821)
      {
        localPosition.x = Mathf.Max(defaultVert, localPosition.x - Time.fixedDeltaTime * 1f);
        flag = true;
      }
      else if (localPosition.x - (double) defaultVert < -0.0099999997764825821)
      {
        localPosition.x = Mathf.Min(defaultVert, localPosition.x + Time.fixedDeltaTime * 1f);
        flag = true;
      }
      Vector3 eulerAngles = hoopBone.transform.localRotation.eulerAngles;
      if (eulerAngles.x - (double) defaultRot > 0.10000000149011612)
      {
        eulerAngles.x = Mathf.Max(defaultRot, eulerAngles.x - Time.fixedDeltaTime * 90f);
        flag = true;
      }
      else if (eulerAngles.x - (double) defaultRot < -0.10000000149011612)
      {
        eulerAngles.x = Mathf.Min(defaultRot, eulerAngles.x + Time.fixedDeltaTime * 90f);
        flag = true;
      }
      if (!flag)
      {
        localPosition.x = defaultVert;
        eulerAngles.x = defaultRot;
        mode = Mode.IDLE;
      }
      hoopBone.transform.localPosition = localPosition;
      hoopBone.transform.localRotation = Quaternion.Euler(eulerAngles);
    }
    if (mode == Mode.ACTIVE)
    {
      int num2 = (int) Math.Floor(endTime - num1) / 60;
      if (num2 != timeLeftToDisplay)
      {
        timeLeftToDisplay = num2;
        timeText.text = timeLeftToDisplay.ToString();
      }
    }
    else
      timeText.text = "--:--";
    if (scoreToDisplay == currScore)
      return;
    scoreToDisplay = currScore;
    scoreText.text = scoreToDisplay.ToString();
  }

  public void AddScore()
  {
    if (mode == Mode.IDLE)
    {
      mode = Mode.ACTIVE;
      startTime = timeDir.WorldTime();
      endTime = startTime + 3600.0;
      currScore = 0;
      musicDir.EnableSlimeHoopMode(endTime);
    }
    if (mode == Mode.RESETTING)
      return;
    SpawnAndPlayFX(scoreFX, scoreFxTransform.position, scoreFxTransform.rotation);
    if (scoreCue != null)
      SECTR_AudioSystem.Play(scoreCue, scoreFxTransform.position, false);
    ++currScore;
    SetAweFactor(Mathf.Min(1f, currScore / 10f));
    achieveDir.MaybeUpdateMaxStat(AchievementsDirector.IntStat.SLIMEBALL_SCORE, currScore);
  }

  private float CurrVert(double time) => (float) (Math.Sin(time * 0.0095199784263968468) * 1.0) + defaultVert;

  private float CurrRot(double time) => (float) (Math.Sin(time * (Math.PI / 390.0)) * 45.0) + defaultRot;

  private enum Mode
  {
    IDLE,
    ACTIVE,
    RESETTING,
  }
}
