using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;
using ToggleableThrowingWeapons.Content;
using UnityModManagerNet;

namespace ToggleableThrowingWeapons
{
    class BuildEarly
    {


        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                Main.TTWContext.Logger.LogHeader("Starting Building Toggleable Daggers");
                try
                {
                    BaseWeapons.MakeBaseWeapons();
                    ModeSwitching.CreateModeSwitchAbilities();

                    PairCreation.CreatePairs();
                   
                }
                catch (Exception e)
                {
                    Main.TTWContext.Logger.LogError(e, "");
                }
            }
        }
    }
    class BuildLate
    {
    
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch2
        {
            static bool Initialized;

            [HarmonyPriority(Priority.Last)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                if (UnityModManager.FindMod("ThrownDaggers") != null)
                {
                    MatchHambeardWeapons.MatchAll();
                    
                }
                try
                {
                    DeployMeleeWeaponTypeDamageStatReplacement.Do();
                }
                catch (Exception e)
                {
                    Main.TTWContext.Logger.LogError(e, "");
                }
            }
        }
    }
}
