using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Haruka
{
    public class HarukaKunai : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 34;
			projectile.friendly = false;
            projectile.hostile = true;
			projectile.timeLeft = 1200;
			projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.aiStyle = -1;
		}

        public override void AI()
        {
            BaseMod.BaseAI.AIThrownWeapon(projectile, ref projectile.ai, false, 40);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Kunai");
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), 180);
            projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
			     Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType<Dusts.CthulhuAuraDust>(), projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 0);
			
		}
	}
}