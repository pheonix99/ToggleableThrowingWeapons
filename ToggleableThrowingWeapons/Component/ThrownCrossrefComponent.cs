using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Component
{
    [AllowedOn(typeof(BlueprintItemWeapon))]
    public class ThrownCrossrefComponent : BlueprintComponent
    {
        public BlueprintItemWeapon OtherForm
        {
            get => m_OtherForm.Get();
        }

        public BlueprintItemWeaponReference m_OtherForm;
    }
}
