using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class CarrotDust : ModDust
	{
        public override void SetDefaults()
        {
            Main.dust[Type].noGravity = false;
        }
        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            return false;
        }
    }
}