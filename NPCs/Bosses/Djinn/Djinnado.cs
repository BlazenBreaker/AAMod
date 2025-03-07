using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Djinn
{
    public class Djinnado : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Djinnado");
		}

		public override void SetDefaults()
		{;
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 127;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
        }

        public override void AI()
        {
            float num1123 = 300f;
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = -1;
                Main.PlaySound(SoundID.Item82, projectile.Center);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= num1123)
            {
                projectile.Kill();
            }
            float num1125 = 15f;
            float num1126 = 15f;
            Point point8 = projectile.Center.ToTileCoordinates();
            int num1127;
            int num1128;
            Collision.ExpandVertically(point8.X, point8.Y, out num1127, out num1128, (int)num1125, (int)num1126);
            num1127++;
            num1128--;
            Vector2 value72 = new Vector2((float)point8.X, (float)num1127) * 16f + new Vector2(8f);
            Vector2 value73 = new Vector2((float)point8.X, (float)num1128) * 16f + new Vector2(8f);
            Vector2 vector145 = Vector2.Lerp(value72, value73, 0.5f);
            Vector2 value74 = new Vector2(0f, value73.Y - value72.Y);
            value74.X = value74.Y * 0.2f;
            projectile.width = (int)(value74.X * 0.65f);
            projectile.height = (int)value74.Y;
            projectile.Center = vector145;
            if (projectile.ai[0] < num1123 - 120f)
            {
                for (int num1131 = 0; num1131 < 1; num1131++)
                {
                    float value75 = -0.5f;
                    float value76 = 0.9f;
                    float amount3 = Main.rand.NextFloat();
                    Vector2 value77 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value75, value76, amount3));
                    value77.X *= MathHelper.Lerp(2.2f, 0.6f, amount3);
                    value77.X *= -1f;
                    Vector2 value78 = new Vector2(6f, 10f);
                    Vector2 position3 = vector145 + value74 * value77 * 0.5f + value78;
                    Dust dust34 = Main.dust[Dust.NewDust(position3, 0, 0, 269, 0f, 0f, 0, default, 1f)];
                    dust34.position = position3;
                    dust34.customData = vector145 + value78;
                    dust34.fadeIn = 1f;
                    dust34.scale = 0.3f;
                    if (value77.X > -1.2f)
                    {
                        dust34.velocity.X = 1f + Main.rand.NextFloat();
                    }
                    dust34.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
                }
                return;
            }
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188));
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
