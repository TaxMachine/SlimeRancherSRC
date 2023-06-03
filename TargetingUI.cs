// Decompiled with JetBrains decompiler
// Type: TargetingUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TargetingUI : SRSingleton<TargetingUI>
{
  public TMP_Text nameText;
  public TMP_Text infoText;
  private PediaDirector pediaDir;
  private MessageBundle uiBundle;
  private MessageBundle pediaBundle;
  private PlayerState player;
  private float holdInfoUntil;
  private const float HOLD_DURATION = 1f;
  private GameObject currentTarget;

  public void Start()
  {
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
  }

  public void OnBundlesAvailable(MessageDirector msgDir)
  {
    uiBundle = msgDir.GetBundle("ui");
    pediaBundle = msgDir.GetBundle("pedia");
    currentTarget = null;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
  }

  public void Update()
  {
    GameObject targeting = player.Targeting;
    if (targeting != null && currentTarget == targeting)
    {
      holdInfoUntil = Time.time + 1f;
    }
    else
    {
      currentTarget = null;
      if (targeting != null && (GetIdentifiableTarget(targeting) || GetGordoIdentifiableTarget(targeting) || GetDroneTarget(targeting)))
      {
        holdInfoUntil = Time.time + 1f;
        currentTarget = targeting;
      }
      bool flag = Time.time <= (double) holdInfoUntil;
      nameText.enabled = flag;
      infoText.enabled = flag;
    }
  }

  private bool GetIdentifiableTarget(GameObject gameObject)
  {
    Identifiable.Id id = Identifiable.GetId(gameObject);
    if (id != Identifiable.Id.NONE)
    {
      if (Identifiable.IsPlort(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = uiBundle.Get("m.hudinfo_plort");
        return true;
      }
      if (Identifiable.IsEcho(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = uiBundle.Get("m.hudinfo_echo");
        return true;
      }
      if (Identifiable.IsEchoNote(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = uiBundle.Get("m.hudinfo_echo_note");
        return true;
      }
      if (Identifiable.IsOrnament(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = uiBundle.Get("m.hudinfo_ornament");
        return true;
      }
      if (Identifiable.IsToy(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = id != Identifiable.Id.KOOKADOBA_BALL ? uiBundle.Get("m.hudinfo_toy") : uiBundle.Get("m.hudinfo_fruitball");
        return true;
      }
      if (pediaDir.GetPediaId(id).HasValue)
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = GetIdentifiableInfoText(id);
        return true;
      }
      if (Identifiable.IsTarr(id))
      {
        nameText.text = Identifiable.GetName(Identifiable.Id.TARR_SLIME);
        infoText.text = GetIdentifiableInfoText(Identifiable.Id.TARR_SLIME);
        return true;
      }
      if (Identifiable.IsSlime(id))
      {
        nameText.text = Identifiable.GetName(id);
        infoText.text = GetIdentifiableInfoText(id);
        return true;
      }
    }
    return false;
  }

  private string GetIdentifiableInfoText(Identifiable.Id identId)
  {
    SlimeEat component = SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(identId).GetComponent<SlimeEat>();
    return Identifiable.IsTarr(identId) ? uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_diet", new string[1]
    {
      "m.foodgroup.tarr"
    })) : (identId == Identifiable.Id.PUDDLE_SLIME ? uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_diet", new string[1]
    {
      "m.foodgroup.water"
    })) : (identId == Identifiable.Id.FIRE_SLIME ? uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_diet", new string[1]
    {
      "m.foodgroup.ash"
    })) : (component != null ? uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_diet", new string[1]
    {
      component.slimeDefinition.Diet.GetModulesFoodGroupsMsg()
    })) : uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_type", new string[1]
    {
      SlimeDiet.GetFoodCategoryMsg(identId)
    })))));
  }

  private bool GetGordoIdentifiableTarget(GameObject gameObject)
  {
    GordoIdentifiable component1 = gameObject.GetComponent<GordoIdentifiable>();
    GordoEat component2 = gameObject.GetComponent<GordoEat>();
    if (!(component1 != null) || !(component2 != null) || !Identifiable.IsGordo(component1.id))
      return false;
    nameText.text = Identifiable.GetName(component1.id);
    infoText.text = uiBundle.Xlate(MessageUtil.Compose("m.hudinfo_diet", new string[1]
    {
      component2.GetDirectFoodGroupsMsg()
    }));
    return true;
  }

  private bool GetDroneTarget(GameObject gameObject)
  {
    Drone component = gameObject.GetComponent<Drone>();
    if (!(component != null))
      return false;
    nameText.text = pediaBundle.Get("m.gadget.name.drone");
    infoText.text = string.Join(", ", component.gadget.programs.Where(p => p.IsComplete()).Select(p => p.target.GetName()).ToArray());
    return true;
  }
}
