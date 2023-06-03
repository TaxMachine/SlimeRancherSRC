// Decompiled with JetBrains decompiler
// Type: SwitchHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SwitchHandler
{
  private Animator anim;
  private int animUpId;
  private int animUpImmediateId;
  private int animDownImmediateId;

  public SwitchHandler(Animator anim, GameObject go)
  {
    this.anim = anim;
    animUpId = Animator.StringToHash("Up");
    animUpImmediateId = Animator.StringToHash("UpImmediate");
    animDownImmediateId = Animator.StringToHash("DownImmediate");
  }

  public void SetState(State state, bool immediate)
  {
    bool flag = state == State.UP;
    if (flag == anim.GetBool(animUpId))
      return;
    anim.SetBool(animUpId, flag);
    if (!immediate)
      return;
    if (flag)
      anim.SetTrigger(animUpImmediateId);
    else
      anim.SetTrigger(animDownImmediateId);
  }

  public enum State
  {
    UP,
    DOWN,
  }

  public abstract class Switchable : SRBehaviour
  {
    public abstract void SetState(State state, bool immediate);
  }
}
