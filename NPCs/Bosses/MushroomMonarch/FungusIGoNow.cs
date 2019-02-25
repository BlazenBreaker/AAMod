using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    public class FungusIGoNow: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feudal Fungus");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.damage = 24;
            projectile.width = 74;
            projectile.height = 80;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
            projectile.alpha = 0;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            projectile.velocity *= 0;

            if (projectile.velocity.X <= .1f && projectile.velocity.X >= -.1f)
            {
                projectile.velocity.X = 0;
            }
            if (projectile.velocity.Y <= .1f && projectile.velocity.Y >= -.1f)
            {
                projectile.velocity.Y = 0;
            }
            projectile.alpha -= 10;
            if (projectile.alpha >= 255)
            {
                projectile.active = false;
            }
        }

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/FungusIGoNow_Glow");
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile, AAColor.Glow);
        }
    }
}