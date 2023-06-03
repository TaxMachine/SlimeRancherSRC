// Decompiled with JetBrains decompiler
// Type: SlimeAppearancePopupUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using Assets.Script.Util.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlimeAppearancePopupUI : PopupUI<SlimeAppearance>, PopupDirector.Popup
{
  [Tooltip("Lifetime of the popup (seconds).")]
  public float lifetime;
  [Tooltip("Text representing the appearance name.")]
  public TMP_Text appearanceName;
  [Tooltip("Image representing the appearance icon.")]
  public Image appearanceIcon;

  public void Awake()
  {
    SRSingleton<SceneContext>.Instance.PopupDirector.PopupActivated(this);
    Destroyer.Destroy(gameObject, lifetime, nameof (SlimeAppearancePopupUI));
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    if (!(SRSingleton<SceneContext>.Instance != null))
      return;
    SRSingleton<SceneContext>.Instance.PopupDirector.PopupDeactivated(this);
  }

  public override void Init(SlimeAppearance appearance)
  {
    base.Init(appearance);
    appearanceIcon.sprite = appearance.Icon;
  }

  public override void OnBundleAvailable(MessageDirector messageDirector) => appearanceName.text = messageDirector.GetBundle("actor").Get(idEntry.NameXlateKey);

  public bool ShouldClear() => false;

  public class PopupCreator : PopupDirector.PopupCreator
  {
    private readonly SlimeAppearance appearance;

    public PopupCreator(SlimeAppearance appearance) => this.appearance = appearance;

    public override void Create() => Instantiate(SRSingleton<SceneContext>.Instance.SlimeAppearanceDirector.appearancePopupUI).GetRequiredComponent<SlimeAppearancePopupUI>().Init(appearance);

    public override bool Equals(object other) => other is PopupCreator && ((PopupCreator) other).appearance == appearance;

    public override int GetHashCode() => appearance.GetHashCode();

    public override bool ShouldClear() => false;
  }
}
