using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;
using ToggleableThrowingWeapons.ModLogic;
using UnityModManagerNet;

namespace ToggleableThrowingWeapons
{
    static class Main
    {
        public static bool Enabled;
        public static ModContextTTW TTWContext;
        public static bool Errored = false;
        
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {


                var harmony = new Harmony(modEntry.Info.Id);
                TTWContext = new ModContextTTW(modEntry);
                TTWContext.ModEntry.OnSaveGUI = OnSaveGUI;
                TTWContext.ModEntry.OnGUI = UMMSettingsUI.OnGUI;
                
#if DEBUG
                TTWContext.Debug = true;
                TTWContext.Blueprints.Debug = true;
#endif

                harmony.PatchAll();

                PostPatchInitializer.Initialize(TTWContext);

                return true;
            }
            catch (Exception e)
            {
                Main.TTWContext.Logger.LogError(e, e.Message);
                return false;
            }
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            TTWContext.SaveAllSettings();



        }
    }
}
