using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class BunnySummon3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunny Summon");
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            projectile.damage = 0;
            projectile.knockBack = 0;
            Move(new Vector2(projectile.ai[0], projectile.ai[1]));
            if (Vector2.Distance(projectile.Center, new Vector2(projectile.ai[0], projectile.ai[1])) < 10)
            {
                Kill(projectile.timeLeft);
            }
        }

        public override void Kill(int timeLeft)
        {
            int Minion = NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType<BunnyBattler>(), 0);
            Main.npc[Minion].netUpdate2 = true;
            if (Main.netMode == 2 && Minion < 200)
            {
                NetMessage.SendData(23, -1, -1, null, Minion, 0f, 0f, 0f, 0, 0, 0);
            }
            projectile.active = false;
            projectile.netUpdate2 = true;
        }

        public void Move(Vector2 point)
        {
            float Speed = 13;

            float velMultiplier = 1f;
            Vector2 dist = point - projectile.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < Speed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / Speed);
            }
            if (length < 200f)
            {
                Speed *= 0.5f;
            }
            if (length < 100f)
            {
                Speed *= 0.5f;
            }
            if (length < 50f)
            {
                Speed *= 0.5f;
            }
            projectile.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            projectile.velocity *= Speed;
            projectile.velocity *= velMultiplier;
        }
    }
}
