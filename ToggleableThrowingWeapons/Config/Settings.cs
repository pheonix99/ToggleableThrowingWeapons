using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Config;

namespace ToggleableThrowingWeapons.Config
{
    class Settings : IUpdatableSettings
    {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup Hotswapping;
        public void Init()
        {
            
        }

        public void OverrideSettings(IUpdatableSettings userSettings)
        {
            var loadedSettings = userSettings as Settings;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            Hotswapping.LoadSettingGroup(loadedSettings.Hotswapping, NewSettingsOffByDefault);

        }
    }
}
