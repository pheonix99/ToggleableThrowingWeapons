using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.WeaponEnchants;
using Kingmaker.ResourceLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;
using ToggleableThrowingWeapons.Component;
using UnityModManagerNet;

namespace ToggleableThrowingWeapons.Content
{
    class BaseWeapons
    {


        public static void MakeBaseWeapons()
        {
            Main.TTWContext.Logger.LogHeader($"Making Base AltMode Weapons");

            string strengthGuid = "c4d213911e9616949937e1520c80aaf3";
            string stockDaggerGuid = "07cc1a7fceaee5b42b3e43da960fe76d";
            var ThrownDaggerGuid = Main.TTWContext.Blueprints.GetGUID("TTWThrownDagger");
            var ThrownDLC2DaggerGuid = Main.TTWContext.Blueprints.GetGUID("TTWThrownDLC2Dagger");

            var ThrownStarknifeGuid = Main.TTWContext.Blueprints.GetGUID("ThrownStarknife");

            BlueprintTool.AddGuidsByName(("ThrownStarknife", ThrownStarknifeGuid.ToString()));
            BlueprintTool.AddGuidsByName(("TTWThrownDLC2Dagger", ThrownDLC2DaggerGuid.ToString()));
            BlueprintTool.AddGuidsByName(("DLC2Dagger", "90ba10e82a7a4135bb380234a255f5ae"));

            BlueprintTool.AddGuidsByName(("TTWThrownDagger", ThrownDaggerGuid.ToString()), ("Dagger", stockDaggerGuid), ("Starknife", "5a939137fc039084580725b2b0845c3f"), ("Dart", "f415ae950523a7843a74d7780dd551af"));
            var Starknife = BlueprintTool.Get<BlueprintWeaponType>("Starknife");
            var Dart = BlueprintTool.Get<BlueprintWeaponType>("Dart");

            WeaponEnchantmentConfigurator.For(strengthGuid).RemoveComponents(x => x is WeaponDamageMultiplierStatReplacement).AddComponent<ThrownOnlyStatToDamage>(x =>
            {
                x.Stat = Kingmaker.EntitySystem.Stats.StatType.Strength;
                x.Multiplier = 1;
            }).Configure();

            var throwingStar = Helpers.CreateCopy<BlueprintWeaponType>(BlueprintTool.Get<BlueprintWeaponType>("Starknife"), x =>
            {
                x.AssetGuid = ThrownStarknifeGuid;
                x.name = "ThrownStarknife";

                x.m_AttackRange = new Kingmaker.Utility.Feet(20);
                x.m_AttackType = Kingmaker.RuleSystem.AttackType.Ranged;
                x.m_VisualParameters = new WeaponVisualParameters
                {
                    m_Projectiles = Dart.VisualParameters.Projectiles,
                    m_WeaponAnimationStyle = Kingmaker.View.Animation.WeaponAnimationStyle.ThrownStraight,
                    m_WeaponModel = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "6edd1e90-2239-3ef4-98f1ccb0e606a6a6" },

                    m_WeaponBeltModelOverride = new Kingmaker.ResourceLinks.PrefabLink(),
                    m_WeaponSheathModelOverride = new Kingmaker.ResourceLinks.PrefabLink(),
                    m_OverrideAttachSlots = true,
                    m_PossibleAttachSlots = new Kingmaker.View.Equipment.UnitEquipmentVisualSlotType[]
                    {
                        Kingmaker.View.Equipment.UnitEquipmentVisualSlotType.LeftBack01,
                        Kingmaker.View.Equipment.UnitEquipmentVisualSlotType.RightBack01
                    },
                    m_SoundSize = Kingmaker.Visual.Sound.WeaponSoundSizeType.Medium,
                    m_SoundType = Kingmaker.Visual.Sound.WeaponSoundType.PierceMetal,
                    m_WhooshSound = "KnifeWoosh",
                    m_MissSoundType = Kingmaker.Visual.Sound.WeaponMissSoundType.MediumMetal,
                    m_EquipSound = "Weapons_1H_Slashing_Equip_Dagger",
                    m_UnequipSound = "Weapons_1H_Slashing_Remove_Dagger",
                    m_InventoryEquipSound = "SwordEquip",
                    m_InventoryPutSound = "SwordPut",
                    m_InventoryTakeSound = "SwordTake"



                };
                x.m_FighterGroupFlags = WeaponFighterGroupFlags.BladesLight | WeaponFighterGroupFlags.Thrown;
            });
            BlueprintTools.AddBlueprint(Main.TTWContext, throwingStar);
            Main.TTWContext.Logger.Log($"About To Set  Starknife Descriptions");
            var thrownStarknifeConfig = WeaponTypeConfigurator.For(throwingStar).SetDefaultNameText(LocalizationTool.CreateString("ThrownStarknife.Name", "Thrown Starknife")).AddToEnchantments("c4d213911e9616949937e1520c80aaf3").SetDescriptionText(LocalizationTool.CreateString("ThrownStarknife.Desc", "Thrown Starknife")).Configure();

            if (Main.TTWContext.Settings.Hotswapping.IsEnabled("EnableHotswapping"))
            {
                Main.TTWContext.Logger.Log($"About To Set Thrown To Regular Starknife Link");
                thrownStarknifeConfig.AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = Starknife.ToReference<BlueprintWeaponTypeReference>();
                });




            }
            Main.TTWContext.Logger.Log($"About To Add Strength (thrown) to starknife");

           
            WeaponTypeConfigurator.For("5a939137fc039084580725b2b0845c3f").AddToEnchantments("c4d213911e9616949937e1520c80aaf3").Configure();

            

            if (Main.TTWContext.Settings.Hotswapping.IsEnabled("EnableHotswapping"))
            {
                Main.TTWContext.Logger.Log($"About To Set Regular To Thrown Starknife Link");
                BlueprintTools.GetBlueprint<BlueprintWeaponType>("5a939137fc039084580725b2b0845c3f").AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = BlueprintTool.GetRef<BlueprintWeaponTypeReference>("ThrownStarknife");
                });


            }
            
            Main.TTWContext.Blueprints.GetDerivedMaster("ThrownStarknifeMasterId");
            Main.TTWContext.Logger.LogPatch(thrownStarknifeConfig);



            var thrownDaggers = Helpers.CreateCopy<BlueprintWeaponType>(BlueprintTool.Get<BlueprintWeaponType>("Dagger"), x =>
            {
                x.name = "TTWThrownDagger";
                x.AssetGuid = ThrownDaggerGuid;
                x.m_AttackRange = new Kingmaker.Utility.Feet(20);
                x.m_AttackType = Kingmaker.RuleSystem.AttackType.Ranged;
                x.m_VisualParameters = new WeaponVisualParameters
                {
                    m_Projectiles = Dart.VisualParameters.Projectiles,
                    m_WeaponAnimationStyle = Kingmaker.View.Animation.WeaponAnimationStyle.ThrownStraight,
                    m_WeaponModel = new PrefabLink() { AssetId = "8a068458-1898-ee64-2aa771427d77f9ab" },
                    m_WeaponSheathModelOverride = new PrefabLink() { AssetId = "544192df-823e-cd74-6a0fae1c1af7de16" },
                    m_SoundSize = Kingmaker.Visual.Sound.WeaponSoundSizeType.Small,
                    m_SoundType = Kingmaker.Visual.Sound.WeaponSoundType.PierceMetal,
                    m_WhooshSound = "KnifeWoosh",

                    m_MissSoundType = Kingmaker.Visual.Sound.WeaponMissSoundType.MediumMetal,
                    m_EquipSound = "Weapons_1H_Slashing_Equip_Dagger",
                    m_UnequipSound = "Weapons_1H_Slashing_Remove_Dagger",
                    m_InventoryEquipSound = "SwordEquip",
                    m_InventoryPutSound = "DaggerPut",
                    m_InventoryTakeSound = "DaggerTake"
                };
                x.m_FighterGroupFlags = WeaponFighterGroupFlags.Thrown | WeaponFighterGroupFlags.BladesLight;

            });
           
            BlueprintTools.AddBlueprint(Main.TTWContext, thrownDaggers);
            var throwndaggerConfig = WeaponTypeConfigurator.For(thrownDaggers).AddToEnchantments("c4d213911e9616949937e1520c80aaf3").SetDefaultNameText(LocalizationTool.CreateString("TTWThrownDagger.Name", "Thrown Dagger"));
            if (Main.TTWContext.Settings.Hotswapping.IsEnabled("EnableHotswapping"))
            {
                throwndaggerConfig.AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = BlueprintTool.GetRef<BlueprintWeaponTypeReference>("Dagger");
                });

                WeaponTypeConfigurator.For("Dagger").AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = BlueprintTool.GetRef<BlueprintWeaponTypeReference>("TTWThrownDagger");
                }).Configure();
            }
            var finishedDagger = throwndaggerConfig.Configure();
           
            Main.TTWContext.Logger.LogPatch(finishedDagger);
            WeaponTypeConfigurator.For("Dagger").AddToEnchantments("c4d213911e9616949937e1520c80aaf3").Configure();


            Main.TTWContext.Blueprints.GetDerivedMaster("DaggerMasterId");
            Main.TTWContext.Blueprints.GetDerivedMaster("ThrownDaggerMasterId");

            var thrownDLC2Daggers = Helpers.CreateCopy<BlueprintWeaponType>(BlueprintTool.Get<BlueprintWeaponType>("DLC2Dagger"), x =>
            {
                x.name = "TTWThrownDLC2Dagger";
                
                x.AssetGuid = ThrownDLC2DaggerGuid;
                x.m_AttackRange = new Kingmaker.Utility.Feet(20);
                x.m_AttackType = Kingmaker.RuleSystem.AttackType.Ranged;
                x.m_VisualParameters = new WeaponVisualParameters
                {
                    m_Projectiles = Dart.VisualParameters.Projectiles,
                    m_WeaponAnimationStyle = Kingmaker.View.Animation.WeaponAnimationStyle.ThrownStraight,
                    m_WeaponModel = new PrefabLink() { AssetId = "c859a560-b553-8854-f8913486e29efc07" },
                    m_WeaponSheathModelOverride = new PrefabLink() { AssetId = "3c601278-3bba-9b54-592eb941bd30702e" },
                    m_SoundSize = Kingmaker.Visual.Sound.WeaponSoundSizeType.Small,
                    m_SoundType = Kingmaker.Visual.Sound.WeaponSoundType.PierceMetal,
                    m_WhooshSound = "KnifeWoosh",

                    m_MissSoundType = Kingmaker.Visual.Sound.WeaponMissSoundType.MediumMetal,
                    m_EquipSound = "Weapons_1H_Slashing_Equip_Dagger",
                    m_UnequipSound = "Weapons_1H_Slashing_Remove_Dagger",
                    m_InventoryEquipSound = "SwordEquip",
                    m_InventoryPutSound = "DaggerPut",
                    m_InventoryTakeSound = "DaggerTake"
                };
                x.m_FighterGroupFlags = WeaponFighterGroupFlags.Thrown | WeaponFighterGroupFlags.BladesLight;

            });
            
            BlueprintTools.AddBlueprint(Main.TTWContext, thrownDLC2Daggers);
            var thrownDLC2daggerConfig = WeaponTypeConfigurator.For(thrownDLC2Daggers).AddToEnchantments("c4d213911e9616949937e1520c80aaf3").SetDefaultNameText(LocalizationTool.CreateString("TTWThrownDLC2Dagger.Name", "Thrown Dagger"));
            if (Main.TTWContext.Settings.Hotswapping.IsEnabled("EnableHotswapping"))
            {
                thrownDLC2daggerConfig.AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = BlueprintTool.GetRef<BlueprintWeaponTypeReference>("DLC2Dagger");
                });

                WeaponTypeConfigurator.For("DLC2Dagger").AddComponent<ThrownCrossrefTypeComponent>(x =>
                {
                    x.m_OtherForm = BlueprintTool.GetRef<BlueprintWeaponTypeReference>("TTWThrownDLC2Dagger");
                }).Configure();
            }
            WeaponTypeConfigurator.For("DLC2Dagger").AddToEnchantments("c4d213911e9616949937e1520c80aaf3").Configure();
            Main.TTWContext.Blueprints.GetDerivedMaster("ThrownDLC2DaggerMasterId");
            var finishedDLC2Dagger = thrownDLC2daggerConfig.Configure();

            Main.TTWContext.Logger.LogPatch(finishedDagger);












        }


    }
}
