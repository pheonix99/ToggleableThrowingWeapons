using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace ToggleableThrowingWeapons.Component
{
	// Token: 0x020022CA RID: 8906
	[AllowMultipleComponents]
	[AllowedOn(typeof(BlueprintUnitFact), false)]

	public class MeleeWeaponTypeDamageStatReplacement : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
	{
		// Token: 0x0600E33A RID: 58170 RVA: 0x003A1CAC File Offset: 0x0039FEAC
		public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
		{
			if (evt.Weapon.Blueprint.IsRanged )
				return;

			UnitBody body = evt.Initiator.Body;
			if (this.OnlyOneHanded && body.SecondaryHand.HasItem)
			{
				return;
			}
			ModifiableValueAttributeStat modifiableValueAttributeStat = evt.Initiator.Descriptor.Stats.GetStat(this.Stat) as ModifiableValueAttributeStat;
			ModifiableValueAttributeStat modifiableValueAttributeStat2 = (evt.DamageBonusStat != null) ? (evt.Initiator.Descriptor.Stats.GetStat(evt.DamageBonusStat.Value) as ModifiableValueAttributeStat) : null;
			if (modifiableValueAttributeStat != null && (modifiableValueAttributeStat2 == null || modifiableValueAttributeStat.Bonus > modifiableValueAttributeStat2.Bonus) && evt.Weapon.Blueprint.Type.Category == this.Category)
			{
				evt.OverrideDamageBonusStat(this.Stat);
			}
			if (this.TwoHandedBonus)
			{
				evt.TwoHandedStatReplacement = true;
			}
		}

		// Token: 0x0600E33B RID: 58171 RVA: 0x003A1D8C File Offset: 0x0039FF8C
		public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
		{
		}

		public StatType Stat;


		public WeaponCategory Category;


		public bool OnlyOneHanded;

		public bool TwoHandedBonus;
	}

	public static class DeployMeleeWeaponTypeDamageStatReplacement
    {
		public static void Do()
        {
			Main.TTWContext.Logger.Log($"Patching rogue finesse training");
			var finesseselect = BlueprintTools.GetBlueprint<BlueprintFeatureSelection>("b78d146cea711a84598f0acef69462ea");
			foreach (var feature in finesseselect.AllFeatures)
			{
				var existing = feature.GetComponent<WeaponTypeDamageStatReplacement>();
				if (existing == null)
				{
					Main.TTWContext.Logger.Log($"Did not patch finesse training ver {feature.name}");
					continue;
				}

				feature.AddComponent<MeleeWeaponTypeDamageStatReplacement>(x =>
				{
					x.Stat = existing.Stat;
					x.OnlyOneHanded = existing.OnlyOneHanded;
					x.TwoHandedBonus = existing.TwoHandedBonus;
					x.Category = existing.Category;

				});
				feature.RemoveComponent(existing);
				Main.TTWContext.Logger.LogPatch("Patched Finesse Training for melee only", feature);
            }
        }
    }


}
