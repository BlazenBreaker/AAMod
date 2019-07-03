using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Terratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
			item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 3;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 60;
            item.pick = 215;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                AAMod.instance.TerratoolState.ToggleUI(AAMod.instance.TerratoolInterface);
                item.pick = UI.TerratoolUI.Pick;
                item.axe = UI.TerratoolUI.Axe;
                item.hammer = UI.TerratoolUI.Hammer;
                return true;
            }
            else
            {
                // do stuff
            }

            return false;
        }

        public override void AddRecipes()  //How to craft this item
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TrueNightaxe");
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {

                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TrueScalpel");
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
