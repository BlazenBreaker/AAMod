using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Rajah;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.Items.BossSummons
{
    public class DiamondCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ten Carat Carrot");
            Tooltip.SetDefault(@"The fury of the Raging Rajah can be felt radiating from this ornate carrot...");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = 2;
            item.maxStack = 20;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.noUseGraphic = true;
            item.consumable = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah");
        }

        public override bool CanUseItem(Player player)
        {
            return AAModGlobalNPC.Rajah != -1;
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("GRAVE MISTAKE, TERRARIAN!", 107, 137, 179);
            int overrideDirection = (Main.rand.Next(2) == 0 ? -1 : 1);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("SupremeRajah"), false, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), "Rajah Rabbit");
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldenCarrot", 1);
            recipe.AddIngredient(null, "RoyalRabbit", 1);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}