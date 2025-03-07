using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using AAMod.NPCs.Bosses.Akuma;
using System.Collections.Generic;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DraconianRune : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Sun Rune");
            Tooltip.SetDefault(@"An enchanted tablet bursting with flaming chaotic energy
Summons Akuma Awakened
Only Usable during the day in the inferno
Non-Consumable");
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
                    line2.overrideColor = new Color(0, 191, 255);
                }
            }
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.", new Color(180, 41, 32), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                if (!player.GetModPlayer<AAPlayer>(mod).ZoneRisingSunPagoda && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("An image of the scorched tower at the peak of the inferno flashes through your mind", Color.Indigo, false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Hey kid, that rune only works once, ya know.", new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Hey kid, that rune only works once, ya know.", new Color(0, 191, 255), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("AkumaTransition")))
                {
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("You can only use that rune in the Inferno, kid.", new Color(180, 41, 32), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode != 1) BaseUtility.Chat("The air around you begins to heat up...", new Color(175, 75, 255));
            if (Main.netMode != 1) BaseUtility.Chat("Cutting right to the chase I see..? Alright then, prepare for hell..!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("AkumaA"), false, 0, 0, "Akuma, Draconian Demon", false);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }
    }
}