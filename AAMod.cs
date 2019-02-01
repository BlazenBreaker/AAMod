using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.UI;
using AAMod.Backgrounds;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System.Reflection;
using AAMod.Items.Materials;
using AAMod.Items.Armor.Darkmatter;
using AAMod.Items.Armor.Radium;
using AAMod.Items.Blocks;
using AAMod;

namespace AAMod
{
    class AAMod : Mod
    {
        public static int GoblinSoul;
        public static ModHotKey InfinityHotKey;
        public static ModHotKey AbilityKey;
        internal static AAMod instance;
        internal UserInterface UserInterface;
        public static bool AkumaMusic = false;
        public static bool YamataMusic = false;
        public static bool Slayer = false;
        public static AAMod self = null;
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public static string BLANK_TEX = "AAMod/BlankTex";

        public static int[] SNAKETYPES = new int[0];
        public static int[] SERPENTTYPES = new int[0];

        #region mod loaded bools
        public static bool fargoLoaded = false;
        public static bool calamityLoaded = false;
        public static bool grealmLoaded = false;
        public static bool sacredToolsLoaded = false;
        public static bool spiritLoaded = false;
        public static bool thoriumLoaded = false;
        public static bool tremorLoaded = false;
        public static bool redemptionLoaded = false;
        public static bool cheatsheetLoaded = false;
        public static bool herosLoaded = false;
        #endregion

        public AAMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
            instance = this;
        }

        public override void PostSetupContent()
        {
            Mod DradonIsDum = ModLoader.GetMod("AchievementLibs");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            Mod Calamity = ModLoader.GetMod("CalamityMod");
            Mod Thorium = ModLoader.GetMod("ThoriumMod");
            Mod Spirit = ModLoader.GetMod("SpiritMod");
            Mod Fargos = ModLoader.GetMod("Fargowiltas");
            Mod GRealm = ModLoader.GetMod("GRealm");
            Mod SacredTools = ModLoader.GetMod("SacredTools");
            Mod Tremor = ModLoader.GetMod("Tremor");
            Mod Redemption = ModLoader.GetMod("Redemption");
            Mod CheatSheet = ModLoader.GetMod("CheatSheet");
            Mod HEROsMod = ModLoader.GetMod("HEROsMod");
            if (Calamity != null) calamityLoaded = true;
            if (Thorium != null) thoriumLoaded = true;
            if (Spirit != null) spiritLoaded = true;
            if (Fargos != null) fargoLoaded = true;
            if (GRealm != null) grealmLoaded = true;
            if (SacredTools != null) sacredToolsLoaded = true;
            if (Tremor != null) tremorLoaded = true;
            if (Redemption != null) redemptionLoaded = true;
            if (CheatSheet != null) cheatsheetLoaded = true;
            if (HEROsMod != null) herosLoaded = true;
            if (yabhb != null)
            {
                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF1"));

                Call("RegisterHealthBarMini", instance.NPCType("YamataHeadF2"));

                #region Healthbars


                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/MBarHead"),
                    GetTexture("Healthbars/MBarBody"),
                    GetTexture("Healthbars/MBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Firebrick,
                    Color.Firebrick,
                    Color.Firebrick);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("MushroomMonarch")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/RGCBarHead"),
                    GetTexture("Healthbars/RGCBarBody"),
                    GetTexture("Healthbars/RGCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("GripOfChaosRed")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/BGCBarHead"),
                    GetTexture("Healthbars/BGCBarBody"),
                    GetTexture("Healthbars/BGCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("GripOfChaosBlue")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/BMBarHead"),
                    GetTexture("Healthbars/BMBarBody"),
                    GetTexture("Healthbars/BMBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Broodmother")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/HydraBarHead"),
                    GetTexture("Healthbars/HydraBarBody"),
                    GetTexture("Healthbars/HydraBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Hydra")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SSBarHead"),
                    GetTexture("Healthbars/SSBarBody"),
                    GetTexture("Healthbars/SSBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("SerpentHead")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/DDBarHead"),
                    GetTexture("Healthbars/DDBarBody"),
                    GetTexture("Healthbars/DDBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.IndianRed,
                    Color.IndianRed,
                    Color.IndianRed);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Djinn")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Retriever")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Raider")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/SBarHead"),
                    GetTexture("Healthbars/SBarBody"),
                    GetTexture("Healthbars/SBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Violet,
                    Color.Violet,
                    Color.Violet);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Orthrus")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/DBBarHead"),
                    GetTexture("Healthbars/DBBarBody"),
                    GetTexture("Healthbars/DBBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("DaybringerHead")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/NCBarHead"),
                    GetTexture("Healthbars/NCBarBody"),
                    GetTexture("Healthbars/NCBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumBlue,
                    Color.MediumBlue,
                    Color.MediumBlue);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("NightcrawlerHead")));


                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/BGBarHead"),
                    GetTexture("Healthbars/BGBarBody"),
                    GetTexture("Healthbars/BGBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("BlazeGrip")));


                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/AGBarHead"),
                    GetTexture("Healthbars/AGBarBody"),
                    GetTexture("Healthbars/AGBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Navy,
                    Color.Navy,
                    Color.Navy);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("AbyssGrip")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/YamataBarHead"),
                    GetTexture("Healthbars/YamataBarBody"),
                    GetTexture("Healthbars/YamataBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Navy,
                    Color.Navy,
                    Color.Navy);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("Yamata")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/YamataABarHead"),
                    GetTexture("Healthbars/YamataABarBody"),
                    GetTexture("Healthbars/YamataABarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumVioletRed,
                    Color.MediumVioletRed,
                    Color.MediumVioletRed);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("YamataA")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroBarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroBarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("ZeroAwakened")));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    GetTexture("Healthbars/ZeroABarHead"),
                    GetTexture("Healthbars/ZeroBarBody"),
                    GetTexture("Healthbars/ZeroABarTail"),
                    GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Black,
                    Color.Black,
                    Color.Black);
                yabhb.Call("hbSetMidBarOffset", 30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", (instance.NPCType("ZeroAwakened")));
                #endregion

            }
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Mushroom Monarch", 0.0000000000000000001f, (Func<bool>)(() => AAWorld.downedMonarch), "Use a [i:" + ItemType("IntimidatingMushroom") + "] during the day");
                bossChecklist.Call("AddBossWithInfo", "Grips of Chaos", 2.00000000001f, (Func<bool>)(() => AAWorld.downedGrips), "Use a [i:" + ItemType("CuriousClaw") + "] or [i:" + ItemType("InterestingClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Broodmother", 4.00000000001f, (Func<bool>)(() => AAWorld.downedBrood), "Use a [i:" + ItemType("DragonBell") + "] in the Inferno during the day");
                bossChecklist.Call("AddBossWithInfo", "Hydra", 4.00000000001f, (Func<bool>)(() => AAWorld.downedHydra), "Use a [i:" + ItemType("HydraChow") + "] in the Mire at night");
                bossChecklist.Call("AddBossWithInfo", "Subzero Serpent", 5.00000000001f, (Func<bool>)(() => AAWorld.downedSerpent), "Use a [i:" + ItemType("SubzeroCrystal") + "] in the Snow biome at night");
                bossChecklist.Call("AddBossWithInfo", "Desert Djinn", 5.00000000001f, (Func<bool>)(() => AAWorld.downedDjinn), "Use a [i:" + ItemType("DjinnLamp") + "] in the Desert during the day");
                //bossChecklist.Call("AddBossWithInfo", "Truffle Toad", 6.00000000001f, (Func<bool>)(() => AAWorld.downedToad), "Use a [i:" + ItemType("Toadstool") + "] in an underground mushroom biome");
                bossChecklist.Call("AddBossWithInfo", "Retriever", 6.9999997f, (Func<bool>)(() => AAWorld.downedRetriever), "Use a [i:" + ItemType("CyberneticClaw") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Raider Ultima", 6.9999997f, (Func<bool>)(() => AAWorld.downedRaider), "Use a [i:" + ItemType("CyberneticBell") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Orthrus X", 6.9999997f, (Func<bool>)(() => AAWorld.downedOrthrus), "Use a [i:" + ItemType("HydraChow") + "] at night");
                bossChecklist.Call("AddBossWithInfo", "Emperor Fishron", 14.00000000001f, (Func<bool>)(() => AAWorld.downedEFish), "Use a [i:" + ItemType("ShroomGrub") + " at the ocean]");
                bossChecklist.Call("AddBossWithInfo", "Nightcrawler & Daybringer", 14.00000000002f, (Func<bool>)(() => AAWorld.downedEquinox), "Use a [i:" + ItemType("EquinoxWorm") + "]");
                bossChecklist.Call("AddBossWithInfo", "Kraken", 16.1f, (Func<bool>)(() => AAWorld.downedKraken), "Use a [i:" + ItemType("Portal") + "] at the Ocean");
                bossChecklist.Call("AddBossWithInfo", "Yamata", 16.2f, (Func<bool>)(() => AAWorld.downedYamata), "Use a [i:" + ItemType("DreadSigil") + "] in the Mire at night");
                bossChecklist.Call("AddBossWithInfo", "Akuma", 16.3f, (Func<bool>)(() => AAWorld.downedAkuma), "Use a [i:" + ItemType("DraconianSigil") + "] in the Inferno during the day");
                bossChecklist.Call("AddBossWithInfo", "Zero", 16.4f, (Func<bool>)(() => AAWorld.downedZero), "Use a [i:" + ItemType("ZeroTesseract") + "] in the Void");
                //bossChecklist.Call("AddBossWithInfo", "Soul of Cthulhu", 17.997f, (Func<bool>)(() => AAWorld.downedSoC), "Use a [i:" + ItemType("SpatialWheel") + "at the ocean]");
                //bossChecklist.Call("AddBossWithInfo", "Infinity Zero", 17.998f, (Func<bool>)(() => AAWorld.downedIZ), "Use a [i:" + ItemType("InfinityOverloader") + "]");
                //bossChecklist.Call("AddBossWithInfo", "Shen Doragon", 17.999f, (Func<bool>)(() => AAWorld.downedShen), "Use a [i:" + ItemType("ChaosSigil") + "]");


                //SlimeKing = 1f;
                //EyeOfCthulhu = 2f;
                //EaterOfWorlds = 3f;
                //QueenBee = 4f;
                //Skeletron = 5f;
                //WallOfFlesh = 6f;
                //TheTwins = 7f;
                //TheDestroyer = 8f;
                //SkeletronPrime = 9f;
                //Plantera = 10f;
                //Golem = 11f;
                //DukeFishron = 12f;
                //LunaticCultist = 13f;
                //Moonlord = 14f;
            }
            if (DradonIsDum != null)
            {
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Doin' Shrooms", "Defeat the feudal fungus, the Mushroom Monarch", instance.GetTexture("BlankTex"), AAWorld.downedMonarch);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Get a Grip", "Defeat the claws of catastrophe, the Grips of Chaos", instance.GetTexture("Achievments/Grips"), AAWorld.downedGrips);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Magmatic Meltdown", "Defeat the magmatic matriarch, the Broodmother", instance.GetTexture("Achievments/Brood"), AAWorld.downedBrood);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Amphibious Atrocity", "Defeat the three-headed horror, the Hydra", instance.GetTexture("BlankTex"), AAWorld.downedHydra);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Slithering Snowmongerer", "Defeat the Snow-burrowing Snake, the Subzero Serpent", instance.GetTexture("BlankTex"), AAWorld.downedSerpent);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Sandskrit Sandman", "Defeat majin of magic, the Desert Djinn", instance.GetTexture("BlankTex"), AAWorld.downedDjinn);
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "T O D E", "Defeat the fungal frog, the Truffle Toad", instance.GetTexture("BlankTex"), AAWorld.downedToad);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Shocking", "Destroy any of the S.I.E.G.E. unit bosses", instance.GetTexture("Achievments/Storm"), AAWorld.downedStormAny);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Storming Smackdown", "Destroy all of the S.I.E.G.E. unit bosses", instance.GetTexture("Achievments/Storm"), AAWorld.downedStormAll);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Equinox Eradicator", "Defeat the time-turning worms, the Equinox Duo", instance.GetTexture("Achievments/Equinox"), AAWorld.downedEquinox);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Grip it and rip it", "Rematch the Grips of Chaos in their enhanced, discordian form", instance.GetTexture("Achievments/Grips"), AAWorld.downedGripsS);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Hurricane Horror", "Defeat the Spatial Squid of the Ocean, the Kraken", instance.GetTexture("BlankTex"), AAWorld.downedKraken);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Kraken Slayer", "Defeat the Kraken in Expert mode", instance.GetTexture("BlankTex"), (AAWorld.downedKraken && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Clockwork Catastrophe", "Defeat the destructive doomsday construct, Zero", instance.GetTexture("Achievments/Zero"), AAWorld.downedZero);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Doom Slayer", "Destroy Zero's true, dark form, Zero Protocol", instance.GetTexture("Achievments/ZeroA"), (AAWorld.downedZero && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Trial By Fire", "Defeat the draconian demon of the Inferno, Akuma", instance.GetTexture("Achievments/Akuma"), AAWorld.downedAkuma);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Serpent Slayer", "Slay Akuma's true, blazing form, Akuma Awakened", instance.GetTexture("Achievments/Akuma"), (AAWorld.downedAkuma && Main.expertMode));
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Crescent of Madness", "Defeat the dread nightmare of the Mire, Yamata", instance.GetTexture("BlankTex"), AAWorld.downedYamata);
                DradonIsDum.Call("AddAchievementWithoutReward", this, "Hydra Slayer", "Slay Yamata's true, abyssal form, Yamata Awakened", instance.GetTexture("BlankTex"), (AAWorld.downedYamata && Main.expertMode));
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "Unyielding Discord", "Defeat the discordian doomsayer of chaos, Shen Doragon", instance.GetTexture("BlankTex"), AAWorld.downedShen);
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "Dragon Slayer", "Slay Shen Doragon's true, chaotic form, Shen Doragon Awakened", instance.GetTexture("BlankTex"), (AAWorld.downedShen && Main.expertMode));
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "Endless Nothing", "Destroy the slayer of worlds, Infinity Zero", instance.GetTexture("BlankTex"), AAWorld.downedIZ);
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "Song of the Sea", "Defeat the nautical nightmare, the Soul of Cthulhu", instance.GetTexture("BlankTex"), AAWorld.downedSoC);
                //DradonIsDum.Call("AddAchievementWithoutReward", this, "Endless Nothing", "Triumph over the Soul of Cthulhu's unbound form, Cthulhu; Cosmic Calamity", instance.GetTexture("BlankTex"), AAWorld.downedSoC && Main.expertMode);
            }

        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void Load()
        {
            Textures = (IDictionary<string, Texture2D>)typeof(Mod).GetField("textures", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
            instance = this;
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new CustomCurrency(ItemType<Items.Currency.GoblinSoul>(), 999L));
            if (Main.rand == null)
                Main.rand = new Terraria.Utilities.UnifiedRandom();

            InfinityHotKey = RegisterHotKey("Snap", "G");

            AbilityKey = RegisterHotKey("Armor Ability", "Y");

            if (!Main.dedServ)
            {

                PremultiplyTexture(GetTexture("Backgrounds/VoidBH"));
                PremultiplyTexture(GetTexture("Backgrounds/MireMoon"));
                PremultiplyTexture(GetTexture("Backgrounds/InfernoSun"));
                PremultiplyTexture(GetTexture("Backgrounds/InfernoSky"));
                PremultiplyTexture(GetTexture("Backgrounds/MireSky"));
                PremultiplyTexture(GetTexture("Backgrounds/VoidSky"));
                PremultiplyTexture(GetTexture("Backgrounds/fog"));
                PremultiplyTexture(GetTexture("Backgrounds/AkumaSun"));
                PremultiplyTexture(GetTexture("Backgrounds/YamataMoon"));

                AddEquipTexture(null, EquipType.Legs, "N1_Legs", "AAMod/Items/Vanity/N1/N1_Legs");

                SNAKETYPES = GetWormTypes("MiniSerpent");
                SERPENTTYPES = GetWormTypes("Serpent");

                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanHead(), null, EquipType.Head, "PepsimanHead", "AAMod/Items/Vanity/Pepsi/PepsimanHead");
                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanBody(), null, EquipType.Body, "PepsimanBody", "AAMod/Items/Vanity/Pepsi/PepsimanBody", "AAMod/Items/Vanity/Pepsi/PepsimanBody_Arms");
                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanLegs(), null, EquipType.Legs, "PepsimanLegs", "AAMod/Items/Vanity/Pepsi/PepsimanLegs");

                if (GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch") != 0) //ensure music was loaded!
                {
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch"), ItemType("MonarchBox"), TileType("MonarchBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GripsTheme"), ItemType("GripsBox"), TileType("GripsBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne"), ItemType("N1Box"), TileType("N1Box"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/HydraTheme"), ItemType("HydraBox"), TileType("HydraBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BroodTheme"), ItemType("BroodBox"), TileType("BroodBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface"), ItemType("InfernoBox"), TileType("InfernoBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface"), ItemType("MireBox"), TileType("MireBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground"), ItemType("InfernoUBox"), TileType("InfernoUBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground"), ItemType("MireUBox"), TileType("MireUBox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TODE"), ItemType("TodeBox"), TileType("TodeBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Boss6"), ItemType("SerpentBox"), TileType("SerpentBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Siege"), ItemType("SiegeBox"), TileType("SiegeBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox"), ItemType("Equibox"), TileType("Equibox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Void"), ItemType("VoidBox"), TileType("VoidBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero"), ItemType("ZeroBox"), TileType("ZeroBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Zero2"), ItemType("Zero2Box"), TileType("Zero2Box"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma"), ItemType("AkumaBox"), TileType("AkumaBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2"), ItemType("AkumaABox"), TileType("AkumaABox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata"), ItemType("YamataBox"), TileType("YamataBox"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2"), ItemType("YamataABox"), TileType("YamataABox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Shen"), ItemType("ShenBox"), TileType("ShenBox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA"), ItemType("ShenABox"), TileType("ShenABox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/IZ"), ItemType("IZBox"), TileType("IZBox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RayOfHope"), ItemType("RoHBox"), TileType("RoHBox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand"), ItemType("SABox"), TileType("SABox"));
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium"), ItemType("TerrariumBox"), TileType("TerrariumBox"));
                }

                Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:MireSky"] = new MireSky();
                MireSky.PlanetTexture = GetTexture("Backgrounds/MireMoon");

                Filters.Scene["AAMod:StormSky"] = new Filter(new StormSkyData("FilterMiniTower").UseColor(0.4f, 0f, 0.6f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:StormSky"] = new StormSky();

                Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:VoidSky"] = new VoidSky();
                VoidSky.PlanetTexture = GetTexture("Backgrounds/VoidBH");
                VoidSky.Asteroids1 = GetTexture("Backgrounds/Asteroids1");
                VoidSky.Asteroids2 = GetTexture("Backgrounds/Asteroids2");
                VoidSky.Asteroids3 = GetTexture("Backgrounds/Asteroids3");
                VoidSky.Echo = GetTexture("Backgrounds/Echo");
                VoidSky.LB = GetTexture("Backgrounds/LB");
                VoidSky.boltTexture = GetTexture("Backgrounds/VoidBolt");
                VoidSky.flashTexture = GetTexture("Backgrounds/VoidFlash");

                Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
                SkyManager.Instance["AAMod:InfernoSky"] = new InfernoSky();
                InfernoSky.PlanetTexture = GetTexture("Backgrounds/InfernoSun");

                Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:AkumaSky"] = new AkumaSky();
                AkumaSky.PlanetTexture = GetTexture("Backgrounds/AkumaSun");

                Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();
                YamataSky.PlanetTexture = GetTexture("Backgrounds/YamataMoon");

                UserInterface = new UserInterface();
                Main.itemTexture[1291] = GetTexture("Resprites/LifeFruit");
                Main.itemTexture[1327] = GetTexture("Resprites/DeathSickle");
                Main.itemTexture[3460] = GetTexture("Resprites/Luminite");
                Main.itemTexture[512] = GetTexture("Resprites/SoulOfNight");
                Main.itemTexture[5] = GetTexture("Resprites/Mushroom");
            }
        }

        public int[] GetWormTypes(string s)
        {
            return new int[] { NPCType(s + "Head"), NPCType(s + "Body"), NPCType(s + "Tail") };
        }

        public override void Unload()
        {
            InfinityHotKey = null;
            AbilityKey = null;
        }
        public override void AddRecipeGroups()
        {
            // Registers the new recipe group with the specified name
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Darkmatter Helmet", new int[]
            {
                ItemType<DarkmatterVisor>(),
                ItemType<DarkmatterHelm>(),
                ItemType<DarkmatterHelmet>(),
                ItemType<DarkmatterHeaddress>(),
                ItemType<DarkmatterMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Radium Helmet", new int[]
            {
                ItemType<RadiumHat>(),
                ItemType<RadiumHelm>(),
                ItemType<RadiumHelmet>(),
                ItemType<RadiumHeadgear>(),
                ItemType<RadiumMask>()
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Gold", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("AAMod:Gold", group2);
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Astral Station", new int[]
            {
                ItemType("RadiantArcanum"),
                ItemType("QuantumFusionAccelerator"),
            });
            RecipeGroup.RegisterGroup("AAMod:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Ancient Material", new int[]
            {
                ItemType("UnstableSingularity"),
                ItemType("CrucibleScale"),
                ItemType("DreadScale")
            });
            RecipeGroup.RegisterGroup("AAMod:AncientMaterials", group4);

            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Superancient Material", new int[]
            {
                ItemType("ChaosSoul")
            });
            RecipeGroup.RegisterGroup("AAMod:SuperAncientMaterials", group5);

            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any World Evil Material", new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);

            group6 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " Hardmode Forge", validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group6);

            RecipeGroup group7 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Chaos Claw", new int[]
            {
                ItemType<DragonClaw>(),
                ItemType<HydraClaw>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosClaw", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Iron", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAMod:Iron", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Copper", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAMod:Copper", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Silver", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAMod:Silver", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAMod:EvilBar", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Chaos Bar", new int[]
            {
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Evil/Chaos Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ItemType<IncineriteBar>(),
                ItemType<AbyssiumBar>(),
            });
            RecipeGroup.RegisterGroup("AAMod:EvilorChaosBar", group13);

            RecipeGroup group14 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Ancient Crafting Station", new int[]
            {
                ItemType<BinaryReassembler>(),
                ItemType<ChaosCrucible>()
            });
            RecipeGroup.RegisterGroup("AAMod:ACS", group14);

            RecipeGroup group15 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Evil Summon Staff", new int[]
            {
                ItemType<Items.Summoning.EaterStaff>(),
                ItemType<Items.Summoning.CrimsonStaff>()
            });
            RecipeGroup.RegisterGroup("AAMod:EvilStaff", group15);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ItemType<OroborosWood>());
            }
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.gameMenu)
                return;
            if (priority > MusicPriority.Environment)
                return;
            Player player = Main.LocalPlayer;
            if (!player.active)
                return;
            AAPlayer Ancients = player.GetModPlayer<AAPlayer>();
            bool zoneIZ = Ancients.ZoneVoid && !AAWorld.downedIZ;
            //bool zoneShen = (Ancients.ZoneRisingSunPagoda || Ancients.ZoneRisingMoonLake) && !AAWorld.downedShen;
            //bool zoneSoC = player.ZoneBeach && !AAWorld.downedSoC;
            if (AkumaMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (YamataMusic == true)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata2");

                priority = MusicPriority.BossHigh;
                return;
            }
            if (AAWorld.downedZero && zoneIZ)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepingGiant");
                return;
            }
            if (Ancients.Terrarium)
            {

                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Terrarium");

                return;
            }
            if (Ancients.ZoneVoid)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Void");
                return;
            }
            if (Ancients.ZoneShip)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Ship");
                return;
            }
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (player.HeldItem.type == ItemType("Sax"))
                {

                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne");

                    priority = MusicPriority.BossHigh;

                    return;
                }
            }
            
            if (Ancients.ZoneStorm)
            {
                priority = MusicPriority.Event;
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Maelstrom");
                return;
            }
            if (Ancients.ZoneInferno)
            {
                if (Ancients.ZoneRisingSunPagoda && AAWorld.downedEquinox && !AAWorld.downedAkuma)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/AkumaShrine");
                    return;
                }
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoUnderground");
                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/InfernoSurface");
                    return;
                }
            }
            if (Ancients.ZoneMire)
            {
                if (Ancients.ZoneRisingMoonLake && AAWorld.downedEquinox && !AAWorld.downedYamata)
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Shrines");
                    return;
                }
                if (player.ZoneRockLayerHeight)
                {
                    priority = MusicPriority.BiomeMedium;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireUnderground");

                    return;
                }
                else
                {
                    priority = MusicPriority.BiomeHigh;
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MireSurface");

                    return;
                }
            }
            if (Ancients.ZoneMush)
            {
                music = MusicID.Mushrooms;

                return;
            }
            
        }

        #region recipes
        public override void AddRecipes()
        {
            RecipeFinder finder = new RecipeFinder();
            {
                finder = new RecipeFinder();
                finder.AddIngredient(ItemID.BloodButcherer, 1);
                finder.AddIngredient(ItemID.FieryGreatsword, 1);
                finder.AddIngredient(ItemID.BladeofGrass, 1);
                finder.AddIngredient(ItemID.Muramasa, 1);
                finder.AddTile(TileID.DemonAltar);
                finder.SetResult(ItemID.NightsEdge, 1);
                Recipe recipe2 = finder.FindExactRecipe();
                if (recipe2 != null)
                {
                    RecipeEditor editor = new RecipeEditor(recipe2);
                    editor.DeleteRecipe();
                }
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "HallowedOre", 4);
                recipe.AddTile(null, "HallowedForge");
                recipe.SetResult(ItemID.HallowedBar, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddIngredient(ItemID.Hay, 5);
                recipe.AddTile(null, "HellstoneAnvil");
                recipe.SetResult(ItemID.GuideVoodooDoll, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.TrueExcalibur, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.TerraBlade, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.IronBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Wood, 30);
                recipe.AddIngredient(ItemID.LeadBar, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBox, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GrassSeeds, 10);
                recipe.AddIngredient(ItemID.DirtBlock, 10);
                recipe.AddIngredient(ItemID.Wood, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltOverworldDay, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Lens, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxNight, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BottledWater, 5);
                recipe.AddIngredient(ItemID.UmbrellaHat, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxRain, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SnowBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSnow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddIngredient(ItemID.BorealWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxIce, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SandBlock, 40);
                recipe.AddIngredient(ItemID.Cactus, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDesert, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
                recipe.AddIngredient(ItemID.SharkFin, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSandstorm, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Coral, 3);
                recipe.AddIngredient(ItemID.Starfish, 3);
                recipe.AddIngredient(ItemID.Seashell, 3);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxOcean, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.LeadOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DirtBlock, 50);
                recipe.AddIngredient(ItemID.IronOre, 10);
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxAltUnderground, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Feather, 20);
                recipe.AddIngredient(ItemID.SunplateBlock, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxSpace, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GlowingMushroom, 20);
                recipe.AddIngredient(ItemID.Mushroom, 10);
                recipe.AddIngredient(ItemID.MushroomGrassSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMushrooms, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MudBlock, 20);
                recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
                recipe.AddIngredient(ItemID.RichMahogany, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxJungle, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddIngredient(ItemID.CorruptSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.EbonstoneBlock, 30);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCorruption, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddIngredient(ItemID.CrimsonSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrimstoneBlock, 30);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundCrimson, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.CrystalShard, 10);
                recipe.AddIngredient(ItemID.HallowedSeeds, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTheHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PearlstoneBlock, 30);
                recipe.AddIngredient(ItemID.UnicornHorn, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxUndergroundHallow, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.AshBlock, 20);
                recipe.AddIngredient(ItemID.Hellstone, 15);
                recipe.AddIngredient(ItemID.ObsidianBrick, 10);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxHell, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BlueBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GreenBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PinkBrick, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDungeon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TempleKey, 1);
                recipe.AddIngredient(ItemID.LihzahrdBrick, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTemple, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 15);
                recipe.AddIngredient(ItemID.DemoniteBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofFright, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss1, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
                recipe.AddIngredient(null, "DevilSilk", 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofSight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.TissueSample, 15);
                recipe.AddIngredient(ItemID.CrimtaneBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.SoulofMight, 10);
                recipe.AddIngredient(ItemID.HallowedBar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss3, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeetleHusk, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss4, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.BeeWax, 20);
                recipe.AddIngredient(ItemID.BottledHoney, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxBoss5, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddIngredient(null, "PlanteraPetal", 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPlantera, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Meteorite, 20);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.Shackle, 1);
                recipe.AddIngredient(ItemID.MoneyTrough, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEerie, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxEclipse, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.GoblinBattleStandard, 1);
                recipe.AddIngredient(ItemID.SpikyBall, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxGoblins, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PirateMap, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPirates, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.MartianConduitPlating, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxMartians, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.PumpkinMoonMedallion, 30);
                recipe.AddIngredient(ItemID.SpookyWood, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxPumpkinMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.NaughtyPresent, 1);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxFrostMoon, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 3);
                recipe.AddIngredient(ItemID.FragmentSolar, 3);
                recipe.AddIngredient(ItemID.FragmentVortex, 3);
                recipe.AddIngredient(ItemID.FragmentStardust, 3);
                recipe.AddIngredient(ItemID.FallenStar, 5);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxTowers, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.LunarOre, 30);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxLunarBoss, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.MusicBox, 1);
                recipe.AddIngredient(ItemID.DefenderMedal, 15);
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(ItemID.MusicBoxDD2, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Glass, 10);
                recipe.AddIngredient(ItemID.SnowBlock, 10);
                recipe.AddRecipeGroup("Wood");
                recipe.AddTile(TileID.GlassKiln);
                recipe.SetResult(ItemID.SnowGlobe, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.SnowGlobe, 1);
                recipe.AddIngredient(ItemID.SoulofFlight, 5);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.SoulofLight, 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.GravityGlobe, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(null, "MushiumBar", 1);
                recipe.AddIngredient(ItemID.GlowingMushroom, 5);
                recipe.AddTile(TileID.Autohammer);
                recipe.SetResult(ItemID.ShroomiteBar, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Deathweed, 1);
                recipe.AddIngredient(null, "DragonClaw", 3);
                recipe.AddIngredient(null, "DragonScale", 1);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.RagePotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Deathweed, 1);
                recipe.AddIngredient(null, "HydraClaw", 3);
                recipe.AddIngredient(null, "MirePod", 1);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.WrathPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Waterleaf, 1);
                recipe.AddIngredient(null, "MirePod", 2);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.WaterWalkingPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddIngredient(ItemID.Waterleaf, 1);
                recipe.AddIngredient(null, "DragonScale", 2);
                recipe.AddTile(TileID.Bottles);
                recipe.SetResult(ItemID.ObsidianSkinPotion, 1);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.Glass, 10);
                recipe.AddIngredient(ItemID.RecallPotion, 10);
                recipe.AddTile(TileID.GlassKiln);
                recipe.SetResult(ItemID.MagicMirror);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IceBrick, 10);
                recipe.AddIngredient(ItemID.RecallPotion, 10);
                recipe.AddTile(TileID.IceMachine);
                recipe.SetResult(ItemID.IceMirror);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(ItemID.IceBlock, 30);
                recipe.AddIngredient(ItemID.Diamond, 1);
                recipe.AddIngredient(ItemID.Sapphire, 1);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(ItemID.IceBlade);
                recipe.AddRecipe();
            }
    }
        #endregion

        //Stuff 4 Grox

        public override object Call(params object[] args)
        {
            if (args.Length <= 0 || !(args[0] is string)) return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");
            string methodName = (string)args[0];
            if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on player and name.
            {
                Func<string, bool> downed = (name) =>
                {
                    name = name.ToLower();
                    switch (name)
                    {
                        default: return false;
                        case "mushroommonarch": return AAWorld.downedMonarch;
                        case "broodmother": return AAWorld.downedBrood;
                        case "hydra": return AAWorld.downedHydra;
                        case "grips":
                        case "gripsofchaos": return AAWorld.downedGrips;
                        case "tode": return AAWorld.downedToad;
                        case "retriever": return AAWorld.downedRetriever;
                        case "orthrus": return AAWorld.downedOrthrus;
                        case "raider": return AAWorld.downedRaider;
                        case "stormany": return AAWorld.downedStormAny;
                        case "stormall": return AAWorld.downedStormAll;
                        case "daybringer": return AAWorld.downedDB;
                        case "nightcrawler": return AAWorld.downedNC;
                        case "equinox": return AAWorld.downedEquinox;
                        case "ancient":
                        case "ancientany": return AAWorld.downedAncient;
                        case "sancient":
                        case "sancientany": return AAWorld.downedSAncient;
                        case "gripsS":
                        case "discordgrips": return AAWorld.downedGripsS;
                        case "kraken": return AAWorld.downedKraken;
                        case "akuma": return AAWorld.downedAkuma;
                        case "yamata": return AAWorld.downedYamata;
                        case "zero": return AAWorld.downedZero;
                        case "shen":
                        case "shendoragon": return AAWorld.downedShen;
                        case "iz":
                        case "infinityzero": return AAWorld.downedIZ;
                        case "soc":
                        case "soulofcthulhu": return AAWorld.downedSoC;
                    }
                };
                return downed;
            }
            else
            if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
            {
                Func<Player, string, bool> inZone = (p, name) =>
                {
                    name = name.ToLower();
                    AAPlayer aap = p.GetModPlayer<AAPlayer>();
                    switch (name)
                    {
                        default: return false;
                        case "mire": return aap.ZoneMire;
                        case "lake": return aap.ZoneRisingMoonLake;
                        case "inferno": return aap.ZoneInferno;
                        case "pagoda": return aap.ZoneRisingSunPagoda;
                        case "ship": return aap.ZoneShip;
                        case "storm": return aap.ZoneStorm;
                        case "void": return aap.ZoneVoid;
                        case "mush": return aap.ZoneMush;
                        case "terrarium": return aap.Terrarium;
                    }
                };
                return inZone;
            }
            return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD FOUND: " + methodName);
        }
    }
}
