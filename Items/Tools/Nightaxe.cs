using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Nightaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 15;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useAnimation = 23;
            item.useTime = 10;
            item.pick = 110;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 1, 8, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightaxe");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NightmarePickaxe);
            recipe.AddIngredient(mod, "Grasscutter");
            recipe.AddIngredient(mod, "Toothpick");
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
