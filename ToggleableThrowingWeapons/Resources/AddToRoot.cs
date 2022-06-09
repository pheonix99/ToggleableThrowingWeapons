using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Resources
{
    class AddToRoot
    {
        public static AddFactOnlyParty CreateAddFactOnlyParty(BlueprintUnitFactReference fact, FeatureParam param = null)
        {
            var result = new AddFactOnlyParty();
            result.Feature = fact;
            result.Parameter = param;
            return result;
        }

    }
}
