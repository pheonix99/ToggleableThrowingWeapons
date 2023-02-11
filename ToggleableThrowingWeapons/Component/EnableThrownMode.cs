using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Component
{
    [AllowedOn(typeof(BlueprintBuff))]
    class EnableThrownMode : UnitFactComponentDelegate
    {
        /*
        public override void OnActivate()
        {
            base.OnActivate();
            EventBus.RaiseEvent<IUnitActiveEquipmentSetHandler>(delegate (IUnitActiveEquipmentSetHandler h)
            {
                h.HandleUnitChangeActiveEquipmentSet(this.Owner);
            }, true);
        }
        public override void OnDeactivate()
        {
            base.OnDeactivate();
            EventBus.RaiseEvent<IUnitActiveEquipmentSetHandler>(delegate (IUnitActiveEquipmentSetHandler h)
            {
                h.HandleUnitChangeActiveEquipmentSet(this.Owner);
            }, true);
        }
        */
    }
}
