using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Legs)]
	public class DynaskullGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dynaskull Greaves");
            Tooltip.SetDefault("10% Increased ranged critical chance");

        }

		public override void SetDefaults()
		{
            item.width = 30;
			item.height = 28;
			item.value = 90000;
			item.rare = 4;
			item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FossilPants, 1);
            recipe.AddIngredient(null, "DynaskullOre", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}