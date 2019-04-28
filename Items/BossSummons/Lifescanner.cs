using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class Lifescanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lifescanner");
            Tooltip.SetDefault(@"Summons Sagittarius
Can only be used in the Void");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(mod, player, "Sagittarius");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Lifescanner doesn't do anything.", new Color(216, 60, 0), false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Sagittarius.Sagittarius>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Lifescanner doesn't do anything.", new Color(216, 60, 0), false);
                return false;
            }
            return true;
        }
        

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}