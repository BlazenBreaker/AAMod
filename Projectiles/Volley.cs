using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Volley : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volley");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 2;
            projectile.timeLeft = 600;
            projectile.light = 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool PreAI()
        {
            if (projectile.frameCounter++ >= 9)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }


        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 6,
                    projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}
