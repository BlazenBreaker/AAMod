using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueDoomite
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueDoomiteBreastplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Overload Doomite Chestplate");
			Tooltip.SetDefault(@"15% increased minion damage
Increases your max number of minions");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 5, 0, 0);
			item.rare = 7;
			item.defense = 20;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.15f;
            player.maxMinions += 1;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DoomiteBreastplate"));
			recipe.AddIngredient(null, "VoidCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}