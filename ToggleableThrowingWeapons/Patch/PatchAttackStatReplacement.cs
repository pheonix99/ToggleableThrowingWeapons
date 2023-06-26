using BlueprintCore.Blueprints.Components.Replacements;
using HarmonyLib;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Patch
{
    class PatchAttackStatReplacement
    {
        [HarmonyPatch(typeof(AttackStatReplacement), nameof(AttackStatReplacement.OnEventAboutToTrigger))]
        static class FixAttackStatReplacement
        {

            [HarmonyPostfix]
            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(AttackStatReplacement __instance, RuleCalculateAttackBonusWithoutTarget evt)
            {
                Main.TTWContext.Logger.Log($"patching AttackStatReplacement start");
                if (__instance.CheckWeaponTypes)
                {
                    var crossref = evt.Weapon?.Blueprint.Type.Components.OfType<ThrownCrossrefTypeComponent>().FirstOrDefault();
                    if (crossref != null)
                    {
                        Main.TTWContext.Logger.Log($"Assessing AttackStatReplacement Crossref for hackage: {crossref.m_OtherForm.NameSafe()}");
                        Main.TTWContext.Logger.Log($"First AttackStatReplacement local weapon type is {__instance.WeaponTypes.FirstOrDefault().name}");
                        ModifiableValueAttributeStat modifiableValueAttributeStat = __instance.Owner.Stats.GetStat(evt.AttackBonusStat) as ModifiableValueAttributeStat;
                        ModifiableValueAttributeStat modifiableValueAttributeStat2 = __instance.Owner.Stats.GetStat(__instance.ReplacementStat) as ModifiableValueAttributeStat;
                        bool flag = modifiableValueAttributeStat2 != null && modifiableValueAttributeStat != null && modifiableValueAttributeStat2.Bonus >= modifiableValueAttributeStat.Bonus;

                        if (flag)
                        {
                            if (__instance.WeaponTypes.HasReference(crossref.m_OtherForm))
                            {
                                
                                evt.AttackBonusStat = __instance.ReplacementStat;
                                Main.TTWContext.Logger.Log($"Patching AttackStatReplacement Crossref");
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPatch]
        static class FixAttackStatReplacementFixed
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                return AccessTools.AllTypes()
                    .Where(t => t.Name == "AttackStatReplacementFixed")
                    .Where(t => t.GetMethod("OnEventAboutToTrigger") != null)
                    .Select(c => c.GetMethod("OnEventAboutToTrigger"));
            }

            [HarmonyPostfix]
            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(AttackStatReplacementFixed __instance, RuleCalculateAttackBonusWithoutTarget evt)
            {
                var crossref = evt.Weapon?.Blueprint.Type.Components.OfType<ThrownCrossrefTypeComponent>().FirstOrDefault();
                Main.TTWContext.Logger.Log($"patching AttackStatReplacementFixed start crossref is {crossref}");
                if (crossref != null)
                {
                    var attackBonus = __instance.Owner.Stats.GetStat(evt.AttackBonusStat) as ModifiableValueAttributeStat;
                    var replacementBonus = __instance.Owner.Stats.GetStat(__instance.ReplacementStat) as ModifiableValueAttributeStat;
                    Main.TTWContext.Logger.Log($"Assessing AttackStatReplacementFixed Crossref for hackage: {crossref.m_OtherForm.NameSafe()}");
                    Main.TTWContext.Logger.Log($"First AttackStatReplacementFixed local weapon type is {__instance.WeaponTypes.FirstOrDefault().name}");
                    bool shouldReplace = attackBonus is not null && replacementBonus is not null && replacementBonus.Bonus >= attackBonus.Bonus;
                    if (shouldReplace)
                    {
                        if (__instance.WeaponTypes.HasReference(crossref.m_OtherForm))
                        {
                                evt.AttackBonusStat = __instance.ReplacementStat;
                                Main.TTWContext.Logger.Log($"Patching AttackStatReplacementFixed Crossref");
                        } else
                        {

                        }
                    } else
                    {
                        Main.TTWContext.Logger.Log($"patching shouldreplace false old bonus: {attackBonus} new bonus: {replacementBonus}");
                    }
                }
            }
        }
    }
}
