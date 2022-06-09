using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Patch
{
    static class OverrideItemEntityBlueprintHook
    {
        private static void ActualPatch(ref BlueprintItem __result, ItemEntity __instance)
        {
            if (Main.TTWContext.Settings.Hotswapping.IsDisabled("EnableHotswapping"))
                return;

            if (__instance.m_Blueprint == null)
            {
                //Main.TTWContext.Logger.Log($"m_blueprint is null");
                return;
            }
            if (!(__instance.m_Blueprint is BlueprintItemWeapon))
            {
                //Main.TTWContext.Logger.Log($"m_blueprint is not weapon");
                return;
            }

            if (__instance.Owner == null)
            {
                //Main.TTWContext.Logger.Log($"owner is null ");
                return;
            }
            else
            {
                //Main.TTWContext.Logger.Log($"Owner is {__instance.Owner.CharacterName} blueprint {__instance.Owner.Blueprint.Name}");
            }

            if (Game.Instance == null)
            {
                return;
            }
            if (Game.Instance.CurrentMode == Kingmaker.GameModes.GameModeType.Dialog || Game.Instance.CurrentMode == Kingmaker.GameModes.GameModeType.Cutscene || Game.Instance.CurrentMode == Kingmaker.GameModes.GameModeType.CutsceneGlobalMap)
                return;
            if (__instance.Wielder == null)
            {
                //Main.TTWContext.Logger.Log($"Not Wielded ");
                return;
            }
            if (__instance.Owner.Faction == null)
            {
                //Main.TTWContext.Logger.Log($"No Owner Faction");
            }
            else if (__instance.Owner.Faction.AssetGuidThreadSafe != Game.Instance.BlueprintRoot.PlayerFaction.AssetGuidThreadSafe)
            {
                //Main.TTWContext.Logger.Log($"Faction GUID mismatch");
            }
            else if (Game.Instance.SaveManager.CurrentState != Kingmaker.EntitySystem.Persistence.SaveManager.State.None)
            {
                //Main.TTWContext.Logger.Log($"Save Manager In Unsafe State");
            }


            else if (__instance.m_Blueprint is BlueprintItemWeapon weapon)
            {


                //Main.TTWContext.Logger.Log($"Reached blueprint hacker - weapon {weapon.Name}, owner: {__instance.Owner.CharacterName} owner bp {__instance.Owner.Blueprint.name}");


                var crossref = __instance.m_Blueprint.Components.OfType<ThrownCrossrefComponent>().FirstOrDefault();
                if (crossref != null && crossref.m_OtherForm != null)
                {
                    bool throwing = __instance.Owner.Buffs.Enumerable.Any(x => x.Blueprint.Components.OfType<EnableThrownMode>().Any());


                    if (throwing && weapon.IsMelee)
                    {
                        __result = crossref.OtherForm;
                        //Main.TTWContext.Logger.Log($"Throwing enabled: {weapon.Name} should be hacked to ranged");

                    }
                    else if (!__instance.Owner.Buffs.Enumerable.Any(x => x.Components.OfType<EnableThrownMode>().Any()) && weapon.IsRanged)
                    {
                        //Main.TTWContext.Logger.Log($"Throwing Disabled: {weapon.Name} should be hacked to melee");
                        __result = crossref.OtherForm;

                    }
                    else
                    {
                        //Main.TTWContext.Logger.Log($"Throwing {(throwing ? "Enabled" : "Disabled")} {weapon.Name} does not need a hack");
                    }
                }
                else if (crossref == null)
                {
                    //Main.TTWContext.Logger.Log($"No Crossref component");
                }
                else if (crossref.m_OtherForm == null)
                {
                    //Main.TTWContext.Logger.Log($"Crossref component is missing other form");
                }
            }
        }

        [HarmonyPatch(typeof(ItemEntity<BlueprintItem>), "Blueprint", MethodType.Getter)]
        static class RedirectToAltBlueprint
        {

            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(ref BlueprintItem __result, ItemEntity<BlueprintItem> __instance)
            {
                //Main.TTWContext.Logger.Log($"Started Blueprint Hack attempt - in ItemEntity<BlueprintItem>");

                try
                {
                    ActualPatch(ref __result, __instance);
                }
                catch (Exception e)
                {
                    Main.TTWContext.Logger.LogError(e, "Crash in ItemEntity<BlueprintItem> hack");
                }
            }
        }

        [HarmonyPatch(typeof(ItemEntity), "Blueprint", MethodType.Getter)]
        static class RedirectToAltBlueprint2
        {

            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(ref BlueprintItem __result, ItemEntity __instance)
            {
                //Main.TTWContext.Logger.Log($"Started Blueprint Hack attempt - in ItemEntity: {__instance.GetType().ToString()}");
                try
                {
                    ActualPatch(ref __result, __instance);
                }
                catch (Exception e)
                {
                    Main.TTWContext.Logger.LogError(e, "Crash in ItemEntity hack");
                }

            }
        }

        /*
        [HarmonyPatch(typeof(CheckItemCondition), "CheckOnUnit")]
        static class Hijack_CheckItemCOndition_CheckOnUnit
        {
            public static void Postfix(ref bool __result, UnitEntityData unit, BlueprintItem item)
            {
                if (Main.TTWContext.Settings.Hotswapping.IsDisabled("EnableHotswapping"))
                    return;
                ItemEntity itemEntity = null;
                if (__result)
                {
                    return;
                }
                var corssref = item.Components.OfType<ThrownCrossrefComponent>().FirstOrDefault();
                if (corssref == null || corssref.OtherForm == null)
                {
                    return;
                }
                if (unit != null)
                {
                    unit.Inventory.TryFind((ItemEntity x) => x.Blueprint.Equals(item) || x.Blueprint.Equals((BlueprintItem)corssref.OtherForm), out itemEntity);
                }
                if (itemEntity != null)
                {
                    UnitDescriptor wielder = itemEntity.Wielder;
                    __result = wielder != null && wielder.Unit.Equals(unit);
                }

            }
        }

        [HarmonyPatch(typeof(ItemsEnough), "CheckCondition")]
        static class Hijack_ItemsEnough_CheckCondition
        {
            public static void Postfix(ref bool __result, ItemsEnough __instance)
            {
                if (Main.TTWContext.Settings.Hotswapping.IsDisabled("EnableHotswapping"))
                    return;

                if (__result)
                    return;
                if (__instance.Money || __instance.ItemToCheck.GetComponent<MoneyReplacement>())
                {
                    return;
                }
                var crossref = __instance.m_ItemToCheck.Get().GetComponent<ThrownCrossrefComponent>();

                if ( crossref != null)
                {
                    __result = Contains(GameHelper.GetPlayerCharacter().Inventory, new BlueprintItemReference[] { __instance.m_ItemToCheck, BlueprintTool.GetRef<BlueprintItemReference>(crossref.m_OtherForm.deserializedGuid.ToString()) }, __instance.Quantity);
                }
            }
        }

        private static bool Contains(ItemsCollection collection, IEnumerable<BlueprintItemReference> items, int count)
        {
            int num = 0;
            for (int i = 0; i < collection.m_Items.Count; i++)
            {
                ItemEntity itemEntity = collection.m_Items[i];
                if (items.Any(x=>x.deserializedGuid == itemEntity.m_Blueprint.AssetGuidThreadSafe))
                {
                    num += itemEntity.Count;
                    if (num >= count)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        */
    }
}
