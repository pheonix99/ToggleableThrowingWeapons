using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;
using ToggleableThrowingWeapons.Component;
using ToggleableThrowingWeapons.Resources;

namespace ToggleableThrowingWeapons.Content
{
    public static class ModeSwitching
    {
        public static void CreateModeSwitchAbilities()
        {
            Main.TTWContext.Logger.LogHeader("Creating Mode Switching Ability");
            var buffBP = Helpers.CreateBlueprint<BlueprintBuff>(Main.TTWContext, "ThrowWeaponsBuff");

            var buffDesc = LocalizationTool.CreateString("ThrowWeaponsBuff.Desc", "Use Daggers And Starknives As Ranged Weapons");
            var buffconfig = BuffConfigurator.For(buffBP).SetDisplayName(LocalizationTool.CreateString("ThrowWeaponsBuff.Name", "Throw Weapons")).SetDescription(buffDesc).SetDescriptionShort(buffDesc);

            buffconfig.AddComponent<EnableThrownMode>();
        
            buffconfig.SetIcon(BlueprintTool.Get<BlueprintWeaponType>("Dagger").Icon);
            var builtBuff = buffconfig.Configure();
            Main.TTWContext.Logger.LogPatch(builtBuff);

            var toggleBP = Helpers.CreateBlueprint<BlueprintActivatableAbility>(Main.TTWContext, "ThrowWeaponsToggle");
            var desc = LocalizationTool.CreateString("ThrowWeaponsToggle.Desc", "Use Daggers And Starknives As Ranged Weapons");
            var toglleCOnfig = ActivatableAbilityConfigurator.For(toggleBP).SetDisplayName(LocalizationTool.CreateString("ThrowWeaponsToggle.Name", "Throw Weapons")).SetDescription(desc).SetDescriptionShort(desc);
            toglleCOnfig.SetActivationType(AbilityActivationType.Immediately);
            toglleCOnfig.SetDeactivateIfCombatEnded(false);
            toglleCOnfig.SetDeactivateImmediately(true);
            toglleCOnfig.SetBuff(buffBP.AssetGuidThreadSafe); 

            toglleCOnfig.SetIcon(BlueprintTool.Get<BlueprintWeaponType>("Dagger").Icon);
            var togglebuild = toglleCOnfig.Configure();

            /*var toggleFeatureBP = Helpers.CreateBlueprint<BlueprintFeature>(Main.TTWContext, "ThrowWeaponsToggleFeature");
            var toggleFeatureBPdesc = LocalizationTool.CreateString("ThrowWeaponsToggle.Desc", "Start Throwing Things At People");
            var toggleFeatureConfig = FeatureConfigurator.For(toggleFeatureBP).SetDisplayName(LocalizationTool.CreateString("ThrowWeaponsToggleFeature.Name", "Throw Weapons")).SetDescription(toggleFeatureBPdesc).SetDescriptionShort(toggleFeatureBPdesc);
            toggleFeatureConfig.AddFacts(new List<Blueprint<BlueprintUnitFactReference>>() { togglebuild });


            toggleFeatureConfig.SetIcon(BlueprintTool.Get<BlueprintWeaponType>("Dagger").Icon);
            toggleFeatureConfig.SetHideInCharacterSheetAndLevelUp(true);
            toggleFeatureConfig.SetHideInUI(true);
            var toggleFeature = toggleFeatureConfig.Configure();*/

            Main.TTWContext.Logger.LogPatch(togglebuild);
            var Root = BlueprintTools.GetBlueprint<BlueprintProgression>("5b72dd2ca2cb73b49903806ee8986325");
            Root.AddComponent(AddToRoot.CreateAddFactOnlyParty(togglebuild.ToReference<BlueprintUnitFactReference>()));
            //Root.LevelEntries.FirstOrDefault(x => x.Level == 1)?.m_Features.Add(togglebuild.ToReference<BlueprintFeatureBaseReference>());
            Main.TTWContext.Logger.LogPatch(Root);
        }

    }
}
