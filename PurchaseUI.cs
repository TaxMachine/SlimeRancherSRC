// Decompiled with JetBrains decompiler
// Type: PurchaseUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Assets.Script.Util.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PurchaseUI : BaseUI
{
  [Tooltip("Internal title image")]
  public Image titleImg;
  [Tooltip("Internal title text")]
  public TMP_Text titleText;
  [Tooltip("Internal button content panel")]
  public GameObject buttonListPanel;
  [Tooltip("Internal unavailable-item button content panel")]
  public GameObject unavailButtonListPanel;
  [Tooltip("Internal cost content panel")]
  public GameObject costListPanel;
  [Tooltip("Internal selected panel title image")]
  public Image selectedImg;
  [Tooltip("Internal selected panel title text")]
  public TMP_Text selectedTitle;
  [Tooltip("Internal selected panel description text")]
  public TMP_Text selectedDesc;
  [Tooltip("Internal selected panel purchase cost text")]
  public TMP_Text selectedCost;
  [Tooltip("Internal selected panel purchase cost panel")]
  public GameObject selectedCostPanel;
  [Tooltip("Internal selected panel purchase no-cost placeholder panel")]
  public GameObject selectedNoCostPanel;
  [Tooltip("Internal selected panel pedia button")]
  public Button selectedPediaBtn;
  [Tooltip("Internal selected panel purchase button")]
  public Button purchaseBtn;
  [Tooltip("Internal selected panel purchase button text")]
  public TMP_Text purchaseBtnText;
  [Tooltip("Internal hold to purchase button (for example, used when demolishing silos).")]
  public Button holdToPurchaseBtn;
  [Tooltip("Internal hold to purchase button text (for example, used when demolishing silos).")]
  public TMP_Text holdToPurchaseBtnText;
  [Tooltip("Internal main action right-side panel")]
  public GameObject actionPanel;
  [Tooltip("Internal placeholder right-side panel")]
  public GameObject placeholderPanel;
  [Tooltip("Internal panel for type-specific customizations")]
  public GameObject customizationPanel;
  [Tooltip("Internal costs panel far-right, not always active.")]
  public GameObject costsPanel;
  [Tooltip("Internal category tabs panel.")]
  public GameObject tabsPanel;
  [Tooltip("Internal item warning text.")]
  public TMP_Text warningText;
  [Tooltip("Internal scrolling region for selection list.")]
  public ScrollRect selectionScroller;
  [Tooltip("Internal loading panel.")]
  public GameObject loadingPanel;
  [Tooltip("Internal selection panel.")]
  public GameObject selectionPanel;
  public GameObject buttonListItemPrefab;
  public GameObject costListItemPrefab;
  public GameObject catTabPrefab;
  public Material unavailIconMat;
  public GameObject purchaseFX;
  private Purchasable selected;
  private Dictionary<string, Category> categories;
  private Category selectedCategory;
  public OnSelected onSelected;
  private OnClose onClose;
  private MessageBundle pediaBundle;
  private MessageBundle actorBundle;
  private Dictionary<Toggle, Purchasable> toggleMap = new Dictionary<Toggle, Purchasable>();
  private Dictionary<string, Toggle> categoryMap = new Dictionary<string, Toggle>();
  private Dictionary<Purchasable, GameObject> buttonDict = new Dictionary<Purchasable, GameObject>();
  private bool hideNubuckCosts;
  private bool waitForPedia;
  private string purchaseMsg = "b.purchase";
  private string purchaseUnavailMsg = "b.sold_out";

  public override void Awake()
  {
    base.Awake();
    pediaBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia");
    actorBundle = SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("actor");
    actionPanel.SetActive(false);
    placeholderPanel.SetActive(true);
  }

  public void Init(Sprite icon, string titleKey, OnClose onClose)
  {
    titleImg.sprite = icon;
    titleText.text = pediaBundle.Xlate(titleKey);
    this.onClose = onClose;
    toggleMap.Clear();
  }

  public void PlayPurchaseFX()
  {
    if (!(purchaseFX != null))
      return;
    Instantiate(purchaseFX).transform.SetParent(purchaseBtn.transform, false);
  }

  public void SetPurchaseMsgs(string availMsg, string unavailMsg)
  {
    purchaseMsg = availMsg;
    purchaseUnavailMsg = unavailMsg;
    if (selected == null)
      return;
    Select(selected);
  }

  public void AddButton(Purchasable purchasable, bool unavailInMainList)
  {
    GameObject button = CreateButton(purchasable);
    if (purchasable.avail() | unavailInMainList)
      button.transform.SetParent(buttonListPanel.transform, false);
    else
      button.transform.SetParent(unavailButtonListPanel.transform, false);
    button.SetActive(purchasable.unlocked());
    button.GetRequiredComponent<Toggle>().group = buttonListPanel.GetRequiredComponentInParent<ToggleGroup>(true);
    buttonDict[purchasable] = button;
  }

  public void ClearButtons()
  {
    foreach (UnityEngine.Object instance in buttonDict.Values)
      Destroyer.Destroy(instance, "PurchaseUI.ClearButtons");
  }

  public void Rebuild(bool unavailInMainList)
  {
    foreach (KeyValuePair<Purchasable, GameObject> keyValuePair in buttonDict)
    {
      UpdateButton(keyValuePair.Key, keyValuePair.Value);
      keyValuePair.Value.SetActive(keyValuePair.Key.unlocked());
      Transform parent = keyValuePair.Key.avail() | unavailInMainList ? buttonListPanel.transform : unavailButtonListPanel.transform;
      if (keyValuePair.Value.transform.parent != parent)
        keyValuePair.Value.transform.SetParent(parent, false);
    }
    if (selectedCategory != null)
      ActivateCategory(selectedCategory);
    if (buttonDict.ContainsKey(selected) && selected.avail() | unavailInMainList)
      buttonDict[selected].GetComponent<Toggle>().Select();
    else
      SelectFirst();
  }

  public void HideSelectionPanel() => selectionPanel.SetActive(false);

  private void SetCosts(GadgetDefinition.CraftCost[] costs)
  {
    costsPanel.SetActive(true);
    ClearCostListPanel(true);
    foreach (GadgetDefinition.CraftCost cost in costs)
      CreateCost(cost).transform.SetParent(costListPanel.transform, false);
  }

  private void ClearCostListPanel(bool isRequiresTextEnabled)
  {
    costListPanel.transform.GetChild(0).gameObject.SetActive(isRequiresTextEnabled);
    for (int index = 1; index < costListPanel.transform.childCount; ++index)
      Destroyer.Destroy(costListPanel.transform.GetChild(index).gameObject, "PurchaseUI.SetCosts");
  }

  public void HideNubuckCost() => hideNubuckCosts = true;

  private GameObject CreateButton(Purchasable purchasable)
  {
    GameObject buttonObj = Instantiate(buttonListItemPrefab);
    UpdateButton(purchasable, buttonObj);
    Toggle component = buttonObj.GetComponent<Toggle>();
    component.onValueChanged.AddListener(isOn =>
    {
      if (!isOn)
        return;
      Select(purchasable);
    });
    OnSelectDelegator.Create(buttonObj, () => buttonObj.GetComponent<Toggle>().isOn = true);
    toggleMap[component] = purchasable;
    return buttonObj;
  }

  private void UpdateButton(Purchasable purchasable, GameObject buttonObj)
  {
    MeshToggleButtonStyler component1 = buttonObj.GetComponent<MeshToggleButtonStyler>();
    TMP_Text component2 = buttonObj.transform.Find("Content/Name").gameObject.GetComponent<TMP_Text>();
    Image component3 = buttonObj.transform.Find("Content/Icon").gameObject.GetComponent<Image>();
    TMP_Text component4 = buttonObj.transform.Find("Content/Count").gameObject.GetComponent<TMP_Text>();
    string str = pediaBundle.Xlate(purchasable.nameKey);
    component2.text = str;
    component3.sprite = purchasable.icon;
    int num = purchasable.currCount == null ? -1 : purchasable.currCount();
    component4.text = uiBundle.Xlate(MessageUtil.Tcompose("l.curr_count", num));
    component4.gameObject.SetActive(num >= 0);
    if (!purchasable.avail())
    {
      component1.ChangeStyle("ListEntryUnavail");
      component3.material = unavailIconMat;
    }
    else
    {
      component1.ChangeStyle("ListEntry");
      component3.material = null;
    }
  }

  private GameObject CreateCost(GadgetDefinition.CraftCost cost)
  {
    GameObject cost1 = Instantiate(costListItemPrefab);
    TMP_Text component1 = cost1.transform.Find("Content/Name").gameObject.GetComponent<TMP_Text>();
    Image component2 = cost1.transform.Find("Content/Icon").gameObject.GetComponent<Image>();
    TMP_Text component3 = cost1.transform.Find("CountsOuterPanel/CountsPanel/Counts").gameObject.GetComponent<TMP_Text>();
    Sprite icon = SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(cost.id);
    int refineryCount = SRSingleton<SceneContext>.Instance.GadgetDirector.GetRefineryCount(cost.id);
    component1.text = actorBundle.Xlate("l." + cost.id.ToString().ToLowerInvariant());
    component2.sprite = icon;
    component3.text = uiBundle.Xlate(MessageUtil.Tcompose("m.count_of_required", refineryCount, cost.amount));
    if (refineryCount < cost.amount)
      component3.color = Color.red;
    return cost1;
  }

  public void Select(Purchasable purchasable)
  {
    actionPanel.SetActive(true);
    placeholderPanel.SetActive(false);
    selected = purchasable;
    selectedImg.sprite = purchasable.mainImg;
    selectedTitle.text = pediaBundle.Xlate(purchasable.nameKey);
    selectedDesc.text = pediaBundle.Xlate(purchasable.descKey);
    selectedCost.text = purchasable.cost.ToString();
    selectedCostPanel.SetActive(purchasable.cost > 0 && !hideNubuckCosts);
    selectedNoCostPanel.SetActive(purchasable.cost <= 0 && !hideNubuckCosts);
    selectedPediaBtn.gameObject.SetActive(purchasable.pediaId.HasValue);
    SetupActivePurchaseButton(purchasable);
    if (purchasable.craftCosts != null)
      SetCosts(purchasable.craftCosts);
    warningText.gameObject.SetActive(purchasable.warning != null);
    if (purchasable.warning != null)
      warningText.text = uiBundle.Xlate(purchasable.warning);
    if (onSelected != null)
      onSelected(purchasable);
    if (purchasable.onSelected == null)
      return;
    purchasable.onSelected(purchasable);
  }

  private void SetupActivePurchaseButton(Purchasable purchasable)
  {
    string str = uiBundle.Xlate(purchasable.btnOverride != null ? purchasable.btnOverride : (purchasable.avail() ? purchaseMsg : purchaseUnavailMsg));
    bool flag = purchasable.avail();
    if (purchasable.requireHoldToPurchase)
    {
      purchaseBtn.gameObject.SetActive(false);
      holdToPurchaseBtn.gameObject.SetActive(true);
      holdToPurchaseBtn.interactable = flag;
      holdToPurchaseBtnText.text = str;
    }
    else
    {
      purchaseBtn.gameObject.SetActive(true);
      holdToPurchaseBtn.gameObject.SetActive(false);
      purchaseBtn.interactable = flag;
      purchaseBtnText.text = str;
    }
  }

  public void SetCategories(List<Category> categories)
  {
    tabsPanel.SetActive(true);
    Toggle toggle = null;
    this.categories = new Dictionary<string, Category>();
    foreach (Category category in categories)
      this.categories[category.name] = category;
    selectedCategory = categories.Count > 0 ? categories[0] : null;
    foreach (Category category in categories)
    {
      GameObject gameObject = Instantiate(catTabPrefab);
      gameObject.transform.SetParent(tabsPanel.transform, false);
      gameObject.GetComponentInChildren<XlateText>().SetKey("b." + category.name);
      Toggle component = gameObject.GetComponent<Toggle>();
      if (toggle == null)
        toggle = component;
      component.group = tabsPanel.GetComponent<ToggleGroup>();
      Category fCategory = category;
      component.onValueChanged.AddListener(isOn =>
      {
        if (!isOn)
          return;
        ActivateCategory(fCategory);
        SelectFirst();
      });
      categoryMap[category.name] = component;
    }
    if (!(toggle != null))
      return;
    toggle.isOn = true;
  }

  private void ActivateCategory(Category category)
  {
    bool flag1 = false;
    foreach (KeyValuePair<Toggle, Purchasable> toggle in toggleMap)
    {
      bool flag2 = Array.IndexOf(category.items, toggle.Value) != -1 && toggle.Value.unlocked();
      toggle.Key.gameObject.SetActive(flag2);
      toggle.Key.isOn = false;
      flag1 |= flag2;
    }
    actionPanel.SetActive(flag1);
    placeholderPanel.SetActive(!flag1);
    selectedCategory = category;
  }

  public void SelectFirst()
  {
    for (int index = 0; index < buttonListPanel.transform.childCount; ++index)
    {
      GameObject gameObject = buttonListPanel.transform.GetChild(index).gameObject;
      if (gameObject.activeSelf)
      {
        gameObject.GetComponent<Toggle>().Select();
        return;
      }
    }
    for (int index = 0; index < unavailButtonListPanel.transform.childCount; ++index)
    {
      GameObject gameObject = unavailButtonListPanel.transform.GetChild(index).gameObject;
      if (gameObject.activeSelf)
      {
        gameObject.GetComponent<Toggle>().Select();
        return;
      }
    }
    actionPanel.SetActive(false);
    placeholderPanel.SetActive(true);
    ClearCostListPanel(false);
    selected = null;
  }

  public void Pedia()
  {
    if (waitForPedia || selected == null || !selected.pediaId.HasValue)
      return;
    waitForPedia = true;
    PediaUI component = SRSingleton<SceneContext>.Instance.PediaDirector.ShowPedia(selected.pediaId.Value).GetComponent<PediaUI>();
    component.onDestroy = component.onDestroy + (() =>
    {
      if (!(SRSingleton<SceneContext>.Instance != null))
        return;
      ReselectOnReturnFromPedia();
      waitForPedia = false;
    });
  }

  public void Purchase()
  {
    if (waitForPedia || selected == null)
      return;
    selected.onPurchase();
  }

  public override void Close()
  {
    base.Close();
    if (onClose == null)
      return;
    onClose();
  }

  public Category GetSelectedCategory() => selectedCategory;

  public void Resize(float widthSelection, float widthAction)
  {
    selectionScroller.GetComponent<LayoutElement>().preferredWidth = widthSelection;
    actionPanel.GetComponent<LayoutElement>().preferredWidth = widthAction;
    loadingPanel.GetComponent<LayoutElement>().preferredWidth = widthSelection + widthAction;
  }

  public void ReselectOnReturnFromPedia()
  {
    if (selected != null && buttonDict.ContainsKey(selected))
      buttonDict[selected].GetComponent<Toggle>().Select();
    else
      SelectFirst();
  }

  public void SetActivePanels(Panel active)
  {
    selectionPanel.SetActive((active & Panel.SELECTION) != 0);
    actionPanel.SetActive((active & Panel.ACTION) != 0);
    placeholderPanel.SetActive((active & Panel.PLACEHOLDER) != 0);
    costsPanel.SetActive((active & Panel.COSTS) != 0);
    loadingPanel.SetActive((active & Panel.LOADING) != 0);
  }

  public delegate void OnClose();

  public delegate void OnSelected(Purchasable purchasable);

  public class Category
  {
    public string name;
    public Purchasable[] items;

    public Category(string name, params Purchasable[] items)
    {
      this.name = name;
      this.items = items;
    }
  }

  public class Purchasable
  {
    public string nameKey;
    public Sprite icon;
    public Sprite mainImg;
    public string descKey;
    public int cost;
    public PediaDirector.Id? pediaId;
    public UnityAction onPurchase;
    public Func<bool> unlocked;
    public Func<bool> avail;
    public string btnOverride;
    public string warning;
    public Func<int> currCount;
    public GadgetDefinition.CraftCost[] craftCosts;
    public OnSelected onSelected;
    public bool requireHoldToPurchase;

    public Purchasable(
      string nameKey,
      Sprite icon,
      Sprite mainImg,
      string descKey,
      int cost,
      PediaDirector.Id? pediaId,
      UnityAction onPurchase,
      Func<bool> unlocked,
      Func<bool> avail,
      string btnOverride = null,
      string warning = null,
      Func<int> currCount = null,
      GadgetDefinition.CraftCost[] craftCosts = null,
      bool requireHoldToPurchase = false)
    {
      this.nameKey = nameKey;
      this.icon = icon;
      this.mainImg = mainImg;
      this.descKey = descKey;
      this.cost = cost;
      this.pediaId = pediaId;
      this.onPurchase = onPurchase;
      this.unlocked = unlocked;
      this.avail = avail;
      this.btnOverride = btnOverride;
      this.warning = warning;
      this.currCount = currCount;
      this.craftCosts = craftCosts;
      this.requireHoldToPurchase = requireHoldToPurchase;
    }

    public Purchasable()
    {
    }
  }

  [Flags]
  public enum Panel
  {
    NONE = 0,
    SELECTION = 1,
    ACTION = 2,
    PLACEHOLDER = 4,
    COSTS = 8,
    LOADING = 16, // 0x00000010
    DEFAULT = ACTION | SELECTION, // 0x00000003
  }
}
