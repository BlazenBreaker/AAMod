using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Battery : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Unstable Power Cell");
            Tooltip.SetDefault(@"Acts as a bullet
Non-consumable");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 10));
        }

        public override void SetDefaults()
		{
			item.damage = 26;
			item.ranged = true;
			item.width = 20;
			item.height = 32;
			item.consumable = false;
			item.knockBack = 7f;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.rare = 6;
			item.shoot = mod.ProjectileType("RealityLaser");
			item.shootSpeed = 1f;
			item.ammo = AmmoID.Bullet;
            item.rare = 9; AARarity = 13;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MoonlordBullet, 999);
            recipe.AddIngredient(null, "ApocalyptitePlate", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 1);
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
