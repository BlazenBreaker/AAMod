using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Summoning
{
    public class TerraGauntlet : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 60;
            item.noMelee = true;
            item.summon = true;
            item.width = 18;
            item.height = 42;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("Minion1");
            item.buffType = mod.BuffType("TerraSummon");
            item.knockBack = 2;
            item.rare = 8;
            item.UseSound = SoundID.Item44;
            item.autoReuse = false;
            item.shootSpeed = 1f;
            item.mana = 10;
        }

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
		
        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Terra Gauntlet");
              Tooltip.SetDefault(@"Summons a Terra Squid, Terra Sphere, Terra Crawler, or Terra Weaver to Fight for you");
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int shootMe = Main.rand.Next(4);
            switch (shootMe)
            {
                case 0:
                    shootMe = mod.ProjectileType("Minion1");
                    break;
                case 1:
                    shootMe = mod.ProjectileType("Minion2");
                    break;
                case 2:
                    shootMe = mod.ProjectileType("Minion3");
                    break;
                case 3:
                    shootMe = mod.ProjectileType("Minion4Head");
                    break;
            }
            if (shootMe == mod.ProjectileType<Minions.Terra.Minion4Head>())
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);

                int head = -1;
                int tail = -1;
                for (int j = 0; j < Main.projectile.Length; j++)
                {
                    if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer)
                    {
                        if (head == -1 && Main.projectile[j].type == mod.ProjectileType("Minion4Head"))
                        {
                            head = j;
                        }
                        if (tail == -1 && Main.projectile[j].type == mod.ProjectileType("Minion4Tail"))
                        {
                            tail = j;
                        }
                        if (head != -1 && tail != -1)
                        {
                            break;
                        }
                    }
                }
                if (head == -1 && tail == -1)
                {
                    vector2.X = Main.mouseX + Main.screenPosition.X;
                    vector2.Y = Main.mouseY + Main.screenPosition.Y;

                    int current = Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("Minion4Head"), damage, knockBack, Main.myPlayer);

                    int previous = current;
                    for (int k = 0; k < 4; k++)
                    {
                        current = Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("Minion4Body"), damage, knockBack, Main.myPlayer, previous);
                        previous = current;
                    }

                    previous = current;
                    current = Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("Minion4Tail"), damage, knockBack, Main.myPlayer, previous);
                    Main.projectile[previous].localAI[1] = current;
                    Main.projectile[previous].netUpdate = true;
                }
                return false;
            }
            int i = Main.myPlayer;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector;
            int num78 = 0;
            int num79 = 0;
            vector.X = Main.mouseX + Main.screenPosition.X;
            vector.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector.X, vector.Y, num78, num79, shootMe, num73, num74, i, 0f, 0f);
            return false;
        }
    }
}
