using HarmonyLib;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Patch
{
    class PatchWeaponCategoryExtension
    {
        [HarmonyPatch(typeof(WeaponCategoryExtension), "HasSubCategory")]
        static class ApplyThrownFixToHasSubCategory
        {

            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(ref bool __result, WeaponCategory category, WeaponSubCategory subCategory)
            {
                if ((category == WeaponCategory.Dagger || category == WeaponCategory.Starknife) && subCategory == WeaponSubCategory.Ranged)
                {
                    __result = true;
                }
            }
        }

        [HarmonyPatch(typeof(WeaponCategoryExtension), "GetSubCategories")]
        static class ApplyThrownFixToGetSubCategories
        {

            [HarmonyPriority(Priority.Normal)]
            public static void Postfix(ref WeaponSubCategory[] __result, WeaponCategory category)
            {
                if ((category == WeaponCategory.Dagger || category == WeaponCategory.Starknife) && !__result.Contains(WeaponSubCategory.Ranged))
                {
                    __result = __result.Append(WeaponSubCategory.Ranged).ToArray();
                }
            }
        }
    }

    
}

