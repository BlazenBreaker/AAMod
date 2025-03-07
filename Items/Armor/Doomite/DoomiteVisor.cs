using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Head)]
    public class DoomiteVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Visor");
            Tooltip.SetDefault(@"+1 Minion slot");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 4;
            item.defense = 6;
            item.value = 9000;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DoomiteBreastplate") && legs.type == mod.ItemType("DoomiteGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"+1 Max Minion
A void searcher fights by your side";
            player.maxMinions += 1;
            player.GetModPlayer<AAPlayer>(mod).doomite = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("Searcher")) == -1)
                {
                    player.AddBuff(mod.BuffType("Searcher"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("Searcher")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Searcher"), 30, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}