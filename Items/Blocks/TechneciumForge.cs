using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TechneciumForge : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Forge");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 7;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150000;
            item.createTile = mod.TileType("TechneciumForge");
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TechneciumOre", 30);
                recipe.AddIngredient(ItemID.Hellforge, 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
