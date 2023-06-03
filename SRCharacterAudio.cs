// Decompiled with JetBrains decompiler
// Type: SRCharacterAudio
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SRCharacterAudio : SECTR_CharacterAudio, EventHandlerRegistrable
{
  private vp_FPPlayerEventHandler playerEvents;
  private vp_FPController playerController;

  public void Awake()
  {
    playerController = GetComponent<vp_FPController>();
    playerEvents = GetComponentInChildren<vp_FPPlayerEventHandler>();
    GetComponentInChildren<vp_FPCamera>().BobStepCallback = () =>
    {
      if (!playerController.Grounded)
        return;
      OnFootstep(null);
    };
  }

  protected virtual void OnEnable()
  {
    if (!(playerEvents != null))
      return;
    Register(playerEvents);
  }

  protected virtual void OnDisable()
  {
    if (!(playerEvents != null))
      return;
    Unregister(playerEvents);
  }

  public virtual void OnStart_Jump() => OnJump(null);

  public virtual void OnMessage_FallImpact(float val)
  {
    if (val <= 0.05000000074505806)
      return;
    OnLand(null);
  }

  public void Register(vp_EventHandler eventHandler)
  {
    eventHandler.RegisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.RegisterActivity("Jump", OnStart_Jump, null, null, null, null, null);
  }

  public void Unregister(vp_EventHandler eventHandler)
  {
    eventHandler.UnregisterMessage<float>("FallImpact", OnMessage_FallImpact);
    eventHandler.UnregisterActivity("Jump", OnStart_Jump, null, null, null, null, null);
  }
}
