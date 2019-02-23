using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Ytrium
{
    [AutoloadEquip(EquipType.Body)]
    public class YtriumPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ytrium Chestplate");
            Tooltip.SetDefault(@"8% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 70000;
            item.rare = 4;
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.08f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}