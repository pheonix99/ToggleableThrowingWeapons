using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.ModLogic;
using UnityModManagerNet;

namespace ToggleableThrowingWeapons.ModLogic
{
    class ModContextTTW : ModContextBase
    {
        public Config.Settings Settings;

        public ModContextTTW(UnityModManager.ModEntry modEntry) : base(modEntry)
        {
            LoadSettings("Settings.json", "ToggleableThrowingWeapons.Config", ref Settings);
            LoadBlueprints("ToggleableThrowingWeapons.Config", this);
        }

        public override void LoadAllSettings()
        {
            base.AfterBlueprintCachePatches();
            if (Debug)
            {
                //Blueprints.RemoveUnused();
                SaveSettings(BlueprintsFile, Blueprints);
                
            }
        }

        public override void AfterBlueprintCachePatches()
        {
            base.AfterBlueprintCachePatches();
            if (Debug)
            {
                Blueprints.RemoveUnused();
                SaveSettings(BlueprintsFile, Blueprints);
                //ModLocalizationPack.RemoveUnused();
                //SaveLocalization(ModLocalizationPack);
            }
        }
        public override void SaveAllSettings()
        {
            base.SaveAllSettings();
            SaveSettings("Settings.json", Settings);

            
        }
    }
}
