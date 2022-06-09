using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Component
{
    [AllowedOn(typeof(BlueprintWeaponType))]
    class ThrownCrossrefTypeComponent : BlueprintComponent
    {
        public BlueprintWeaponType OtherForm
        {
            get => m_OtherForm.Get();
        }

        public BlueprintWeaponTypeReference m_OtherForm;
    }
}
