using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Component
{
    [AllowedOn(typeof(BlueprintBuff))]
    class EnableThrownMode : BlueprintComponent
    {
    }
}
