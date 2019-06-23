using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class CometBar : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.value = 16000;
            item.rare = 2;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("CometBar");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comet Bar");
            Tooltip.SetDefault("Said to be a piece of the sky itself");
        }

		public override void AddRecipes()
        {  
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CometOre", 3);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
