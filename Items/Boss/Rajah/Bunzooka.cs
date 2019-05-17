using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah
{

    public class Bunzooka : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunzooka");
            Tooltip.SetDefault("Fires Rabbit Rockets");
        }

        public override void SetDefaults()
        {
            item.damage = 220;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7.5f;
            item.value = 5000000;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("RabbitRocket3");
            item.useAmmo = AmmoID.Rocket;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, 200);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("RabbitRocket3"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}