using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Akuma;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class DraconianSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Sun Sigil");
            Tooltip.SetDefault(@"An ornate tablet said to contain the radiant power of a thousand suns
Summons Akuma
Only Usable during the day");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(180, 41, 32);
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.", new Color(180, 41, 32), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                if (NPC.AnyNPCs(mod.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey kid, that Sigil only works once, ya know.", new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey kid, that Sigil only works once, ya know.", new Color(0, 191, 255), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You can only use that Sigil in the Inferno, kid.", new Color(180, 41, 32), false);
            return false;
        }

        public override bool UseItem(Player player)
        {

            if (!AAWorld.downedAkuma && !Main.expertMode)
            {
                Main.NewText("Heh, I hope you’re ready to feel the fury of the blazing sun kid.", new Color(180, 41, 32));
            }
            if (!AAWorld.downedAkumaA && Main.expertMode)
            {
                Main.NewText("Heh, I hope you’re ready to feel the fury of the blazing sun kid.", new Color(180, 41, 32));
            }
            if (!Main.expertMode && AAWorld.downedAkuma)
            {
                Main.NewText("Back for more, kid? Don’t you have better things to do? You already beat me once.  Alright, but I won’t go easy on you.", new Color(180, 41, 32));
            }
            if (Main.expertMode && AAWorld.downedAkumaA)
            {
                Main.NewText("Back for more, kid? Don’t you have better things to do? You already beat me once.  Alright, but I won’t go easy on you.", new Color(180, 41, 32));
            }

            SpawnBoss(player, "Akuma", "Akuma; Draconian Demon");
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 10);
            recipe.AddIngredient(null, "RadiumBar", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}