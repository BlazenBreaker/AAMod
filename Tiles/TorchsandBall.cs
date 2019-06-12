using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class TorchsandBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torchsand Ball");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.damage = 0;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.tileCollide = true;
            projectile.aiStyle = 10;
            aiType = ProjectileID.SandBallFalling;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override void Kill(int timeLeft)
        {
            WorldGen.PlaceTile((int)(projectile.position.X / 16), (int)(projectile.position.Y / 16), mod.TileType<Torchsand>());
        }
    }
}