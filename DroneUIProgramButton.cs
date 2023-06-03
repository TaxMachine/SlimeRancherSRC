// Decompiled with JetBrains decompiler
// Type: DroneUIProgramButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DroneUIProgramButton : MonoBehaviour
{
  [Tooltip("Selection title.")]
  public TMP_Text title;
  [Tooltip("Selection name.")]
  public TMP_Text name;
  [Tooltip("Selection button.")]
  public Button button;
  [Tooltip("Selection image.")]
  public Image image;
  private Title titleMetadata;

  public void OnDestroy()
  {
    if (!(SRSingleton<GameContext>.Instance != null))
      return;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnMessageBundlesChanged);
  }

  public DroneUIProgramButton Init(
    DroneMetadata.Program.BaseComponent element,
    Title title = null)
  {
    name.text = element.GetName();
    image.sprite = element.GetImage();
    titleMetadata = title;
    SRSingleton<GameContext>.Instance.MessageDirector.UnregisterBundlesListener(OnMessageBundlesChanged);
    SRSingleton<GameContext>.Instance.MessageDirector.RegisterBundlesListener(OnMessageBundlesChanged);
    return this;
  }

  private void OnMessageBundlesChanged(MessageDirector messageDirector)
  {
    if (titleMetadata != null)
    {
      title.gameObject.SetActive(true);
      title.text = string.Format("{0}{1}", messageDirector.GetBundle("ui").Get(string.Format("l.drone.{0}", titleMetadata.type.ToString().ToLowerInvariant())), titleMetadata.index.HasValue ? string.Format(" {0}", titleMetadata.index.Value) : (object) string.Empty);
    }
    else
      title.gameObject.SetActive(false);
  }

  public class Title
  {
    public Type type;
    public int? index;

    public enum Type
    {
      TARGET,
      SOURCE,
      DESTINATION,
    }
  }
}
