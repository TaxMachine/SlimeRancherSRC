// Decompiled with JetBrains decompiler
// Type: RefineryUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefineryUI : BaseUI
{
  [Tooltip("Internal inventory content panel")]
  public GameObject inventoryGridPanel;
  public GameObject inventoryEntryPrefab;
  public Identifiable.Id[] listedItems;
  public Sprite lockedIcon;
  private MessageBundle actorBundle;
  private const int MIN_ENTRIES = 15;

  public override void Awake()
  {
    base.Awake();
    actorBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("actor");
    GadgetDirector gadgetDirector = SRSingleton<SceneContext>.Instance.GadgetDirector;
    PediaDirector pediaDirector = SRSingleton<SceneContext>.Instance.PediaDirector;
    int num = 0;
    foreach (Identifiable.Id listedItem in listedItems)
    {
      int refineryCount = gadgetDirector.GetRefineryCount(listedItem);
      Identifiable.Id id = listedItem;
      PediaDirector.Id? pediaId = pediaDirector.GetPediaId(Identifiable.IsPlort(listedItem) ? PlortToSlime(listedItem) : listedItem);
      if (refineryCount == 0 && pediaId.HasValue && !pediaDirector.IsUnlocked(pediaId.Value))
        id = Identifiable.Id.NONE;
      AddInventory(id, refineryCount);
      ++num;
    }
    for (int index = num; index < 15; ++index)
      AddEmptyInventory();
  }

  private Identifiable.Id PlortToSlime(Identifiable.Id plortId) => (Identifiable.Id) Enum.Parse(typeof (Identifiable.Id), plortId.ToString().Replace("_PLORT", "_SLIME"));

  private void AddInventory(Identifiable.Id id, int count) => CreateInventoryEntry(id, count).transform.SetParent(inventoryGridPanel.transform, false);

  private void AddEmptyInventory() => CreateEmptyInventoryEntry().transform.SetParent(inventoryGridPanel.transform, false);

  private GameObject CreateInventoryEntry(Identifiable.Id id, int count)
  {
    GameObject inventoryEntry = Instantiate(inventoryEntryPrefab);
    TMP_Text component1 = inventoryEntry.transform.Find("Content/Name").gameObject.GetComponent<TMP_Text>();
    Image component2 = inventoryEntry.transform.Find("Content/Icon").gameObject.GetComponent<Image>();
    TMP_Text component3 = inventoryEntry.transform.Find("CountsOuterPanel/CountsPanel/Counts").gameObject.GetComponent<TMP_Text>();
    if (id == Identifiable.Id.NONE)
    {
      component1.text = actorBundle.Xlate(MessageUtil.Qualify("pedia", "t.locked"));
      component2.sprite = lockedIcon;
    }
    else
    {
      Sprite icon = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(id);
      component1.text = actorBundle.Xlate("l." + id.ToString().ToLowerInvariant());
      component2.sprite = icon;
    }
    string str = count > 999 ? string.Format("{0}+", 999) : count.ToString();
    component3.text = str;
    return inventoryEntry;
  }

  private GameObject CreateEmptyInventoryEntry()
  {
    GameObject emptyInventoryEntry = Instantiate(inventoryEntryPrefab);
    TMP_Text component1 = emptyInventoryEntry.transform.Find("Content/Name").gameObject.GetComponent<TMP_Text>();
    Image component2 = emptyInventoryEntry.transform.Find("Content/Icon").gameObject.GetComponent<Image>();
    TMP_Text component3 = emptyInventoryEntry.transform.Find("CountsOuterPanel/CountsPanel/Counts").gameObject.GetComponent<TMP_Text>();
    component1.text = "";
    component2.enabled = false;
    component3.text = "";
    return emptyInventoryEntry;
  }
}
