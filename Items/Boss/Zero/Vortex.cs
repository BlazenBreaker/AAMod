using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Vortex : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Vortex");
            Tooltip.SetDefault(@"Spins fast enough to drag all enemies into its gravitational pull");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Terrarian);
            item.damage = 250;                            
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 2;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shoot = mod.ProjectileType("Vortex");
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.Terrarian);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
