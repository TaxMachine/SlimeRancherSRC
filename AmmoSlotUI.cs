// Decompiled with JetBrains decompiler
// Type: AmmoSlotUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSlotUI : SRSingleton<AmmoSlotUI>
{
  public GameObject selectedPrefab;
  private const int MAX_SLOTS = 5;
  public Slot[] slots;
  public Sprite backEmpty;
  public Sprite backFilled;
  public Sprite frontEmpty;
  public Sprite frontFilled;
  public Sprite backEmptyWater;
  public Sprite backFilledWater;
  public Sprite frontEmptyWater;
  public Sprite frontFilledWater;
  public TMP_Text liquidValueText;
  public TMP_Text liquidValueTimer;
  public GameObject liquidFXObj;
  public SlimeAppearanceDirector slimeAppearanceDirector;
  private GameObject selected;
  private PlayerState player;
  private TimeDirector timeDir;
  private LookupDirector lookupDir;
  private int animSelectedId;
  private Dictionary<Identifiable.Id, string> cachedNames = new Dictionary<Identifiable.Id, string>(Identifiable.idComparer);
  private int lastUsableSlotCount = -1;
  private int lastSelectedAmmoIndex = -1;
  private int[] lastSlotCounts = new int[5];
  private int[] lastSlotMaxAmmos = new int[5];
  private Identifiable.Id[] lastSlotIds = new Identifiable.Id[5];

  public override void Awake()
  {
    base.Awake();
    timeDir = SRSingleton<SceneContext>.Instance.TimeDirector;
    lookupDir = SRSingleton<GameContext>.Instance.LookupDirector;
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnBundlesAvailable);
    animSelectedId = Animator.StringToHash("selected");
    for (int index = 0; index < slots.Length; ++index)
      slots[index].keyBinding.SetActive(true);
  }

  public void Start()
  {
    selected = Instantiate(selectedPrefab);
    player = SRSingleton<SceneContext>.Instance.PlayerState;
    slimeAppearanceDirector.onSlimeAppearanceChanged += OnSlimeAppearanceChanged;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    slimeAppearanceDirector.onSlimeAppearanceChanged -= OnSlimeAppearanceChanged;
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnBundlesAvailable);
  }

  public void OnBundlesAvailable(MessageDirector msgDir)
  {
    cachedNames.Clear();
    foreach (VacItemDefinition vacItemDefinition in lookupDir.VacItemDefinitions)
      GetName(vacItemDefinition.Id, true);
    cachedNames[Identifiable.Id.NONE] = " ";
    for (int index = 0; index < lastSlotIds.Length; ++index)
      lastSlotIds[index] = Identifiable.Id.NONE;
  }

  private string GetName(Identifiable.Id id, bool recache = false)
  {
    if (recache || !cachedNames.ContainsKey(id))
      cachedNames[id] = Identifiable.GetName(id);
    return cachedNames[id];
  }

  public void Update()
  {
    int usableSlotCount = player.Ammo.GetUsableSlotCount();
    int selectedAmmoIdx = player.Ammo.GetSelectedAmmoIdx();
    for (int index = 0; index < slots.Length; ++index)
    {
      Slot slot = slots[index];
      if (usableSlotCount != lastUsableSlotCount)
        ToggleSlotUsability(slot, index, usableSlotCount);
      if (index < usableSlotCount)
      {
        Identifiable.Id slotName = player.Ammo.GetSlotName(index);
        int slotMaxCount = player.Ammo.GetSlotMaxCount(index);
        int slotCount = player.Ammo.GetSlotCount(index);
        if (lastSlotCounts[index] != slotCount || lastSlotMaxAmmos[index] != slotMaxCount)
        {
          if (slotName != Identifiable.Id.NONE)
          {
            slot.bar.currValue = slotCount;
            slot.bar.maxValue = slotMaxCount;
            lastSlotCounts[index] = slotCount;
          }
          else
          {
            slot.bar.currValue = 0.0f;
            slot.bar.maxValue = slotMaxCount;
            lastSlotCounts[index] = 0;
          }
          lastSlotMaxAmmos[index] = slotMaxCount;
        }
        if (lastSlotIds[index] != slotName)
        {
          slot.icon.enabled = slotName != 0;
          slot.icon.sprite = GetCurrentIcon(slotName);
          slot.bar.barColor = GetCurrentColor(slotName);
          Sprite sprite1 = index == slots.Length - 1 ? (slot.bar.currValue == 0.0 ? frontEmptyWater : frontFilledWater) : (slot.bar.currValue == 0.0 ? frontEmpty : frontFilled);
          Sprite sprite2 = index == slots.Length - 1 ? (slot.bar.currValue == 0.0 ? backEmptyWater : backFilledWater) : (slot.bar.currValue == 0.0 ? backEmpty : backFilled);
          if (slot.front.sprite != sprite1)
            slot.front.sprite = sprite1;
          if (slot.back.sprite != sprite2)
            slot.back.sprite = sprite2;
          slot.label.text = GetName(slotName);
          lastSlotIds[index] = slotName;
        }
        if (lastSelectedAmmoIndex != selectedAmmoIdx)
        {
          slots[index].anim.SetBool(animSelectedId, selectedAmmoIdx == index);
          if (selectedAmmoIdx == index)
          {
            selected.transform.SetParent(slot.bar.transform);
            selected.transform.localPosition = Vector3.zero;
            selected.transform.localScale = Vector3.one;
            selected.transform.SetSiblingIndex(0);
          }
        }
      }
    }
    double waterIsMagicMins = player.Ammo.GetRemainingWaterIsMagicMins();
    if (waterIsMagicMins > 0.0)
    {
      liquidValueTimer.text = timeDir.FormatTime((int) Math.Floor(waterIsMagicMins));
      liquidFXObj.SetActive(true);
      liquidValueText.gameObject.SetActive(false);
      liquidValueTimer.gameObject.SetActive(true);
    }
    else
    {
      liquidValueText.gameObject.SetActive(true);
      liquidValueTimer.gameObject.SetActive(false);
      liquidFXObj.SetActive(false);
    }
    lastUsableSlotCount = usableSlotCount;
    lastSelectedAmmoIndex = selectedAmmoIdx;
  }

  private void OnSlimeAppearanceChanged(SlimeDefinition definition, SlimeAppearance appearance)
  {
    for (int slotIdx = 0; slotIdx < player.Ammo.GetUsableSlotCount(); ++slotIdx)
    {
      Slot slot = slots[slotIdx];
      Identifiable.Id slotName = player.Ammo.GetSlotName(slotIdx);
      if (slotName == definition.IdentifiableId)
      {
        slot.icon.sprite = GetCurrentIcon(slotName);
        slot.bar.barColor = GetCurrentColor(slotName);
      }
    }
  }

  private void ToggleSlotUsability(Slot slot, int slotIndex, int usableSlotCount)
  {
    GameObject gameObject = slot.label.transform.parent.gameObject;
    if (slotIndex >= usableSlotCount)
    {
      if (!gameObject.activeSelf)
        return;
      gameObject.SetActive(false);
    }
    else
    {
      if (gameObject.activeSelf)
        return;
      gameObject.SetActive(true);
    }
  }

  public void SpawnAndPlayFX(GameObject prefab, int index, int count)
  {
    GameObject gameObject = SRBehaviour.SpawnAndPlayFX(prefab, slots[index].anim.gameObject);
    gameObject.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[1]
    {
      new ParticleSystem.Burst(0.0f, (float) count)
    });
    Sprite currentIcon = GetCurrentIcon(player.Ammo.GetSlotName(index));
    UiParticles.UiParticles component = gameObject.GetComponent<UiParticles.UiParticles>();
    component.materialForRendering.mainTexture = currentIcon.texture;
    component.SetMaterialDirty();
  }

  private Sprite GetCurrentIcon(Identifiable.Id id)
  {
    if (Identifiable.IsSlime(id))
      return slimeAppearanceDirector.GetCurrentSlimeIcon(id);
    return id != Identifiable.Id.NONE ? lookupDir.GetIcon(id) : null;
  }

  private Color GetCurrentColor(Identifiable.Id id)
  {
    if (Identifiable.IsSlime(id))
      return slimeAppearanceDirector.GetChosenSlimeAppearance(id).ColorPalette.Ammo;
    return id != Identifiable.Id.NONE ? lookupDir.GetColor(id) : Color.clear;
  }

  [Serializable]
  public class Slot
  {
    public Image icon;
    public StatusBar bar;
    public Animator anim;
    public Image back;
    public Image front;
    public TMP_Text label;
    public GameObject keyBinding;
  }
}
