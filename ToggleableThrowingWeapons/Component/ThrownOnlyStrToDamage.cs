using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleableThrowingWeapons.Component
{
	class ThrownOnlyStatToDamage : WeaponEnchantmentLogic, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
	{
		// Token: 0x0600C1F7 RID: 49655 RVA: 0x00311EA0 File Offset: 0x003100A0
		public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
		{
			ModifiableValueAttributeStat modifiableValueAttributeStat = evt.Initiator.Descriptor.Stats.GetStat(this.Stat) as ModifiableValueAttributeStat;
			if (evt.Weapon == base.Owner && modifiableValueAttributeStat != null && evt.Weapon.Blueprint.IsRanged)
			{
				evt.OverrideDamageBonusStat(this.Stat);
				evt.OverrideDamageBonusStatMultiplier(this.Multiplier);
			}
		}

		// Token: 0x0600C1F8 RID: 49656 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
		{
		}

		// Token: 0x04007E90 RID: 32400
		public StatType Stat;

		// Token: 0x04007E91 RID: 32401
		public float Multiplier;
	}
}
