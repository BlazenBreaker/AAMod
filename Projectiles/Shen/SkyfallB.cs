using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Shen
{
    public class SkyfallB : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skyfall");

            Main.projFrames[projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 5;
            projectile.penetrate = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.alpha = 255;
        }


        public bool EnemyHit = false;
        public bool TileHit = false;

        public override void AI()
        {
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.position.X / 16, (int)projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust19.position = projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust18.position = projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            TileHit = true;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            EnemyHit = true;
            target.AddBuff(mod.BuffType<Buffs.DiscordInferno>(), 600);
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, 1, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 30, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MeteorStrikeBlue"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].ranged = true;
            }
            if (EnemyHit)
            {
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 30, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MeteorBoomBlue"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].ranged = true;
            }
        }
    }
}