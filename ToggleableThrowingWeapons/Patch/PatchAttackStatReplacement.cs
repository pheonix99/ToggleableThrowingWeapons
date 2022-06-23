using HarmonyLib;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Patch
{
    class PatchAttackStatReplacement
    {
        [HarmonyPatch(typeof(AttackStatReplacement), "OnEventAboutToTrigger")]
        static class FixAttackStatReplacer
        {

            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(AttackStatReplacement __instance, RuleCalculateAttackBonusWithoutTarget evt)
            {
                if (__instance.CheckWeaponTypes)
                {
                    

                    var crossref = evt.Weapon?.Blueprint.Type.Components.OfType<ThrownCrossrefTypeComponent>().FirstOrDefault();
                    if (crossref != null)
                    {
                        Main.TTWContext.Logger.Log($"Assessing Crossref for hackage: {crossref.m_OtherForm.NameSafe()}");
                        Main.TTWContext.Logger.Log($"First local weapon type is {__instance.WeaponTypes.FirstOrDefault().name}");
                        ModifiableValueAttributeStat modifiableValueAttributeStat = __instance.Owner.Stats.GetStat(evt.AttackBonusStat) as ModifiableValueAttributeStat;
                        ModifiableValueAttributeStat modifiableValueAttributeStat2 = __instance.Owner.Stats.GetStat(__instance.ReplacementStat) as ModifiableValueAttributeStat;
                        bool flag = modifiableValueAttributeStat2 != null && modifiableValueAttributeStat != null && modifiableValueAttributeStat2.Bonus >= modifiableValueAttributeStat.Bonus;

                        if (flag)
                        {
                            

                            if (__instance.WeaponTypes.HasReference(crossref.m_OtherForm))
                            {
                                
                                evt.AttackBonusStat = __instance.ReplacementStat;
                                Main.TTWContext.Logger.Log($"Patching Crossref");
                            }
                        }

                    }
                        
                }
            }
        }

    }
}
