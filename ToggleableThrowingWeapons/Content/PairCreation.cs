using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Config;
using TabletopTweaks.Core.Utilities;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Content
{

    class PairCreation
    {
        public static List<BlueprintItemWeaponReference> MatchedMelee = new();
        public static List<BlueprintItemWeaponReference> MatchedThrown = new();
        public static List<BlueprintItemWeaponReference> Matched = new();
        public static List<BlueprintItemWeaponReference> ToMatchViaType = new();

        public static string[] DaggerGuids = new string[]
        {
            "5892429bf5634b158d84ec1217e9f45c",
            "493c0cee946597141a71c99589fabf08",
            "c3f0071bec7a17d42b0cc3da8fd3d45e",
            "f3ea56e2538b0834fa4208be4d0414fa",
            "8ccce68a6349232468a40b24c9e179cc",
            "ebea3139c4649e7459079d424c89e561",
            "b103b6468f2eff042903377b6ed940b2",
            "e55e8179b23f16e40999320d6e7d1664",
            "2c07bcb9fc794cda86467a414cacf9dd",
            "bf9cd50decc79734b9def40942a8e4e9",
            "9d9e6e711cf49004ba84a0f8e0340e63",
            "08d4333b9b4a64e439c94765ee1cb5f1",
            "840d56ad51e927741aa639adbdcc6881",
            "d9385822b9871ac468e00aa01b68d45a",
            "8271578c75b872b47a74271d3edf37a3",
            "2005289e2e10f194089bd781168e3630",
            "a39defcc7226e074a9c60727e641a561",
            "de993695211c7b84d9543ce69cb571d7",
            "4607f83de16245844acb4596fc797147",
            "a074b8cc7a0fa0940ad6bc8bec1be8de",
            "bbbfd3c767d1fe44181ad183baa473d3",
            "2a45458f776442e43bba57de65f9b738",
            "5527c7d072019e7408c557f42470fe6f",
            "02edf077ed6285643984fdd994d31c9e",
            "4c08dc9c56fd3ea4da4c6825105f0498",
            "bf301596ad8e90446880be62a93cc64f",
            "fbb105478a8d2824ab0301b2aa4e28d5",
            "c5df3da9ea298214fa1b9de35800c8c3",
            "82a5d652d250ff747a2292e84948d5fe",
            "801183fdc4e6d524f84cb209abb6e2f8",
            "1d802a20dc593a34795ef62567edf6bd",
            "0d1f412c73da61c4d96db61ec61db3d2",
            "7d88fb635e054783aab93ee8af382866",
            "51a26a9df9f30ab48b9ebd6e4810d2bb",
            "85cc154284504c74ba1d050bab204b4c",
            "9329ea6b7e8c00f4dad03f0c7b8ad85d",
            "ba53644f9e8895844b3f0dad849bba94",
            "1c54a278449baf045800cd1e97a912a5",
            "3b3b9723d646a6649ab3c3281d5b656b",
            "10d74936dad17d24b9e50df643f6b1eb",
            "a47a055edd9493640bd923b6656b5e0d",
            "52a5c1edbf1846a45a1cb2d9bb4134ff",
            "a0fcdf7509c4a2f46adc914f55ed6ec0",
            "1ea51ba3d4927bf49ac598a11c67686a",
            "e7754310f0df3524ca08de172162874e",
            "cfb388c120032164aac081f4232f2f43",
            "e1108159fa11a2e41bb60899c775e5b3",
            "6d200ef07f62df64fb5000c8fd5b4f79",
            "dda495b9b24ec334588d31c56ddea8b8",
            "b5fd33edbe6835c4ba6e1c5299aafbff",
            "dfc92affae244554e8745a9ee9b7c520",
            "955fed173f194e50a0207a1233cb51e6",
            "40cb2aa35a04b204eab6da2dd310ec89",
            "54325f72a38c404fbc2bc6708866cc16",
            "3e2067a7fb05343409024e3f17b76cde",
            "1f3cd44fb795f50498a4b47b93dc399d",
            "aa514dbf4c3d61f4e9c0738bd4d373cb",
            "7124b5d7b77509847b8990fe26a50a65",
            "7359ebb98cad3d54aafecc2027b3359e"
        };

        public static string[] StarknifeGuids = new string[]
        {
            "ae962398e034c4842bcfdfa95b8de0c7",
            "a662282ca2939144592ee4b1c8b1522e",
            "eb69120058a964c4298cea2fe3c9fc2b",
            "33c55991f51bd6444989f5d60ac3d867",
            "f01006f4204dc7a429997714196defe7",
            "f4d0fb848ef585d41bc2f9dfe8d5c1e7",
            "13ba8689545ee644f8bcd7cae514a5a6",
            "8a668978ce3ba3b4390a286eac525acc",
            "bf16fbb49b31d5d4fa94924b24f6796b",
            "3c95880358ea6f646b70c87e5c0af368",
            "9630773155e5b964d9c4e8e235edadeb",
            "38dfe0768e8ffec4d8a5a34c7ba41204",
            "657eca867b9324c4ca46cbf9ca01b940",
            "59afd5fc58e8cd54fa53b6fb254167a9",
            "d19662d357d752447a801951b7bec798",
            "e9550e8e58b4afd488a6fe9e39224319",
            "5fc80f60cecbaa54cb10f3af7b573fbe",
            "38f1a139d9325cb438ca8a15756d2625",
            "a610dc50943648e4197a0dd3f6051324",
            "00f0be3bd164d144db4429c0be892af1",
            "d1fb2590ec82a1f4f9c2ec2ddd6102bf",
            "268e5eb549570534f9b891ac7124a605",
            "2adef6fc11bb5714787e4f59e8436ff2",
            "4652e0fd3d2be574c8c7929552d5faab",
            "d86fc88b9965ee845954f21eca9d8e01",
            "bed41d0b082ec0044b1eeea79cd58161",
            "b448eee7d7a0ae44895aeadfbe27b7c8",
            "6c1b7fee71dba1249b40539d0ee08510",
            "48407283260c4756b35456f4cfc5bc5f",
            "bacea00ed10657043bf90641eeabde95",
            "0bbde89825672ba49b2fe9879eca3111",
            "7ee8552b08e63a14a8acff60c8811ee7",
            "f2f9a3cd2f9db73489b15572ed82eb0a",
            "3f3a6ef933caf104a8a699ab6059a9b4",
            "d954718606e4d794fb7076b0afd55680",
            "0d24d60ba1def9c41b9ced877667cd2f",
            "bd2c082b4588edd429532c295bcc288b",
            "057e3fbee4c36ee408f82af4290354df",
            "b726f8b106ec5dc4dbe79c72936e8071",
            "8f8853a606c87944085530685c1cf5ad",
            "d48b165b5172c774caf53cd15baeb137",
            "7510be4c91e6a754d8c887db44585324",
            "3ac1cb7febe74fd5ac34b96f89775043"
        };

        public static string[] RustyDaggerGuids = new string[]
        {
            "49337509adbc459e8b054380bf876fb9"

        };

        private static void AttachComponentsToPair(string melee, string thrown)
        {
            if (MatchedMelee.Any(x => x.deserializedGuid == melee) || MatchedThrown.Any(x => x.deserializedGuid == thrown))
            {

                return;
            }
            

            var meleeWep = ItemWeaponConfigurator.For(melee).AddComponent<ThrownCrossrefComponent>(x => x.m_OtherForm = BlueprintTool.GetRef<BlueprintItemWeaponReference>(thrown)).Configure();
            
            var thrownWep = ItemWeaponConfigurator.For(thrown).AddComponent<ThrownCrossrefComponent>(x => x.m_OtherForm = BlueprintTool.GetRef<BlueprintItemWeaponReference>(melee)).Configure();
            
            PairCreation.MatchedMelee.Add(meleeWep.ToReference<BlueprintItemWeaponReference>());
            PairCreation.MatchedThrown.Add(thrownWep.ToReference<BlueprintItemWeaponReference>());
            
            Main.TTWContext.Logger.LogPatch("Paired With Matching Thrown", meleeWep);
            Main.TTWContext.Logger.LogPatch("Paired With Matching Melee", thrownWep);
        }

        

        public static void CreatePairs()
        {
            Main.TTWContext.Logger.LogHeader("Patching Base Game Daggers");
            foreach(string s in DaggerGuids)
            {
                CreatePartner(BlueprintTool.Get<BlueprintItemWeapon>(s));
            }
            Main.TTWContext.Logger.LogHeader("Patching Base Game Starknives");
            foreach (string s in StarknifeGuids)
            {
                CreatePartner(BlueprintTool.Get<BlueprintItemWeapon>(s));
            }
            Main.TTWContext.Logger.LogHeader("Patching DLC2 Daggers");
            foreach (string s in RustyDaggerGuids)
            {
                CreatePartner(BlueprintTool.Get<BlueprintItemWeapon>(s));
            }
        }

        public static void CreatePartner(BlueprintItemWeapon unpairedWeapon)
        {
            if (MatchedMelee.Any(x => x.deserializedGuid == unpairedWeapon.AssetGuidThreadSafe) || MatchedThrown.Any(x => x.deserializedGuid == unpairedWeapon.AssetGuidThreadSafe || Matched.Any(x=>x.deserializedGuid == unpairedWeapon.AssetGuidThreadSafe)))
            {
                return;
            }
            var otherType = unpairedWeapon.Type.Components.OfType<ThrownCrossrefTypeComponent>().FirstOrDefault()?.m_OtherForm;
            if (otherType == null)
            {
                return;
            }
            Matched.Add(unpairedWeapon.ToReference<BlueprintItemWeaponReference>());
            Main.TTWContext.Logger.LogPatch("Patching toggle-version of", unpairedWeapon);
            BlueprintGuid? master = null;
            if (unpairedWeapon.Type.AssetGuidThreadSafe == "07cc1a7fceaee5b42b3e43da960fe76d")//Base Game Dagger
            {
                master = Main.TTWContext.Blueprints.GetDerivedMaster("ThrownDaggerMasterId");

            }
            else if (unpairedWeapon.Type.AssetGuidThreadSafe == "90ba10e82a7a4135bb380234a255f5ae")
            {
                master = Main.TTWContext.Blueprints.GetDerivedMaster("ThrownDLC2DaggerMasterId");
            }
            else if (unpairedWeapon.Type.AssetGuidThreadSafe == BlueprintTool.Get<BlueprintWeaponType>("TTWThrownDagger").AssetGuidThreadSafe)
            {
                master = Main.TTWContext.Blueprints.GetDerivedMaster("DaggerMasterId");

            }
            else if (unpairedWeapon.Type.AssetGuidThreadSafe == "5a939137fc039084580725b2b0845c3f")
            {
                master = Main.TTWContext.Blueprints.GetDerivedMaster("ThrownStarknifeMasterId");

            }

            if (master != null)
            {
               
                string derivedGuidCode = (otherType.Get().IsRanged ? "TTWThrownMode" : "TTWMeleeMode" ) + unpairedWeapon.name  ;
                var paired = Main.TTWContext.Blueprints.GetDerivedGUID(derivedGuidCode, master.Value, unpairedWeapon.AssetGuid);
                var newBP = Helpers.CreateCopy<BlueprintItemWeapon>(unpairedWeapon, x =>
                {
                    
                    x.name = derivedGuidCode;
                    x.AssetGuid = paired;
                    x.m_Type = otherType;
                    
                });
                
                Main.TTWContext.Logger.LogPatch($"Created {otherType.NameSafe()} from {unpairedWeapon.Name}", newBP);

                BlueprintTools.AddBlueprint(Main.TTWContext, newBP);
                
                if (unpairedWeapon.IsMelee)
                    AttachComponentsToPair(unpairedWeapon.AssetGuidThreadSafe, paired.ToString());
                else
                    AttachComponentsToPair(paired.ToString(), unpairedWeapon.AssetGuidThreadSafe);
                Main.TTWContext.Logger.LogPatch("Patched altMode onto:", unpairedWeapon);
                
            }
        }

    }
}
