using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Depthsprayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Depthsprayer");
			Tooltip.SetDefault("Covers enemies in Hydratoxin");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.mana = 9;
			item.autoReuse = true;
			item.useStyle = 5;
			item.useAnimation = 15;
			item.useTime = 5;
			item.knockBack = 4f;
			item.width = 38;
			item.height = 10;
			item.damage = 23;
			item.shoot = mod.ProjectileType("Depthsprayer");
			item.shootSpeed = 12f;
			item.UseSound = SoundID.Item13;
			item.rare = 4;
			item.value = 250000;
			item.magic = true;
			item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("OceanTrident"));
			recipe.AddIngredient(mod.ItemType("BlazePike"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}