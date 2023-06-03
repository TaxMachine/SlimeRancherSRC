// Decompiled with JetBrains decompiler
// Type: GadgetPortableSlimeBaitAttractor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using System.Linq;
using UnityEngine;

public class GadgetPortableSlimeBaitAttractor : Attractor
{
  private Identifiable.Id bait;

  public void Awake()
  {
    switch (GetComponentInParent<Gadget>().id)
    {
      case Gadget.Id.PORTABLE_SLIME_BAIT_FRUIT:
        bait = Identifiable.FRUIT_CLASS.First();
        break;
      case Gadget.Id.PORTABLE_SLIME_BAIT_VEGGIE:
        bait = Identifiable.VEGGIE_CLASS.First();
        break;
      case Gadget.Id.PORTABLE_SLIME_BAIT_MEAT:
        bait = Identifiable.MEAT_CLASS.First();
        break;
      default:
        Log.Error("Failed to get bait type for GadgetPortableSlimeBaitAttractor.", "gadget", GetComponentInParent<Gadget>().id);
        break;
    }
  }

  public override float AweFactor(GameObject slime)
  {
    SlimeEat component = slime.GetComponent<SlimeEat>();
    return !(component != null) || !component.enabled || !component.DoesEat(bait) ? 0.0f : 1f;
  }

  public override bool CauseMoveTowards() => true;
}
