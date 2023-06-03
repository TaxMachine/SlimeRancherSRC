// Decompiled with JetBrains decompiler
// Type: ChickenVampirism
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ChickenVampirism : FindConsumable, ControllerCollisionListener
{
  public GameObject fx;
  public int damagePerTouch = 10;
  public float repeatTime = 1f;
  public float maxJump = 12f;
  private GameObject activeFX;
  private GameObject target;
  private float nextTime;
  private bool isNight;
  private TimeDirector timeDir;
  private ModDirector modDir;
  private float nextLeapAvail;
  private const float INIT_NO_DAMAGE_WINDOW = 0.1f;

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    modDir = SRSingleton<SceneContext>.Instance.ModDirector;
    modDir.RegisterModsListener(SetEnabled);
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    modDir.UnregisterModsListener(SetEnabled);
  }

  private void SetEnabled()
  {
    enabled = modDir.VampiricChickens();
    if (enabled || !(activeFX != null))
      return;
    Destroyer.Destroy(activeFX, "ChickenVampirism.SetEnabled");
  }

  public override void OnEnable()
  {
    base.OnEnable();
    if (modDir.VampiricChickens())
      return;
    enabled = false;
  }

  public override void OnDisable()
  {
    base.OnDisable();
    if (!(activeFX != null))
      return;
    Destroyer.Destroy(activeFX, "ChickenVampirism.OnDisable");
  }

  public void Update()
  {
    float num = timeDir.CurrDayFraction();
    isNight = num < 0.25 || num > 0.75;
    if (isNight && activeFX == null)
    {
      activeFX = Instantiate(fx);
      activeFX.transform.SetParent(transform, false);
    }
    else
    {
      if (isNight || !(activeFX != null))
        return;
      Destroyer.Destroy(activeFX, "ChickenVampirism.Update");
    }
  }

  public void OnControllerCollision(GameObject gameObj)
  {
    if (!enabled || !isNight || Time.time < (double) nextTime)
      return;
    if (gameObj.GetInterfaceComponent<Damageable>().Damage(damagePerTouch, gameObject))
      DeathHandler.Kill(gameObj, DeathHandler.Source.CHICKEN_VAMPIRISM, gameObject, "ChickenVampirism.OnControllerCollision");
    nextTime = Time.time + repeatTime;
  }

  protected override Dictionary<Identifiable.Id, DriveCalculator> GetSearchIds() => new Dictionary<Identifiable.Id, DriveCalculator>(Identifiable.idComparer)
  {
    [Identifiable.Id.PLAYER] = new ChickenDriveCalculator()
  };

  public override float Relevancy(bool isGrounded)
  {
    if (!enabled || !isNight)
      return 0.0f;
    target = FindNearestConsumable(out float _);
    return !(target == null) ? 0.99f : 0.0f;
  }

  public override void Selected()
  {
  }

  public override void Action()
  {
    if (!(target != null))
      return;
    MoveTowards(GetGotoPos(target), IsBlocked(target), ref nextLeapAvail, maxJump);
  }

  private class ChickenDriveCalculator : DriveCalculator
  {
    public ChickenDriveCalculator()
      : base(SlimeEmotions.Emotion.NONE, 0.0f, 0.0f)
    {
    }

    public override float Drive(SlimeEmotions emotions, Identifiable.Id id) => 1f;
  }
}
