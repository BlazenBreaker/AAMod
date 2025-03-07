﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class HydraSpray : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spray");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 6;
            projectile.extraUpdates = 2;
            projectile.magic = true;
        }

        public override void AI()
        {
        	Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.01f / 255f, (255 - projectile.alpha) * 0.05f / 255f, (255 - projectile.alpha) * 0.15f / 255f);
			projectile.scale -= 0.002f;
			if (projectile.scale <= 0f)
			{
				projectile.Kill();
			}
			if (projectile.ai[0] <= 3f)
			{
				projectile.ai[0] += 1f;
				return;
			}
			projectile.velocity.Y = projectile.velocity.Y + 0.075f;
			for (int num151 = 0; num151 < 3; num151++)
			{
				float num152 = projectile.velocity.X / 3f * num151;
				float num153 = projectile.velocity.Y / 3f * num151;
				int num154 = 14;
				int num155 = Dust.NewDust(new Vector2(projectile.position.X + num154, projectile.position.Y + num154), projectile.width - num154 * 2, projectile.height - num154 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100);
				Main.dust[num155].noGravity = true;
				Main.dust[num155].velocity *= 0.1f;
				Main.dust[num155].velocity += projectile.velocity * 0.5f;
				Dust expr_6A04_cp_0 = Main.dust[num155];
				expr_6A04_cp_0.position.X -= num152;
				Dust expr_6A1F_cp_0 = Main.dust[num155];
				expr_6A1F_cp_0.position.Y -= num153;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num156 = 16;
				int num157 = Dust.NewDust(new Vector2(projectile.position.X + num156, projectile.position.Y + num156), projectile.width - num156 * 2, projectile.height - num156 * 2, mod.DustType<Dusts.HydraDust>(), 0f, 0f, 100, default, 0.5f);
				Main.dust[num157].velocity *= 0.25f;
				Main.dust[num157].velocity += projectile.velocity * 0.5f;
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.immune[projectile.owner] = 8;
        	target.AddBuff(BuffID.Poisoned, 200);
        }
    }
}