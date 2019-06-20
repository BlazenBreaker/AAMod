using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class SporeSac : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.width = 22;
            item.height = 26;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 28;
			item.useAnimation = 28;
			item.shoot = mod.ProjectileType("SPORZ");
			item.shootSpeed = 1f;
			item.useStyle = 1;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Bag");
			Tooltip.SetDefault(@"Spreads the surface mushroom biome");
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.shoot = mod.ProjectileType<Projectiles.Flasks.SporeSac>();
                item.shootSpeed = 9f;
            }
            else
            {
                item.shoot = mod.ProjectileType("SPORZ");
                item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == mod.ProjectileType("Flask"))
            {
                Projectile.NewProjectile(position, new Microsoft.Xna.Framework.Vector2(speedX, speedY), type, 0, 0, Main.myPlayer, 9);
                return false;
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

    }
}
