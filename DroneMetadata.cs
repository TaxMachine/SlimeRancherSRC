// Decompiled with JetBrains decompiler
// Type: DroneMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EA3B7D-451B-4F90-8B7D-688AF16EE773
// Assembly location: W:\SteamLibrary\steamapps\common\Slime Rancher\SlimeRancher_Data\Managed\Assembly-CSharp.dll

using MonomiPark.SlimeRancher.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DroneMetadata : ScriptableObject
{
  [Header("GameObject Prefabs")]
  public DroneUI droneUI;
  public DroneUIProgram droneUIProgram;
  public DroneUIProgramPicker droneUIProgramPicker;
  public DroneUIProgramButton droneUIProgramButton;
  [Header("Program Component Images")]
  public Sprite imageTargetCollectionPlorts;
  public Sprite imageTargetCollectionFruits;
  public Sprite imageTargetCollectionVeggies;
  public Sprite imageTargetCollectionMeats;
  public Sprite imageTargetCollectionElders;
  public Sprite imageSourceCorral;
  public Sprite imageSourcePond;
  public Sprite imageSourceIncinerator;
  public Sprite imageSourceGarden;
  public Sprite imageSourceCoop;
  public Sprite imageSourceSilo;
  public Sprite imageSourcePlortCollector;
  public Sprite imageSourceOutsidePlots;
  public Sprite imageSourceFreeRange;
  public Sprite imageDestinationCorral;
  public Sprite imageDestinationSilo;
  public Sprite imageDestinationFeeder;
  public Sprite imageDestinationIncinerator;
  public Sprite imageDestinationPlortMarket;
  public Sprite imageDestinationRefinery;
  public Sprite imageNone;
  public Sprite pickTargetIcon;
  public Sprite pickSourceIcon;
  public Sprite pickDestinationIcon;
  [Header("SFX")]
  public SECTR_AudioCue onActiveCue;
  public SECTR_AudioCue onGatherBeginCue;
  public SECTR_AudioCue onGatherLoopCue;
  public SECTR_AudioCue onGatherEndCue;
  public SECTR_AudioCue onDepositBeginCue;
  public SECTR_AudioCue onDepositLoopCue;
  public SECTR_AudioCue onDepositEndCue;
  public SECTR_AudioCue onRestBeginCue;
  public SECTR_AudioCue onRestLoopCue;
  public SECTR_AudioCue onRestEndCue;
  public SECTR_AudioCue onHappyCue;
  public SECTR_AudioCue onGrumpyCue;
  public SECTR_AudioCue onBoppedCue;
  public SECTR_AudioCue onBatteryFilledCue;
  public SECTR_AudioCue onGuiEnableCue;
  public SECTR_AudioCue onGuiDisableCue;
  public SECTR_AudioCue onGuiButtonTargetCue;
  public SECTR_AudioCue onGuiButtonSourceCue;
  public SECTR_AudioCue onGuiButtonDestinationCue;
  public SECTR_AudioCue onGuiButtonActivateCue;
  public SECTR_AudioCue onGuiButtonResetCue;
  [Header("FX")]
  public GameObject onBatteryFilledFX;
  public GameObject onTeleportFX;
  [Header("Coins Override")]
  public Sprite coinsIcon;
  public Color coinsColor;
  public SECTR_AudioCue coinsCue;
  public const string TARGET_NONE_ID = "drone.target.none";
  public const string BEHAVIOUR_NONE_ID = "drone.behaviour.none";

  public void OnEnable()
  {
    targets = new Program.Target[39]
    {
      new Program.Target.Collection("plorts", Identifiable.PLORT_CLASS, imageTargetCollectionPlorts),
      new Program.Target.Collection("veggies", Identifiable.VEGGIE_CLASS, imageTargetCollectionVeggies),
      new Program.Target.Collection("fruits", Identifiable.FRUIT_CLASS, imageTargetCollectionFruits),
      new Program.Target.Collection("meats", Identifiable.MEAT_CLASS, imageTargetCollectionMeats),
      new Program.Target.Collection("elders", Identifiable.ELDER_CLASS, imageTargetCollectionElders),
      new Program.Target.Basic(Identifiable.Id.PINK_PLORT),
      new Program.Target.Basic(Identifiable.Id.ROCK_PLORT),
      new Program.Target.Basic(Identifiable.Id.TABBY_PLORT),
      new Program.Target.Basic(Identifiable.Id.PHOSPHOR_PLORT),
      new Program.Target.Basic(Identifiable.Id.RAD_PLORT),
      new Program.Target.Basic(Identifiable.Id.BOOM_PLORT),
      new Program.Target.Basic(Identifiable.Id.HONEY_PLORT),
      new Program.Target.Basic(Identifiable.Id.PUDDLE_PLORT),
      new Program.Target.Basic(Identifiable.Id.CRYSTAL_PLORT),
      new Program.Target.Basic(Identifiable.Id.HUNTER_PLORT),
      new Program.Target.Basic(Identifiable.Id.QUANTUM_PLORT),
      new Program.Target.Basic(Identifiable.Id.MOSAIC_PLORT),
      new Program.Target.Basic(Identifiable.Id.DERVISH_PLORT),
      new Program.Target.Basic(Identifiable.Id.TANGLE_PLORT),
      new Program.Target.Basic(Identifiable.Id.FIRE_PLORT),
      new Program.Target.Basic(Identifiable.Id.SABER_PLORT),
      new Program.Target.Basic(Identifiable.Id.CARROT_VEGGIE),
      new Program.Target.Basic(Identifiable.Id.OCAOCA_VEGGIE),
      new Program.Target.Basic(Identifiable.Id.BEET_VEGGIE),
      new Program.Target.Basic(Identifiable.Id.PARSNIP_VEGGIE),
      new Program.Target.Basic(Identifiable.Id.ONION_VEGGIE),
      new Program.Target.Basic(Identifiable.Id.POGO_FRUIT),
      new Program.Target.Basic(Identifiable.Id.MANGO_FRUIT),
      new Program.Target.Basic(Identifiable.Id.CUBERRY_FRUIT),
      new Program.Target.Basic(Identifiable.Id.LEMON_FRUIT),
      new Program.Target.Basic(Identifiable.Id.PEAR_FRUIT),
      new Program.Target.Basic(Identifiable.Id.HEN),
      new Program.Target.Basic(Identifiable.Id.ROOSTER),
      new Program.Target.Basic(Identifiable.Id.STONY_HEN),
      new Program.Target.Basic(Identifiable.Id.BRIAR_HEN),
      new Program.Target.Basic(Identifiable.Id.PAINTED_HEN),
      new Program.Target.Basic(Identifiable.Id.ELDER_HEN),
      new Program.Target.Basic(Identifiable.Id.ELDER_ROOSTER),
      new Program.Target.Basic(Identifiable.Id.SPICY_TOFU)
    };
    DroneMetadata.Program.Behaviour[] behaviourArray1 = new DroneMetadata.Program.Behaviour[9];
    DroneMetadata.Program.Behaviour behaviour1 = new DroneMetadata.Program.Behaviour();
    behaviour1.id = "m.drone.source.name.corral";
    behaviour1.image = imageSourceCorral;
    behaviour1.types = new Type[1]
    {
      typeof (DroneProgramSourceCorral)
    };
    behaviour1.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    behaviourArray1[0] = behaviour1;
    DroneMetadata.Program.Behaviour behaviour2 = new DroneMetadata.Program.Behaviour();
    behaviour2.id = "m.drone.source.name.pond";
    behaviour2.image = imageSourcePond;
    behaviour2.types = new Type[1]
    {
      typeof (DroneProgramSourcePond)
    };
    behaviour2.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    behaviourArray1[1] = behaviour2;
    DroneMetadata.Program.Behaviour behaviour3 = new DroneMetadata.Program.Behaviour();
    behaviour3.id = "m.drone.source.name.incinerator";
    behaviour3.image = imageSourceIncinerator;
    behaviour3.types = new Type[1]
    {
      typeof (DroneProgramSourceIncinerator)
    };
    behaviour3.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    behaviourArray1[2] = behaviour3;
    DroneMetadata.Program.Behaviour behaviour4 = new DroneMetadata.Program.Behaviour();
    behaviour4.id = "m.drone.source.name.garden";
    behaviour4.image = imageSourceGarden;
    behaviour4.types = new Type[1]
    {
      typeof (DroneProgramSourceGarden)
    };
    behaviour4.isCompatible = p => Identifiable.IsFruit(p.target.ident) || Identifiable.IsVeggie(p.target.ident);
    behaviourArray1[3] = behaviour4;
    DroneMetadata.Program.Behaviour behaviour5 = new DroneMetadata.Program.Behaviour();
    behaviour5.id = "m.drone.source.name.coop";
    behaviour5.image = imageSourceCoop;
    behaviour5.isCompatible = p => Identifiable.IsAnimal(p.target.ident);
    behaviour5.types = new Type[2]
    {
      typeof (DroneProgramSourceElderCollector),
      typeof (DroneProgramSourceCoop)
    };
    behaviourArray1[4] = behaviour5;
    DroneMetadata.Program.Behaviour behaviour6 = new DroneMetadata.Program.Behaviour();
    behaviour6.id = "m.drone.source.name.silo";
    behaviour6.image = imageSourceSilo;
    behaviour6.types = new Type[1]
    {
      typeof (DroneProgramSourceSilo)
    };
    behaviourArray1[5] = behaviour6;
    DroneMetadata.Program.Behaviour behaviour7 = new DroneMetadata.Program.Behaviour();
    behaviour7.id = "m.drone.source.name.plort_collector";
    behaviour7.image = imageSourcePlortCollector;
    behaviour7.types = new Type[1]
    {
      typeof (DroneProgramSourcePlortCollector)
    };
    behaviour7.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    behaviourArray1[6] = behaviour7;
    DroneMetadata.Program.Behaviour behaviour8 = new DroneMetadata.Program.Behaviour();
    behaviour8.id = "m.drone.source.name.dynamic";
    behaviour8.image = imageSourceOutsidePlots;
    behaviour8.types = new Type[1]
    {
      typeof (DroneProgramSourceOutsidePlots)
    };
    behaviour8.isCompatible = p => !Identifiable.IsPlort(p.target.ident);
    behaviourArray1[7] = behaviour8;
    DroneMetadata.Program.Behaviour behaviour9 = new DroneMetadata.Program.Behaviour();
    behaviour9.id = "m.drone.source.name.free_range";
    behaviour9.image = imageSourceFreeRange;
    behaviour9.types = new Type[1]
    {
      typeof (DroneProgramSourceFreeRange)
    };
    behaviour9.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    behaviourArray1[8] = behaviour9;
    sources = behaviourArray1;
    DroneMetadata.Program.Behaviour[] behaviourArray2 = new DroneMetadata.Program.Behaviour[6];
    DroneMetadata.Program.Behaviour behaviour10 = new DroneMetadata.Program.Behaviour();
    behaviour10.id = "m.drone.destination.name.corral";
    behaviour10.image = imageDestinationCorral;
    behaviour10.types = new Type[1]
    {
      typeof (DroneProgramDestinationCorral)
    };
    behaviour10.isCompatible = p => Identifiable.IsFood(p.target.ident);
    behaviourArray2[0] = behaviour10;
    DroneMetadata.Program.Behaviour behaviour11 = new DroneMetadata.Program.Behaviour();
    behaviour11.id = "m.drone.destination.name.silo";
    behaviour11.image = imageDestinationSilo;
    behaviour11.types = new Type[1]
    {
      typeof (DroneProgramDestinationSilo)
    };
    behaviourArray2[1] = behaviour11;
    DroneMetadata.Program.Behaviour behaviour12 = new DroneMetadata.Program.Behaviour();
    behaviour12.id = "m.drone.destination.name.auto_feeder";
    behaviour12.image = imageDestinationFeeder;
    behaviour12.types = new Type[1]
    {
      typeof (DroneProgramDestinationFeeder)
    };
    behaviour12.isCompatible = p => Identifiable.IsFood(p.target.ident);
    behaviourArray2[2] = behaviour12;
    DroneMetadata.Program.Behaviour behaviour13 = new DroneMetadata.Program.Behaviour();
    behaviour13.id = "m.drone.destination.name.incinerator";
    behaviour13.image = imageDestinationIncinerator;
    behaviour13.types = new Type[1]
    {
      typeof (DroneProgramDestinationIncinerator)
    };
    behaviour13.isCompatible = p => Identifiable.IsFood(p.target.ident);
    behaviourArray2[3] = behaviour13;
    Program.GadgetBehaviour gadgetBehaviour1 = new Program.GadgetBehaviour();
    gadgetBehaviour1.id = "m.drone.destination.name.plort_market";
    gadgetBehaviour1.image = imageDestinationPlortMarket;
    gadgetBehaviour1.types = new Type[1]
    {
      typeof (DroneProgramDestinationPlortMarket)
    };
    gadgetBehaviour1.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    gadgetBehaviour1.gadget = Gadget.Id.MARKET_LINK;
    gadgetBehaviour1.gadgetPredicate = () => SRSingleton<SceneContext>.Instance.Player.GetComponent<RegionMember>().regions.All(r => r.gameObject.name != "cellRanch_Home");
    behaviourArray2[4] = gadgetBehaviour1;
    Program.GadgetBehaviour gadgetBehaviour2 = new Program.GadgetBehaviour();
    gadgetBehaviour2.id = "m.drone.destination.name.refinery";
    gadgetBehaviour2.image = imageDestinationRefinery;
    gadgetBehaviour2.types = new Type[1]
    {
      typeof (DroneProgramDestinationRefinery)
    };
    gadgetBehaviour2.isCompatible = p => Identifiable.IsPlort(p.target.ident);
    gadgetBehaviour2.gadget = Gadget.Id.REFINERY_LINK;
    gadgetBehaviour2.gadgetPredicate = () => SRSingleton<SceneContext>.Instance.Player.GetComponent<RegionMember>().regions.All(r => r.gameObject.name != "cellRanch_Lab");
    behaviourArray2[5] = gadgetBehaviour2;
    destinations = behaviourArray2;
  }

  public Program.Target GetDefaultTarget()
  {
    Program.Target defaultTarget = new Program.Target();
    defaultTarget.id = "drone.target.none";
    defaultTarget.image = imageNone;
    defaultTarget.predicate = id => false;
    return defaultTarget;
  }

  public DroneMetadata.Program.Behaviour GetDefaultBehaviour()
  {
    DroneMetadata.Program.Behaviour defaultBehaviour = new DroneMetadata.Program.Behaviour();
    defaultBehaviour.id = "drone.behaviour.none";
    defaultBehaviour.image = imageNone;
    defaultBehaviour.types = new Type[0];
    return defaultBehaviour;
  }

  public Program.Target[] targets { get; private set; }

  public DroneMetadata.Program.Behaviour[] sources { get; private set; }

  public DroneMetadata.Program.Behaviour[] destinations { get; private set; }

  public class Program
  {
    public Target target;
    public Behaviour source;
    public Behaviour destination;

    public Program()
    {
    }

    public Program(
      Target target,
      Behaviour source,
      Behaviour destination)
    {
      this.target = target;
      this.source = source;
      this.destination = destination;
    }

    public Program Clone() => new Program(target, source, destination);

    public bool IsComplete() => target.id != "drone.target.none" && source.id != "drone.behaviour.none" && destination.id != "drone.behaviour.none";

    public bool IsReset() => target.id == "drone.target.none" && source.id == "drone.behaviour.none" && destination.id == "drone.behaviour.none";

    public abstract class BaseComponent
    {
      public string id;
      public Sprite image;

      public virtual string GetName() => SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("ui").Get(id);

      public virtual Sprite GetImage() => image;
    }

    public class Target : BaseComponent
    {
      public Identifiable.Id ident;
      public Predicate<Identifiable.Id> predicate;

      public class Basic : Target
      {
        public Basic(Identifiable.Id ident)
        {
          id = string.Format("m.drone.target.identifiable.{0}", Enum.GetName(typeof (Identifiable.Id), ident).ToLowerInvariant());
          this.ident = ident;
          predicate = rhs => rhs == ident;
        }

        public override string GetName() => Identifiable.GetName(ident);

        public override Sprite GetImage() => SRSingleton<GameContext>.Instance.LookupDirector.GetIcon(ident);
      }

      public class Collection : Target
      {
        public HashSet<Identifiable.Id> collection;

        public Collection(string id, HashSet<Identifiable.Id> collection, Sprite image)
        {
          this.id = string.Format("m.drone.target.name.category_{0}", id);
          ident = collection.First();
          predicate = rhs => collection.Contains(rhs);
          this.collection = collection;
          this.image = image;
        }
      }
    }

    public class Behaviour : BaseComponent
    {
      public Type[] types;
      public Predicate<Program> isCompatible = p => true;
    }

    public class GadgetBehaviour : Behaviour
    {
      public Func<bool> gadgetPredicate;
      public Gadget.Id gadget;

      public override string GetName() => gadgetPredicate() ? SRSingleton<GameContext>.Instance.MessageDirector.GetBundle("pedia").Get(string.Format("m.gadget.name.{0}", gadget.ToString().ToLowerInvariant())) : base.GetName();

      public override Sprite GetImage() => gadgetPredicate() ? SRSingleton<GameContext>.Instance.LookupDirector.GetGadgetDefinition(gadget).icon : base.GetImage();
    }
  }
}
