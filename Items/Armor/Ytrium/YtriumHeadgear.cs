using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Ytrium
{
    [AutoloadEquip(EquipType.Head)]
    public class YtriumHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yttrium Headgear");
            Tooltip.SetDefault(@"+50 Max Mana
5% increased magic critical strike chance
8% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 70000;
            item.rare = 4;
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
            player.statManaMax2 += 50;
            player.moveSpeed *= 1.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("YtriumPlate") && legs.type == mod.ItemType("YtriumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"You can do a lightning-quick dash.";
            player.dash = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "YtriumBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}