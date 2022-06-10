using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleableThrowingWeapons.Component;

namespace ToggleableThrowingWeapons.Content
{
    public static class MatchHambeardWeapons
    {
        public static string[] HambeardNewWeaponGUIDs = new string[]
        {
            "DA09AA28EF8F4EC280F4DDC623453B4E",
            "FD567B09DD0443EF8838BF5536948513",
            "6F8785FD3B7B4A8BB8E465444B60E60C",
            "D6AE45836F2E4ECB857736C6513ACD28",
            "11F0CC28ECDE4679B11378BDAC498D5C",
            "F760A339C44D49C39388DAB7A9EDFEA6",
            "14E4654EEA7042B79921679E9DBC5030",
            "299D9F872E5242A98187CCD7C022E067",
            "CE459D19CB5F49919363F36E0ACF7882",
            "CCEFBDE83CBD4750BDA3CD736EA12883",

        };

        private static void HandleHambeardWithExistingCounterpart(string counterpart, string existing)
        {
            SimpleProcessHambeard(existing);
        }
        
        private static void SimpleProcessHambeard(string guid)
        {
            if (Main.TTWContext.Settings.Hotswapping.IsDisabled("EnableHotswapping"))
            {
                PairCreation.CreatePartner(BlueprintTool.Get<BlueprintItemWeapon>(guid));
            }
            else
            {
                var tweaked = ItemWeaponConfigurator.For(guid).SetType("Dagger").Configure();
                PairCreation.CreatePartner(tweaked);
            }
        }
        public static bool matched = false;
        public static void MatchAll()
        {
            if (matched)
                return;
            matched = true;

            Main.TTWContext.Logger.LogHeader("Matching Hambeard's Throwing Daggers");
            HandleHambeardWithExistingCounterpart("aa514dbf4c3d61f4e9c0738bd4d373cb", "97B6A7AAC76240ABBC2582B3D4018816");//Standard
            HandleHambeardWithExistingCounterpart("dfc92affae244554e8745a9ee9b7c520", "763DDAD0AE58487DBDD428F2D7FB4D82");//Masterwork
            HandleHambeardWithExistingCounterpart("e55e8179b23f16e40999320d6e7d1664", "A1AB772D47C248BE8E31DC05EDD00B12");//Masterwork Cold Iron
            HandleHambeardWithExistingCounterpart("b103b6468f2eff042903377b6ed940b2", "49A0EE1D36844C3692077B7F1E16533F");//Cold Iron
            HandleHambeardWithExistingCounterpart("2a45458f776442e43bba57de65f9b738", "F977EB309C3449569BF3A81355FC5505");//Plus 1
            HandleHambeardWithExistingCounterpart("4607f83de16245844acb4596fc797147", "0F0562914B7641AAAB8A63322AC6AC41");//Plus 2
            HandleHambeardWithExistingCounterpart("4c08dc9c56fd3ea4da4c6825105f0498", "F31884547EEF4DA680FAD4032BE94336");//Plus 3
            HandleHambeardWithExistingCounterpart("c5df3da9ea298214fa1b9de35800c8c3", "D720EE243F424742833932C091A61A59");//Plus 4
            HandleHambeardWithExistingCounterpart("801183fdc4e6d524f84cb209abb6e2f8", "D570D3C51F7146D6909F53A96822B5CA");//Plus 5
            HandleHambeardWithExistingCounterpart("ebea3139c4649e7459079d424c89e561", "FB810AE739D3426A9DDAC552FDEB9DED");//Cold Iron Plus 1
          

            HandleHambeardWithExistingCounterpart("bbbfd3c767d1fe44181ad183baa473d3", "D3D4E155C5B046D4B73ECE2D33F49823");//Flaming Plus 1
            

            HandleHambeardWithExistingCounterpart("a39defcc7226e074a9c60727e641a561", "6A28AD0756564203B680311CAB3FBCE9");//Frost Plus 1
            

            HandleHambeardWithExistingCounterpart("fbb105478a8d2824ab0301b2aa4e28d5", "00D8323934AE4E57B17B48A2D945CC33");//Shock Plus 1
            

            HandleHambeardWithExistingCounterpart("9d9e6e711cf49004ba84a0f8e0340e63", "1114B48318FC49D4B116173E9338B443");//Acid Plus 1
            



            HandleHambeardWithExistingCounterpart("08d4333b9b4a64e439c94765ee1cb5f1", "94D00BA9499142188AC024C90E3941D0");//Keen Plus 1
            foreach(string s in HambeardNewWeaponGUIDs)
            {

                if (Main.TTWContext.Settings.Hotswapping.IsDisabled("EnableHotswapping"))
                {
                    PairCreation.CreatePartner(BlueprintTool.Get<BlueprintItemWeapon>(s));
                }
                else
                {
                    var tweaked = ItemWeaponConfigurator.For(s).SetType("Dagger").Configure();
                    PairCreation.CreatePartner(tweaked);
                }
            }   
        }

        
    }
}
